
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Global
{
    /// <summary>
    /// 私が支えます（坚固光环）
    /// </summary>
    public class ISupport : ISkill
    {
        ISupportUser user = ISupportUser.Partner;
        public enum ISupportUser
        {
            Player,
            Mob,
            Boss,
            Partner
        }
        public ISupport()
        {
        }
        public ISupport(ISupportUser user)
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
            Map map = Manager.MapManager.Instance.GetMap(dActor.MapID);
            if (user == ISupportUser.Mob)
            {
                lifetime = 16000;
                dActor = sActor;
            }
            else if (user == ISupportUser.Boss)
            {
                lifetime = 60000;
                dActor = sActor;
            }
            else if (user == ISupportUser.Partner)
            {
                lifetime = 60000;
                List<Actor> affected = map.GetActorsArea(sActor, 500, false);
                Actor ActorlowHP = sActor;
                foreach (Actor act in affected)
                {
                    if(!SkillHandler.Instance.CheckValidAttackTarget(sActor,act))
                    {
                        int a = SagaLib.Global.Random.Next(0, 99);
                        if (a < 40)
                        {
                            dActor = act;
                        }
                    }
                    
                }

            }
            if (sActor.ActorID == dActor.ActorID)
            {
                EffectArg arg2 = new EffectArg();
                arg2.effectID = 5167;
                arg2.actorID = dActor.ActorID;
                map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.SHOW_EFFECT, arg2, dActor, true);
            }

            DefaultBuff skill = new DefaultBuff(args.skill, dActor, "MobKyrie", lifetime);
            skill.OnAdditionStart += this.StartEventHandler;
            skill.OnAdditionEnd += this.EndEventHandler;
            SkillHandler.ApplyAddition(dActor, skill);
        }
        void StartEventHandler(Actor actor, DefaultBuff skill)
        {
            if (user == ISupportUser.Mob)
            {
                skill["MobKyrie"] = 7;
            }
            else if (user == ISupportUser.Boss)
            {
                skill["MobKyrie"] = 25;
            }
            else if (user == ISupportUser.Partner)
            {
                skill["MobKyrie"] = 12;
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
