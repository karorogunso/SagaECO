using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
//所在地圖:奧克魯尼亞東部平原(10025001) NPC基本信息:飛空庭繩子(11000942) X:174 Y:97
namespace SagaScript.M10025001
{
    public class S11000942 : Event
    {
        public S11000942()
        {
            this.EventID = 11000942;
        }

        public override void OnEvent(ActorPC pc)
        {
            byte x, y;

            Say(pc, 11000941, 131, "嗯? 想再去一次飞空庭吗?$R;", "玛莎");

            switch (Select(pc, "想再去一次飞空庭吗?", "", "想去!", "不，不用了"))
            {
                case 1:
                    x = (byte)Global.Random.Next(7, 7);
                    y = (byte)Global.Random.Next(12, 12);

                    Warp(pc, 30201000, x, y);
                    break;
                    
                case 2:
                    break;
            }
        }
    }
}
