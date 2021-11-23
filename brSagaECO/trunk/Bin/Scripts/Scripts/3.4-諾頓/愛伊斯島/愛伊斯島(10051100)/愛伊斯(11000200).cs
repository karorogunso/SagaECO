using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10051100
{
    public class S11000200 : Event
    {
        public S11000200()
        {
            this.EventID = 11000200;
        }

        public override void OnEvent(ActorPC pc)
        {
            if (CountItem(pc, 10011000) >= 1)
            {
                Say(pc, 131, "從您身上，可以感覺到強大的水晶波動阿$R;" +
                    "難道您身上有$R『奇怪的水晶』嗎？$R;");
                return;
            }
            Say(pc, 131, "唉…$R不知多久沒有$R和活著的人見面了…$R;");
        }
    }
}