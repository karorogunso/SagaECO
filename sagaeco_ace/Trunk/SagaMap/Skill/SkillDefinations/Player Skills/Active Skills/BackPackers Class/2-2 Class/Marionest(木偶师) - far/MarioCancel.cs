
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
using SagaMap.Network.Client;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Marionest
{
    /// <summary>
    /// 釋放活動木偶（マリオネットディスチャージ）
    /// </summary>
    public class MarioCancel : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            uint PUPPET_SkillID = 2371;
            float factor = 1.7f + 0.3f * level;
            ActorPC sActorPC = (ActorPC)sActor;
            if (sActorPC.Skills2.ContainsKey(PUPPET_SkillID))
            {
                factor += 0.3f * sActorPC.Skills2[PUPPET_SkillID].Level;
            }
            if (sActorPC.SkillsReserve.ContainsKey(PUPPET_SkillID))
            {
                factor += 0.3f * sActorPC.SkillsReserve[PUPPET_SkillID].Level;
            }

            SkillHandler.Instance.MagicAttack(sActor, dActor, args, SagaLib.Elements.Neutral, factor);
            int rate = 30 + 10 * level;
            if (SagaLib.Global.Random.Next(0, 99) < rate)
            {
                if (dActor.type == ActorType.PC)
                {
                    ActorPC dActorPC = (ActorPC)dActor;
                    if (dActorPC.Marionette != null)
                    {
                        MapClient.FromActorPC(dActorPC).MarionetteDeactivate();
                    }
                }
            }
            rate = 40 + 5 * level;
            if (dActor.type == ActorType.MOB)
            {
                if (SkillHandler.Instance.CanAdditionApply(sActor, dActor, SkillHandler.DefaultAdditions.硬直, rate))
                {
                    Stiff skill = new Stiff(args.skill, sActor, 4000 + 1000 * level);
                    SkillHandler.ApplyAddition(dActor, skill);
                }
            }   
        }
        #endregion
    }
}