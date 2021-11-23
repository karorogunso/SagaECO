using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaLib;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations
{
    public class S11102 : ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            if (SkillHandler.Instance.CheckSkillCanCastForWeapon(pc, args))
                return 0;
            return -5;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            dActor = SkillHandler.Instance.GetdActor(sActor,args);
            if (dActor == null) return;

            float factor = 2.35f + 0.65f * level;

            if (sActor.Status.Additions.ContainsKey("刀剑宗师"))
                factor += factor * sActor.TInt["刀剑宗师提升%"] / 100f;

            args.argType = SkillArg.ArgType.Attack;
            args.delayRate = 0.5f;
            SkillHandler.Instance.ShowEffectOnActor(dActor, 8059, sActor);

            SkillHandler.Instance.PhysicalAttack(sActor, dActor, args, Elements.Neutral, factor);
            SkillHandler.Instance.SetNextComboSkill(sActor, 11100);
            SkillHandler.Instance.SetNextComboSkill(sActor, 11101);
            SkillHandler.Instance.SetNextComboSkill(sActor, 11102);

            if (sActor.SkillCombo != null)
                sActor.SkillCombo.Add(3);

            sActor.EP += 300;
            if (sActor.EP > sActor.MaxEP) sActor.EP = sActor.MaxEP;
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, null, sActor, true);
        }
        #endregion
    }
}