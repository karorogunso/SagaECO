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
            Say(pc, 131, "唔!这里是铁匠铺$R;" +
                "$R在这里可以用你们搜集得来的材料$R冶炼成铁后制作武器$R;" +
                "$R有什么需要的武器吗?$R;");

            switch (Select(pc, "想做什么?", "", "委托制作武器", "委托制作防具", "委托冶炼金属", "委托制作『弓箭』", "什么都不做"))
            {
                case 1: 
                switch (Select(pc, "想制作什么?", "", "制作武器", "制作魔杖", "制作弓", "放弃"))
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
