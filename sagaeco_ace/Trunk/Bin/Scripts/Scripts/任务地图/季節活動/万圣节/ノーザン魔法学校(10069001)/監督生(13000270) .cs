using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10069001
{
    public class S13000270 : Event
    {
        public S13000270()
        {
            this.EventID = 13000270;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "" + pc.Name + "様。$R;" +
            "$Rこの階段は$R;" +
            "城の外へと続く避難階段です。$R;" +
            "$R一方通行ですので$R;" +
            "ご注意くださいませ……。$R;", "監督生");
        }
    }
}