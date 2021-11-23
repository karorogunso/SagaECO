using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;

namespace SagaMap.Skill.SkillDefinations.Elementaler
{
    /// <summary>
    /// ファシライズ
    /// </summary>
    class EarthNum : ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            int rate = 0;
            int lifetime = 0;
            SkillHandler.Instance.MagicAttack(sActor, dActor, args, SagaLib.Elements.Earth, 0);
            args.flag[0] = SagaLib.AttackFlag.NONE;
            switch (level)
            {
                case 1:
                    rate = 20;
                    lifetime = 4000;
                    break;
                case 2:
                    rate = 30;
                    lifetime = 4500;
                    break;
                case 3:
                    rate = 40;
                    lifetime = 5000;
                    break;
                case 4:
                    rate = 55;
                    lifetime = 5500;
                    break;
                case 5:
                    rate = 70;
                    lifetime = 6000;
                    break;
            }
            float rateModify = 0F;//The higher value of elemet the higher rate of freezen possibility.
            int element_dActor = 0;
            element_dActor = dActor.Elements[SagaLib.Elements.Earth];
            if (element_dActor > 1 && element_dActor <= 100)
                rateModify = 0.25F;
            if (element_dActor > 100 && element_dActor <= 200)
                rateModify = 0.5F;
            if (element_dActor > 200 && element_dActor <= 300)
                rateModify = 0.75F;
            if (element_dActor > 300)
                rateModify = 0.9F;
            rate = (int)(100 - (100 - rate) * (1 - rateModify));//If dActor attach water element, the rate increase. 
            if (SagaLib.Global.Random.Next(0, 99) < rate)
            {
                Additions.Global.Stone skill = new SagaMap.Skill.Additions.Global.Stone(args.skill, dActor, lifetime);
                SkillHandler.ApplyAddition(dActor, skill);
            }
        }
        #endregion
    }
}
