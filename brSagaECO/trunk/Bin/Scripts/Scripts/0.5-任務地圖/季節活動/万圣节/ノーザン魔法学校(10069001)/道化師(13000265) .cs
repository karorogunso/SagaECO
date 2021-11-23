using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10069001
{
    public class S13000265 : Event
    {
        public S13000265()
        {
            this.EventID = 13000265;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<wsj> wsj_mask = new BitMask<wsj>(pc.CMask["wsj"]);
            //int selection;
            if (wsj_mask.Test(wsj.项链))
            {
                Say(pc, 131, "儀式は終わりました。$R;" +
                "現在清掃中ですので$R;" +
                "中に入ることは出来ません。$R;", "道化師");
            }
            Say(pc, 131, "お待ちしておりました。$R;" +
            "" + pc.Name + "様！$R;" +
            "まもなく儀式が始まります。$R;" +
            "$Rさあさあ、このまま$R;" +
            "奥の広間へとお進みください。$R;" +
            "$Pおっと、その前に$R;" +
            "一つだけ、ご注意願いたいことが……。$R;" +
            "$Pいいですか、奥では決して$R;" +
            "騒いでは行けません。$R;" +
            "$R死者たちは、激しい感情を嫌います。$R;" +
            "心を静め……$R;" +
            "何が起こっても冷静に……。$R;" +
            "$Pこれを守れなかった場合$R;" +
            "身の安全は保障いたしかねます。$R;" +
            "よろしいですね？$R;" +
            "$Rヒヒヒッ……。$R;" +
            "$Pでは、奥へどうぞ……。$R;", "道化師");
        }
    }
}