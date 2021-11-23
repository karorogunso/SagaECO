using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
using SagaMap.ActorEventHandlers;
namespace SagaMap.Skill.SkillDefinations.Gambler
{
    /// <summary>
    /// 艾卡卡（アルカナカード）[接續技能]
    /// </summary>
    public class SumArcanaCard4 : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            int lifetime = 5000;
            uint MobID = 10320006;//艾卡納王后
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            ActorMob mob=map.SpawnMob(MobID,
                (short)(sActor.X + SagaLib.Global.Random.Next(1, 11)),
                (short)(sActor.Y + SagaLib.Global.Random.Next(1, 11)),
                2500, sActor);
            MobEventHandler mh = (MobEventHandler)mob.e;
            mh.AI.Mode = new SagaMap.Mob.AIMode(0);
            SumArcanaCardBuff skill = new SumArcanaCardBuff(args.skill, sActor, mob, lifetime);
            SkillHandler.ApplyAddition(sActor, skill);
            SkillHandler.Instance.FixAttack(mob, sActor, args, SagaLib.Elements.Holy, -600f);
        }
        public class SumArcanaCardBuff : DefaultBuff
        {
            ActorMob mob;
            public SumArcanaCardBuff(SagaDB.Skill.Skill skill, Actor actor,ActorMob mob, int lifetime)
                : base(skill, actor, "SumArcanaCard", lifetime)
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
                Map map = Manager.MapManager.Instance.GetMap(actor.MapID);
                mob.ClearTaskAddition();
                map.DeleteActor(mob);
            }
        }
        #endregion
    }
}
