
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
namespace SagaMap.Skill.SkillDefinations.Global
{
    /// <summary>
    /// 招換怪物
    /// </summary>
    public class SumMob : ISkill
    {
        uint MobID;
        int Count;
        public SumMob(uint MobID)
        {
            this.MobID = MobID;
            this.Count = 1;//SagaLib.Global.Random.Next(8, 15);
        }
        public SumMob(uint MobID, int count)
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
                    if (mob.Buff.Dead)
                    {
                        AliveCount++;
                    }
                }
                if (AliveCount == 0)
                {
                    for (int i = 0; i < sActor.Slave.Count; i++)
                    {
                        ActorMob mob = (ActorMob)sActor.Slave[i];
                        if (mob.Buff.Dead)
                        {
                            xy = map.GetRandomPosAroundActor(sActor);
                            //mob.Buff.Clear();
                            ////mob.Buff = new Buff();
                            //mob.X = xy[0];
                            //mob.Y = xy[1];
                            //map.RegisterActor(mob);
                            //mob.HP = mob.MaxHP;
                            //mob.MP = mob.MaxMP;
                            //mob.SP = mob.MaxSP;
                            //mob.invisble = false;
                            //map.OnActorVisibilityChange(mob);
                            //map.SendVisibleActorsToActor(mob);
                            //((ActorEventHandlers.MobEventHandler)(mob.e)).AI.Start();
                            sActor.Slave[i] = map.SpawnMob(MobID, xy[0], xy[1], 2500,null);
                        }
                    }
                }
            }
        }
        #endregion
    }
}