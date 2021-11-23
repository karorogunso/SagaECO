using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30100000
{
    public class S11000766 : Event
    {
        public S11000766()
        {
            this.EventID = 11000766;
        }

        public override void OnEvent(ActorPC pc)
        {
            /*
            if (pc.Account.GMLevel == 1)
            {
                Call(EVT1100076625);
                return;
            }//*
            if (!_0c54)
            {
                _0c54 = true;
                Say(pc, 131, "我是从法伊斯特来的$R;" +
                    "希望得到阿克罗波利斯的帮助$R;" +
                    "$P有目击者报称，$R在东部森林发现大型魔物啊$R;" +
                    "目击报告显示$R;" +
                    "古城周围的魔物数量$R;" +
                    "有逐渐增加的趋势呀$R;" +
                    "$R我军为了打败它们，打算突袭$R;" +
                    "$R恳请各位鼎力支持$R;");
            }
            else
            {
                Say(pc, 131, "现在，绿盾军$R;" +
                    "正计划扫荡法伊斯特的$R;" +
                    "东部森林和南部古城$R;" +
                    "周围的魔物唷$R;" +
                    "$R请各位协力相助吧！$R;");
            }//*/
            //EVT1100076601
            Say(pc, 131, "参加作战吗？$R;" +
                "$R『军团适用』的任务$R;" +
                "只有军团成员才能参加喔$R;" +
                "请注意，不是相同队伍$R;" +
                "但所属同一个军团也可以$R;" +
                "$P『队伍适用』的任务$R;" +
                "只有队伍成员才能参加啊$R;" +
                "请多加注意$R;");
            //EVT1100076602
            switch (Select(pc, "做什么呢？", "", "任务服务台", "什么也不做"))
            {
                case 1:
                    //GOTO EVT1100076603;
                    break;
                case 2:
                    break;
            }
        }
    }
}