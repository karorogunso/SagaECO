using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M20021000
{
    public class S11000632 : Event
    {
        public S11000632()
        {
            this.EventID = 11000632;
        }

        public override void OnEvent(ActorPC pc)
        {
            int day = DateTime.Now.Day;
            int wday = int.Parse(DateTime.Now.DayOfWeek.ToString("d"));
            int a = day * 5 + wday * 10 + 100;
            int mgt = 0;
            
            if (CountItem(pc, 10016700) >= 1)
            {
                Say(pc, 131, "喂，$R;" +
                    "$R那個是摩根炭吧?$R;" +
                    "賣給我吧？$R;" +
                    "我給您高價！$R;");
                Say(pc, 131, "今日的市價是1個$R;" +
                    a + "金幣$R;");
                switch (Select(pc, "賣摩根炭嗎？", "", "賣", "不賣"))
                {
                    case 1:
                        while (CountItem(pc, 10016700) > mgt)
                        {
                            mgt++;
                        }
                        a += 250;
                        pc.Gold += (int)(mgt * a);
                        TakeItem(pc, 10016700, (ushort)mgt);
                        Say(pc, 131, "賣了摩根炭" + mgt + "個,$R;" +
                        "得到" + mgt * a + "個金幣$R;");
                        break;
                    case 2:
                        break;
                }
                return;
            }
            
            Say(pc, 131, "啊，真是熱啊$R;" +
                "討厭……$R;");
        }
    }
}