using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;

namespace SagaMap.Skill.SkillDefinations.Druid
{
    /// <summary>
    /// 範圍治癒（エリアヒール）
    /// </summary>
    public class AreaHeal :ISkill 
    {
        bool MobUse;
        public AreaHeal()
        {
            this.MobUse = false;
        }
        public AreaHeal(bool MobUse)
        {
            this.MobUse = MobUse;
        }
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            if (MobUse)
            {
                level = 5;
            }
            float factor = -(1f + 0.4f * level);
            if (sActor.Status.Additions.ContainsKey("Cardinal"))//3转10技提升治疗量
                factor = factor * sActor.Status.Cardinal_Rank;
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            List<Actor> affected = map.GetActorsArea(sActor, 100, true);
            List<Actor> realAffected = new List<Actor>();
            ActorPC pc = (ActorPC)sActor;
            foreach (Actor act in affected)
            {
                if (act.type== ActorType.PC || act.type == ActorType.PET || act.type == ActorType.SHADOW)
                {
                    if (pc.PossessionTarget == 0)
                    {
                        realAffected.Add(act);
                    }
                }
            }
            SkillHandler.Instance.MagicAttack(sActor, realAffected, args, SkillHandler.DefType.IgnoreAll, SagaLib.Elements.Holy, factor);
        }

        #endregion
    }
}
