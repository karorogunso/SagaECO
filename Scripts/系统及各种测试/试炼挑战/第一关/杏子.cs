
using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
using SagaMap.ActorEventHandlers;
namespace WeeklyExploration
{
    public partial class 杏子 : Event
    {
        public 杏子()
        {
            this.EventID = 50010000;
        }
        public override void OnEvent(ActorPC pc)
        {
            Select(pc, "怎么办", "", "让我开始试炼");
            杏子试炼(pc);
        }
        void 杏子试炼(ActorPC pc)
        {
            pc.TInt["试炼临时地图"] = CreateMapInstance(10001001, 90001000, 35, 15, true, 999, true);
            uint mapid = (uint)pc.TInt["试炼临时地图"];
            Warp(pc, mapid, pc.X, pc.Y);
            WeeklySpawn.Instance.SpawnMob(10030401, mapid, 185, 239, 5, 1, 30, 北极熊王Info(), 北极熊王AI(), (MobCallback)Ondie, 1);
        }
        void Ondie(MobEventHandler e, ActorPC pc)
        {
            switch(Select(pc, "通关了", "", "前往第二关", "回到飞空艇"))
            {

            }
        }




        void 叫醒选项(ActorPC pc)
        {
            switch (Select(pc, "她似乎在睡觉，怎么办呢？", "", "轻轻的叫醒她", "在她耳边大喊", "踩她", "向她丢雪球", "不理睬"))
            {
                case 1:
                    Say(pc, 0, "你试着轻轻的叫醒她……");
                    Wait(pc, 2000);
                    Say(pc, 0, "但是似乎没醒...");
                    叫醒选项(pc);
                    break;
                case 2:
                    Say(pc, 0, "啊————");
                    Wait(pc, 500);
                    Say(pc, 0, "……");
                    Wait(pc, 500);
                    Say(pc, 0, "啊————————————！");
                    Wait(pc, 200);
                    Say(pc, 2070, "哇呀————", "杏子");
                    Say(pc, 2070, "你你你干什么！！？？", "杏子");
                    if(Select(pc,"作何回答","","我是来试炼的","你可真难叫醒！") == 2)
                    {
                        Say(pc, 2070, "你可真是没礼貌！$R人家以前可不是这样。", "杏子");
                        Say(pc, 2070, "自从上次吃错药$R吃到了【睡眠草】之后，留下来的后遗症就一直这样困扰着我。", "杏子");
                    }
                    Say(pc, 2070, "啊，你是来试炼的冒险家吗？$R你叫什么名字？", "杏子");
                    if(Select(pc, "作何回答", "", "我叫" + pc.Name, "你事先不知道吗？")==2)
                    {
                        Say(pc, 2070, "嗯..最近参加试炼的人数相比以前要多好多，从总教官那得到的名字根本不想一个一个去记住。", "杏子");
                        Select(pc, "作何回答", "", "我叫" + pc.Name);
                    }
                    string s = "";
                    if (pc.Gender == PC_GENDER.FEMALE)
                        s = "先生";
                    if (pc.Gender == PC_GENDER.MALE)
                        s = "小姐";
                    Say(pc, 2070, "初次见面，请多多指教，" + pc.Name + s+ "，$R我叫杏子，是你的个人试炼导员。", "杏子");
                    Say(pc, 2070, "那么，让我们开始第一个试炼吧。", "杏子");
                    break;
            }
        }
    }
}

