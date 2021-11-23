using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

using SagaLib;
using SagaDB.Actor;

using SagaMap.Network.Client;
namespace SagaMap.Tasks.PC
{
    public class AutoSave : MultiRunTask
    {
        ActorPC pc;
        public AutoSave(ActorPC pc)
        {
            this.period = 300000;
            this.pc = pc;            
        }

        public override void CallBack()
        {
            if (pc == null)
            {
                this.Deactivate();
                return;
            }
            if(!pc.Online)
            {
                this.Deactivate();
                return;
            }
            CheckTitleProcesses();
            MapClient.FromActorPC(pc).SendQuestInfo();
            ClientManager.EnterCriticalArea();
            try
            {
                DateTime now = DateTime.Now;
                MapServer.charDB.SaveChar(pc, false);
                Logger.ShowInfo("Autosaving " + pc.Name + "'s data, 耗时:" + (DateTime.Now - now).TotalMilliseconds +"ms");
            }
            catch (Exception ex)
            {
                Logger.ShowError(ex);
            }
            ClientManager.LeaveCriticalArea();
        }

        /// <summary>
        /// 检查该function中的一些称号
        /// </summary>
        void CheckTitleProcesses()
        {
            MapClient client = MapClient.FromActorPC(pc);

            //朋友很多
            List<ActorPC> friendlist = MapServer.charDB.GetFriendList2(pc);
            client.SetTitleProccess(pc, 80, (uint)friendlist.Count);
        }
    }
}
