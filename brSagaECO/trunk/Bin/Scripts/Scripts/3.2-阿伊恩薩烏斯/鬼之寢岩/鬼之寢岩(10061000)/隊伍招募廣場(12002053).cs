using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
namespace SagaScript.M10061000
{
    public class S12002053 : Event
    {
        public S12002053()
        {
            this.EventID = 12002053;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "這地方是你們各位戰士$R召募隊伍隊員的廣場$R;" +
                "$R組成一個合作無間的……$R;" +
                "$R　　　　　『阿伊恩薩烏斯聯邦』$R;");
        }
    }
}