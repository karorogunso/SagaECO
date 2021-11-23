
using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
using SagaDB.Actor;
using SagaMap.Mob;
using SagaDB.Mob;
using SagaMap.ActorEventHandlers;
namespace Exploration
{
    public class S11001575 : Event
    {
        public S11001575()
        {
            this.EventID = 11001575;
        }
        public override void OnEvent(ActorPC pc)
        {
            //ChangeMessageBox(pc);
            switch (pc.CInt["通天塔艾力克技能点任务"])
            {
                case 0:
                    Say(pc, 0, "特意来这里的冒险家……真少见，$R哦，抱歉，我不是有意冒犯。", "艾力克");
                    Say(pc, 0, "这里除了那座塔，就几乎只是个荒岛而已。$R来这里的冒险者，无外乎是对这座塔感到兴趣，$R但是……", "艾力克");
                    Say(pc, 0, "那座塔内有强力的守护者，$R一般的冒险者甚至都无法接近它，就会被它的气势震慑。", "艾力克");
                    Say(pc, 0, "嗯……如果你是对那座塔有兴趣的话，我们不妨来打个赌吧。", "艾力克");
                    Say(pc, 0, "赌你能否活着回来，呵呵呵……", "艾力克");
                    Select(pc, " ", "", "……？！");
                    Say(pc, 0, "如果你能活着回来，并带回战胜它的证明……", "艾力克");
                    Say(pc, 0, "那么相应地，我会奉上对你的敬意。", "艾力克");
                    pc.CInt["通天塔艾力克技能点任务"] = 1;
                    break;
                case 1:
                    if ((CountItem(pc, 10081300) == 0))
                    {
                        Say(pc, 0, "你已经忘了我们打的赌了吗？$R进入塔内，带回战胜塔内的守护者的证明，并活着回来见我。$R……还是说，你不敢去呢？", "艾力克");
                        return;
                    }
                    if ((CountItem(pc, 10081300) > 0))
                    {
                        TakeItem(pc, 10081300, 1);
                        Say(pc, 0, "这是……「蓝银的龙玉」$R也就是说，你成功打败了塔内的守护者？", "艾力克");
                        Say(pc, 0, "厉害，我对你刮目相看了。", "艾力克");
                        Wait(pc, 1000);
                        pc.SkillPoint3 += 1;
                        SagaMap.Network.Client.MapClient.FromActorPC(pc).SendPlayerInfo();
                        ShowEffect(pc, 4131);
                        pc.CInt["通天塔艾力克技能点任务"] = 2;
                        pc.CInt["通天塔艾力克技能点获得"] = 1;
                        Say(pc, 0, "获得了1点技能点", " ");
                        Say(pc, 0, "那么，这是我一点小小的敬意。$R祝你此后的旅程一帆风顺，伟大的冒险者。", "艾力克");
                        return;
                    }
                    break;
                default:
                    Say(pc, 0, "被派到这座无聊的岛上，哈啊……都快要睡着了。", "艾力克");
                    break;
            }
        }
    }
}

