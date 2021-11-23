
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
using SagaMap.Skill;
using SagaMap.Skill.Additions.Global;
namespace SagaScript.M30210000
{
    public class S110003412 : Event
    {
        public S110003412()
        {
            EventID = 110003412;
        }
        public override void OnEvent(ActorPC pc)
        {
            if (CountItem(pc, 110003412) < 1) return;
            if(pc.Status.Additions.ContainsKey("解毒果实CD"))
            {
                SkillHandler.SendSystemMessage(pc, "你刚刚吃了解毒果实过了！30秒内无法再次使用。");
                return;
            }
            if(pc.Status.Additions.ContainsKey("腐毒丧尸感染"))
            {
                Addition 腐毒丧尸感染 = pc.Status.Additions["腐毒丧尸感染"];
                SkillHandler.RemoveAddition(pc, 腐毒丧尸感染);
                SkillHandler.SendSystemMessage(pc, "你吃了解毒果实，中毒效果解除了。");
                OtherAddition skill = new OtherAddition(null, pc, "解毒果实CD", 3000);
                SkillHandler.ApplyAddition(pc, skill);
            }
            ShowEffect(pc, 4149);
            TakeItem(pc, 110003412, 1);
        }
    }
}