# crystal-mir2-modified
基于 [Crystal Mir2](https://github.com/Suprcode/Crystal)，添加了一些外挂辅助功能，主要用于单机自己玩

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
      蓝保护线设置为 10~20%

   ```
   Server.Library\MirObjects\HumanObject.cs
   
   public void ChangeHP() -> Protected override void ChangeHP()
   public void ChangeMP() -> Protected override void ChangeMP()
   ```

4、法师幻影术可以召唤最多 10 个分身（可以设置成无限制）

      原来的逻辑是法师没蓝以后分身会消失  
      锁蓝以后分身就会一直存在，无法消失  

   ```
   Server.Library\MirObjects\HumanObject.cs
   
   private void Mirroring()
   ```
   
5、道士召唤术可以召唤最多 10 个宝宝（可以设置成无限制）

   ```
   Server.Library\MirObjects\HumanObject.cs
   
   private void SummonSkeleton()
   private void SummonShinsu()
   ```

6、仓库购买额外存储空间时间限制由 10 天改成 1 年

   ```
   Shared\Language.cs
   public static string ExtraStorage
   public static string ExtendYourRentalPeriod
   ```
   ```
   Server.Library\MirObjects\PlayerObject.cs
   
   public void Chat() :ADDSTORAGE
   ```

7、物品持久保护

   ```
   Server.Library\MirObjects\HumanObject.cs
   
   private void DamageDura()
   public void DamageWeapon()
   public void DamageItem() -> 50% 掉持久, 50% 持久增加
   ```

8、增加小极品物品掉落概率
   ```
   Server.Library\MirEnvir\Envir.cs
   
   public UserItem CreateDropItem() -> public void UpgradeItemHacked()
   
   Server\Configs\RandomItemStats.ini

   ```

9、增加打怪爆率及掉落物品种类

   ```
   Server\Envir\Drops\*.txt
   ```                     
                     
10. 主界面上添加时间、人物属性和装备持久显示
   ```
   Client\MirScenes\Dialogs\MainDialogs.cs
   public void MainDialog.Process()
   public ChatControlBar()
   ```

11. 扩展角色名称可用字符集
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

战士添加宝宝

自动显示、拾取超过一定等级的物品，自动捡钱

自动喝药、自动喝灵符

自动修理装备

自动传送回城

自动挂机打怪

自动换装备

自动宠物回血

自动存取仓库
