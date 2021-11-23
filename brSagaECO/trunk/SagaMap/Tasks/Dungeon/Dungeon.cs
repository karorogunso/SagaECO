using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

using SagaLib;
using SagaDB.Actor;

using SagaMap.Network.Client;
using SagaMap.Dungeon;
namespace SagaMap.Tasks.Dungeon
{
    public class Dungeon : MultiRunTask
    {
        SagaMap.Dungeon.Dungeon dungeon;
        public int lifeTime;
        public int counter = 0;
        public Dungeon(SagaMap.Dungeon.Dungeon dungeon, int lifeTime)
        {
            this.period = 1000;
            this.dueTime = 1000;
            this.dungeon = dungeon;
            this.lifeTime = lifeTime;
        }

        public override void  CallBack()
        {
            try
            {
                counter++;
                int rest = lifeTime - counter;

                if (rest == 0)
                {
                    this.Deactivate();
                    dungeon.Destory(DestroyType.TimeOver);
                    return;
                }
                if (rest == 7200 ||                    
                    rest == 3600 ||
                    rest == 1800 ||
                    rest == 900 ||
                    rest == 600 ||
                    rest == 300 ||
                    rest == 240 ||
                    rest == 180 ||
                    rest == 120 ||
                    rest == 60 ||
                    rest == 50 ||
                    rest == 40 ||
                    rest == 30 ||
                    rest == 20 ||
                    rest <= 15)
                {
                    string time = "";
                    if (rest >= 3600)
                        time = (rest / 3600).ToString() + Manager.LocalManager.Instance.Strings.ITD_HOUR;
                    if (rest < 3600 && rest >= 60)
                        time = (rest / 60).ToString() + Manager.LocalManager.Instance.Strings.ITD_MINUTE;
                    if (rest < 60)
                        time = rest.ToString() + Manager.LocalManager.Instance.Strings.ITD_SECOND;
                    string announce = string.Format(Manager.LocalManager.Instance.Strings.ITD_CRASHING, time);
                    foreach (DungeonMap i in dungeon.Maps)
                    {
                        foreach (Actor j in i.Map.Actors.Values)
                        {
                            if (j.type == ActorType.PC)
                            {
                                if (((ActorPC)j).Online)
                                {
                                    ActorEventHandlers.PCEventHandler eh = (SagaMap.ActorEventHandlers.PCEventHandler)j.e;
                                    eh.Client.SendAnnounce(announce);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.ShowError(ex);
            }            
        }
    }
}
