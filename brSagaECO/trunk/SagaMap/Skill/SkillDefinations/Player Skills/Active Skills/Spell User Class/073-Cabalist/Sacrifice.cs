using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
using SagaDB.Skill;
using SagaLib;
namespace SagaMap.Skill.SkillDefinations.Cabalist
{
    /// <summary>
    /// 献祭 (ソウルサケリファイス)
    /// </summary>
    public class Sacrifice : ISkill
    {
        DefaultBuff oldSacrifice = null;
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            if (sActor.HP < (uint)(sActor.MaxHP * 0.08 * args.skill.Level))
                return -12;
            else
                return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            Actor RealdActor = SkillHandler.Instance.GetPossesionedActor((ActorPC)sActor);
            float hpdmg = (float)(sActor.MaxHP * (0.08 * level));
            int lifetime = 24000 + 12000 * level;
            if (RealdActor.Status.Additions.ContainsKey("Sacrifice"))
                oldSacrifice = RealdActor.Status.Additions["Sacrifice"] as DefaultBuff;
            else
                oldSacrifice = null;
            DefaultBuff skill = new DefaultBuff(args.skill, RealdActor, "Sacrifice", lifetime);
            skill.OnAdditionStart += this.StartEventHandler;
            skill.OnAdditionEnd += this.EndEventHandler;
            SkillHandler.ApplyAddition(RealdActor, skill);
            sActor.HP -= (uint)hpdmg;
            SkillHandler.Instance.ShowVessel(sActor, (int)hpdmg);
        }
        void StartEventHandler(Actor actor, DefaultBuff skill)
        {
            int level = skill.skill.Level;
            int max_atk1_add = (int)(actor.Status.max_atk_ori * (0.8 + 0.4 * level));
            int max_atk2_add = (int)(actor.Status.max_atk_ori * (0.8 + 0.4 * level));
            int max_atk3_add = (int)(actor.Status.max_atk_ori * (0.8 + 0.4 * level));
            int min_atk1_add = (int)(actor.Status.min_atk_ori * (0.8 + 0.4 * level));
            int min_atk2_add = (int)(actor.Status.min_atk_ori * (0.8 + 0.4 * level));
            int min_atk3_add = (int)(actor.Status.min_atk_ori * (0.8 + 0.4 * level));
            int max_matk_add = (int)(actor.Status.max_matk_ori * (0.8 + 0.4 * level));
            int min_matk_add = (int)(actor.Status.min_matk_ori * (0.8 + 0.4 * level));
            int def_add = 5 * level;
            int mdef_add = 5 * level;

            if (oldSacrifice != null)
            {
                if (oldSacrifice.Variable["Sacrifice_Max_ATK1"] > max_atk1_add)
                {
                    max_atk1_add = oldSacrifice.Variable["Sacrifice_Max_ATK1"];
                    max_atk2_add = oldSacrifice.Variable["Sacrifice_Max_ATK2"];
                    max_atk3_add = oldSacrifice.Variable["Sacrifice_Max_ATK3"];
                    min_atk1_add = oldSacrifice.Variable["Sacrifice_Min_ATK1"];
                    min_atk2_add = oldSacrifice.Variable["Sacrifice_Min_ATK2"];
                    min_atk3_add = oldSacrifice.Variable["Sacrifice_Min_ATK3"];
                    max_matk_add = oldSacrifice.Variable["Sacrifice_Max_MATK"];
                    min_matk_add = oldSacrifice.Variable["Sacrifice_Min_MATK"];
                    def_add = oldSacrifice.Variable["Sacrifice_Def"];
                    mdef_add = oldSacrifice.Variable["Sacrifice_MDef"];
                }
            }

            //大傷
            if (skill.Variable.ContainsKey("Sacrifice_Max_ATK1"))
                skill.Variable.Remove("Sacrifice_Max_ATK1");
            skill.Variable.Add("Sacrifice_Max_ATK1", max_atk1_add);
            actor.Status.max_atk1_skill += (short)max_atk1_add;

            if (skill.Variable.ContainsKey("Sacrifice_Max_ATK2"))
                skill.Variable.Remove("Sacrifice_Max_ATK2");
            skill.Variable.Add("Sacrifice_Max_ATK2", max_atk2_add);
            actor.Status.max_atk2_skill += (short)max_atk2_add;

            if (skill.Variable.ContainsKey("Sacrifice_Max_ATK3"))
                skill.Variable.Remove("Sacrifice_Max_ATK3");
            skill.Variable.Add("Sacrifice_Max_ATK3", max_atk3_add);
            actor.Status.max_atk3_skill += (short)max_atk3_add;

            //小伤
            if (skill.Variable.ContainsKey("Sacrifice_Min_ATK1"))
                skill.Variable.Remove("Sacrifice_Min_ATK1");
            skill.Variable.Add("Sacrifice_Min_ATK1", min_atk1_add);
            actor.Status.min_atk1_skill += (short)min_atk1_add;

            if (skill.Variable.ContainsKey("Sacrifice_Min_ATK2"))
                skill.Variable.Remove("Sacrifice_Min_ATK2");
            skill.Variable.Add("Sacrifice_Min_ATK2", min_atk2_add);
            actor.Status.min_atk2_skill += (short)min_atk2_add;

            if (skill.Variable.ContainsKey("Sacrifice_Min_ATK3"))
                skill.Variable.Remove("Sacrifice_Min_ATK3");
            skill.Variable.Add("Sacrifice_Min_ATK3", min_atk3_add);
            actor.Status.min_atk3_skill += (short)min_atk3_add;
            //魔伤
            if (skill.Variable.ContainsKey("Sacrifice_Max_MATK"))
                skill.Variable.Remove("Sacrifice_Max_MATK");
            skill.Variable.Add("Sacrifice_Max_MATK", max_matk_add);
            actor.Status.max_matk_skill += (short)max_matk_add;

            if (skill.Variable.ContainsKey("Sacrifice_Min_MATK"))
                skill.Variable.Remove("Sacrifice_Min_MATK");
            skill.Variable.Add("Sacrifice_Min_MATK", min_matk_add);
            actor.Status.min_matk_skill += (short)min_matk_add;

            //防御
            if (skill.Variable.ContainsKey("Sacrifice_Def"))
                skill.Variable.Remove("Sacrifice_Def");
            skill.Variable.Add("Sacrifice_Def", def_add);
            actor.Status.def_add_skill += (short)def_add;

            if (skill.Variable.ContainsKey("Sacrifice_MDef"))
                skill.Variable.Remove("Sacrifice_MDef");
            skill.Variable.Add("Sacrifice_MDef", mdef_add);
            actor.Status.mdef_add_skill += (short)mdef_add;

            actor.Buff.MaxAtkUp = true;
            actor.Buff.MinAtkUp = true;
            actor.Buff.MinMagicAtkUp = true;
            actor.Buff.MaxMagicAtkUp = true;
            actor.Buff.DefUp = true;
            actor.Buff.MagicDefUp = true;
            actor.Buff.NoRegen = true;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
        void EndEventHandler(Actor actor, DefaultBuff skill)
        {
            //大傷
            actor.Status.max_atk1_skill -= (short)skill.Variable["Sacrifice_Max_ATK1"];
            actor.Status.max_atk2_skill -= (short)skill.Variable["Sacrifice_Max_ATK2"];
            actor.Status.max_atk3_skill -= (short)skill.Variable["Sacrifice_Max_ATK3"];
            //小傷
            actor.Status.min_atk1_skill -= (short)skill.Variable["Sacrifice_Min_ATK1"];
            actor.Status.min_atk2_skill -= (short)skill.Variable["Sacrifice_Min_ATK2"];
            actor.Status.min_atk3_skill -= (short)skill.Variable["Sacrifice_Min_ATK3"];
            //魔伤
            actor.Status.max_matk_skill -= (short)skill.Variable["Sacrifice_Max_MATK"];
            actor.Status.min_matk_skill -= (short)skill.Variable["Sacrifice_Min_MATK"];
            //防御
            actor.Status.def_add_skill -= (short)skill.Variable["Sacrifice_Def"];
            actor.Status.mdef_add_skill -= (short)skill.Variable["Sacrifice_MDef"];

            actor.Buff.MaxAtkUp = false;
            actor.Buff.MinAtkUp = false;
            actor.Buff.MinMagicAtkUp = false;
            actor.Buff.MaxMagicAtkUp = false;
            actor.Buff.DefUp = false;
            actor.Buff.MagicDefUp = false;
            actor.Buff.NoRegen = false;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);

        }
    }
}