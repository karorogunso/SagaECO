using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Item;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30096000
{
    public class S11000860 : Event
    {
        public S11000860()
        {
            this.EventID = 11000860;

        }

        public override void OnEvent(ActorPC pc)
        {
            if (pc.JobBasic == PC_JOB.SWORDMAN ||
                pc.JobBasic == PC_JOB.FENCER ||
                pc.JobBasic == PC_JOB.SCOUT ||
                pc.JobBasic == PC_JOB.ARCHER)
            {
                Say(pc, 131, "您好今天有什么事情啊？$R;");
                switch (Select(pc, "今天有什么事情啊?", "", "任务服务台", "什么也不做"))
                {
                    case 1:
                        if (pc.Level > 63 &&
                            pc.JobBasic == PC_JOB.SWORDMAN &&
                            pc.Job != PC_JOB.SWORDMAN)
                        {
                            任務(pc);
                            return;
                        }
                        if (pc.Level > 63 &&
                            pc.JobBasic == PC_JOB.FENCER &&
                            pc.Job != PC_JOB.FENCER)
                        {
                            任務(pc);
                            return;
                        }
                        if (pc.Level > 63 &&
                            pc.JobBasic == PC_JOB.SCOUT &&
                            pc.Job != PC_JOB.SCOUT)
                        {
                            任務(pc);
                            return;
                        }
                        if (pc.Level > 63 &&
                            pc.JobBasic == PC_JOB.ARCHER &&
                            pc.Job != PC_JOB.ARCHER)
                        {
                            任務(pc);
                            return;
                        }
                        Say(pc, 131, "这里委托的任务，$R;" +
                            "需要体力、精神、$R;" +
                            "更重要的是丰富的经验呀$R;" +
                            "$P您想挑战这个任务，$R;" +
                            "需要更多的修炼$R;" +
                            "先去累积经验提高一下水准吧。$R;");
                        break;
                    case 2:

                        break;
                }
            }
            Say(pc, 131, "摩戈原来没那么热$R;" +
                "这行会里不知为什么非常热…$R;" +
                "说实在的，好像蒸气房阿$R;" +
                "$P哎呀…对战士相关人士$R;" +
                "可得绝对保密哦$R;");
        }

        void 任務(ActorPC pc)
        {
            Say(pc, 131, "这里是战士系行会分会，$R;" +
                "给战士系职业的人介绍各种工作的。$R;" +
                "$P不过这里的任务工作$R;" +
                "说实在的比较困难阿。$R;" +
                "$P为什么呢？$R;" +
                "因为委托的事情，$R都是只有战士相关人士才能做到的$R;" +
                "击退凶猛魔物之类的事情。$R;" +
                "$P看一下这里的事情吧$R;" +
                "$R连埃米尔知道后，都逃走了$R;" +
                "就可以知道严重性了…$R;");
            Say(pc, 131, "您…少说废话阿！$R;");
            Say(pc, 131, "哎呀…我说了不该说的话了$R;" +
                "$P很多都是危险的事情，$R;" +
                "所以只给有经验的人介绍事情呀。$R;" +
                "$P看一下…让我找一找$R;" +
                "有没有适合您的事情唷$R;");
            //_0c74 = true;
        }
    }
}