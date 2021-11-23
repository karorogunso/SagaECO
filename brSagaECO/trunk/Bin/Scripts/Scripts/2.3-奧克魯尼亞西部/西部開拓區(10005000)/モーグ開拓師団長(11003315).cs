using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;

namespace SagaScript.M10005000
{
    public class S11003315 : Event
    {
        public S11003315()
        {
            this.EventID = 11003315;
        }

        public override void OnEvent(ActorPC pc)
        {
            /*開發中 進度
            基本資料O
            原文搬運O
            翻譯校對O
            細節修正X
            */
            BitMask<ExpandedArea> ExpandedArea_mask = pc.CMask["ExpandedArea"];
            //int selection
            if ((!ExpandedArea_mask.Test(ExpandedArea.已得到油)))
            {
                ExpandedArea_mask.SetValue(ExpandedArea.已跟開拓師團長對話, true);
                Say(pc, 0, "假如某處良い速遞員でも$R;" +
                    "いれば助かるのだか……。$R;", "モーグ開拓師団長");
                /*
                Say(pc, 0, "どこか良い運び屋でも$R;" +
                    "いれば助かるのだか……。$R;", "モーグ開拓師団長");
                */
                switch (Select(pc, "要甚樣做？", "", "說來……", "甚麼也不做"))
                {
                    //switch (Select(pc, "どうする？", "", "そういえば……", "何もしない"))
                case 1:
                    Say(pc, 0, "現在就去叫他過來幹活吧$R;" +
                        "……。$R;", "モーグ開拓師団長");
                    break;
                case 2:
                    return;
                }
                if ((ExpandedArea_mask.Test(ExpandedArea.已得到油)))
                {
                    return;
                }
            }
        }
    }
}
