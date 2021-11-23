using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
using SagaLib;
namespace SagaMap.Skill.SkillDefinations.Monster
{
    /// <summary>
    /// 怪物用各屬性祝福
    /// </summary>
    public class MobElementBless : ISkill
    {
        public Elements element;
        public MobElementBless(Elements e)
        {
            element = e;
        }
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            if (dActor.Status.Additions.ContainsKey(element.ToString() + "Rise"))
            {
                return -1;
            }
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
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
            if (dActor.Status.elements_skill[Elements.Earth] != 0)
                dActor.Status.elements_skill[Elements.Earth] = 0;
            if (dActor.Status.elements_skill[Elements.Water] != 0)
                dActor.Status.elements_skill[Elements.Water] = 0;
            if (dActor.Status.elements_skill[Elements.Fire] != 0)
                dActor.Status.elements_skill[Elements.Fire] = 0;
            if (dActor.Status.elements_skill[Elements.Wind] != 0)
                dActor.Status.elements_skill[Elements.Wind] = 0;
            if (dActor.Status.elements_skill[Elements.Dark] != 0)
                dActor.Status.elements_skill[Elements.Dark] = 0;
            if (dActor.Status.elements_skill[Elements.Holy] != 0)
                dActor.Status.elements_skill[Elements.Holy] = 0;
            int lifetime = 50000 ;
            DefaultBuff skill = new DefaultBuff(args.skill, dActor, element.ToString() + "Rise", lifetime);
            skill.OnAdditionStart += this.StartEventHandler;
            skill.OnAdditionEnd += this.EndEventHandler;
            SkillHandler.ApplyAddition(dActor, skill);
        }
        void StartEventHandler(Actor actor, DefaultBuff skill)
        {
            int ElementAdd = 50;

            //原屬性值
            if (skill.Variable.ContainsKey("ElementRise_" + element.ToString()))
                skill.Variable.Remove("ElementRise_" + element.ToString());
            skill.Variable.Add("ElementRise_" + element.ToString(), ElementAdd);

            actor.Status.elements_skill[element] += ElementAdd;
            //actor.Status.attackElements_skill[element] += ElementAdd;
            if (actor.Status.elements_skill[element] > 100)
                actor.Status.elements_skill[element] = 100;
            if (actor.Status.attackElements_skill[element] > 100)
                actor.Status.attackElements_skill[element] = 100;
            Type type = actor.Buff.GetType();
            System.Reflection.PropertyInfo propertyInfo = type.GetProperty("Body" + element.ToString() + "ElementUp");
            propertyInfo.SetValue(actor.Buff, true, null);
        }
        void EndEventHandler(Actor actor, DefaultBuff skill)
        {
            short value = (short)skill.Variable["ElementRise_" + element.ToString()];
            if (skill.Variable.ContainsKey("ElementRise_" + element.ToString()))
                skill.Variable.Remove("ElementRise_" + element.ToString());
            //原屬性值
            actor.Status.elements_skill[element] -= value;
            //actor.Status.attackElements_skill[element] -= value;
            if (actor.Status.elements_skill[element] < 0)
                actor.Status.elements_skill[element] = 0;
            if (actor.Status.attackElements_skill[element] < 0)
                actor.Status.attackElements_skill[element] = 0;
            Type type = actor.Buff.GetType();
            System.Reflection.PropertyInfo propertyInfo = type.GetProperty("Body" + element.ToString() + "ElementUp");
            propertyInfo.SetValue(actor.Buff, false, null);
        }
        #endregion
    }
}
 