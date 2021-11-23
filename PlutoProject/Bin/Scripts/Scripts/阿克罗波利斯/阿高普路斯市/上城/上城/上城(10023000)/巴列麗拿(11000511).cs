using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
//所在地圖:上城(10023000) NPC基本信息:巴列麗拿(11000511) X:126 Y:62
namespace SagaScript.M10023000
{
    public class S11000511 : Event
    {
        public S11000511()
        {
            this.EventID = 11000511;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<NDFlags> mask = new BitMask<NDFlags>(pc.CMask["ND"]);

            mask.SetValue(NDFlags.中央的巴列麗拿, true);
        }
    }
}
