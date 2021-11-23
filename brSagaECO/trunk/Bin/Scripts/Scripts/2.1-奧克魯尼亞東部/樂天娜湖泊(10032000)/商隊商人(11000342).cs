using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10032000
{
    public class S11000342 : Event
    {
        public S11000342()
        {
            this.EventID = 11000342;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<LLHPFlags> mask = new BitMask<LLHPFlags>(pc.CMask["LLHP"]);
            if (mask.Test(LLHPFlags.商隊商人對話))
            {
                mask.SetValue(LLHPFlags.商隊商人對話, false);
                Say(pc, 131, "那後面浮着的$R能看到嗎?$R那個就是被稱爲飛空庭$R;" +
                    "$P我們商人是$R雖然主要使用在搬運東西上$R;" +
                    "$R但是也可以栽培農作物或$R裝上大帆後$R飛越多個都市$R;" +
                    "$P你也趕緊$R擁有一個就好了$R;");
                return;
            }
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
                    mask.SetValue(LLHPFlags.商隊商人對話, true);
                    Say(pc, 131, "擁有自己的『帳篷』的是$R;" +
                        "已經成爲堂堂背後包裝者的證據$R;" +
                        "$R你也成爲像我這樣$R;" +
                        "就好了!$R;");
                    return;
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
                        "$R現在無法接受$R;" +
                        "以後再給你吧$R;");
                    return;
                }
                mask.SetValue(LLHPFlags.商隊商人對話, true);
                Say(pc, 131, "『野營』的時候$R;" +
                    "需要『帳篷』!$R;" +
                    "$R已經知道了嗎?$R;");
                return;
            }
            mask.SetValue(LLHPFlags.商隊商人對話, true);
            Say(pc, 131, "呼，這附近的妖怪$R;" +
                "很粗暴阿$R;");
        }
    }
}
