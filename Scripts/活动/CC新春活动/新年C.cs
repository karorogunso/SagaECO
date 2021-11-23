
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
    public class S60000027 : Event
    {
        public S60000027()
        {
            this.EventID = 60000027;
        }
        public override void OnEvent(ActorPC pc)
        {
            DateTime endDT = new DateTime(2017, 2, 12);//设置结束日期为2017年2月12日
            if (DateTime.Now < endDT)
            {
                if (pc.Account.GMLevel > 200 && pc.CInt["CC新春活动"] == 0 && pc.Level >= 20)//此段代码为新年C所用
                {
                    ChangeMessageBox(pc);
                    Say(pc, 60000025, 131, "嗯嗯嗯？", "新年c");
                    Say(pc, 60000025, 131, "你居然看得见我！？", "新年c");
                    if (Select(pc, " ", "", "是呀是呀", "没有，快滚") == 1)
                    {
                        Say(pc, 60000025, 131, "既然那么有缘！我们来玩个游戏如何？", "新年c");
                        Say(pc, 60000025, 131, "如果你赢了的话，我就送你个新年特大礼包！", "新年c");
                        Select(pc, " ", "", "什么游戏……？");
                        GiveItem(pc, 950000033, 1);//item9创建道具：身份卡-平民
                        pc.CInt["CC新春活动"] = 1;
                        Say(pc, 60000025, 131, "你知道“狼人杀”吗？", "新年c");
                        Say(pc, 60000025, 131, "简单说就是由一方玩家扮演狼人，另一方玩家扮演村民。$R村民方以投票为手段投死狼人获取最后胜利，$R狼人阵营隐匿于村民中间，靠夜晚杀人及投票消灭村民方成员为获胜手段。", "新年c");
                        Say(pc, 60000025, 131, "但是我们这次玩的没有那么复杂~", "新年c");
                        Say(pc, 60000025, 131, "你等会……我先给你一张身份卡……", "新年c");
                        Say(pc, 60000025, 131, "嗯，这样人数就齐了！", "新年c");
                    }
                }
                if (pc.CInt["CC新春活动"] == 1 && pc.CInt["CCHelloComplete"] == 0)
                    {
                        ChangeMessageBox(pc);
                        Say(pc, 60000025, 131, "这次的游戏有10个人参加，$R啊当然有包括你啦！游戏规则是这样的：", "新年c");
                        Say(pc, 60000025, 131, "我会给每个参加的人发一张身份卡，$R其中有2张凶手卡，6张平民卡，还有1张小丑卡", "新年c");
                        Say(pc, 60000025, 131, "凶手卡也就是扮演“狼人”的一方，$R平民卡就是扮演“村民”的一方，$R在这次的游戏里，“狼人”和“村民”一样，$R事先并不知道其他人的身份哦！", "新年c");
                        Say(pc, 60000025, 131, "至于小丑卡嘛……比较特殊！$R持有它的人能事先知道其他人的身份，$R并以自己的意志决定要站在哪一个阵营！", "新年c");
                        Say(pc, 60000025, 131, "对了，$R自己是不能主动向他人展示身份卡的哦~$R这样就没意思啦。", "新年c");
                        Say(pc, 60000025, 131, "大体规则就是这样！$R剩下的等游戏正式开始后再说吧！", "新年c");
                        Say(pc, 60000025, 131, "现在还在准备阶段呢……$R不然……你先去认识下其他参赛者如何？", "新年c");
                        Say(pc, 60000025, 131, "他们是：$R3个“做料理的人”，$R2个“坐着休息的人”，$R2个“在室内的人”，$R1个“手持剑之人”，$R1个“手持魔法书之人”", "新年c");
                        return;
                    }
                    if (pc.CInt["CC新春活动"] == 1 && pc.CInt["CCHelloComplete"] == 1)
                    {
                        ChangeMessageBox(pc);
                        Say(pc, 0, "咦？你这么快就都认识好了？", "新年c");
                        Say(pc, 0, "游戏将在下午5点正式开始，$R现在离正式开始还有3个小时呢~", "新年c");
                        Say(pc, 0, "嘛，我们这边也有很多东西要筹备的啦！", "新年c");
                        Say(pc, 0, "你先随便逛逛吧！", "新年c");
                       if (Select(pc, "……", "", "在这等3个小时", "先去干点别的事") == 1)
                       {
                        Say(pc, 0, "（你发呆了3小时）", pc.Name);
                        Say(pc, 0, "……", pc.Name);
                        Say(pc, 0, "好的！$R游戏要开始啦！$R我们先前往集合地点吧！", "新年c");
                        Warp(pc, 10054001, 149, 144);//复制后的鱼缸岛
                        pc.CInt["CC新春活动"] = 2;
                        SetNextMoveEvent(pc, 4000000);
                        return;
                       }
                       else
                        return;
                }
                    if (pc.CInt["CC新春活动"] >= 2)
                    {
                        ChangeMessageBox(pc);
                        Say(pc, 0, "咦？你怎么在这？", "新年c");
                        Say(pc, 0, "……哦，我懂了，人有三急嘛。", "新年c");
                        Say(pc, 0, "那么，你要回去狼人杀活动场地吗？", "新年c");
                        if (Select(pc, "怎么说？", "", "走！", "……突然肚子又痛了") == 1)
                        {
                            Warp(pc, 10054001, 149, 144);//复制后的鱼缸岛
                            SetNextMoveEvent(pc, 4000000);
                            return;
                        }
                        else
                            Say(pc, 0, "咳咳，你该不会是想临阵脱逃吧？！", "新年c");
                        return;
                    }
            }
            Say(pc, 0, "……", "新年c");
        }
    }
}