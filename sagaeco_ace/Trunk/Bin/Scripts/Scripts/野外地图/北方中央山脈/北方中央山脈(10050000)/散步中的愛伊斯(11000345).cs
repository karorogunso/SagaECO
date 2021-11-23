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
                "在找『冰光花』?$R;" +
                "$R冰灵之花是把『幸福花饰』用魔法冰冻的$R;" +
                "$P去我们岛的虽然很多…$R;" +
                "$R不过只要有幸福花饰$R;" +
                "我可以用魔法冰冻后作给你的$R;");
            switch (Select(pc, "怎么做呢?", "", "那就拜託了", "不用了"))
            {
                case 1:
                    if (CountItem(pc, 50035600) >= 1)
                    {
                        if (CheckInventory(pc, 50035651, 1))
                        {
                            if (pc.Marionette == null)//ISMARIO
                            {
                                Say(pc, 131, "是…冰光花是非常敏感的花$R;" +
                                    "$R因为人们的手太暖和了$R;" +
                                    "所以会融化掉$R;" +
                                    "$P所以变身成活动木偶状态下给你~$R;");
                                return;
                            }
                            GiveItem(pc, 50035651, 1);
                            TakeItem(pc, 50035600, 1);
                            ShowEffect(pc, 8028);
                            Say(pc, 131, "啊，那个啊!$R;");
                            Say(pc, 131, "『幸福花饰』成了『冰光花』!$R;");
                            return;
                        }
                        Say(pc, 131, "东西好像太多了$R;");
                        return;
                    }
                    Say(pc, 131, "那把幸福花饰拿过来吧$R;");
                    break;
                case 2:
                    break;
            }
        }
    }
}
