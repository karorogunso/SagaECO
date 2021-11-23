using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaMap.Skill.Additions.Global;
using SagaDB.Actor;

namespace SagaMap.Skill.SkillDefinations.BountyHunter
{
    /// <summary>
    /// 參擊無雙（斬撃無双）
    /// </summary>
    public class MuSoU : ISkill 
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            if (CheckPossible(sActor))
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
                    if (pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].BaseData.itemType == SagaDB.Item.ItemType.SWORD ||
                        pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].BaseData.itemType == SagaDB.Item.ItemType.RAPIER ||
                        pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].BaseData.itemType == SagaDB.Item.ItemType.SHORT_SWORD ||
                        pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].BaseData.itemType == SagaDB.Item.ItemType.CLAW)
                        return true;
                    else
                        return false;
                }
                else
                    return false;
            }
            else
                return true;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            float factor = 0.6f + 0.1f * level;
            args.argType = SkillArg.ArgType.Attack;
            args.type = ATTACK_TYPE.BLOW;
            List<Actor> dest = new List<Actor>();
            for (int i = 0; i < 7; i++)
            {
                dest.Add(dActor);
            }
            SkillHandler.Instance.PhysicalAttack(sActor, dest, args, SagaLib.Elements.Neutral, factor);

            DefaultBuff skill = new DefaultBuff(args.skill, sActor, "MuSoU", 20000);
            skill.OnAdditionStart += this.StartEventHandler;
            skill.OnAdditionEnd += this.EndEventHandler;
            skill.OnCheckValid += this.ValidCheck;
            SkillHandler.ApplyAddition(sActor, skill);
        }
        void ValidCheck(ActorPC pc, Actor dActor, out int result)
        {
            result = TryCast(pc, dActor, null);
        }

        void StartEventHandler(Actor actor, DefaultBuff skill)
        {
            if (skill.Variable.ContainsKey("MuSoU2"))
                skill.Variable.Remove("MuSoU2");
            skill.Variable["MuSoU2"] = skill.skill.Level;
            actor.Status.aspd_rate_skill += (short)(15 + 5 * skill.Variable["MuSoU2"]);

            actor.Buff.ShortSwordDelayCancel = true;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }

        void EndEventHandler(Actor actor, DefaultBuff skill)
        {
            short raspd_skill_perc_restore = (short)(15 + 5 * skill.Variable["MuSoU2"]);
            if (actor.Status.aspd_rate_skill > 100 + raspd_skill_perc_restore)
            {
                actor.Status.aspd_rate_skill -= raspd_skill_perc_restore;
            }
            else
            {
                actor.Status.aspd_rate_skill = 100;
            }

            actor.Buff.ShortSwordDelayCancel = false;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
        #endregion
    }
}
