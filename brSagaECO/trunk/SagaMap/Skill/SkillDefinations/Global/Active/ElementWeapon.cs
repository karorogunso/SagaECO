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
            SkillHandler.RemoveAddition(dActor, "FireWeapon");
            SkillHandler.RemoveAddition(dActor, "WaterWeapon");
            SkillHandler.RemoveAddition(dActor, "WindWeapon");
            SkillHandler.RemoveAddition(dActor, "EarthWeapon");
            SkillHandler.RemoveAddition(dActor, "DarkWeapon");
            SkillHandler.RemoveAddition(dActor, "HolyWeapon");

            DefaultBuff skill = new DefaultBuff(args.skill, dActor, element.ToString() + "Weapon", 50000);
            skill.OnAdditionStart += this.StartEventHandler;
            skill.OnAdditionEnd += this.EndEventHandler;
            SkillHandler.ApplyAddition(dActor, skill);
        }
        void StartEventHandler(Actor actor, DefaultBuff skill)
        {
            int atk = 5 + skill.skill.Level * 4;
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
            int value = skill.Variable["ElementWeapon"];
            actor.Status.attackElements_skill[element] -= value;
            Type type = actor.Buff.GetType();
            System.Reflection.PropertyInfo propertyInfo = type.GetProperty("Weapon" + element.ToString() + "ElementUp");
            propertyInfo.SetValue(actor.Buff, false, null);
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }


    }
}
