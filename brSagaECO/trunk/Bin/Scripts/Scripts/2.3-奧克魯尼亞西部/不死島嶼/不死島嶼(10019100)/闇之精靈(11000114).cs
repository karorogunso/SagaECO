using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
//所在地圖:不死島嶼(10019100)NPC基本信息:闇之精靈(11000114) X:12 Y:79
namespace SagaScript.M10019100
{
    public class S11000114 : Event
    {
        public S11000114()
        {
            this.EventID = 11000114;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<JobBasic_08> JobBasic_08_mask = new BitMask<JobBasic_08>(pc.CMask["JobBasic_08"]);
            BitMask<Crystal> Crystal_mask = pc.CMask["Crystal"];

            if (Crystal_mask.Test(Crystal.開始收集) &&
                !Crystal_mask.Test(Crystal.暗之精靈) &&
                CountItem(pc, 10014300) >= 1)
            {
                if (!Crystal_mask.Test(Crystal.光之精靈) &&
                    !Crystal_mask.Test(Crystal.炎之精靈) &&
                    !Crystal_mask.Test(Crystal.水之精靈) &&
                    !Crystal_mask.Test(Crystal.土之精靈) &&
                    !Crystal_mask.Test(Crystal.風之精靈) &&
                    CountItem(pc, 10014300) >= 1)
                {
                    Crystal_mask.SetValue(Crystal.暗之精靈, true);
                    Say(pc, 131, "…?$R;" +
                        "$R我叫勒達！是黑暗精靈！$R;" +
                        "$R什麼事呢！$R;" +
                        "$P…$R;" +
                        "想在『水晶』上注入力量？$R;" +
                        "$R…$R;" +
                        "$R好的！對我來說是件小事，$R給您辦吧！$R;" +
                        "不要動！$R;" +
                        "$P…?$R;" +
                        "$R來，開始了！$R;");
                    PlaySound(pc, 3120, false, 100, 50);
                    ShowEffect(pc, 4036);
                    Say(pc, 131, "在『水晶』裡注入力量了$R;" +
                        "…?$R;" +
                        "不能再注入力量了$R;");
                    return;
                }
                Say(pc, 131, "…?$R;" +
                    "$R我叫勒達！是黑暗精靈！$R;" +
                    "$R什麼事呢？$R;" +
                    "$P…$R;" +
                    "想在『水晶』上注入力量？$R;" +
                    "$R…$R;" +
                    "哎呀！$R;" +
                    "已經有別的精靈力量了$R;" +
                    "$P現在不能注入我的力量呀$R;");
                return;
            }

            if (JobBasic_08_mask.Test(JobBasic_08.選擇轉職為魔攻師) &&
                !JobBasic_08_mask.Test(JobBasic_08.已經從闇之精靈那裡把心染為黑暗) &&
                pc.Race == PC_RACE.TITANIA)
            {
                魔攻師轉職任務(pc);
                return;
            }

            Say(pc, 11000114, 131, "…?$R;" +
                                   "$R我叫「勒達」，是「闇之精靈」。$R;" +
                                   "$R找我有什麼事嗎?$R;", "闇之精靈");	
        }

        void 魔攻師轉職任務(ActorPC pc)
        {
            BitMask<JobBasic_08> JobBasic_08_mask = new BitMask<JobBasic_08>(pc.CMask["JobBasic_08"]);

            JobBasic_08_mask.SetValue(JobBasic_08.已經從闇之精靈那裡把心染為黑暗, true);

            Say(pc, 11000114, 131, "…?$R;" +
                                   "$R什麼事呢?$R;" +
                                   "$P…想把心染成黑暗是嗎?$R;" +
                                   "朋友增加，應該是值得高興的事吧!$R;", "闇之精靈");

            Say(pc, 11000114, 131, "現在您的心已經染成黑暗了。$R;", "闇之精靈");	
        }
    }
}
