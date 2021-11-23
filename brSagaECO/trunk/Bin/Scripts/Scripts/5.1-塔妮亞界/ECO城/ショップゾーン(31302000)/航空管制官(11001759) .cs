using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M31302000
{
    public class S11001759 : Event
    {
        public S11001759()
        {
            this.EventID = 11001759;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "飛空庭は出せるけど$R;" +
            "移動するのは危険だよ。$R;" +
            "$Rあのくじら山に$R;" +
            "吸い寄せられるような感じで$R;" +
            "墜落してしまうんだ。$R;", "航空管制官");
}
}

        
    }


