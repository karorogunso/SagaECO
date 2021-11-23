using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations.Shaman
{
    public class FireWeapon:ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            SkillHandler.RemoveAddition(dActor, "WindWeapon");
            SkillHandler.RemoveAddition(dActor, "WaterWeapon");
            SkillHandler.RemoveAddition(dActor, "EarthWeapon");
            SkillHandler.RemoveAddition(dActor, "HolyWeapon");
            SkillHandler.RemoveAddition(dActor, "DarkWeapon");
            DefaultBuff skill = new DefaultBuff(args.skill, dActor, "FireWeapon", 50000);
            skill.OnAdditionStart += this.StartEventHandler;
            skill.OnAdditionEnd += this.EndEventHandler;
            SkillHandler.ApplyAddition(dActor, skill);
        }

        void StartEventHandler(Actor actor, DefaultBuff skill)
        {
            int atk = skill.skill.Level * 3;
            if (skill.Variable.ContainsKey("WeaponEle"))
                skill.Variable.Remove("WeaponEle");
            skill.Variable.Add("WeaponEle", atk);
            actor.AttackElements[SagaLib.Elements.Fire] += atk;

            actor.Buff.武器の火属性上昇 = true;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }

        void EndEventHandler(Actor actor, DefaultBuff skill)
        {
            int value = skill.Variable["WeaponEle"];
            actor.AttackElements[SagaLib.Elements.Fire] -= value;
            actor.Buff.武器の火属性上昇 = false;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
        #endregion
    }
}
