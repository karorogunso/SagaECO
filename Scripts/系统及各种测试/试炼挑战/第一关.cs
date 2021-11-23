
using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace WeeklyExploration
{
    public partial class Trial : Event
    {
        void 开始第一关(ActorPC pc)
        {
            //pc.TInt["试炼临时地图"] = CreateMapInstance(10001001, 90001000, 35, 15, true, 999, true);
            //uint mapid = (uint)pc.TInt["试炼临时地图"];
            Warp(pc, 10001001, 194, 251);
        }
    }
}

