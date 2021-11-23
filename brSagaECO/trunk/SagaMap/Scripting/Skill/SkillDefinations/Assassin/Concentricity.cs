using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations.Assassin
{
    public class Concentricity : ISkill 
    {
        bool MobUse;
        public Concentricity()
        {
            this.MobUse = false;
        }
        public Concentricity(bool MobUse)
        {
            this.MobUse = MobUse;
        }
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            if (MobUse)
            {
                level = 5;
            }
            int lifetime = 0;
            switch (level)
            {
                case 1:
                    lifetime = 12000;
                    break;
                case 2:
                    lifetime = 15000;
                    break;
                case 3:
                    lifetime = 20000;
                    break;
                case 4:
                    lifetime = 25000;
                    break;
                case 5:
                    lifetime = 30000;
                    break;
            }
            DefaultBuff skill = new DefaultBuff(args.skill, dActor, "Concentricity", lifetime);
            skill.OnAdditionStart += this.StartEventHandler;
            skill.OnAdditionEnd += this.EndEventHandler;
            SkillHandler.ApplyAddition(dActor, skill);
        }
        void StartEventHandler(Actor actor, DefaultBuff skill)
        {
            actor.Buff.クリティカル率上昇 = true;
            actor.Status.cri_skill += 30;
        }
        void EndEventHandler(Actor actor, DefaultBuff skill)
        {
            //爆擊率
            actor.Buff.クリティカル率上昇 = false;
            actor.Status.cri_skill  -= 30;
        }
        #endregion
    }
}
