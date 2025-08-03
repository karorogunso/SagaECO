using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

using SagaLib;
using SagaDB.Actor;

using SagaMap.Network.Client;
namespace SagaMap.Tasks.PC
{
    public class Possession : MultiRunTask
    {
        MapClient client;
        ActorPC target;
        PossessionPosition pos;
        string comment;
        public Possession(MapClient client, ActorPC target, PossessionPosition position, string comment, int reduce)
        {
            if (reduce > 9)
                reduce = 9;
            this.dueTime = 10000 - reduce * 1000;
            this.period = 10000 - reduce * 1000;
            this.client = client;
            this.target = target;
            this.pos = position;
            this.comment = comment;
        }

        public override void CallBack()
        {
            ClientManager.EnterCriticalArea();
            try
            {
                client.Character.Buff.GetReadyPossession = false;
                client.Map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, client.Character, true);
                client.PossessionPerform(target, pos, comment);
                if (client.Character.Tasks.ContainsKey("Possession"))
                    client.Character.Tasks.Remove("Possession");
                this.Deactivate();

            }
            catch (Exception ex)
            {
                Logger.ShowError(ex);
                this.Deactivate();
            }
            ClientManager.LeaveCriticalArea();
        }
    }
}
