using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;

using SagaScript.Chinese.Enums;
namespace SagaScript.M10042000
{
    public class S11000344 : Event
    {
        public S11000344()
        {
            this.EventID = 11000344;
            this.questTransportSource = "這個可以轉交一下嗎?$R;";
            this.questTransportDest = "啊~這個好不好意思啊$R;" +
                                      "辛苦了$R;" +
                                      "真的謝謝$R;";
            this.transport = "那拜託你轉交了$R;";
            this.questTransportCompleteSrc = "$R報酬$R;" +
                                             "去任務服務台領取吧！$R;";
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<LLHPFlags> mask = new BitMask<LLHPFlags>(pc.CMask["LLHP"]);
            if (pc.Job == PC_JOB.TATARABE ||
                pc.Job == PC_JOB.BLACKSMITH ||
                pc.Job == PC_JOB.MACHINERY ||
                pc.Job == PC_JOB.FARMASIST ||
                pc.Job == PC_JOB.ALCHEMIST ||
                pc.Job == PC_JOB.MARIONEST ||
                pc.Job == PC_JOB.RANGER ||
                pc.Job == PC_JOB.EXPLORER ||
                pc.Job == PC_JOB.TREASUREHUNTER ||
                pc.Job == PC_JOB.MERCHANT ||
                pc.Job == PC_JOB.TRADER ||
                pc.Job == PC_JOB.GAMBLER)
            {
                if (pc.Level < 15)
                {
                    Say(pc, 131, "擁有自己的『帳篷』的是$R;" +
                        "已經成爲堂堂背後包裝者的證據$R;" +
                        "$R你也成爲像我這樣$R;" +
                        "就好了!$R;");
                }
                if (!mask.Test(LLHPFlags.得到豪華席))
                {
                    Say(pc, 131, "現在你也成爲了堂堂的背後包裝者$R;" +
                        "$R這是我送的小小的$R;" +
                        "禮物$R;");
                    if (CheckInventory(pc, 10021302, 1))
                    {
                        mask.SetValue(LLHPFlags.得到豪華席, true);
                        GiveItem(pc, 10021302, 1);
                        Say(pc, 131, "這是爲了製造帳篷的$R;" +
                            "合成材料『豪華席』$R;" +
                            "$R把這個放在最下面以後用『防水布』$R;" +
                            "『結實的繩索』『鐵棒』$R;" +
                            "製作房頂的話帳篷就完成了$R;" +
                            "$P雖然是有點難的操作$R;" +
                            "但是自身的帳篷完成後的感動是$R;" +
                            "真的是最刺激的!$R;" +
                            "$R那努力的做一下帳篷吧!$R;");
                        PlaySound(pc, 2040, false, 100, 50);
                        Say(pc, 131, "『豪華席』開始入手了!$R;");
                        return;
                    }
                    Say(pc, 131, "東西太多了$R;" +
                        "把道具扔掉或減少以後$R;" +
                        "再來吧$R;");
                }
                Say(pc, 131, "那後面浮着的$R能看到嗎?$R那個就是被稱爲『非工程』的$R在天上飛的『庭院』$R;" +
                    "$P我們商人是$R雖然主要使用在搬運東西上$R;" +
                    "$R但是也可以栽培農作物或$R裝上大帆後$R飛越多個都市$R;" +
                    "$P你也趕緊$R擁有一個就好了$R;");
            }
            switch (Select(pc, "歡迎光臨!", "", "買東西", "賣東西", "什麽都不做"))
            {
                case 1:
                    OpenShopBuy(pc, 70);
                    break;
                case 2:
                    OpenShopSell(pc, 70);
                    break;
                case 3:
                    break;
            }
 
        }
    }
}