using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

using SagaLib;
using SagaDB.Actor;

using SagaMap.Network.Client;
namespace SagaMap.Tasks.PC
{
    public class AutoAttack : MultiRunTask
    {
        MapClient client;
        Packets.Client.CSMG_SKILL_ATTACK p;
        public AutoAttack(MapClient client, int duetime,int delay, Packets.Client.CSMG_SKILL_ATTACK p)
        {
            dueTime = duetime;
            period = delay;
            this.p = p;
            this.client = client;
        }

        public override void CallBack()
        {
            try
            {
                if (client.Character == null || !client.Character.AutoAttack)
                {
                    client.Character.Tasks.Remove("自动攻击线程");
                    Deactivate();
                    return;
                }
                client.OnSkillAttack(p, true);
                
            }
            catch (Exception ex)
            {
                Logger.ShowError(ex);
                client.Character.Tasks.Remove("自动攻击线程");
                Deactivate();
            }
        }
    }
}
