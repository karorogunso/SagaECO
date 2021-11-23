using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaDB.Skill;

namespace SagaMap.Skill.Additions.Global
{
    public class Fish : DefaultBuff
    {
        private bool isMarionette = false;
        private bool isFiset = false;
        public Fish(SagaDB.Skill.Skill skill, Actor actor, int lifetime, int period)
            : base(skill, actor, "fish", lifetime, period)
        {
            this.OnAdditionStart += this.StartEvent;
            this.OnAdditionEnd += this.EndEvent;
            this.OnUpdate += this.TimerUpdate;
        }

        void StartEvent(Actor actor, DefaultBuff skill)
        {
            isFiset = true;
            Map map = Manager.MapManager.Instance.GetMap(actor.MapID);
            actor.Buff.FishingState = true;
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }

        void EndEvent(Actor actor, DefaultBuff skill)
        {
            Map map = Manager.MapManager.Instance.GetMap(actor.MapID);
            actor.Buff.FishingState = false;
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }

        void TimerUpdate(Actor actor, DefaultBuff skill)
        {
            if (isFiset)
            {
                isFiset = false;
            }
            else
            {
                SagaMap.Network.Client.MapClient client = SagaMap.Network.Client.MapClient.FromActorPC((ActorPC)actor);
                Packets.Server.SSMG_FISHING_RESULT p = new Packets.Server.SSMG_FISHING_RESULT();
                SagaDB.Item.Item bait = client.Character.Inventory.GetItem(10104900, SagaDB.Item.Inventory.SearchType.ITEM_ID);

                if (((ActorPC)actor).Inventory.Equipments.ContainsKey(SagaDB.Item.EnumEquipSlot.RIGHT_HAND))
                {
                    SkillHandler.Instance.WeaponWorn((ActorPC)actor);
                    if (bait != null)
                    {
                        client.DeleteItemID(bait.ItemID, 1, false);
                        if (SagaLib.Global.Random.Next(0, 100) > 90)//lose
                        {
                            p.ItemID = 0;
                            p.IsSucceed = 0;
                            if(SagaLib.Global.Random.Next(0,60) == 2)
                            {
                                Map map = Manager.MapManager.Instance.GetMap(actor.MapID);
                                map.SpawnMob(26100000, client.Character.X, client.Character.Y, 5000, null);
                                client.SendSystemMessage("好像钓出了怪物！！");
                            }
                        }
                        else
                        {
                            SagaDB.Fish.Fish fish = null;
                            fish = SagaDB.Fish.FishFactory.Instance.GetRandomItem("钓鱼");
                            p.ItemID = fish.ID;
                            p.IsSucceed = 2;

                            SagaDB.Item.Item item = SagaDB.Item.ItemFactory.Instance.GetItem(fish.ID);
                            client.AddItem(item, false);
                        }
                        client.netIO.SendPacket(p);
                    }
                    else
                    {
                        SkillHandler.RemoveAddition(actor, "fish");
                    }
                }
                else
                {
                    SkillHandler.RemoveAddition(actor, "fish");
                }
            }
        }
    }
}
