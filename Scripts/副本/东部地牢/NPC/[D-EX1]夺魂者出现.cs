
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

    public class P10000826 : Event
    {
        public P10000826()
        {
            EventID = 10000826;
        }
        public override void OnEvent(ActorPC pc)
        {
            if (pc.TInt["东牢幻觉"] < 4)
            {
                if (pc.Party != null)
                {
                    if (pc.Party.TInt["S20090000"] != 0)
                        Warp(pc, (uint)pc.Party.TInt["S20090000"], 49, 3);
                }
                else
                {
                    if (pc.TInt["S20090000"] != 0 && pc.TInt["副本复活标记"] == 4)
                        Warp(pc, (uint)pc.TInt["S20090000"], 49, 3);
                }
            }
            else
            {
                Say(pc, 0, "刚才吃的果子令你很不舒服...");
                ChangeMessageBox(pc);
                ShowEffect(pc, 5441);
                Wait(pc, 2000);
                Say(pc, 0, "这……这是……什么！？", pc.Name);
                Wait(pc, 1500);
                ShowEffect(pc, 48, 240, 4386);
                Wait(pc, 1000);
                ShowEffect(pc, 48, 240, 4355);
                CreateNpcPict(pc, 81000004, 16010000, 48, 240, 0, 500, 1, 111, 10);
                Wait(pc, 2000);
                Say(pc, 0, "徘徊的灵魂在虚空中呢喃，奏起通向异界的合唱", "夺魂者");
                Say(pc, 0, "我一直在等待……等待强大的灵魂来到我的世界", "夺魂者");
                ShowEffect(pc, 48, 240, 4384);
                Say(pc, 81000004, 369, "你的灵魂将被幻象吸收，成为我的力量，$R为了完成主人的愿望……为了暮色教团！", "夺魂者");
                ShowEffect(pc, 5022);
                Wait(pc, 1800);
                ShowEffect(pc, 4385);
                Wait(pc, 3500);
                ShowEffect(pc, 48, 240, 4386);
                Wait(pc, 1000);
                ShowPictCancel(pc, 81000004);
                Wait(pc, 3000);
                ShowEffect(pc, 53, 250, 4011);
                CreateNpcPict(pc, 81000001, 60000006, 53, 250, 1, 500, 1, 111, 10);
                Wait(pc, 1000);
                Say(pc, 0, "" + pc.Name + "……？", "沙月");
                Say(pc, 0, "怎么回事…为什么" + pc.Name + "也会陷入了幻象？", "沙月");
                Say(pc, 0, "这……可恶！", "沙月");
                ShowEffect(pc, 5024);
                CreateNpcPict(pc, 81000002, 10136900, 50, 217, 0, 500, 1, 111, 0);
                NPCMove(pc, 81000002, 52, 246, 1000, 0, 0xb, 122, 10, 0);
                Say(pc, 81000001, 422, "" + pc.Name + "，坚持住，我们这就来救你！", "沙月");
                //Wait(pc, 3000);
                //NPCMove(pc, 81000002, 51, 248, 700, 7, 0xb, 122, 10, 0);
                //Say(pc, 0, "吼……！", "冥王");
                //NPCMove(pc, 81000001, 53, 250, 1000, 3, 0xb, 111, 10, 0);
                //Say(pc, 0, "你也要来吗…真是帮大忙了", "沙月");
                ShowEffect(pc, 53, 250, 1022);
                Say(pc, 81000001, 999, "我们走吧，去" + pc.Name + "的幻象中！", "沙月");
                Wait(pc, 2000);
                ShowEffect(pc, 51, 248, 4023);
                ShowEffect(pc, 53, 250, 4023);
                ShowEffect(pc, 4023);
                if (pc.Party != null)
                {
                    if (pc.Party.TInt["S60903000"] != 0)
                    {
                        Warp(pc, (uint)pc.Party.TInt["S60903000"], 35, 68);
                    }
                }
                else
                {
                    if (pc.TInt["S60903000"] != 0 && pc.TInt["副本复活标记"] == 4)
                    {
                        Warp(pc, (uint)pc.TInt["S60903000"], 35, 68);
                    }
                }
            }
        }
    }
    //原始地圖:毒濕地(20091000)
    //目標地圖:東方地牢(20090000)
    //目標坐標:(48,2) ~ (51,4)

    public partial class S100000103 : Event
    {
        public S100000103()
        {
            this.EventID = 100000103;
        }
        public override void OnEvent(ActorPC pc)//待补完 在吃下4颗果实的情况下触摸传送门（ID：10000826）触发此脚本
        {
            if (pc.Party != null)
                Warp(pc, (uint)pc.Party.TInt["S20090000"], 35, 68);
            else if (pc.TInt["S20090000"] != 0 && pc.TInt["副本复活标记"] == 4)
                Warp(pc, (uint)pc.TInt["S20090000"], 35, 68);
        }
    }
}