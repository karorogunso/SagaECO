using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M21193000
{
    public class S11001810 : Event
    {
        public S11001810()
        {
            this.EventID = 11001810;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 0, "待ち合わせの時間になっても、$R;" +
            "彼女が来ないんだ…。$R;" +
            "$Rフラれちゃったのかなぁ…。$R;" +
            "$Pいや、ひょっとしたら何かあったのかも！$R;" +
            "$R大丈夫かな？$R;" +
            "事故にあったりしてないかな？$R;" +
            "$Pう～、心配でたまらないよ！$R;", "心配してる男");
        }
    }
}