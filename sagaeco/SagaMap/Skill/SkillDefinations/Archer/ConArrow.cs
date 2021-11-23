using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations.Archer
{
    public class ConArrow: ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
                if (!SkillHandler.Instance.CheckSkillCanCastForWeapon(pc,args))
                    return -5;
                else
                    return 0;
            
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            int combo = 2;
            float factor = 0;
            args.argType = SkillArg.ArgType.Attack;
            args.delayRate = 5f;
            switch (level)
            {
                case 1:
                    factor = 0.9f;
                    break;
                case 2:
                    factor = 1.0f;
                    break;
                case 3:
                    factor = 1.1f;
                    break;
                case 4:
                    factor = 1.2f;
                    break;
                case 5:
                    factor = 1.3f;
                    break;
            }

            List<Actor> target = new List<Actor>();
            //args.delayRate = 1f;
            for (int i = 0; i < combo; i++)
            {
                target.Add(dActor);
            }
            SkillHandler.Instance.PhysicalAttack(sActor, target, args, SagaLib.Elements.Neutral, factor);
            SagaMap.Skill.Additions.Global.DefaultBuff db = new Additions.Global.DefaultBuff(args.skill, sActor, "ConArrow", 5000);
            SkillHandler.ApplyAddition(sActor, db);
        }
        #endregion
    }
}
