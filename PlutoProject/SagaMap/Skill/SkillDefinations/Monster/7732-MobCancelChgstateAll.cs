using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.SkillDefinations.Global;
using SagaLib;
using SagaMap;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations.Monster
{
    /// <summary>
    /// 驅逐
    /// </summary>
    public class MobCancelChgstateAll : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            int rate = 30;
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            List<Actor> affected = map.GetActorsArea(sActor, 100, false);
            foreach (Actor act in affected)
            {
                if (SkillHandler.Instance.CheckValidAttackTarget(sActor, act))
                {
                    if (SagaLib.Global.Random.Next(0, 99) < rate)
                    {
                        List<Addition> WillBeRemove = new List<Addition>();
                        
                        foreach (KeyValuePair<string, Addition> s in act.Status.Additions)
                        {
                            if (!(s.Value is DefaultPassiveSkill))
                            {
                                Addition addition = (Addition)s.Value;
                                WillBeRemove.Add(addition);
                            }
                                                
                        }
                        foreach (Addition i in WillBeRemove)
                        {
                            if (i.Activated)
                            {
                                SkillHandler.RemoveAddition(act, i);
                            }
                        }
                    }
                }
            }
        }
        #endregion
    }
}