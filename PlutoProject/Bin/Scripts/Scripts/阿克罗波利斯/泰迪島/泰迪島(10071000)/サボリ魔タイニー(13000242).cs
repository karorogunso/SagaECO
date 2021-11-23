using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
namespace SagaScript.M10071000
{
    public class S13000242 : Event
    {
        public S13000242()
        {
            this.EventID = 13000242;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 135, "ふぃ～休憩中だよ～$R;" +
            "再開予定はないよ～$R;", "サボリ魔タイニー");

        }
    }
}