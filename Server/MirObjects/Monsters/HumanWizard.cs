using Server.MirDatabase;
using System.Numerics;
using S = ServerPackets;

namespace Server.MirObjects.Monsters
{
    public class HumanWizard : MonsterObject
    {
        public long FearTime, DecreaseMPTime;
        public byte AttackRange = 6;
        public bool Summoned;

        protected internal HumanWizard(MonsterInfo info)
            : base(info)
        {
            Direction = MirDirection.Down;
            Summoned = true;
        }

        protected override bool InAttackRange()
        {
            return CurrentMap == Target.CurrentMap && Functions.InRange(CurrentLocation, Target.CurrentLocation, AttackRange);
        }

        protected override void Attack()
        {
            if (!Target.IsAttackTarget(this))
            {
                Target = null;
                return;
            }
            
            ShockTime = 0;

            // [hack] 分身的攻击方式为随机选择法术中的一种进行攻击
            List<Spell> spells = [Spell.ThunderBolt,Spell.FireWall, Spell.ThunderStorm, Spell.IceStorm];

            Direction = Functions.DirectionFromPoint(CurrentLocation, Target.CurrentLocation);
            Broadcast(new S.ObjectMagic { 
                ObjectID = ObjectID, 
                Direction = Direction, 
                Location = CurrentLocation, 
                Spell = spells[Envir.Random.Next(spells.Count)], 
                TargetID = Target.ObjectID, 
                Target = Target.CurrentLocation, 
                Cast = true, 
                Level = 3 });

            ActionTime = Envir.Time + 300;
            AttackTime = Envir.Time + AttackSpeed;

            // [hack] 根据玩家的职业调整分身的攻击力计算伤害
            PlayerObject player = Master as PlayerObject;
            int damage = 0;
            if (player != null)
            {
                List<int> min_attack = [player.Stats[Stat.MinDC], player.Stats[Stat.MinMC], player.Stats[Stat.MinSC]];
                List<int> max_attack = [player.Stats[Stat.MaxDC], player.Stats[Stat.MaxMC], player.Stats[Stat.MaxSC]];
                int min_limit = min_attack[(int)player.Class];
                int max_limit = max_attack[(int)player.Class];
                damage = GetAttackPower(min_limit, max_limit);
            }
            else
                damage = GetAttackPower(Stats[Stat.MinDC], Stats[Stat.MaxDC]);
            if (damage == 0) return;

            DelayedAction action = new DelayedAction(DelayedType.Damage, Envir.Time + 500, Target, damage, DefenceType.MAC);
            ActionList.Add(action);
        }

        protected override void ProcessAI()
        {
            base.ProcessAI();

            if (Master != null && Master is PlayerObject && Envir.Time > DecreaseMPTime)
            {
                DecreaseMPTime = Envir.Time + 1000;
                if (!Master.Dead) ((PlayerObject)Master).ChangeMP(-10);

                if (((PlayerObject)Master).MP <= 0) Die();
            }
        }

        protected override void ProcessTarget()
        {
            if (Target == null || !CanAttack) return;

            if (Master != null)
            {
                //MoveTo(Master.CurrentLocation);
                // [hack] 分身会自动移动到主人的位置附近而不是每次都移动到主人所在的确切位置，避免影响主人的移动
                int x = Envir.Random.Next(-2, 2);
                int y = Envir.Random.Next(-2, 2);
                MoveTo(new System.Drawing.Point(Master.CurrentLocation.X + x, Master.CurrentLocation.Y + y));
            }

            if (InAttackRange() && (Master != null || Envir.Time < FearTime))
            {
                Attack();
                return;
            }

            FearTime = Envir.Time + 5000;

            if (Envir.Time < ShockTime)
            {
                Target = null;
                return;
            }

            int dist = Functions.MaxDistance(CurrentLocation, Target.CurrentLocation);

            if (dist < AttackRange)
            {
                MirDirection dir = Functions.DirectionFromPoint(Target.CurrentLocation, CurrentLocation);

                if (Walk(dir)) return;

                switch (Envir.Random.Next(2)) //No favour
                {
                    case 0:
                        for (int i = 0; i < 7; i++)
                        {
                            dir = Functions.NextDir(dir);

                            if (Walk(dir))
                                return;
                        }
                        break;
                    default:
                        for (int i = 0; i < 7; i++)
                        {
                            dir = Functions.PreviousDir(dir);

                            if (Walk(dir))
                                return;
                        }
                        break;
                }
            }
        }

        public override void Spawned()
        {
            base.Spawned();
            Summoned = false;
        }

        public override void ChangeHP(int amount)
        {
            if (Master != null && Master is PlayerObject)
            {
                ((PlayerObject)Master).ChangeMP(amount);
                return;
            }
            base.ChangeHP(amount);
        }

        public override void Die()
        {
            if (Dead) return;

            HP = 0;
            Dead = true;

            //DeadTime = Envir.Time + DeadDelay;
            DeadTime = 0;

            Broadcast(new S.ObjectDied { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Type = (byte)(Master != null ? 1 : 0) });

            if (EXPOwner != null && EXPOwner.Node != null && Master == null && EXPOwner.Race == ObjectType.Player) EXPOwner.WinExp(Experience);

            if (Respawn != null)
                Respawn.Count--;

            if (Master == null)
                Drop();

            Master = null;

            PoisonList.Clear();
            Envir.MonsterCount--;

            if (CurrentMap != null)
                CurrentMap.MonsterCount--;
        }

        public override Packet GetInfo()
        {
            PlayerObject master = null;
            short weapon = -1;
            short armour = 0;
            byte wing = 0;

            if (Master != null && Master is PlayerObject) 
                master = (PlayerObject)Master;

            if (master != null)
            {
                weapon = master.Looks_Weapon;
                armour = master.Looks_Armour;
                wing = master.Looks_Wings;
            }

            return new S.ObjectPlayer
            {
                ObjectID = ObjectID,
                Name = master != null ? master.Name : Name,

                // [hack] 添加昵称显示
                //Nickname = master != null ? Nickname : string.Empty,

                NameColour = NameColour,
                Class = master != null ? master.Class : MirClass.Wizard,
                Gender =  master != null ? master.Gender : MirGender.Male,
                Location = CurrentLocation,
                Direction = Direction,
                Hair = master != null ? master.Hair : (byte)0,
                Weapon = weapon,
                Armour = armour,
                Light = master != null ? master.Light : Light,
                Poison = CurrentPoison,
                Dead = Dead,
                Hidden = Hidden,
                Effect = SpellEffect.None,
                WingEffect = wing,
                Extra = Summoned,
                TransformType = -1
            };
        }
    }
}
