using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;

using SagaScript.Chinese.Enums;
namespace SagaScript.M12035000
{
    public class S11001782 : Event
    {
        public S11001782()
        {
            this.EventID = 11001782;
        }

        public override void OnEvent(ActorPC pc)
        {
            if(Select(pc, "ウェストフォートに行く？", "", "行かない", "行く")==2)
            {
         
                    Say(pc, 11001669, 65535, "じゃ、行くよ。$R;" +
                    "$R振り落とされないよう$R;" +
                    "しっかり捕まってンだよ！$R;", "レジスタンス傭兵");
                    Warp(pc, 12019000, 3, 79);
                    
            }
        }
    }
}