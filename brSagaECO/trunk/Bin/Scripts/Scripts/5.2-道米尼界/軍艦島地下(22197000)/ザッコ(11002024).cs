using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M22197000
{
    public class S11002024 : Event
    {
        public S11002024()
        {
            this.EventID = 11002024;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 11002025, 131, "ザッコ的兄長$R;" +
                "ポッ和出來的男孩隊長如此$R;" +
                "我不能令人信服。$R;", "ヨーワイ");

            Say(pc, 131, "閉嘴、ヨーワイ！$R;" +
            "$P……但是、那張撲克臉。$R;" +
            "感覺在某處看過$R;" +
            "最近在ゴカツヤク得意忘形$R;" +
            "又不是紅色雷神サマ。$R;" +
            "$P我的隊長、我的隊長是不是符合謠言所傳的實力$R;" +
            "在實戰中看看吧。$R;", "ザッコ");
            //
            /*
            Say(pc, 11002025, 131, "ザッコの兄貴$R;" +
            "ポッと出の坊やが隊長なんて$R;" +
            "俺は納得できませんぜ。$R;", "ヨーワイ");

            Say(pc, 131, "黙ってろ、ヨーワイ！$R;" +
            "$P……しかし、あのスカしたツラ。$R;" +
            "どっかで見たと思ったら$R;" +
            "最近ゴカツヤクで調子に乗ってやがる$R;" +
            "紅の雷神サマじゃねえか。$R;" +
            "$P噂の実力、俺らの隊長にふさわしいか否か$R;" +
            "実戦でお手並み拝見といこうぜ。$R;", "ザッコ");
            */

        }
    }
}


        
   


