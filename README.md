# crystal-mir2-modified
基于 [Crystal Mir2](https://github.com/Suprcode/Crystal)，添加了一些外挂辅助功能，主要用于单机自己玩 ![:-)](https://github.com/masmx86/crystal-mir2-modified/blob/main/smile.jpg)

##### 文件中做过的修改：

###### 都在注释 [hack] 标签下

1、解除了负重跑步限制

   ```
   Client\MirScenes\GameScene.cs

   private bool CanRun()
   ```
   ```
   Server.Library\MirObjects\HumanObject.cs
   public virtual bool CanRun()
   public bool Run()
   ```

2、比奇和盟重的彩票员猜数字活动，无论输赢都会得到 1 千万金币

   ```
   Server\Envir\NPCs\BichonProvince\BichonWall\Lottery.txt
   Server\Envir\NPCs\MongchonProvince\MudWall\Lottery.txt
   ```

3、锁红锁蓝

      红保护线设置为 10~20%，永不死亡
      蓝保护线设置为 10~20% （锁蓝时法师分身无法消失，一直占用名额，会导致无法招其他神兽和骷髅宝宝）

   ```
   Server.Library\MirObjects\HumanObject.cs
   
   public void ChangeHP()
   public void ChangeMP()
   ```

4、法师幻影术可以召唤最多 10 个分身（可以设置成无限制）

      原来的逻辑是法师没蓝以后分身会消失  
      锁蓝以后分身就会一直存在，无法消失  

   ```
   Server.Library\MirObjects\HumanObject.cs
   
   private void Mirroring()
   ```
   
5、道士召唤术可以召唤多个宝宝（可以设置成无限制）

      可以召唤最多 10 个宝宝
      可以召唤最多 4 个分身

   ```
   Server.Library\MirObjects\HumanObject.cs
   
   private void SummonSkeleton()
   private void SummonShinsu()
   ```

6、法师、道士可以召唤分身、神兽和骷髅宝宝


7、仓库购买额外存储空间时间限制由 10 天改成 1 年

   ```
   Server.Library\MirObjects\HumanObject.cs
   
   public void Attack() :Thrusting
   ```

7、仓库购买额外存储空间时间限制由 10 天改成 1 年

   ```
   Shared\Language.cs
   public static string ExtraStorage
   public static string ExtendYourRentalPeriod
   ```
   ```
   Server.Library\MirObjects\PlayerObject.cs
   
   public void Chat() :ADDSTORAGE
   ```

12、物品持久保护

   ```
   Server.Library\MirObjects\HumanObject.cs
   
   private void DamageDura()
   public void DamageWeapon()
   public void DamageItem()
   ```

13、增加小极品物品掉落概率
   ```
   Server.Library\MirEnvir\Envir.cs
   
   public UserItem CreateDropItem()
   public void UpgradeItemHacked()
   
   Server\Configs\RandomItemStats.ini

   ```

14、增加打怪爆率及掉落物品种类

   ```
   Server\Envir\Drops\*.txt
   ```                     
                     
10. 主界面上添加时间、人物属性和装备持久显示
   ```
   Server.Library\MirObjects\PlayerObject.cs
   
   private void StartGameSuccess()
   public void StopGame()
   ```
   ```
   Server.Library\MirObjects\PlayerObject.cs
   
   public override void UseItem()
       ......
       UserItem item = null;
       int index = -1;

       for (int i = 0; i < Info.Inventory.Length; i++)
       {
           item = Info.Inventory[i];
           if (item == null || item.UniqueID != id) continue;
           index = i;
           break;
       }
       if (item == null || index == -1 || !CanUseItem(item))
       {
           Enqueue(p);
           return;
       }
       ......

       switch (item.Info.Type)
       {
           case ItemType.Book:
               UserMagic magic = new UserMagic((Spell)item.Info.Shape);
               if (magic.Info == null)
               {
                   Enqueue(p);
                   return;
               }
               Info.Magics.Add(magic);
               SendMagicInfo(magic);
               RefreshStats();
               break;
           ......
   ```

15. 主界面上添加时间显示
   ```
   Client\MirScenes\Dialogs\MainDialogs.cs
   public void MainDialog.Process()
   ```

16. 增加角色名称可用字符集范围
   ```
   Client\MirScenes\Dialogs\NewCharacterDialog.cs
   public sealed class NewCharacterDialog : MirImageControl{}

   Server.Library\MirEnvir\Envir.cs
   static Envir()
   ```

12. 道士自动使用符、毒
   ```
   Server.Library\MirObjects\HumanObject.cs
   protected UserItem GetAmulet()
   protected UserItem GetPoison()
   ```

##### TODO：

自动整理包裹，物品自动归类排序

宠物取小名

战士添加宝宝

##### TODO：

道士自动换符、换毒药

自动显示、拾取超过一定等级的物品，自动捡钱

自动喝药、自动喝灵符

自动修理装备

自动传送回城

自动挂机打怪

自动换装备

自动宠物回血

自动存取仓库

宠物增加昵称
\Server\MirDatabase\CharacterInfo.cs

    BugBagMaggot = 43,  // [hack] fix naming error
C:\Users\masm32\source\repos\Legend-of-Mir-2\Shared\Enums.cs

            // [hack] allow charactername to use a mix of ascii and chinese characters
            //CharacterReg = new Regex(@"^[\u4e00-\u9fa5_A-Za-z0-9]{" + Globals.MinCharacterNameLength + "," + Globals.MaxCharacterNameLength + "}$");
            CharacterReg = new Regex(@"^[\u4e00-\u9fff\u3000-\u309f_A-Za-z0-9]{" + Globals.MinCharacterNameLength + "," + Globals.MaxCharacterNameLength + "}$");
C:\Users\masm32\source\repos\Legend-of-Mir-2\Server\MirEnvir\Envir.cs

            // [hack] increase chance of hacked items with more random stats
            UpgradeItem(item);
            //UpgradeItemHacked(item);
C:\Users\masm32\source\repos\Legend-of-Mir-2\Server\MirEnvir\Envir.cs
public UserItem CreateDropItem(ItemInfo info)


        // [hack] increase chance to get more stats on items by replacing
        //        Random.Next(x) == 0 ==> Random.Next(x) % 2 == 0 and
        //        RandomomRange()     ==> RandomomRangeHacked()
        public void UpgradeItemHacked(UserItem item)
        {
            if (item.Info.RandomStats == null) return;

            var stat = item.Info.RandomStats;
            if (stat.MaxDuraChance > 0 && Random.Next(stat.MaxDuraChance) == 0)
            {
                var dura = RandomomRange(stat.MaxDuraMaxStat, stat.MaxDuraStatChance);
                item.MaxDura = (ushort)Math.Min(ushort.MaxValue, item.MaxDura + dura * 1000);
                item.CurrentDura = (ushort)Math.Min(ushort.MaxValue, item.CurrentDura + dura * 1000);
            }


            ItemGrade grade = item.Info.Grade;

            bool hasDC = item.Info.Stats[Stat.MaxDC] > 0 ? true : false;
            bool hasMC = item.Info.Stats[Stat.MaxMC] > 0 ? true : false;
            bool hasSC = item.Info.Stats[Stat.MaxSC] > 0 ? true : false;

            if (!hasDC && !hasMC && !hasSC)
            {
                // no DC/MC/SC on item info, randomly assign one
                int rand_choice = Random.Next(3);
                switch (rand_choice)
                {
                    case 0:
                        hasDC = true;
                        break;
                    case 1:
                        hasMC = true;
                        break;
                    case 2:
                        hasSC = true;
                        break;
                }
            }

            if (item.Info.Type == ItemType.Weapon)
            {
                // weapons get extra MC|SC only
                if (hasMC || hasSC) hasDC = false;
            }

            int extraDC = hasDC ?
                (RandomomRangeHacked(stat.MaxDcMaxStat - 1 + (byte)grade, stat.MaxDcStatChance) + 1) :
                (RandomomRange(stat.MaxDcMaxStat - 1, stat.MaxDcStatChance) + 1);
            int extraMC = hasMC ?
                (RandomomRangeHacked(stat.MaxMcMaxStat - 1 + (byte)grade, stat.MaxMcStatChance) + 1) :
                (RandomomRange(stat.MaxMcMaxStat - 1, stat.MaxMcStatChance) + 1);
            int extraSC = hasSC ?
                (RandomomRangeHacked(stat.MaxScMaxStat - 1 + (byte)grade, stat.MaxScStatChance) + 1) :
                (RandomomRange(stat.MaxScMaxStat - 1, stat.MaxScStatChance) + 1);


            if (hasDC && stat.MaxDcChance > 0 && Random.Next(stat.MaxDcChance) % 2 == 0)
            {
                item.AddedStats[Stat.MaxDC] = (byte)Math.Min(extraDC, stat.MaxDcMaxStat);
            }
            if (hasMC && stat.MaxMcChance > 0 && Random.Next(stat.MaxMcChance) % 2 == 0)
            {
                item.AddedStats[Stat.MaxMC] = (byte)Math.Min(extraMC, stat.MaxMcMaxStat);
            }
            if (hasSC && stat.MaxScChance > 0 && Random.Next(stat.MaxScChance) % 2 == 0)
            {
                item.AddedStats[Stat.MaxSC] = (byte)Math.Min(extraSC, stat.MaxScMaxStat);
            }

            // AC
            if (stat.MaxAcChance > 0 && Random.Next(stat.MaxAcChance) % 2 == 0)
                item.AddedStats[Stat.MaxAC] = item.Info.Stats[Stat.MaxAC] > 0 ?
                    (byte)(RandomomRangeHacked(stat.MaxAcMaxStat - 1 + (byte)grade, stat.MaxAcStatChance) + 1) :
                    (byte)(RandomomRange(stat.MaxAcMaxStat - 1, stat.MaxAcStatChance) + 1);

            // MAC
            if (stat.MaxMacChance > 0 && Random.Next(stat.MaxMacChance) % 2 == 0)
                item.AddedStats[Stat.MaxMAC] = item.Info.Stats[Stat.MaxMAC] > 0 ?
                    (byte)(RandomomRangeHacked(stat.MaxMacMaxStat - 1 + (byte)grade, stat.MaxMacStatChance) + 1) :
                    (byte)(RandomomRange(stat.MaxMacMaxStat - 1, stat.MaxMacStatChance) + 1);

            // Accuracy
            if (stat.AccuracyChance > 0 && Random.Next(stat.AccuracyChance) % 2 == 0)
                item.AddedStats[Stat.Accuracy] = item.Info.Stats[Stat.Accuracy] > 0 ?
                    (byte)(RandomomRangeHacked(stat.AccuracyMaxStat - 1 + (byte)grade, stat.AccuracyStatChance) + 1) + (byte)grade :
                    (byte)(RandomomRange(stat.AccuracyMaxStat - 1, stat.AccuracyStatChance) + 1);

            // Agility
            if (stat.AgilityChance > 0 && Random.Next(stat.AgilityChance) % 2 == 0)
                item.AddedStats[Stat.Agility] = item.Info.Stats[Stat.Agility] > 0 ?
                    (byte)(RandomomRangeHacked(stat.AgilityMaxStat - 1 + (byte)grade, stat.AgilityStatChance) + 1) + (byte)grade :
                    (byte)(RandomomRange(stat.AgilityMaxStat - 1, stat.AgilityStatChance) + 1);

            // HP
            if (stat.HpChance > 0 && Random.Next(stat.HpChance) % 2 == 0)
                item.AddedStats[Stat.HP] = item.Info.Stats[Stat.HP] > 0 ?
                    (byte)(RandomomRangeHacked(stat.HpMaxStat - 1 + (byte)grade, stat.HpStatChance) + 1) :
                    (byte)(RandomomRange(stat.HpMaxStat - 1, stat.HpStatChance) + 1);

            // MP
            if (stat.MpChance > 0 && Random.Next(stat.MpChance) % 2 == 0)
                item.AddedStats[Stat.MP] = item.Info.Stats[Stat.MP] > 0 ?
                    (byte)(RandomomRangeHacked(stat.MpMaxStat - 1 + (byte)grade, stat.MpStatChance) + 1) :
                    (byte)(RandomomRange(stat.MpMaxStat - 1, stat.MpStatChance) + 1);

            // Strength
            if (stat.StrongChance > 0 && Random.Next(stat.StrongChance) % 2 == 0)
                item.AddedStats[Stat.Strong] = item.Info.Stats[Stat.Strong] > 0 ?
                    (byte)(RandomomRangeHacked(stat.StrongMaxStat - 1 + (byte)grade, stat.StrongStatChance) + 1) :
                    (byte)(RandomomRange(stat.StrongMaxStat - 1, stat.StrongStatChance) + 1);

            // Magic Resist
            if (stat.MagicResistChance > 0 && Random.Next(stat.MagicResistChance) % 2 == 0)
                item.AddedStats[Stat.MagicResist] = item.Info.Stats[Stat.MagicResist] > 0 ?
                    (byte)(RandomomRangeHacked(stat.MagicResistMaxStat - 1 + (byte)grade, stat.MagicResistStatChance) + 1) + (byte)grade :
                    (byte)(RandomomRange(stat.MagicResistMaxStat - 1, stat.MagicResistStatChance) + 1);

            // Poison Resist
            if (stat.PoisonResistChance > 0 && Random.Next(stat.PoisonResistChance) % 2 == 0)
                item.AddedStats[Stat.PoisonResist] = item.Info.Stats[Stat.PoisonResist] > 0 ?
                    (byte)(RandomomRangeHacked(stat.PoisonResistMaxStat - 1 + (byte)grade, stat.PoisonResistStatChance) + 1) + (byte)grade :
                    (byte)(RandomomRange(stat.PoisonResistMaxStat - 1, stat.PoisonResistStatChance) + 1);

            // HP Recovery
            if (stat.HpRecovChance > 0 && Random.Next(stat.HpRecovChance) % 2 == 0)
                item.AddedStats[Stat.HealthRecovery] = item.Info.Stats[Stat.HealthRecovery] > 0 ?
                    (byte)(RandomomRangeHacked(stat.HpRecovMaxStat - 1 + (byte)grade, stat.HpRecovStatChance) + 1) :
                    (byte)(RandomomRange(stat.HpRecovMaxStat - 1, stat.HpRecovStatChance) + 1);

            // MP Recovery
            if (stat.MpRecovChance > 0 && Random.Next(stat.MpRecovChance) % 2 == 0)
                item.AddedStats[Stat.SpellRecovery] = item.Info.Stats[Stat.SpellRecovery] > 0 ?
                    (byte)(RandomomRangeHacked(stat.MpRecovMaxStat - 1 + (byte)grade, stat.MpRecovStatChance) + 1) :
                    (byte)(RandomomRange(stat.MpRecovMaxStat - 1, stat.MpRecovStatChance) + 1);

            // Poison Recovery
            if (stat.PoisonRecovChance > 0 && Random.Next(stat.PoisonRecovChance) % 2 == 0)
                item.AddedStats[Stat.PoisonRecovery] = item.Info.Stats[Stat.PoisonRecovery] > 0 ?
                    (byte)(RandomomRangeHacked(stat.PoisonRecovMaxStat - 1 + (byte)grade, stat.PoisonRecovStatChance) + 1) + (byte)grade :
                    (byte)(RandomomRange(stat.PoisonRecovMaxStat - 1, stat.PoisonRecovStatChance) + 1);

            // Critical Rate
            if (stat.CriticalRateChance > 0 && Random.Next(stat.CriticalRateChance) % 2 == 0)
                item.AddedStats[Stat.CriticalRate] = item.Info.Stats[Stat.CriticalRate] > 0 ?
                    (byte)(RandomomRangeHacked(stat.CriticalRateMaxStat - 1 + (byte)grade, stat.CriticalRateStatChance) + 1) :
                    (byte)(RandomomRange(stat.CriticalRateMaxStat - 1, stat.CriticalRateStatChance) + 1);

            // Critical Damage
            if (stat.CriticalDamageChance > 0 && Random.Next(stat.CriticalDamageChance) % 2 == 0)
                item.AddedStats[Stat.CriticalDamage] = item.Info.Stats[Stat.CriticalDamage] > 0 ?
                    (byte)(RandomomRangeHacked(stat.CriticalDamageMaxStat - 1 + (byte)grade, stat.CriticalDamageStatChance) + 1) :
                    (byte)(RandomomRange(stat.CriticalDamageMaxStat - 1, stat.CriticalDamageStatChance) + 1);

            // Freezing
            if (stat.FreezeChance > 0 && Random.Next(stat.FreezeChance) % 2 == 0)
                item.AddedStats[Stat.Freezing] = item.Info.Stats[Stat.Freezing] > 0 ?
                    (byte)(RandomomRangeHacked(stat.FreezeMaxStat - 1 + (byte)grade, stat.FreezeStatChance) + 1) + (byte)grade :
                    (byte)(RandomomRange(stat.FreezeMaxStat - 1, stat.FreezeStatChance) + 1);

            //  Poison Attack
            if (stat.PoisonAttackChance > 0 && Random.Next(stat.PoisonAttackChance) % 2 == 0)
                item.AddedStats[Stat.PoisonAttack] = item.Info.Stats[Stat.PoisonAttack] > 0 ?
                    (byte)(RandomomRangeHacked(stat.PoisonAttackMaxStat - 1 + (byte)grade, stat.PoisonAttackStatChance) + 1) + (byte)grade :
                    (byte)(RandomomRange(stat.PoisonAttackMaxStat - 1, stat.PoisonAttackStatChance) + 1);

            // Attack Speed
            if (stat.AttackSpeedChance > 0 && Random.Next(stat.AttackSpeedChance) % 2 == 0)
                item.AddedStats[Stat.AttackSpeed] = item.Info.Stats[Stat.AttackSpeed] > 0 ?
                    (sbyte)(RandomomRangeHacked(stat.AttackSpeedMaxStat - 1 + (byte)grade, stat.AttackSpeedStatChance) + 1) + (byte)grade :
                    (byte)(RandomomRange(stat.AttackSpeedMaxStat - 1, stat.AttackSpeedStatChance) + 1);

            // Luck
            if (stat.LuckChance > 0 && Random.Next(stat.LuckChance) % 2 == 0)
                item.AddedStats[Stat.Luck] = item.Info.Stats[Stat.Luck] > 0 ?
                    (sbyte)(RandomomRangeHacked(stat.LuckMaxStat - 1 + (byte)grade, stat.LuckStatChance) + 1) :
                    (byte)(RandomomRange(stat.LuckMaxStat - 1, stat.LuckStatChance) + 1);

            // Cursed
            if (stat.CurseChance > 0 && Random.Next(100) <= stat.CurseChance)
                item.Cursed = true;

            // Slots
            if (stat.SlotChance > 0 && Random.Next(stat.SlotChance) == 0)
            {
                var slot = (byte)(RandomomRange(stat.SlotMaxStat - 1, stat.SlotStatChance) + 1);

                if (slot > item.Info.Slots)
                {
                    item.SetSlotSize(slot);
                }
            }
        }

        // [hack] increase chance to get more stats on items
        public int RandomomRangeHacked(int count, int rate)
        {
            var x = 0;
            for (var i = 0; i < count; i++) if (Random.Next(rate) % 2 == 0) x++;
            return x;
        }


C:\Users\masm32\source\repos\Legend-of-Mir-2\Client\MirScenes\GameScene.cs
            // [hack] bypass bag weight & wear weight restrictions
            //if (User.CurrentBagWeight > User.Stats[Stat.BagWeight]) return false;   // bag weight
            //if (User.CurrentWearWeight > User.Stats[Stat.BagWeight]) return false;  // wear weight

C:\Users\masm32\source\repos\Legend-of-Mir-2\Shared\Globals.cs
        // [hack] change from 5 to 10 to allow more pets
        MaxPets = 10,

C:\Users\masm32\source\repos\Legend-of-Mir-2\Server\MirObjects\HeroObject.cs
protected List<MapObject> FindAllTargets(int dist, Point location, bool needSight = true)
                            switch (ob.Race)
                            {
                                case ObjectType.Monster:
                                case ObjectType.Player:
                                case ObjectType.Hero:
                                    // [hack] add BugBagMaggot to hero's attck target
                                    if (ob is BugBagMaggot)
                                    {
                                        targets.Add(ob);
                                        continue;
                                    }
                                    else if (ob.Master != null && (ob.Name == Settings.BugBatName || ob.Name == Settings.BombSpiderName))
                                    {
                                        Target = ob.Master;
                                        continue;
                                    }
                                    // [/hack]

                                    if (!ob.IsAttackTarget(this)) continue;
                                    if (ob.Hidden && (!CoolEye || Level < ob.Level) && needSight) continue;
                                    if (ob.Race == ObjectType.Player)
                                    {
                                        PlayerObject player = ((PlayerObject)ob);
                                        if (player.GMGameMaster) continue;
                                    }
                                    targets.Add(ob);
                                    continue;
                                default:
                                    continue;
                            }

protected virtual void FindTarget()
                            switch (ob.Race)
                            {
                                case ObjectType.Monster:
                                case ObjectType.Hero:
                                    // [hack] add BugBagMaggot to attack target
                                    if (ob is BugBagMaggot)
                                    {
                                        Target = ob;
                                        Master.ReceiveChat(Info.Name + " targeting " + ob.Name, ChatType.Normal);
                                        continue;
                                    }
                                    if(ob.Name == Settings.BugBatName || ob.Name == Settings.BombSpiderName)
                                    {
                                        if (ob.Master != null)
                                        {
                                            Target = ob.Master;
                                            Master.ReceiveChat(Info.Name + " targeting ob.Master " + ob.Master.Name, ChatType.Normal);
                                        }
                                        else if (ob.Owner != null)
                                        {
                                            Target = ob.Owner;
                                            Master.ReceiveChat(Info.Name + " targeting ob.Owner " + ob.Master.Name, ChatType.Normal);
                                        }
                                        else
                                        {
                                            Target = ob;
                                            Master.ReceiveChat(Info.Name + " targeting " + ob.Name, ChatType.Normal);
                                        }
                                        continue;
                                    }
                                    // [/hack]

                                    if (ob is TownArcher) continue;
                                    if (!ob.IsAttackTarget(Owner)) continue;
                                    if (ob.Hidden && (!CoolEye || Level < ob.Level)) continue;
                                    if (ob.Master != null && Target != ob) continue;
                                    if (Owner.Info.HeroBehaviour == HeroBehaviour.CounterAttack && ob.Target != this && ob.Target != Owner) continue;

                                    Target ??= ob;
                                    return;
                                case ObjectType.Player:
                                    PlayerObject playerob = (PlayerObject)ob;
                                    if (!ob.IsAttackTarget(Owner)) continue;
                                    if (playerob.GMGameMaster || ob.Hidden && (!CoolEye || Level < ob.Level)) continue;
                                    if (Target != ob && Owner.LastHitter != ob && ob.LastHitter != Owner) continue;

                                    Target = ob;

                                    if (Owner != null)
                                    {
                                        for (int j = 0; j < playerob.Pets.Count; j++)
                                        {
                                            MonsterObject pet = playerob.Pets[j];

                                            if (!pet.IsAttackTarget(this)) continue;
                                            Target = pet;
                                            break;
                                        }
                                    }
                                    return;
                                default:
                                    continue;
                            }

C:\Users\masm32\source\repos\Legend-of-Mir-2\Server\MirObjects\HumanObject.cs
public virtual bool CanRun
                // [hack] bypass run restriction by removing bag weight limit
                //return !Dead && Envir.Time >= ActionTime && (_stepCounter > 0 || FastRun) && (!Sneaking || ActiveSwiftFeet) && CurrentBagWeight <= Stats[Stat.BagWeight] && !CurrentPoison.HasFlag(PoisonType.Paralysis) && !CurrentPoison.HasFlag(PoisonType.LRParalysis) && !CurrentPoison.HasFlag(PoisonType.Frozen);
                return !Dead && Envir.Time >= ActionTime && (_stepCounter > 0 || FastRun) && (!Sneaking || ActiveSwiftFeet) && !CurrentPoison.HasFlag(PoisonType.Paralysis) && !CurrentPoison.HasFlag(PoisonType.LRParalysis) && !CurrentPoison.HasFlag(PoisonType.Frozen);

        // [hack] last used poison 
        protected PoisonType lastUsedPoison;
        public PoisonType LastUsedPoison
        {
            get { return lastUsedPoison; }
            set { lastUsedPoison = value; }
        }

public void ChangeHP(int amount)

        // [hack] protect hp so player never dies
        // [todo] read player name list from config and only protect those players
        private void ProtectHP()
        {
            int hp_protection_val = (int) (Stats[Stat.HP] * 0.10);
            if (HP < hp_protection_val)
            {
                HP = hp_protection_val + (int) Envir.Random.Next(0, hp_protection_val);
            }
        }

public void ChangeMP(int amount)