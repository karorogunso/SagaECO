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
    public class S11001122 : Event
    {
        public S11001122()
        {
            this.EventID = 11001122;
        }

        public override void OnEvent(ActorPC pc)
        {
            if (pc.Marionette != null)
            {
                Say(pc, 255, "唉，训练好烦呀。$R;");
                return;
            }
            Say(pc, 255, "不要妨碍我呀$R;");
        }
    }
}