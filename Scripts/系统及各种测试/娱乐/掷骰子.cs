
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

namespace SagaScript.M30210000
{
    public class S60050000 : Event
    {
        public S60050000()
        {
            this.EventID = 60050000;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 0, "沉迷赌博有害健康！$R珍爱生命！远离赌博！$R$R马上关掉了，下次活动再开~！");
            return;
            if (pc.Account.GMLevel > 200)
            {
                switch (Select(pc, "GM控制台", "", "调整奖池金额","强制重置（在卡住的时候使用）", "继续"))
                {
                    case 1:
                    int gold = int.Parse(InputBox(pc, "输入金额", InputType.Bank));
                    SInt["筛子奖池"] = gold;
                        break;
                    case 2:
                        SInt["筛子进程记录"] = 0;
                        SDict["筛子玩家内容"] = new VariableHolderA<string, int>();
                        break;
                }
            }
            Map map = SagaMap.Manager.MapManager.Instance.GetMap(pc.MapID);
            ActorGolem Golem = (ActorGolem)map.GetActor((uint)pc.TInt["触发的GOLEMID"]);
            Say(pc, 0, "那个~小姐姐~要来玩骰子吗？$R目前已经有$CR" + SDict["筛子玩家内容"].Count.ToString() + "$CD人下注了哦$R"
                + "要不要来试试运气呢？$R$R当前奖池：$CC" + SInt["筛子奖池"].ToString() + "$CD", "啊");
            switch (Select(pc, "要玩筛子吗？", "", "我要下注！(10000G/次)", "询问规则", "离开"))
            {
                case 1:
                    if (SInt["筛子进程记录"] != 1)
                        SDict["筛子玩家内容"] = new VariableHolderA<string, int>();
                    if (SDict["筛子玩家内容"].ContainsKey(pc.Name))
                    {
                        Say(pc, 0, "你已经下注过啦，$R请等待开奖哦", "啊");
                        return;
                    }
                    if (pc.Gold < 10000)
                    {
                        Say(pc, 0, "你的钱似乎不够呢？", "啊");
                        return;
                    }
                    if (SInt["筛子进程记录"] != 1)//开奖计时
                    {
                        SInt["筛子轮数"]++;
                        SagaMap.Skill.SkillHandler.Instance.ActorSpeak(Golem, "有玩家开始下注了哦！第" + SInt["筛子轮数"].ToString() + "轮摇奖将在120秒后开奖，大家赶紧来下注呀！");
                        Timer timer = new Timer("筛子计时", 0, 120000);
                        timer.AttachedPC = pc;
                        timer.OnTimerCall += (s, e) =>
                        {
                            try
                            {
                                int result = Global.Random.Next(3, 27);
                                int result2 = Global.Random.Next(3, 27);
                                int result3 = Global.Random.Next(3, 27);
                                while (result2 == result)
                                {
                                    result2 = Global.Random.Next(3, 27);
                                }
                                while (result3 == result || result3 == result2)
                                {
                                    result3 = Global.Random.Next(3, 27);
                                }
                                List<ActorPC> pcs = new List<ActorPC>();//中奖者
                                foreach (var item in SDict["筛子玩家内容"])
                                {
                                    if (item.Value == result || item.Value == result2 || item.Value == result3)
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
                                SagaMap.Skill.SkillHandler.Instance.ActorSpeak(Golem, "开奖的时间到了！本次开奖的点数是:" + result.ToString() + "," + result2.ToString() + "," + result3.ToString() );
                                SDict["筛子玩家内容"] = new VariableHolderA<string, int>();
                                if (pcs.Count == 0)
                                    SagaMap.Skill.SkillHandler.Instance.ActorSpeak(Golem, "很遗憾，这次没有人猜中点数，下注的金额将会进入奖池，欢迎再来哦。");
                                else
                                {
                                    string names = "";
                                    int bouns = SInt["筛子奖池"] / 2;
                                    SInt["筛子奖池"] -= bouns;
                                    SInt["骰子总支出"] += bouns;
                                    int singleb = SInt["筛子奖池"] / pcs.Count;
                                    for (int i = 0; i < pcs.Count; i++)
                                    {
                                        names += " " + pcs[i].Name;
                                        pcs[i].Gold += singleb;
                                    }
                                    SagaMap.Skill.SkillHandler.Instance.ActorSpeak(Golem, "哇！本轮共有" + pcs.Count.ToString() + "位中奖者！恭喜：" + names + "！将会获得奖池内金钱的一半："+bouns);
                                }
                                SInt["筛子进程记录"] = 0;
                                timer.Deactivate();
                            }
                            catch(Exception ex)
                            {
                                timer.Deactivate();
                            }
                        };
                        SInt["筛子进程记录"] = 1;
                        timer.Activate();
                    }

                    pc.Gold -= 10000;
                    SInt["骰子总收入"] += 10000;
                    SInt["筛子奖池"] += 9000;
                    int num1 = Global.Random.Next(1, 9);
                    int num2 = Global.Random.Next(1, 9);
                    int num3 = Global.Random.Next(1, 9);
                    int sum = num1 + num2 + num3;
                    Say(pc, 0, "叮呤咣啷...$R" +
                        "┏━━┳━━┳━━┓$R" +
                        "┃   " + num1.ToString() + "  ┃   " + num2.ToString() + "  ┃   " + num3.ToString() + "  ┃$R" +
                        "┗━━┻━━┻━━┛$R" +
                        "你的掷点总数为$CR" + sum.ToString() + "$CD点。$R请等待结果哦！","掷点机");
                    SDict["筛子玩家内容"][pc.Name] = sum;
                    SagaMap.Skill.SkillHandler.Instance.ActorSpeak(Golem, pc.Name + "掷出了"+ sum.ToString()+"点！那就祝你好运咯！");
                    break;
                case 2:
                    Say(pc, 0, "下注后会掷3个点，$R然后下注结束后NPC会掷3个点$R$R如果玩家正中NPC的点数，" +
                        "$R则玩家获得奖池内的金币的一半。$R(如有多名玩家则平分)$R$R如果未中，则下注额的90％会进入奖池。", "玩法规则");
                    break;
                case 3:
                    break;
            }
        }

    }
}