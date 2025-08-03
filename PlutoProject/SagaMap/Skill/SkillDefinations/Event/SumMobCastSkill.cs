
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
using SagaMap.ActorEventHandlers;
namespace SagaMap.Skill.SkillDefinations.Event
{
    /// <summary>
    /// 招喚怪物並且使用技能
    /// </summary>
    public class SumMobCastSkill : ISkill
    {
        uint NextSkillID;
        uint MobID;
        public SumMobCastSkill(uint NextSkillID, uint MobID)
        {
            this.NextSkillID = NextSkillID;
            this.MobID = MobID;
        }
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            int lifetime = 5000;
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            short[] xy = map.GetRandomPosAroundActor(sActor);
            ActorMob mob = map.SpawnMob(MobID, SagaLib.Global.PosX8to16(args.x,map.Width),SagaLib.Global.PosY8to16( args.y,map.Height) , 100, sActor);
            SumMobCastSkillBuff skill = new SumMobCastSkillBuff(args.skill, dActor, mob, lifetime);
            SkillHandler.ApplyAddition(dActor, skill);
            MobEventHandler mh = (MobEventHandler)mob.e;
            mh.AI.CastSkill(NextSkillID, 1, sActor);
        }
        #endregion
        public class SumMobCastSkillBuff : DefaultBuff
        {
            ActorMob mob;
            public SumMobCastSkillBuff(SagaDB.Skill.Skill skill, Actor actor,ActorMob mob, int lifetime)
                : base(skill, actor, "SumMobCastSkillBuff", lifetime)
            {
                this.OnAdditionStart += this.StartEvent;
                this.OnAdditionEnd += this.EndEvent;
                this.mob = mob;
            }
            void StartEvent(Actor actor, DefaultBuff skill)
            {
            }
            void EndEvent(Actor actor, DefaultBuff skill)
            {
                mob.ClearTaskAddition();
                Map map = Manager.MapManager.Instance.GetMap(mob.MapID);
                map.DeleteActor(mob);
            }
        }
    }
}