public static class Globals
{
    public const string ProductCodename = "Crystal";

    public const int

        MinAccountIDLength = 3,
        MaxAccountIDLength = 15,

        MinPasswordLength = 5,
        MaxPasswordLength = 15,

        MinCharacterNameLength = 2, // [hack] 人物名字字符长度右 3 改成 2
        MaxCharacterNameLength = 15,
        MaxCharacterCount = 4,

        MaxChatLength = 80,

        StorageGridSize = 80,

        MaxGroup = 15,

        
        MaxPets = 10,               // [hack] 允许的最大宝宝数量，从5增加到10
        MaxHeroes = 10,
        PetRecallHPPercent = 10,    // [hack] 宠物血量低于这个百分比就召唤回到主人身边
        LockHPPercent = 10,         // [hack] 锁红百分比
        LockMPPercent = 10,         // [hack] 锁蓝百分比
        HealthDropShoutOutLoud = 50,// [hack] 掉血超过这个数值的时候会随机触发聊天信息显示

        MaxAttackRange = 9,

        MaxDragonLevel = 13,

        ClassWeaponCount = 100,

        FlagIndexCount = 1999,

        MaxConcurrentQuests = 20,

        LogDelay = 10000,

        DataRange = 16;//Was 24

    public static float Commission = 0.05F;

    public const uint SearchDelay = 500,
                      ConsignmentLength = 7,
                      ConsignmentCost = 5000,
                      MinConsignment = 5000,
                      MaxConsignment = 50000000,
                      AuctionCost = 5000,
                      MinStartingBid = 0,
                      MaxStartingBid = 50000;

    public static int[] FishingRodShapes = new int[] { 49, 50 };

    public static Spell[] RangedSpells = new Spell[]
    {
        Spell.FireBall,
        Spell.ThunderBolt,
        Spell.FireBang,
        Spell.FireWall,
        Spell.FrostCrunch,
        Spell.Vampirism,
        Spell.FlameDisruptor,
        Spell.IceStorm,
        Spell.MeteorStrike,
        Spell.Blizzard,
        Spell.SoulFireBall,
        Spell.StraightShot,
        Spell.ElementalShot,
        Spell.PoisonShot
    };
}