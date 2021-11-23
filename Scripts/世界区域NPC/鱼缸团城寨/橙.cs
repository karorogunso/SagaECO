
using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
using WeeklyExploration;
using System.Globalization;
namespace SagaScript.M30210000
{
    public class S60000024 : Event
    {
        public S60000024()
        {
            this.EventID = 60000024;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 190, "如果你有【限定KUJI代币】$R$R我可以帮你换目前$R限定的KUJI哦！", "橙");
            string SE = "";
            ushort numS = 0;

            switch (Select(pc, "请选择要兑换的限定KUJI。", "", "SS·[1币]收押入狱[7月2日下架]", "SS·[1币][复刻]ECO×MaryMagdalene[7月2日下架]", "S·[1币]贾巴沃克的仙境奇遇[7月2日下架]",
                "SSS·[2币][复刻]魅惑的莉莉姆闺房[7月16日下架]", "SS·[1币]维拉德In哥特之夜[7月16日下架]", "S·[1币]珍贵的婚礼[7月16日下架]", "离开"))
            {
                case 1:
                    SE = InputBox(pc, "输入兑换收押入狱礼盒数量", InputType.Bank);
                    if (SE == "")
                        return;
                    try
                    {
                        numS = ushort.Parse(SE);
                    }
                    catch
                    {
                        Say(pc, 0, "嗯？我怀疑你根本拿不下这么多。", "橙");
                        return;
                    }
                    if (numS == 0) return;
                    if (CountItem(pc, 950000025) >= numS)
                    {
                        TakeItem(pc, 950000025, (ushort)(numS));
                        GiveItem(pc, 120000141, numS);
                    }
                    else
                    {
                        Say(pc, 190, "你似乎没有足够的币哦", "橙");
                        return;
                    }
                    break;
                case 2:
                    SE = InputBox(pc, "输入兑换ECO×MaryMagdalene礼盒数量", InputType.Bank);
                    if (SE == "")
                        return;
                    try
                    {
                        numS = ushort.Parse(SE);
                    }
                    catch
                    {
                        Say(pc, 0, "嗯？我怀疑你根本拿不下这么多。", "橙");
                        return;
                    }
                    if (numS == 0) return;
                    if (CountItem(pc, 950000025) >= numS)
                    {
                        TakeItem(pc, 950000025, numS);
                        GiveItem(pc, 120000153, numS);
                    }
                    else
                    {
                        Say(pc, 190, "你似乎没有足够的币哦", "橙");
                        return;
                    }
                    break;
                case 3:
                    SE = InputBox(pc, "输入兑换贾巴沃克的仙境奇遇礼盒数量", InputType.Bank);
                    if (SE == "")
                        return;
                    try
                    {
                        numS = ushort.Parse(SE);
                    }
                    catch
                    {
                        Say(pc, 0, "嗯？我怀疑你根本拿不下这么多。", "橙");
                        return;
                    }
                    if (numS == 0) return;
                    if (CountItem(pc, 950000025) >= numS)
                    {
                        TakeItem(pc, 950000025, numS);
                        GiveItem(pc, 120000216, numS);
                    }
                    else
                    {
                        Say(pc, 190, "你似乎没有足够的币哦", "橙");
                        return;
                    }
                    break;
                case 4:
                    SE = InputBox(pc, "输入兑换收魅惑的莉莉姆闺房数量", InputType.Bank);
                    if (SE == "")
                        return;
                    try
                    {
                        numS = ushort.Parse(SE);
                    }
                    catch
                    {
                        Say(pc, 0, "嗯？我怀疑你根本拿不下这么多。", "橙");
                        return;
                    }
                    if (numS == 0) return;
                    if (CountItem(pc, 950000025) >= numS *2)
                    {
                        TakeItem(pc, 950000025, (ushort)(numS * 2));
                        GiveItem(pc, 120000202, numS);
                    }
                    else
                    {
                        Say(pc, 190, "你似乎没有足够的币哦", "橙");
                        return;
                    }
                    break;
                case 5:
                    SE = InputBox(pc, "输入兑换维拉德In哥特之夜礼盒数量", InputType.Bank);
                    if (SE == "")
                        return;
                    try
                    {
                        numS = ushort.Parse(SE);
                    }
                    catch
                    {
                        Say(pc, 0, "嗯？我怀疑你根本拿不下这么多。", "橙");
                        return;
                    }
                    if (numS == 0) return;
                    if (CountItem(pc, 950000025) >= numS)
                    {
                        TakeItem(pc, 950000025, numS);
                        GiveItem(pc, 120000178, numS);
                    }
                    else
                    {
                        Say(pc, 190, "你似乎没有足够的币哦", "橙");
                        return;
                    }
                    break;
                case 6:
                    SE = InputBox(pc, "输入兑换珍贵的婚礼礼盒数量", InputType.Bank);
                    if (SE == "")
                        return;
                    try
                    {
                        numS = ushort.Parse(SE);
                    }
                    catch
                    {
                        Say(pc, 0, "嗯？我怀疑你根本拿不下这么多。", "橙");
                        return;
                    }
                    if (numS == 0) return;
                    if (CountItem(pc, 950000025) >= numS)
                    {
                        TakeItem(pc, 950000025, numS);
                        GiveItem(pc, 120000064, numS);
                    }
                    else
                    {
                        Say(pc, 190, "你似乎没有足够的币哦", "橙");
                        return;
                    }
                    break;
            }
        }
    }
}