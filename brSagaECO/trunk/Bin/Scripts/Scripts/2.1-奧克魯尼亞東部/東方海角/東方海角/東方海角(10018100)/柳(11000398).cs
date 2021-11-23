using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;

using SagaScript.Chinese.Enums;
namespace SagaScript.M10018100
{
    public class S11000398 : Event
    {
        public S11000398()
        {
            this.EventID = 11000398;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<DFHJFlags> mask = new BitMask<DFHJFlags>(pc.CMask["DFHJ"]);
            //EVT11000398
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
                if (mask.Test(DFHJFlags.寻找撒帕涅结束))
                {
                    if (!mask.Test(DFHJFlags.开启商店))
                    {
                        mask.SetValue(DFHJFlags.开启商店, true);
                        Say(pc, 131, "之前真的非常感謝$R;" +
                            "$R因爲姐姐也不來所以沒辦法$R;" +
                            "想在這裡做買賣看看$R;");
                    }
                    Say(pc, 131, "要不要看一下帕斯特漂亮的衣服$R;");

                    if (pc.Job == PC_JOB.TATARABE ||
                    pc.Job == PC_JOB.BLACKSMITH ||
                    pc.Job == PC_JOB.MACHINERY)
                    {
                        OpenShopBuy(pc, 124);
                        return;
                    }

                    if (pc.Job == PC_JOB.FARMASIST ||
                    pc.Job == PC_JOB.ALCHEMIST ||
                    pc.Job == PC_JOB.MARIONEST)
                    {
                        OpenShopBuy(pc, 125);
                        return;
                    }

                    if (pc.Job == PC_JOB.RANGER ||
                    pc.Job == PC_JOB.EXPLORER ||
                    pc.Job == PC_JOB.TREASUREHUNTER)
                    {
                        OpenShopBuy(pc, 126);
                        return;
                    }

                    if (pc.Job == PC_JOB.MERCHANT ||
                    pc.Job == PC_JOB.TRADER ||
                    pc.Job == PC_JOB.GAMBLER)
                    {
                        OpenShopBuy(pc, 127);
                        return;
                    }
                    Say(pc, 131, "沒有可賣得東西$R;");
                    return;
                }

                if (mask.Test(DFHJFlags.已找到撒帕涅))
                {
                    mask.SetValue(DFHJFlags.寻找撒帕涅结束, true);
                    Say(pc, 131, "啊…見到了?$R;" +
                        "真的謝謝!$R;" +
                        "$P嗯?還在阿高普路斯裡?$R;" +
                        "$R真是不能信任啊…$R;" +
                        "$P還好知道在哪裡，有點安心了$R;" +
                        "$R真的謝謝$R;" +
                        "$P聽説去帕斯特的橋的封鎖也開了$R;" +
                        "要回去才可以…$R;");
                    return;
                }

                if (mask.Test(DFHJFlags.寻找撒帕涅开始))
                {
                    Say(pc, 131, "在哪裡看到撒帕涅姐姐的話$R;" +
                        "轉告她我在東域等她$R;" +
                        "所以讓她快點過來$R;");
                    return;
                }

                Say(pc, 131, "撒帕涅姐姐要來的話，要多久啊?$R;" +
                    "真是管不了姐姐啊$R;");
                switch (Select(pc, "怎麼做呢？", "", "只是裝作不知道", "什麽事?"))
                {
                    case 1:
                        break;
                    case 2:
                        if (pc.Fame > 99 && pc.Level > 39)
                        {
                            Say(pc, 131, "那…不好意思，你是?$R;" +
                                "$P阿是…$R;" +
                                "" + pc.Name + "不是嗎?$R;" +
                                "是冒險者啊$R;" +
                                "$R好像不是普通人啊$R;" +
                                "$P我是帕斯特島的柳$R;" +
                                "$R跟姐姐來阿高普路斯來觀光的$R;" +
                                "但是我特別討厭人多…$R;" +
                                "$P所以定了下次見面的日子後$R;" +
                                "先回到這個東域的村子裡了$R;" +
                                "$P但是怎麽等姐姐都不來$R;" +
                                "數了一下天數，都過了一個星期$R;" +
                                "$R真…不知道再做什麽…$R;" +
                                "$P萬一在某個地方看到姐姐的話$R;" +
                                "可以轉告她我在東域等她$R;" +
                                "讓她快點過來嗎?$R;");
                            switch (Select(pc, "怎麼做呢？", "", "不要", "知道了"))
                            {
                                case 1:
                                    break;
                                case 2:
                                    mask.SetValue(DFHJFlags.寻找撒帕涅开始, true);
                                    Say(pc, 131, "那拜託了$R;");
                                    break;
                            }
                            return;
                        }
                        Say(pc, 131, "什…什麽都不是$R;" +
                            "不要跟陌生人說話，是爸媽說的!$R;" +
                            "$R像你這樣的冒險家的名字$R;" +
                            "也沒看到過也沒聽説過啊!$R;" +
                            "不能相信啊!$R;");
                        break;
                }
                return;
            }
            Say(pc, 131, "撒帕涅的姐姐要來的話，要多久啊?$R;" +
                "真是管不了姐姐啊$R;");
        }
    }
}