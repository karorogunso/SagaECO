using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations.ForceMaster
{
    public class BarrierShield : ISkill
    {
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            DefaultBuff skill = new DefaultBuff(args.skill, dActor, "BarrierShield", 600000);
            skill.OnAdditionStart += this.StartEventHandler;
            skill.OnAdditionEnd += this.EndEventHandler;
            if(dActor.Status.Additions.ContainsKey("BarrierShield"))
                dActor.Status.Additions.Remove("BarrierShield");
            else
            SkillHandler.ApplyAddition(dActor, skill);
        }
        void StartEventHandler(Actor actor, DefaultBuff skill)
        {
            int[] def_down = { 0, 100, 95, 85, 75, 60 };
            if(skill.Variable.ContainsKey("BarrierShield_Def"))
                skill.Variable.Remove("BarrierShield_Def");
            skill.Variable.Add("BarrierShield_Def",def_down[skill.skill.Level]);
            actor.Status.def_skill -= (short)def_down[skill.skill.Level];
            actor.Status.mdef_skill += 200;
            actor.Buff.三转魔法抗体 = true;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
        void EndEventHandler(Actor actor, DefaultBuff skill)
        {
            actor.Status.def_skill += (short)skill.Variable["BarrierShield_Def"];
            actor.Status.mdef_skill -= 200;
            actor.Buff.三转魔法抗体 = false;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
    }
}
