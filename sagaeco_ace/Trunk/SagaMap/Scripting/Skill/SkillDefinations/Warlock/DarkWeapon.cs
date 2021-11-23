using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Warlock
{
    public class DarkWeapon:ISkill
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
            SkillHandler.RemoveAddition(dActor, "HolyWeapon");
            DefaultBuff skill = new DefaultBuff(args.skill, dActor, "DarkWeapon", 50000);
            skill.OnAdditionStart += this.StartEventHandler;
            skill.OnAdditionEnd += this.EndEventHandler;
            SkillHandler.ApplyAddition(dActor, skill);
        }

        void StartEventHandler(Actor actor, DefaultBuff skill)
        {
            float value;
            value = (float)skill.skill.Level * 0.025f;
            int atk1, atk2, atk3, atk4, atk5, atk6;
            atk1 = (int)(actor.Status.min_atk_ori * (value));
            atk2 = (int)(actor.Status.max_atk_ori * (value));
            atk3 = (int)(actor.Status.min_atk_ori * (value));
            atk4 = (int)(actor.Status.max_atk_ori * (value));
            atk5 = (int)(actor.Status.min_atk_ori * (value));
            atk6 = (int)(actor.Status.max_atk_ori * (value));

            int atk;
            atk = skill.skill.Level * 3;
            if (skill.Variable.ContainsKey("WeaponEle"))
                skill.Variable.Remove("WeaponEle");
            skill.Variable.Add("WeaponEle", atk);
            actor.AttackElements[SagaLib.Elements.Dark] += atk;

            /*if (skill.Variable.ContainsKey("DarkWeaponATK1"))
                skill.Variable.Remove("DarkWeaponATK1");
            skill.Variable.Add("DarkWeaponATK1", atk1);
            actor.Status.min_atk1_skill += (short)atk1;
            if (skill.Variable.ContainsKey("DarkWeaponATK2"))
                skill.Variable.Remove("DarkWeaponATK2");
            skill.Variable.Add("DarkWeaponATK2", atk2);
            actor.Status.max_atk1_skill += (short)atk2;
            if (skill.Variable.ContainsKey("DarkWeaponATK3"))
                skill.Variable.Remove("DarkWeaponATK3");
            skill.Variable.Add("DarkWeaponATK3", atk3);
            actor.Status.min_atk2_skill += (short)atk3;
            if (skill.Variable.ContainsKey("DarkWeaponATK4"))
                skill.Variable.Remove("DarkWeaponATK4");
            skill.Variable.Add("DarkWeaponATK4", atk4);
            actor.Status.max_atk2_skill += (short)atk4;
            if (skill.Variable.ContainsKey("DarkWeaponATK5"))
                skill.Variable.Remove("DarkWeaponATK5");
            skill.Variable.Add("DarkWeaponATK5", atk5);
            actor.Status.min_atk3_skill += (short)atk5;
            if (skill.Variable.ContainsKey("DarkWeaponATK6"))
                skill.Variable.Remove("DarkWeaponATK6");
            skill.Variable.Add("DarkWeaponATK6", atk6);
            actor.Status.max_atk3_skill += (short)atk6;*/

            actor.Buff.武器の闇属性上昇 = true;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }

        void EndEventHandler(Actor actor, DefaultBuff skill)
        {
            /*int value = skill.Variable["DarkWeaponATK1"];
            actor.Status.min_atk1_skill -= (short)value;
            value = skill.Variable["DarkWeaponATK2"];
            actor.Status.max_atk1_skill -= (short)value;
            value = skill.Variable["DarkWeaponATK3"];
            actor.Status.min_atk2_skill -= (short)value;
            value = skill.Variable["DarkWeaponATK4"];
            actor.Status.max_atk2_skill -= (short)value;
            value = skill.Variable["DarkWeaponATK5"];
            actor.Status.min_atk3_skill -= (short)value;
            value = skill.Variable["DarkWeaponATK6"];
            actor.Status.max_atk3_skill -= (short)value;*/

            int value = skill.Variable["WeaponEle"];
            actor.AttackElements[SagaLib.Elements.Dark] -= value;
            actor.Buff.武器の闇属性上昇 = false;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
        #endregion
    }
}
