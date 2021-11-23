using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M21193000
{
    public class S11001814 : Event
    {
        public S11001814()
        {
            this.EventID = 11001814;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 0, "唔！真美味！$R;" +
            "$R不愧是、我看得上眼的店子！$R;" +
            "$P待會、我不只靜靜地品味！$R;" +
            "$R這最幸福的時間我也要品味唷。$R;", "吃飯中的男子");

            //
            /*
            Say(pc, 0, "うん！美味い！$R;" +
            "$R流石、僕が見込んだだけの店だよ！$R;" +
            "$Pおっと、落ち着いて味わって食べないと！$R;" +
            "$Rこの至福の時間も味わうのだよ。$R;", "食事中の男");
            */
        }
    }
}