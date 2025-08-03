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
            this.questTransportSource = "这个可以转交一下吗?$R;";
            this.questTransportDest = "啊~这个好不好意思啊$R;" +
                                      "辛苦了$R;" +
                                      "真的谢谢$R;";
            this.transport = "那拜托你转交了$R;";
            this.questTransportCompleteSrc = "$R报酬$R;" +
                                             "去任务服务台领取吧！$R;";
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
                    Say(pc, 131, "拥有自己的『帐篷』的是$R;" +
                        "已经成为堂堂冒险家的证据$R;" +
                        "$R你也成为像我这样$R;" +
                        "就好了!$R;");
                }
                if (!mask.Test(LLHPFlags.得到豪華席))
                {
                    Say(pc, 131, "现在你也成为了堂堂的冒险家$R;" +
                        "$R这是我送的小小的$R;" +
                        "礼物$R;");
                    if (CheckInventory(pc, 10021302, 1))
                    {
                        mask.SetValue(LLHPFlags.得到豪華席, true);
                        GiveItem(pc, 10021302, 1);
                        Say(pc, 131, "这是为了制造帐篷的$R;" +
                            "合成材料『防潮席』$R;" +
                            "$R把这个放在最下面以后用『防水布』$R;" +
                            "『结实的绳索』『铁棒』$R;" +
                            "制作房顶的话帐篷就完成了$R;" +
                            "$P虽然是有点难的操作$R;" +
                            "但是自身的帐篷完成后的感动是$R;" +
                            "真的是最刺激的!$R;" +
                            "$R那努力的做一下帐篷吧!$R;");
                        PlaySound(pc, 2040, false, 100, 50);
                        Say(pc, 131, "『防潮席』开始入手了!$R;");
                        return;
                    }
                    Say(pc, 131, "东西太多了$R;" +
                        "把道具扔掉或减少以后$R;" +
                        "再来吧$R;");
                }
                Say(pc, 131, "那后面浮着的$R能看到吗?$R那个就是被称为『飞空庭』的$R在天上飞的『庭院』$R;" +
                    "$P我们商人是$R虽然主要使用在搬运东西上$R;" +
                    "$R但是也可以栽培农作物或$R装上大帆后$R飞越多个都市$R;" +
                    "$P你也赶紧$R拥有一个就好了$R;");
            }
            switch (Select(pc, "欢迎光临!", "", "买东西", "卖东西", "什么都不做"))
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