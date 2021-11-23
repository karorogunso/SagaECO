using SagaDB.Actor;
using SagaLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SagaMap.Skill.SkillDefinations.ForceMaster
{
    /// <summary>
    /// デストラクショングレアー (DeathTractionGlare)
    /// </summary>
    public class DeathTractionGlare : ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            float factor = 12.5f + 0.5f * level;
            if (sActor.type == ActorType.PC)
            {
                ActorPC pc = (ActorPC)sActor;
                if (pc.Skills.ContainsKey(3298))
                    factor += (2.0f * level - 0.5f);
            }

            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            List<Actor> actors = map.GetActorsArea(dActor, 200, true);
            List<Actor> affected = new List<Actor>();
            args.affectedActors.Clear();
            foreach (Actor i in actors)
            {
                if (SkillHandler.Instance.CheckValidAttackTarget(sActor, i))
                    affected.Add(i);
            }
            SkillHandler.Instance.MagicAttack(sActor, affected, args, Elements.Neutral, factor);
            args.autoCast.Add(SkillHandler.Instance.CreateAutoCastInfo(3429, level, 2000));
        }

        #endregion
    }
}
