
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
namespace SagaMap.Skill.SkillDefinations.Druid
{
    /// <summary>
    /// 破曉之光（スパーナルレイ）
    /// </summary>
    public class LightHigeCircle : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            float factor = 2.3f + 0.2f * level;
            uint TURNUNDEAD_SkillID = 3078;
            ActorPC actorPC = (ActorPC)sActor;
            if (actorPC.Skills.ContainsKey(TURNUNDEAD_SkillID))
            {
                SagaDB.Skill.Skill TURNUNDEAD = actorPC.Skills[TURNUNDEAD_SkillID];
                factor += (0.5f + 0.5f * TURNUNDEAD.Level);
            }
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            List<Actor> affected = map.GetActorsArea(sActor, 300, false);
            List<Actor> realAffected = new List<Actor>();
            foreach (Actor act in affected)
            {
                if (act.type== ActorType.MOB)
                {
                    ActorMob m = (ActorMob)act;
                    if(m.BaseData.mobType.ToString().ToLower().IndexOf("undead")>-1)
                    {
                        realAffected.Add(act);
                    }
                }
            }
            SkillHandler.Instance.MagicAttack(sActor, realAffected, args, SagaLib.Elements.Holy, factor);
        }
        #endregion
    }
}