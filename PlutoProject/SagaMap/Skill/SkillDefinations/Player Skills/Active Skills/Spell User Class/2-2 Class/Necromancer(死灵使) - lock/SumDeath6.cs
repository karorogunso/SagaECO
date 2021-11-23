
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
using SagaMap.Mob;
using SagaMap.ActorEventHandlers;
namespace SagaMap.Skill.SkillDefinations.Necromancer
{
    /// <summary>
    /// 死神召喚（死神召喚）[接續技能]
    /// </summary>
    public class SumDeath6 : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            float factor = 7.0f;
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            List<Actor> affected = map.GetActorsArea(dActor, 300, true);
            List<Actor> realAffected = new List<Actor>();
            foreach (Actor act in affected)
            {
                if (SkillHandler.Instance.CheckValidAttackTarget(sActor, act))
                {
                    realAffected.Add(act);
                }
            }
            SkillHandler.Instance.MagicAttack (sActor, realAffected, args, SagaLib.Elements.Dark, factor);
            if (sActor.type == ActorType.MOB)
            {
                try
                {
                    ActorMob mob = (ActorMob)sActor;
                    MobEventHandler mobe = (MobEventHandler)mob.e;
                    Actor Master = mobe.AI.Master;
                    Master.Slave.Remove(mob);
                    mob.ClearTaskAddition();
                    map.DeleteActor(mob);
                }
                catch (Exception)
                {
                }
            }
        }
        #endregion
    }
}