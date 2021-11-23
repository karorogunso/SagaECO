using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

using SagaLib;

namespace SagaMap.Partner
{
    public class AIThread : Singleton<AIThread>
    {
        static List<PartnerAI> ais = new List<PartnerAI>();//线程中的AI
        Thread mainThread;//主线程
        static List<PartnerAI> deleting = new List<PartnerAI>();//删除ai队列
        static List<PartnerAI> adding = new List<PartnerAI>();//增加ai队列
        public AIThread()//构造函数
        {
            mainThread = new Thread(mainLoop);
            mainThread.Name = string.Format("PartnerAIThread({0})", mainThread.ManagedThreadId);
            SagaLib.Logger.ShowInfo("PartnerAI线程启动：" + mainThread.Name);
            ClientManager.AddThread(mainThread);
            mainThread.Start();
        }

        public void RegisterAI(PartnerAI ai)
        {
            lock (adding)
            {
                adding.Add(ai);//如果adding没有被其他线程访问中，则将ai添加入增加队列
            }
        }

        public void RemoveAI(PartnerAI ai)
        {
            lock (deleting)
            {
                deleting.Add(ai);//如果deleting没有被其他线程访问中，则将ai添加入删除队列
            }
        }

        public int ActiveAI
        {
            get
            {
                return ais.Count;//返回线程中ai的数量
            }
        }

        static void mainLoop()
        {
            while (true)
            {
                lock (deleting)//如果deleting没有被其他线程访问中，则遍历删除队列，并移除线程中ai中的要删除的线程，然后清空删除队列
                {
                    foreach (PartnerAI i in deleting)
                    {
                        if (ais.Contains(i))
                            ais.Remove(i);
                    }
                    deleting.Clear();
                }
                lock (adding)
                {
                    foreach (PartnerAI i in adding)
                    {
                        if (!ais.Contains(i))
                            ais.Add(i);
                    }
                    adding.Clear();
                }
                foreach (PartnerAI i in ais)
                {
                    if (!i.Activated)
                        continue;
                    if (DateTime.Now > i.NextUpdateTime)
                    {
                        //ClientManager.EnterCriticalArea();
                        try
                        {
                            i.CallBack(null);
                        }
                        catch (Exception ex)
                        {
                            Logger.ShowError(ex);
                        }
                        i.NextUpdateTime = DateTime.Now + new TimeSpan(0, 0, 0, 0, i.period);
                        //ClientManager.LeaveCriticalArea();
                    }
                }
                if (ais.Count == 0)
                    Thread.Sleep(500);
                else
                    Thread.Sleep(10);
            }
        }
    }
}
