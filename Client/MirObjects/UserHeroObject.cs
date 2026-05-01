using Client.MirScenes;
using Client.MirScenes.Dialogs;
using S = ServerPackets;

namespace Client.MirObjects
{
    public class UserHeroObject : UserObject
    {
        public bool AutoPot;
        public uint AutoHPPercent;
        public uint AutoMPPercent;

        public UserItem[] HPItem = new UserItem[1];
        public UserItem[] MPItem = new UserItem[1];
        public override BuffDialog GetBuffDialog => GameScene.Scene.HeroBuffsDialog;
        public UserHeroObject(uint objectID)
        {
            ObjectID = objectID;
            Stats = new Stats();
            Frames = FrameSet.Player;
        }

        public override void Load(S.UserInformation info)
        {
            Name = info.Name;
            // [hack] 添加昵称
            //Nickname = info.Nickname;
            NameColour = info.NameColour;
            Class = info.Class;
            Gender = info.Gender;
            Level = info.Level;
            Hair = info.Hair;

            HP = info.HP;
            MP = info.MP;

            Experience = info.Experience;
            MaxExperience = info.MaxExperience;

            // [hack] 自动打开刺杀和半月
            Thrusting = true;
            HalfMoon = true;

            Inventory = info.Inventory;
            Equipment = info.Equipment;

            Magics = info.Magics;
            for (int i = 0; i < Magics.Count; i++)
            {
                Magics[i].CastTime += CMain.Time;
            }

            BindAllItems();                        
        }      
    }
}
