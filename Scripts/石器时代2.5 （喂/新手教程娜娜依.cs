
using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
using WeeklyExploration;
using System.Globalization;
namespace SagaScript.M30210000
{
    public class S500000001 : Event
    {
        public S500000001()
        {
            this.EventID = 500000001;
        }
        void 国庆返利(ActorPC pc)
        {
            OpenPprotectListOpen(pc);
            if (SDict["国庆CP_排行榜"]["国庆CP_" +pc.Account.AccountID] != 0)
            {
                int c = SDict["国庆CP_排行榜"]["国庆CP_" + pc.Account.AccountID];
                SDict["国庆CP_排行榜"]["国庆CP_" + pc.Account.AccountID] = 0;

                SagaMap.Network.Client.MapClient.FromActorPC(pc).SendSystemMessage("恭喜你获得了国庆CP兑换活动的返利20%！");
                pc.CP += (uint)(c / 5);
            }
        }
        void 每日奖励(ActorPC pc)
        {
            if (pc.AStr["每日奖励记录"] != DateTime.Now.ToString("yyyy-MM-dd"))
            {
                pc.AInt["每日盖章"]++;
                DailyStamp(pc, (uint)pc.AInt["每日盖章"], 2);
                GiveItem(pc, 910000002, 1);
                pc.AStr["每日奖励记录"] = DateTime.Now.ToString("yyyy-MM-dd");
                if (pc.AInt["每日盖章"] >= 10)
                {
                    pc.AInt["每日盖章"] = 0;
                    GiveItem(pc, 910000015, 1);
                    PlaySound(pc, 4015, false, 100, 50);
                }
                SagaMap.Network.Client.MapClient.FromActorPC(pc).SendSystemMessage("领到了每日登陆奖励！");
            }
        }
        void 每周清零(ActorPC pc)
        {
            if (pc.CInt["周清零记录"] != GetWeekOfYear(DateTime.Now))
            {
                pc.CInt["周清零记录"] = GetWeekOfYear(DateTime.Now);
                if (pc.Level > 1)
                {
if(pc.Level >=105)
GiveItem(pc,910000046,1);
else if(pc.Level >= 103)
GiveItem(pc,910000045,1);
                   else if (pc.Level >= 100)
                        GiveItem(pc, 910000023, 1);
                    else if (pc.Level >= 90)
                        GiveItem(pc, 910000022, 1);
                    else if (pc.Level >= 70)
                        GiveItem(pc, 910000021, 1);
                    else if (pc.Level >= 40)
                        GiveItem(pc, 910000020, 1);
                    pc.Level = 1;
                    pc.CEXP = 0;
                    ShowEffect(pc, 4243);
                    PlaySound(pc, 4003, false, 100, 50);
                    SagaMap.Network.Client.MapClient.FromActorPC(pc).SendEXP();
                    SagaMap.Network.Client.MapClient.FromActorPC(pc).SendPlayerLevel();
                    SagaMap.Network.Client.MapClient.FromActorPC(pc).SendSystemMessage("现在是今年的第" + pc.CInt["周清零记录"].ToString() + "周，由于时空扭曲，你的等级回到了从前。");
                }
            }
        }
        private int GetWeekOfYear(DateTime dt)
        {
            GregorianCalendar gc = new GregorianCalendar();
            return gc.GetWeekOfYear(dt, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
        }
        public override void OnEvent(ActorPC pc)
        {
            if (pc.CInt["FFBGM"] < 10)
                pc.CInt["FFBGM"] = 1174;
            //pc.CInt["FFBGM"] = 1168;
            pc.TInt["TempBGM"] = pc.CInt["FFBGM"];
            FaceToPC(pc);
            每日奖励(pc);
            每周清零(pc);
            国庆返利(pc);
            ChangeMessageBox(pc);
            Say(pc, 131, "喵？", "娜娜依");
            BitMask<新手教程娜娜依> 娜娜依 = pc.CMask["新手教程娜娜依"];
            if (!娜娜依.Test(新手教程娜娜依.初次觸發腳本))
            {
                初次觸發腳本(pc, 娜娜依);
                return;
            }
            else if (娜娜依.Test(新手教程娜娜依.初次觸發腳本) && !娜娜依.Test(新手教程娜娜依.第一次與娜娜依對話))
            {
                第一次與娜娜依對話(pc, 娜娜依);
                return;
            }
            第一次與娜娜依對話(pc, 娜娜依);
        }
        #region 初次觸發腳本
        void 初次觸發腳本(ActorPC pc, BitMask<新手教程娜娜依> 娜娜依)
        {
            Say(pc, 131, "欢迎来到Yggdrasil ECO$R$R目前測試階段，$R對話坑還尚未完成。", "娜娜依");
            Wait(pc, 300);
            Say(pc, 131, "啊，對了，忘記介紹了，$R$R我叫娜娜依。$R$R劇情坑完成后，$R你會更加的了解我。", "娜娜依");
            Wait(pc, 300);
            Say(pc, 131, "總之，$R請先選擇一門職業吧。$R$R※職業可以更改，但有一定的條件", "娜娜依");
            pc.SaveMap = 91000999;
            pc.SaveX = 23;
            pc.SaveY = 32;
            switch (Select(pc, "請選擇您要作為的職業(職業名暫定)", "", "勇者[原劍+騎]", "斥候[原賊+弓]", "魔法師[原wiz+元素]", "吟遊詩人[原光與暗]"))
            {
                case 1:
                    ChangePlayerJob(pc, PC_JOB.GLADIATOR);
                    PlaySound(pc, 3087, false, 100, 50);
                    ShowEffect(pc, 4131);
                    Wait(pc, 1000);
                    Say(pc, 131, "恭喜你$R你已經成為『勇者』了。", "娜娜依");
                    PlaySound(pc, 4012, false, 100, 50);
                    break;
                case 2:
                    ChangePlayerJob(pc, PC_JOB.HAWKEYE);
                    PlaySound(pc, 3087, false, 100, 50);
                    ShowEffect(pc, 4131);
                    Wait(pc, 1000);
                    Say(pc, 131, "恭喜你$R你已經成為『斥候』了。", "娜娜依");
                    PlaySound(pc, 4012, false, 100, 50);
                    break;
                case 3:
                    ChangePlayerJob(pc, PC_JOB.FORCEMASTER);
                    PlaySound(pc, 3087, false, 100, 50);
                    ShowEffect(pc, 4131);
                    Wait(pc, 1000);
                    Say(pc, 131, "恭喜你$R你已經成為『魔法師』了。", "娜娜依");
                    PlaySound(pc, 4012, false, 100, 50);
                    break;
                case 4:
                    ChangePlayerJob(pc, PC_JOB.CARDINAL);
                    PlaySound(pc, 3087, false, 100, 50);
                    ShowEffect(pc, 4131);
                    Wait(pc, 1000);
                    Say(pc, 131, "恭喜你$R你已經成為『吟遊詩人』了。", "娜娜依");
                    PlaySound(pc, 4012, false, 100, 50);
                    break;
            }
            Wait(pc, 1000);
            Say(pc, 131, "快到據點了，我們出發吧！", "娜娜依");
            Wait(pc, 1000);
            Warp(pc, 91000999, 23, 32);
            //SagaMap.Manager.ExperienceManager.Instance.ApplyExp(pc, 84634900, 33676, 1f, 1, true);
            SkillPointBonus(pc, 200);
            SkillPointBonus2T(pc, 200);
            SkillPointBonus2X(pc, 200);
            GiveItem(pc, 100000001, 1);
            GiveItem(pc, 910000001, 1);
            GiveItem(pc, 60073900, 1);
            GiveItem(pc, 60073901, 1);
            GiveItem(pc, 61085202, 1);
            GiveItem(pc, 60089600, 1);
            GiveItem(pc, 50137601, 1);
            GiveItem(pc, 50107500, 1);
            GiveItem(pc, 50085100, 1);
            GiveItem(pc, 50128600, 1);
            GiveItem(pc, 50132100, 1);
            GiveItem(pc, 50127400, 1);
            GiveItem(pc, 50090114, 1);
            GiveItem(pc, 16017300, 1);
            GiveItem(pc, 16017500, 1);
            GiveItem(pc, 16017600, 1);
            GiveItem(pc, 16017700, 1);
            GiveItem(pc, 60071250, 1);
            GiveItem(pc, 60011200, 1);
            GiveItem(pc, 60040000, 1);
            GiveItem(pc, 60060250, 1);
            GiveItem(pc, 60090050, 1);


            娜娜依.SetValue(新手教程娜娜依.初次觸發腳本, true);
        }
        #endregion
        #region 第一次與娜娜依對話
        void 第一次與娜娜依對話(ActorPC pc, BitMask<新手教程娜娜依> 娜娜依)
        {
            switch (Select(pc, "要做什麼呢？", "", "回到『主城』", "沒事"))
            {
                case 1:
                    pc.HP = pc.MaxHP;
                    pc.MP = pc.MaxMP;
                    pc.SP = pc.MaxSP;
                    Warp(pc, 91000999, 23, 32);
                    break;
                case 2:
                    return;
                /*List<WeeklyCore.MAPS> maps = WeeklyMaps.Instance.獲取區域地圖表();
                string[] paras = WeeklyCore.Instance.Tolist(maps);
                int SID = Select(pc, "請選擇你想到達的位於 " + SStr["AJI本周区域名称"] + " 的地圖", "", WeeklyCore.Instance.ToListForS(paras)) - 1;
                if (SID == WeeklyCore.Instance.ToListForS(paras).Length - 1) return;
                if (SInt[maps[SID].name] == 0) return;
                else
                {
                    uint mapid = maps[SID].mapid;
                    byte x = maps[SID].x;
                    byte y = maps[SID].y;
                    Wait(pc, 2000);
                    ShowEffect(pc, 8055);
                    Wait(pc, 500);
                    Warp(pc, mapid, x, y);
                    return;
                }*/
                case 4: break;
            }
        }
        #endregion
    }
}

