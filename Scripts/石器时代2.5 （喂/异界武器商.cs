using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;

using SagaMap.ActorEventHandlers;
namespace SagaScript.M30210000
{
    public class S30001002 : Event
    {
        public S30001002()
        {
            this.EventID = 30001002;
        }

        public override void OnEvent(ActorPC pc)
        {
            if((pc.CInt["异界武器商"] & 0x1) == 0)
                初次对话(pc);
            if ((pc.CInt["异界武器商"] & 0x2) == 0 && (pc.CInt["异界武器商"] & 0x1) != 0)
                试炼开始(pc);
        }
        void 初次对话(ActorPC pc)
        {
            //闲话段
            Say(pc, 131, "呵呵呵...", "异界武器商");
            Wait(pc, 500);
            Select(pc, "", "", "你笑什么");
            Wait(pc, 200);
            Say(pc, 131, "你是不是还不知道$R自己的处境呢$R", "异界武器商");
            Say(pc, 131, "这里的人、$R这里的事、$R以及关于你自己的事", "异界武器商");
            Select(pc, "", "", "请告诉我…！");
            Wait(pc, 200);
            Say(pc, 131, "呵呵呵.....", "异界武器商");
            Say(pc, 131, "试着打败我的结界吧$R然后我会为你指引", "异界武器商");

            pc.CInt["异界武器商"] = 0x1;//标记

            switch (Select(pc, "怎么办呢", "", "我接受你的试炼！", "再等等看"))
            {
                case 1://"我接受你的试炼！"
                    pc.CInt["异界武器商"] = 0x2;//标记
                    Say(pc, 131, "很好,很好！$R$R准备好之后就来找我吧！", "异界武器商");
                    break;
            }
        }
        void 试炼开始(ActorPC pc)
        {
            Say(pc, 131, "呵呵呵呵..$R$R你 准备好了吗？", "异界武器商");
            switch (Select(pc, "", "走吧", "还没有准备好"))
            {
                case 1:
                    if (pc.Party == null)//队伍有无情况必须分开判断，如果玩家没有队伍
                    {
                        创建副本地图(pc);//执行函数内的内容，传入玩家数据
                        传送入图(pc);
                    }
                    else//如果有队伍
                    {
                        if (pc.Party.Leader != pc)//判定队伍队长是不是触发者
                            Say(pc, 131, "...你似乎不是队长", "异界武器商");
                        else
                        {
                            创建副本地图(pc.Party.Leader);//就传队长的数据
                            Timer timer = new Timer("进入计时", 0, 5000);//建立计时器，5秒后生效
                            timer.OnTimerCall += timer_OnTimerCall;//计时结束函数
                            timer.Activate();//计时开始
                        }
                    }
                    break;
                case 2:
                    Say(pc, 131, "呵呵..", "异界武器商");
                    break;
            }
        }

        void timer_OnTimerCall(Timer timer, ActorPC pc)
        {
            timer.Deactivate();//删除计时器，不删除会重复执行
            switch(Select(pc,"是否进入队长的副本？","","进入","不进"))
            {
                case 1:
                    Warp(pc, (uint)pc.Party.Leader.TInt["异界武器商的试炼地图1"], 42, 53);
                    break;
                case 2:
                    break;
            }
        }
        void 创建副本地图(ActorPC creater)
        {
            creater.TInt["异界武器商的试炼地图1"] = CreateMapInstance(63000000, 30210000, 50, 50);//TInt为临时变量 CreateMapInstance是创建副本函数
            creater.TInt["异界武器商的试炼地图2"] = CreateMapInstance(63001000, 30210000, 50, 50);
            creater.TInt["异界武器商的试炼地图3"] = CreateMapInstance(63002000, 30210000, 50, 50);
            刷怪(creater);
            SagaMap.Network.Client.MapClient.FromActorPC(creater).SendSystemMessage("系统提示:你刚才创建了一个副本地图"); // FromActorPC获取ActorPC的MapClient类，然后MapClient类里有SendSystemMessage函数，作用是发送给客户端黄字内容
        }
        void 传送入图(ActorPC pc)
        {
            Warp(pc, (uint)pc.TInt["异界武器商的试炼地图1"], 42, 53);
        }
        void 刷怪(ActorPC pc)
        {
            ActorMob mob = SpawnMob((uint)pc.TInt["异界武器商的试炼地图1"], 42, 53, 10000000, 1,new MobCallback(怪物使用技能时事件),2)[0];
            ((MobEventHandler)(mob.e)).Dying += S30001002_Dying; //死亡事件
            ((MobEventHandler)(mob.e)).Moving += 怪物使用技能时事件;//怪物移动
            ((MobEventHandler)(mob.e)).SkillUsing += 怪物使用技能时事件;//技能使用事件
            ((MobEventHandler)(mob.e)).Attacking += S30001002_Attacking;//怪物发动攻击触发的事件
            ((MobEventHandler)(mob.e)).Defending += S30001002_Dying;//怪物血量变化时
        }


        void S30001002_Attacking(MobEventHandler eh, ActorPC pc)
        {
            SagaMap.Skill.SkillHandler.Instance.ActorSpeak(eh.AI.Mob, "打你");
        }
        void S30001002_Dying(MobEventHandler eh, ActorPC pc)
        {
            SagaMap.Skill.SkillHandler.Instance.ActorSpeak(eh.AI.Mob, "你坏坏");
            ShowPortal(pc, 42, 53, 42000000);
        }

        void 怪物使用技能时事件(MobEventHandler eh, ActorPC pc)
        {
            SagaMap.Skill.SkillHandler.Instance.ActorSpeak(eh.AI.Mob, "啊啊啊——看我用技能打你");
        }
    }
}
