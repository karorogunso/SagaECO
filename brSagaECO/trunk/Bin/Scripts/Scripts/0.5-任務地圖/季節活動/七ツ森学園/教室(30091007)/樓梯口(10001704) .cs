using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30091007
{
    public class S10001704 : Event
    {
        public S10001704()
        {
            this.EventID = 10001704;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<nanatumori> nanatumori_mask = new BitMask<nanatumori>(pc.CMask["nanatumori"]);
            //int selection;
            if (nanatumori_mask.Test(nanatumori.地下室))
            {
               switch (Select(pc, "どこへいく？", "", "美術室へ", "廊下へ", "生物室へ", "地下室へ", "ここにとどまる"))
               {
                   case 1:
                       Warp(pc, 30158000, 8, 12);
                       break;
                   case 2:
                       Warp(pc, 20129001, 7, 42);
                       break;
                   case 3:
                       Warp(pc, 30152002, 6, 10);
                       break;
                   case 4:
                       Warp(pc, 30159000, 9, 12);
                       break;
               }
               return;
            }


           switch (Select(pc, "どこへいく？", "", "美術室へ", "廊下へ", "生物室へ", "ここにとどまる"))
            {
               case 1:
                   Warp(pc, 30158000, 8, 12);
                   break;
               case 2:
                   Warp(pc, 20129001, 7, 42);
                   break;
               case 3:
                   Warp(pc, 30152002, 6, 10);
                   break;

            }

        }
    }
}