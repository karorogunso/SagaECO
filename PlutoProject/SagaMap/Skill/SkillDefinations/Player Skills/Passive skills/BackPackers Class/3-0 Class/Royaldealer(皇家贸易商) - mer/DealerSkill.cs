using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
using SagaDB.Item;

namespace SagaMap.Skill.SkillDefinations.Royaldealer
{
    /// <summary>
    /// ブレイドマスタリー
    /// </summary>
    public class DealerSkill : ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            if (SkillHandler.Instance.isEquipmentRight(pc, ItemType.CARD))
            {
                return 0;
            }
            return -14;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            bool active = false;
            if (sActor.type == ActorType.PC)
            {
                ActorPC pc = (ActorPC)sActor;
                if (pc.Inventory.Equipments.ContainsKey(SagaDB.Item.EnumEquipSlot.RIGHT_HAND))
                {
                    if (pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].BaseData.itemType == SagaDB.Item.ItemType.CARD)
                    {
                        active = true;
                    }
                }
                DefaultPassiveSkill skill = new DefaultPassiveSkill(args.skill, dActor, "DealerSkill", active);
                skill.OnAdditionStart += this.StartEventHandler;
                skill.OnAdditionEnd += this.EndEventHandler;
                SkillHandler.ApplyAddition(dActor, skill);
            }
        }

        void StartEventHandler(Actor actor, DefaultPassiveSkill skill)
        {
            int MaxAttack = 70 + 22 * skill.skill.Level;
            int MinAttack = MaxAttack;

            //最大攻擊
            int max_atk1_add = (int)(MaxAttack);
            if (skill.Variable.ContainsKey("Royal_max_atk1"))
                skill.Variable.Remove("Royal_max_atk1");
            skill.Variable.Add("Royal_max_atk1", max_atk1_add);
            actor.Status.max_atk1_skill += (short)max_atk1_add;

            //最大攻擊
            int max_atk2_add = (int)(MaxAttack);
            if (skill.Variable.ContainsKey("Royal_max_atk2"))
                skill.Variable.Remove("Royal_max_atk2");
            skill.Variable.Add("Royal_max_atk2", max_atk2_add);
            actor.Status.max_atk2_skill += (short)max_atk2_add;

            //最大攻擊
            int max_atk3_add = (int)(MaxAttack);
            if (skill.Variable.ContainsKey("Royal_max_atk3"))
                skill.Variable.Remove("Royal_max_atk3");
            skill.Variable.Add("Royal_max_atk3", max_atk3_add);
            actor.Status.max_atk3_skill += (short)max_atk3_add;

            //最小攻擊
            int min_atk1_add = (int)(MinAttack);
            if (skill.Variable.ContainsKey("Royal_min_atk1"))
                skill.Variable.Remove("Royal_min_atk1");
            skill.Variable.Add("Royal_min_atk1", min_atk1_add);
            actor.Status.min_atk1_skill += (short)min_atk1_add;

            //最小攻擊
            int min_atk2_add = (int)(MinAttack);
            if (skill.Variable.ContainsKey("Royal_min_atk2"))
                skill.Variable.Remove("Royal_min_atk2");
            skill.Variable.Add("Royal_min_atk2", min_atk2_add);
            actor.Status.min_atk2_skill += (short)min_atk2_add;

            //最小攻擊
            int min_atk3_add = (int)(MinAttack);
            if (skill.Variable.ContainsKey("Royal_min_atk3"))
                skill.Variable.Remove("Royal_min_atk3");
            skill.Variable.Add("Royal_min_atk3", min_atk3_add);
            actor.Status.min_atk3_skill += (short)min_atk3_add;

            //远命中
            int hit_ranged_add = MaxAttack;
            if (skill.Variable.ContainsKey("Royal_hit_ranged"))
                skill.Variable.Remove("Royal_hit_ranged");
            skill.Variable.Add("Royal_hit_ranged", hit_ranged_add);
            actor.Status.hit_ranged_skill += (short)hit_ranged_add;

            //爆擊率
            int cri_add = 10 + 2 * skill.skill.Level;
            if (skill.Variable.ContainsKey("Royal_cri"))
                skill.Variable.Remove("Royal_cri");
            skill.Variable.Add("Royal_cri", cri_add);
            actor.Status.cri_skill += (short)cri_add;

            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.CHANGE_STATUS, null, actor, true);
        }

        void EndEventHandler(Actor actor, DefaultPassiveSkill skill)
        {
            //最大攻擊
            actor.Status.max_atk1_skill -= (short)skill.Variable["Royal_max_atk1"];

            //最大攻擊
            actor.Status.max_atk2_skill -= (short)skill.Variable["Royal_max_atk2"];

            //最大攻擊
            actor.Status.max_atk3_skill -= (short)skill.Variable["Royal_max_atk3"];

            //最小攻擊
            actor.Status.min_atk1_skill -= (short)skill.Variable["Royal_min_atk1"];

            //最小攻擊
            actor.Status.min_atk2_skill -= (short)skill.Variable["Royal_min_atk2"];

            //最小攻擊
            actor.Status.min_atk3_skill -= (short)skill.Variable["Royal_min_atk3"];

            //近命中
            actor.Status.hit_ranged_skill -= (short)skill.Variable["Royal_hit_ranged"];

            //爆擊率
            actor.Status.cri_skill -= (short)skill.Variable["Royal_cri"];

            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.CHANGE_STATUS, null, actor, true);
        }

        #endregion
    }
}
