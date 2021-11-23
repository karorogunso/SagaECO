using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.SkillDefinations.Global;
using SagaLib;
using SagaMap;
using SagaMap.Skill.Additions.Global;


namespace SagaMap.Skill.SkillDefinations.Stryder
{
    class SkillForbid : ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            int[] lifetime = { 0, 3000, 4000, 4000, 4000, 5000 };
            if (dActor.type == ActorType.PC)
                lifetime = new int[] { 0, 3000, 4000, 4000, 4000, 5000 };
            if (dActor.type == ActorType.MOB)
                lifetime = new int[] { 0, 45000, 60000, 75000, 90000, 120000 };
            if(SkillHandler.Instance.isBossMob(sActor))
                lifetime = new int[] { 0, 2000, 2000, 2000, 2000, 2000};
            DefaultBuff skill = new DefaultBuff(args.skill, dActor, "SkillForbid", lifetime[level]);
            skill.OnAdditionStart += this.StartEventHandler;
            skill.OnAdditionEnd += this.EndEventHandler;
            SkillHandler.ApplyAddition(dActor, skill);
        }
        #endregion
        void StartEventHandler(Actor actor, DefaultBuff skill)
        {
            actor.Buff.三转禁言レストスキル = true;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
        void EndEventHandler(Actor actor, DefaultBuff skill)
        {
            actor.Buff.三转禁言レストスキル = false;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
    }
}
