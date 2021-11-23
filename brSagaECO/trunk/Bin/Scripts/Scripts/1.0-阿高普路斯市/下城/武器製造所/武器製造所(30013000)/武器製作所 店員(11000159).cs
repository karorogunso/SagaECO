using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:武器製造所(30013000) NPC基本信息:武器製作所 店員(11000159) X:3 Y:1
namespace SagaScript.M30013000
{
    public class S11000159 : Event
    {
        public S11000159()
        {
            this.EventID = 11000159;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "唔!這裡是武器製作所$R;" +
                "$R在這裡可以用你們搜集得來的材料$R冶煉成鐵後製作武器$R;" +
                "$R有什麼需要的武器嗎?$R;");

            switch (Select(pc, "想做什麼?", "", "委託製作武器", "委託製作防具", "委託冶煉金屬", "委託製作『弓箭』", "什麼都不做"))
            {
                case 1: 
                switch (Select(pc, "想製作什麼?", "", "製作武器", "製作魔杖", "製作弓", "放棄"))
                    {
                        case 1:
                            Synthese(pc, 2010, 10);
                            break;
                        case 2:
                            Synthese(pc, 2021, 5);
                            break;
                        case 3:
                            Synthese(pc, 2034, 5);
                            break;
                    }
                    break;
                case 2:
                    Synthese(pc, 2017, 5);
                    break;
                case 3:
                    Synthese(pc, 2051, 3);
                    break;
                case 4:
                    Synthese(pc, 2035, 5);
                    break;
            }

        }
    }
}
