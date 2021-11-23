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
            Say(pc, 0, "相約好見面的時間、$R;" +
            "她沒有到來…。$R;" +
            "$R被爽約了麼…。$R;" +
            "$P不、也許有可能是有甚麼發生了！$R;" +
            "$R沒問題的吧？$R;" +
            "是不是發生了事故呢？$R;" +
            "$P嗚、擔心停不下來！$R;", "擔心中的男孩");

            //
            /*
            Say(pc, 0, "待ち合わせの時間になっても、$R;" +
            "彼女が来ないんだ…。$R;" +
            "$Rフラれちゃったのかなぁ…。$R;" +
            "$Pいや、ひょっとしたら何かあったのかも！$R;" +
            "$R大丈夫かな？$R;" +
            "事故にあったりしてないかな？$R;" +
            "$Pう～、心配でたまらないよ！$R;", "心配してる男");
            */
        }
    }
}
 