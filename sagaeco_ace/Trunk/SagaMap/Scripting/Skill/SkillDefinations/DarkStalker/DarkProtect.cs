
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.DarkStalker
{
    /// <summary>
    /// 黑暗的加護（ダークプロテクション）
    /// </summary>
    public class DarkProtect : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            bool active = false;
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            if (map.Info.dark[args.x, args.y] > 0)
            {
                active = true;
            }
            DefaultPassiveSkill skill = new DefaultPassiveSkill(args.skill, sActor, "DarkProtect", active);
            skill.OnAdditionStart += this.StartEventHandler;
            skill.OnAdditionEnd += this.EndEventHandler;
            SkillHandler.ApplyAddition(sActor, skill);
        }
        void StartEventHandler(Actor actor, DefaultPassiveSkill skill)
        {
            int level = skill.skill.Level;

            //最小攻擊
            int min_atk1_add = (int)(5 + 3 * level);
            if (skill.Variable.ContainsKey("DarkProtect_min_atk1"))
                skill.Variable.Remove("DarkProtect_min_atk1");
            skill.Variable.Add("DarkProtect_min_atk1", min_atk1_add);
            actor.Status.min_atk1_skill += (short)min_atk1_add;

            //最小攻擊
            int min_atk2_add = (int)(5 + 3 * level);
            if (skill.Variable.ContainsKey("DarkProtect_min_atk2"))
                skill.Variable.Remove("DarkProtect_min_atk2");
            skill.Variable.Add("DarkProtect_min_atk2", min_atk2_add);
            actor.Status.min_atk2_skill += (short)min_atk2_add;

            //最小攻擊
            int min_atk3_add = (int)(5 + 3 * level);
            if (skill.Variable.ContainsKey("DarkProtect_min_atk3"))
                skill.Variable.Remove("DarkProtect_min_atk3");
            skill.Variable.Add("DarkProtect_min_atk3", min_atk3_add);
            actor.Status.min_atk3_skill += (short)min_atk3_add;

            //最小魔攻
            int min_matk_add = (int)(5 + 3 * level);
            if (skill.Variable.ContainsKey("DarkProtect_min_matk"))
                skill.Variable.Remove("DarkProtect_min_matk");
            skill.Variable.Add("DarkProtect_min_matk", min_matk_add);
            actor.Status.min_matk_skill += (short)min_matk_add;

            //右防禦
            int def_add_add = (int)(5 + 2 * level);
            if (skill.Variable.ContainsKey("DarkProtect_def_add"))
                skill.Variable.Remove("DarkProtect_def_add");
            skill.Variable.Add("DarkProtect_def_add", def_add_add);
            actor.Status.def_add_skill += (short)def_add_add;

            //右魔防
            int mdef_add_add = (int)(5 + 2 * level);
            if (skill.Variable.ContainsKey("DarkProtect_mdef_add"))
                skill.Variable.Remove("DarkProtect_mdef_add");
            skill.Variable.Add("DarkProtect_mdef_add", mdef_add_add);
            actor.Status.mdef_add_skill += (short)mdef_add_add;
                                        
        }
        void EndEventHandler(Actor actor, DefaultPassiveSkill skill)
        {
            //最小攻擊
            actor.Status.min_atk1_skill -= (short)skill.Variable["DarkProtect_min_atk1"];

            //最小攻擊
            actor.Status.min_atk2_skill -= (short)skill.Variable["DarkProtect_min_atk2"];

            //最小攻擊
            actor.Status.min_atk3_skill -= (short)skill.Variable["DarkProtect_min_atk3"];

            //最小魔攻
            actor.Status.min_matk_skill -= (short)skill.Variable["DarkProtect_min_matk"];

            //右防禦
            actor.Status.def_add_skill -= (short)skill.Variable["DarkProtect_def_add"];

            //右魔防
            actor.Status.mdef_add_skill -= (short)skill.Variable["DarkProtect_mdef_add"];
              
        }
        #endregion
    }
}

