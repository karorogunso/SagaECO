using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations.Swordman
{
    /// <summary>
    /// 防衛板
    /// </summary>
    public class Parry:ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            if (CheckPossible(pc))
                return 0;
            else
                return -5;
        }

        bool CheckPossible(Actor sActor)
        {
            if (sActor.type == ActorType.PC)
            {
                ActorPC pc = (ActorPC)sActor;
                if (pc.Inventory.Equipments.ContainsKey(SagaDB.Item.EnumEquipSlot.RIGHT_HAND) || pc.Inventory.GetContainer(SagaDB.Item.ContainerType.RIGHT_HAND2).Count > 0)
                    return true;
                else
                    return false;
            }
            else
                return true;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            args.dActor = 0;//不显示效果
            if (CheckPossible(sActor))
            {
                int lifetime = 0;
                lifetime = 6 + level;
                if (level == 5)
                    lifetime++;
                lifetime *= 1000;
                DefaultBuff skill = new DefaultBuff(args.skill, dActor, "Parry", lifetime);
                skill.OnAdditionStart += this.StartEventHandler;
                skill.OnAdditionEnd += this.EndEventHandler;
                SkillHandler.ApplyAddition(dActor, skill);
            }
        }

        void StartEventHandler(Actor actor, DefaultBuff skill)
        {
            if (actor.type == ActorType.PC)
            {
                ActorPC pc = (ActorPC)actor;
                Network.Client.MapClient.FromActorPC(pc).SendSystemMessage(string.Format(Manager.LocalManager.Instance.Strings.SKILL_STATUS_ENTER, skill.skill.Name));
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
