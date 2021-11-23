using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations.Global
{
    /// <summary>
    /// 各種魂
    /// </summary>
    public class Soul : ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            DefaultPassiveSkill skill = new DefaultPassiveSkill(args.skill, sActor, "Soul", true);
            skill.OnAdditionStart += this.StartEventHandler;
            skill.OnAdditionEnd += this.EndEventHandler;
            SkillHandler.ApplyAddition(sActor, skill);
        }
        void StartEventHandler(Actor actor, DefaultPassiveSkill skill)
        {
            SetupSkill(actor, skill);
        }

        void EndEventHandler(Actor actor, DefaultPassiveSkill skill)
        {
            DeleteSkill(actor, skill);
        }
        void DeleteSkill(Actor actor, DefaultPassiveSkill skill)
        {
            //最小攻擊
            actor.Status.min_atk1_possession =0; //-= (short)skill.Variable["Soul_min_atk_ori_add"];
            actor.Status.min_atk2_possession = 0; //-= (short)skill.Variable["Soul_min_atk_ori_add"];
            actor.Status.min_atk3_possession = 0; //-= (short)skill.Variable["Soul_min_atk_ori_add"];
            //大傷
            actor.Status.max_atk1_possession = 0; //-= (short)skill.Variable["Soul_max_atk_ori_add"];
            actor.Status.max_atk2_possession = 0; //-= (short)skill.Variable["Soul_max_atk_ori_add"];
            actor.Status.max_atk3_possession = 0; //-= (short)skill.Variable["Soul_max_atk_ori_add"];
            //HP MP SP
            actor.Status.hp_possession =0; //-= (short)skill.Variable["Soul_hp_add"];
            actor.Status.mp_possession =0; //-= (short)skill.Variable["Soul_mp_add"];
            actor.Status.sp_possession =0; //-= (short)skill.Variable["Soul_sp_add"];
            //最大魔攻
            actor.Status.max_matk_possession =0; //-= (short)skill.Variable["Soul_Max_MAtk"];
            //最小魔攻
            actor.Status.min_matk_possession =0; //-= (short)skill.Variable["Soul_Min_MAtk"];
            //近戰迴避
            actor.Status.avoid_melee_possession =0; //-= (short)skill.Variable["Soul_avo_melee_add"];
            //遠距迴避
            actor.Status.avoid_ranged_possession =0; //-= (short)skill.Variable["Soul_avo_ranged_add"];
            //近戰命中
            actor.Status.hit_melee_possession =0; //-= (short)skill.Variable["Soul_hit_melee_add"];
            //遠距命中
            actor.Status.hit_ranged_possession =0; //-= (short)skill.Variable["Soul_hit_ranged_add"];
            //左防禦力
            actor.Status.def_possession =0; //-= (short)skill.Variable["Soul_left_def_add"];
            //右防禦力
            actor.Status.def_add_possession =0; //-= (short)skill.Variable["Soul_right_def_add"];
            //左魔法防禦
            actor.Status.mdef_possession =0; //-= (short)skill.Variable["Soul_left_mdef_add"];
            //右魔法防禦
            actor.Status.mdef_add_possession =0; //-= (short)skill.Variable["Soul_right_mdef_add"];
             
        }
        void SetupSkill(Actor actor, DefaultPassiveSkill skill)
        {
            ActorPC actorPC = (ActorPC)actor;
            int max_atk_ori_add = 0, min_atk_ori_add = 0;
            int max_matk_add = 0, min_matk_add = 0, avo_melee_add = 0, avo_ranged_add = 0, hit_melee_add = 0, hit_ranged_add = 0;
            int left_def_add = 0, right_def_add = 0, left_mdef_add = 0, right_mdef_add = 0, hp_add = 0, mp_add = 0, sp_add = 0;
            if (actorPC.PossessionPosition == SagaLib.PossessionPosition.RIGHT_HAND)
            {
                max_atk_ori_add = (int)(actor.Status.max_atk_bs * (0.14));
                min_atk_ori_add = (int)(actor.Status.min_atk_bs  * (0.14));
                max_atk_ori_add = (int)(actor.Status.max_atk_bs * (0.14));
                min_atk_ori_add = (int)(actor.Status.min_atk_bs * (0.14));
                max_atk_ori_add = (int)(actor.Status.max_atk_bs * (0.14));
                min_atk_ori_add = (int)(actor.Status.min_atk_bs * (0.14));
                max_matk_add = (int)(actor.Status.max_matk_bs * 0.14);
                min_matk_add = (int)(actor.Status.min_matk_bs * 0.14);
                avo_melee_add = (int)(actor.Status.avoid_ranged_bs * 0.06);
                avo_ranged_add = (int)(actor.Status.avoid_ranged_bs * 0.06);
                hit_melee_add = (int)(actor.Status.avoid_ranged_bs * 0.14);
                hit_ranged_add = (int)(actor.Status.avoid_ranged_bs * 0.14);
                left_def_add = (int)(actor.Status.def_bs * 0.03);
                //right_def_add = (int)(actor.Status.def * def);
                left_mdef_add = (int)(actor.Status.mdef_bs * 0.03);
                //right_mdef_add = (int)(actor.Status.mdef * def);
                hp_add = (int)(actor.MaxHP * 0.05);
                mp_add = (int)(actor.MaxMP * 0.05);
                sp_add = (int)(actor.MaxSP * 0.05);
            }
            else if (actorPC.PossessionPosition == SagaLib.PossessionPosition.LEFT_HAND )
            {
                max_atk_ori_add = (int)(actor.Status.max_atk_bs * (0.07));
                min_atk_ori_add = (int)(actor.Status.min_atk_bs * (0.07));
                max_matk_add = (int)(actor.Status.max_matk_bs * 0.07);
                min_matk_add = (int)(actor.Status.min_matk_bs * 0.05);
                avo_melee_add = (int)(actor.Status.avoid_ranged_bs * 0.14);
                avo_ranged_add = (int)(actor.Status.avoid_ranged_bs * 0.07);
                hit_melee_add = (int)(actor.Status.avoid_ranged_bs * 0.06);
                hit_ranged_add = (int)(actor.Status.avoid_ranged_bs * 0.06);
                left_def_add = (int)(actor.Status.def_bs * 0.13);
                //right_def_add = (int)(actor.Status.def * def);
                left_mdef_add = (int)(actor.Status.mdef_bs * 0.13);
                //right_mdef_add = (int)(actor.Status.mdef * def);
                hp_add = (int)(actor.MaxHP * 0.07);
                mp_add = (int)(actor.MaxMP * 0.07);
                sp_add = (int)(actor.MaxSP * 0.07);
            }
            else if (actorPC.PossessionPosition == SagaLib.PossessionPosition.NECK )
            {
                max_atk_ori_add = (int)(actor.Status.max_atk_bs * (0.08));
                min_atk_ori_add = (int)(actor.Status.min_atk_bs * (0.08));
                max_matk_add = (int)(actor.Status.max_matk_bs * 0.08);
                min_matk_add = (int)(actor.Status.min_matk_bs * 0.08);
                avo_melee_add = (int)(actor.Status.avoid_ranged_bs * 0.07);
                avo_ranged_add = (int)(actor.Status.avoid_ranged_bs * 0.14);
                hit_melee_add = (int)(actor.Status.avoid_ranged_bs * 0.09);
                hit_ranged_add = (int)(actor.Status.avoid_ranged_bs * 0.09);
                left_def_add = (int)(actor.Status.def_bs * 0.07);
                //right_def_add = (int)(actor.Status.def * def);
                left_mdef_add = (int)(actor.Status.mdef_bs * 0.07);
                //right_mdef_add = (int)(actor.Status.mdef * def);
                hp_add = (int)(actor.MaxHP * 0.07);
                mp_add = (int)(actor.MaxMP * 0.07);
                sp_add = (int)(actor.MaxSP * 0.07);
            }
            else if (actorPC.PossessionPosition == SagaLib.PossessionPosition.CHEST)
            {
                max_atk_ori_add = (int)(actor.Status.max_atk_bs  * (0.05));
                min_atk_ori_add = (int)(actor.Status.min_atk_bs  * (0.05));
                max_matk_add = (int)(actor.Status.max_matk_bs * 0.05);
                min_matk_add = (int)(actor.Status.min_matk_bs * 0.05);
                avo_melee_add = (int)(actor.Status.avoid_ranged_bs * 0.08);
                avo_ranged_add = (int)(actor.Status.avoid_ranged_bs * 0.06);
                hit_melee_add = (int)(actor.Status.avoid_ranged_bs * 0.05);
                hit_ranged_add = (int)(actor.Status.avoid_ranged_bs * 0.05);
                left_def_add = (int)(actor.Status.def_bs * 0.07);
                //right_def_add = (int)(actor.Status.def * def);
                left_mdef_add = (int)(actor.Status.mdef_bs * 0.07);
                //right_mdef_add = (int)(actor.Status.mdef * def);
                hp_add = (int)(actor.MaxHP * 0.14);
                mp_add = (int)(actor.MaxMP  * 0.14);
                sp_add = (int)(actor.MaxSP  * 0.14);
            }
            //最大魔攻
            if (skill.Variable.ContainsKey("Soul_Max_MAtk"))
                skill.Variable.Remove("Soul_Max_MAtk");
            skill.Variable.Add("Soul_Max_MAtk", max_matk_add);
            actor.Status.max_matk_possession = (short)max_matk_add;
            //最小魔攻
            if (skill.Variable.ContainsKey("Soul_Min_MAtk"))
                skill.Variable.Remove("Soul_Min_MAtk");
            skill.Variable.Add("Soul_Min_MAtk", min_matk_add);
            actor.Status.min_matk_possession = (short)min_matk_add;
            //近戰迴避
            if (skill.Variable.ContainsKey("Soul_avo_melee_add"))
                skill.Variable.Remove("Soul_avo_melee_add");
            skill.Variable.Add("Soul_avo_melee_add", avo_melee_add);
            actor.Status.avoid_melee_possession = (short)avo_melee_add;
            //遠距迴避
            if (skill.Variable.ContainsKey("Soul_avo_ranged_add"))
                skill.Variable.Remove("Soul_avo_ranged_add");
            skill.Variable.Add("Soul_avo_ranged_add", avo_ranged_add);
            actor.Status.avoid_ranged_possession = (short)avo_ranged_add;
            //近戰命中
            if (skill.Variable.ContainsKey("Soul_hit_melee_add"))
                skill.Variable.Remove("Soul_hit_melee_add");
            skill.Variable.Add("Soul_hit_melee_add", hit_melee_add);
            actor.Status.hit_melee_possession = (short)hit_melee_add;
            //遠距命中
            if (skill.Variable.ContainsKey("Soul_hit_ranged_add"))
                skill.Variable.Remove("Soul_hit_ranged_add");
            skill.Variable.Add("Soul_hit_ranged_add", hit_ranged_add);
            actor.Status.hit_ranged_possession = (short)hit_ranged_add;
            //左防禦力
            if (skill.Variable.ContainsKey("Soul_left_def_add)"))
                skill.Variable.Remove("Soul_left_def_add");
            skill.Variable.Add("Soul_left_def_add", left_def_add);
            actor.Status.def_possession = (short)left_def_add;
            //右防禦力
            if (skill.Variable.ContainsKey("Soul_right_def_add"))
                skill.Variable.Remove("Soul_right_def_add");
            skill.Variable.Add("Soul_right_def_add", right_def_add);
            actor.Status.def_add_possession = (short)right_def_add;
            //左魔法防禦
            if (skill.Variable.ContainsKey("Soul_left_mdef_add"))
                skill.Variable.Remove("Soul_left_mdef_add");
            skill.Variable.Add("Soul_left_mdef_add", left_mdef_add);
            actor.Status.mdef_possession = (short)left_mdef_add;
            //右魔法防禦
            if (skill.Variable.ContainsKey("Soul_right_mdef_add"))
                skill.Variable.Remove("Soul_right_mdef_add");
            skill.Variable.Add("Soul_right_mdef_add", right_mdef_add);
            actor.Status.mdef_add_possession = (short)right_mdef_add;
            //HP
            if (skill.Variable.ContainsKey("Soul_hp_add"))
                skill.Variable.Remove("Soul_hp_add");
            skill.Variable.Add("Soul_hp_add", hp_add);
            actor.Status.hp_possession = (short)hp_add;
            //MP
            if (skill.Variable.ContainsKey("Soul_mp_add"))
                skill.Variable.Remove("Soul_mp_add");
            skill.Variable.Add("Soul_mp_add", mp_add);
            actor.Status.mp_possession = (short)mp_add;
            //SP
            if (skill.Variable.ContainsKey("Soul_sp_add"))
                skill.Variable.Remove("Soul_sp_add");
            skill.Variable.Add("Soul_sp_add", sp_add);
            actor.Status.sp_possession = (short)sp_add;
            //大傷
            if (skill.Variable.ContainsKey("Soul_max_atk_ori_add"))
                skill.Variable.Remove("Soul_max_atk_ori_add");
            skill.Variable.Add("Soul_max_atk_ori_add", max_atk_ori_add);
            actor.Status.max_atk1_possession = (short)max_atk_ori_add;

            if (skill.Variable.ContainsKey("Soul_max_atk_ori_add"))
                skill.Variable.Remove("Soul_max_atk_ori_add");
            skill.Variable.Add("Soul_max_atk_ori_add", max_atk_ori_add);
            actor.Status.max_atk2_possession = (short)max_atk_ori_add;

            if (skill.Variable.ContainsKey("Soul_max_atk_ori_add"))
                skill.Variable.Remove("Soul_max_atk_ori_add");
            skill.Variable.Add("Soul_max_atk_ori_add", max_atk_ori_add);
            actor.Status.max_atk3_possession = (short)max_atk_ori_add;
            //小傷
            if (skill.Variable.ContainsKey("Soul_min_atk_ori_add"))
                skill.Variable.Remove("Soul_min_atk_ori_add");
            skill.Variable.Add("Soul_min_atk_ori_add", min_atk_ori_add);
            actor.Status.min_atk1_possession = (short)min_atk_ori_add;

            if (skill.Variable.ContainsKey("Soul_min_atk_ori_add"))
                skill.Variable.Remove("Soul_min_atk_ori_add");
            skill.Variable.Add("Soul_min_atk_ori_add", min_atk_ori_add);
            actor.Status.min_atk2_possession = (short)min_atk_ori_add;

            if (skill.Variable.ContainsKey("Soul_min_atk_ori_add"))
                skill.Variable.Remove("Soul_min_atk_ori_add");
            skill.Variable.Add("Soul_min_atk_ori_add", min_atk_ori_add);
            actor.Status.min_atk3_possession = (short)min_atk_ori_add;
        }
       #endregion
    }
}
