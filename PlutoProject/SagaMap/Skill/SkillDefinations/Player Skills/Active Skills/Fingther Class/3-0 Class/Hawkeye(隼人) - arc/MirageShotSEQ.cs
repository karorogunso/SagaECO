using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.SkillDefinations.Global;
using SagaLib;
using SagaMap;

namespace SagaMap.Skill.SkillDefinations.Hawkeye
{
    class MirageShotSEQ : ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            if (sActor.type != ActorType.PC)
            {
                level = 3;
            }
            args.argType = SkillArg.ArgType.Attack;
            args.type = ATTACK_TYPE.STAB;
            List<Actor> dest = new List<Actor>();
            float factor = 0;
            int countMax = 0;
            switch (level)
            {
                case 1:
                    factor = 1.7f;
                    countMax = 4;
                    //this.period = 1000;
                    break;
                case 2:
                    factor = 2.15f;
                    countMax = 6;
                    //this.period = 800;
                    break;
                case 3:
                    factor = 2.5f;
                    countMax = 10;
                    //this.period = 700;
                    break;
            }
            for (int i = 0; i < countMax; i++)
                dest.Add(dActor);
            args.delayRate = 4.5f;
            SkillHandler.Instance.PhysicalAttack(sActor, dest, args, sActor.WeaponElement, factor);
        }

        #endregion
    }
}
