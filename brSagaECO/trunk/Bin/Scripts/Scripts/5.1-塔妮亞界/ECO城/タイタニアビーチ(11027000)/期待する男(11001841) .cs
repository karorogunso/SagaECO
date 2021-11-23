using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M11027000
{
    public class S11001841 : Event
    {
        public S11001841()
        {
            this.EventID = 11001841;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 0, "……まったく、期待ハズレだよ。$R;" +
            "ここなら見えると思ったのに……。$R;", "期待する男");

            Say(pc, 131, "うわっ！$R;" +
            "$Rな、なんだよ、おまえ！$R;" +
            "べ、別に脱衣所を$R;" +
            "覗いていた訳じゃないぞ！$R;", "期待する男");
}
}

        
    }


