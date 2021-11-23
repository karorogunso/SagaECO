using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
namespace SagaScript.M21193000
{
    public class S11001786 : Event
    {
        public S11001786()
        {
            this.EventID = 11001786;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<ECOchen> ECOchen_mask = new BitMask<ECOchen>(pc.CMask["ECOchen"]);
            //int selection;
            if (ECOchen_mask.Test(ECOchen.打到放水龙后))
            {
                HandleQuest(pc, 68);
                return;
            }
            Say(pc, 0, "♪振り返ってみても ah...$R;" +
            "♪貴方の羽は もう見えない$R;" +
            "$R♪アクロポリスの丘での誓い$R;" +
            "♪忘れてしまったの？$R;" +
            "$P♪Don't you cry！ いつまでもプルル$R;" +
            "$R♪貴方の感触 とても似ている$R;" +
            "$P♪Don't you cry！ そこまではコッコー$R;" +
            "$R♪貴方の温もり とても似ている$R;" +
            "$P♪消えないものは 頭の輪っか$R;" +
            "♪今もそのまま$R;" +
            "$Pゴホゴホッ！$R;" +
            "$P何かのどの調子が悪いみたいだわ。$R;" +
            "$P困ったなぁ、$R;" +
            "このままじゃ私の美声を$R;" +
            "皆にお伝えできないわ！$R;", "ルイス");
        }
    }
}