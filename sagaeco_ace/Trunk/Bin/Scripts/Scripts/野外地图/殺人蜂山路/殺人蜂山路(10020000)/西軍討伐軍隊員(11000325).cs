using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;

using SagaScript.Chinese.Enums;
namespace SagaScript.M10020000
{
    public class S11000325 : Event
    {
        public S11000325()
        {
            this.EventID = 11000325;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<SRFSLFlags> mask = new BitMask<SRFSLFlags>(pc.CMask["SRFSL"]);
            int selection;
            if (pc.Level > 15)
            {
                Say(pc, 131, "往裡走是『蜜蜂巢穴』$R;" +
                    "$R对像你那样经验丰富的冒险家来说$R;" +
                    "会有点无聊的$R;" +
                    "这里就交给年轻的冒险家吧!$R;");
                return;
            }
            if (!mask.Test(SRFSLFlags.西軍討伐軍隊員第一次對話))
            {
                mask.SetValue(SRFSLFlags.西軍討伐軍隊員第一次對話, true);
                Say(pc, 131, "往里走是『蜜蜂巢穴』$R;" +
                    "从地面的裂缝中出来了蜜蜂$R;" +
                    "$P派调查团查明是$R;" +
                    "巨大蜂蜜『螫针蜂』的$R;" +
                    "$R决定击退了…给我下命令…$R;" +
                    "$P虽然进去就能知道$R;" +
                    "但是里面有我讨厌的魔物…$R;" +
                    "虽然不是不能做$R;" +
                    "但是我有虫子恐怖症…$R;" +
                    "$R你帮我击退可以吗?$R;");
            }
            selection = Select(pc, "做什么呢?", "", "讨伐蜜蜂巢穴!", "听螫针蜂攻略法", "什么都不做");
            while (selection == 2)
            {
                switch (selection)
                {
                    case 1:
                        if (pc.Quest == null)
                        {
                            if (pc.QuestRemaining < 3)
                            {
                                Say(pc, 131, "这个任务是消耗任务点数『3』$R;" +
                                    "$R怎么看都觉得任务点数不足啊$R;" +
                                    "不好意思下次再挑战吧!$R;");
                                return;
                            }
                            Say(pc, 131, "这个任务是消耗任务点数『3』$R;");
                            switch (Select(pc, "接受任务?", "", "当然接受了", "还是放弃吧!"))
                            {
                                case 1:
                                    HandleQuest(pc, 20);
                                    break;
                                case 2:
                                    break;
                            }
                            return;
                        }
                        Say(pc, 131, "你在执行其他任务中啊$R;" +
                            "$R等结束现在的任务后$R;" +
                            "可以再过来吗?$R;");
                        break;
                    case 2:
                        Say(pc, 131, "螫针蜂的周边有叫『蚂蜂』的$R;" +
                            "像血一样红的蜜蜂$R;" +
                            "$P想要同时击退螫针蜂和蚂蜂的话$R;" +
                            "会马上全灭的$R;" +
                            "$R首先想办法把魔物们拉开$R;" +
                            "$P我们通常使用的作战是$R;" +
                            "『马拉松大作战!』$R;" +
                            "$R选一个有体力的家伙后$R;" +
                            "在那些魔物们周围全力奔驰引起注意$R;" +
                            "$P趁那个机会藏起来的其他队员$R;" +
                            "一个一个杀掉蚂蜂$R;" +
                            "$R因为蚂蜂的迴避率较高$R;" +
                            "所以比较难攻击$R;" +
                            "可以魔法攻击的话会比较容易$R;" +
                            "$P杀掉蚂蜂后全体人员攻击螫针蜂$R;" +
                            "会很容易击退的$R;");
                        break;
                    case 3:
                        break;
                }
                selection = Select(pc, "做什么呢?", "", "讨伐蜜蜂巢穴!", "听螫针蜂攻略法", "什么都不作");
            }
        }
    }
}
