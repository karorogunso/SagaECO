using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Item;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30095000
{
    public class S11000854 : Event
    {
        public S11000854()
        {
            this.EventID = 11000854;

        }

        public override void OnEvent(ActorPC pc)
        {
            /*
            if (_6a70)
            {
                Say(pc, 131, "怎麼辦，不好意思…$R;"); 
                return;
            }
            */
            Say(pc, 131, "现在有点忙$R;");

        }
    }
}