using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.SkillDefinations.Global;
using SagaLib;
using SagaMap;
using SagaMap.Skill.Additions.Global;


namespace SagaMap.Skill.SkillDefinations.Royaldealer
{
    class Rhetoric : ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            int lifetime = 30000 + 30000 * level;
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            List<Actor> affected = map.GetActorsArea(sActor, 250, true);
            foreach (Actor act in affected)
            {
                if (!SkillHandler.Instance.CheckValidAttackTarget(sActor, act))
                {
                    DefaultBuff skill = new DefaultBuff(args.skill, act, "Rhetoric", lifetime);
                    skill.OnAdditionStart += this.StartEventHandler;
                    skill.OnAdditionEnd += this.EndEventHandler;
                    SkillHandler.ApplyAddition(act, skill);
                }
            }

            
        }
        #endregion
        void StartEventHandler(Actor actor, DefaultBuff skill)
        {
            int up = 80 + 70 * skill.skill.Level;
            if (skill.Variable.ContainsKey("Rhetoric"))
                skill.Variable.Remove("Rhetoric");
            skill.Variable.Add("Rhetoric", up);
            actor.Status.mp_skill += (short)up;
            actor.Status.sp_skill += (short)up;
            actor.Buff.三转レトリック = true;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
        void EndEventHandler(Actor actor, DefaultBuff skill)
        {
            actor.Status.mp_skill -= (short)skill.Variable["Rhetoric"];
            actor.Status.sp_skill -= (short)skill.Variable["Rhetoric"];
            actor.Buff.三转レトリック = false;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
    }
}