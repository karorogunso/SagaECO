using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Item;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10060000
{
    public class S11000817 : Event
    {
        public S11000817()
        {
            this.EventID = 11000817;

        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<MorgFlags> mask = pc.CMask["MorgFlags"];

            if (mask.Test(MorgFlags.獲得基本職業) ||
                mask.Test(MorgFlags.獲得技術職業) ||
                mask.Test(MorgFlags.獲得專門職業))
            {
                Say(pc, 131, "太感谢您了$R;");
                return;
            }

            Say(pc, 255, "一直盯着我这么看$R;", "");
            Say(pc, 131, "哇…有不认识的人$R;" +
                "妈妈~~~~~！$R;");

        }
    }
}