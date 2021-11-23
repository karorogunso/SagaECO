using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
//所在地圖:下城(10024000) NPC基本信息:黑佰特(11000079) X:147 Y:209
namespace SagaScript.M10024000
{
    public class S11000079 : Event
    {
        public S11000079()
        {
            this.EventID = 11000079;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<JobBasic_08> JobBasic_08_mask = new BitMask<JobBasic_08>(pc.CMask["JobBasic_08"]);

            if (JobBasic_08_mask.Test(JobBasic_08.選擇轉職為魔攻師))
            {
                if (pc.Race == PC_RACE.TITANIA)
                {
                    if (JobBasic_08_mask.Test(JobBasic_08.已經從闇之精靈那裡把心染為黑暗))
                    {
                        魔攻師轉職任務(pc);
                        return;
                    }
                }
                else
                {
                    魔攻師轉職任務(pc);
                    return;
                }
            }

            Say(pc, 11000079, 131, "…$R;" +
                                   "$R走開! 走開!$R;", "黑佰特");
        }

        void 魔攻師轉職任務(ActorPC pc)
        {
            BitMask<JobBasic_08> JobBasic_08_mask = new BitMask<JobBasic_08>(pc.CMask["JobBasic_08"]);

            int selection;

            if (!JobBasic_08_mask.Test(JobBasic_08.已經從黑佰特那裡聽取有關黑暗魔法的知識))
            {
                Say(pc, 11000079, 131, pc.Name + "對吧?$R;" +
                                       "$R我從「黑之聖堂祭司」那裡聽說過了，$R;" +
                                       "聽說您想進一步瞭解「黑暗魔法」。$R;" +
                                       "$R打起精神好好聽吧!$R;", "黑佰特");

                switch (Select(pc, "想瞭解關於「黑暗魔法」嗎?", "", "我想知道", "不怎麼想…"))
                {
                    case 1:
                        JobBasic_08_mask.SetValue(JobBasic_08.已經從黑佰特那裡聽取有關黑暗魔法的知識, true);
                        break;
                        
                    case 2:
                        return;
                }
            }
            else
            {
                Say(pc, 11000079, 131, "又是你啊?$R;" +
                                       "真煩人的傢伙…$R;", "黑佰特");

                Say(pc, 11000079, 131, pc.Name + "對吧?$R;" +
                                       "$R我從「黑之聖堂祭司」那裡聽說過了，$R;" +
                                       "聽說您想進一步瞭解「黑暗魔法」。$R;" +
                                       "$R打起精神好好聽吧!$R;", "黑佰特");
            }

            selection = Select(pc, "想知道什麼呢?", "", "關於「黑暗魔法」", "關於「屬性」", "成為魔攻師有什麼好處?", "有什麼魔法?", "不用了");

            while (selection != 5)
            {
                switch (selection)
                {
                    case 1:
                        Say(pc, 11000079, 131, "「黑暗魔法」是「屬性魔法」的一種。$R;" +
                                               "$P「屬性魔法」是火/水/光/闇等，$R;" +
                                               "利用其中一種精靈力量的魔法。$R;" +
                                               "$P我們的魔法中，$R;" +
                                               "也有利用到「闇」精靈的力量。$R;" +
                                               "$P「闇屬性」魔法一般是，$R;" +
                                               "為了傷害敵人而存在的。$R;" +
                                               "$R嘿嘿嘿…$R;", "黑佰特");
                        break;

                    case 2:
                        Say(pc, 11000079, 131, "屬性有『火/水/風/土/光』$R;" +
                                               "還有我們使用的『闇』，一共分成6種。$R;" +
                                               "$P在屬性之間也有強弱關係，$R;" +
                                               "古語有云……$R;" +
                                               "$P『光』會吞滅『闇』，$R;" +
                                               "『闇』會吞掉『一切』。$R;" +
                                               "$P『一切』又會吞滅『光』。$R;" +
                                               "$P這裡說的『一切』就是指$R;" +
                                               "火/水/風/土，4種屬性。$R;" +
                                               "$P重要的是，$R;" +
                                               "幾乎所有魔物們$R;" +
                                               "都是屬於這4種屬性。$R;" +
                                               "$P知道我在說什麼吧?$R;" +
                                               "就是說，「闇」在所有魔物的上面…$R;" +
                                               "$R嘿嘿嘿…$R;", "黑佰特");
                        break;

                    case 3:
                        Say(pc, 11000079, 131, "『魔攻師』可以操作一些像$R;" +
                                               "匕首、短劍等簡單的武器。$R;" +
                                               "$P喜歡近距離對戰的話，$R;" +
                                               "熟練使用武器的技能也不錯。$R;" +
                                               "$P跟其他弱於近距離對戰的魔法師，$R;" +
                                               "有點不同呢!$R;" +
                                               "$R呵呵呵…$R;", "黑佰特");
                        break;

                    case 4:
                        Say(pc, 11000079, 131, "嘿嘿…$R;" +
                                               "$R那就告訴您一點吧!$R;" +
                                               "$P首先是$R;" +
                                               "「闇屬性」的攻擊魔法『闇靈之力』!$R;" +
                                               "$R這個魔法$R;" +
                                               "對「光屬性」的魔物是沒效的，$R;" +
                                               "所以要注意啊!$R;" +
                                               "$P然後是『乾坤一擊』!$R;" +
                                               "$R這是利用鈍器攻擊的方法，$R;" +
                                               "是近距離攻擊的專用技能。$R;" +
                                               "$P然後是『沉默詛咒』!$R;" +
                                               "$R這是可以有效對付會使用$R;" +
                                               "麻煩技能的魔物的魔法。$R;" +
                                               "$P最後是『邪靈知識』!$R;" +
                                               "$R對死了也執著於現世的「不死系」，$R;" +
                                               "學習後，相關的知識會增加，$R;" +
                                               "也有可能會撿到稀有的道具…$R;", "黑佰特");
                        break;
                }

                selection = Select(pc, "想知道什麼呢?", "", "關於「黑暗魔法」", "關於「屬性」", "成為魔攻師有什麼好處?", "有什麼魔法?", "不用了");
            }

            Say(pc, 11000079, 131, "真的沒關係嗎?$R;", "黑佰特");
        }
    }
}
