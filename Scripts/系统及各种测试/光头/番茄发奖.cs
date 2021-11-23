
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
    public class S2017: Event
    {
        public S2017()
        {
            this.EventID = 2017;
        }

        public override void OnEvent(ActorPC pc)
        {
            switch (Select(pc, "番茄给你们发奖啦！", "", "活动一：放烟花，迎新年！ ", "活动二：谁会笑到最后！", "活动三：追杀GM...", "活动四：故事接龙", "0点准时放礼", "卧槽我点错了！"))
            {
                case 1:
                    foreach (var item in SagaMap.Manager.MapClientManager.Instance.OnlinePlayer)
                    {
                        if (item.Character.MapID == 10071004 && item.Character.AInt["已领取活动一奖励"] == 0)
                        {
                            //ChangeMessageBox(item.Character);
                            GiveItem(item.Character, 60082550, 1);//烟花棒
                            item.Character.AInt["已领取活动一奖励"] = 1;
                            Wait(item.Character, 1000);
                            Say(item.Character, 131, "番茄茄在此祝你新年快乐！！$R获得「手持烟花棒」1个。");
                        }
                    }
                    break;
                case 2:
                    foreach (var item in SagaMap.Manager.MapClientManager.Instance.OnlinePlayer)
                    {
                        if (item.Character.MapID == 10071004 && item.Character.AInt["已领取活动二奖励"] == 0)
                        {
                            //ChangeMessageBox(item.Character);
                            GiveItem(item.Character, 910000104, 1);//不可交易的任务点300
                            GiveItem(item.Character, 910000115, 10);//红包
                            GiveItem(item.Character, 950000030, 500);//年糕
                            item.Character.AInt["已领取活动二奖励"] = 1;
                            Wait(item.Character, 1000);
                            Say(item.Character, 131, "「谁能活到最后！奖励发放！」$R$R获得「任务点增加勋章[300点]」1个，「红包」10个，「年糕」500个。");
                        }
                    }
                    break;
                case 3:
                    foreach (var item in SagaMap.Manager.MapClientManager.Instance.OnlinePlayer)
                    {
                        if (item.Character.MapID == 10071004 && item.Character.AInt["已领取活动三奖励"] == 0)
                        {
                            //ChangeMessageBox(item.Character);
                            GiveItem(item.Character, 910000104, 1);//不可交易的任务点300
                            GiveItem(item.Character, 910000105, 10);//迎春炸裂盒子
                            GiveItem(item.Character, 950000030, 500);//年糕
                            item.Character.AInt["已领取活动三奖励"] = 1;
                            Wait(item.Character, 1000);
                            Say(item.Character, 131, "追杀GM活动奖励！！$R获得「任务点增加勋章[300点]」1个，「迎春炸裂盒子」10个，「年糕」500个");
                        }
                    }
                    break;
                case 4:
                    foreach (var item in SagaMap.Manager.MapClientManager.Instance.OnlinePlayer)
                    {
                        if (item.Character.MapID == 10071004 && item.Character.AInt["已领取活动四奖励"] == 0)
                        {
                            //ChangeMessageBox(item.Character);
                            GiveItem(item.Character, 910000104, 1);//不可交易的任务点300
                            GiveItem(item.Character, 910000115, 10);//红包
                            item.Character.AInt["已领取活动四奖励"] = 1;
                            Wait(item.Character, 1000);
                            Say(item.Character, 131, "番茄茄在此祝你新年快乐！！$R获得「任务点增加勋章[300点]」1个，「红包」10个");
                        }
                    }
                    break;
                case 5:
                    foreach (var item in SagaMap.Manager.MapClientManager.Instance.OnlinePlayer)
                    {
                        if (item.Character.MapID == 10071004 && item.Character.AInt["已领取零点在线奖励"] == 0)
                        {
                            //ChangeMessageBox(item.Character);
                            GiveItem(item.Character, 950000030, 1000);//年糕
                            GiveItem(item.Character, 910000105, 18);//迎春炸裂盒子
                            GiveItem(item.Character, 910000115, 8);//红包
                            item.Character.AInt["已领取零点在线奖励"] = 1;
                            Wait(item.Character, 1000);
                            Say(item.Character, 131, "番茄茄在此祝你新年快乐！！$R「红包」8个，「迎春炸裂盒子」18个，「年糕」1000个 ");
                        }
                    }
                    break;
                case 6:
                    break;
            }
        }
    }
}