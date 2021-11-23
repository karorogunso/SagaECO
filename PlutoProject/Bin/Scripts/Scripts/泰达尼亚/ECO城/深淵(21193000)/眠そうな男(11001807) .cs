using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M21193000
{
    public class S11001807 : Event
    {
        public S11001807()
        {
            this.EventID = 11001807;
        }

        public override void OnEvent(ActorPC pc)
        {

            Say(pc, 0, "……。$R;" +
            "$P………。$R;" +
            "$P…………ハッ！？$R;" +
            "$Rついつい眠っちゃったよ。$R;" +
            "危ない危ない！$R;" +
            "$P…でもやっぱ眠いなぁ……。$R;" +
            "$R……。$R;", "眠そうな男");
        }
    }
}