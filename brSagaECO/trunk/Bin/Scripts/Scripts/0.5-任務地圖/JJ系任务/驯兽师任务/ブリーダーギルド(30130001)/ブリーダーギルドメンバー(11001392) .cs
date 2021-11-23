using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30130001
{
    public class S11001392 : Event
    {
        public S11001392()
        {
            this.EventID = 11001392;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 11001391, 131, "ここからは関係者以外$R;" + 
            "立ち入り禁止……です。$R;", "ブリーダーギルドメンバー");

        }
    }
}