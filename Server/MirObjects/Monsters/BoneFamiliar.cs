using Server.MirDatabase;
using S = ServerPackets;

namespace Server.MirObjects.Monsters
{
    public class BoneFamiliar : MonsterObject
    {
        public bool Summoned;

        protected internal BoneFamiliar(MonsterInfo info) : base(info)
        {
            Direction = MirDirection.DownLeft;
        }

        // [hack] 从神兽复制过来的逻辑
        protected override void Attack()
        {
            if (!Target.IsAttackTarget(this))
            {
                Target = null;
                return;
            }

            ActionTime = Envir.Time + 300;
            AttackTime = Envir.Time + AttackSpeed;
            ShockTime = 0;

            Direction = Functions.DirectionFromPoint(CurrentLocation, Target.CurrentLocation);
            Broadcast(new S.ObjectAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation });
            PlayerObject ob = Master as PlayerObject;
            int damage = 0;
            if (ob != null)
            {
                List<int> min_attack = [ob.Stats[Stat.MinDC], ob.Stats[Stat.MinMC], ob.Stats[Stat.MinSC]];
                List<int> max_attack = [ob.Stats[Stat.MaxDC], ob.Stats[Stat.MaxMC], ob.Stats[Stat.MaxSC]];
                int min_limit = min_attack[Envir.Random.Next(min_attack.Count)];
                int max_limit = max_attack[Envir.Random.Next(max_attack.Count)];
                // [todo] 可以根据体力值等参数调整攻击力
                damage = GetAttackPower(min_limit, max_limit); // * Math.Min(Level, MaxPetLevel) / MaxPetLevel; // * HealthPercent / 100;
            }
            else
                damage = GetAttackPower(Stats[Stat.MinDC], Stats[Stat.MaxDC]);
            if (damage == 0) return;

            LineAttack(damage, 1);
        }

        public override void Spawned()
        {
            base.Spawned();

            Summoned = true;
        }

        public override Packet GetInfo()
        {
            var packet = (S.ObjectMonster)base.GetInfo();
            packet.Extra = Summoned;

            // [hack] 添加昵称显示
            //packet.Nickname = Nickname;

            return packet;
        }
    }
}
