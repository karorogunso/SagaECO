using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
//所在地圖:東方海角(10018003) NPC基本信息:飛空庭繩子(11001434) X:32 Y:50
namespace SagaScript.M10018003
{
    public class S11001434 : Event
    {
        public S11001434()
        {
            this.EventID = 11001434;
        }

        public override void OnEvent(ActorPC pc)
        {
            byte x, y;

            Say(pc, 11001433, 131, "那是連接飛空庭的繩子，$R" +
                                   "上去看看裡面吧。$R;" +
                                   "$P想結束指導的話，$R;" +
                                   "可以把您送到城市裡唷，您想怎做呢?$R;", "瑪莎");

            switch (Select(pc, "去飛空庭嗎?", "", "去飛空庭", "進行下一個指導", "結束指導", "現在還沒有準備好"))
            {
                case 1:
                    Say(pc, 11001433, 131, "那麼走吧!$R;" +
                                           "$R進去後，再仔細告訴您。$R;", "瑪莎");

                    x = (byte)Global.Random.Next(7, 7);
                    y = (byte)Global.Random.Next(12, 12);

                    Warp(pc, 30201000, x, y);
                    break;

                case 2:
                    Say(pc, 11001433, 131, "「飛空庭」的情報都知道了嗎?$R;" +
                                           "$R那麼，$R;" +
                                           "用「飛空庭」送您去下一個地點吧!$R;", "瑪莎");

                    x = (byte)Global.Random.Next(171, 174);
                    y = (byte)Global.Random.Next(100, 103);

                    Warp(pc, 100250001, x, y);
                    break;

                case 3:
                    Say(pc, 11001433, 131, "知道了，$R;" +
                                           "把您送到城市裡吧?$R;" +
                                           "$R;「提多」就在橋上唷，$R;" +
                                           "剩下的去問他吧!$R;" +
                                           "$P那麼加油吧!!$R;", "瑪莎");

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
