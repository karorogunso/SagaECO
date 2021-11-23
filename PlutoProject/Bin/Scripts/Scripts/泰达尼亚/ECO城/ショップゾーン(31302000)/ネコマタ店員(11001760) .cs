using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M31302000
{
    public class S11001760 : Event
    {
        public S11001760()
        {
            this.EventID = 11001760;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "ようこそ！$R;" +
            "ネコマタレストランへ！$R;" +
            "$R本日、当店のオススメは$R;" +
            "「うにヘルメット」となっております。$R;" +
            "$Rトゲトゲヘッドが、彼氏$R;" +
            "彼女のハートを射止めること$R;" +
            "間違いなしですっ♪$R;", "ネコマタ店員");
            OpenShopBuy(pc, 407);

            Say(pc, 131, "またきてね～♪$R;", "ネコマタ店員");
}
}

        
    }


