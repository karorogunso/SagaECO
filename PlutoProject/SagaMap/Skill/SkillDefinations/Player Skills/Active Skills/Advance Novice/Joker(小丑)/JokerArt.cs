using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;

namespace SagaMap.Skill.SkillDefinations.Global
{
    /// <summary>
    /// 小丑的寂寞
    /// </summary>
    public class JokerArt : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            float factor = new float[] { 0, 15.0f, 23.0f, 31.0f, 39.0f, 47.0f }[level];

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
            int random = SagaLib.Global.Random.Next(0, 9999);
            if (random <= 7000)
            {
                SkillHandler.Instance.PhysicalAttack(sActor, sActor, args, sActor.WeaponElement, 1000.0f);
            }
            else
            {
                int dmg = (int)(sActor.HP - 1);
                SkillHandler.Instance.CauseDamage(sActor, sActor, dmg);
                SkillHandler.Instance.ShowVessel(dActor, dmg);
            }
            SkillHandler.Instance.PhysicalAttack(sActor, realAffected, args, sActor.WeaponElement, factor);
        }
        #endregion
    }
}
