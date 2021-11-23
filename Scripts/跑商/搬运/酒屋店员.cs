using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaMap;
using SagaDB.Actor;
using SagaMap.Skill;
using SagaDB.Item;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30210000
{
    public class S11000270 : Event
    {
        public S11000270()
        {
            this.EventID = 11000270;
        }

        public override void OnEvent(ActorPC pc)
        {
            /*if (pc.AInt["跑商次数卡死2"] == 1)
            {
                Say(pc, 131, "抱歉，$R您由于某些原因已被限制该任务。$R$R可能的原因如下：$R您多开了$R使用了黑科技$R等");
                return;
            }
            byte count = 0;
            string lastip = "";
            foreach (SagaMap.Network.Client.MapClient i in SagaMap.Manager.MapClientManager.Instance.OnlinePlayer)
                if (i.Character.Account.LastIP == pc.Account.LastIP && i.Character.Account.GMLevel < 20)
                    count++;
            if (count > 1)
            {
                foreach (SagaMap.Network.Client.MapClient i in SagaMap.Manager.MapClientManager.Instance.OnlinePlayer)
                    if (i.Character.Account.LastIP == lastip && i.Character.Account.GMLevel < 20)
                        i.Character.AInt["跑商次数卡死2"] = 1;
                Say(pc, 131, "抱歉，$R您由于某些原因已被限制该任务。$R$R可能的原因如下：$R您多开了$R使用了黑科技$R等");
                return;
            }*/
            Say(pc, 131, "啊...我们酒店还没开张呢，$R$R请问你有什么事情吗？", "酒屋店员");
            switch(Select(pc,"怎么办呢？","","送达货物","没事"))
            {
                case 1:
                    if(pc.AInt["接受了搬运任务"] == 1 && CountItem(pc, 953200001) >= 1)
                    {
                        pc.AInt["南国搬运完成"]++;
                        pc.AInt["接受了搬运任务"] = 0;
                        if (pc.AInt["南国搬运完成"] < 10)
                            Say(pc, 131, "啊，我的包裹终于到了！$R这样我可以继续为酒屋做准备了。$R$R大老远跑来真是辛苦你了，$R来，这是你的报酬。", "酒屋店员");
                        else if (pc.AInt["南国搬运完成"] < 30)
                        {
                            Say(pc, 131, "呃..最近怎么总是你来帮忙送货呢。$R$R其实..我买的东西不全是为旧屋做准备的..$R我最近从鱼缸网上买了很多私人用品啦，比如○○○……", "酒屋店员");
                            Say(pc,131,"啊，你没听见把！$R总之这是给你的报酬，$R辛苦你啦。", "酒屋店员");
                        }
                        else
                        {
                            Say(pc, 131, "你好……谢谢……$R这是给你的报酬……", "酒屋店员");
                            Say(pc, 131, "最近沉迷网购了，也许酒屋永远都不会开业了呢？", "酒屋店员");
                        }
                        if (pc.AInt["南国搬运完成"] >= 10 && pc.AInt["南国搬运技能点获取"] != 1)
                        {
                            Say(pc, 131, "你获得了一个技能点。");
                            pc.AInt["南国搬运技能点获取"] = 1;
                            pc.SkillPoint3 += 1;
                        }
TakeItem(pc,953200001,1);
TitleProccess(pc, 56, 1);
                        pc.Gold += Global.Random.Next(50000, 80000);
                        int rate = Global.Random.Next(30 ,50);
                        uint exp = (uint)(pc.Level * pc.JobLevel3 * rate);
                        SagaMap.Manager.ExperienceManager.Instance.ApplyExp(pc, exp, exp, 1f);
                        PlaySound(pc, 4012, false, 100, 50);
                        Wait(pc, 1000);
                        if (pc.AInt["南国搬运完成"] < 30)
                            Say(pc,131,"等开业了之后，$R一定记得来喝一杯！", "酒屋店员");
                        return;
                    }
                    break;
            }
        }
    }
}

