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
    public class SpeedEnchant : ISkill
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
            DefaultBuff skill = new DefaultBuff(args.skill, dActor, "SpeedEnchant", 60000);
            skill.OnAdditionStart += this.StartEventHandler;
            skill.OnAdditionEnd += this.EndEventHandler;
            SkillHandler.ApplyAddition(dActor, skill);
        }
        void StartEventHandler(Actor actor, DefaultBuff skill)
        {
            int level = skill.skill.Level;
            float atk = -0.05f - level * 0.02f,
                avo = 0,
                def = 0,
                spd = 0.08f + level * 0.04f,
                delaycancel = new float[] { 0, 0.18f, 0.31f, 0.625f }[level];

            switch (level)
            {
                case 1:
                    avo = 0.08f;
                    def = -0.18f;
                    break;
                case 2:
                    avo = 0.12f;
                    def = -0.21f;
                    break;
                case 3:
                    avo = 0.16f;
                    def = -0.24f;
                    break;
            }
            //int atk_add = (int)(actor.Status.min_atk_ori * atk);
            //int matk_add = (int)(actor.Status.min_matk_ori * atk);
            //int def_add = (int)(actor.Status.def * def);
            //int mdef_add = (int)(actor.Status.mdef * def);

            ////最小攻擊
            //if (skill.Variable.ContainsKey("SpeedEnchant_atk"))
            //    skill.Variable.Remove("SpeedEnchant_atk");
            //skill.Variable.Add("SpeedEnchant_atk", atk_add);
            ////最小魔攻
            //if (skill.Variable.ContainsKey("SpeedEnchant_matk"))
            //    skill.Variable.Remove("SpeedEnchant_matk");
            //skill.Variable.Add("SpeedEnchant_matk", matk_add);
            ////左防禦力下降
            //if (skill.Variable.ContainsKey("SpeedEnchant_def"))
            //    skill.Variable.Remove("SpeedEnchant_def");
            //skill.Variable.Add("SpeedEnchant_def", def_add);
            ////左魔法防禦力
            //if (skill.Variable.ContainsKey("SpeedEnchant_mdef"))
            //    skill.Variable.Remove("SpeedEnchant_mdef");
            //skill.Variable.Add("SpeedEnchant_mdef", mdef_add);
            ////最小攻擊
            //actor.Status.min_atk1_skill += (short)atk_add;
            //actor.Status.min_atk2_skill += (short)atk_add;
            //actor.Status.min_atk3_skill += (short)atk_add;
            ////最小魔攻
            //actor.Status.min_matk_skill += (short)matk_add;
            ////左防禦力下降
            //actor.Status.def_skill += (short)def_add;
            ////左魔法防禦力
            //actor.Status.mdef_skill += (short)mdef_add;

            int aspd_add = (int)(actor.Status.aspd * spd);
            int cspd_add = (int)(actor.Status.cspd * spd);
            int avo_add = (int)(actor.Status.avoid_ranged * avo);

            //弓和枪享受的DC效果是其他武器的70%
            ActorPC pc = actor as ActorPC;
            if (pc.Inventory.Equipments.ContainsKey(SagaDB.Item.EnumEquipSlot.RIGHT_HAND))
            {
                if (pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].BaseData.itemType == SagaDB.Item.ItemType.BOW || 
                    pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].BaseData.itemType == SagaDB.Item.ItemType.GUN ||
                    pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].BaseData.itemType == SagaDB.Item.ItemType.DUALGUN ||
                    pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].BaseData.itemType == SagaDB.Item.ItemType.EXGUN )
                {
                    delaycancel *= 0.7f;
                }
            }

            //近戰迴避
            if (skill.Variable.ContainsKey("SpeedEnchant_savo"))
                skill.Variable.Remove("SpeedEnchant_savo");
            skill.Variable.Add("SpeedEnchant_savo", avo_add);
            //攻擊速度
            if (skill.Variable.ContainsKey("SpeedEnchant_aspd"))
                skill.Variable.Remove("SpeedEnchant_aspd");
            skill.Variable.Add("SpeedEnchant_aspd", (int)(delaycancel * 1000.0f));
            //施法速度
            if (skill.Variable.ContainsKey("SpeedEnchant_cspd"))
                skill.Variable.Remove("SpeedEnchant_cspd");
            skill.Variable.Add("SpeedEnchant_cspd", cspd_add);
            //魔法抵抗
            if (skill.Variable.ContainsKey("SpeedEnchant_magre"))
                skill.Variable.Remove("SpeedEnchant_magre");
            skill.Variable.Add("SpeedEnchant_magre", (int)(avo * 100.0f));
            //近戰迴避
            actor.Status.avoid_ranged_skill += (short)avo_add;
            //魔法抵抗
            actor.Status.MagicRuduceRate += avo;

            //攻擊速度
            actor.Status.aspd_skill_perc += delaycancel;
            //施法速度
            actor.Status.speedenchantcspdbonus += (short)cspd_add;

            actor.Buff.AttackSpeedUp = true;
            actor.Buff.CastSpeedUp = true;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }

        void EndEventHandler(Actor actor, DefaultBuff skill)
        {
            ////最小攻擊
            //actor.Status.min_atk1_skill -= (short)skill.Variable["SpeedEnchant_atk"];
            //actor.Status.min_atk2_skill -= (short)skill.Variable["SpeedEnchant_atk"];
            //actor.Status.min_atk3_skill -= (short)skill.Variable["SpeedEnchant_atk"];
            ////最小魔攻
            //actor.Status.min_matk_skill -= (short)skill.Variable["SpeedEnchant_matk"];
            ////左防禦力下降
            //actor.Status.def_skill -= (short)skill.Variable["SpeedEnchant_def"];
            ////左魔法防禦力
            //actor.Status.mdef_skill -= (short)skill.Variable["SpeedEnchant_mdef"];

            //近戰迴避
            actor.Status.avoid_ranged_skill -= (short)skill.Variable["SpeedEnchant_savo"];
            //攻擊速度
            actor.Status.aspd_skill_perc -= ((float)skill.Variable["SpeedEnchant_aspd"] / 1000.0f);
            //施法速度
            actor.Status.speedenchantcspdbonus -= (short)skill.Variable["SpeedEnchant_cspd"];
            //魔法抵抗
            actor.Status.MagicRuduceRate -= ((float)skill.Variable["SpeedEnchant_magre"] / 100.0f);

            actor.Buff.AttackSpeedUp = false;
            actor.Buff.CastSpeedUp = false;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
        #endregion
    }
}
