using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10002000
{
    public class S11000090 : Event
    {
        public S11000090()
        {
            this.EventID = 11000090;
            this.questTransportSource = "小鬼，你要幫我轉交嗎?$R;" +
                                        "怎麽感覺到有些不安啊…$R;" +
                                        "$R沒辦法…只好將就一下了!拜託你啦$R;";
            this.transport = "小鬼，那就拜託你啦$R;";
            this.questTransportDest = "您帶了東西給我?$R;" +
                                      "年紀輕輕，不過小姐可真是厲害$R謝謝您了喔$R;";
            this.questTransportCompleteSrc = "已經把東西交給對方了?$R;" +
                                             "小鬼動作挺快的嘛，謝謝啦$R;" +
                                             "$R請去任務服務台領取報酬吧！$R;";
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<RLSXYFlags> mask = new BitMask<RLSXYFlags>(pc.CMask["RLSXY"]);
            if (!mask.Test(RLSXYFlags.瑪秀夏第一次對話))
            {
                Say(pc, 131, "這裡就是『冰洞』裝備都準備好了?$R;" +
                    "如果小看這裡的話，很容易發生意外$R;");
                mask.SetValue(RLSXYFlags.瑪秀夏第一次對話, true);
                return;
            }
            Say(pc, 131, "不久前因爲從地牢裡傳出了慘叫聲$R;" +
                "我趕緊進去看了一下，結果……$R;" +
                "$P故事暫時就到這裡停止!$R想要繼續往下聽的話，請付錢！$R;" +
                "想怎麽做呢?$R;");
            mask.SetValue(RLSXYFlags.瑪秀夏第一次對話, false);
            switch (Select(pc, "要支付5000金幣嗎?", "", "支付", "不支付"))
            {
                case 1:

                    if (pc.Gold > 4999)
                    {
                        Say(pc, 131, "是啊，好好考慮吧$R;" +
                            "來，先把錢收下吧$R;");
                        pc.Gold -= 5000;
                        PlaySound(pc, 2030, false, 100, 50);
                        Say(pc, 131, "給了他5000金幣$R;");
                        Say(pc, 131, "嘿嘿嘿…貪財貪財，那我就繼續說吧$R;" +
                            "當時我就趕緊到裡面看看情況…$R;" +
                            "$P那小子就在那裡!$R;" +
                            "$R外貌看起來雖然可愛，可是超強啊!$R;" +
                            "真是太可愛了，都到了討厭的程度$R;" +
                            "$P他的名字?我不知道阿…$R;" +
                            "要不您親自去確認吧?$R;" +
                            "我在地下2樓見過他$R;");
                        return;
                    }
                    PlaySound(pc, 2041, false, 100, 50);
                    Say(pc, 131, "錢不夠啊…真是可惜啊$R;" +
                        "這樣的話，那我就不能講給您聽囉$R;");
                    break;
                case 2:
                    break;
            }
        }
    }
}
