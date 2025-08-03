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
                    "$R那个是摩戈炭吧?$R;" +
                    "卖给我吧？$R;" +
                    "我给您高价！$R;");
                Say(pc, 131, "今日的市价是1个$R;" +
                    a + "金币$R;");
                switch (Select(pc, "卖摩戈炭吗？", "", "卖", "不卖"))
                {
                    case 1:
                        while (CountItem(pc, 10016700) > mgt)
                        {
                            mgt++;
                        }
                        a += 250;
                        pc.Gold += (int)(mgt * a);
                        TakeItem(pc, 10016700, (ushort)mgt);
                        Say(pc, 131, "卖了摩戈炭" + mgt + "个,$R;" +
                        "得到" + mgt * a + "个金币$R;");
                        break;
                    case 2:
                        break;
                }
                return;
            }

            Say(pc, 131, "啊，真是热啊$R;" +
                "讨厌……$R;");
        }
    }
}