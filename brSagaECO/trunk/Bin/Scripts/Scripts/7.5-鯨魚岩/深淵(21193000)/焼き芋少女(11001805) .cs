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
            Say(pc, 0, "正在烤番薯。$R;" +
            "會不會烤得美味呢？$R;" +
            "$P…啊哩哩？$R;" +
            "火已經熄滅了！$R;" +
            "$R嘛、為了美味的烤番薯、$R;" +
            "所以會耐心地在慢火前守候喔～♪$R;", "紅薯少女");
            //
            /*
             Say(pc, 0, "焼き芋を焼いてるんです。$R;" +
            "美味しく焼き上がるかな？$R;" +
            "$P…あれれ？$R;" +
            "火が消えちゃってる！$R;" +
            "$Rまぁ、美味しい焼き芋のためなら、$R;" +
            "気長に火が付くのを待ちますよ～♪$R;", "焼き芋少女");
            */
        }
    }
}
 