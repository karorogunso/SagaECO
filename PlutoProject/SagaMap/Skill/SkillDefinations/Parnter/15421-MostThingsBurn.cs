using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
using SagaLib;
namespace SagaMap.Skill.SkillDefinations.Global
{
    public class MostThingsBurn : ISkill
    {
        /// <summary>
        /// 大抵のモノは燃えます☆（女儿用火属性附加）
        /// </summary>
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            if (sActor.type == ActorType.PARTNER)
            {
                ActorPartner pet = (ActorPartner)sActor;
                int a = SagaLib.Global.Random.Next(1, 2);
                switch (a)
                {
                    case 1:
                        dActor = pet;
                        break;
                    case 2:
                        dActor = pet.Owner;
                        break;
                }
            }

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
            DefaultBuff skill = new DefaultBuff(args.skill, dActor, "FireWeapon", 50000);
            skill.OnAdditionStart += this.StartEventHandler;
            skill.OnAdditionEnd += this.EndEventHandler;
            SkillHandler.ApplyAddition(dActor, skill);
        }
        void StartEventHandler(Actor actor, DefaultBuff skill)
        {
            int atk = 30;
            if (skill.Variable.ContainsKey("ElementWeapon"))
                skill.Variable.Remove("ElementWeapon");
            skill.Variable.Add("ElementWeapon", atk);
            actor.Status.attackElements_skill[Elements.Fire] += atk;
            Type type = actor.Buff.GetType();
            System.Reflection.PropertyInfo propertyInfo = type.GetProperty("WeaponFireElementUp");
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
            actor.Status.attackElements_skill[Elements.Fire] -= value;
            Type type = actor.Buff.GetType();
            System.Reflection.PropertyInfo propertyInfo = type.GetProperty("WeaponFireElementUp");
            propertyInfo.SetValue(actor.Buff, false, null);
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }


    }
}
