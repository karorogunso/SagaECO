using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaMap.Skill.Additions.Global;
using SagaDB.Actor;
namespace SagaMap.Skill.SkillDefinations.Event
{
    /// <summary>
    /// スキル石?救援邀請（スキル石?ソリッドオーラ）
    /// </summary>
    public class Kyrie : ISkill
    {
        bool MobUse = false;
        public Kyrie()
        {
        }
        public Kyrie(bool MobUse)
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
            level = 5;
            int lifetime = 7000 + 1000 * level;
            if (MobUse)
            {
                lifetime = 16000;
            }
            DefaultBuff skill = new DefaultBuff(args.skill, dActor, "MobKyrie", lifetime);
            skill.OnAdditionStart += this.StartEventHandler;
            skill.OnAdditionEnd += this.EndEventHandler;
            SkillHandler.ApplyAddition(dActor, skill);
        }
        void StartEventHandler(Actor actor, DefaultBuff skill)
        {
            if (MobUse)
            {
                skill["MobKyrie"] = 7;
            }
            else
            {
                int[] times = { 0, 5, 5, 6, 6, 7 };
                skill["MobKyrie"] = times[skill.skill.Level];
                if (actor.type == ActorType.PC)
                {
                    ActorPC pc = (ActorPC)actor;
                    Network.Client.MapClient.FromActorPC(pc).SendSystemMessage(string.Format(Manager.LocalManager.Instance.Strings.SKILL_STATUS_ENTER, skill.skill.Name));
                }
            }
        }
        void EndEventHandler(Actor actor, DefaultBuff skill)
        {
            if (actor.type == ActorType.PC)
            {
                ActorPC pc = (ActorPC)actor;
                Network.Client.MapClient.FromActorPC(pc).SendSystemMessage(string.Format(Manager.LocalManager.Instance.Strings.SKILL_STATUS_LEAVE, skill.skill.Name));
            }
        }
        #endregion
    }
}
