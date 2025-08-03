using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations.Archer
{
    /// <summary>
    /// 飄渺之境
    /// </summary>
    public class LHitUp : ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            if (CheckPossible(pc))
                return 0;
            else
                return -5;
        }

        bool CheckPossible(Actor sActor)
        {
            if (sActor.type == ActorType.PC)
            {
                ActorPC pc = (ActorPC)sActor;
                pc = SkillHandler.Instance.GetPossesionedActor((ActorPC)sActor);
                if (pc.Inventory.Equipments.ContainsKey(SagaDB.Item.EnumEquipSlot.RIGHT_HAND))
                {
                    if (pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].BaseData.itemType == SagaDB.Item.ItemType.BOW ||
                        pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].BaseData.itemType == SagaDB.Item.ItemType.GUN ||
                        pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].BaseData.itemType == SagaDB.Item.ItemType.RIFLE ||
                        pc.Inventory.GetContainer(SagaDB.Item.ContainerType.RIGHT_HAND2).Count > 0)
                        return true;
                    else
                        return false;
                }
                else
                    return false;
            }
            else
                return true;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            Actor realdActor = SkillHandler.Instance.GetPossesionedActor((ActorPC)sActor);
            int life = 0;
            life = 180000;
            DefaultBuff skill = new DefaultBuff(args.skill, realdActor, "MarkMannAura", life);
            skill.OnAdditionStart += this.StartEventHandler;
            skill.OnAdditionEnd += this.EndEventHandler;
            SkillHandler.ApplyAddition(realdActor, skill);
        }

        void StartEventHandler(Actor actor, DefaultBuff skill)
        {

            short Lhit = (short)(6 * skill.skill.Level);
            if (skill.Variable.ContainsKey("MarkMannAura_LHit"))
                skill.Variable.Remove("MarkMannAura_LHit");
            skill.Variable.Add("MarkMannAura_LHit", Lhit);
            actor.Status.hit_ranged_skill += Lhit;
            if (actor.type == ActorType.PC)
            {
                SagaMap.Network.Client.MapClient.FromActorPC((ActorPC)actor).SendSystemMessage("受到名射手的光环的效果");
            }
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }

        void EndEventHandler(Actor actor, DefaultBuff skill)
        {
            actor.Status.hit_ranged_skill -= (short)skill.Variable["MarkMannAura_LHit"];
            if (actor.type == ActorType.PC)
            {
                SagaMap.Network.Client.MapClient.FromActorPC((ActorPC)actor).SendSystemMessage("名射手的光环的效果解除");
            }
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
        #endregion
    }
}
