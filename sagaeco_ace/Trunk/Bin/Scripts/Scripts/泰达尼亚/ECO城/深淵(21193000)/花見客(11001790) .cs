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
            Say(pc, 0, "花は良いんだけどさ…$R;" +
            "$R歌はいらないのさ…$R;" +
            "$Rあいつしか歌わないしさ…$R;" +
            "$Pそれはそうと、$R;" +
            "お腹空いたなぁ。$R;" +
            "$P買出しに行ったアイツ、$R;" +
            "いつ帰ってくるのかなぁ。$R;", "花見客");
        }
    }
}