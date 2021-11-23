
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
namespace SagaMap.Skill.SkillDefinations.Global
{
    /// <summary>
    /// 王的招換
    /// </summary>
    public class SumSlaveMob : ISkill
    {
        uint MobID;
        int Count;
        public SumSlaveMob(uint MobID)
        {
            this.MobID = MobID;
            this.Count = 8;//SagaLib.Global.Random.Next(8, 15);
        }
        public SumSlaveMob(uint MobID, int count)
        {
            this.MobID = MobID;
            this.Count = count;
        }
        #region ISkill Members
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            short[] xy;
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            if (sActor.Slave.Count == 0)
            {
                for (int i = 0; i < Count; i++)
                {
                    xy = map.GetRandomPosAroundActor(sActor);
                    var m = map.SpawnMob(MobID, xy[0], xy[1], 2500, sActor);
                    sActor.Slave.Add(m);
                }
            }
            else
            {
                int AliveCount = 0;
                for (int i = 0; i < sActor.Slave.Count; i++)
                {
                    ActorMob mob = (ActorMob)sActor.Slave[i];
                    if (!mob.Buff.Dead)
                    {
                        AliveCount++;
                    }
                }
                if (AliveCount <= 3)
                {
                    for (int i = 0; i < sActor.Slave.Count; i++)
                    {
                        ActorMob mob = (ActorMob)sActor.Slave[i];
                        if (mob.Buff.Dead)
                        {
                            xy = map.GetRandomPosAroundActor(sActor);
                            sActor.Slave[i] = map.SpawnMob(MobID, xy[0], xy[1], 2500, sActor);
                        }
                    }
                }
            }
        }
        #endregion
    }
}