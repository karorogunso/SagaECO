using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;
using SagaDB.Marionette;
using SagaDB.Treasure;

using SagaMap.Network.Client;

namespace SagaMap.Tasks.Golem
{
    public class GolemTask : MultiRunTask
    {
        ActorGolem golem;
        int counter = 0;
        DateTime nextGatherTime = DateTime.Now + new TimeSpan(2, 0, 0, 0);
        TimeSpan gatherSpan;
        public GolemTask(ActorGolem golem)
        {
            this.dueTime = (int)60000;
            this.period = (int)60000;
            this.golem = golem;

            Map map = Manager.MapManager.Instance.GetMap(golem.MapID);
            switch (golem.GolemType)
            {
                case GolemType.Plant:
                    if (map.Info.gatherInterval.ContainsKey(GatherType.Plant))
                    {
                        gatherSpan = new TimeSpan(0, map.Info.gatherInterval[GatherType.Plant], 0);
                        nextGatherTime = DateTime.Now + gatherSpan;
                    }
                    break;
                case GolemType.Mineral:
                    if (map.Info.gatherInterval.ContainsKey(GatherType.Mineral))
                    {
                        gatherSpan = new TimeSpan(0, map.Info.gatherInterval[GatherType.Mineral], 0);
                        nextGatherTime = DateTime.Now + gatherSpan;
                    }
                    break;
                case GolemType.Magic:
                    if (map.Info.gatherInterval.ContainsKey(GatherType.Magic))
                    {
                        gatherSpan = new TimeSpan(0, map.Info.gatherInterval[GatherType.Magic], 0);
                        nextGatherTime = DateTime.Now + gatherSpan;
                    }
                    break;
                case GolemType.TreasureBox:
                    if (map.Info.gatherInterval.ContainsKey(GatherType.Treasurebox))
                    {
                        gatherSpan = new TimeSpan(0, map.Info.gatherInterval[GatherType.Treasurebox], 0);
                        nextGatherTime = DateTime.Now + gatherSpan;
                    }
                    break;
                case GolemType.Excavation:
                    if (map.Info.gatherInterval.ContainsKey(GatherType.Excavation))
                    {
                        gatherSpan = new TimeSpan(0, map.Info.gatherInterval[GatherType.Excavation], 0);
                        nextGatherTime = DateTime.Now + gatherSpan;
                    }
                    break;
                case GolemType.Any:
                    if (map.Info.gatherInterval.ContainsKey(GatherType.Any))
                    {
                        gatherSpan = new TimeSpan(0, map.Info.gatherInterval[GatherType.Any], 0);
                        nextGatherTime = DateTime.Now + gatherSpan;
                    }
                    break;
                case GolemType.Strange:
                    if (map.Info.gatherInterval.ContainsKey(GatherType.Strange))
                    {
                        gatherSpan = new TimeSpan(0, map.Info.gatherInterval[GatherType.Strange], 0);
                        nextGatherTime = DateTime.Now + gatherSpan;
                    }
                    break;
                case GolemType.Food:
                    if (map.Info.gatherInterval.ContainsKey(GatherType.Food))
                    {
                        gatherSpan = new TimeSpan(0, map.Info.gatherInterval[GatherType.Food], 0);
                        nextGatherTime = DateTime.Now + gatherSpan;
                    }
                    break;                    
            }
            
        }

        public override void  CallBack()
        {
            counter++;
            try
            {
                if (counter == 24 * 60)
                {
                    Map map = Manager.MapManager.Instance.GetMap(golem.MapID);
                    if (golem.GolemType >= GolemType.Plant && golem.GolemType <= GolemType.Strange)
                    {
                        ActorEventHandlers.MobEventHandler eh = (SagaMap.ActorEventHandlers.MobEventHandler)golem.e;
                        golem.e = new ActorEventHandlers.NullEventHandler();
                        eh.AI.Pause();
                    }
                    golem.invisble = true;
                    map.OnActorVisibilityChange(golem);
                    golem.Tasks.Remove("GolemTask");
                    this.Deactivate();
                }
                if (this.nextGatherTime <= DateTime.Now)
                {
                    if (golem.GolemType >= GolemType.Plant && golem.GolemType <= GolemType.Strange)
                    {
                        Marionette mario = MarionetteFactory.Instance[golem.Item.BaseData.marionetteID];
                        if (mario != null)
                        {
                            TreasureItem item = null;
                            int det = 0;
                            switch (golem.GolemType)
                            {
                                case GolemType.Plant:
                                    if (!mario.gather[GatherType.Plant])
                                        det = Global.Random.Next(0, 99);
                                    if (det <= 10)
                                        item = TreasureFactory.Instance.GetRandomItem("Gather_Plant");
                                    break;
                                case GolemType.Mineral:
                                    if (!mario.gather[GatherType.Mineral])
                                        det = Global.Random.Next(0, 99);
                                    if (det <= 10)
                                        item = TreasureFactory.Instance.GetRandomItem("Gather_Mineral");
                                    break;
                                case GolemType.Food:
                                    if (!mario.gather[GatherType.Food])
                                        det = Global.Random.Next(0, 99);
                                    if (det <= 10)
                                        item = TreasureFactory.Instance.GetRandomItem("Gather_Food");
                                    break;
                                case GolemType.Magic:
                                    if (!mario.gather[GatherType.Magic])
                                        det = Global.Random.Next(0, 99);
                                    if (det <= 10)
                                        item = TreasureFactory.Instance.GetRandomItem("Gather_Magic");
                                    break;
                                case GolemType.TreasureBox:
                                    if (!mario.gather[GatherType.Treasurebox])
                                        det = Global.Random.Next(0, 99);
                                    if (det <= 10)
                                        item = TreasureFactory.Instance.GetRandomItem("Gather_Treasurebox");
                                    break;
                                case GolemType.Excavation:
                                    if (!mario.gather[GatherType.Excavation])
                                        det = Global.Random.Next(0, 99);
                                    if (det <= 10)
                                        item = TreasureFactory.Instance.GetRandomItem("Gather_Excavation");
                                    break;
                                case GolemType.Any:
                                    if (!mario.gather[GatherType.Any])
                                        det = Global.Random.Next(0, 99);
                                    if (det <= 10)
                                        item = TreasureFactory.Instance.GetRandomItem("Gather_Any");
                                    break;
                                case GolemType.Strange:
                                    if (!mario.gather[GatherType.Strange])
                                        det = Global.Random.Next(0, 99);
                                    if (det <= 10)
                                        item = TreasureFactory.Instance.GetRandomItem("Gather_Strange");
                                    break;
                            }
                            if (item != null)
                            {
                                if (item.ID != 0)
                                {
                                    SagaDB.Item.Item newItem = SagaDB.Item.ItemFactory.Instance.GetItem(item.ID, true); //免鉴定
                                    newItem.Stack = (ushort)item.Count;
                                    if (newItem.Stack > 0)
                                    {
                                        Logger.LogItemGet(Logger.EventType.ItemGolemGet, this.golem.Owner.Name + "(" + this.golem.Owner.CharID + ")", newItem.BaseData.name + "(" + newItem.ItemID + ")",
                                            string.Format("GolemCollect Count:{0}", newItem.Stack), false);
                                    }
            
                                    golem.Owner.Inventory.AddItem(SagaDB.Item.ContainerType.GOLEMWAREHOUSE, newItem);                                    
                                }
                            }
                        }
                    }
                    nextGatherTime += gatherSpan;
                }
            }
            catch (Exception ex)
            {
                Logger.ShowError(ex);
                golem.Tasks.Remove("GolemTask");
                this.Deactivate();
            }
        }
    }
}
