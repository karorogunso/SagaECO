using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
namespace SagaScript.M10071000
{
    public class S13000246 : Event
    {
        public S13000246()
        {
            this.EventID = 13000246;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 134, "うっ！$R;" +
            "背が低くて…届かない…$R;", "屋台タイニー");

        }
    }
}