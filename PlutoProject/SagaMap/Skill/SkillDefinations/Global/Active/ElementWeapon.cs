using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
using SagaLib;
namespace SagaMap.Skill.SkillDefinations.Global
{
    public class ElementWeapon : ISkill
    {
        public Elements element;
        public ElementWeapon(Elements e)
        {
            element = e;
        }
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            if (dActor.Status.Additions.ContainsKey("YugenKeiyaku"))
            {
                return;
            }
            if (dActor.Status.Additions.ContainsKey("FireWeapon"))
                SkillHandler.RemoveAddition(dActor, "FireWeapon");
            if (dActor.Status.Additions.ContainsKey("WaterWeapon"))
                SkillHandler.RemoveAddition(dActor, "WaterWeapon");
            if (dActor.Status.Additions.ContainsKey("WindWeapon"))
                SkillHandler.RemoveAddition(dActor, "WindWeapon");
            if (dActor.Status.Additions.ContainsKey("EarthWeapon"))
                SkillHandler.RemoveAddition(dActor, "EarthWeapon");
            if (dActor.Status.Additions.ContainsKey("DarkWeapon"))
                SkillHandler.RemoveAddition(dActor, "DarkWeapon");
            if (dActor.Status.Additions.ContainsKey("HolyWeapon"))
                SkillHandler.RemoveAddition(dActor, "HolyWeapon");
            dActor.Buff.WeaponDarkElementUp = false;
            dActor.Buff.WeaponEarthElementUp = false;
            dActor.Buff.WeaponFireElementUp = false;
            dActor.Buff.WeaponHolyElementUp = false;
            dActor.Buff.WeaponWaterElementUp = false;
            dActor.Buff.WeaponWindElementUp = false;
            DefaultBuff skill = new DefaultBuff(args.skill, dActor, element.ToString() + "Weapon", 50000);
            skill.OnAdditionStart += this.StartEventHandler;
            skill.OnAdditionEnd += this.EndEventHandler;
            SkillHandler.ApplyAddition(dActor, skill);
        }
        void StartEventHandler(Actor actor, DefaultBuff skill)
        {
            int atk = 5 + skill.skill.Level * 4;
            uint SkillID = 0;
            if (actor.type == ActorType.PC)
            {
                switch (element)
                {
                    case Elements.Earth:
                        SkillID = 939;
                        break;
                    case Elements.Water:
                        SkillID = 937;
                        break;
                    case Elements.Fire:
                        SkillID = 936;
                        break;
                    case Elements.Wind:
                        SkillID = 938;
                        break;
                    case Elements.Holy:
                        SkillID = 940;
                        break;
                    case Elements.Dark:
                        SkillID = 941;
                        break;
                    default:
                        SkillID = 0;
                        break;
                }
                ActorPC pc = (ActorPC)actor;
                if ((pc.Skills2_1.ContainsKey(934) || pc.DualJobSkill.Exists(x => x.ID == 934)) && element == Elements.Holy)//GU2-1光明之魂
                {
                    var duallv = 0;
                    if (pc.DualJobSkill.Exists(x => x.ID == 934))
                        duallv = pc.DualJobSkill.FirstOrDefault(x => x.ID == 934).Level;


                    var mainlv = 0;
                    if (pc.Skills2_1.ContainsKey(934))
                        mainlv = pc.Skills2_1[934].Level;

                    atk += 25 + 5 * Math.Max(duallv, mainlv);
                }
                else if ((pc.Skills2_2.ContainsKey(935) || pc.DualJobSkill.Exists(x => x.ID == 935)) && element == Elements.Dark)//GU2-2黑暗之魂
                {
                    var duallv = 0;
                    if (pc.DualJobSkill.Exists(x => x.ID == 935))
                        duallv = pc.DualJobSkill.FirstOrDefault(x => x.ID == 935).Level;


                    var mainlv = 0;
                    if (pc.Skills2_2.ContainsKey(935))
                        mainlv = pc.Skills2_2[935].Level;

                    atk += 25 + 5 * Math.Max(duallv, mainlv);
                }
                else if (pc.Skills2_1.ContainsKey(SkillID) || pc.DualJobSkill.Exists(x => x.ID == SkillID))
                {
                    var duallv = 0;
                    if (pc.DualJobSkill.Exists(x => x.ID == SkillID))
                        duallv = pc.DualJobSkill.FirstOrDefault(x => x.ID == SkillID).Level;


                    var mainlv = 0;
                    if (pc.Skills2_1.ContainsKey(SkillID))
                        mainlv = pc.Skills2_1[SkillID].Level;

                    atk += 5 * Math.Max(duallv, mainlv);
                }
            }







            if (skill.Variable.ContainsKey("ElementWeapon"))
                skill.Variable.Remove("ElementWeapon");
            skill.Variable.Add("ElementWeapon", atk);
            actor.Status.attackElements_skill[element] += atk;
            Type type = actor.Buff.GetType();
            System.Reflection.PropertyInfo propertyInfo = type.GetProperty("Weapon" + element.ToString() + "ElementUp");
            propertyInfo.SetValue(actor.Buff, true, null);
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }

        void EndEventHandler(Actor actor, DefaultBuff skill)
        {
            if (actor.Status.Additions.ContainsKey("YugenKeiyaku"))
            {
                SkillHandler.RemoveAddition(actor, "YugenKeiyaku");
            }
            int value = skill.Variable["ElementWeapon"];
            actor.Status.attackElements_skill[element] -= value;
            Type type = actor.Buff.GetType();
            System.Reflection.PropertyInfo propertyInfo = type.GetProperty("Weapon" + element.ToString() + "ElementUp");
            propertyInfo.SetValue(actor.Buff, false, null);
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }


    }
}
