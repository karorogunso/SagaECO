using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations
{
    public class S12109 : ISkill
    {
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            dActor = SkillHandler.Instance.GetdActor(sActor, args);
            if (dActor == null) return;
            SkillHandler.Instance.ChangdeWeapons(sActor, 0);

            float factor = 2.0f + 0.5f * level;
            float bloodsuck = 0.05f * level;

            if (sActor.Status.Additions.ContainsKey("暗樱"))
                factor += factor * sActor.TInt["暗樱提升%"] / 100f;

            List<Actor> da = new List<Actor>() { dActor };
            if (!sActor.Status.Additions.ContainsKey("Invisible") && !sActor.Status.Additions.ContainsKey("刺心回血CD"))
            {
                OtherAddition sk = new OtherAddition(null, sActor, "刺心回血CD", 3000);
                SkillHandler.ApplyAddition(sActor, sk);
                SkillHandler.Instance.ShowEffectOnActor(dActor, 5238, sActor);
                SkillHandler.Instance.PhysicalAttack(sActor, da, args, SkillHandler.DefType.Def, SagaLib.Elements.Neutral, 0, factor, false, bloodsuck, false);
            }
            else
                SkillHandler.Instance.PhysicalAttack(sActor, da, args, SkillHandler.DefType.Def, SagaLib.Elements.Neutral, 0, factor, false);
           
            float suckrate = 0.3f + 0.1f * level;

            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            uint epheal = 100;
            sActor.EP += epheal;
            if (sActor.EP > sActor.MaxEP) sActor.EP = sActor.MaxEP;
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, null, sActor, true);
            if (sActor.Status.Additions.ContainsKey("Invisible"))//隐身加成
            {
                SkillHandler.Instance.PhysicalAttack(sActor, da, args, SkillHandler.DefType.Def, SagaLib.Elements.Neutral, 0, factor, false, suckrate, false);
            }
            else
                SkillHandler.Instance.PhysicalAttack(sActor, da, args, SkillHandler.DefType.Def, SagaLib.Elements.Neutral, 0, factor, false);

        }
    }
}
