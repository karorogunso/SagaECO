using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaMap.Skill.Additions.Global;
using SagaDB.Actor;
using SagaLib;
using SagaMap.Mob;

namespace SagaMap.Skill.SkillDefinations
{
    public class S11110 : ISkill
    {
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            if (!pc.Inventory.Equipments.ContainsKey(SagaDB.Item.EnumEquipSlot.LEFT_HAND) || pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.LEFT_HAND].BaseData.itemType != SagaDB.Item.ItemType.SHIELD)
            {
                return -5;
            }
            if (pc.Status.Additions.ContainsKey("圣盾加护CD") && pc.Account.GMLevel < 200)
            {
                Network.Client.MapClient.FromActorPC(pc).SendSystemMessage("『圣盾加护』冷却中");
                return -30;
            }
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            ActorPC pc = (ActorPC)sActor;
            List<Actor> targets = new List<Actor>();
            if (pc.Party == null)
                targets.Add(sActor);
            else
            {
                List<Actor> actors = map.GetActorsArea(sActor, 800, true);

                foreach (var item in actors)
                {
                    if (item.type == ActorType.PC)
                    {
                        ActorPC tpc = (ActorPC)item;
                        if (tpc.Mode == pc.Mode && tpc.HP > 0 && tpc.Online)
                            targets.Add(tpc);
                    }
                }
            }
            float rate = 0.1f + 0.2f * level;
            uint shieldHp = (uint)(sActor.MaxHP * rate);
            foreach (var item in targets)
            {
                if (!item.Status.Additions.ContainsKey("圣盾加护CD"))
                {
                    SkillHandler.Instance.ShowEffectOnActor(item, 4254, sActor);
                    item.SHIELDHP = shieldHp;
                    OtherAddition skill = new OtherAddition(null, item, "圣盾加护", 60000);
                    skill.OnAdditionStart += (s, e) =>
                    {
                        if(item.type == ActorType.PC)
                        Network.Client.MapClient.FromActorPC((ActorPC)item).SendSystemMessage("从 "+ sActor.Name +" 获得了护盾值： "+ shieldHp + " 的『圣盾加护』效果。");
                        item.Buff.三转DEF增强 = true;
                        map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, item, true);
                    };
                    skill.OnAdditionEnd += (s, e) =>
                    {
                        item.SHIELDHP = 0;
                        item.Buff.三转DEF增强 = false;
                        map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, item, true);
                    };
                    SkillHandler.ApplyAddition(item, skill);

                    DefaultBuff skill2 = new DefaultBuff(args.skill, item, "圣盾加护CD", 60000);
                    skill2.OnAdditionEnd += (s, e) =>
                    {
                        SkillHandler.Instance.ShowEffect(map, item, 4267);
                        Network.Client.MapClient.FromActorPC((ActorPC)item).SendSystemMessage("『圣盾加护』冷却结束！");
                    };
                    SkillHandler.ApplyAddition(item, skill2);
                }
            }
        }
    }
}
