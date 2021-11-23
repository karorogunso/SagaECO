using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations.Wizard
{
    public class EnergyShield:ISkill
    {
         bool MobUse;
        public EnergyShield()
        {
            this.MobUse = false;
        }
        public EnergyShield(bool MobUse)
        {
            this.MobUse = MobUse;
        }
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {

            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            DefaultBuff skill = new DefaultBuff(args.skill, sActor, "能量加护", 20000);
            skill.OnAdditionStart += this.StartEventHandler;
            skill.OnAdditionEnd += this.EndEventHandler;
            SkillHandler.ApplyAddition(sActor, skill);
        }

        void StartEventHandler(Actor actor, DefaultBuff skill)
        {
            int[] times = { 0, 1, 2, 3, 4, 5 };
            skill["能量加护"] = times[skill.skill.Level];
            SagaMap.Network.Client.MapClient.FromActorPC((ActorPC)actor).SendSystemMessage("进入能量加护状态");
        }

        void EndEventHandler(Actor actor, DefaultBuff skill)
        {
            SagaMap.Network.Client.MapClient.FromActorPC((ActorPC)actor).SendSystemMessage("能量加护状态消失");
        }

        public void RemoveAddition(Actor actor, String additionName)
        {
            if (actor.Status.Additions.ContainsKey(additionName))
            {
                Addition addition = actor.Status.Additions[additionName];
                actor.Status.Additions.Remove(additionName);
                if (addition.Activated)
                {
                    addition.AdditionEnd();
                }
                addition.Activated = false;
            }
        }
        #endregion
    }
}