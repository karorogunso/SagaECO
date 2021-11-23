using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M21193000
{
    public class S11001800 : Event
    {
        public S11001800()
        {
            this.EventID = 11001800;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 0, "YAー！$R;" +
            "海真是好呢ー！$R;" +
            "$R廣闊、涼快、說起夏天就是海呢！$R;" +
            "$P喂、看看這藍海！$R;" +
            "$R跟這個相比、$R;" +
            "人類甚麼的、$R;" +
            "是多麼渺小的啊！$R;", "遠くを見つめる男");

            //
            /*
            Say(pc, 0, "いやー！$R;" +
            "海は良いよなー！$R;" +
            "$R広いし、涼しいし、夏と言えば海だね！$R;" +
            "$Pほら見ろよ、この青い海！$R;" +
            "$Rこれに比べれば、$R;" +
            "人間なんて、$R;" +
            "ちっぽけなものなんだよな！$R;", "遠くを見つめる男");
            */
        }
    }
}
