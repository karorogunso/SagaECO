
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
namespace SagaMap.Skill.SkillDefinations.Machinery
{
    /// <summary>
    /// 火花球（スパークボール）
    /// </summary>
    public class RobotSparkBall : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            ActorPet pet = SkillHandler.Instance.GetPet(sActor);
            if (pet == null)
            {
                return -53;//需回傳"需裝備寵物"
            }
            if (SkillHandler.Instance.CheckMobType(pet, "MACHINE_RIDE_ROBOT"))
            {
                return 0;
            }
            return -53;//需回傳"需裝備寵物"
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            float factor = 0.65f + 0.1f * level;
            short[] range = { 0, 600, 600, 600, 700, 700 };
            int[] times = { 0, 4, 4, 4, 4, 4 };
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            List<Actor> affected = map.GetActorsArea(sActor, range[level], true);
            List<Actor> realAffected = new List<Actor>();
            foreach (Actor act in affected)
            {
                if (SkillHandler.Instance.CheckValidAttackTarget(sActor, act))
                {
                    for (int i = 0; i < times[level]; i++)
                    {
                        realAffected.Add(act);
                    }
                }
            }
            if (realAffected.Count > 0)
            {
                List<Actor> finalAffected = new List<Actor>();
                for (int i = 0; i < realAffected.Count; i++)
                {
                    finalAffected.Add(realAffected[SagaLib.Global.Random.Next(0, realAffected.Count - 1)]);
                }
                SkillHandler.Instance.PhysicalAttack(sActor, finalAffected, args, SagaLib.Elements.Neutral, factor);
            }
        }
        #endregion
    }
}