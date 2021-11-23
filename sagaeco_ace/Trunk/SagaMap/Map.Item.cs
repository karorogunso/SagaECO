using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;


using SagaDB.Actor;
using SagaDB.Item;
using SagaDB.Map;
using SagaLib;
using SagaMap.Manager;

namespace SagaMap
{
    public partial class Map
    {
        public void AddItemDrop(uint itemID, string treasureGroup, Actor ori, bool party, bool Pubilc)
        {
            Actor owner = null;
            if (ori.type == ActorType.MOB)
            {
                ActorEventHandlers.MobEventHandler eh = (ActorEventHandlers.MobEventHandler)ori.e;
                if (eh.AI.firstAttacker != null)
                    owner = eh.AI.firstAttacker;
            }
            List<Actor> owners = new List<Actor>();
            List<Actor> owners2 = new List<Actor>();
            if (owner != null)
            {
                if (owner.type == ActorType.PC)
                {
                    ActorPC pc = (ActorPC)owner;
                    if (pc.Party != null && party)
                    {
                        foreach (ActorPC i in pc.Party.Members.Values)
                        {
                            if (!i.Online)
                                continue;
                            if (i.MapID != ori.MapID)
                                continue;
                            owners.Add(i);
                        }
                    }
                    else if (Pubilc)
                    {
                        Map map = Manager.MapManager.Instance.GetMap(ori.MapID);
                        List<Actor> actors = map.GetActorsArea(ori, 3000, false, true);
                        if (ori.type == ActorType.MOB)
                        {
                            ActorMob mob = (ActorMob)ori;
                            foreach (Actor ac in actors)
                            {
                                if (((SagaMap.ActorEventHandlers.MobEventHandler)mob.e).AI.DamageTable.ContainsKey(ac.ActorID))
                                {
                                    if (((SagaMap.ActorEventHandlers.MobEventHandler)mob.e).AI.DamageTable[ac.ActorID] > ori.MaxHP * 0.05f)
                                    {
                                        if (!owners.Contains(ac))
                                        {
                                            owners.Add(ac);
                                        }
                                    }
                                    owners2.Add(ac);
                                }

                            }
                        }
                    }
                    else
                        owners.Add(owner);
                    if (party)
                    {
                        Item item = null;
                        if (itemID != 0)
                            foreach (Actor i in owners)
                            {
                                if (i.type == ActorType.PC)
                                {
                                    item = ItemFactory.Instance.GetItem(itemID, true);
                                    EffectArg arg = new EffectArg();
                                    arg.actorID = 0xFFFFFFFF;
                                    arg.effectID = 7116;
                                    arg.x = Global.PosX16to8(i.X, this.width);
                                    arg.y = Global.PosY16to8(i.Y, this.height);
                                    arg.oneTime = false;
                                    this.SendEventToAllActorsWhoCanSeeActor(EVENT_TYPE.SHOW_EFFECT, arg, ori, false);
                                    arg.effectID = 7115;
                                    this.SendEventToAllActorsWhoCanSeeActor(EVENT_TYPE.SHOW_EFFECT, arg, ori, false);

                                    SagaMap.Network.Client.MapClient.FromActorPC((ActorPC)i).AddItem(item, true);
                                }
                            }
                        else
                        {
                            if (treasureGroup != null && treasureGroup != "")
                            {
                                foreach (var o in owners)
                                {
                                    Item itemDroped = null;
                                    if (SagaDB.Treasure.TreasureFactory.Instance.Items.ContainsKey(treasureGroup))
                                    {
                                        SagaDB.Treasure.TreasureItem i = SagaDB.Treasure.TreasureFactory.Instance.GetRandomItem(treasureGroup);

                                        itemDroped = ItemFactory.Instance.GetItem(i.ID, true);
                                        itemDroped.Stack = (ushort)i.Count;
                                    }
                                    else
                                        itemDroped = ItemFactory.Instance.GetItem(itemID, true);

                                    EffectArg arg = new EffectArg();
                                    arg.actorID = 0xFFFFFFFF;
                                    arg.effectID = 7116;
                                    arg.x = Global.PosX16to8(o.X, this.width);
                                    arg.y = Global.PosY16to8(o.Y, this.height);
                                    arg.oneTime = false;
                                    this.SendEventToAllActorsWhoCanSeeActor(EVENT_TYPE.SHOW_EFFECT, arg, ori, false);
                                    arg.effectID = 7115;
                                    this.SendEventToAllActorsWhoCanSeeActor(EVENT_TYPE.SHOW_EFFECT, arg, ori, false);

                                    SagaMap.Network.Client.MapClient.FromActorPC((ActorPC)o).AddItem(itemDroped, true);
                                }
                            }
                        }
                        //foreach (Actor i in owners2)
                        //{
                        //    item = ItemFactory.Instance.GetItem(40501002, true);
                        //    EffectArg arg = new EffectArg();
                        //    arg.actorID = 0xFFFFFFFF;
                        //    arg.effectID = 7115;
                        //    arg.x = Global.PosX16to8(i.X, this.width);
                        //    arg.y = Global.PosY16to8(i.Y, this.height);
                        //    arg.oneTime = false;
                        //    this.SendEventToAllActorsWhoCanSeeActor(EVENT_TYPE.SHOW_EFFECT, arg, ori, false);

                        //    SagaMap.Network.Client.MapClient.FromActorPC((ActorPC)i).AddItem(item, true);
                        //}
                    }
                    else
                    {
                        foreach (Actor i in owners)
                        {
                            Item itemDroped = null;
                            if (itemID != 0)
                                itemDroped = ItemFactory.Instance.GetItem(itemID, true);
                            if (treasureGroup != null && treasureGroup != "")
                            {
                                if (SagaDB.Treasure.TreasureFactory.Instance.Items.ContainsKey(treasureGroup))
                                {
                                    SagaDB.Treasure.TreasureItem item = SagaDB.Treasure.TreasureFactory.Instance.GetRandomItem(treasureGroup);
                                    if (item == null)
                                        continue;
                                    itemDroped = ItemFactory.Instance.GetItem(item.ID, true);
                                    itemDroped.Stack = (ushort)item.Count;
                                }
                                else
                                {
                                    Logger.ShowWarning("Cant't find TreasureGroup: " + treasureGroup + " for mob: "+ori.Name +"'s loot");
                                    continue;
                                }
                            }
                            ActorItem actor = new ActorItem(itemDroped);
                            actor.e = new ActorEventHandlers.ItemEventHandler(actor);
                            actor.Owner = i;
                            actor.Party = party;
                            actor.MapID = this.ID;
                            short[] pos;
                            if (party)
                                pos = this.GetRandomPosAroundActor(ori);
                            else if (Pubilc)
                            {
                                pos = new short[2];
                                pos[0] = i.X;
                                pos[1] = i.Y;
                            }
                            else
                            {
                                pos = new short[2];
                                pos[0] = ori.X;
                                pos[1] = ori.Y;
                            }
                            actor.X = pos[0];
                            actor.Y = pos[1];
                            this.RegisterActor(actor);
                            actor.invisble = false;
                            this.OnActorVisibilityChange(actor);

                            if (party)
                            {
                                EffectArg arg = new EffectArg();
                                arg.actorID = 0xFFFFFFFF;
                                arg.effectID = 7116;
                                arg.x = Global.PosX16to8(pos[0], this.width);
                                arg.y = Global.PosY16to8(pos[1], this.height);
                                arg.oneTime = false;
                                this.SendEventToAllActorsWhoCanSeeActor(EVENT_TYPE.SHOW_EFFECT, arg, ori, false);
                            }
                            else if (Pubilc)
                            {
                                EffectArg arg = new EffectArg();
                                arg.actorID = 0xFFFFFFFF;
                                arg.effectID = 7116;
                                arg.x = Global.PosX16to8(i.X, this.width);
                                arg.y = Global.PosY16to8(i.Y, this.height);
                                arg.oneTime = false;
                                this.SendEventToAllActorsWhoCanSeeActor(EVENT_TYPE.SHOW_EFFECT, arg, ori, false);
                                arg.effectID = 7115;
                                this.SendEventToAllActorsWhoCanSeeActor(EVENT_TYPE.SHOW_EFFECT, arg, ori, false);
                            }
                            Tasks.Item.DeleteItem task = new SagaMap.Tasks.Item.DeleteItem(actor);
                            task.Activate();
                            actor.Tasks.Add("DeleteItem", task);
                        }
                    }
                }
            }
        }
    }
}
