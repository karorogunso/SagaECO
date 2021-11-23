using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10057000
{
    public class S12001098 : Event
    {
        public S12001098()
        {
            this.EventID = 12001098;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<PSTFlags> mask = pc.CMask["PST"];
            Say(pc, 131, "唧唧！唧唧！唧唧啾！$R;");
            if (mask.Test(PSTFlags.開始尋找小雞) && !mask.Test(PSTFlags.找到2號小雞))
            {
                switch (Select(pc, "怎么办?", "", "抓住小鸡！", "什么都不做"))
                {
                    case 1:
                        mask.SetValue(PSTFlags.找到2號小雞, true);
                        Say(pc, 131, "唧唧！唧唧！唧唧啾！$R;");
                        //NPCPICTCHANGE 12001098 12001103
                        Say(pc, 131, "它在发脾气不让人摸喔$R;");
                        break;
                }
                return;
            }
        }
    }
}