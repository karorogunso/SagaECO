using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaLib;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations
{
    public class S40300 : ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            if (pc.Inventory.Equipments.ContainsKey(SagaDB.Item.EnumEquipSlot.LEFT_HAND))
            {
                if (pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.LEFT_HAND].BaseData.itemType != SagaDB.Item.ItemType.SHIELD)
                    return -5;
            }
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            
            int result = SkillHandler.Instance.PhysicalAttack(sActor, dActor, args, Elements.Neutral, 1.5f);


            if(result > 0)//若造成伤害
            {
                Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
                if (dActor.Tasks.ContainsKey("SkillCast"))
                {
                    if (dActor.Tasks["SkillCast"].getActivated())
                    {
                        dActor.Tasks["SkillCast"].Deactivate();
                        dActor.Tasks.Remove("SkillCast");
                    }

                    SkillArg arg = new SkillArg();
                    arg.sActor = sActor.ActorID;
                    arg.dActor = dActor.ActorID;
                    arg.skill = SagaDB.Skill.SkillFactory.Instance.GetSkill(3311, level);
                    arg.x = 0;
                    arg.y = 0;
                    arg.hp = new List<int>();
                    arg.sp = new List<int>();
                    arg.mp = new List<int>();
                    arg.hp.Add(0);
                    arg.sp.Add(0);
                    arg.mp.Add(0);
                    arg.flag.Add(AttackFlag.NONE);
                    //arg.affectedActors.Add(this.Character);
                    arg.argType = SkillArg.ArgType.Active;
                    map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.SKILL, arg, dActor, true);
                }
            }
        }
        #endregion
    }
}