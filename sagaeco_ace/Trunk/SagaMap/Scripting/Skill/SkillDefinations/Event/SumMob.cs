using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
using SagaLib;
namespace SagaMap.Skill.SkillDefinations.Event
{
    /// <summary>
    /// MOB召喚
    /// </summary>
    public class SumMob : ISkill
    {
        private uint MobID;
        public SumMob(uint MobID)
        {
            this.MobID = MobID;
        }
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            ActorMob mob = map.SpawnMob(MobID, (short)(sActor.X + SagaLib.Global.Random.Next(1, 10))
                                             , (short)(sActor.Y + SagaLib.Global.Random.Next(1, 10))
                                             , 50, sActor);
            sActor.Slave.Add(mob);
        }
        #endregion
    }
}



                                                          