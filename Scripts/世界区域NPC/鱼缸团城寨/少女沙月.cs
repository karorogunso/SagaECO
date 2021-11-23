
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
    public class S60000006 : Event
    {
        public S60000006()
        {
            this.EventID = 60000006;
        }

        public override void OnEvent(ActorPC pc)
        {
            DateTime endDT = new DateTime(2017, 2, 12);//设置结束日期为2017年2月12日
            if (DateTime.Now < endDT)
            {
                if (pc.CInt["CC新春活动"] == 1 && pc.CInt["CC新春活动 沙月"] == 0)//此段代码为沙月所用
                {
                    ChangeMessageBox(pc);
                    Say(pc, 0, "……奇怪的卡片？", "沙月");
                    Say(pc, 0, "噢，这件事啊……！", "沙月");
                    Say(pc, 0, "我是平民哟~", "沙月");
                    Select(pc, " ", "", "直接说出来不要紧吗！？");
                    Say(pc, 0, "没有主动展示卡片~应该没事吧！", "沙月");
                    Say(pc, 0, "……（看着玩家不说话）", "小衣");
                    Say(pc, 0, "（心情复杂.jpg）", pc.Name);
                    Say(pc, 0, "（……去认识认识其他参赛者吧）", pc.Name);
                    pc.CInt["CC新春活动 沙月"] = 1;//该part的最终标记
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

            ChangeMessageBox(pc);


            if (CountItem(pc, 100704001) >= 5 && pc.CInt["沙月技能点任务标记"] < 1)//若玩家持有的奇怪碎片等于大于5，且未完成过沙月技能点
            {
                Say(pc, 0, "……", "少女沙月·魉皇鬼");
                Say(pc, 0, "啊，你……$R$R等一下，你手上拿着的那个，$R是正体不明的碎片吗！？", "少女沙月·魉皇鬼");
                Say(pc, 0, "…其实，$R我对这个碎片很感兴趣，$R可以请你把这片碎片让给我吗？$R$R我会给你一些其他东西作为交换的。", "少女沙月·魉皇鬼");
                if (Select(pc, " ", "", "好好好", "丢掉也不给你") == 1)
                {
                    Say(pc, 0, "太谢谢了！$R$R忘记做自我介绍了，我叫沙月，$R是比你们早一些来到这座岛上的冒险者。", "少女沙月·魉皇鬼");
                    Say(pc, 0, "其实之前我也尝试着$R去帮助解决离岛的异变$R$R但是在尝试从海盗岛登录的时候$R遭到了正体不明的阻挡。", "少女沙月·魉皇鬼");
                    Say(pc, 0, "不…$R$R我也不清楚究竟它是来自何方。$R$R但是这个敌人的身上有着非常强大的$R破坏幻术与干扰时间的反制魔法，$R正好是克制我的魔法的类型。", "少女沙月·魉皇鬼");
                    Say(pc, 0, "这片碎片中可以隐约感觉到$R那种力量的残留$R$R我想，$R它对我来说会是非常重要的研究材料。", "少女沙月·魉皇鬼");
                    Select(pc, " ", "", "那么，就给你吧，我拿着也不知道有什么用。");
                    Say(pc, 0, "真是帮大忙了，$R$R我也没有什么能给你的，$R希望这个能帮到你吧。", "少女沙月·魉皇鬼");
                    TakeItem(pc, 100704001, 5);//取走5个奇怪的碎片
                    pc.SkillPoint3 += 1;//得到一个技能点
                    SagaMap.Network.Client.MapClient.FromActorPC(pc).SendPlayerInfo();//发送玩家信息封包
                    ShowEffect(pc, 4131);//显示特效，ID4131
                    pc.CInt["沙月技能点任务标记"] = 1;//技能点获取标记
                    Wait(pc, 3000);
                    Say(pc, 0, "获得了1点技能点", " ");
                    Say(pc, 0, "啊对了，$R$R这件事不要告诉团长啊，$R他似乎也在收集离岛事件相关的$R线索与材料。", "少女沙月·魉皇鬼");
                    Say(pc, 0, "之前我试图收集其他材料的时候$R就明显觉得他有些不高兴了。$R$R我不想再因为这件事增添我们之间的沟壑。", "少女沙月·魉皇鬼");
                    Say(pc, 0, "那么总之，辛苦你了，冒险者。", "少女沙月·魉皇鬼");
                    return;
                }
                else
                {
                    Say(pc, 0, "……这样啊，打扰了。", "少女沙月·魉皇鬼");
                    return;
                }
            }
            else if(pc.CInt["沙月技能点任务标记"] == 0)
            {
                Say(pc, 0, "突然出现在那座岛屿的怪物……$R还有那个『$CR奇怪的碎片$CD』……$R到底是发生什么事了呢？$R如果能弄来一些，说不定就可以开始研究了。", "少女沙月·魉皇鬼");
                return;
            }
			if (pc.CInt["灾祸的见证者任务"] == 2)
            {
                Say(pc, 0, "……不知道东之国内会不会有$CR遗留的线索$CD呢？", "少女沙月·魉皇鬼");
                return;
            }
            if (pc.CInt["灾祸的见证者任务"] == 1)
			{
				Say(pc, 0, "……", "少女沙月·魉皇鬼");
				Say(pc, 0, "……", "少女沙月·魉皇鬼");
				Select(pc, " ", "", "……");
                NPCMove(pc, 60000006, 138, 138, 300, 7, 0xb, 111, 10, 0);
                Say(pc, 0, "啊，抱歉！$R$R你在和我说话吗？$R我刚刚在想事情，没有注意到你。", "少女沙月·魉皇鬼");
				Select(pc, " ", "", "在想事情？");
                Say(pc, 0, "…嗯。$R$R东方地牢中的夺魂者…$R总觉得，我很久之前就见到过这样的家伙，$R不，说不定其实就是这家伙…", "少女沙月·魉皇鬼");
				Wait(pc, 1000);
				Say(pc, 131, "还记得吗？它说过的那些话。$R幻象，暮色教团，愿望，收割灵魂，力量……$R$R仔细想想，$R这简直就和东之国发生的事件完全吻合不是吗？", "少女沙月·魉皇鬼");
				Say(pc, 131, "当初东之国究竟发生了什么，至今还是个谜。$R我最初因为对这个事件的兴趣来到鱼缸岛。$R然而这么长时间过去了，却一点相关的线索都没有找到。", "少女沙月·魉皇鬼");
				Wait(pc, 500);
				Say(pc, 2133, "可惜，现在的东之国已经彻底成为了死城，$R了解当时情况的人$R恐怕已经无法给我们任何答案了吧…", "少女沙月·魉皇鬼");
				pc.CInt["灾祸的见证者任务"] = 2;
				return;
			}
            Say(pc, 0, "喝着咖啡享受宁静的下午，$R没有比这更好的事了，$R你不这么认为吗？", "少女沙月");
        }
    }
}