using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M50038000
{
    public class S10001029 : Event
    {
        public S10001029()
        {
            this.EventID = 10001029;
        }

        public override void OnEvent(ActorPC pc)
        {
            pc.CInt["Neko_06_Map_05"] = CreateMapInstance(50039000, 10023000, 135, 64);
            Warp(pc, (uint)pc.CInt["Neko_06_Map_05"], 9, 14);
        }
    }
}