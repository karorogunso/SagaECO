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
            Say(pc, 131, "这地方是各位战士$R召募队伍队员的广场$R;" +
                "$R组成一个合作无间的……$R;" +
                "$R　　　　　“阿伊恩萨乌斯联邦”$R;");
        }
    }
}