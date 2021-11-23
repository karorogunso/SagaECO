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
    public class S11001125 : Event
    {
        public S11001125()
        {
            this.EventID = 11001125;
        }

        public override void OnEvent(ActorPC pc)
        {
            if (pc.Marionette != null)
            {
                Say(pc, 255, "变成人类可不是件容易的事情，$R;");
                return;
            }
            Say(pc, 255, "不要说话$R;");
        }
    }
}