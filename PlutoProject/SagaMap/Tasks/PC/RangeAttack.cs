using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

using SagaLib;
using SagaDB.Actor;
using SagaMap.Skill;
using SagaMap.Network.Client;
namespace SagaMap.Tasks.PC
{
    public class RangeAttack : MultiRunTask
    {
        MapClient client;
        int count;
        public RangeAttack(MapClient client)
        {
            dueTime = 500;
            period = 500;
            this.client = client;
            count = 0;
            client.Character.TInt["RangeAttackMark"] = 0;
        }

        public override void CallBack()
        {
            ClientManager.EnterCriticalArea();
            try
            {
                if(client.Character.MP >= client.Character.MaxMP * 0.33f)
                    count++;
                if (count == 1)
                {
                    client.Character.TInt["RangeAttackMark"] = 1;
                    //SkillHandler.Instance.ShowEffectOnActor(client.Character, 5167);
                    SkillHandler.Instance.ShowEffectOnActor(client.Character, 4230);
                }
                else if (count == 3 && (client.Character.TInt["绽放次数"] >= 5 || (client.Character.Status.Playman > 0 && 
                    client.Character.MP == client.Character.MaxMP && client.Character.Job == PC_JOB.HAWKEYE)))
                {
                    client.Character.TInt["RangeAttackMark"] = 2;
                    SkillHandler.Instance.ShowEffectOnActor(client.Character, 4163);
                    Deactivate();
                }
                else if (count == 5 && client.Character.TInt["绽放次数"] >= 5)
                {

                }
                else if (count >= 5)
                    Deactivate();
            }
            catch (Exception ex)
            {
                Logger.ShowError(ex);
                Deactivate();
            }
            ClientManager.LeaveCriticalArea();
        }
    }
}
