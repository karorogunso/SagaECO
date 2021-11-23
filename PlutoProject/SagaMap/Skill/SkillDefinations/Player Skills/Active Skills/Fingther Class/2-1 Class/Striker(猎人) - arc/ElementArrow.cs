using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations.Striker
{
    /// <summary>
    /// 各種屬性箭
    /// </summary>
    public class ElementArrow : ISkill
    {
        private SagaLib.Elements element = SagaLib.Elements.Neutral;
        public ElementArrow(SagaLib.Elements e)
        {
            element = e;
        }
        #region ISkill Members
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            if (CheckPossible(pc))
            {
                uint ItemID = 0;
                switch (element)
                {
                    case SagaLib.Elements.Earth:
                        ItemID = 10026505;
                        break;
                    case SagaLib.Elements.Water:
                        ItemID = 10026504;
                        break;
                    case SagaLib.Elements.Fire:
                        ItemID = 10026500;
                        break;
                    case SagaLib.Elements.Wind:
                        ItemID = 10026507;
                        break;
                }
                if (pc.Inventory.Equipments.ContainsKey(SagaDB.Item.EnumEquipSlot.LEFT_HAND))
                {
                    if (pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.LEFT_HAND].ItemID == ItemID)
                    {
                        if (SkillHandler.Instance.CountItem(pc, ItemID) > 0)
                        {
                            SkillHandler.Instance.TakeItem(pc, ItemID, 1);
                            return 0;
                        }
                        else
                            return -55;
                    }
                    else if (SkillHandler.Instance.CountItem(pc, ItemID) > 0)
                    {
                        SkillHandler.Instance.TakeItem(pc, ItemID, 1);
                        return 0;
                    }
                    else
                    {
                        return -2;
                    }
                }
                else if (SkillHandler.Instance.CountItem(pc, ItemID) > 0)
                {
                    SkillHandler.Instance.TakeItem(pc, ItemID, 1);
                    return 0;
                }
                else
                {
                    return -2;
                }
            }
            else
                return -5;
            
        }

        bool CheckPossible(Actor sActor)
        {
            if (sActor.type == ActorType.PC)
            {
                ActorPC pc = (ActorPC)sActor;
                pc = SkillHandler.Instance.GetPossesionedActor((ActorPC)sActor);
                if (pc.Inventory.Equipments.ContainsKey(SagaDB.Item.EnumEquipSlot.RIGHT_HAND))
                {
                    if (pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].BaseData.itemType == SagaDB.Item.ItemType.BOW)
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
            if (sActor.type != ActorType.PC)
            {
                level = 5;
            }
            if (dActor.Status.Additions.ContainsKey("HolyShield"))
                SkillHandler.RemoveAddition(dActor, "HolyShield");
            if (dActor.Status.Additions.ContainsKey("DarkShield"))
                SkillHandler.RemoveAddition(dActor, "DarkShield");
            if (dActor.Status.Additions.ContainsKey("FireShield"))
                SkillHandler.RemoveAddition(dActor, "FireShield");
            if (dActor.Status.Additions.ContainsKey("WaterShield"))
                SkillHandler.RemoveAddition(dActor, "WaterShield");
            if (dActor.Status.Additions.ContainsKey("WindShield"))
                SkillHandler.RemoveAddition(dActor, "WindShield");
            if (dActor.Status.Additions.ContainsKey("EarthShield"))
                SkillHandler.RemoveAddition(dActor, "EarthShield");
            dActor.Buff.BodyDarkElementUp = false;
            dActor.Buff.BodyEarthElementUp = false;
            dActor.Buff.BodyFireElementUp = false;
            dActor.Buff.BodyWaterElementUp = false;
            dActor.Buff.BodyWindElementUp = false;
            dActor.Buff.BodyHolyElementUp = false;
            args.argType = SkillArg.ArgType.Active;
            args.type = ATTACK_TYPE.STAB;
            SkillHandler.Instance.PhysicalAttack(sActor, dActor, args, SagaLib.Elements.Neutral, 1.0f);
            int life = new int[] { 0, 10000, 12000, 14000, 16000, 18000 }[level];
            DefaultBuff skill = new DefaultBuff(args.skill, dActor, element.ToString() + "Shield", life);
            skill.OnAdditionStart += this.StartEventHandler;
            skill.OnAdditionEnd += this.EndEventHandler;
            SkillHandler.ApplyAddition(dActor, skill);
        }
        void StartEventHandler(Actor actor, DefaultBuff skill)
        {
            int atk1 = 60;
            if (skill.Variable.ContainsKey("ElementShield"))
                skill.Variable.Remove("ElementShield");
            skill.Variable.Add("ElementShield", atk1);
            actor.Status.elements_skill[element] += atk1;

            Type type = actor.Buff.GetType();
            System.Reflection.PropertyInfo propertyInfo = type.GetProperty("Body" + element.ToString() + "ElementUp");
            propertyInfo.SetValue(actor.Buff, true, null);
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
        void EndEventHandler(Actor actor, DefaultBuff skill)
        {
            int value = skill.Variable["ElementShield"];
            actor.Status.elements_skill[element] -= (short)value;

            Type type = actor.Buff.GetType();
            System.Reflection.PropertyInfo propertyInfo = type.GetProperty("Body" + element.ToString() + "ElementUp");
            propertyInfo.SetValue(actor.Buff, false, null);
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
        #endregion
    }
}
