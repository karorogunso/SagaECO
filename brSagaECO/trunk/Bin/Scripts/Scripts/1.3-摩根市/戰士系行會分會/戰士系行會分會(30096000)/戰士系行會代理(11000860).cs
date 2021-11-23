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
                Say(pc, 131, "您好今天有什麼事情啊？$R;");
                switch (Select(pc, "今天有什麼事情啊?", "", "任務服務台", "什麼也不做"))
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
                        Say(pc, 131, "這裡委託的任務，$R;" +
                            "需要體力、精神、$R;" +
                            "更重要的是豐富的經驗呀$R;" +
                            "$P您想挑戰這個任務，$R;" +
                            "需要更多的修煉$R;" +
                            "先去累積經驗提高一下水準吧。$R;");
                        break;
                    case 2:

                        break;
                }
            }
            Say(pc, 131, "摩根原來沒那麼熱$R;" +
                "這行會裡不知為什麼非常熱…$R;" +
                "說實在的，好像蒸氣房阿$R;" +
                "$P哎呀…對戰士相關人士$R;" +
                "可得絕對保密唷$R;");
        }

        void 任務(ActorPC pc)
        {
            Say(pc, 131, "這裡是戰士系行會分會，$R;" +
                "給戰士系職業的人介紹各種工作的。$R;" +
                "$P不過這裡的任務工作$R;" +
                "說實在的比較困難阿。$R;" +
                "$P為什麼呀？$R;" +
                "因為委託的事情，$R都是只有戰士相關人士才能做到的$R;" +
                "擊退惡猛魔物之類的事情。$R;" +
                "$P看一下這裡的事情吧$R;" +
                "$R連埃米爾知道後，都逃走了$R;" +
                "就可以知道嚴重性了…$R;");
            Say(pc, 131, "您…少說廢話阿！$R;");
            Say(pc, 131, "哎呀…我說了不該說的話了$R;" +
                "$P很多都是危險的事情，$R;" +
                "所以只給有經驗的人介紹事情呀。$R;" +
                "$P看一下…讓我找一找$R;" +
                "有沒有適合您的事情唷$R;");
            //_0c74 = true;
        }
    }
}