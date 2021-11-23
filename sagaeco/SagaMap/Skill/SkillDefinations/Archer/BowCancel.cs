using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations.Archer
{
    /// <summary>
    /// 弓之達人
    /// </summary>
    public class BowCancel: ISkill
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
                if (pc.Inventory.Equipments.ContainsKey(SagaDB.Item.EnumEquipSlot.RIGHT_HAND))
                {
                    if (pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].BaseData.itemType == SagaDB.Item.ItemType.BOW ||  SkillHandler.Instance.CheckDEMRightEquip(sActor, SagaDB.Item.ItemType.PARTS_BLOW))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return true;
            }
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            args.dActor = 0;//不显示效果
            Actor realdActor = SkillHandler.Instance.GetPossesionedActor((ActorPC)sActor);
            if (CheckPossible(realdActor))
            {
                int life = 20000;
                DefaultBuff skill = new DefaultBuff(args.skill, realdActor, "BowCancel", life);
                skill.OnAdditionStart += this.StartEventHandler;
                skill.OnAdditionEnd += this.EndEventHandler;
                skill.OnCheckValid += this.ValidCheck;
                SkillHandler.ApplyAddition(realdActor, skill);
            }
        }
        void ValidCheck(ActorPC pc, Actor dActor, out int result)
        {
            result = TryCast(pc, dActor, null);
        }
        void StartEventHandler(Actor actor, DefaultBuff skill)
        {
            actor.Status.aspd_rate_skill += (short)(20f + 30f * skill.skill.Level);

            actor.Buff.BowDelayCancel = true;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }

        void EndEventHandler(Actor actor, DefaultBuff skill)
        {
            short raspd_skill_perc_restore = (short)(20f + 30f* skill.skill.Level);
            if (actor.Status.aspd_rate_skill > 100 + raspd_skill_perc_restore)
            {
                actor.Status.aspd_rate_skill -= raspd_skill_perc_restore;
            }
            else
            {
                actor.Status.aspd_rate_skill = 100;
            }
            actor.Buff.BowDelayCancel = false;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }

        #endregion
    }
}
