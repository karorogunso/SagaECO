using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

namespace SagaScript
{
    public abstract class 劇場傳送門 : Event
    {
        uint mapID;
        byte x, y;

        public 劇場傳送門()
        {
        
        }

        protected void Init(uint eventID, uint mapID, byte x, byte y)
        {
            this.EventID = eventID;
            this.mapID = mapID;
            this.x = x;
            this.y = y;
        }

        public override void OnEvent(ActorPC pc)
        {
            SagaDB.Theater.Movie nextMovie = GetNextMovie(mapID);
            SagaDB.Theater.Movie currentMovie = GetCurrentMovie(mapID);
            if (nextMovie != null)
            {
                if (currentMovie != null)
                {
                    Say(pc, 131, string.Format("現在正在放映 {0}，$R;" +
                        "無法進入！$R;" +
                        "下一場電影將于{1:00}:{2:00}開始播放$R;{3}",
                        currentMovie.Name, nextMovie.StartTime.Hour, nextMovie.StartTime.Minute, nextMovie.Name));
                }
                else
                {
                    DateTime now = new DateTime(1970, 1, 1, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
                    TimeSpan span = nextMovie.StartTime - now;
                    if (span.TotalMinutes > 10 || span.TotalMinutes < 0)
                    {
                        DateTime enter = nextMovie.StartTime - new TimeSpan(0, 10, 0);
                        Say(pc, 131, string.Format("下一場電影將于{0:00}:{1:00}開始播放$R;" +
                            "{2}$R;您最早能在{3:00}:{4:00}入場", nextMovie.StartTime.Hour, nextMovie.StartTime.Minute, nextMovie.Name,
                            enter.Hour, enter.Minute));
                        return;
                    }
                    if (span.TotalMinutes < 1)
                    {
                        Say(pc, 131, string.Format("電影 {0} 即將上映，$R;您已經錯過了進場時間$R;請下次再來吧！", nextMovie.Name));
                        return;
                    }
                    if (CountItem(pc, nextMovie.Ticket) > 0)
                    {
                        if (pc.PossesionedActors.Count == 0)
                            Warp(pc, mapID, x, y);
                        else
                        {
                            Say(pc, 131, string.Format("下一場電影將于{0:00}:{1:00}開始播放$R;" +
                            "{2}$R;咦？有誰在憑依？$R;快點下來檢票吧！", nextMovie.StartTime.Hour, nextMovie.StartTime.Minute, nextMovie.Name));
                        }
                    }
                    else
                    {
                        Say(pc, 131, string.Format("下一場電影將于{0:00}:{1:00}開始播放$R;" +
                            "{2}$R;不過您好像沒有買票耶", nextMovie.StartTime.Hour, nextMovie.StartTime.Minute, nextMovie.Name));
                    }
                }
            }
            else
                Warp(pc, mapID + 4000, 11, 21);
        }
    }
}
