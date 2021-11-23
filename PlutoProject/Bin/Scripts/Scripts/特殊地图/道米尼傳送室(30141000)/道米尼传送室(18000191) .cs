using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;

namespace SagaScript.M30141000
{
    public class S18000191 : Event
    {
        public S18000191()
        {
            this.EventID = 18000191;
        }

        public override void OnEvent(ActorPC pc)
        {
        Say(pc, 131, "" + pc.Name + "よ……。$R;" +
"$R本当にいくのだな。$R;" + 
"$Pあの世界には、エミルの民はもちろん$R;" + 
"我らドミニオンの永遠の宿敵である$R;" + 
"タイタニアの民もいるのだぞ。$R;" + 
"$P……言っても詮無きことか。$R;" + 
"$P我らの世界を脅かす侵略者と$R;" + 
"争い、勝利する力を求めて$R;" + 
"三界を彷徨うのが我が種族の定め。$R;" + 
"$Rおまえにも、ついにそのときが$R;" + 
"来たということなのだろう……。$R;" + 
"$P……旅立つがいい。$R;" + 
"$Rおまえの戦いが実り多きものになるよう$R;" + 
"私はここで祈っている。$R;", "ドミニオン転送室主任");
Warp(pc, 10018104, 204, 64);
         }
     }
}
