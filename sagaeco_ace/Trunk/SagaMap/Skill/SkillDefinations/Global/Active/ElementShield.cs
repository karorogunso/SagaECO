using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
using SagaLib;
namespace SagaMap.Skill.SkillDefinations.Global
{
    public class ElementShield : ISkill
    {
        public Elements element;
        public ElementShield(Elements e)
        {
            element = e;
        }
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            SkillHandler.RemoveAddition(dActor, "HolyShield");
            SkillHandler.RemoveAddition(dActor, "DarkShield");
            SkillHandler.RemoveAddition(dActor, "FireShield");
            SkillHandler.RemoveAddition(dActor, "WaterShield");
            SkillHandler.RemoveAddition(dActor, "WindShield");
            SkillHandler.RemoveAddition(dActor, "EarthShield");
            int life = new int[] { 0, 15000, 35000, 60000, 100000, 150000 }[level];
            DefaultBuff skill = new DefaultBuff(args.skill, dActor, element.ToString() + "Shield", life);
            skill.OnAdditionStart += this.StartEventHandler;
            skill.OnAdditionEnd += this.EndEventHandler;
            SkillHandler.ApplyAddition(dActor, skill);
        }
        void StartEventHandler(Actor actor, DefaultBuff skill)
        {
            int atk1 = skill.skill.Level * 5;
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
    }
}
