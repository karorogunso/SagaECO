
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
    public class S4000000 : Event
    {
        public S4000000()
        {
            this.EventID = 4000000;
        }

        public override void OnEvent(ActorPC pc)
        {
            DateTime endDT = new DateTime(2017, 2, 12);//设置结束日期为2017年2月12日
            if (DateTime.Now < endDT)
            {
                if (pc.CInt["CC新春活动"] == 2)//此段代码为鱼缸岛2-1自动触发用 ID4000000
                {
                    ChangeMessageBox(pc);
                    Say(pc, 0, "发生了什么？$R为何大家都集中在鱼缸团本部门口？", pc.Name);
                    Say(pc, 0, "为什么会这样呢？$R门打不开了？说好在里面集合的呢！！", "星麻麻");
                    Say(pc, 0, "为什么会这样呢？", "众人");
                    Say(pc, 0, "嘎哦！！！！！！！", "星麻麻");
                    Say(pc, 0, "说好有10个人呢？好像少了？", "兔麻麻");
                    Say(pc, 0, "真的诶！", "星麻麻");
                    Say(pc, 0, "为什么会这样呢？", "星麻麻");
                    Say(pc, 0, "为什么会这样呢？", "众人");
                    Say(pc, 0, "嘎哦嘎哦！！！！！！！", "星麻麻");
                    Say(pc, 0, "比起那个，$R我们还是先想想怎么进去吧！", "番茄茄");
                    Say(pc, 0, "众人望向新年c，$R发现她一副事不关己高高挂起的样子");
                    Say(pc, 0, "（……还是自己试试怎么进去好了）", pc.Name);
                    pc.CInt["CC新春活动"] = 3;
                }
                Select(pc, " ", "", "……有一种不安的预感");
                ShowPortal(pc, 157, 137, 4000001);
            }
        }
    }
}