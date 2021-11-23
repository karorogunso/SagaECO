
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Fencer
{
    /// <summary>
    /// 重裝鎧化（ディフェンス・バースト）
    /// </summary>
    public class MobDefUpSelf : ISkill
    {
        bool MobUse;
        public MobDefUpSelf()
        {
            this.MobUse = false;
        }
        public MobDefUpSelf(bool MobUse)
        {
            this.MobUse = MobUse;
        }
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
                return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            args.dActor = sActor.ActorID;
            int[] life = { 0, 5000 };
            DefaultBuff skill = new DefaultBuff(args.skill, dActor, "铁壁", life[level]);
            skill.OnAdditionStart += this.StartEventHandler;
            skill.OnAdditionEnd += this.EndEventHandler;
            SkillHandler.ApplyAddition(dActor, skill);
        }
        void StartEventHandler(Actor actor, DefaultBuff skill)
        {
            actor.Buff.DefAddUp = true;
            actor.Buff.AvdMagicUp = true;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
                                        
        }
        void EndEventHandler(Actor actor, DefaultBuff skill)
        {
            actor.Buff.DefAddUp = false;
            actor.Buff.AvdMagicUp = false;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);       
        }
        #endregion
    }
}
