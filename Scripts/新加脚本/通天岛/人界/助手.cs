
using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
using SagaDB.Actor;
using SagaMap.Mob;
using SagaDB.Mob;
using SagaMap.ActorEventHandlers;
namespace Exploration
{
    public class S11003589 : Event
    {
        public S11003589()
        {
            this.EventID = 11003589;
        }
        public override void OnEvent(ActorPC pc)
        {
            //ChangeMessageBox(pc);
            if (pc.AStr["通天塔西瓜"] != DateTime.Now.ToString("yyyy-MM-dd"))
            {
                Say(pc, 0, "啊……口好渴啊，$R好想吃西瓜……，$R那个……请问你有$CR两片$CD西瓜吗？", "助手");
                if (CountItem(pc, 10004350) > 1)
                {
                    switch (Select(pc, "怎么说？", "", "有有有，给你。", "不给，我自己吃"))
                    {
                        case 1:
                            TakeItem(pc, 10004350, 2);
                            Wait(pc, 2000);
                            Say(pc, 0, "嚼嚼……啊，老师，这个是你的。", "助手");
                            Say(pc, 0, "哦，谢谢。", "考古学者");
                            Say(pc, 0, "嗯嗯……西瓜真好吃，可算缓过来了，", "考古学者");
                            Say(pc, 0, "真不好意思！忘记自我介绍了，我是助手莉可，这位是我的老师克里斯，他是考古界的专家~$R我目前在老师的手下学习考古技术。", "助手");
                            Say(pc, 0, "你好，我是克里斯，你真是一个热心肠的冒险者，谢谢你的西瓜。", "考古学者");
                            Say(pc, 0, "我们的目的是寻找传说中的$CR鬼神铁$CD，目前在考察这座塔周边的矿石。", "助手");
                            Say(pc, 0, "如果你拿到了「含金属的石头」可以将它拿到我这边进行鉴定哦。", "助手");
                            pc.AStr["通天塔西瓜"] = DateTime.Now.ToString("yyyy-MM-dd");
                            break;
                        case 2:
                            Say(pc, 0, "……", "助手");
                            break;
                    }
                }
                else if ((CountItem(pc, 10004350) < 2))
                {
                    Say(pc, 0, "好像西瓜并不够……", pc.Name);
                    return;
                }
            }
            if (pc.AStr["通天塔西瓜"] == DateTime.Now.ToString("yyyy-MM-dd"))
            {
                switch (Select(pc, "你好呀，要鉴定「含金属的石头」吗？", "", "鉴定(500g/次)", "退出"))
                {
                    case 1:
                        if ((CountItem(pc, 10014800) == 0))
                        {
                            Say(pc, 0, "你身上没带着「含金属的石头」哦？", "助手");
                            return;
                        }
                        ushort count = 0;
                        if (CountItem(pc, 10014800) == 1)
                            count = 1;
                        else if (CountItem(pc, 10014800) > 1)
                        {
                            Say(pc, 0, "我看你带来了好多，$R要全部都鉴定吗？", "助手");
                            if (Select(pc, "是否全部鉴定呢？", "", "全部鉴定", "只鉴定一个") == 1)
                                count = (ushort)CountItem(pc, 10014800);
                            else count = 1;
                        }
                        if(pc.Gold < count * 500)
                        {
                            Say(pc, 0, "你没带够钱哦？", "助手");
                            return;
                        }
                        PlaySound(pc, 2040, false, 100, 50);
                        pc.Gold -= count * 500;
                        TakeItem(pc, 10014800, count);
                        for (int i = 0; i < count; i++)
                        {
                            int ran = SagaLib.Global.Random.Next(1, 10000);
                            if (ran <= 10)
                            {
                                GiveItem(pc, 10015200, 1);//玉钢
                                continue;
                            }
                            else if (ran > 10 && ran <= 100)
                            {
                                GiveItem(pc, 10015706, 1);//鬼神鉄
                                continue;
                            }
                            else if (ran > 100 && ran <= 500)
                            {
                                GiveItem(pc, 10015300, 1);//金块
                                continue;
                            }
                            else if (ran > 500 && ran <= 1500)
                            {
                                GiveItem(pc, 10015701, 1);//红色矿石
                                continue;
                            }
                            else if (ran > 1500 && ran <= 3000)
                            {
                                GiveItem(pc, 10015000, 1);//砂金
                                continue;
                            }
                            else if (ran > 3000 && ran <= 5000)
                            {
                                GiveItem(pc, 10015600, 1);//铁块
                                continue;
                            }
                            else if (ran > 5000)
                            {
                                GiveItem(pc, 10014600, 1);//石块
                                continue;
                            }
                        }
                        break;
                    case 2:
                        break;
                }
            }
        }
    }
}
