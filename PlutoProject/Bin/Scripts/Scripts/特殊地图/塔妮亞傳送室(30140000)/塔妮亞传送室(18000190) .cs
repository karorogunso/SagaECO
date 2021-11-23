using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;

namespace SagaScript.M30140000
{
    public class S18000190 : Event
    {
        public S18000190()
        {
            this.EventID = 18000190;
        }

        public override void OnEvent(ActorPC pc)
        {
        Say(pc, 131, "" + pc.Name + "よ……。$R;" +
"$Rおまえはこれから$R;" + 
"エミルの世界におもむくことになる。$R;" + 
"$P我らタイタニアの民は$R;" + 
"生まれてから壱千弐百番目の月の日に$R;" + 
"異界の地に墜とされるしきたり。$R;" + 
"$Pそれはおまえも一族の長から$R;" + 
"教えられていたことだろう。$R;" + 
"$Pひとつおまえに言っておかなければ$R;" + 
"ならないことがある。$R;" + 
"$Pこれからのおまえの試練$R;" + 
"終わらせる鍵はお前の頭の中にある。$R;" + 
"$R……いまはそれを思い出すことは$R;" + 
"できないだろうが。$R;" + 
"$Pエミルの地で旅を続けるうち$R;" + 
"記憶の封印が解かれ$R;" + 
"おまえはそれを思い出すことになる。$R;" + 
"$P自分の力でさがし出すのだ。$R;" + 
"おまえの記憶の封印を解く鍵を。$R;" + 
"$P旅立つがいい、若者よ。$R;" + 
"おまえの前途に幸多からんことを。$R;", "タイタニア転送室主任");
Warp(pc, 10018104, 203, 64);
         }
     }
}
