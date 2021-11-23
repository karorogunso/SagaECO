using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
//所在地圖:東方海角(10018001) NPC基本信息:飛空庭繩子(11000937) X:32 Y:50
namespace SagaScript.M10018001
{
    public class S11000937 : Event
    {
        public S11000937()
        {
            this.EventID = 11000937;
        }

        public override void OnEvent(ActorPC pc)
        {
            byte x, y;

            Say(pc, 11000936, 131, "那是连接飞空庭的绳子，$R" +
                                   "上去看看里面吧。$R;" +
                                   "$P想结束指导的话，$R;" +
                                   "可以把您送到城市里，您想怎做呢?$R;", "玛莎");

            switch (Select(pc, "去飞空庭吗?", "", "去飞空庭", "进行下一个指导", "结束指导", "现在还没有准备好"))
            {
                case 1:
                    Say(pc, 11000936, 131, "那么走吧!$R;" +
                                           "$R进去后，再仔细告诉您。$R;", "玛莎");

                    x = (byte)Global.Random.Next(7, 7);
                    y = (byte)Global.Random.Next(12, 12);

                    Warp(pc, 30201000, x, y);
                    break;

                case 2:
                    Say(pc, 11000936, 131, "「飞空庭」的情报都知道了吗?$R;" +
                                           "$R那么，$R;" +
                                           "用「飞空庭」送您去下一个地点吧!$R;", "玛莎");

                    x = (byte)Global.Random.Next(171, 174);
                    y = (byte)Global.Random.Next(100, 103);

                    Warp(pc, 100250001, x, y);
                    break;

                case 3:
                    Say(pc, 11000936, 131, "知道了，$R;" +
                                           "把您送到城市里吧?$R;" +
                                           "$R;「泰塔斯」就在桥上，$R;" +
                                           "剩下的去问他吧!$R;" +
                                           "$P那么加油吧!!$R;", "玛莎");

                    x = (byte)Global.Random.Next(18, 29);
                    y = (byte)Global.Random.Next(124, 130);

                    Warp(pc, 10025001, x, y);
                    break;

                case 4:
                    break;
            }
        }
    }
}
