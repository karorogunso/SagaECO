using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;

namespace SagaMap.Skill.SkillDefinations.Scout
{
    /// <summary>
    /// 追擊要害
    /// </summary>
    public class VitalAttack:ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }


        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            float factor = 1.9f+0.3f*level;
            if (SagaLib.Global.Random.Next(0, 100) < 20)
            {
                if (sActor.type == ActorType.PC)
                {
                    ActorPC pc = (ActorPC)sActor;
                    EffectArg arg = new EffectArg();
                    arg.effectID = 8059;
                    arg.actorID = dActor.ActorID;

                    SkillArg add = new SkillArg();
                    //add.argType = SkillArg.ArgType.Actor_Active;
                    add = args.Clone();

                    
                    SkillHandler.Instance.PhysicalAttack(sActor, dActor, add, SagaLib.Elements.Neutral, 2.9f + 0.3f * level);
                    add.skill.BaseData.id = 100;
                    SagaMap.Network.Client.MapClient.FromActorPC(pc).map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.SHOW_EFFECT, arg, pc, true);
                    add.skill.BaseData.id = 2126;
                }

            }
            
            SkillHandler.Instance.PhysicalAttack(sActor, dActor, args, sActor.WeaponElement, factor);

        }

        #endregion
    }
}
