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
        public void AddItemDrop(uint itemID, string treasureGroup, Actor ori, bool party, bool Public1, bool Public20, ushort count = 1, ushort minCount = 0, ushort maxCount = 0, int rate = 10000, bool roll = false, uint pictID = 0)
        {
            Actor owner = null;
            ActorMob MMob = null;
            if (ori.type == ActorType.MOB)
            {
                ActorEventHandlers.MobEventHandler eh = (ActorEventHandlers.MobEventHandler)ori.e;
                if (eh.AI.firstAttacker != null)
                    owner = eh.AI.firstAttacker;
                MMob = (ActorMob)ori;
            }
            List<Actor> owners = new List<Actor>();
            List<Actor> owners2 = new List<Actor>();
            if (owner != null)
            {
                if (owner.type == ActorType.PC)
                {
                    ActorPC pc = (ActorPC)owner;
                    if (pc.Party != null)
                    {
                        foreach (ActorPC i in pc.Party.Members.Values)
                        {
                            if (!i.Online)
                                continue;
                            if (i.MapID != ori.MapID)
                                continue;
                            owners2.Add(i);
                        }
                    }
                    if (pc.Party != null && party)
                    {
                        foreach (ActorPC i in pc.Party.Members.Values)
                        {
                            if (!i.Online)
                                continue;
                            if (i.MapID != ori.MapID)
                                continue;
                            if (rate != 10000 && party)
                            {
                                if (Global.Random.Next(0, 10000) <= rate)
                                    owners.Add(i);
                            }
                            else owners.Add(i);
                        }
                    }
                    else if (Public20)
                    {
                        Map map = Manager.MapManager.Instance.GetMap(ori.MapID);
                        List<Actor> actors = map.GetActorsArea(ori, 10000, false, true);
                        if (ori.type == ActorType.MOB)
                        {
                            ActorMob mob = (ActorMob)ori;
                            foreach (Actor ac in actors)
                            {
                                if (((SagaMap.ActorEventHandlers.MobEventHandler)mob.e).AI.DamageTable.ContainsKey(ac.ActorID))
                                {
                                    if (((SagaMap.ActorEventHandlers.MobEventHandler)mob.e).AI.DamageTable[ac.ActorID] > ori.MaxHP * 0.2f)
                                    {
                                        if (!owners.Contains(ac))
                                        {
                                            owners.Add(ac);
                                        }
                                    }
                                    //owners2.Add(ac);
                                }

                            }
                        }
                    }
                    else if (Public1)
                    {
                        Map map = Manager.MapManager.Instance.GetMap(ori.MapID);
                        List<Actor> actors = map.GetActorsArea(ori, 10000, false, true);
                        if (ori.type == ActorType.MOB)
                        {
                            ActorMob mob = (ActorMob)ori;
                            foreach (Actor ac in actors)
                            {
                                if (((SagaMap.ActorEventHandlers.MobEventHandler)mob.e).AI.DamageTable.ContainsKey(ac.ActorID))
                                {
                                    int damage = ((ActorEventHandlers.MobEventHandler)mob.e).AI.DamageTable[ac.ActorID];
                                    if (damage >= 1 /*ori.MaxHP * 0.001f*/ && damage < ori.MaxHP * 0.2f)
                                    {
                                        if (!owners.Contains(ac))
                                        {
                                            owners.Add(ac);
                                        }
                                    }
                                }
                                else
                                {
                                    if (!owners.Contains(ac))
                                    {
                                        if (ac.type == ActorType.PC)
                                        {
                                            Network.Client.MapClient.FromActorPC((ActorPC)ac).SendSystemMessage("真可惜，你没有对BOSS造成足够的伤害，不能获得战利品。");
                                            //悲伤：在可获取BOSS战利品的范围内，却没有获得战利品5次
                                            Skill.SkillHandler.Instance.TitleProccess(ac, 88, 1);
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                        owners.Add(owner);
                    if (Public1 || Public20)//直接入包
                    {
                        Item item = null;
                        //List<string> IPs = new List<string>();
                        foreach (Actor i in owners)
                        {
                            if (i.type == ActorType.PC)
                            {
                                ActorPC pcs = (ActorPC)i;
                                byte countss = 0;
                                foreach (Network.Client.MapClient x in MapClientManager.Instance.OnlinePlayer)
                                    if (x.Character.Account.MacAddress == pcs.Account.MacAddress && pcs.Account.GMLevel < 20)
                                        countss++;
                                /*if (countss > 1)
                                {
                                    Network.Client.MapClient.FromActorPC((ActorPC)i).SendSystemMessage("系统检测到您有可能多开，因此无法获得野外BOSS奖励。");
                                    continue;
                                }*/
                                item = ItemFactory.Instance.GetItem(itemID, true);
                                if (itemID == 10020758)
                                {
                                    item.PictID = pictID;
                                }
                                item.Stack = count;
                                EffectArg arg = new EffectArg();
                                arg.actorID = 0xFFFFFFFF;
                                arg.effectID = 7116;
                                arg.x = Global.PosX16to8(i.X, this.width);
                                arg.y = Global.PosY16to8(i.Y, this.height);
                                arg.oneTime = false;
                                this.SendEventToAllActorsWhoCanSeeActor(EVENT_TYPE.SHOW_EFFECT, arg, ori, false);
                                arg.effectID = 7115;
                                this.SendEventToAllActorsWhoCanSeeActor(EVENT_TYPE.SHOW_EFFECT, arg, ori, false);

                                Network.Client.MapClient.FromActorPC((ActorPC)i).AddItem(item, true,true,true);

                            }
                        }
                    }
                    else if(party)
                    {
                        Item item = null;
                        foreach (Actor i in owners)
                        {
                            if (i.type == ActorType.PC)
                            {
                                item = ItemFactory.Instance.GetItem(itemID, true);
                                if (itemID == 10020758)
                                {
                                    item.PictID = pictID;
                                }
                                item.Stack = count;
                                EffectArg arg = new EffectArg();
                                arg.actorID = 0xFFFFFFFF;
                                arg.effectID = 7116;
                                arg.x = Global.PosX16to8(i.X, this.width);
                                arg.y = Global.PosY16to8(i.Y, this.height);
                                arg.oneTime = false;
                                this.SendEventToAllActorsWhoCanSeeActor(EVENT_TYPE.SHOW_EFFECT, arg, ori, false);
                                arg.effectID = 7115;
                                this.SendEventToAllActorsWhoCanSeeActor(EVENT_TYPE.SHOW_EFFECT, arg, ori, false);

                                Network.Client.MapClient client = Network.Client.MapClient.FromActorPC((ActorPC)i);
                                client.AddItem(item, true, true, true);

                                if (i.Buff.Dead && client.map.IsMapInstance)
                                    Skill.SkillHandler.Instance.TitleProccess(i, 111, 1);
                            }
                        }
                    }
                    else//掉率在地上
                    {
                        //List<string> IPs = new List<string>();
                        foreach (Actor i in owners)
                        {
                            Item itemDroped = null;
                            if (i.type == ActorType.PC)
                            {
                                ActorPC pcs = (ActorPC)i;
                                /*if (IPs.Contains(pcs.Account.LastIP))
                                    continue;
                                else
                                    IPs.Add(pcs.Account.LastIP);*/
                            }
                            if (itemID != 0)
                            {
                                itemDroped = ItemFactory.Instance.GetItem(itemID, true);
                                if (minCount == 0 && maxCount == 0)
                                    itemDroped.Stack = count;
                                else
                                    itemDroped.Stack = (ushort)Global.Random.Next(minCount, maxCount);
                            }
                            if (itemID == 10020758)
                            {
                                itemDroped.PictID = pictID;
                            }
                            if (treasureGroup != null)
                            {
                                if (SagaDB.Treasure.TreasureFactory.Instance.Items.ContainsKey(treasureGroup))
                                {
                                    SagaDB.Treasure.TreasureItem item2 = SagaDB.Treasure.TreasureFactory.Instance.GetRandomItem(treasureGroup);
                                    itemDroped = ItemFactory.Instance.GetItem(item2.ID, true);
                                    itemDroped.Stack = (ushort)item2.Count;
                                }
                                else
                                    itemDroped = ItemFactory.Instance.GetItem(itemID, true);
                            }
                            ActorItem actor = new ActorItem(itemDroped);
                            if (roll) actor.Roll = true;
                            actor.e = new ActorEventHandlers.ItemEventHandler(actor);
                            actor.Owner = i;
                            actor.Party = party;
                            actor.MapID = this.ID;
                            short[] pos;
                            if (party)
                                pos = this.GetRandomPosAroundActor(ori);
                            else if (Public1)
                            {
                                pos = new short[2];
                                pos[0] = i.X;
                                pos[1] = i.Y;
                            }
                            else
                            {
                                pos = new short[2];
                                pos[0] = i.X;
                                pos[1] = i.Y;
                            }
                            actor.X = pos[0];
                            actor.Y = pos[1];
                            this.RegisterActor(actor);
                            actor.invisble = false;
                            this.OnActorVisibilityChange(actor);

                            //中秋节活动

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
                            else if (Public1)
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
                            Tasks.Item.DeleteItem task = new Tasks.Item.DeleteItem(actor);
                            task.Activate();
                            actor.Tasks.Add("DeleteItem", task);
                        }
                    }
                }
            }
        }
    }
}
