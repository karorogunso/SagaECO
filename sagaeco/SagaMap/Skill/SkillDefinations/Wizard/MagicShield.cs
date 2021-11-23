using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations.Wizard
{
    public class MagicShield:ISkill
    {
        bool MobUse;
        public MagicShield()
        {
            this.MobUse = false;
        }
        public MagicShield(bool MobUse)
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
            if (MobUse)
            {
                level = 5;
            }
            DefaultBuff skill = new DefaultBuff(args.skill, sActor, "魔力屏障", 20000);
            skill.OnAdditionStart += this.StartEventHandler;
            skill.OnAdditionEnd += this.EndEventHandler;
            SkillHandler.ApplyAddition(sActor, skill);
        }

        void StartEventHandler(Actor actor, DefaultBuff skill)
        {
            int[] times = { 0, 1, 2, 3, 4, 5 };
            skill["魔力屏障"] = times[skill.skill.Level];
            SagaMap.Network.Client.MapClient.FromActorPC((ActorPC)actor).SendSystemMessage("进入魔力屏障状态");
        }

        void EndEventHandler(Actor actor, DefaultBuff skill)
        {
            SagaMap.Network.Client.MapClient.FromActorPC((ActorPC)actor).SendSystemMessage("魔力屏障状态消失");
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