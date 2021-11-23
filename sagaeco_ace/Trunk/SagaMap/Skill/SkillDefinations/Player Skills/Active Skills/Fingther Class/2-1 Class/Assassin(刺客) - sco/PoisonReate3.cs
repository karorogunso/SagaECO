using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations.Assassin
{
    /// <summary>
    /// 豪腕毒（豪腕毒）
    /// </summary>
    public class PoisonReate3 : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            uint itemID = 10000353;//刺客的內服藥（互換毒）
            if (SkillHandler.Instance.CountItem(pc, itemID) > 0)
            {
                SkillHandler.Instance.TakeItem(pc, itemID, 1);
                return 0;
            }
            return -2;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            int lifetime = 300000 - 30000 * level;
            DefaultBuff skill = new DefaultBuff(args.skill, sActor, "PoisonReate3", lifetime);
            skill.OnAdditionStart += this.StartEventHandler;
            skill.OnAdditionEnd += this.EndEventHandler;
            SkillHandler.ApplyAddition(sActor, skill);
        }
        void StartEventHandler(Actor actor, DefaultBuff skill)
        {
            int level = skill.skill.Level, max_atk_add, min_atk_add, rate = 0;
            float factor = 0.05f + 0.02f * level;

            max_atk_add = (int)(factor * actor.Status.max_atk3);
            min_atk_add = (int)(factor * actor.Status.min_atk3);

            rate = new int[] { 0, 0, 12, 24, 36, 50 }[level];


            //大傷
            if (skill.Variable.ContainsKey("PoisonReate3_Max"))
                skill.Variable.Remove("PoisonReate3_Max");
            skill.Variable.Add("PoisonReate3_Max", max_atk_add);
            actor.Status.max_atk3_skill += (short)max_atk_add;
            //小傷
            if (skill.Variable.ContainsKey("PoisonReate3_Min"))
                skill.Variable.Remove("PoisonReate3_Min");
            skill.Variable.Add("PoisonReate3_Min", min_atk_add);
            actor.Status.min_atk3_skill += (short)min_atk_add;

            //中毒?
            if (SkillHandler.Instance.CanAdditionApply(actor, actor, SkillHandler.DefaultAdditions.Poison, rate))
            {
                Additions.Global.Poison nskill = new SagaMap.Skill.Additions.Global.Poison(skill.skill, actor, 7000);
                SkillHandler.ApplyAddition(actor, nskill);
            }
            actor.Buff.MinAtkUp = true;
            actor.Buff.MaxAtkUp = true;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
        void EndEventHandler(Actor actor, DefaultBuff skill)
        {
            //大傷
            actor.Status.max_atk3_skill -= (short)skill.Variable["PoisonReate3_Max"];
            //小傷
            actor.Status.min_atk3_skill -= (short)skill.Variable["PoisonReate3_Min"];

            actor.Buff.MinAtkUp = false;
            actor.Buff.MaxAtkUp = false;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
        #endregion
    }
}
