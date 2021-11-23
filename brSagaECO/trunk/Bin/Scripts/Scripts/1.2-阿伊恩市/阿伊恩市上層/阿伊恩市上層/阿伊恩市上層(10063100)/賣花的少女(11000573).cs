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
                Say(pc, 131, "鮮花！鮮花！$R;" +
                    "每支1個金幣$R;");
                switch (Select(pc, "買花吧？", "", "買", "不買"))
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
                                Say(pc, 131, "花兒在唱歌呢。$R;");
                                Wait(pc, 2000);
                                PlaySound(pc, 3087, false, 100, 50);
                                ShowEffect(pc, 4131);
                                Wait(pc, 2000);
                                Say(pc, 131, "技能點數上升1點$R;");
                                Say(pc, 131, "阿伊恩薩烏斯的花$R;" +
                                    "偶爾會創造奇蹟呢。$R;" +
                                    "$R很神奇吧？$R;");
                                return;
                            }
                            Say(pc, 131, "行李太多了，整理一下再來吧$R;");
                            return;
                        }
                        Say(pc, 131, "錢不夠$R;");
                        break;
                }
                return;
            }

            if (Job2X_11_mask.Test(Job2X_11.轉職開始) && Job2X_11_mask.Test(Job2X_11.買花) && pc.Job == PC_JOB.RANGER)
            {
                Say(pc, 131, "$P愛伊斯族生活在$R;" +
                    "諾頓大陸的一個小島$R;" +
                    "$R但如果想上島的話$R;" +
                    "就要有『奇怪的水晶』呢。$R;" +
                    "$P我也在找這種花$R;" +
                    "可是很難找啊$R;" +
                    "$R如果見到$R;" +
                    "島外的愛伊斯族人的話$R;" +
                    "就問一問吧！$R;");
                return;
            }

            if (Job2X_11_mask.Test(Job2X_11.轉職開始) && !Job2X_11_mask.Test(Job2X_11.買花) && pc.Job == PC_JOB.RANGER)
            {
                Say(pc, 131, "鮮花！鮮花！$R;" +
                    "每支1個金幣$R;");
                switch (Select(pc, "買花吧？", "", "買", "還是比較喜歡冰花"))
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
                                Say(pc, 131, "啊，這是『冰花』?$R;" +
                                    "您在找這個嗎？$R;" +
                                    "$P當然知道了。$R;" +
                                    "您是說愛伊斯族的花吧$R;" +
                                    "$P傳說，女孩子得到這種花$R;" +
                                    "會一生幸福$R;" +
                                    "是女孩子中很有名的傳說$R;" +
                                    "$P愛伊斯族生活在$R;" +
                                    "諾頓大陸的一個小島$R;" +
                                    "$R但如果想進島的話$R;" +
                                    "就要有『奇怪的水晶』呢。$R;" +
                                    "$P我也在找這種花$R;" +
                                    "可是很難找啊$R;" +
                                    "$R如果見到$R;" +
                                    "島外的愛伊斯族人的話$R;" +
                                    "就問一問吧！$R;");
                                return;
                            }
                            Say(pc, 131, "行李太多了，整理一下再來吧$R;");
                            return;
                        }
                        Say(pc, 131, "錢不夠$R;");
                        break;
                }
                return;
            }
            
            Say(pc, 131, "您好，$R;" +
                "$R在這種地方還會開花啊$R;");
        }
    }
}