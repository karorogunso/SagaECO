using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Gladiator
{
    /// <summary>
    /// リムーブウエポン
    /// </summary>
    public class Disarm : ISkill
    {
        #region ISkill 成員

        public int TryCast(SagaDB.Actor.ActorPC sActor, SagaDB.Actor.Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(SagaDB.Actor.Actor sActor, SagaDB.Actor.Actor dActor, SkillArg args, byte level)
        {
            float factor = 1f + 0.5f * level;
            int lifetime = 4000 + 2000 * level;
            SkillHandler.Instance.PhysicalAttack(sActor, dActor, args, SagaLib.Elements.Neutral, factor);
            if (dActor.type == ActorType.MOB)
            {
                DefaultBuff skill = new DefaultBuff(args.skill, dActor, "DisarmMOB", lifetime);
                skill.OnAdditionStart += this.StartEventHandlerMOB;
                skill.OnAdditionEnd += this.EndEventHandlerMOB;
                SkillHandler.ApplyAddition(dActor, skill);
            }
            if (dActor.type == ActorType.PC)
            {
                DefaultBuff skill = new DefaultBuff(args.skill, dActor, "DisarmPC", lifetime);
                skill.OnAdditionStart += this.StartEventHandlerPC;
                skill.OnAdditionEnd += this.EndEventHandlerPC;
                SkillHandler.ApplyAddition(dActor, skill);
                ActorPC pc = (ActorPC)dActor;
                if (pc.Inventory.Equipments.ContainsKey(SagaDB.Item.EnumEquipSlot.RIGHT_HAND))
                {
                    SagaDB.Item.Item item = pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND];
                    item = pc.Inventory.Equipments[item.EquipSlot[0]];
                    Packets.Client.CSMG_ITEM_MOVE p = new SagaMap.Packets.Client.CSMG_ITEM_MOVE();
                    p.data = new byte[20];
                    p.Target = SagaDB.Item.ContainerType.BODY;
                    p.InventoryID = item.Slot;
                    p.Count = item.Stack;
                    SagaMap.Network.Client.MapClient.FromActorPC(pc).OnItemMove(p);
                }
                if (pc.Inventory.Equipments.ContainsKey(SagaDB.Item.EnumEquipSlot.LEFT_HAND))
                {
                    SagaDB.Item.Item item = pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.LEFT_HAND];
                    item = pc.Inventory.Equipments[item.EquipSlot[0]];
                    Packets.Client.CSMG_ITEM_MOVE p = new SagaMap.Packets.Client.CSMG_ITEM_MOVE();
                    p.data = new byte[20];
                    p.Target = SagaDB.Item.ContainerType.BODY;
                    p.InventoryID = item.Slot;
                    p.Count = item.Stack;
                    SagaMap.Network.Client.MapClient.FromActorPC(pc).OnItemMove(p);
                }
            }
        }
        void StartEventHandlerPC(Actor actor, DefaultBuff skill)
        {
            short vitdown = (short)(10 * skill.skill.Level);
            if (skill.Variable.ContainsKey("DisarmPC_VIT_DOWN"))
                skill.Variable.Remove("DisarmPC_VIT_DOWN");
            skill.Variable.Add("DisarmPC_VIT_DOWN", vitdown);
            actor.Status.vit_skill -= vitdown;
            actor.Buff.VIT減少 = true;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
        void EndEventHandlerPC(Actor actor, DefaultBuff skill)
        {
            actor.Status.vit_skill += (short)skill.Variable["DisarmPC_VIT_DOWN"];
            actor.Buff.VIT減少 = false;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
        void StartEventHandlerMOB(Actor actor, DefaultBuff skill)
        {
            float atkdowm = 0.15f + 0.05f * skill.skill.Level;
            actor.Status.max_atk1_skill -= (short)(actor.Status.max_atk1 * atkdowm);
            actor.Status.max_atk2_skill -= (short)(actor.Status.max_atk2 * atkdowm);
            actor.Status.max_atk3_skill -= (short)(actor.Status.max_atk3 * atkdowm);
            actor.Status.min_atk1_skill -= (short)(actor.Status.min_atk1 * atkdowm);
            actor.Status.min_atk2_skill -= (short)(actor.Status.min_atk2 * atkdowm);
            actor.Status.min_atk3_skill -= (short)(actor.Status.min_atk3 * atkdowm);
            actor.Status.max_matk_skill -= (short)(actor.Status.max_matk * atkdowm);
            actor.Status.min_matk_skill -= (short)(actor.Status.min_matk * atkdowm);
        }
        void EndEventHandlerMOB(Actor actor, DefaultBuff skill)
        {
            float atkdowm = 0.15f + 0.05f * skill.skill.Level;
            actor.Status.max_atk1_skill += (short)(actor.Status.max_atk1 * atkdowm);
            actor.Status.max_atk2_skill += (short)(actor.Status.max_atk2 * atkdowm);
            actor.Status.max_atk3_skill += (short)(actor.Status.max_atk3 * atkdowm);
            actor.Status.min_atk1_skill += (short)(actor.Status.min_atk1 * atkdowm);
            actor.Status.min_atk2_skill += (short)(actor.Status.min_atk2 * atkdowm);
            actor.Status.min_atk3_skill += (short)(actor.Status.min_atk3 * atkdowm);
            actor.Status.max_matk_skill += (short)(actor.Status.max_matk * atkdowm);
            actor.Status.min_matk_skill += (short)(actor.Status.min_matk * atkdowm);
        }
        #endregion
    }
}
