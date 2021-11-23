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
    public class S11000220 : Event
    {
        public S11000220()
        {
            this.EventID = 11000220;
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
            Say(pc, 131, "你好，我是瓦雷利亚。$R请问有什么事呢？", "瓦雷利亚");
            switch (Select(pc, "怎么办呢？", "", "送达货物", "没事"))
            {
                case 1:
                    if (pc.AInt["接受了搬运任务"] == 1 && CountItem(pc, 953200000) >= 1)
                    {
                        pc.AInt["北国搬运完成"]++;
                        pc.AInt["接受了搬运任务"] = 0;
                        if (pc.AInt["北国搬运完成"] < 10)
                            Say(pc, 131, "啊，这是我定的货物！$R$R大老远跑来真是辛苦你了，$R来，这是你的报酬。", "瓦雷利亚");
                        else if (pc.AInt["北国搬运完成"] < 30)
                            Say(pc, 131, "咳咳...$R最近经常看你来帮忙搬东西啊。$R$R你问我买这么多包裹干嘛？$R我才不告诉你呢！", "瓦雷利亚");
                        else
                            Say(pc, 131, "啊，又是你！$R我都认识你了，$R你叫" + pc.Name + "对吧，听说你是个大佬。$R$R嘛，总之辛苦你啦。", "瓦雷利亚");
                        pc.Gold += Global.Random.Next(50000, 80000);
                        if (pc.AInt["北国搬运完成"] >= 10 && pc.AInt["北国搬运技能点获取"] != 1)
                        {
                            Say(pc, 131, "你获得了一个技能点。");
                            pc.AInt["北国搬运技能点获取"] = 1;
                            pc.SkillPoint3 += 1;
                        }
TakeItem(pc,953200000,1);
TitleProccess(pc, 56, 1);
                        int rate = Global.Random.Next(30, 50);
                        uint exp = (uint)(pc.Level * pc.JobLevel3 * rate);
                        SagaMap.Manager.ExperienceManager.Instance.ApplyExp(pc, exp, exp, 1f);
                        PlaySound(pc, 4012, false, 100, 50);
                        Wait(pc, 1000);
                        if (pc.AInt["北国搬运完成"] < 10)
                            Say(pc, 131, "没事的话还可以多坐坐哦", "瓦雷利亚");
                        return;
                    }
                    break;
            }
        }
    }
}

