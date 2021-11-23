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
    public class S11000818 : Event
    {
        public S11000818()
        {
            this.EventID = 11000818;
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
            Say(pc, 131, "您好，欢迎光临，请问需要什么？", "酒屋店主");
            switch (Select(pc, "怎么办呢？", "", "送达货物", "没事"))
            {
                case 1:
                    if (pc.AInt["接受了搬运任务"] == 1 && CountItem(pc, 953200002) >= 1)
                    {
                        pc.AInt["西国搬运完成"]++;
                        pc.AInt["接受了搬运任务"] = 0;
                        if (pc.AInt["西国搬运完成"] < 10)
                            Say(pc, 131, "对，这是我订购的货物。$R辛苦了。", "酒屋店主");
                        else if (pc.AInt["西国搬运完成"] < 30)
                            Say(pc, 131, "辛苦了，不如坐下来喝杯酒吧，$R当然是要收费的，呵呵呵。", "酒屋店主");
                        else
                            Say(pc, 131, "最近客人增多，订单量也上去了。$R可能要经常麻烦你$R喝杯酒吧，就当我请你的。", "酒屋店主");
                        pc.Gold += Global.Random.Next(50000, 80000);
                        if (pc.AInt["西国搬运完成"] >= 10 && pc.AInt["西国搬运技能点获取"] != 1)
                        {
                            Say(pc, 131, "你获得了一个技能点。");
                            pc.AInt["西国搬运技能点获取"] = 1;
                            pc.SkillPoint3 += 1;
                        }
TakeItem(pc,953200002,1);
TitleProccess(pc, 56, 1);
                        int rate = Global.Random.Next(30, 50);
                        uint exp = (uint)(pc.Level * pc.JobLevel3 * rate);
                        SagaMap.Manager.ExperienceManager.Instance.ApplyExp(pc, exp, exp, 1f);
                        PlaySound(pc, 4012, false, 100, 50);
                        Wait(pc, 1000);
                        if (pc.AInt["西国搬运完成"] < 10)
                            Say(pc, 131, "欢迎随时到我的酒馆坐。$R呵呵呵……", "酒屋店主");
                        return;
                    }
                    break;
            }
        }
    }
}

