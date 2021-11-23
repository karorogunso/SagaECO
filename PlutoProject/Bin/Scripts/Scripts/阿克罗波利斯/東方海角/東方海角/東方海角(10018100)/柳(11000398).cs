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
                        Say(pc, 131, "之前真的非常感谢$R;" +
                            "$R因为姐姐也不来所以没办法$R;" +
                            "想在这里做买卖看看$R;");
                    }
                    Say(pc, 131, "要不要看一下法伊斯特漂亮的衣服$R;");

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
                    Say(pc, 131, "没有可卖的东西$R;");
                    return;
                }

                if (mask.Test(DFHJFlags.已找到撒帕涅))
                {
                    mask.SetValue(DFHJFlags.寻找撒帕涅结束, true);
                    Say(pc, 131, "啊…见到了?$R;" +
                        "真的谢谢!$R;" +
                        "$P嗯?还在阿克罗波利斯里?$R;" +
                        "$R真是不能信任啊…$R;" +
                        "$P还好知道在哪里，有点安心了$R;" +
                        "$R真的谢谢$R;" +
                        "$P听说去法伊斯特的桥的封锁也解除了$R;" +
                        "要回去才可以…$R;");
                    return;
                }

                if (mask.Test(DFHJFlags.寻找撒帕涅开始))
                {
                    Say(pc, 131, "在哪裡看到撒帕涅姐姐的话$R;" +
                        "转告她我在东国等她$R;" +
                        "所以让她快点过来$R;");
                    return;
                }

                Say(pc, 131, "撒帕涅姐姐要来的话，要多久啊?$R;" +
                    "真是管不了姐姐啊$R;");
                switch (Select(pc, "怎么做呢？", "", "只是装作不知道", "什么事?"))
                {
                    case 1:
                        break;
                    case 2:
                        if (pc.Fame > 99 && pc.Level > 39)
                        {
                            Say(pc, 131, "那…不好意思，你是?$R;" +
                                "$P啊，是…$R;" +
                                "" + pc.Name + "不是吗?$R;" +
                                "是冒险者啊$R;" +
                                "$R好像不是普通人啊$R;" +
                                "$P我是法伊斯特的柳$R;" +
                                "$R跟姐姐来阿克罗波利斯来观光的$R;" +
                                "但是我特别讨厌人多…$R;" +
                                "$P所以定了下次见面的日子后$R;" +
                                "先回到这个东国的村子里了$R;" +
                                "$P但是怎么等姐姐都不来$R;" +
                                "数了一下天数，都过了一个星期$R;" +
                                "$R真…不知道再做什么…$R;" +
                                "$P万一在某个地方看到姐姐的话$R;" +
                                "可以转告她我在东国等她$R;" +
                                "让她快点过来吗?$R;");
                            switch (Select(pc, "怎么做呢？", "", "不要", "知道了"))
                            {
                                case 1:
                                    break;
                                case 2:
                                    mask.SetValue(DFHJFlags.寻找撒帕涅开始, true);
                                    Say(pc, 131, "那拜托了$R;");
                                    break;
                            }
                            return;
                        }
                        Say(pc, 131, "什…什么都不是$R;" +
                            "不要跟陌生人说话，是父母说的!$R;" +
                            "$R像你这样的冒险家的名字$R;" +
                            "没看到过也没听说过啊!$R;" +
                            "不能相信啊!$R;");
                        break;
                }
                return;
            }
            Say(pc, 131, "撒帕涅的姐姐要来的话，要多久啊?$R;" +
                "真是管不了姐姐啊$R;");
        }
    }
}