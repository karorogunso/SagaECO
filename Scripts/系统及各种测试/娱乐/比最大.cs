
using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaMap;
using SagaDB.Actor;
using SagaDB.Item;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
using WeeklyExploration;
using System.Globalization;
using SagaMap.Manager;
using System.Linq;
using SagaMap.Network.Client;
using SagaMap.ActorEventHandlers;

namespace SagaScript.M30210000
{
    public class S60050002 : Event
    {
        public S60050002()
        {
            this.EventID = 60050002;
        }

        public override void OnEvent(ActorPC pc)
        {
            Map map = MapManager.Instance.GetMap(pc.MapID);
            ActorGolem Golem = (ActorGolem)map.GetActor((uint)pc.TInt["触发的GOLEMID"]);

            Say(pc, 0, "那个~小姐姐~要来玩猜谁最大吗？$R目前已经有$CR" + SDict["猜最大玩家内容"].Count.ToString() + "$CD人下注了哦$R"
                + "要不要来试试运气呢？", "啊");
            string ssa = "";
            if (pc.Account.GMLevel > 200)
                ssa = "(GM控制台)";
            switch (Select(pc, "要玩谁的点数最大吗？", "", "我要下注！(10000G/次)", "排行榜"+ssa, "离开"))
            {
                case 1:
                    if (SInt["猜最大进程记录"] != 1)
                        SDict["猜最大玩家内容"] = new VariableHolderA<string, int>();
                    if (SDict["猜最大玩家内容"].ContainsKey(pc.Name))
                    {
                        Say(pc, 0, "你已经下注过啦，$R请等待开奖哦", "啊");
                        return;
                    }
                    if (pc.Gold < 10000)
                    {
                        Say(pc, 0, "你的钱似乎不够呢？", "啊");
                        return;
                    }
                    if (SInt["猜最大进程记录"] != 1)//开奖计时
                    {
                        SInt["猜最大轮数"]++;
                        SagaMap.Skill.SkillHandler.Instance.ActorSpeak(Golem, "有玩家开始下注了哦！第" + SInt["猜最大轮数"].ToString() + "轮猜最大将在30秒后揭晓，最大数的玩家可以拿走全部的奖金！大家赶紧来下注呀！");
                        Timer timer = new Timer("猜最大计时", 0, 30000);
                        timer.AttachedPC = pc;
                        SInt["猜最大奖池"] += 10000;
                        timer.OnTimerCall += (x, e) =>
                        {
                            try
                            {
                                int result = Global.Random.Next(3, 27);
                                if (SInt["指定点数"] != 0)
                                    result = SInt["指定点数"];
                                SInt["指定点数"] = 0;
                                List<ActorPC> pcs = new List<ActorPC>();//中奖者
                                int max = result;
                                foreach (var item in SDict["猜最大玩家内容"])
                                    if (item.Value > max)
                                        max = item.Value;
                                foreach (var item in SDict["猜最大玩家内容"])
                                {
                                    if (item.Value == max)
                                    {
                                        string name = item.Key;
                                        var chr =
                                        from c in MapClientManager.Instance.OnlinePlayer
                                        where c.Character.Name == name
                                        select c;
                                        MapClient tClient = chr.First();
                                        pcs.Add(tClient.Character);
                                    }
                                }
                                SagaMap.Skill.SkillHandler.Instance.ActorSpeak(Golem, "揭晓的时间到了！最后我摇出的点数是：" + result.ToString());
                                SDict["猜最大玩家内容"] = new VariableHolderA<string, int>();
                                if (pcs.Count == 0)
                                    SagaMap.Skill.SkillHandler.Instance.ActorSpeak(Golem, "本轮最大的点数是" + max + "很遗憾，这次没有人比我的点数大，下注额全部归我了。", 1000);
                                else
                                {
                                    int totalcount = pcs.Count;
                                    if (max == result)
                                        totalcount += 1;
                                    string names = "";
                                    int bouns = SInt["猜最大奖池"];
                                    int singleb = SInt["猜最大奖池"] / totalcount;
                                    for (int i = 0; i < pcs.Count; i++)
                                    {
                                        SDict["比最大_排行榜"][pc.Name]++;
                                        names += " " + pcs[i].Name;
                                        pcs[i].Gold += singleb;
                                    }
                                    if (max == result)
                                        names = "我！和" + names;
                                    SagaMap.Skill.SkillHandler.Instance.ActorSpeak(Golem, "本轮最大的点数是" + max + "。哇！本轮共有" + totalcount.ToString() + "位中奖者！恭喜：" + names + "！获得了全部的下注额：" + bouns, 1000);
                                }
                                SInt["猜最大奖池"] = 0;
                                SInt["猜最大进程记录"] = 0;
                                timer.Deactivate();
                            }
                            catch (Exception ex)
                            {
                                Logger.ShowError(ex);
                                timer.Deactivate();
                            }
                        };
                        SInt["猜最大进程记录"] = 1;
                        timer.Activate();
                    }
                    pc.Gold -= 10000;
                    SInt["猜最大奖池"] += 10000;
                    int num1 = Global.Random.Next(1, 9);
                    int num2 = Global.Random.Next(1, 9);
                    int num3 = Global.Random.Next(1, 9);
                    int sum = num1 + num2 + num3;
                    Say(pc, 0, "叮呤咣啷...$R" +
                        "┏━━┳━━┳━━┓$R" +
                        "┃  " + num1.ToString() + "  ┃  " + num2.ToString() + "  ┃  " + num3.ToString() + "  ┃$R" +
                        "┗━━┻━━┻━━┛$R" +
                        "你的掷点总数为$CR" + sum.ToString() + "$CD点。$R请等待结果哦！$R$R如果你的点数最大，将会获得本次下注的全部金币。", "掷点机");
                    SDict["猜最大玩家内容"][pc.Name] = sum;
                    SagaMap.Skill.SkillHandler.Instance.ActorSpeak(Golem, pc.Name + "掷出了" + sum.ToString() + "点！那就祝你好运咯！下注金额已经累积了：" + SInt["猜最大奖池"]);
                    break;
                case 2:
                    if (pc.Account.GMLevel > 200)
                    {
                        switch (Select(pc, "GM控制台", "", "调整下次NPC点数", "强制重置（在卡住的时候使用）", "将NPC改为不可移动", "继续"))
                        {
                            case 1:
                                int num = int.Parse(InputBox(pc, "输入点数（3-27）", InputType.Bank));
                                SInt["指定点数"] = num;
                                break;
                            case 2:
                                SInt["猜最大进程记录"] = 0;
                                SDict["猜最大玩家内容"] = new VariableHolderA<string, int>();
                                break;
                            case 3:
                                MobEventHandler eh = new MobEventHandler(Golem);
                                eh.AI.Mode = new SagaMap.Mob.AIMode(4);
                                break;
                        }
                    }
                           
                    Dictionary<string, int> d = new Dictionary<string, int>();
                    foreach (var item in SDict["比最大_排行榜"])
                        d.Add(item.Key, item.Value);
                    List<detail> 排名 = DictonarySort(d);
                    SStr["未产出"] = "未产出";
                    string s = "————————";
                    s += "$R第一名：" + 排名[0].name + " - " + 排名[0].count.ToString();
                    s += " $R第二名：" + 排名[1].name + " - " + 排名[1].count.ToString();
                    s += " $R第三名：" + 排名[2].name + " - " + 排名[2].count.ToString();
                    s += " $R第四名：" + 排名[3].name + " - " + 排名[3].count.ToString();
                    s += " $R第五名：" + 排名[4].name + " - " + 排名[4].count.ToString();
                    s += "$P————————";
                    s += " $R第六名" + 排名[5].name + " - " + 排名[5].count.ToString();
                    s += " $R第七名：" + 排名[6].name + " - " + 排名[6].count.ToString();
                    s += "$R第八名：" + 排名[7].name + " - " + 排名[7].count.ToString();
                    s += " $R第九名：" + 排名[8].name + " - " + 排名[8].count.ToString();
                    s += " $R第十名：" + 排名[9].name + " - " + 排名[9].count.ToString();
                    s += "$P————————";
                    s += " $R第十一名：" + 排名[10].name + " - " + 排名[10].count.ToString();
                    s += " $R第十二名：" + 排名[11].name + " - " + 排名[11].count.ToString();
                    s += " $R第十三名：" + 排名[12].name + " - " + 排名[12].count.ToString();
                    s += " $R第十四名：" + 排名[13].name + " - " + 排名[13].count.ToString();
                    s += " $R第十五名：" + 排名[14].name + " - " + 排名[14].count.ToString();
                    Say(pc, 131, s, "排行榜");
                    break;
            }
        }
        private List<detail> DictonarySort(Dictionary<string, int> dic)
        {
            List<detail> 排名 = new List<detail>();
            var dicSort = from objDic in dic orderby objDic.Value descending select objDic;
            foreach (KeyValuePair<string, int> kvp in dicSort)
            {
                detail d = new detail();
                d.name = kvp.Key;
                d.count = kvp.Value;
                排名.Add(d);
            }
            if (排名.Count < 15)
            {
                for (int i = 0; i < 15; i++)
                {
                    if (i >= 排名.Count)
                    {
                        detail d = new detail();
                        d.name = "未产出";
                        d.count = 0;
                        排名.Add(d);
                    }
                }
            }
            return 排名;
        }
        class detail
        {
            public string name;
            public int count;
        }
    }
}