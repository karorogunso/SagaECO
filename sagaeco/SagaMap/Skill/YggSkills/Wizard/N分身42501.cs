using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaLib;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations
{
    public class S42501 : ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            if (pc.MapID == 10049000 || pc.MapID == 10050000 || pc.MapID == 10051000 || pc.MapID == 10069001) return -2;
            if (pc.EP < 2000) return -2;
            //if (pc.Status.Additions.ContainsKey("属性契约")) return -2;
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);

            sActor.EP -= 2000;
            if (sActor.EP > sActor.MaxEP) sActor.EP = sActor.MaxEP;
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, null, sActor, true);

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
                actor.MaxHP = 3000;
                actor.HP = actor.MaxHP;
                actor.Speed = 300;
                actor.range = 1.5f;
                ActorEventHandlers.PetEventHandler eh = new ActorEventHandlers.PetEventHandler(actor);
                actor.e = eh;

                eh.AI.Mode = new SagaMap.Mob.AIMode(0);
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
