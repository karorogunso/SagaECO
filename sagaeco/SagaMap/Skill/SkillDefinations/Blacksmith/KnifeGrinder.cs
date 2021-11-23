
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Blacksmith
{
    /// <summary>
    /// 閃電刀刃（ウェットウエポン）
    /// </summary>
    public class KnifeGrinder : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            ActorPC pc = (ActorPC)dActor;
            if (pc.Inventory.Equipments.ContainsKey(SagaDB.Item.EnumEquipSlot.RIGHT_HAND))
            {
                if (pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].BaseData.itemType == SagaDB.Item.ItemType.SHORT_SWORD || pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].BaseData.itemType == SagaDB.Item.ItemType.SWORD)
                {
                    int[] lifetimes={0,30000,40000,60000};
                    int lifetime = lifetimes[level];
                    DefaultBuff skill = new DefaultBuff(args.skill, dActor, "KnifeGrinder", lifetime);
                    skill.OnAdditionStart += this.StartEventHandler;
                    skill.OnAdditionEnd += this.EndEventHandler;
                    SkillHandler.ApplyAddition(dActor, skill);
                }
            }
        }
        void StartEventHandler(Actor actor, DefaultBuff skill)
        {


            //最大攻擊
            int max_atk1_add = 30;
            if (skill.Variable.ContainsKey("KnifeGrinder_max_atk1"))
                skill.Variable.Remove("KnifeGrinder_max_atk1");
            skill.Variable.Add("KnifeGrinder_max_atk1", max_atk1_add);
            actor.Status.max_atk1_skill += (short)max_atk1_add;

            //最大攻擊
            int max_atk2_add = 30;
            if (skill.Variable.ContainsKey("KnifeGrinder_max_atk2"))
                skill.Variable.Remove("KnifeGrinder_max_atk2");
            skill.Variable.Add("KnifeGrinder_max_atk2", max_atk2_add);
            actor.Status.max_atk2_skill += (short)max_atk2_add;

            //最大攻擊
            int max_atk3_add = 30;
            if (skill.Variable.ContainsKey("KnifeGrinder_max_atk3"))
                skill.Variable.Remove("KnifeGrinder_max_atk3");
            skill.Variable.Add("KnifeGrinder_max_atk3", max_atk3_add);
            actor.Status.max_atk3_skill += (short)max_atk3_add;

            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
                                        

        }
        void EndEventHandler(Actor actor, DefaultBuff skill)
        {
            //最大攻擊
            actor.Status.max_atk1_skill -= (short)skill.Variable["KnifeGrinder_max_atk1"];

            //最大攻擊
            actor.Status.max_atk2_skill -= (short)skill.Variable["KnifeGrinder_max_atk2"];

            //最大攻擊
            actor.Status.max_atk3_skill -= (short)skill.Variable["KnifeGrinder_max_atk3"];

            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);

        }
        #endregion
    }
}
