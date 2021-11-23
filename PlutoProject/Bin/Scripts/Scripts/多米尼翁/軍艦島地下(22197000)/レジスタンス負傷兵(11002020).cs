using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M22197000
{
    public class S11002020 : Event
    {
        public S11002020()
        {
            this.EventID = 11002020;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 112, "イタたたたた。$R;", "レジスタンス負傷兵");

            Say(pc, 11002018, 131, "フフフフフ$R;" +
            "なかなか改造……もとい$R;" +
            "治し甲斐のありそうな元気な患者だねぇ$R;", "レジスタンスドクター");

            Say(pc, 112, "え？$R;", "レジスタンス負傷兵");

            Say(pc, 11002018, 131, "なに、悪いようにはせんさ。$R;" +
            "ちょっとだけ研究の……。$R;", "レジスタンスドクター");

            Say(pc, 112, "あ、いや、えっと、すみません！$R;" +
            "この傷、気のせいみたいです！$R;", "レジスタンス負傷兵");

            Say(pc, 11002019, 131, "ドクター！$R;" +
            "あまり若い子をからかわないで下さい。$R;" +
            "かわいそうじゃないですか！$R;", "レジスタンスナース");

            Say(pc, 11002018, 131, "あ～、スマンスマン。$R;" +
            "患者をからかうのは我輩の悪い癖でな。$R;" +
            "自称『腕は確か』だから安心したまえ。$R;", "レジスタンスドクター");

            Say(pc, 112, "じ、自称！？$R;", "レジスタンス負傷兵");

        }
    }
}


        
   


