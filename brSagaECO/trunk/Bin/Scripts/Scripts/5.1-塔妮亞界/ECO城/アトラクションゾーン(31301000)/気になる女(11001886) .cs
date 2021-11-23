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
            Say(pc, 0, "（……即使如此那個裝束$R;" +
            "會不會感到羞怯的呢。）$R;", "很在意的女子");

            Say(pc, 0, 0, " 穿著纏腰布的$R;" +
            "頂級模特兒的視線$R;" +
            "很性感……。$R;", " ");

            Say(pc, 0, "（希望、不要再望向這邊了。$R;" +
            "不知為甚麼變得$R;" +
            "感到很羞怯。）$R;", "很在意的女子");

            //
            /*
            Say(pc, 0, "（……それにしてもあの格好$R;" +
            "恥ずかしくないのかしら。）$R;", "気になる女");

            Say(pc, 0, 0, "ふんどしを身につけた$R;" +
            "トップモデルの視線を$R;" +
            "激しく感じる……。$R;", " ");

            Say(pc, 0, "（もうっ、こっちを見ないで欲しいわ。$R;" +
            "なんだかこっちまで恥ずかしく$R;" +
            "なっちゃう。）$R;", "気になる女");
            */
        }
}

        
    }


