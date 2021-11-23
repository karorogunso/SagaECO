using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaLib;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations
{
    public class S40305 : ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            if (pc.Inventory.Equipments.ContainsKey(SagaDB.Item.EnumEquipSlot.LEFT_HAND))
            {
                if (pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.LEFT_HAND].BaseData.itemType != SagaDB.Item.ItemType.SHIELD)
                    return -5;
            }
            if (pc.Party == null) return -2;
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            List<Actor> targets = new List<Actor>();
            if (sActor.type == ActorType.PC)
            {
                ActorPC pc = (ActorPC)sActor;
                if (pc.Party == null) return;
                else
                {
                    Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
                    List<Actor> actors = map.GetActorsArea(sActor, 500, false);
                    foreach (var item in actors)
                    {
                        if (item.type == ActorType.PC)
                        {
                            if (((ActorPC)item).Party == pc.Party)
                                targets.Add(item);
                        }
                    }
                }
                foreach (var item in targets)
                {
                    OtherAddition oa = new OtherAddition(args.skill, item, "圣骑士的牺牲",30000);
                    oa.OnAdditionStart += Oa_OnAdditionStart;
                    oa.OnAdditionEnd += Oa_OnAdditionEnd;
                    SkillHandler.ApplyAddition(item, oa);
                    ((ActorPC)item).TInt["牺牲者ActorID"] = (int)sActor.ActorID;
                }
            }
        }

        private void Oa_OnAdditionEnd(Actor actor, OtherAddition skill)
        {
            Network.Client.MapClient.FromActorPC((ActorPC)actor).SendSystemMessage("受到了【圣骑士的牺牲】，靠近牺牲者时，你所受伤害将由牺牲者承担。");
        }

        private void Oa_OnAdditionStart(Actor actor, OtherAddition skill)
        {
            ((ActorPC)actor).TInt["牺牲者ActorID"] = 0;
        }
        #endregion
    }
}