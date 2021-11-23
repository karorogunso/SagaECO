using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M21193000
{
    public class S11001802 : Event
    {
        public S11001802()
        {
            this.EventID = 11001802;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 0, "私に取っての芸術は、$R;" +
            "私の魂を世に残すことなのだよ。$R;" +
            "$R私がこの世から消えてしまっても、$R;" +
            "私の魂は生き続ける…。$R;" +
            "$P私が思うに、絵を描く上で重要なのは、$R;" +
            "私自身の心なのだ。$R;" +
            "$R私は今、心の準備をしている。$R;" +
            "私が絵を描くのは、その後なのだよ。$R;", "芸術家");
        }
    }
}