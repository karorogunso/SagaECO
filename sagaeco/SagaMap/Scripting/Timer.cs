using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaLib;
using SagaMap.Network.Client;
using SagaDB.Actor;
namespace SagaMap.Scripting
{
    public class Timer : MultiRunTask
    {
        public event TimerCallback OnTimerCall;

        ActorPC pc;
        List<object> customObjects = new List<object>();
        bool needScript;

        /// <summary>
        /// 挂钩的玩家
        /// </summary>
        public ActorPC AttachedPC { get { return this.pc; } set { this.pc = value; } }

        /// <summary>
        /// 自定义物件
        /// </summary>
        public List<object> CustomObjects { get { return this.customObjects; } }

        /// <summary>
        /// 是否需要用到脚本
        /// </summary>
        public bool NeedScript { get { return this.needScript; } set { this.needScript = value; } }

        public Timer(string name,int period, int due)
        {
            this.Name = name;
            this.period = period;
            this.dueTime = due;
        }

        public override void CallBack()
        {
            try
            {
                if (needScript && pc != null)
                {
                    if (MapClient.FromActorPC(pc).scriptThread != null)
                        return;
                    MapClient.FromActorPC(pc).scriptThread = new System.Threading.Thread(Run);
                    MapClient.FromActorPC(pc).scriptThread.Start();
                }
                else
                {
                    if (pc != null)
                    {
                        if (OnTimerCall != null)
                            OnTimerCall(this, pc);
                    }
                    else
                    {
                        OnTimerCall(this, null);
                        this.Deactivate();
                    }
                }
            }
            catch (Exception ex)
            {
                SagaLib.Logger.ShowError(ex);
            }

        }

        void Run()
        {
            ClientManager.EnterCriticalArea();
            try
            {
                if (pc != null)
                {
                    OnTimerCall(this, pc);
                }
                else
                    this.Deactivate();
            }
            catch (Exception ex)
            {
                SagaLib.Logger.ShowError(ex);
            }
            ClientManager.LeaveCriticalArea();
            MapClient.FromActorPC(pc).scriptThread = null;
        }
    }
}
