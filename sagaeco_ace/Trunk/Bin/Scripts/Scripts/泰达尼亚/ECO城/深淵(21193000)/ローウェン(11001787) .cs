using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
namespace SagaScript.M21193000
{
    public class S11001787 : Event
    {
        public S11001787()
        {
            this.EventID = 11001787;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<ECOchen> ECOchen_mask = new BitMask<ECOchen>(pc.CMask["ECOchen"]);
            //int selection;
            if (ECOchen_mask.Test(ECOchen.打到放水龙后))
            {
                if (CountItem(pc, 10066500) > 0)
                {
                    if (pc.ECoin >= 10000)
                    {
                        Say(pc, 0, "虹色的碎片拿来吧$R;" +
                                    "$P还有10000ecoin$R;" +
                                    "马上帮你做,哼哼$R;", "罗文");
                        if (Select(pc, "换么？", "", "换", "不换") == 1)
                        {
                            pc.ECoin -= 10000;
                            TakeItem(pc, 10066500, 1);
                            GiveItem(pc, 10066600, 1);
                            return;
                        }
                    }
                    else
                    {
                        Say(pc, 0, "没ecoin啊$R;", "罗文");
                    }
                    return;
                }
                Say(pc, 0, "你想要虹色的线？$R;" +
                        "$P准备虹色的碎片还有加工费$R;" + "ecoin10000,去吧$R;", "罗文");
                return;
            }
            Say(pc, 0, "見ての通り、僕は本が好きなんだ。$R;" +
            "$R本棚に好きな書籍が並んでいるだけで$R;" +
            "ニヤニヤしてしまえるぐらいにね。$R;" +
            "$Pさて、今日は何を読むかな？$R;", "罗文");
        }
    }
}