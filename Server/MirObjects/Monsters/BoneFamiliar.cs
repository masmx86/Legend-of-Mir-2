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

            PlayerObject player = Master as PlayerObject;
            int damage = 0;
            if (player != null)
            {
                List<int> min_attack = [player.Stats[Stat.MinDC], player.Stats[Stat.MinMC], player.Stats[Stat.MinSC]];
                List<int> max_attack = [player.Stats[Stat.MaxDC], player.Stats[Stat.MaxMC], player.Stats[Stat.MaxSC]];
                int min_limit = min_attack[(int)player.Class]; // [Envir.Random.Next(min_attack.Count)];
                int max_limit = max_attack[(int)player.Class]; // [Envir.Random.Next(max_attack.Count)];
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

            return packet;
        }
    }
}
