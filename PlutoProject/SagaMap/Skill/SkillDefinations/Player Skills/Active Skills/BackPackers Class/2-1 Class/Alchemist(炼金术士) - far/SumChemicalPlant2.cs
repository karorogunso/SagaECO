
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
namespace SagaMap.Skill.SkillDefinations.Alchemist
{
    /// <summary>
    /// 化工廠（ケミカルプラント）[接續技能]
    /// </summary>
    public class SumChemicalPlant2 : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {

            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            float factor = 1.0f + 3.0f * level;
            ActorMob mob = (ActorMob)sActor.Slave[0];
            List<Actor> actors = Manager.MapManager.Instance.GetMap(sActor.MapID).GetActorsArea(dActor, 150, true);
            List<Actor> affected = new List<Actor>();
            foreach (var item in actors)
            {
                if (SkillHandler.Instance.CheckValidAttackTarget(sActor, item))
                    affected.Add(item);
            }
            SkillHandler.Instance.PhysicalAttack(sActor, affected, args, sActor.WeaponElement, factor);
            sActor.Slave.Remove(mob);
            map.DeleteActor(mob);
            //float factor = 0.5f + 1.5f * level;
            //Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            //ActorMob mob = (ActorMob)sActor.Slave[0];
            //List<Actor> affected = map.GetActorsArea(dActor, 150, false);
            //List<Actor> realAffected = new List<Actor>();
            //foreach (Actor act in affected)
            //{
            //    if (SkillHandler.Instance.CheckValidAttackTarget(sActor, act))
            //    {
            //        realAffected.Add(act);
            //    }
            //}
            //SkillHandler.Instance.PhysicalAttack(sActor, realAffected, args, sActor.WeaponElement, factor);
            //sActor.Slave.Remove(mob);
            //map.DeleteActor(mob);
        }
        #endregion
    }
}