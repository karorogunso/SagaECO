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
            int lifetime = 15000 + 5000 * level;
            SkillHandler.Instance.PhysicalAttack(sActor, dActor, args, sActor.WeaponElement, factor);
            if (dActor.type == ActorType.MOB)
            {
                if (SkillHandler.Instance.isBossMob(dActor) == true)
                {
                    lifetime = 4000 + 2000 * level;
                }
                DefaultBuff skill = new DefaultBuff(args.skill, dActor, "DisarmMOB", lifetime);
                skill.OnAdditionStart += this.StartEventHandlerMOB;
                skill.OnAdditionEnd += this.EndEventHandlerMOB;
                SkillHandler.ApplyAddition(dActor, skill);
            }
            if (dActor.type == ActorType.PC)
            {
                lifetime = 4000 + 2000 * level;
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
            actor.Buff.VITDown = true;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
        void EndEventHandlerPC(Actor actor, DefaultBuff skill)
        {
            actor.Status.vit_skill += (short)skill.Variable["DisarmPC_VIT_DOWN"];
            actor.Buff.VITDown = false;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
        void StartEventHandlerMOB(Actor actor, DefaultBuff skill)
        {
            float atkdowm = 0.15f + 0.05f * skill.skill.Level;
            if (skill.Variable.ContainsKey("DisarmMOB_MAXATK1_DOWN"))
                skill.Variable.Remove("DisarmMOB_MAXATK1_DOWN");
            skill.Variable.Add("DisarmMOB_MAXATK1_DOWN", (short)(actor.Status.max_atk1 * atkdowm));
            actor.Status.max_atk1_skill -= (short)(actor.Status.max_atk1 * atkdowm);
            if (skill.Variable.ContainsKey("DisarmMOB_MAXATK2_DOWN"))
                skill.Variable.Remove("DisarmMOB_MAXATK2_DOWN");
            skill.Variable.Add("DisarmMOB_MAXATK2_DOWN", (short)(actor.Status.max_atk2 * atkdowm));
            actor.Status.max_atk2_skill -= (short)(actor.Status.max_atk2 * atkdowm);
            if (skill.Variable.ContainsKey("DisarmMOB_MAXATK3_DOWN"))
                skill.Variable.Remove("DisarmMOB_MAXATK3_DOWN");
            skill.Variable.Add("DisarmMOB_MAXATK3_DOWN", (short)(actor.Status.max_atk3 * atkdowm));
            actor.Status.max_atk3_skill -= (short)(actor.Status.max_atk3 * atkdowm);
            if (skill.Variable.ContainsKey("DisarmMOB_MINATK1_DOWN"))
                skill.Variable.Remove("DisarmMOB_MINATK1_DOWN");
            skill.Variable.Add("DisarmMOB_MINATK1_DOWN", (short)(actor.Status.min_atk1 * atkdowm));
            actor.Status.min_atk1_skill -= (short)(actor.Status.min_atk1 * atkdowm);
            if (skill.Variable.ContainsKey("DisarmMOB_MINATK2_DOWN"))
                skill.Variable.Remove("DisarmMOB_MINATK2_DOWN");
            skill.Variable.Add("DisarmMOB_MINATK2_DOWN", (short)(actor.Status.min_atk2 * atkdowm));
            actor.Status.min_atk2_skill -= (short)(actor.Status.min_atk2 * atkdowm);
            if (skill.Variable.ContainsKey("DisarmMOB_MINATK3_DOWN"))
                skill.Variable.Remove("DisarmMOB_MINATK3_DOWN");
            skill.Variable.Add("DisarmMOB_MINATK3_DOWN", (short)(actor.Status.min_atk3 * atkdowm));
            actor.Status.min_atk3_skill -= (short)(actor.Status.min_atk3 * atkdowm);
            if (skill.Variable.ContainsKey("DisarmMOB_MAXMATK_DOWN"))
                skill.Variable.Remove("DisarmMOB_MAXMATK_DOWN");
            skill.Variable.Add("DisarmMOB_MAXMATK_DOWN", (short)(actor.Status.max_matk * atkdowm));
            actor.Status.max_matk_skill -= (short)(actor.Status.max_matk * atkdowm);
            if (skill.Variable.ContainsKey("DisarmMOB_MINMATK_DOWN"))
                skill.Variable.Remove("DisarmMOB_MINMATK_DOWN");
            skill.Variable.Add("DisarmMOB_MINMATK_DOWN", (short)(actor.Status.min_matk * atkdowm));
            actor.Status.min_matk_skill -= (short)(actor.Status.min_matk * atkdowm);
            actor.Buff.MinAtkDown = true;
            actor.Buff.MaxAtkDown = true;
            actor.Buff.MinMagicAtkDown = true;
            actor.Buff.MaxMagicAtkDown = true;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
        void EndEventHandlerMOB(Actor actor, DefaultBuff skill)
        {
            actor.Status.max_atk1_skill += (short)skill.Variable["DisarmMOB_MAXATK1_DOWN"];
            actor.Status.max_atk2_skill += (short)skill.Variable["DisarmMOB_MAXATK2_DOWN"];
            actor.Status.max_atk3_skill += (short)skill.Variable["DisarmMOB_MAXATK3_DOWN"];
            actor.Status.min_atk1_skill += (short)skill.Variable["DisarmMOB_MINATK1_DOWN"];
            actor.Status.min_atk2_skill += (short)skill.Variable["DisarmMOB_MINATK2_DOWN"];
            actor.Status.min_atk3_skill += (short)skill.Variable["DisarmMOB_MINATK3_DOWN"];
            actor.Status.max_matk_skill += (short)skill.Variable["DisarmMOB_MAXMATK_DOWN"];
            actor.Status.min_matk_skill += (short)skill.Variable["DisarmMOB_MINMATK_DOWN"];
            actor.Buff.MinAtkDown = false;
            actor.Buff.MaxAtkDown = false;
            actor.Buff.MinMagicAtkDown = false;
            actor.Buff.MaxMagicAtkDown = false;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
        #endregion
    }
}
