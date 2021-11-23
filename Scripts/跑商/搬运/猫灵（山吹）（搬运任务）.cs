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
    public class S83000000 : Event
    {
        public S83000000()
        {
            this.EventID = 83000000;
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
            if (pc.MapID != 20022000 && pc.Account.GMLevel < 10)
            {
                SInt[pc.Name + "跑商异常"]++;
                pc.Account.Banned = true;
                return;
            }
            Map map = SagaMap.Manager.MapManager.Instance.GetMap(pc.MapID);

            short X = Global.PosX8to16(14, map.Width);
            short Y = Global.PosY8to16(9, map.Height);

            byte Range = (byte)(Math.Max(Math.Abs(pc.X - X) / 100, Math.Abs(pc.Y - Y) / 100));

            if (Range > 9)
                return;

            Say(pc, 131, "喵呜，有什么事吗？我的主人正忙着呢喵$R要是找我家主人有什么事，$R可以跟我说喵。", "猫灵（山吹）");
            switch (Select(pc, "怎么办呢？", "", "送达货物", "没事"))
            {
                case 1:
                    if (pc.AInt["接受了搬运任务"] == 1 && CountItem(pc, 953200003) >= 1)
                    {
                        pc.AInt["南部矿区搬运完成"]++;
                        pc.AInt["接受了搬运任务"] = 0;
                        if (pc.AInt["南部矿区搬运完成"] < 10)
                            Say(pc, 131, "给主人的货物喵？里头是小鱼干喵！？$R什么……是给主人的生活用品？", "猫灵（山吹）");
                        else if (pc.AInt["南部矿区搬运完成"] < 30)
                        {
                            Say(pc, 131, "喵喵喵！！你很眼熟耶。$R哼，反正也还是给主人的东西吧。", "猫灵（山吹）");
                            Say(pc, 131, "咦？$R这是特意给我带的……小鱼干？", "猫灵（山吹）");
                        }
                        else
                        {
                            Say(pc, 131, "辛苦了，替我家主人感谢你喵~", "猫灵（山吹）");
                        }
                        if (pc.AInt["南部矿区搬运完成"] >= 10 && pc.AInt["南部矿区搬运完成技能点获取"] != 1)
                        {
                            Say(pc, 131, "你获得了一个技能点。");
                            pc.AInt["南部矿区搬运完成技能点获取"] = 1;
                            pc.SkillPoint3 += 1;
                        }
                        TakeItem(pc, 953200003, 1);
                        TitleProccess(pc, 56, 1);
                        pc.Gold += Global.Random.Next(180000, 250000);
                        int rate = Global.Random.Next(120, 200);
                        uint exp = (uint)(pc.Level * pc.JobLevel3 * rate);
                        SagaMap.Manager.ExperienceManager.Instance.ApplyExp(pc, exp, exp, 1f);
                        PlaySound(pc, 4012, false, 100, 50);
                        Wait(pc, 1000);
                        if (pc.AInt["南部矿区搬运完成"] < 30)
                            Say(pc, 131, "也请多多关照我家主人的矿石生意喵。", "猫灵（山吹）");
                        return;
                    }
                    break;
            }
        }
    }
}

