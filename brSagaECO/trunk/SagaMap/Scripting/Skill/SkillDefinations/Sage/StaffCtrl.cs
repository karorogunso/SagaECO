
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Sage
{
    /// <summary>
    /// 活化杖（リビングスタッフ）
    /// </summary>
    public class StaffCtrl : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            int lifetime =  20000 * level;
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            ActorMob mob = map.SpawnMob(26330000, SagaLib.Global.PosX8to16(args.x, map.Width)
                                               , SagaLib.Global.PosY8to16(args.y, map.Height)
                                               , 2500, sActor);
            ActorEventHandlers.MobEventHandler eE = (ActorEventHandlers.MobEventHandler)mob.e;
            eE.AI.Mode = new Mob.AIMode(1);
            eE.AI.Mode.EventAttacking.Add(3281, 100);	//魔法衝擊波
            eE.AI.Mode.EventAttackingSkillRate = 100;
            StaffCtrlBuff skill = new StaffCtrlBuff(mob,args.skill, sActor, lifetime);
            SkillHandler.ApplyAddition(sActor, skill);
        }
        public class StaffCtrlBuff : DefaultBuff
        {
            private Actor mob;
            public StaffCtrlBuff(Actor mob,SagaDB.Skill.Skill skill, Actor actor, int lifetime)
                : base(skill, actor, "StaffCtrl", lifetime)
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
                if (mob != null)
                {
                    mob.ClearTaskAddition();
                    map.DeleteActor(mob);
                }                
            }
        }
        #endregion
    }
}
                             

