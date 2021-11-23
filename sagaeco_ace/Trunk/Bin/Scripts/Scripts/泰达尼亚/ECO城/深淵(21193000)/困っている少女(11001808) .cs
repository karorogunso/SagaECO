using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M21193000
{
    public class S11001808 : Event
    {
        public S11001808()
        {
            this.EventID = 11001808;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 0, "寒いから、暖炉に火を点けたいの。$R;" +
            "$Rでも火がないから無理なの。$R;" +
            "$Pこのままじゃ凍え死んじゃう！$R;" +
            "$R困った困った…。$R;", "困っている少女");
        }
    }
}