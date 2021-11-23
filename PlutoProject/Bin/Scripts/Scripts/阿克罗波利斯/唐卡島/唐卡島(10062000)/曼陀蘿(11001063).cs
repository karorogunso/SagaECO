using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10062000
{
    public class S11001063 : Event
    {
        public S11001063()
        {
            this.EventID = 11001063;
        }

        public override void OnEvent(ActorPC pc)
        {
            if (pc.Marionette != null)
            {
                Say(pc, 131, "跟埃米尔打交道$R真是苦差使$R;");
                return;
            }
            Say(pc, 131, "您…好..$R;");
        }
    }
}