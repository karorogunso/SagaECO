using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30077001
{
    public class S11000584 : Event
    {
        public S11000584()
        {
            this.EventID = 11000584;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<Job2X_12> Job2X_12_mask = pc.CMask["Job2X_12"];

            if (!Job2X_12_mask.Test(Job2X_12.轉職開始))//_3A63)
            {
                return;
            }

            if (Job2X_12_mask.Test(Job2X_12.收集石油) && !Job2X_12_mask.Test(Job2X_12.給予石油))//_3A72 && !_3A77)
            {
                Say(pc, 11000589, 131, "古鲁杜先生!又开始出力了$R电压也上升了$R;" +
                    "$R阿~现在活了~!还以为错了呢!!$R;" +
                    "$R这大动力火炉是艾恩萨乌斯的心脏$R如果心脏停的话就是大事了啊!$R;");
                Say(pc, 11000584, 131, "嗯…从古董商店中找出来的出土品$R再活用部件好像很合适啊$R;" +
                    "$R这个都是托家伙的实力阿$R把阿克罗波利斯$R手艺好的机械当奖励吧$R;");
                Say(pc, 11000584, 131, "啊，是你啊$R太晚了~!太晚了啊!$R;" +
                    "$R…??$R;");

                if (CountItem(pc, 10000701) >= 10)
                {
                    TakeItem(pc, 10000701, 10);
                    Job2X_12_mask.SetValue(Job2X_12.給予石油, true);
                    Job2X_12_mask.SetValue(Job2X_12.收集花束, true);
                    Job2X_12_mask.SetValue(Job2X_12.收集石油, false);
                    //_3A77 = true;
                    //_3A73 = true;
                    //_3A72 = false;
                    Say(pc, 11000584, 131, "确实是「油」10份$R没问题$R;" +
                        "$R按照客人的订单$R确实的把商品送到$R对我们商人来説是最重要的事情$R;");
                    Say(pc, 11000589, 131, "啊，是弟子吗?$R「石油」真谢谢了$R;" +
                        "$P这个「石油」是蒸馏精制后$R制作大动力火炉的润滑油$R;" +
                        "$R总是欠对古鲁杜先生人情啊$R;" +
                        "$P古鲁杜先生$R收了相当聪明的弟子啊~$R;");
                    Say(pc, 11000584, 131, "不是!!还远着呢，不要夸了!$R;" +
                        "$R那下次要转交东西的地方是…$R;" +
                        "可以给在阿克罗波利斯上城的$R4楼的行会宫殿导游小姐$R转交10束「花束」吗?$R;" +
                        "$P稍后我也会去的$R那就拜托了!$R;");
                    return;
                }
                Say(pc, 11000584, 131, "!!$R不是「油」10份啊!!$R重新拿过来!$R;");
                return;
            }
            Say(pc, 11000584, 131, "可以给在阿克罗波利斯上城的$R4楼的行会宫殿导游小姐$R转交10束「花束」吗?$R;" +
                "$R那就拜托了$R;");
        }
    }
}