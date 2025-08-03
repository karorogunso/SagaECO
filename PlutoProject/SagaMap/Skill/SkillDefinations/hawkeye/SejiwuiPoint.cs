using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations.Hawkeye
{
    public class SejiwuiPoint : ISkill
    {
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            int lifetime = 1500000;
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            List<Actor> affected = map.GetActorsArea(sActor, 300, true);
            List<Actor> realAffected = new List<Actor>();
            foreach (Actor act in affected)
            {
                if (act.type == ActorType.PC)
                {
                    ActorPC pc = (ActorPC)act;
                    if (pc.Online)
                    {
                        DefaultBuff skill = new DefaultBuff(args.skill, act, "SejiwuiPoint", lifetime);
                        skill.OnAdditionStart += this.StartEventHandler;
                        skill.OnAdditionEnd += this.EndEventHandler;
                        SkillHandler.ApplyAddition(act, skill);
                    }
                }
            }
        }
        void StartEventHandler(Actor actor, DefaultBuff skill)
        {
            int hit_add = (int)(actor.Status.hit_melee_bs * 0.02f * skill.skill.Level);
            int cri_add = 3 * skill.skill.Level;

            if (skill.Variable.ContainsKey("SejiwuiPoint_hit"))
                skill.Variable.Remove("SejiwuiPoint_hit");
            skill.Variable.Add("SejiwuiPoint_hit", hit_add);
            actor.Status.hit_melee_skill += (short)hit_add;

            if (skill.Variable.ContainsKey("SejiwuiPoint_cri"))
                skill.Variable.Remove("SejiwuiPoint_cri");
            skill.Variable.Add("SejiwuiPoint_cri", cri_add);
            actor.Status.hit_critical_skill += (short)cri_add;

            actor.Buff.三转せーチウィークポイント = true;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
        void EndEventHandler(Actor actor, DefaultBuff skill)
        {
            actor.Status.hit_melee_skill -= (short)skill.Variable["SejiwuiPoint_hit"];
            actor.Status.hit_critical_skill -= (short)skill.Variable["SejiwuiPoint_cri"];
            actor.Buff.三转せーチウィークポイント = false;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
    }
}
