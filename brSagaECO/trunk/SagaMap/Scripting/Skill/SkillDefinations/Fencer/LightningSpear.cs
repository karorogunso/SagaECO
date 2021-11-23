using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaMap.Skill.Additions.Global;
using SagaDB.Actor;

namespace SagaMap.Skill.SkillDefinations.Fencer
{
    /// <summary>
    /// ライトニングスピア
    /// </summary>
    public class LightningSpear:ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
                return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            float factor = 0;

            args.type = ATTACK_TYPE.STAB;

            factor = 0.9f + 0.25f * level;

            List<Actor> actors = new List<Actor>();
            actors.Add(dActor);
            SkillHandler.Instance.PhysicalAttack(sActor, actors, args, SkillHandler.DefType.Def, SagaLib.Elements.Neutral, 0, factor, false, 0, true);

            DefaultBuff skill = new DefaultBuff(args.skill, dActor, "刺击破甲", 5000);
            skill.OnAdditionStart += this.StartEventHandler;
            skill.OnAdditionEnd += this.EndEventHandler;
            SkillHandler.ApplyAddition(dActor, skill);
        }
        void StartEventHandler(Actor actor, DefaultBuff skill)
        {
            int level = skill.skill.Level;
            float value = 0.02f * level;

            short def = (short)(actor.Status.def * value);
            short mdef = (short)(actor.Status.mdef * value);

            skill.AddBuff("刺击破甲DEF", def);
            actor.Status.def_skill -= def;
            skill.AddBuff("刺击破甲MDEF", mdef);
            actor.Status.mdef_skill -= mdef;
        }
        void EndEventHandler(Actor actor, DefaultBuff skill)
        {
            actor.Status.def_skill += (short)skill.Variable["刺击破甲DEF"];
            actor.Status.mdef_skill += (short)skill.Variable["刺击破甲MDEF"];
        }
        #endregion
    }
}
