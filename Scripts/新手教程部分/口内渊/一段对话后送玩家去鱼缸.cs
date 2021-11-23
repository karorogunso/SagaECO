
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
    public class S80000103 : Event
    {
        public S80000103()
        {
            this.EventID = 80000103;
        }

        public override void OnEvent(ActorPC pc)
        {
            if(pc.AInt["账号第一次拿任务点"] != 1)
            {
                pc.QuestRemaining = 50;
                SagaMap.Network.Client.MapClient.FromActorPC(pc).SendQuestPoints();
                pc.AInt["账号第一次拿任务点"] = 1;
            }
            Say(pc, 0, 131, "这里…是哪里？", "");
            Say(pc, 0, 131, "我应该是前往ECO城$R参加『YggOnline』测试的才对。$R$R但是这里和之前介绍中$R说明的地方似乎有些不一样……", "");

            Wait(pc, 2000);
            ShowEffect(pc, 34, 25, 5135);
            Wait(pc, 1000);
            ShowEffect(pc, 34, 18, 5422);
            Wait(pc, 200);
            ShowEffect(pc, 5218);
            Wait(pc, 1000);
            ShowEffect(pc, 34, 25, 5198);
            ShowEffect(pc, 30, 29, 5198);
            ShowEffect(pc, 33, 30, 5198);
            Wait(pc, 200);
            ShowEffect(pc, 34, 25, 5451);
            ShowEffect(pc, 30, 29, 5451);
            ShowEffect(pc, 33, 30, 5451);
            NPCShow(pc, 80000100);
            NPCShow(pc, 80000101);
            NPCShow(pc, 80000102);
            Wait(pc, 1000);
            Say(pc, 80000102, 131, "嘎哦！$R$R老夏泥不要骗窝！$R这里真的是$CCECO城$CD吗！？", "蓝头发的萌妹子");
            Wait(pc, 500);
            Say(pc, 80000101, 131, "这么阴森森的地方，$R怎么想也不会是$CCECO城$CD吧！$R$Rw~~~~(>_<)~~~~我好怕，麻麻快保护我呀", "粉头发的萌妹子");
            Wait(pc, 500);
            Say(pc, 80000100, 131, "别吵$R我的传送门法术可是很完美的，$R我很确定我指定的坐标$R就是ECO城的大富豪游戏厅。$R$R会出现这种情况，$R八成是你们告诉我的$R传送目的地有问题吧", "红头发的家伙");
            Wait(pc, 500);
            Say(pc, 80000101, 131, "这里看起来好奇怪啊，$R你们来过这种地方吗？", "粉头发的萌妹子");
            Wait(pc, 500);
            Say(pc, 80000100, 131, "并没有，$R不过传送目的地出错了的话，$R传送终点通向哪里都有可能，$R$R我们没有从悬崖边掉下去摔死已经是万幸了", "红头发的家伙");
            Wait(pc, 500);
            Say(pc, 80000102, 131, "那边好像有人在，$R要不要去过问一问？", "蓝头发的萌妹子");
            Wait(pc, 500);
            Select(pc, "……", "", "那边的人？是说我吗？");
            Wait(pc, 500);
            Say(pc, 80000101, 131, "还是不要了吧，$R$R随意去接触不认识的家伙$R可是很危险的。", "粉头发的萌妹子");
            Wait(pc, 500);
            Say(pc, 80000100, 131, "不能凭借外表判断事物可是常识啊，$R$R比如头上长着角的萌妹子，$R尾巴上长着针的萌妹子，$R背后长着发条的…", "红头发的家伙");
            Wait(pc, 500);
            Select(pc, "……", "", "啊咧？");
            Wait(pc, 500);
            Say(pc, 80000100, 131, "…$R$R有些看起来人畜无害，$R但是只要你一靠近就会忽然过来攻击你，$R还会满地板丢刀片什么的…", "红头发的家伙");
            Wait(pc, 500);
            Select(pc, "……", "", "他们在说些什么？？？");
            Wait(pc, 500);
            Say(pc, 80000100, 131, "…总而言之，$R$R在这种扭曲虚空中，$R遇到什么魑魅魍魉都是不足为奇的", "红头发的家伙");
            Wait(pc, 1000);
            Say(pc, 80000101, 131, "唔啊…$R你不要吓我$R$R这样说的话，$R我们是不是先离开这里比较好啊", "粉头发的萌妹子");
            Wait(pc, 500);
            Say(pc, 80000100, 131, "也对呢，$R那就先回去鱼缸岛吧", "红头发的家伙");
            ShowEffect(pc, 34, 25, 5452);
            Wait(pc, 500);
            Say(pc, 80000102, 131, "这次可不要搞错传送门的坐标了啊", "蓝头发的萌妹子");
            Wait(pc, 500);
            ShowEffect(pc, 34, 25, 5211);
            ShowEffect(pc, 30, 29, 5211);
            ShowEffect(pc, 33, 30, 5211);
            Wait(pc, 800);
            NPCHide(pc, 80000100);
            NPCHide(pc, 80000101);
            NPCHide(pc, 80000102);
            Say(pc, 0, 131, "3人消失在了传送门中", "");
            Wait(pc, 1000);
            Say(pc, 0, 131, "说起来，他们之前说过$CCECO城$CD是吧", "");
            Say(pc, 0, 131, "如果跟上去的话，$R是不是就能找到前往$CCECO城$CD的方法了？", "");
            Wait(pc, 1000);
            Select(pc, "要怎么做？", "", "触摸上面的传送门", "触摸左下的传送门", "触摸右下的传送门");
            Wait(pc, 500);
            ShowEffect(pc, 5211);
            Wait(pc, 800);
            Fade(pc, FadeType.Out, FadeEffect.Black);
            Wait(pc, 500);
            Wait(pc, 2000);
            //Say(pc, 0, "哦！$R$R你终于来了吗！$R我等你好久辣！！", "天宫希");
            Warp(pc, 10054000, 199, 168);
            SetNextMoveEvent(pc, 60000010);
        }
    }
}