using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations.Assassin
{
    /// <summary>
    /// 潛行（クローキング）
    /// </summary>
    public class Cloaking : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            int lifetime = 3600000;
            DefaultBuff skill = new DefaultBuff(args.skill, sActor, "Cloaking", lifetime);
            skill.OnAdditionStart += this.StartEvent;
            skill.OnAdditionEnd += this.EndEvent;
            skill.OnUpdate += this.TimerUpdate;
            SkillHandler.ApplyAddition(sActor, skill);
        }
        void StartEvent(Actor actor, DefaultBuff skill)
        {
            //隱藏自己
            actor.Buff.Transparent = true;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }

        void EndEvent(Actor actor, DefaultBuff skill)
        {

            //顯示自己
            actor.Buff.Transparent = false;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
        void TimerUpdate(Actor actor, DefaultBuff skill)
        {
            
            if (actor.type != ActorType.PC)
            {
                return;
            }
            ActorPC dActorPC = (ActorPC)actor;

            
            if (actor.SP > 0 && dActorPC.Motion != SagaLib.MotionType.SIT)
            {
                
                Map map = Manager.MapManager.Instance.GetMap(actor.MapID);
                actor.SP -= 1;
                map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, null, actor, true);
            }
            else
            {
                //顯示自己
                skill.AdditionEnd();
            }
        }
        #endregion
    }
}
