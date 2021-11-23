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

        }
    }
}


        
   


