using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;

namespace SagaMap.Skill.SkillDefinations.Stryder
{
    /// <summary>
    /// ピレッジアクト
    /// </summary>
    public class PillageAct : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            float factor = 0;
            factor = new float[] { 0, 0.5f, 3.0f, 8.0f, 13.0f, 18.0f }[level];

            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            List<Actor> affected = map.GetActorsArea(sActor, 200, false);
            List<Actor> realAffected = new List<Actor>();
            foreach (Actor act in affected)
            {
                if (SkillHandler.Instance.CheckValidAttackTarget(sActor, act))
                {
                    realAffected.Add(act);
                    if (act.type == ActorType.MOB)
                    {
                        ActorMob mob = (ActorMob)act;
                        //预留偷窃逻辑位置,等级越低成功率越高
                    }
                }
                
            }
            SkillHandler.Instance.PhysicalAttack(sActor, realAffected, args, sActor.WeaponElement, factor);
        }
        #endregion
    }
}
