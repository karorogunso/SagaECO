using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
//所在地圖:下城(10024000) NPC基本信息:麦吉克(11000076) X:122 Y:49
namespace SagaScript.M10024000
{
    public class S11000076 : Event
    {
        public S11000076()
        {
            this.EventID = 11000076;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<JobBasic_05> JobBasic_05_mask = new BitMask<JobBasic_05>(pc.CMask["JobBasic_05"]);

            if (JobBasic_05_mask.Test(JobBasic_05.選擇轉職為魔法師))
            {
                魔法師轉職任務(pc);
                return;
            }

            Say(pc, 11000076, 131, "新生魔法的真谛…$R;" +
                                   "有谁了解呢?$R;", "麦吉克");
        }

        void 魔法師轉職任務(ActorPC pc)
        {
            BitMask<JobBasic_05> JobBasic_05_mask = new BitMask<JobBasic_05>(pc.CMask["JobBasic_05"]);

            int selection;

            if (!JobBasic_05_mask.Test(JobBasic_05.已經從魔術那裡聽取有關新生魔法的知識))
            {
                Say(pc, 11000076, 131, pc.Name + "对吧?$R;" +
                                       "$R我从总管那里听说过了，$R;" +
                                       "您想学习「新生魔法」是吧?$R;", "麦吉克");

                switch (Select(pc, "想了解关于「新生魔法」吗?", "", "我想了解", "不用了"))
                {
                    case 1:
                        JobBasic_05_mask.SetValue(JobBasic_05.已經從魔術那裡聽取有關新生魔法的知識, true); 
                        break;

                    case 2:
                        return;
                }
            }
            else
            {
                Say(pc, 11000076, 131, "还有不了解的地方?$R;", "麦吉克");
            }

            Say(pc, 11000076, 131, "让我介绍一下「新生魔法」吧!$R;", "麦吉克");

            selection = Select(pc, "想知道什么呢?", "", "关于「新生魔法」", "「新生魔法」跟「属性魔法」有什么不同?", "有什么魔法?", "「新生魔法」名字的由来?", "不用了");

            while (selection != 5)
            {
                switch (selection)
                {
                    case 1:
                        Say(pc, 11000076, 131, "这个应该从总管那里听说过吧?$R;" +
                                               "$R「新生魔法」是不需要火、水等$R;" +
                                               "属性力量的新魔法!$R;" +
                                               "$P跟「属性魔法」不同，$R;" +
                                               "是利用『次元的扭转』中，$R;" +
                                               "所产生的能源。$R;" +
                                               "$R有关『次元的扭转』，$R;" +
                                               "现在还没有详细的资料。$R;" +
                                               "$P虽然抱有疑问的人很多…$R;" +
                                               "泰达尼亚和多米尼翁，$R;" +
                                               "一起生活过的「那个世界」…$R;" +
                                               "$R如果知道去「那个世界」的方法的话，$R;" +
                                               "就可以大规模扩展$R;" +
                                               "「新生魔法」的研究呢!$R;", "麦吉克");
                        break;

                    case 2:
                        Say(pc, 11000076, 131, "「新生魔法」是不会受属性拘束的。$R;" +
                                               "这个应该从总管那里听说过吧?$R;" +
                                               "$R这个其实是什么意思呢?$R;" +
                                               "让我来替您做个解释一下吧。$R;" +
                                               "$P属性这东西有强弱关系。$R;" +
                                               "$R火比水强，但比风弱，$R;" +
                                               "风比火强，但比土弱。$R;" +
                                               "$P火之力强的地方，$R;" +
                                               "火之魔法就很强。$R;" +
                                               "但是水之魔法威力就接近0。$R;" +
                                               "$P所以只有水之魔法的话，$R;" +
                                               "在火之力强的地方战斗就特别困难。$R;" +
                                               "$P当然其他属性情况也一样。$R;" +
                                               "$P使用「属性魔法」的人，$R;" +
                                               "在不同地方能力也会跟着不同。$R;" +
                                               "$R而且魔物也有属性之分，$R;" +
                                               "所以要算好强弱关系才好!$R;" +
                                               "$P挺复杂吧?$R;" +
                                               "$R但是在「新生魔法」里没有这些麻烦，$R;" +
                                               "不管在什么地方面对什么魔物，$R;" +
                                               "魔法的效果都不会变的。$R;" +
                                               "$P我认为「新生魔法」这样比较方便，$R;" +
                                               "可是每个人的想法都不同呢。$R;", "麦吉克");
                        break;

                    case 3:
                        Say(pc, 11000076, 131, "告诉您几个具代表性的魔法吧!$R;" +
                                               "$P攻击魔法的『魔法箭』。$R;" +
                                               "$P以分身欺骗敌人的『魔法师的残像』。$R;" +
                                               "$P保护身体，$R;" +
                                               "免受风雪、灼热火花伤害的$R;" +
                                               "『温暖的气流』和『抗火护盾』。$R;" +
                                               "$P提高防御力的『魔力护盾』。$R;" +
                                               "$P让魔物中毒的『剧毒诅咒』。$R;" +
                                               "$P除了这些，还有很多呢!$R;", "麦吉克");
                        break;

                    case 4:
                        Say(pc, 11000076, 131, "「新生魔法」是最近才发现的，$R;" +
                                               "所以历史比较短。$R;" +
                                               "$P为了跟「属性魔法」区分，$R;" +
                                               "以新产生的魔法的意思命名的。$R;", "麦吉克");
                        break;
                }

                selection = Select(pc, "想知道什么呢?", "", "关于「新生魔法」", "「新生魔法」跟「属性魔法」有什么不同?", "有什么魔法?", "「新生魔法」名字的由来?", "不用了");
            } 

            Say(pc, 11000076, 131, "下次再来!$R;", "麦吉克");
        }
    }
}
