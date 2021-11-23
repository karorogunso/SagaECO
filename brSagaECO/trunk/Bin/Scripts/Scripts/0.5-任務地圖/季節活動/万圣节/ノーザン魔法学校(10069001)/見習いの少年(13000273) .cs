using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10069001
{
    public class S13000273 : Event
    {
        public S13000273()
        {
            this.EventID = 13000273;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "この上はかぼちゃ畑だよ。$R;" +
            "$Rパンプキンの種がとれるけど$R;" +
            "専門家と一緒じゃないと難しいかな？$R;", "見習いの少年");
        }
    }
}