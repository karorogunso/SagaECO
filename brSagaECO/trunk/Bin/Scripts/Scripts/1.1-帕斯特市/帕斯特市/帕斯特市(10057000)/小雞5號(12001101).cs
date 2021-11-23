using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10057000
{
    public class S12001101 : Event
    {
        public S12001101()
        {
            this.EventID = 12001101;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<PSTFlags> mask = pc.CMask["PST"];
            Say(pc, 131, "唧唧！唧唧！唧唧啾！$R;");
            if (mask.Test(PSTFlags.開始尋找小雞) && !mask.Test(PSTFlags.找到5號小雞))
            {
                switch (Select(pc, "怎麼辦?", "", "抓住小雞！", "什麽都不做"))
                {
                    case 1:
                        mask.SetValue(PSTFlags.找到5號小雞, true);
                        Say(pc, 131, "唧唧！唧唧！唧唧啾！$R;");
                        //NPCPICTCHANGE 12001098 12001103
                        Say(pc, 131, "牠在發脾氣不讓人摸喔$R;");
                        break;
                }
                return;
            }
        }
    }
}