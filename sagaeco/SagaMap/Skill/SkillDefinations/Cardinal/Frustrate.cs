using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.SkillDefinations.Global;
using SagaLib;
using SagaMap;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations.Cardinal
{
    class Frustrate : ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            int lifetime = 0;
            short[] range = { 0, 100, 100, 200, 100, 200 };
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            List<Actor> actors = map.GetActorsArea(dActor, range[level], true);
            List<Actor> affected = new List<Actor>();
            foreach (Actor i in actors)
            {
                if (SkillHandler.Instance.CheckValidAttackTarget(sActor, i))
                {
                    DefaultBuff skill = new DefaultBuff(args.skill, i, "Frustrate", lifetime);
                    skill.OnAdditionStart += this.StartEventHandler;
                    skill.OnAdditionEnd += this.EndEventHandler;
                    SkillHandler.ApplyAddition(i, skill);
                }
            }
        }
        #endregion

        void StartEventHandler(Actor actor, DefaultBuff skill)
        {
            float[] rank = { 0, 0.12f, 0.15f, 0.18f, 0.21f, 0.24f };
            if (skill.Variable.ContainsKey("Frustrate_DEF"))
                skill.Variable.Remove("Frustrate_DEF");
            skill.Variable.Add("Frustrate_DEF", (int)(actor.Status.def_bs * rank[skill.skill.Level]));
            actor.Status.def_skill -= (short)(actor.Status.def_bs * rank[skill.skill.Level]);

            if (skill.Variable.ContainsKey("Frustrate_MDEF"))
                skill.Variable.Remove("Frustrate_MDEF");
            skill.Variable.Add("Frustrate_MDEF", (int)(actor.Status.mdef_bs * rank[skill.skill.Level]));
            actor.Status.mdef_skill -= (short)(actor.Status.mdef_bs * rank[skill.skill.Level]);
            actor.Buff.防御力減少 = true;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
        void EndEventHandler(Actor actor, DefaultBuff skill)
        {
            actor.Status.def_skill += (short)skill.Variable["Frustrate_DEF"];
            actor.Status.mdef_skill += (short)skill.Variable["Frustrate_MDEF"];
            actor.Buff.防御力減少 = false;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
    }
}
