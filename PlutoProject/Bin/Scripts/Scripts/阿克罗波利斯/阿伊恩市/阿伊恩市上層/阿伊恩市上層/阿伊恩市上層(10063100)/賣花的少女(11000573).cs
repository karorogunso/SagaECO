using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10063100
{
    public class S11000573 : Event
    {
        public S11000573()
        {
            this.EventID = 11000573;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<AYEFlags> mask = new BitMask<AYEFlags>(pc.CMask["AYE"]);
            BitMask<Job2X_11> Job2X_11_mask = pc.CMask["Job2X_11"];

            if (!mask.Test(AYEFlags.買花的少女第一次對話))
            {
                Say(pc, 131, "鲜花！鲜花！$R;" +
                    "每支1个金币$R;");
                switch (Select(pc, "买花吧？", "", "买", "不买"))
                {
                    case 1:
                        if (pc.Gold > 0)
                        {
                            if (CheckInventory(pc, 10005300, 1))
                            {
                                mask.SetValue(AYEFlags.買花的少女第一次對話, true);
                                //_2a66 = true;
                                pc.Gold--;
                                GiveItem(pc, 10005300, 1);
                                SkillPointBonus(pc, 1);
                                Say(pc, 131, "花儿在唱歌呢。$R;");
                                Wait(pc, 2000);
                                PlaySound(pc, 3087, false, 100, 50);
                                ShowEffect(pc, 4131);
                                Wait(pc, 2000);
                                Say(pc, 131, "技能点数上升1点$R;");
                                Say(pc, 131, "艾恩萨乌斯的花$R;" +
                                    "偶尔会创造奇迹呢。$R;" +
                                    "$R很神奇吧？$R;");
                                return;
                            }
                            Say(pc, 131, "行李太多了，整理一下再来吧$R;");
                            return;
                        }
                        Say(pc, 131, "钱不够$R;");
                        break;
                }
                return;
            }

            if (Job2X_11_mask.Test(Job2X_11.轉職開始) && Job2X_11_mask.Test(Job2X_11.買花) && pc.Job == PC_JOB.RANGER)
            {
                Say(pc, 131, "$P冰精灵一族生活在$R;" +
                    "诺森地区的一个小岛$R;" +
                    "$R但如果想上岛的话$R;" +
                    "就要有『不可思议的水晶』呢。$R;" +
                    "$P我也在找这种花$R;" +
                    "可是很难找啊$R;" +
                    "$R如果见到$R;" +
                    "岛外的冰精灵的话$R;" +
                    "就问一问吧！$R;");
                return;
            }

            if (Job2X_11_mask.Test(Job2X_11.轉職開始) && !Job2X_11_mask.Test(Job2X_11.買花) && pc.Job == PC_JOB.RANGER)
            {
                Say(pc, 131, "鲜花！鲜花！$R;" +
                    "每支1个金币$R;");
                switch (Select(pc, "买花吧？", "", "买", "还是比较喜欢冰花"))
                {
                    case 1:
                        if (pc.Gold > 0)
                        {
                            if (CheckInventory(pc, 10005300, 1))
                            {
                                Job2X_11_mask.SetValue(Job2X_11.買花, true);
                                //_3a47 = true;
                                pc.Gold--;
                                GiveItem(pc, 10005300, 1);
                                Say(pc, 131, "啊，这是『冰花』?$R;" +
                                    "您在找这个吗？$R;" +
                                    "$P当然知道了。$R;" +
                                    "您是说冰精灵的花吧$R;" +
                                    "$P传说，女孩子得到这种花$R;" +
                                    "会一生幸福$R;" +
                                    "是女孩子中很有名的传说$R;" +
                                    "$P爱伊斯族生活在$R;" +
                                    "诺顿大六的一个小岛$R;" +
                                    "$R但如果想进岛的话$R;" +
                                    "就要有『不可思议的水晶』呢。$R;" +
                                    "$P我也在找这种花$R;" +
                                    "可是很难找啊$R;" +
                                    "$R如果见到$R;" +
                                    "岛外的冰精灵的话$R;" +
                                    "就问一问吧！$R;");
                                return;
                            }
                            Say(pc, 131, "行李太多了，整理一下再来吧$R;");
                            return;
                        }
                        Say(pc, 131, "钱不够$R;");
                        break;
                }
                return;
            }
            
            Say(pc, 131, "您好，$R;" +
                "$R在这种地方还会开花啊$R;");
        }
    }
}