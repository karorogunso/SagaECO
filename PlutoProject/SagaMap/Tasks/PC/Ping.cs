using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

using SagaLib;
using SagaDB.Actor;

using SagaMap.Network.Client;
using System.Globalization;

namespace SagaMap.Tasks.PC
{
    public class Ping : MultiRunTask
    {
        MapClient pc;
        public Ping(MapClient pc)
        {
            this.period = 10000;
            this.pc = pc;            
        }

        public override void CallBack()
        {
            checkdailylogin(pc);
            if (!pc.Character.Tasks.ContainsKey("Recover"))//自然恢复
            {
                Recover reg = new Recover(pc);
                pc.Character.Tasks.Add("Recover", reg);
                reg.Activate();
            }
            if ((pc.ping - DateTime.Now).TotalSeconds > 120)
            {
                ClientManager.EnterCriticalArea();
                try
                {
                    pc.netIO.Disconnect();
                }
                catch (Exception ex)
                {
                    Logger.ShowError(ex);
                }
                ClientManager.LeaveCriticalArea();
            }
        }

        void checkdailylogin(MapClient pc)
        {
            pc.Character.AInt["累积在线时长10秒加1"]++;
            if (pc.Character.AInt["累积在线时长10秒加1"] >= 360)
            {
                pc.Character.AInt["累积在线时长10秒加1"] = 0;
                pc.Character.AInt["累积在线时长小时"]++;
                pc.TitleProccess(pc.Character, 15, 1);
                pc.TitleProccess(pc.Character, 16, 1);
                pc.TitleProccess(pc.Character, 17, 1);
                pc.TitleProccess(pc.Character, 18, 1);
                pc.TitleProccess(pc.Character, 19, 1);
            }

            if (pc.Character.AStr["连续登陆记录"] != DateTime.Now.ToString("yyyy-MM-dd"))
            {
                string mark = pc.Character.AStr["连续登陆记录"];
                if (mark != "")
                {
                    DateTime da = DateTime.ParseExact(mark, "yyyy-MM-dd", CultureInfo.CurrentCulture, DateTimeStyles.None);
                    pc.Character.AStr["连续登陆记录"] = DateTime.Now.ToString("yyyy-MM-dd");
                    double daya = (DateTime.Now - da).TotalDays;
                    if (daya >= 1 && daya <= 2)
                    {
                        pc.TitleProccess(pc.Character, 20, 1);
                        pc.TitleProccess(pc.Character, 21, 1);
                        pc.TitleProccess(pc.Character, 22, 1);
                    }
                    else
                    {
                        pc.Character.AInt["称号20完成度"] = 1;
                        pc.Character.AInt["称号21完成度"] = 1;
                        pc.Character.AInt["称号22完成度"] = 1;
                    }
                }
                else
                {
                    pc.Character.AStr["连续登陆记录"] = DateTime.Now.ToString("yyyy-MM-dd");
                    pc.TitleProccess(pc.Character, 20, 1);
                    pc.TitleProccess(pc.Character, 21, 1);
                    pc.TitleProccess(pc.Character, 22, 1);
                }
            }
            else if (pc.Character.AInt["称号20完成度"] == 0 && pc.Character.AStr["连续登陆记录"] == DateTime.Now.ToString("yyyy-MM-dd"))
            {
                pc.Character.AInt["称号20完成度"] = 2;
                pc.Character.AInt["称号21完成度"] = 2;
                pc.Character.AInt["称号22完成度"] = 2;
            }
        }
    }
}
