using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30162001
{
    public class S11000227 : Event
    {
        public S11000227()
        {
            this.EventID = 11000227;
        }

        public override void OnEvent(ActorPC pc)
        {
            int selection;
            if (pc.Gender == PC_GENDER.MALE)
            {
                Say(pc, 131, "男士請回$R;");
                Warp(pc, 10065000, 70, 6);
                return;
            }
            selection = Global.Random.Next(1, 2);
            switch (selection)
            {
                case 1:
                    Say(pc, 131, "這裡是諾頓王國軍本部$R;");
                    break;
                case 2:
                    Say(pc, 131, "諾頓王國軍的$R;" +
                        "男女關係不好是吧…$R;" +
                        "$R關係搞好的話，多棒呀$R;");
                    break;
            }
        }
    }
}
