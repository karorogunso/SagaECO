using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

using SagaDB.Mob;
using SagaLib;
using SagaDB.Actor;

using SagaMap.Network.Client;
namespace SagaMap.Tasks.PC
{
    public class Revive : MultiRunTask
    {
        MapClient client;
        int count = 3;
        int mark = 0;
        public Revive(MapClient client,int s = 0,int mark = 0)
        {
            this.dueTime = 0;
            this.period = 1000;
            this.client = client;
            this.count = s;
            if (client.Character.Skills.ContainsKey(141))
                count /= 2;
            this.mark = mark;
        }

        public override void CallBack()
        {
            //ClientManager.EnterCriticalArea();
            try
            {
                if (this.client != null && client.Character.Buff.Dead)
                {
                    bool canre = true;
                    foreach (var item in MobFactory.Instance.BossList)
                    {
                        if (client.Map.ID == item.MapID && item.HP < item.MaxHP && item.HP > 0 && count >= 29)
                        {
                            if (client.Character.TInt["限制复活提示"] == 0)
                            {
                                client.SendSystemMessage("当前地图的领主正在限制你复活。【BOSS正在战斗时无法复活，战斗结束后将自动开始复活倒计时】");
                                client.SendSystemMessage("※不用担心死亡拿不到战利品，只要你对BOSS造成过任何伤害，就可以拿到战利品。");
                            }
                            client.Character.TInt["限制复活提示"]++;
                            if (client.Character.TInt["限制复活提示"] > 10)
                                client.Character.TInt["限制复活提示"] = 0;
                            canre = false;
                        }
                    }
                    if (canre)
                    {
                        count--;
                        client.Character.TInt["限制复活提示"] = 0;
                    }
                    if (count == 119)
                    {
                        this.client.Character.Buff.紫になる = true;
                        Map map = Manager.MapManager.Instance.GetMap(this.client.Character.MapID);
                        map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, this.client.Character, true);
                        this.client.SendSystemMessage("复活倒计时：2分钟后");
                    }
                    else if (count == 59)
                    {
                        this.client.Character.Buff.紫になる = true;
                        Map map = Manager.MapManager.Instance.GetMap(this.client.Character.MapID);
                        map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, this.client.Character, true);
                        this.client.SendSystemMessage("复活倒计时：1分钟后");
                    }
                    else if (count > 0 && count <= 28)
                    {
                        this.client.Character.Buff.紫になる = true;
                        Map map = Manager.MapManager.Instance.GetMap(this.client.Character.MapID);
                        map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, this.client.Character, true);
                        this.client.SendSystemMessage("复活倒计时：" + count.ToString() + " 秒");
                    }
                    if (count < 0)
                    {
                        if (mark == 3)
                        {
                            client.SendSystemMessage("复活次数已重置。");
                            client.Character.TInt["复活次数"] = client.Character.TInt["设定复活次数"];
                        }
                        if (mark == 4)
                        {
                            client.SendSystemMessage("复活次数已重置。");
                            client.Character.TInt["单人复活次数"] = client.Character.TInt["单人复活次数记录"];
                        }
                        this.client.RevivePC(this.client.Character);
                        this.Deactivate();
                        this.client.Character.Tasks.Remove("Recover");

                    }
                }
                else
                {
                    this.Deactivate();
                    this.client.Character.Tasks.Remove("Recover");
                }

            }
            catch (Exception ex)
            {
                Logger.ShowError(ex);
                this.Deactivate();
                this.client.Character.Tasks.Remove("Recover");
            }
            //ClientManager.LeaveCriticalArea();
        }
    }
}
