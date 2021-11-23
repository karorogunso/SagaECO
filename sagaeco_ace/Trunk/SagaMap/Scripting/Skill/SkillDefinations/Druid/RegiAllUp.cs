using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Druid
{
    /// <summary>
    /// 計劃者（プラーナ）
    /// </summary>
    public class RegiAllUp : ISkill 
    {
        #region ISkill Members
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            int lifetime = 40000 + 20000 * level;
            DefaultBuff skill = new DefaultBuff(args.skill, dActor, "RegiAllUp", lifetime);
            skill.OnAdditionStart += this.StartEventHandler;
            skill.OnAdditionEnd += this.EndEventHandler;
            SkillHandler.ApplyAddition(dActor, skill);
            string[] StatusNames = { "Sleep", "Poison", "Stun", "Silence", "Stone", "Confuse", "鈍足", "Frosen", "硬直" };
            foreach (string StatusName in StatusNames)
            {
                DefaultBuff skill2 = new DefaultBuff(args.skill, dActor, StatusName + "Regi", lifetime);
                skill2.OnAdditionStart += this.StartBuffEventHandler;
                skill2.OnAdditionEnd += this.EndBuffEventHandler;
                SkillHandler.ApplyAddition(dActor, skill2);
            }
        }
        void StartBuffEventHandler(Actor actor, DefaultBuff skill)
        {
        }
        void EndBuffEventHandler(Actor actor, DefaultBuff skill)
        {
        }
        void StartEventHandler(Actor actor, DefaultBuff skill)
        {
            actor.Buff.ConfuseResist = true ;
            actor.Buff.FaintResist =true ;
            actor.Buff.FrosenResist =true;
            actor.Buff.ParalysisResist =true;
            actor.Buff.PoisonResist=true;
            actor.Buff.SilenceResist=true;
            actor.Buff.SleepResist=true;
            actor.Buff.SpeedDownResist=true;
            actor.Buff.StoneResist=true;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
        void EndEventHandler(Actor actor, DefaultBuff skill)
        {
            actor.Buff.ConfuseResist = false ;
            actor.Buff.FaintResist =false ;
            actor.Buff.FrosenResist =false;
            actor.Buff.ParalysisResist =false;
            actor.Buff.PoisonResist=false;
            actor.Buff.SilenceResist=false;
            actor.Buff.SleepResist=false;
            actor.Buff.SpeedDownResist=false;
            actor.Buff.StoneResist=false;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
        #endregion
    }
}
