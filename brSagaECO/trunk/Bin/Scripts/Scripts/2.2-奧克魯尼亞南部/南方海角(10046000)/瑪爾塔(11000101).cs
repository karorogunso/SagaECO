using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;

using SagaScript.Chinese.Enums;
namespace SagaScript.M10046000
{
    public class S11000101 : Event
    {
        public S11000101()
        {
            this.EventID = 11000101;
            this.questTransportSource = "在等你了!$R;" +
                                        "來，這個就拜託了!$R;";
            this.transport = "傳送地點在『任務窗』確認吧$R;";
            this.questTransportCompleteSrc = "這麽快就將物品轉交給對方了?$R;" +
                                             "真的謝謝$R;" +
                                             "$R請去任務服務台領取報酬吧！$R;";
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<HZFlags> mask = new BitMask<HZFlags>(pc.CMask["HZ"]);
            if (!mask.Test(HZFlags.瑪爾塔第一次對話))
            {
                Say(pc, 131, "有去過阿伊恩薩烏斯嗎?$R;" +
                    "$R那是這個阿姨的故鄉$R是一個非常有趣的村子$R;" +
                    "$P無論你多麼地忙$R都要抽空去看一次比較好$R;");
                mask.SetValue(HZFlags.瑪爾塔第一次對話, true);
                return;
            }
            Say(pc, 131, "不熱嗎?$R;" +
                "當然熱了!!熱的快要死了~!!$R;");
        }
    }
}

