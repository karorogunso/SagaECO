using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Item;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30099002
{
    public class S11000859 : Event
    {
        public S11000859()
        {
            this.EventID = 11000859;
        }

        public override void OnEvent(ActorPC pc)
        {
            int day = DateTime.Now.Day;
            int wday = int.Parse(DateTime.Now.DayOfWeek.ToString("d"));
            int a = day * 3 + wday * 4 + 10;
            int mgt = 0;
            Say(pc, 131, "這裡是摩根炭冶煉所$R;" +
                "是販買摩根炭的地方唷$R;");
            switch (Select(pc, "", "", "モーグ炭を売る", "モーグ炭を買う", "何もしない"))
            {
                case 1:
                    Say(pc, 131, "今天的行情是每一個…$R;" +
                        a + "金幣！$R;");
                    switch (Select(pc, "賣不賣摩根炭呢？", "", "賣", "不賣"))
                    {
                        case 1:
                            while (CountItem(pc, 10016700) > mgt)
                            {
                                mgt++;
                            }
                            TakeItem(pc, 10016700, (ushort)mgt);
                            pc.Gold += (int)(mgt * a);
                            Say(pc, 131, "賣了摩根炭" + mgt + "個,$R;" +
                            "得到了" + mgt * a + "金幣$R;");
                            break;
                        case 2:
                            break;
                    }
                    break;
                case 2:
                    OpenShopBuy(pc, 222);
                    break;
                case 3:
                    break;
            }
        }
    }
}