
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
    public partial class S60000014: Event
    {
        public S60000014()
        {
            this.EventID = 60000014;
        }

        public override void OnEvent(ActorPC pc)
        {
            DateTime endDT = new DateTime(2017, 2, 12);//设置结束日期为2017年2月12日
            if (DateTime.Now < endDT)
            {
                if (pc.CInt["CC新春活动"] == 1 && pc.CInt["CC新春活动 兔麻麻"] == 0)//此段代码为兔麻麻所用
                {
                    ChangeMessageBox(pc);
                    Say(pc, 60000014, 131, "啊啊！忙死了忙死了！", "兔麻麻");
                    Say(pc, 60000009, 131, "嘎哦！！！！！！！", "星麻麻");
                    Say(pc, 60000014, 131, "那边的你，快点过来帮忙呀！", "兔麻麻");
                    Select(pc, " ", "", "我只是来问下你们有没有……那啥……小卡片的");
                    Say(pc, 60000014, 131, "啊，说起来好像是有这么一回事……", "兔麻麻");
                    Say(pc, 60000014, 131, "不过比起那个！$R你能先帮我收集$CR甜苹果$CD，$CR甜树汁$CD，$CR小红花的花$CD各10个吗？", "兔麻麻");
                    Say(pc, 60000014, 131, "我们为了准备新年庆典的料理$R已经脱不开身了！", "兔麻麻");
                    Say(pc, 60000009, 131, "嘎哦！！！！！！！", "星麻麻");
                    Select(pc, " ", "", "……（被迫接受）");
                    pc.CInt["CC新春活动 兔麻麻"] = 1;
                    return;
                }
                if (CountItem(pc, 10002801) >= 10 && CountItem(pc, 10082500) >= 10 && CountItem(pc, 10092200) >= 10 && pc.CInt["CC新春活动 兔麻麻"] == 1)
                {
                    ChangeMessageBox(pc);
                    TakeItem(pc, 10002801, 10);//甜苹果
                    TakeItem(pc, 10082500, 10);//甜树汁
                    TakeItem(pc, 10092200, 10);//小红花的花
                    GiveItem(pc, 910000115, 1);//红包
                    pc.CInt["CC新春活动 兔麻麻"] = 2;//该part的最终标记
                    Say(pc, 60000014, 131, "谢谢！辛苦你了！", "兔麻麻");
                    Say(pc, 60000014, 131, "作为帮忙的回礼，给你红包！", "兔麻麻");
                    Say(pc, 60000014, 131, "你说奇怪的卡片？$R嗯……有是有", "兔麻麻");
                    Say(pc, 60000014, 131, "别看我这样，我还是挺擅长这类游戏的呢！$R剩下的就等正式开始后再说吧！", "兔麻麻");
                    Say(pc, 60000009, 131, "嘎哦！！！！！！！", "星麻麻");
                    Say(pc, 0, "（……去认识认识其他参赛者吧）", pc.Name);
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
                if (pc.CInt["CC新春活动"] == 1 && pc.CInt["CC新春活动 兔麻麻"] == 1)
                {
                    ChangeMessageBox(pc);
                    Say(pc, 60000014, 131, "是$CR甜苹果$CD，$CR甜树汁$CD，$CR小红花的花$CD各10个哦。", "兔麻麻");
                    return;
                }
                }

            BitMask<羽川柠> mark = pc.CMask["羽川柠对话"];
            if (mark.Test(羽川柠.初次对话) || !mark.Test(羽川柠.和兔纸对话))
                羽川柠剧情1(pc, mark);
            Say(pc, 0, "这个要这样切……嗯嗯。$R你好啊，你也是来做料理的吗？", "做料理的祭司");
        }
    }
}