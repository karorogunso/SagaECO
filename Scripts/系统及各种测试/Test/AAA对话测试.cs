
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
    public class S60000134 : Event
    {
        public S60000134()
        {
            this.EventID = 60000134;
        }

        public override void OnEvent(ActorPC pc)
        {
           if (pc.Account.GMLevel > 200)
            {
            ushort id = ushort.Parse(InputBox(pc, "输入AAA的ID吧！", InputType.Bank));
            ShowDialog(pc, id);
            }
        }
    }
}

