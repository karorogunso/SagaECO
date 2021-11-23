using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaLib;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations
{
    public class S43100 : ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            if (pc.Status.Additions.ContainsKey("ContractCD")) return -30;
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            int lifetime = 300000;
            int cd = 30000;
            if (sActor.type == ActorType.PC)
            {             
                SkillHandler.RemoveAddition(sActor, "WeaponWind");
                SkillHandler.RemoveAddition(sActor, "WeaponEarth");
                SkillHandler.RemoveAddition(sActor, "WeaponFire");
                SkillHandler.RemoveAddition(sActor, "WeaponWater");
                SkillHandler.RemoveAddition(sActor, "WeaponDark");
                SkillHandler.RemoveAddition(sActor, "WeaponHoly");

                ElementContract ec = new ElementContract(args.skill, sActor, lifetime, Elements.Dark);
                SkillHandler.ApplyAddition(sActor, ec);
                WeaponDark w = new WeaponDark(args.skill, sActor, lifetime, 10);
                SkillHandler.ApplyAddition(sActor, w);
            }
            DefaultBuff skill = new DefaultBuff(args.skill, dActor, "ContractCD", cd);
            skill.OnAdditionStart += this.StartEvent;
            skill.OnAdditionEnd += this.EndEvent;
            SkillHandler.ApplyAddition(dActor, skill);
        }
        void StartEvent(Actor actor, DefaultBuff skill)
        {
        }
        void EndEvent(Actor actor, DefaultBuff skill)
        {
            if (actor.type == ActorType.PC)
                Network.Client.MapClient.FromActorPC((ActorPC)actor).SendSystemMessage("现在可以使用其他的『精灵契约』了。");
        }
        #endregion
    }
}
