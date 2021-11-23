
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
            if (sActor.Status.Additions.ContainsKey("铁壁CD"))
                return -30;
            else
                return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            int cd = 80000;
            SkillCD skill2 = new SkillCD(args.skill, sActor, "铁壁CD", cd);
            SkillHandler.ApplyAddition(sActor, skill2);
            args.dActor = sActor.ActorID;
            int[] life = { 0, 10000, 11000, 12000, 13000, 14000 };
            DefaultBuff skill = new DefaultBuff(args.skill, dActor, "铁壁", life[level]);
            skill.OnAdditionStart += this.StartEventHandler;
            skill.OnAdditionEnd += this.EndEventHandler;
            SkillHandler.ApplyAddition(dActor, skill);
        }
        void StartEventHandler(Actor actor, DefaultBuff skill)
        {
            actor.Buff.防御力上昇 = true;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
                                        
        }
        void EndEventHandler(Actor actor, DefaultBuff skill)
        {
            actor.Buff.防御力上昇 = false;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);       
        }
        #endregion
    }
}
