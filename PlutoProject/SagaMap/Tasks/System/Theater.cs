using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;
using SagaDB.Theater;

using SagaMap.Network.Client;
namespace SagaMap.Tasks.System
{
    public class Theater : MultiRunTask
    {
        public Theater()
        {
            this.period = 60000;
            this.dueTime = 0;
        }

        static Theater instance;

        public static Theater Instance
        {
            get
            {
                if (instance == null)
                    instance = new Theater();
                return instance;
            }
        }

        public override void CallBack()
        {
            try
            {
                foreach (uint j in TheaterFactory.Instance.Items.Keys)
                {
                    Movie nextMovie = TheaterFactory.Instance.GetNextMovie(j);
                    Map map = Manager.MapManager.Instance.GetMap(j);
                    Actor[] actors = new Actor[map.Actors.Count];
                    map.Actors.Values.CopyTo(actors, 0);
                    DateTime now = new DateTime(1970, 1, 1, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
                    if (nextMovie != null)
                    {
                        TimeSpan span = nextMovie.StartTime - now;
                        switch ((int)span.TotalMinutes)
                        {
                            case 10:
                            case 7:
                            case 5:
                            case 3:
                            case 2:
                            case 1:
                                Logger.ShowInfo(string.Format("{0} is going to play <{1}> in {2:0} minutes", map.Name, nextMovie.Name, span.TotalMinutes));
                                foreach (Actor i in actors)
                                {
                                    if (i.type != ActorType.PC)
                                        continue;
                                    ActorPC pc = (ActorPC)i;
                                    if (pc.Online)
                                    {
                                        if ((int)span.TotalMinutes == 10)
                                        {
                                            SagaDB.Item.Item item = pc.Inventory.GetItem(nextMovie.Ticket, Inventory.SearchType.ITEM_ID);
                                            if (item.Stack == 0)
                                            {
                                                uint lobby = j - 10000;
                                                map.SendActorToMap(pc, lobby, 10, 1);
                                            }
                                            else
                                            {
                                                Packets.Server.SSMG_THEATER_INFO p3 = new SagaMap.Packets.Server.SSMG_THEATER_INFO();
                                                p3.MessageType = SagaMap.Packets.Server.SSMG_THEATER_INFO.Type.MESSAGE;
                                                p3.Message = Manager.LocalManager.Instance.Strings.THEATER_WELCOME;
                                                MapClient.FromActorPC(pc).netIO.SendPacket(p3);
                                                p3 = new SagaMap.Packets.Server.SSMG_THEATER_INFO();
                                                p3.MessageType = SagaMap.Packets.Server.SSMG_THEATER_INFO.Type.MOVIE_ADDRESS;
                                                p3.Message = nextMovie.URL;
                                                MapClient.FromActorPC(pc).netIO.SendPacket(p3);
                                            }
                                        }
                                        Packets.Server.SSMG_THEATER_INFO p = new SagaMap.Packets.Server.SSMG_THEATER_INFO();
                                        p.MessageType = SagaMap.Packets.Server.SSMG_THEATER_INFO.Type.MESSAGE;
                                        p.Message = string.Format(Manager.LocalManager.Instance.Strings.THEATER_COUNTDOWN, nextMovie.Name, (int)span.TotalMinutes);
                                        MapClient.FromActorPC(pc).netIO.SendPacket(p);
                                        if ((int)span.TotalMinutes == 1)
                                        {
                                            Packets.Server.SSMG_THEATER_INFO p1 = new SagaMap.Packets.Server.SSMG_THEATER_INFO();
                                            p1.MessageType = SagaMap.Packets.Server.SSMG_THEATER_INFO.Type.STOP_BGM;
                                            MapClient.FromActorPC(pc).netIO.SendPacket(p1);                                        
                                        }
                                    }
                                }
                                break;
                            case 0:
                                Logger.ShowInfo(string.Format("{0} is now playing <{1}>", map.Name, nextMovie.Name));
                                foreach (Actor i in actors)
                                {
                                    if (i.type != ActorType.PC)
                                        continue;
                                    ActorPC pc = (ActorPC)i;
                                    if (pc.Online)
                                    {
                                        MapClient.FromActorPC(pc).DeleteItemID(nextMovie.Ticket, 1, true);
                                        Packets.Server.SSMG_THEATER_INFO p = new SagaMap.Packets.Server.SSMG_THEATER_INFO();
                                        p.MessageType = SagaMap.Packets.Server.SSMG_THEATER_INFO.Type.PLAY;
                                        p.Message = "";
                                        MapClient.FromActorPC(pc).netIO.SendPacket(p);
                                    }
                                }
                                break;
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
