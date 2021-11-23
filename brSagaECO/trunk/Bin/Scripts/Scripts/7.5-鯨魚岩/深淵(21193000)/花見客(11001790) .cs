using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M21193000
{
    public class S11001790 : Event
    {
        public S11001790()
        {
            this.EventID = 11001790;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 0, "花是很好不…$R;" +
            "$R不過不需要歌聲…$R;" +
            "$R那傢伙都不是在唱歌的…$R;" +
            "$P說來、$R;" +
            "腹中空空的呃。$R;" +
            "$P伙伴去了買東西、$R;" +
            "待下就回來呃。$R;", "賞花客");
            //
            /*
            Say(pc, 0, "花は良いんだけどさ…$R;" +
            "$R歌はいらないのさ…$R;" +
            "$Rあいつしか歌わないしさ…$R;" +
            "$Pそれはそうと、$R;" +
            "お腹空いたなぁ。$R;" +
            "$P買出しに行ったアイツ、$R;" +
            "いつ帰ってくるのかなぁ。$R;", "花見客");
            */
        }
    }
}
 