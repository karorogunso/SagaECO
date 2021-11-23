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
            actor.Buff.钓鱼状态 = true;
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }

        void EndEvent(Actor actor, DefaultBuff skill)
        {
            Map map = Manager.MapManager.Instance.GetMap(actor.MapID);
            actor.Buff.钓鱼状态 = false;
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
                SagaDB.Item.Item bait = client.Character.Inventory.GetItem(110104900, SagaDB.Item.Inventory.SearchType.ITEM_ID);

                if (((ActorPC)actor).Inventory.Equipments.ContainsKey(SagaDB.Item.EnumEquipSlot.RIGHT_HAND))
                {
                    SagaDB.Item.Item itemE = ((ActorPC)actor).Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND];
                    if (itemE.Durability > 0)
                    {
                        bool worn = true;
                        if (itemE.ItemID == 160088300 || itemE.ItemID == 160088400 || itemE.ItemID == 160088900 || itemE.ItemID == 160089000)
                            if (SagaLib.Global.Random.Next(0, 100) > 20)
                                worn = false;
                        if(itemE.ItemID == 160088500 || itemE.ItemID == 160088600)
                            if (SagaLib.Global.Random.Next(0, 100) > 60)
                                worn = false;
                        if (((ActorPC)actor).Account.GMLevel >= 200)
                            worn = false;
                        if (DateTime.Now.Month == 11 && DateTime.Now.Day >= 17 && DateTime.Now.Day <= 30 && DateTime.Now.Year == 2017)
                        {
                            if (SagaLib.Global.Random.Next(0, 100) > 50)
                                worn = false;
                        }
                        if (worn)
                        SkillHandler.Instance.WeaponWorn((ActorPC)actor);


                        if (bait != null)
                        {
                            client.DeleteItemID(bait.ItemID, 1, false);
                            Period = SagaLib.Global.Random.Next(5000, 20000);
                            if (itemE.ItemID == 160088500)
                                Period = SagaLib.Global.Random.Next(3000, 10000);
                            if (itemE.ItemID == 160088600)
                                Period = SagaLib.Global.Random.Next(3000, 8000);
                            SagaDB.Fish.Fish fish = null;
                            fish = SagaDB.Fish.FishFactory.Instance.GetRandomItem("钓鱼");
                            p.ItemID = fish.ID;
                            p.IsSucceed = 2;


                            SagaDB.Item.Item item = SagaDB.Item.ItemFactory.Instance.GetItem(fish.ID);
                            client.AddItem(item, false);
                            if (DateTime.Now.Month == 11 && DateTime.Now.Day >= 17 && DateTime.Now.Day <= 30 && DateTime.Now.Year == 2017)
                            {
                                if (SagaLib.Global.Random.Next(0, 100) > 90)
                                {
                                    fish = SagaDB.Fish.FishFactory.Instance.GetRandomItem("钓鱼");
                                    SagaDB.Item.Item item2 = SagaDB.Item.ItemFactory.Instance.GetItem(fish.ID);
                                    client.SendSystemMessage("钓上来了额外的鱼！！（活动时间：11月17日-11月30日）");
                                    client.AddItem(item2, false);
                                }
                            }

                            if (item.ItemID == 10111400)
                                client.TitleProccess(client.Character, 63, 1);
                            if (item.ItemID == 10110300)
                                client.TitleProccess(client.Character, 64, 1);
                            if (item.ItemID == 10109900)
                                client.TitleProccess(client.Character, 65, 1);

                            if (item.ItemID == 111120001)
                                client.TitleProccess(client.Character, 116, 1);
                            if (item.ItemID == 111120002)
                                client.TitleProccess(client.Character, 118, 1);
                            if (item.ItemID == 111120000)
                                client.TitleProccess(client.Character, 117, 1);

                            if (DateTime.Now.Month == 11 && DateTime.Now.Day >= 17 && DateTime.Now.Day <= 30 && DateTime.Now.Year == 2017)
                            {
                                if (SagaLib.Global.Random.Next(0, 100) == 1 && itemE.ItemID != 160088200)
                                {
                                    SagaDB.Item.Item box = SagaDB.Item.ItemFactory.Instance.GetItem(950000059);
                                    item.Stack = 1;
                                    client.AddItem(box, true);
                                    client.SendSystemMessage("你钓上来了一个『惊喜盒子』！");
                                }
                            }
                            else
                            {
                                if (SagaLib.Global.Random.Next(0, 150) == 1 && itemE.ItemID != 160088200)
                                {
                                    SagaDB.Item.Item box = SagaDB.Item.ItemFactory.Instance.GetItem(950000059);
                                    item.Stack = 1;
                                    client.AddItem(box, true);
                                    client.SendSystemMessage("你钓上来了一个『惊喜盒子』！");
                                }
                            }

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
