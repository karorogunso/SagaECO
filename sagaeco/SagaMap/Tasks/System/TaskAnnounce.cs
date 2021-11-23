using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

using SagaLib;
using SagaDB.Actor;
using SagaDB.LevelLimit;

using SagaMap.Network.Client;
namespace SagaMap.Tasks.System
{
    public class TaskAnnounce : MultiRunTask
    {
        string announce;
        string aname;
        public TaskAnnounce(string taskname,string announce ,int period)
        {
            this.aname = taskname;
            this.period = period;
            this.dueTime = 0;
            this.announce = announce;
        }
        public TaskAnnounce(string taskname, string announce, int duetime, int period)
        {
            this.aname = taskname;
            this.period = period;
            this.dueTime = duetime;
            this.announce = announce;
        }
        public override void CallBack()
        {
            try
            {
                foreach (MapClient i in SagaMap.Manager.MapClientManager.Instance.OnlinePlayer)
                {
                    /*if (i.Character.Account.GMLevel >= 100)
                        i.SendAnnounce(this.announce + " Task:" + this.aname + "period:"+period.ToString());
                    else*/
                        i.SendAnnounce(this.announce);
                }

            }
            catch (Exception) { }
        }
    }
}
