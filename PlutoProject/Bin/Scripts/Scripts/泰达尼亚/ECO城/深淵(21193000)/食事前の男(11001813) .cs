using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M21193000
{
    public class S11001813 : Event
    {
        public S11001813()
        {
            this.EventID = 11001813;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 0, "注文してから食べ物が来るまでの間って、$R;" +
            "随分と時間が長く感じるよね。$R;" +
            "$Pまだかなまだかな～？$R;" +
            "そわそわ。$R;", "食事前の男");
        }
    }
}