using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30162000
{
    public class S11000225 : Event
    {
        public S11000225()
        {
            this.EventID = 11000225;
        }

        public override void OnEvent(ActorPC pc)
        {

            int selection;
            if (pc.Gender == PC_GENDER.FEMALE)
            {
                Say(pc, 131, "女士請回$R;");
                Warp(pc, 10065000, 32, 6);
                return;
            }
            selection = Global.Random.Next(1, 2);
            switch (selection)
            {
                case 1:
                    Say(pc, 131, "這裡是諾頓王國軍的本部$R;");
                    break;
                case 2:
                    Say(pc, 131, "諾頓王國軍的$R;" +
                       "男女關係不好吧…$R;" +
                       "$R不知道為什麼呀$R;");
                    break;
            }
        }
    }
}
