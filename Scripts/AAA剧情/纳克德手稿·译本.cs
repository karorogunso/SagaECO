
using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
using SagaMap.ActorEventHandlers;
namespace SagaScript.M30210000
{
    public partial class S70001000: Event
    {
        public S70001000()
        {
            this.EventID = 70001000;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 623, "欢迎阅读纳克德手稿·译本。$R$R本译本由Ygg翻译，$R剧情表现形式与原版有很大区别，$R请不要过多在意。$R$R感谢 $CM星妈$CD 翻译。", "纳克德手稿·译本");
            Say(pc,623,"本次先重现序章，$R由于工作量庞大$R（序章就写了近30个脚本文件）$R未来只有反响不错时，才将会考虑重现更多章节。", "纳克德手稿·译本");
            string[] ops = GetChapterList(pc);
            switch (Select(pc,"请选择要阅读的章节","", ops))
            {
                case 1://离开按钮
                    return;
                case 2://好奇的少女
                    if (Select(pc, "确定要开始阅读 序章·好奇的少女 吗？", "", "是的", "算了") != 1)
                        return;
                    if(pc.Level < 40)
                    {
                        Say(pc,623,"阅读等级不够哦。", "纳克德手稿·译本");
                        return;
                    }
                    if(pc.QuestRemaining < 0)
                    {
                        Say(pc, 623, "任务点不够了哦。", "纳克德手稿·译本");
                        return;
                    }
                    if(pc.AInt["序章·好奇的少女"] == 1)
                    {
                        Say(pc,623,"抱歉，暂时无法重复阅读哦", "纳克德手稿·译本");
                        return;
                    }
                    pc.QuestRemaining -= 0;
                    pc.TInt["AAA剧情序章DEM死亡"] = 0;
                    pc.TInt["AAA序章剧情图4防卫兵器"] = 0;
                    序章开始(pc);
                    break;
            }
        }
        public string[] GetChapterList(ActorPC pc)
        {
            List<string> cs = new List<string>();
            cs.Add("离开");
            cs.Add("序章·好奇的少女");
            for (int i = 1; i < cs.Count; i++)
            {
                if (pc.AInt[cs[i]] != 1)
                    cs[i] += "[未阅读]";
                else
                    cs[i] += "[已阅]";
                if (i == 1)
                    cs[i] += "[要求等级40级]";
            }
            string[] ops = new string[cs.Count];
            cs.CopyTo(ops);
            return ops;
        }
    }
}

