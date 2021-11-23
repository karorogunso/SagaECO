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
            Say(pc, 131, "这里是摩戈炭冶炼所$R;" +
                "是买卖摩戈炭的地方哦$R;");
            switch (Select(pc, "", "", "卖摩戈炭", "买摩戈炭", "什么都不做"))
            {
                case 1:
                    Say(pc, 131, "今天的行情是每一个…$R;" +
                        a + "金币！$R;");
                    switch (Select(pc, "卖不卖摩戈炭呢？", "", "卖", "不卖"))
                    {
                        case 1:
                            while (CountItem(pc, 10016700) > mgt)
                            {
                                mgt++;
                            }
                            TakeItem(pc, 10016700, (ushort)mgt);
                            pc.Gold += (int)(mgt * a);
                            Say(pc, 131, "卖了摩戈炭" + mgt + "个,$R;" +
                            "得到了" + mgt * a + "金币$R;");
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