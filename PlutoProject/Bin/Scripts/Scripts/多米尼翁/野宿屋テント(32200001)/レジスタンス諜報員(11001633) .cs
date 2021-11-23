using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
namespace SagaScript.M32200001
{
    public class S11001633 : Event
    {
        public S11001633()
        {
            this.EventID = 11001633;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 11001577, 135, "あ、あっ……。$R;" +
            "その人、起こさないで……。$R;", "野宿屋");

            Say(pc, 136, "……大丈夫、お嬢さん。$R;", "レジスタンス諜報員");

            Say(pc, 136, "……嬢ちゃん、ここから東の$R;" +
            "西アクロニア平原には$R;" +
            "くれぐれも近づかないようにな。$R;" +
            "$Rあそこは危険だ。$R;", "レジスタンス諜報員");
        }
    }
}

        
   


