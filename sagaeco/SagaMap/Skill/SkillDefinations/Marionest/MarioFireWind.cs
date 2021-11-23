
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
namespace SagaMap.Skill.SkillDefinations.Marionest
{
    /// <summary>
    /// 烈焰暴風（ブレイジングトルネード）
    /// </summary>
    public class MarioFireWind : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            ActorPC sActorPC = (ActorPC)sActor;
            if (sActorPC.Marionette != null)
            {
                float factor = 2.1f + 0.3f * level;
                Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
                List<Actor> affected = map.GetActorsArea(sActor, 400, false);
                List<Actor> realAffected = new List<Actor>();
                foreach (Actor act in affected)
                {
                    if (SkillHandler.Instance.CheckValidAttackTarget(sActor, act))
                    {
                        realAffected.Add(act);
                    }
                }                
                SkillHandler.Instance.MagicAttack(sActor, realAffected, args, SagaLib.Elements.Fire, factor);
            }
            
        }
        #endregion
    }
}
