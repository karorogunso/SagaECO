using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M12035001
{
    public class S11001596 : Event
    {
        public S11001596()
        {
            this.EventID = 11001596;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "この先は$R;" +
            "「軍艦島地下」へ繋がっている。$R;" +
            "$R……リーダーからの通行許可を$R;" +
            "得ているようだな。$R;", "レジスタンス");
            if (Select(pc, "「軍艦島地下」へ進むか？", "", "「軍艦島地下」へ行く", "やめる") == 1)
            {
                Warp(pc, 22197000, 8, 104);
            }


        }
    }
}


        
   


