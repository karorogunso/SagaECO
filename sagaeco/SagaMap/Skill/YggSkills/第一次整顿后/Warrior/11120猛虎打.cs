using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaMap.Skill.Additions.Global;
using SagaDB.Actor;
using SagaLib;
using SagaMap.Mob;

namespace SagaMap.Skill.SkillDefinations
{
    public class S11120 : ISkill
    {
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            if (pc.Status.Additions.ContainsKey("猛虎打CD")) return -30;
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            dActor = SkillHandler.Instance.GetdActor(sActor, args);
            if (dActor == null) return;

            float factor = 3.2f + 1.8f * level;


            if (level == 2)
                factor = 10f;

            if (sActor.Status.Additions.ContainsKey("重锤火花"))
                factor += factor * sActor.TInt["重锤火花提升%"] / 100f;


            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);

            if (level != 2)
            {
                args.argType = SkillArg.ArgType.Attack;
                args.type = ATTACK_TYPE.BLOW;
                args.delayRate = 1f;
                OtherAddition cd = new OtherAddition(null, sActor, "猛虎打CD", 1000);
                SkillHandler.ApplyAddition(sActor, cd);
            }
            SkillHandler.Instance.ShowEffectOnActor(dActor, 5376, sActor);

            List<Actor> targets = map.GetActorsArea(dActor, 200, false);
            List<Actor> dest = new List<Actor>();
            foreach (var item in targets)
            {
                if (SkillHandler.Instance.CheckValidAttackTarget(sActor, item))
                    SkillHandler.Instance.DoDamage(true, sActor, item, null, SkillHandler.DefType.Def, Elements.Neutral, 0, factor);
            }

            if (dActor.Status.Additions.ContainsKey("Stun"))
            {
                factor *= 1.5f;
                SkillHandler.RemoveAddition(dActor, "Stun");
            }

            SkillHandler.Instance.PhysicalAttack(sActor, new List<Actor>() { dActor }, args, SkillHandler.DefType.Def, Elements.Neutral, 0, factor, true, 0, true);

            SkillHandler.Instance.SetNextComboSkill(sActor, 11121);
            SkillHandler.Instance.SetNextComboSkill(sActor, 11122);
            SkillHandler.Instance.SetNextComboSkill(sActor, 11123);
            sActor.EP += 700;
            if (sActor.EP > sActor.MaxEP) sActor.EP = sActor.MaxEP;
            Manager.MapManager.Instance.GetMap(sActor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, null, sActor, true);
        }
    }
}
