using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations.Enchanter
{
    /// <summary>
    /// 快速的精靈勢力（スピードエンチャント）
    /// </summary>
    public class SpdUp_AvoUp_AtkDown_DefDown : ISkill
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

            DefaultBuff skill = new DefaultBuff(args.skill, dActor, "SpdUp_AvoUp_AtkDown_DefDown", 30000);
            skill.OnAdditionStart += this.StartEventHandler;
            skill.OnAdditionEnd += this.EndEventHandler;
            SkillHandler.ApplyAddition(dActor, skill);
        }
        void StartEventHandler(Actor actor, DefaultBuff skill)
        {
            int level=skill.skill.Level;
            float atk= -0.05f - level * 0.02f,avo=0,def=0,spd=0.08f+level*0.04f;

            switch (level)
            {
                case 1:
                    avo=0.08f;
                    def=-0.18f;
                    break;
                case 2:
                    avo=0.14f;
                    def=-0.21f;
                    break;
                case 3:
                    avo=0.17f;
                    def = -0.24f;
                    break;
            }
            int atk_add=(int)(actor.Status.min_atk_ori * atk);
            int matk_add = (int)(actor.Status.min_matk_ori * atk);
            int avo_add =(int)(actor.Status.avoid_ranged * avo);
            int def_add=(int)(actor.Status.def * def);
            int mdef_add = (int)(actor.Status.mdef * def);
            int aspd_add = (int)(actor.Status.aspd * spd);
            int cspd_add = (int)(actor.Status.cspd * spd);
            //最小攻擊
            if (skill.Variable.ContainsKey("SpdUp_AvoUp_AtkDown_DefDown_atk"))
                skill.Variable.Remove("SpdUp_AvoUp_AtkDown_DefDown_atk");
            skill.Variable.Add("SpdUp_AvoUp_AtkDown_DefDown_atk", atk_add);
            //最小魔攻
            if (skill.Variable.ContainsKey("SpdUp_AvoUp_AtkDown_DefDown_matk"))
                skill.Variable.Remove("SpdUp_AvoUp_AtkDown_DefDown_matk");
            skill.Variable.Add("SpdUp_AvoUp_AtkDown_DefDown_matk", matk_add);
            //近戰迴避
            if (skill.Variable.ContainsKey("SpdUp_AvoUp_AtkDown_DefDown_avo"))
                skill.Variable.Remove("SpdUp_AvoUp_AtkDown_DefDown_avo");
            skill.Variable.Add("SpdUp_AvoUp_AtkDown_DefDown_avo", avo_add);
            //MP回復率
            if (skill.Variable.ContainsKey("SpdUp_AvoUp_AtkDown_DefDown_mp_recover_skill"))
                skill.Variable.Remove("SpdUp_AvoUp_AtkDown_DefDown_mp_recover_skill");
            skill.Variable.Add("SpdUp_AvoUp_AtkDown_DefDown_mp_recover_skill", (int)(avo*100));
            //左防禦力下降
            if (skill.Variable.ContainsKey("SpdUp_AvoUp_AtkDown_DefDown_def"))
                skill.Variable.Remove("SpdUp_AvoUp_AtkDown_DefDown_def");
            skill.Variable.Add("SpdUp_AvoUp_AtkDown_DefDown_def", def_add);
            //左魔法防禦力
            if (skill.Variable.ContainsKey("SpdUp_AvoUp_AtkDown_DefDown_mdef"))
                skill.Variable.Remove("SpdUp_AvoUp_AtkDown_DefDown_mdef");
            skill.Variable.Add("SpdUp_AvoUp_AtkDown_DefDown_mdef", mdef_add);
            //攻擊速度
            if (skill.Variable.ContainsKey("SpdUp_AvoUp_AtkDown_DefDown_aspd"))
                skill.Variable.Remove("SpdUp_AvoUp_AtkDown_DefDown_aspd");
            skill.Variable.Add("SpdUp_AvoUp_AtkDown_DefDown_aspd", aspd_add);
            //施法速度
            if (skill.Variable.ContainsKey("SpdUp_AvoUp_AtkDown_DefDown_cspd"))
                skill.Variable.Remove("SpdUp_AvoUp_AtkDown_DefDown_cspd");
            skill.Variable.Add("SpdUp_AvoUp_AtkDown_DefDown_cspd", cspd_add);
            
            //最小攻擊
            actor.Status.min_atk1_skill += (short)atk_add;
            actor.Status.min_atk2_skill += (short)atk_add;
            actor.Status.min_atk3_skill += (short)atk_add;
            //最小魔攻
            actor.Status.min_matk_skill += (short)matk_add;
            //近戰迴避
            actor.Status.avoid_ranged_skill += (short)avo_add;
            //MP回復率
            actor.Status.mp_recover_skill += (short)(avo * 100);
            //左防禦力下降
            actor.Status.def_skill += (short)def_add;
            //左魔法防禦力
            actor.Status.mdef_skill += (short)mdef_add;
            //攻擊速度
            actor.Status.aspd_skill += (short)aspd_add;
            //施法速度
            actor.Status.cspd_skill  += (short)cspd_add;

            actor.Buff.攻撃スピード上昇 = true;
            actor.Buff.詠唱スピード上昇 = true;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }

        void EndEventHandler(Actor actor, DefaultBuff skill)
        {
            //最小攻擊
            actor.Status.min_atk1_skill -= (short)skill.Variable["SpdUp_AvoUp_AtkDown_DefDown_atk"];
            actor.Status.min_atk2_skill -= (short)skill.Variable["SpdUp_AvoUp_AtkDown_DefDown_atk"];
            actor.Status.min_atk3_skill -= (short)skill.Variable["SpdUp_AvoUp_AtkDown_DefDown_atk"];
            //最小魔攻
            actor.Status.min_matk_skill -= (short)skill.Variable["SpdUp_AvoUp_AtkDown_DefDown_matk"];
            //近戰迴避
            actor.Status.avoid_ranged_skill -= (short)skill.Variable["SpdUp_AvoUp_AtkDown_DefDown_avo"];
            //MP回復率
            actor.Status.mp_recover_skill -= (short)skill.Variable["SpdUp_AvoUp_AtkDown_DefDown_mp_recover_skill"];
            //左防禦力下降
            actor.Status.def_skill -= (short)skill.Variable["SpdUp_AvoUp_AtkDown_DefDown_def"];
            //左魔法防禦力
            actor.Status.mdef_skill -= (short)skill.Variable["SpdUp_AvoUp_AtkDown_DefDown_mdef"];
            //攻擊速度
            actor.Status.aspd_skill -= (short)skill.Variable["SpdUp_AvoUp_AtkDown_DefDown_aspd"];
            //施法速度
            actor.Status.cspd_skill -= (short)skill.Variable["SpdUp_AvoUp_AtkDown_DefDown_cspd"];

            actor.Buff.攻撃スピード上昇 = false;
            actor.Buff.詠唱スピード上昇 = false;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
        #endregion
    }
}
