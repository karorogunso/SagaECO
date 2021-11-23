using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10051100
{
    public class S11000201 : Event
    {
        public S11000201()
        {
            this.EventID = 11000201;
        }

        public override void OnEvent(ActorPC pc)
        {
            if (CountItem(pc, 10011000) >= 1)
            {
                Say(pc, 131, "哦！$R您的身上有『奇怪的水晶』？$R一定要見到我們族長喔。$R;");
                return;
            }
            Say(pc, 131, "歡迎到水晶之島$R;");
        }
    }
}