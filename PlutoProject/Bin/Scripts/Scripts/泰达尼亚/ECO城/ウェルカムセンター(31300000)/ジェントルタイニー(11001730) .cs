using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M31300000
{
    public class S11001730 : Event
    {
        public S11001730()
        {
            this.EventID = 11001730;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "仕事で立ち寄ったこの街で$R;" +
            "数千年も足止めじゃ。$R;" +
            "$Rうーむ、困った困った……。$R;", "ジェントルタイニー");
        }


    }
}


