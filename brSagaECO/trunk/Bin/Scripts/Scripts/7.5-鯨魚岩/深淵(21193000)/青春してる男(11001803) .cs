using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M21193000
{
    public class S11001803 : Event
    {
        public S11001803()
        {
            this.EventID = 11001803;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 0, "已、經已到秋天了呢。$R;" +
            "季節的更動真快吶～…。$R;" +
            "$P…有甚麼一直都是這樣子的、$R;" +
            "不愧是風格主義吶。$R;", "青春してる男");

            //
            /*
            Say(pc, 0, "も、もう秋なんだね。$R;" +
            "季節が流れるのは早いなぁ～…。$R;" +
            "$P…何かいつもこんなのじゃ、$R;" +
            "流石にマンネリだな。$R;", "青春してる男");
            */
        }
    }
}
 