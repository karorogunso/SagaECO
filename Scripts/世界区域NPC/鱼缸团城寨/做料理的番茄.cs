
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
    public class S60000013 : Event
    {
        public S60000013()
        {
            this.EventID = 60000013;
        }

        public override void OnEvent(ActorPC pc)
        {
            if (pc.Account.GMLevel > 200)
            {
                if (Select(pc, "是否还原东牢进入任务状态", "", "好", "退出") == 1)
                {
                    Say(pc, 0, "草泥马为什么我成专业调试台了？！", "番茄茄");
                    NPCHide(pc, 80000702);//东国小莫
                    NPCShow(pc, 60000130);//咖啡馆小莫
                    pc.CInt["东牢进入任务"] = 0;
                }
            }
            DateTime endDT = new DateTime(2017, 2, 12);//设置结束日期为2017年2月12日
            if (DateTime.Now < endDT)
            {
                if (pc.Account.GMLevel > 200)
                {
                    Say(pc, 0, "？！原来你是GM？$R失敬失敬，小小意思，不成敬意。", "番茄茄");
                    GiveItem(pc, 100200000, 1);//慕斯食谱
                    if (Select(pc, "是否清空所有新春活动标记", "", "清空", "退出") == 1)
                    {
                        pc.CInt["CC新春活动"] = 0;
                        pc.CInt["CC新春活动 兔麻麻"] = 0;
                        pc.CInt["CC新春活动 番茄茄"] = 0;
                        pc.CInt["CC新春活动 天宫希"] = 0;
                        pc.CInt["CC新春活动 沙月"] = 0;
                        pc.CInt["CC新春活动 夏影"] = 0;
                        pc.CInt["CC新春活动 天天"] = 0;
                        pc.CInt["CC新春活动 暗鸣"] = 0;
                        pc.CInt["CCHelloComplete"] = 0;
                    }
                }
                if (pc.CInt["CC新春活动"] == 1 && pc.CInt["CC新春活动 番茄茄"] == 0)//此段代码为番茄茄所用
                {
                    ChangeMessageBox(pc);
                    Say(pc, 0, "哼哼哼，这样就大功告成了！", "番茄茄");
                    Select(pc, " ", "", "你在干嘛？");
                    Say(pc, 0, "惊！！", "番茄茄");
                    Say(pc, 0, "什么啊，是你啊。", "番茄茄");
                    Say(pc, 0, "你要不要喝我特制的番茄汁？", "番茄茄");
                    Say(pc, 0, "你看了眼那杯冒着气泡的黑色不明液体", " ");
                    switch (Select(pc, "喝不喝？", "", "喝", "不喝"))
                    {
                        case 1:
                            Say(pc, 0, "咕噜咕噜咕噜", " ");
                            Fade(pc, FadeType.In, FadeEffect.Black);
                            Say(pc, 0, "你感觉眼前一黑", " ");
                            Wait(pc, 3000);
                            Warp(pc, 30010007, 3, 5);
                            pc.CInt["CC新春活动 番茄茄"] = 1;
                            SetNextMoveEvent(pc, 60000022);
                            return;
                        case 2:
                            Say(pc, 0, "……（此人又开始埋头制作奇怪的饮料）", "番茄茄");
                            Say(pc, 0, "嗯？你说奇怪的卡片？有呀有呀", "番茄茄");
                            Say(pc, 0, "不过我不告诉你我是什么卡！打我啊！诶嘿☆", "番茄茄");
                            Say(pc, 0, "（感觉很欠打的样子）", pc.Name);
                            Say(pc, 0, "（……去认识认识其他参赛者吧）", pc.Name);
                            pc.CInt["CC新春活动 番茄茄"] = 3;//该part的最终标记
                            if (pc.CInt["CC新春活动"] == 1 && pc.CInt["CC新春活动 兔麻麻"] == 2 && pc.CInt["CC新春活动 番茄茄"] == 3 && pc.CInt["CC新春活动 天宫希"] == 3 && pc.CInt["CC新春活动 沙月"] == 1 && pc.CInt["CC新春活动 夏影"] == 1 && pc.CInt["CC新春活动 天天"] == 1 && pc.CInt["CC新春活动 暗鸣"] == 6)
                            {
                                ChangeMessageBox(pc);
                                Say(pc, 0, "似乎把参赛者都认识完了呢……", pc.Name);
                                Say(pc, 0, "去找那个奇怪的新年c看看好了", pc.Name);
                                pc.CInt["CCHelloComplete"] = 1;
                                return;
                            }
                            return;
                    }
                }
                if (pc.CInt["CC新春活动"] == 1 && pc.CInt["CC新春活动 番茄茄"] == 2)//此段代码为番茄茄所用
                {
                    ChangeMessageBox(pc);
                    Say(pc, 0, "……（此人又开始埋头制作奇怪的饮料）", "番茄茄");
                    Say(pc, 0, "嗯？你说奇怪的卡片？有呀有呀", "番茄茄");
                    Say(pc, 0, "不过我不告诉你我是什么卡！打我啊！诶嘿☆", "番茄茄");
                    Say(pc, 0, "（感觉很欠打的样子）", pc.Name);
                    Say(pc, 0, "（……去认识认识其他参赛者吧）", pc.Name);
                    pc.CInt["CC新春活动 番茄茄"] = 3;//该part的最终标记
                    if (pc.CInt["CC新春活动"] == 1 && pc.CInt["CC新春活动 兔麻麻"] == 2 && pc.CInt["CC新春活动 番茄茄"] == 3 && pc.CInt["CC新春活动 天宫希"] == 3 && pc.CInt["CC新春活动 沙月"] == 1 && pc.CInt["CC新春活动 夏影"] == 1 && pc.CInt["CC新春活动 天天"] == 1 && pc.CInt["CC新春活动 暗鸣"] == 6)
                    {
                        ChangeMessageBox(pc);
                        Say(pc, 0, "似乎把参赛者都认识完了呢……", pc.Name);
                        Say(pc, 0, "去找那个奇怪的新年c看看好了", pc.Name);
                        pc.CInt["CCHelloComplete"] = 1;
                        return;
                    }
                    return;
                }
            }
            Say(pc, 0, "做料理真开心啊，$R呼！~再多加一点吧。$R$R（倒进半瓶黑色不明液体）", "做料理的番茄");
        }
    }
}