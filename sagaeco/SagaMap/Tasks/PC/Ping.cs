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
            checkquest(pc);
            checkluckstar(pc);
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
        void checkluckstar(MapClient client)
        {
            if (client.Character.AInt["今日幸运星"] == 1 && client.Character.AInt["名称后缀图标"] != 1)
            {
                client.Character.AInt["名称后缀图标"] = 1;
                client.map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.CHAR_INFO_UPDATE, null, client.Character, true);
            }
            else if (client.Character.AInt["名称后缀图标"] == 1 && (client.Character.AInt["今日幸运星"] == 0 || client.Character.AInt["今日幸运星"] == 2))
            {
                client.Character.AInt["名称后缀图标"] = 0;
                client.map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.CHAR_INFO_UPDATE, null, client.Character, true);
            }
        }

        void checkquest(MapClient client)
        {
            ActorPC pc = client.Character;
            if (pc.CInt["日常任务接受状态"] == 1)
            {
                if (Convert.ToDateTime(pc.CStr["日常任务_失败时间"]) < DateTime.Now)
                {
                    SagaMap.Skill.SkillHandler.SendSystemMessage(pc, "※日常任务※由于超出任务时间，日常任务失败了。失败前已累积：" + pc.CInt["日常任务回合"]);
                    pc.CInt["日常任务接受状态"] = 0;
                    //pc.CInt["日常任务回合"] = 0;
                    return;
                }
            }
        }

        void checkdailylogin(MapClient pc)
        {
            //累积在线部分
            if (pc.Character == null) return;
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

            //每日在线1小时部分
            if (pc.Character.AStr["每日在线1小时记录"] != DateTime.Now.ToString("yyyy-MM-dd"))
            {
                pc.Character.AStr["每日在线1小时记录"] = DateTime.Now.ToString("yyyy-MM-dd");
                pc.Character.AInt["每日在线1小时10秒加1"] = 0;
                pc.Character.AInt["每日1小时惊喜宝箱获得"] = 0;
                pc.Character.AInt["每日2小时惊喜宝箱获得"] = 0;
            }
            pc.Character.AInt["每日在线1小时10秒加1"]++;
            if (pc.Character.AInt["每日在线1小时10秒加1"] == 360 && pc.Character.AInt["每日1小时惊喜宝箱获得"] != 1)
            {
                pc.Character.AInt["每日1小时惊喜宝箱获得"] = 1;
                SagaDB.Item.Item item = SagaDB.Item.ItemFactory.Instance.GetItem(950000059);
                item.Stack = 1;
                pc.AddItem(item, true);
                pc.SendSystemMessage("在线时长达到1小时！获得『惊喜宝箱』");
            }
            if (pc.Character.AInt["每日在线1小时10秒加1"] == 720 && pc.Character.AInt["每日2小时惊喜宝箱获得"] != 1)
            {
                pc.Character.AInt["每日2小时惊喜宝箱获得"] = 1;
                SagaDB.Item.Item item = SagaDB.Item.ItemFactory.Instance.GetItem(950000059);
                item.Stack = 1;
                pc.AddItem(item, true);
                pc.SendSystemMessage("在线时长达到2小时！获得『惊喜宝箱』");
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
