using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaLib;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations
{
    /// <summary>
    /// 变形术：将你的目标变成一只番茄！
    /// </summary>
    public class S14041 : ISkill
    {

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            if (pc.Status.Additions.ContainsKey("变形术CD"))
                return -30;
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {

            if (sActor.type == ActorType.PC)
            {
                ActorPC Me = (ActorPC)sActor;
                Me.TInt["水属性魔法释放"] = 0;
                Me.TInt["火属性魔法释放"] = 0;

                int cooldown = 0;
                switch (Me.Skills2[14041].Level)
                {
                    case 1:
                        cooldown = 30000;
                        break;
                    case 2:
                        cooldown = 20000;
                        break;
                    case 3:
                        cooldown = 15000;
                        break;
                    case 4:
                        cooldown = 10000;
                        break;
                }

                DefaultBuff cd = new DefaultBuff(sActor, "变形术CD", cooldown);
                SkillHandler.ApplyAddition(sActor, cd);

                switch (level)
                {
                    case 1:
                        dActor.IllusionPictID = 10620000;//咕咕
                        break;
                    case 2:
                        dActor.IllusionPictID = 10660000;//哞哞
                        break;
                    case 3:
                        dActor.IllusionPictID = 15150000;//气球小猪
                        break;
                    case 4:
                        dActor.IllusionPictID = 60000013;//番茄
                        break;
                    default:
                        break;
                }

                if (Me.Skills2[14041].Level >= 4 && dActor.Tasks.ContainsKey("SkillCast"))
                {
                    SkillHandler.Instance.CancelSkillCast(dActor);
                    //猪队友：使用LV3变形术打断队友的技能30次以上。
                    if (dActor.type == ActorType.PC && ((ActorPC)dActor).Party == ((ActorPC)sActor).Party)
                        SkillHandler.Instance.TitleProccess(sActor, 97, 1);
                }

                Map map = Manager.MapManager.Instance.GetMap(dActor.MapID);
                map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.CHAR_INFO_UPDATE, null, dActor, true);

                OtherAddition skill = new OtherAddition(null, dActor, "变形术", 10000);
                skill.OnAdditionEnd += (s, e) =>
                {
                    dActor.IllusionPictID = 0;
                    map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.CHAR_INFO_UPDATE, null, dActor, true);
                };
                SkillHandler.ApplyAddition(dActor, skill);
            }
        }
    }
}
