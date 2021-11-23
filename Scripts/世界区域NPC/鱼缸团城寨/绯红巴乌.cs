
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
    public class S60000131 : Event
    {
        public S60000131()
        {
            this.EventID = 60000131;
        }
        private int GetWeekOfYear(DateTime dt)
        {
            GregorianCalendar gc = new GregorianCalendar();
            return gc.GetWeekOfYear(dt, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
        }
        public override void OnEvent(ActorPC pc)
        {
            SagaMap.Skill.SkillHandler.SendSystemMessage(pc, "已累积消耗任务点：" + pc.AInt["本周消耗任务点"]);
            Say(pc, 0, "哈喽，我是绯红巴乌！$R我特别欣赏努力的人。$R$R当前累积消耗任务点："+ pc.AInt["本周消耗任务点"], "每周奖励·绯红巴乌");
            Say(pc, 0, "如果你这周足够努力的话，$R我会给你「$CR好东西$CD」的！", "每周奖励·绯红巴乌");
            switch (Select(pc, "请问找我有什么事呢？", "", "领取每周奖励", "确认奖励列表", "离开"))
            {
                case 1:
                    每周清零(pc);
                    return;
                case 2:
                    奖励列表确认(pc);
                    return;
                case 3:
                    return;
            }
            return;
        }
        void 每周清零(ActorPC pc)
        {
            if (pc.AInt["周清零记录"] != GetWeekOfYear(DateTime.Now))
            {
                if (pc.AInt["本周消耗任务点"] < 100)
                {
                    Say(pc, 0, "我看看……嗯……", "每周奖励·绯红巴乌");
                    Say(pc, 0, "绯红巴乌嗅了嗅你的身体");
                    Say(pc, 0, "不行啊，你这种程度完全不够！$$至少消耗「$CR100个任务点$CD」再来找我吧。", "每周奖励·绯红巴乌");
                    return;
                }
                if (pc.AInt["本周消耗任务点"] > 99)
                {
                    if (pc.AInt["本周消耗任务点"] >= 2100)
                        两千一百点奖励(pc);
                    else if (pc.AInt["本周消耗任务点"] >= 1500)
                        一千五百点奖励(pc);
                    else if (pc.AInt["本周消耗任务点"] >= 1000)
                        一千点奖励(pc);
                    else if (pc.AInt["本周消耗任务点"] >= 600)
                        六百点奖励(pc);
                    else if (pc.AInt["本周消耗任务点"] >= 300)
                        三百点奖励(pc);
                    else if (pc.AInt["本周消耗任务点"] >= 100)
                        一百点奖励(pc);
                    ShowEffect(pc, 4252);
                    Say(pc, 0, "拿着吧，这些是你应得的！~", "每周奖励·绯红巴乌");
                    pc.AInt["周清零记录"] = GetWeekOfYear(DateTime.Now);
                }
            }
            else
            {
                Say(pc,0,"你这周已经领取过了哦，$R下周再来吧！$R$R重置时间：每周一凌晨0点。", "每周奖励·绯红巴乌");
            }
        }
        void 奖励列表确认(ActorPC pc)
        {
            Say(pc, 0, "每周消耗100任务点奖励：    $R  - 项链、武器、衣服强化石各20个    $R  - 封印的凶暴的魔物们篇卡片10张    $R  - 限定KUJI币10个    $R  - 2000CP。", "每周奖励·绯红巴乌");
            Say(pc, 0, "每周消耗300任务点奖励：    $R  - 项链、武器、衣服强化石各23个    $R  - 封印的凶暴的魔物们篇卡片12张    $R  - 限定KUJI币12个    $R  - 2400CP。", "每周奖励·绯红巴乌");
            Say(pc, 0, "每周消耗600任务点奖励：    $R  - 项链、武器、衣服强化石各26个    $R  - 封印的凶暴的魔物们篇卡片15张    $R  - 限定KUJI币14个    $R  - 2800CP。", "每周奖励·绯红巴乌");
            Say(pc, 0, "每周消耗1000任务点奖励：    $R  - 项链、武器、衣服强化石各30个    $R  - 封印的凶暴的魔物们篇卡片17张    $R  - 限定KUJI币16个    $R  - 3200CP。", "每周奖励·绯红巴乌");
            Say(pc, 0, "每周消耗1500任务点奖励：    $R  - 项链、武器、衣服强化石各35个    $R  - 封印的凶暴的魔物们篇卡片18张    $R  - 限定KUJI币18个    $R  - 3600CP。", "每周奖励·绯红巴乌");
            Say(pc, 0, "每周消耗2100任务点奖励：    $R  - 项链、武器、衣服强化石各40个    $R  - 封印的凶暴的魔物们篇卡片20张    $R  - 限定KUJI币20个    $R  - 4000CP。", "每周奖励·绯红巴乌");
        }


        void 一百点奖励(ActorPC pc)
        {
            GiveItem(pc, 960000000, 20);//项链强化石
            GiveItem(pc, 960000001, 20);//武器强化石
            GiveItem(pc, 960000002, 20);//衣服强化石
            GiveItem(pc, 950000105, 1);//十连
            GiveItem(pc, 950000025, 10);//限定币
            pc.CP += 2000;
            pc.AInt["本周消耗任务点"] = 0;
            SagaMap.Network.Client.MapClient.FromActorPC(pc).SendSystemMessage("获得了消耗100任务点的奖励。");
        }
        void 三百点奖励(ActorPC pc)
        {
            GiveItem(pc, 960000000, 23);//项链强化石
            GiveItem(pc, 960000001, 23);//武器强化石
            GiveItem(pc, 960000002, 23);//衣服强化石
            GiveItem(pc, 950000105, 1);//十连
            GiveItem(pc, 950000103, 2);//单抽
            GiveItem(pc, 950000025, 12);//限定币
            pc.CP += 2400;
            pc.AInt["本周消耗任务点"] = 0;
            SagaMap.Network.Client.MapClient.FromActorPC(pc).SendSystemMessage("获得了消耗300任务点的奖励。");
        }
        void 六百点奖励(ActorPC pc)
        {
            GiveItem(pc, 960000000, 26);//项链强化石
            GiveItem(pc, 960000001, 26);//武器强化石
            GiveItem(pc, 960000002, 26);//衣服强化石
            GiveItem(pc, 950000105, 1);//十连
            GiveItem(pc, 950000104, 1);//五抽
            GiveItem(pc, 950000025, 14);//限定币
            pc.CP += 2800;
            pc.AInt["本周消耗任务点"] = 0;
            SagaMap.Network.Client.MapClient.FromActorPC(pc).SendSystemMessage("获得了消耗600任务点的奖励。");
        }
        void 一千点奖励(ActorPC pc)
        {
            GiveItem(pc, 960000000, 30);//项链强化石
            GiveItem(pc, 960000001, 30);//武器强化石
            GiveItem(pc, 960000002, 30);//衣服强化石
            GiveItem(pc, 950000105, 1);//十连
            GiveItem(pc, 950000104, 1);//五抽
            GiveItem(pc, 950000103, 2);//单抽
            GiveItem(pc, 950000025, 16);//限定币
            pc.CP += 3200;
            pc.AInt["本周消耗任务点"] = 0;
            SagaMap.Network.Client.MapClient.FromActorPC(pc).SendSystemMessage("获得了消耗1000任务点的奖励。");
        }
        void 一千五百点奖励(ActorPC pc)
        {
            GiveItem(pc, 960000000, 35);//项链强化石
            GiveItem(pc, 960000001, 35);//武器强化石
            GiveItem(pc, 960000002, 35);//衣服强化石
            GiveItem(pc, 950000105, 1);//十连
            GiveItem(pc, 950000104, 1);//五抽
            GiveItem(pc, 950000103, 3);//单抽
            GiveItem(pc, 950000025, 18);//限定币
            pc.CP += 3600;
            pc.AInt["本周消耗任务点"] = 0;
            SagaMap.Network.Client.MapClient.FromActorPC(pc).SendSystemMessage("获得了消耗1500任务点的奖励。");
        }
        void 两千一百点奖励(ActorPC pc)
        {
            GiveItem(pc, 960000000, 40);//项链强化石
            GiveItem(pc, 960000001, 40);//武器强化石
            GiveItem(pc, 960000002, 40);//衣服强化石
            GiveItem(pc, 950000105, 2);//十连
            GiveItem(pc, 950000025, 20);//限定币
            pc.CP += 4000;
            pc.AInt["本周消耗任务点"] = 0;
            SagaMap.Network.Client.MapClient.FromActorPC(pc).SendSystemMessage("获得了消耗2100任务点的奖励。");
        }
    }
}