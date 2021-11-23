
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
using SagaMap.ActorEventHandlers;
namespace SagaMap.Skill.SkillDefinations.Necromancer
{
    /// <summary>
    /// 死神召喚（死神召喚）[接續技能]
    /// </summary>
    public class SumDeath5 : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            float factor = 6.0f;
            int rate = 50;
            int lifetime = 1000;
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            List<Actor> affected = map.GetActorsArea(dActor, 200, true);
            List<Actor> realAffected = new List<Actor>();
            foreach (Actor act in affected)
            {
                if (SkillHandler.Instance.CheckValidAttackTarget(sActor, act))
                {
                    if (SkillHandler.Instance.CanAdditionApply(sActor, act, SkillHandler.DefaultAdditions.Confuse, rate))
                    {
                        Confuse skill = new Confuse(args.skill, act, lifetime);
                        SkillHandler.ApplyAddition(act, skill);
                    }
                    realAffected.Add(act);
                }
            }
            SkillHandler.Instance.MagicAttack  (sActor, realAffected, args, SagaLib.Elements.Neutral , factor);
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