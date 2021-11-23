using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaLib;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations
{
    public class S18502 : ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            if (!SkillHandler.Instance.CheckSkillCanCastForWeapon(pc, args))
                return -5;
            if (!Network.Client.MapClient.FromActorPC(pc).nextCombo.Contains(18502))
                return -2;
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            dActor = SkillHandler.Instance.GetdActor(sActor,args);
            if (dActor == null) return;

            float factor = 1.5f;
            switch(level)
            {
                case 1:
                    factor = 1.5f;
                    break;
                case 2:
                    factor = 1.8f;
                    break;
                case 3:
                    factor = 2.1f;
                    break;
            }
            SkillHandler.Instance.PhysicalAttack(sActor, dActor, args, Elements.Neutral, factor);
            SkillHandler.Instance.SetNextComboSkill(sActor, 18501);
        }
        #endregion
    }
}