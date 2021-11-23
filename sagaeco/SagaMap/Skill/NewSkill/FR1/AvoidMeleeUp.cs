using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations.Scout
{
    /// <summary>
    /// 敏捷的動作
    /// </summary>
    public class AvoidMeleeUp : ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            int[] lifetime = {0, 10000,22000,35000,50000,60000};
            Skill.Additions.Global.DefaultBuff buff = new DefaultBuff(args.skill, sActor, "瞬步", lifetime[level]);
            buff.OnAdditionStart += buff_OnAdditionStart;
            buff.OnAdditionEnd += buff_OnAdditionEnd;
            SkillHandler.ApplyAddition(sActor, buff);
        }

        void buff_OnAdditionStart(Actor actor, DefaultBuff skill)
        {
            if (actor.type == ActorType.PC)
            {
                ActorPC pc = (ActorPC)actor;
                if (skill.skill.Level == 1 || skill.skill.Level == 2)
                    pc.TInt["瞬步閃避率"] = 10;
                if (skill.skill.Level == 3 || skill.skill.Level == 4)
                    pc.TInt["瞬步閃避率"] = 15;
                if (skill.skill.Level == 5)
                    pc.TInt["瞬步閃避率"] = 20;
                actor.Buff.AvdMeleeUp = true;
                Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
            }
        }
        void buff_OnAdditionEnd(Actor actor, DefaultBuff skill)
        {
            if (actor.type == ActorType.PC)
            {
                ActorPC pc = (ActorPC)actor;
                pc.TInt["瞬步閃避率"] = 0;
                actor.Buff.AvdMeleeUp = false;
                Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
            }
        }
        #endregion
    }
}
