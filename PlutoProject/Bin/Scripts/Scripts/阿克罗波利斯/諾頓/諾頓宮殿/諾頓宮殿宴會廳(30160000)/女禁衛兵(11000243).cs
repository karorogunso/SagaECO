using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30160000
{
    public class S11000243 : Event
    {
        public S11000243()
        {
            this.EventID = 11000243;

            this.notEnoughQuestPoint = "女王陛下委托的事情要$R;" +
                                       "消耗任务点数『3』喔$R;" +
                                       "$R是很重要的事情$R;" +
                                       "请您认真对待哦$R;";
            this.leastQuestPoint = 3;
            this.questFailed = "…$R;" +
                               "$R女王陛下是宽宏大量的人$R;" +
                               "一定会原谅您的$R;";
            this.alreadyHasQuest = "任务顺利吗？$R;";
            this.gotNormalQuest = "那拜託了$R;" +
                "$R等任务结束后，再来找我吧;";
            this.questCompleted = "祝贺成功$R;" +
                                  "请收下报酬吧$R;";
            this.questCanceled = "…真遗憾呀$R;";
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<NDFlags> mask = new BitMask<NDFlags>(pc.CMask["ND"]);
            
            if (mask.Test(NDFlags.职业装任务))
            {
                职业装任务(pc);
                return;
            }


            Say(pc, 131, "…$R;" +
                "从这里开始，禁止出入$R;");

        }

        void 职业装任务(ActorPC pc)
        {
            Say(pc, 131, "从女王陛下那里听说了$R;" +
                "打算怎么办？$R;");
            switch (Select(pc, "怎么办呢？", "", "任务服务台", "去宝物仓库", "什么也不做"))
            {
                case 1:
                    Say(pc, 131, "这是地下王立魔法研究所的委托$R;" +
                        "$R需要的是$R;" +
                        "能力很强的冒险者$R;" +
                        "$R委托内容如下：$R;" +
                        "$P击退迷你兔$R;" +
                        "昨天实验途中的迷您兔逃走了，$R;" +
                        "好像去了北边的洞窟里。$R;" +
                        "$R按照现在繁殖速度繁殖，会很麻烦的$R;" +
                        "赶快把这件事情处理一下吧$R;" +
                        "$P…迷你兔？$R;" +
                        "觉得很容易$R;" +
                        "也许不是一般的迷你兔吧$R;" +
                        "$P怎么办呢？$R;");
                    switch (Select(pc, "怎么办呢？", "", "接受委托", "不接受"))
                    {
                        case 1:
                            HandleQuest(pc, 26);
                            break;
                        case 2:
                            break;
                    }
                    break;
                case 2:
                    Say(pc, 131, "那么请跟我来吧$R;");
                    Warp(pc, 20013002, 63, 60);
                    break;
            }
        }
    }
}