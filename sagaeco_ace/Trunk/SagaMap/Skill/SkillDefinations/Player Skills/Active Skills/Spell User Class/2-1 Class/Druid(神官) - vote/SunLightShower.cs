using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
namespace SagaMap.Skill.SkillDefinations.Druid
{
    /// <summary>
    /// 溫暖陽光（サンライトシャワー）
    /// </summary>
    public class SunLightShower : ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            float factor = 2.0f;
            for (int i = 1; i <= level; i++)
            {
                factor += (float)(level / 10);
            }
            factor *= -1;

            if (sActor.Status.Additions.ContainsKey("Cardinal"))//3转10技提升治疗量
                factor = factor + sActor.Status.Cardinal_Rank;

            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            short range = 350;
            ActorPC sActorPC = (ActorPC)sActor;
            if (sActorPC.PossessionTarget != 0)
            {
                range = 150;
            }
            List<Actor> affected = map.GetActorsArea(sActor, range, true);
            List<Actor> realAffected = new List<Actor>();
            foreach (Actor act in affected)
            {
                if (act.type == ActorType.PC && !SkillHandler.Instance.CheckValidAttackTarget(sActor, act))
                {
                    realAffected.Add(act);
                }
            }
            SkillHandler.Instance.MagicAttack(sActor, realAffected, args, SagaLib.Elements.Holy, factor);
        }

        #endregion
    }
}
