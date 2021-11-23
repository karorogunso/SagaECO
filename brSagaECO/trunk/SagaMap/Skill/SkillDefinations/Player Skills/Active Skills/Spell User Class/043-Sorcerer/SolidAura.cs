
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Sorcerer
{
    /// <summary>
    /// 固体光环（ソリッドオーラ）
    /// </summary>
    public class SolidAura : ISkill
    {
        KyrieUser user = KyrieUser.Player ;
        public enum KyrieUser
        {
            Player,
            Mob,
            Boss
        }
        public SolidAura()
        {
        }
        public SolidAura(KyrieUser user)
        {
            this.user = user;
        }
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            
            int lifetime = 7000 + 1000 * level;
            if (user == KyrieUser.Mob )
            {
                lifetime = 16000;
            }
            else if (user == KyrieUser.Boss)
            {
                lifetime = 60000;
            }
            DefaultBuff skill = new DefaultBuff(args.skill, dActor, "MobKyrie", lifetime);
            skill.OnAdditionStart += this.StartEventHandler;
            skill.OnAdditionEnd += this.EndEventHandler;
            SkillHandler.ApplyAddition(dActor, skill);
        }
        void StartEventHandler(Actor actor, DefaultBuff skill)
        {
            if (user == KyrieUser.Mob)
            {
                skill["MobKyrie"] = 7;
            }
            else if (user == KyrieUser.Boss)
            {
                skill["MobKyrie"] = 25;
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
