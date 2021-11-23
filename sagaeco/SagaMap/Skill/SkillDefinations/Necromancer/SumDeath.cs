
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
using SagaMap.Mob;
using SagaMap.ActorEventHandlers;
namespace SagaMap.Skill.SkillDefinations.Necromancer
{
    /// <summary>
    /// 死神召喚（死神召喚）
    /// </summary>
    public class SumDeath : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            //if (sActor.Slave.Count >= 5)
            //{
            //    sActor.Slave[0].ClearTaskAddition();
            //    map.DeleteActor(sActor.Slave[0]);
            //    sActor.Slave.Remove(sActor.Slave[0]);
            //}
            uint[] MobID = { 0, 10250004, 10250005, 10250402, 10251001, 10251200 };
            ActorMob mob=map.SpawnMob(MobID[level], (short)(sActor.X + SagaLib.Global.Random.Next(1, 10))
                                     , (short)(sActor.Y + SagaLib.Global.Random.Next(1, 10))
                                     , 2500, sActor);
            sActor.Slave.Add(mob);
            if (level < 3)
            {
                int lifetime=8000;
                SumDeathBuff skill = new SumDeathBuff(args.skill, sActor, mob, lifetime);
                SkillHandler.ApplyAddition(sActor, skill);
            }
            uint[] NextSkillID = { 0, 3325, 3326, 3327, 3328, 3329 };
            MobEventHandler mobe = (MobEventHandler)mob.e;
            MobAI ai = mobe.AI;
            ai.CastSkill(NextSkillID[level], level, dActor);
            if (level == 1)
            {
                ai.CastSkill(NextSkillID[level], level, dActor);
                ai.CastSkill(NextSkillID[level], level, dActor);
            }
        }
        #endregion
        public class SumDeathBuff : DefaultBuff
        {
            Actor sActor;
            ActorMob mob;
            public SumDeathBuff(SagaDB.Skill.Skill skill, Actor sActor, ActorMob mob, int lifetime)
                : base(skill, sActor, "SumDeath"+SagaLib.Global.Random.Next(0,99).ToString(), lifetime)
            {
                this.OnAdditionStart += this.StartEvent;
                this.OnAdditionEnd += this.EndEvent;
                this.sActor = sActor;
                this.mob = mob;
            }

            void StartEvent(Actor actor, DefaultBuff skill)
            {
               
            }

            void EndEvent(Actor actor, DefaultBuff skill)
            {
                sActor.Slave.Remove(mob);
                mob.ClearTaskAddition();
                Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
                map.DeleteActor(mob);
            }
        }
    }
}