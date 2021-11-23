using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Item;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30099001
{
    public class S11000866 : Event
    {
        public S11000866()
        {
            this.EventID = 11000866;
        }

        public override void OnEvent(ActorPC pc)
        {
            /*
            if (_6a69)
            {
                Say(pc, 131, "一直在監視他…$R;" +
                    "原來那小子不是犯人呀？$R;");
                return;
            }
            */
            Say(pc, 131, "那銀髮的暗殺者…$R;" +
                "新來的傢伙…$R不知原因，總覺得懷疑他們呀$R;");
        }
    }
}