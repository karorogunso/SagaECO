using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Enchanter
{
    /// <summary>
    ///  旺盛的精靈勢力（パワーエンチャント）
    /// </summary>
    public class AtkUp_DefUp_SpdDown_AvoDown : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            if (dActor.type == ActorType.PC)
            {
                return 0;
            }
            else
            {
                return -14;
            }
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            DefaultBuff skill = new DefaultBuff(args.skill, dActor, "AtkUp_DefUp_SpdDown_AvoDown", 30000);
            skill.OnAdditionStart += this.StartEventHandler;
            skill.OnAdditionEnd += this.EndEventHandler;
            SkillHandler.ApplyAddition(dActor, skill);
        }
        void StartEventHandler(Actor actor, DefaultBuff skill)
        {
            int level = skill.skill.Level;

            //最小攻擊
            int min_atk1_add = (int)(actor.Status.min_atk1 * (0.12f + 0.03f * level));
            if (skill.Variable.ContainsKey("AtkUp_DefUp_SpdDown_AvoDown_min_atk1"))
                skill.Variable.Remove("AtkUp_DefUp_SpdDown_AvoDown_min_atk1");
            skill.Variable.Add("AtkUp_DefUp_SpdDown_AvoDown_min_atk1", min_atk1_add);
            actor.Status.min_atk1_skill += (short)min_atk1_add;

            //最小攻擊
            int min_atk2_add = (int)(actor.Status.min_atk2 * (0.12f + 0.03f * level));
            if (skill.Variable.ContainsKey("AtkUp_DefUp_SpdDown_AvoDown_min_atk2"))
                skill.Variable.Remove("AtkUp_DefUp_SpdDown_AvoDown_min_atk2");
            skill.Variable.Add("AtkUp_DefUp_SpdDown_AvoDown_min_atk2", min_atk2_add);
            actor.Status.min_atk2_skill += (short)min_atk2_add;

            //最小攻擊
            int min_atk3_add = (int)(actor.Status.min_atk3 * (0.12f + 0.03f * level));
            if (skill.Variable.ContainsKey("AtkUp_DefUp_SpdDown_AvoDown_min_atk3"))
                skill.Variable.Remove("AtkUp_DefUp_SpdDown_AvoDown_min_atk3");
            skill.Variable.Add("AtkUp_DefUp_SpdDown_AvoDown_min_atk3", min_atk3_add);
            actor.Status.min_atk3_skill += (short)min_atk3_add;

            //最小魔攻
            int min_matk_add = (int)(actor.Status.min_matk * (0.12f + 0.03f * level));
            if (skill.Variable.ContainsKey("AtkUp_DefUp_SpdDown_AvoDown_min_matk"))
                skill.Variable.Remove("AtkUp_DefUp_SpdDown_AvoDown_min_matk");
            skill.Variable.Add("AtkUp_DefUp_SpdDown_AvoDown_min_matk", min_matk_add);
            actor.Status.min_matk_skill += (short)min_matk_add;

            //左防禦
            int def_add = (int)(actor.Status.def * (0.21f + 0.01f * level));
            if (skill.Variable.ContainsKey("AtkUp_DefUp_SpdDown_AvoDown_def"))
                skill.Variable.Remove("AtkUp_DefUp_SpdDown_AvoDown_def");
            skill.Variable.Add("AtkUp_DefUp_SpdDown_AvoDown_def", def_add);
            actor.Status.def_skill += (short)def_add;

            //左魔防
            int mdef_add = (int)(actor.Status.mdef * (0.21f + 0.01f * level));
            if (skill.Variable.ContainsKey("AtkUp_DefUp_SpdDown_AvoDown_mdef"))
                skill.Variable.Remove("AtkUp_DefUp_SpdDown_AvoDown_mdef");
            skill.Variable.Add("AtkUp_DefUp_SpdDown_AvoDown_mdef", mdef_add);
            actor.Status.mdef_skill += (short)mdef_add;

            //攻擊速度
            int aspd_add = -(int)(actor.Status.aspd * (0.07f + 0.02f * level));
            if (skill.Variable.ContainsKey("AtkUp_DefUp_SpdDown_AvoDown_aspd"))
                skill.Variable.Remove("AtkUp_DefUp_SpdDown_AvoDown_aspd");
            skill.Variable.Add("AtkUp_DefUp_SpdDown_AvoDown_aspd", aspd_add);
            actor.Status.aspd_skill += (short)aspd_add;

            //詠唱速度
            int cspd_add = -(int)(actor.Status.cspd * (0.07f + 0.02f * level));
            if (skill.Variable.ContainsKey("AtkUp_DefUp_SpdDown_AvoDown_cspd"))
                skill.Variable.Remove("AtkUp_DefUp_SpdDown_AvoDown_cspd");
            skill.Variable.Add("AtkUp_DefUp_SpdDown_AvoDown_cspd", cspd_add);
            actor.Status.cspd_skill += (short)cspd_add;

            //近戰迴避
            int avoid_melee_add = -(int)(actor.Status.avoid_melee * (0.11f + 0.03f * level));
            if (skill.Variable.ContainsKey("AtkUp_DefUp_SpdDown_AvoDown_avoid_melee"))
                skill.Variable.Remove("AtkUp_DefUp_SpdDown_AvoDown_avoid_melee");
            skill.Variable.Add("AtkUp_DefUp_SpdDown_AvoDown_avoid_melee", avoid_melee_add);
            actor.Status.avoid_melee_skill += (short)avoid_melee_add;

            //MP回復率
            if (skill.Variable.ContainsKey("SpdUp_AvoUp_AtkDown_DefDown_mp_recover_skill"))
                skill.Variable.Remove("SpdUp_AvoUp_AtkDown_DefDown_mp_recover_skill");
            skill.Variable.Add("SpdUp_AvoUp_AtkDown_DefDown_mp_recover_skill", (int)(-0.11f - 0.03f * level));
            actor.Status.mp_recover_skill += (short)(-0.11f - 0.03f * level);

            actor.Buff.DefUp = true;
            actor.Buff.MagicDefUp = true;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }

        void EndEventHandler(Actor actor, DefaultBuff skill)
        {
            //最小攻擊
            actor.Status.min_atk1_skill -= (short)skill.Variable["AtkUp_DefUp_SpdDown_AvoDown_min_atk1"];

            //最小攻擊
            actor.Status.min_atk2_skill -= (short)skill.Variable["AtkUp_DefUp_SpdDown_AvoDown_min_atk2"];

            //最小攻擊
            actor.Status.min_atk3_skill -= (short)skill.Variable["AtkUp_DefUp_SpdDown_AvoDown_min_atk3"];

            //最小魔攻
            actor.Status.min_matk_skill -= (short)skill.Variable["AtkUp_DefUp_SpdDown_AvoDown_min_matk"];

            //左防禦
            actor.Status.def_skill -= (short)skill.Variable["AtkUp_DefUp_SpdDown_AvoDown_def"];

            //左魔防
            actor.Status.mdef_skill -= (short)skill.Variable["AtkUp_DefUp_SpdDown_AvoDown_mdef"];

            //攻擊速度
            actor.Status.aspd_skill -= (short)skill.Variable["AtkUp_DefUp_SpdDown_AvoDown_aspd"];

            //詠唱速度
            actor.Status.cspd_skill -= (short)skill.Variable["AtkUp_DefUp_SpdDown_AvoDown_cspd"];

            //近戰迴避
            actor.Status.avoid_melee_skill -= (short)skill.Variable["AtkUp_DefUp_SpdDown_AvoDown_avoid_melee"];


            actor.Buff.DefUp = false;
            actor.Buff.MagicDefUp = false;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
        #endregion
    }
}
