using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M12035000
{
    public class S11001593 : Event
    {
        public S11001593()
        {
            this.EventID = 11001593;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "在前方是$R;" +
            "連接著「軍艦島地下」。$R;" +
            "$R……似乎要從首領$R;" +
            "取得通行許可才可以進入。$R;", "レジスタンス");
            //
            /*
            Say(pc, 131, "この先は$R;" +
                "「軍艦島地下」へ繋がっている。$R;" +
                "$R……リーダーからの通行許可を$R;" +
                "得ているようだな。$R;", "レジスタンス");
            */
            if (Select(pc, "要進入「軍艦島地下」嗎？", "", "前往「軍艦島地下」", "不要前往") == 1)
            {
                Warp(pc, 22197000, 104, 6);
                NPCHide(pc, 11002156);
                NPCHide(pc, 11002116);
                NPCHide(pc, 11002115);
                NPCHide(pc, 11002114);
                NPCHide(pc, 11002113);
                NPCHide(pc, 11002112);
                NPCHide(pc, 11002106);
                NPCHide(pc, 11002105);
                NPCHide(pc, 11002104);
                NPCHide(pc, 11002103);
            }


        }
    }
}


        
   


