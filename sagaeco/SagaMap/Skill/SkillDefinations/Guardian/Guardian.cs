using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Guardian
{
    public class Guardian : ISkill
    {
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            int lifetime = (30 + level * 30) * 1000;
            int DEFUP = 50 + 5 * level;
            GuardianBuff skill = new GuardianBuff(args.skill, dActor, lifetime, DEFUP);
            SkillHandler.ApplyAddition(dActor, skill);

        }
        public class GuardianBuff : DefaultBuff
        {
            public GuardianBuff(SagaDB.Skill.Skill skill, Actor actor, int lifetime, int DEFUP)
                : base(skill, actor, "Guardian", lifetime)
            {
                this.OnAdditionStart += this.StartEvent;
                this.OnAdditionEnd += this.EndEvent;
                this["DEFUP"] = DEFUP;
            }
            void StartEvent(Actor actor, DefaultBuff skill)
            {
                int level = skill.skill.Level;
                int def_add = (int)(actor.Status.def_bs * 0.5f + 0.05f * level);
                int mdef_add = (int)(actor.Status.mdef_bs * 0.5f + 0.05f * level);
                if (skill.Variable.ContainsKey("Guardian_def"))
                    skill.Variable.Remove("Guardian_def");
                skill.Variable.Add("Guardian_def", def_add);
                if (skill.Variable.ContainsKey("Guardian_mdef"))
                    skill.Variable.Remove("Guardian_mdef");
                skill.Variable.Add("Guardian_mdef", mdef_add);
                actor.Status.def_add_skill += (short)def_add;
                actor.Status.mdef_add_skill += (short)mdef_add;
                actor.Buff.三转未知强力增强 = true;
                Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
            }
            void EndEvent(Actor actor, DefaultBuff skill)
            {
                actor.Status.def_add_skill -= (short)skill.Variable["Guardian_def"];
                actor.Status.mdef_add_skill -= (short)skill.Variable["Guardian_mdef"];
                actor.Buff.三转未知强力增强 = false;
                Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
            }
        }
    }
}
