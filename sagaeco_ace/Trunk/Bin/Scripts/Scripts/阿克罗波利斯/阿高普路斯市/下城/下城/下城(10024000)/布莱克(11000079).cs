using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
//所在地圖:下城(10024000) NPC基本信息:布莱克(11000079) X:147 Y:209
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
                                   "$R走开! 走开!$R;", "布莱克");
        }

        void 魔攻師轉職任務(ActorPC pc)
        {
            BitMask<JobBasic_08> JobBasic_08_mask = new BitMask<JobBasic_08>(pc.CMask["JobBasic_08"]);

            int selection;

            if (!JobBasic_08_mask.Test(JobBasic_08.已經從黑佰特那裡聽取有關黑暗魔法的知識))
            {
                Say(pc, 11000079, 131, pc.Name + "对吧?$R;" +
                                       "$R我从「黑之圣堂祭司」那里听说过了，$R;" +
                                       "听说您想进一步了解「黑暗魔法」。$R;" +
                                       "$R打起精神好好听吧!$R;", "布莱克");

                switch (Select(pc, "想了解关于「黑暗魔法」吗?", "", "我想知道", "不怎么想…"))
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
                                       "真烦人的家伙…$R;", "布莱克");

                Say(pc, 11000079, 131, pc.Name + "对吧?$R;" +
                                       "$R我从「黑之圣堂祭司」那里听说过了，$R;" +
                                       "听说您想进一步了解「黑暗魔法」。$R;" +
                                       "$R打起精神好好听吧!$R;", "布莱克");
            }

            selection = Select(pc, "想知道什么呢?", "", "关于「黑暗魔法」", "关于「属性」", "成为暗术使有什么好处?", "有什么魔法?", "不用了");

            while (selection != 5)
            {
                switch (selection)
                {
                    case 1:
                        Say(pc, 11000079, 131, "「黑暗魔法」是「属性魔法」的一种。$R;" +
                                               "$P「属性魔法」是火/水/光/暗等，$R;" +
                                               "利用其中一种精灵力量的魔法。$R;" +
                                               "$P我们的魔法中，$R;" +
                                               "也有利用到「暗」精灵的力量。$R;" +
                                               "$P「暗属性」魔法一般是，$R;" +
                                               "为了伤害敌人而存在的。$R;" +
                                               "$R嘿嘿嘿…$R;", "布莱克");
                        break;

                    case 2:
                        Say(pc, 11000079, 131, "属性有『火/水/风/土/光』$R;" +
                                               "还有我们使用的『暗』，一共分成6种。$R;" +
                                               "$P在属性之间也有强弱关系，$R;" +
                                               "古语有云……$R;" +
                                               "$P『光』会吞灭『闇』，$R;" +
                                               "『闇』会吞掉『一切』。$R;" +
                                               "$P『一切』又会吞灭『光』。$R;" +
                                               "$P这里说的『一切』就是指$R;" +
                                               "火/水/风/土，4种属性。$R;" +
                                               "$P重要的是，$R;" +
                                               "几乎所有魔物们$R;" +
                                               "都是属于这4种属性。$R;" +
                                               "$P知道我在说什么吧?$R;" +
                                               "就是说，「闇」在所有魔物的上面…$R;" +
                                               "$R嘿嘿嘿…$R;", "布莱克");
                        break;

                    case 3:
                        Say(pc, 11000079, 131, "『暗术使』可以操作一些像$R;" +
                                               "匕首、短剑等简单的武器。$R;" +
                                               "$P喜欢近距离对战的话，$R;" +
                                               "熟练使用武器的技能也不错。$R;" +
                                               "$P跟其他弱于近距离对战的魔法师，$R;" +
                                               "有点不同呢!$R;" +
                                               "$R呵呵呵…$R;", "布莱克");
                        break;

                    case 4:
                        Say(pc, 11000079, 131, "嘿嘿…$R;" +
                                               "$R那就告诉您一点吧!$R;" +
                                               "$P首先是$R;" +
                                               "「暗属性」的攻击魔法『暗灵之力』!$R;" +
                                               "$R这个魔法$R;" +
                                               "对「光属性」的魔物是没效的，$R;" +
                                               "所以要注意啊!$R;" +
                                               "$P然后是『乾坤一击』!$R;" +
                                               "$R这是利用钝器攻击的方法，$R;" +
                                               "是近距离攻击的专用技能。$R;" +
                                               "$P然后是『沉默诅咒』!$R;" +
                                               "$R这是可以有效对付会使用$R;" +
                                               "麻烦技能的魔物的魔法。$R;" +
                                               "$P最后是『邪灵知识』!$R;" +
                                               "$R对死了也执着于现世的「不死系」，$R;" +
                                               "学习后，相关的知识会增加，$R;" +
                                               "也有可能会捡到稀有的道具…$R;", "布莱克");
                        break;
                }

                selection = Select(pc, "想知道什么呢?", "", "关于「黑暗魔法」", "关于「属性」", "成为暗术使有什么好处?", "有什么魔法?", "不用了");
            }

            Say(pc, 11000079, 131, "真的没关系吗?$R;", "布莱克");
        }
    }
}
