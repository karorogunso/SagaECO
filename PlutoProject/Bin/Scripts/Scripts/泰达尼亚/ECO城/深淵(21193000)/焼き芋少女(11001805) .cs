using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M21193000
{
    public class S11001805 : Event
    {
        public S11001805()
        {
            this.EventID = 11001805;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 0, "焼き芋を焼いてるんです。$R;" +
            "美味しく焼き上がるかな？$R;" +
            "$P…あれれ？$R;" +
            "火が消えちゃってる！$R;" +
            "$Rまぁ、美味しい焼き芋のためなら、$R;" +
            "気長に火が付くのを待ちますよ～♪$R;", "焼き芋少女");
        }
    }
}