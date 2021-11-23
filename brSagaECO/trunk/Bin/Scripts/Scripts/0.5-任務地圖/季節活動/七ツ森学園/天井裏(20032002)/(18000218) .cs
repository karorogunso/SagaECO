using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
namespace SagaScript.M20032002
{
    public class S18000218 : Event
    {
        public S18000218()
        {
            this.EventID = 18000218;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<nanatumori> nanatumori_mask = new BitMask<nanatumori>(pc.CMask["nanatumori"]);
            //int selection;
            if (nanatumori_mask.Test(nanatumori.任務完成))
            {
                Say(pc, 0, 0, "何か崩れたような$R;" +
                "場所がある……。$R;" +
                "$Pここから、奥に$R;" +
                "進むことができそうだ……。$R;", "");
                PlaySound(pc, 2518, false, 100, 50);

                Say(pc, 0, 0, "石をとりのぞけば、$R;" +
                "どうにか入ることが$R;" +
                "できそうだ。$R;", "");
                Warp(pc, 50065000, 5, 8);
            }
        }
    }
}