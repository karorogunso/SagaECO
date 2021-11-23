using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Vates
{
    public class HolyWeapon : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            SkillHandler.RemoveAddition(dActor, "FireWeapon");
            SkillHandler.RemoveAddition(dActor, "WindWeapon");
            SkillHandler.RemoveAddition(dActor, "WaterWeapon");
            SkillHandler.RemoveAddition(dActor, "EarthWeapon");
            SkillHandler.RemoveAddition(dActor, "DarkWeapon");
            DefaultBuff skill = new DefaultBuff(args.skill, dActor, "HolyWeapon", 50000);
            skill.OnAdditionStart += this.StartEventHandler;
            skill.OnAdditionEnd += this.EndEventHandler;
            SkillHandler.ApplyAddition(dActor, skill);
        }

        void StartEventHandler(Actor actor, DefaultBuff skill)
        {
            int atk = 5 + skill.skill.Level * 4;
            if (skill.Variable.ContainsKey("WeaponEle"))
                skill.Variable.Remove("WeaponEle");
            skill.Variable.Add("WeaponEle", atk);
            actor.Status.attackElements_skill[SagaLib.Elements.Holy] += atk;
            actor.Buff.WeaponHolyElementUp = true;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }

        void EndEventHandler(Actor actor, DefaultBuff skill)
        {
            int value = skill.Variable["WeaponEle"];
            actor.Status.attackElements_skill[SagaLib.Elements.Holy] -= value;
            actor.Buff.WeaponHolyElementUp = false;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }

        #endregion
    }
}
