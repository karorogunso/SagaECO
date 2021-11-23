using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M21193000
{
    public class S11001818 : Event
    {
        public S11001818()
        {
            this.EventID = 11001818;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 0, "いいか！$R;" +
            "今こそ団結する時なのだ！$R;" +
            "$P我が団体の力……$R;" +
            "否！$R;" +
            "人類の力を合わせ、$R;" +
            "この問題に取り掛からなければならない！$R;" +
            "$P諸君らの力が必要なのだ！$R;" +
            "$R我々の力で、世界を動かすのだ！$R;", "大先生");
        }
    }
}