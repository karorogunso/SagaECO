using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;
using SagaDB.ODWar;

using SagaMap.Manager;
using SagaMap.Network.Client;

namespace SagaMap.Tasks.System
{
    public class AJImode : MultiRunTask
    {
        public AJImode()
        {
            this.period = 6000;
            this.dueTime = 0;
        }

        static AJImode instance;
        public bool IsMainTain = false;
        public bool StopLogin = false;
        public bool ClearAlready = false;

        public static AJImode Instance
        {
            get
            {
                if (instance == null)
                    instance = new AJImode();
                return instance;
            }
        }
        public override void CallBack()
        {
            try
            {
                /*DateTime now = DateTime.Now;
                if (now.DayOfWeek == DayOfWeek.Sunday && !IsMainTain)
                {
                    if (now.Hour == 23 && now.Minute % 5 == 0 && now.Minute <= 45)
                        Announce("[系统公告]服务器将在 " + (60 - now.Minute).ToString() + " 分钟后开始维护。");
                    if (now.Hour == 23 && now.Minute % 3 == 0 && now.Minute > 45 && now.Minute < 55)
                    {
                        Announce("[系统公告]服务器将在 " + (60 - now.Minute).ToString() + " 分钟后开始维护。");
                        if (!StopLogin) StopLogin = true;
                    }
                    if (now.Hour == 23 && now.Minute >= 55)
                        Announce("[系统公告]服务器将在 " + (60 - now.Minute).ToString() + " 分钟后开始维护，请各位玩家做好下线准备。");
                    if (now.Hour == 23 && now.Minute == 59)
                        MainTainStart(0);
                }
                else if (now.DayOfWeek == DayOfWeek.Monday && IsMainTain)
                {
                    if(now.Hour >= 0 && now.Minute >= 10)
                        MainTainStart(1);
                    if(now.Hour == 3)
                        MainTainStop();
                }*/ //z暂时注释，可随时解除
            }
            catch (Exception ex)
            {
                Logger.ShowError(ex);
            }
        }
        void Announce(string text)
        {
            foreach (MapClient i in SagaMap.Manager.MapClientManager.Instance.OnlinePlayer)
                i.SendAnnounce(text);
        }
        void MainTainStart(byte type)
        {
            IsMainTain = true;
            if (type == 0)
            {
                foreach (MapClient i in SagaMap.Manager.MapClientManager.Instance.OnlinePlayer)
                    i.netIO.Disconnect();
            }
            else if (type == 1 && !ClearAlready)
            {
                //MapServer.charDB.AJIClear();
            }
        }
        void MainTainStop()
        {
            IsMainTain = false;
            StopLogin = false;
        }
    }
}
