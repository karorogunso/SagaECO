using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
namespace SagaScript.M20070103
{
    public class S11001377 : Event
    {
        public S11001377()
        {
            this.EventID = 11001377;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 134, "がぁー！なんでこんな$R;" +
            "入り組んでるんだよ！$R;" +
            "$Rゴールはどこだ！？$R;", "アーク");

        }
    }
}