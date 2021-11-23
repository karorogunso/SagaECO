
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Sage
{
    /// <summary>
    /// 活幽靈（ドッペルゲンガー）
    /// </summary>
    public class SumDop : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            int lifetime = 300000;
            SumDopBuff skill = new SumDopBuff(args.skill, sActor, lifetime);
            SkillHandler.ApplyAddition(sActor, skill);
        }
        public class SumDopBuff : DefaultBuff
        {
            private ActorShadow shadow;
            public SumDopBuff(SagaDB.Skill.Skill skill, Actor actor, int lifetime)
                : base(skill, actor, "SumDop", lifetime)
            {
                this.OnAdditionStart += this.StartEvent;
                this.OnAdditionEnd += this.EndEvent;
            }

            void StartEvent(Actor actor, DefaultBuff skill)
            {
                shadow = SummonMe((ActorPC)actor, skill.skill.Level);
            }

            void EndEvent(Actor actor, DefaultBuff skill)
            {
                Map map = Manager.MapManager.Instance.GetMap(actor.MapID);
                ActorEventHandlers.PetEventHandler eh = (SagaMap.ActorEventHandlers.PetEventHandler)shadow.e;
                eh.AI.Pause();
                shadow.ClearTaskAddition();
                map.DeleteActor(shadow);
            }
            public ActorShadow SummonMe(ActorPC pc, byte level)
            {
                ActorShadow actor = new ActorShadow(pc);
                Map map = Manager.MapManager.Instance.GetMap(pc.MapID);
                actor.Name = pc.Name;
                actor.MapID = pc.MapID;
                actor.X = pc.X;
                actor.Y = pc.Y;
                actor.MaxHP = (uint)(pc.MaxHP * (0.1f + 0.2f * level));
                actor.HP = actor.MaxHP;
                actor.Status.max_matk_skill += (short)(actor.Status.max_matk * 1.5f);
                actor.Status.min_matk_skill += (short)(actor.Status.min_matk * 1.5f);
                actor.Speed = pc.Speed;
                actor.BaseData.mobSize = 1.5f;
                ActorEventHandlers.PetEventHandler eh = new ActorEventHandlers.PetEventHandler(actor);
                actor.e = eh;

                eh.AI.Mode = new SagaMap.Mob.AIMode(1);
                switch (skill.Level)
                {
                    case 1:
                        eh.AI.Mode.EventAttacking.Add(3281, 100);	//魔法衝擊波
                        break;
                    case 2:
                        eh.AI.Mode.EventAttacking.Add(3281, 50);	//魔法衝擊波
                        eh.AI.Mode.EventAttacking.Add(3127, 50);	//魔力放出
                        break;
                    case 3:
                        eh.AI.Mode.EventAttacking.Add(3281, 30);	//魔法衝擊波
                        eh.AI.Mode.EventAttacking.Add(3127, 30);	//魔力放出
                        eh.AI.Mode.EventAttacking.Add(3291, 40);	//燃燒生命
                        break;
                }
                
                eh.AI.Mode.EventAttackingSkillRate = 100;
                eh.AI.Master = pc;
                map.RegisterActor(actor);
                actor.invisble = false;
                map.OnActorVisibilityChange(actor);
                map.SendVisibleActorsToActor(actor);
                eh.AI.Start();
                return actor;
            }
        }
        
        #endregion
    }
}
