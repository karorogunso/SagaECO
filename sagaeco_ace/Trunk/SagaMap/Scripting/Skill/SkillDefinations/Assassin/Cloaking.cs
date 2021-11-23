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
            CloakingBuff skill = new CloakingBuff(args.skill, sActor,  lifetime );
            SkillHandler.ApplyAddition(sActor, skill);
        }
        public class CloakingBuff : DefaultBuff
        {
            public CloakingBuff(SagaDB.Skill.Skill skill, Actor actor, int lifetime)
                : base(skill, actor, "Cloaking", lifetime,1000)
            {
                this.OnAdditionStart += this.StartEvent;
                this.OnAdditionEnd += this.EndEvent;
                this.OnUpdate += this.TimerUpdate;
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
                    this.AdditionEnd();
                }
            }
        }
        
        #endregion
    }
}
