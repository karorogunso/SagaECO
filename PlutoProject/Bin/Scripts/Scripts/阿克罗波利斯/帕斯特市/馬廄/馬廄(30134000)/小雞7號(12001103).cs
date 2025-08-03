using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30134000
{
    public class S12001103 : Event
    {
        public S12001103()
        {
            this.EventID = 12001103;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<PSTFlags> mask = pc.CMask["PST"];
            Say(pc, 131, "唧唧！唧唧！唧唧啾！$R;");
            if (mask.Test(PSTFlags.開始尋找小雞) && !mask.Test(PSTFlags.找到7號小雞))//_5a08 && !_5a15)
            {
                switch (Select(pc, "怎么办?", "", "抓住小鸡！", "什么都不做"))
                {
                    case 1:
                        mask.SetValue(PSTFlags.找到7號小雞, true);
                        //_5a15 = true;
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