using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M31301000
{
    public class S11001886 : Event
    {
        public S11001886()
        {
            this.EventID = 11001886;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 0, "（……それにしてもあの格好$R;" +
            "恥ずかしくないのかしら。）$R;", "気になる女");

            Say(pc, 0, 0, "ふんどしを身につけた$R;" +
            "トップモデルの視線を$R;" +
            "激しく感じる……。$R;", " ");

            Say(pc, 0, "（もうっ、こっちを見ないで欲しいわ。$R;" +
            "なんだかこっちまで恥ずかしく$R;" +
            "なっちゃう。）$R;", "気になる女");
}
}

        
    }


