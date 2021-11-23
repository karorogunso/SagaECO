
using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
using SagaMap.ActorEventHandlers;
namespace SagaScript.M30210000
{
    public partial class S70001000 : Event
    {
        void 序章开始(ActorPC pc)
        {
            pc.TInt["AAA序章剧情图"] = CreateMapInstance(60904000, 10054000, 144, 152,true,0,true);
            Warp(pc, (uint)pc.TInt["AAA序章剧情图"], 83, 35);
            SetNextMoveEvent(pc, 70001001);
        }
    }
}

