
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Marionest
{
    /// <summary>
    /// 調教馴化（モンスターテイミング）
    /// </summary>
    public class EnemyCharming : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            if (dActor.type == ActorType.PC)
            {
                return 0;
            }
            else if (dActor.type == ActorType.MOB)
            {
                ActorMob dActorMob = (ActorMob)dActor;
                if (!SkillHandler.Instance.isBossMob(dActorMob))
                {
                    return 0;
                }
            }
            return -13;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            int rate = 0;
            if (dActor.type == ActorType.PC)
            {
                rate = 10 + 10 * level;
                if (SagaLib.Global.Random.Next(0, 99) < rate)
                {
                    dActor.HP = 0;
                    dActor.e.OnDie();
                    args.affectedActors.Add(dActor);
                    args.Init();
                    args.flag[0] = SagaLib.AttackFlag.DIE;
                }
            }
            else if (dActor.type == ActorType.MOB)
            {
                rate = 40 + 10 * level;
                if (SagaLib.Global.Random.Next(0, 99) < rate)
                {
                    Map map = Manager.MapManager.Instance.GetMap(dActor.MapID);
                    ActorMob dActorMob = (ActorMob)dActor;
                    uint MobID = dActorMob.BaseData.id;
                    short x = dActor.X;
                    short y = dActor.Y;
                    map.DeleteActor(dActor);
                    ActorMob mob = map.SpawnMob(MobID, x, y, 2500, sActor);
                    EnemyCharmingBuff skill = new EnemyCharmingBuff(args.skill, sActor, mob, 600000);
                    SkillHandler.ApplyAddition(sActor, skill);
                }
            }
        }
        public class EnemyCharmingBuff : DefaultBuff
        {
            private ActorMob mob;
            public EnemyCharmingBuff(SagaDB.Skill.Skill skill, Actor actor, ActorMob mob, int lifetime)
                : base(skill, actor, "EnemyCharming", lifetime)
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