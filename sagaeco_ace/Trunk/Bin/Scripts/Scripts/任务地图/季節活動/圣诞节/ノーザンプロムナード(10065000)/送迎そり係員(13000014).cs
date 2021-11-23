using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10065000
{
    public class S13000014 : Event
    {
        public S13000014()
        {
            this.EventID = 13000014;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "ただいまこちらでは$R;" +
            "「アクロポリスシティ」行きの$R;" +
            "そりが出ております。$R;" +
            "$Rお乗りになりますか？$R;", "送迎そり係員");
            if (Select(pc, "乗る？", "", "乗らない", "乗る") == 2)
            {
                Say(pc, 131, "それでは出発しまーす！$R;", "送迎そり係員");
                PlaySound(pc, 2707, false, 100, 50);
                Warp(pc, 10023000, 129, 141);
            }
        }
    }
}