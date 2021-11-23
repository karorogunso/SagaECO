using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M21193000
{
    public class S11001796 : Event
    {
        public S11001796()
        {
            this.EventID = 11001796;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 0, "如果用花拿來跟糰子相比的話、 $R;" +
            "只有糰子能也吃到飽呢。$R;" +
            "$P差不多想吃別的糰子吶。$R;", "喜歡糰子的男孩");

            //
            /*
            Say(pc, 0, "花より団子とは言うけどさぁ、$R;" +
            "団子ばっかり食べてるのも飽きたね。$R;" +
            "$Pそろそろ違う団子が食べたいなぁ。$R;", "団子好きの男");
            */
        }
    }
}
 