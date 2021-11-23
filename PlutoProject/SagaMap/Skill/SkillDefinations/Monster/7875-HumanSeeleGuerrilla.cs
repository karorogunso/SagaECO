using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.SkillDefinations.Global;
using SagaLib;
using SagaMap;

namespace SagaMap.Skill.SkillDefinations.Monster
{
    /// <summary>
    /// DEM龙用灵魂审判
    /// </summary>
    public class HumanSeeleGuerrilla : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            int rate = 50;//也许设置概率改为50%更合适?
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            List<Actor> affected = map.GetActorsArea(sActor, 200, false);
            List<Actor> realAffected = new List<Actor>();
            foreach (Actor act in affected)
            {
                if (SkillHandler.Instance.CheckValidAttackTarget(sActor, act))
                {
                    realAffected.Add(act);
                    if (SkillHandler.Instance.CanAdditionApply(sActor, act, SkillHandler.DefaultAdditions.Silence, rate))
                    {
                        Additions.Global.Silence skill = new SagaMap.Skill.Additions.Global.Silence(args.skill, act, 3000);
                        SkillHandler.ApplyAddition(act, skill);
                    }
                    if (act.type == ActorType.PC)
                    {
                        if (SagaLib.Global.Random.Next(0, 99) < rate)
                        {
                            SkillHandler.Instance.PossessionCancel((ActorPC)act, SagaLib.PossessionPosition.NONE);
                        }
                        
                    }
                }
            }
        }
        #endregion
    }
}