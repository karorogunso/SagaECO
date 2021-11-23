using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10050000
{
    public class S11000345 : Event
    {
        public S11000345()
        {
            this.EventID = 11000345;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<Job2X_11> Job2X_11_mask = pc.CMask["Job2X_11"];
            //冒险者的转职部分

            if (Job2X_11_mask.Test(Job2X_11.轉職開始) && CountItem(pc, 50035651) == 0 && pc.Job == PC_JOB.RANGER)
            {
                转职(pc);
                return;
            }
            
            Say(pc, 131, "你好~$R;" +
                "$R我在散步中，啦啦啦~♪$R;");
        }

        void 转职(ActorPC pc)
        {

            Say(pc, 131, "啊?$R;" +
                "在找『冰靈之花』?$R;" +
                "$R冰靈之花是把『開朗小花』用魔法冰凍的$R;" +
                "$P去我們島的雖然很多…$R;" +
                "$R不過只要有開朗小花$R;" +
                "我可以用魔法冰凍後作給你的$R;");
            switch (Select(pc, "怎麽做呢?", "", "那就拜託了", "不用了"))
            {
                case 1:
                    if (CountItem(pc, 50035600) >= 1)
                    {
                        if (CheckInventory(pc, 50035651, 1))
                        {
                            if (pc.Marionette == null)//ISMARIO
                            {
                                Say(pc, 131, "是…冰靈之花是非常敏感的花$R;" +
                                    "$R因爲人們的手太暖和了$R;" +
                                    "所以會融化掉$R;" +
                                    "$P所以變身成活動木偶狀態下給你~$R;");
                                return;
                            }
                            GiveItem(pc, 50035651, 1);
                            TakeItem(pc, 50035600, 1);
                            ShowEffect(pc, 8028);
                            Say(pc, 131, "啊，那個啊!$R;");
                            Say(pc, 131, "『開朗小花』成了『冰靈之花』!$R;");
                            return;
                        }
                        Say(pc, 131, "東西好像太多了$R;");
                        return;
                    }
                    Say(pc, 131, "那把開朗小花拿過來吧$R;");
                    break;
                case 2:
                    break;
            }
        }
    }
}
