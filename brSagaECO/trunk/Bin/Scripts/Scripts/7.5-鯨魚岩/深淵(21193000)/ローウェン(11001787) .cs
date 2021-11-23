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
                        Say(pc, 0, "没ecoin$R;", "ローウェン");
                    }
                    return;
                }
                Say(pc, 0, "你想要虹の糸？$R;" +
                "$P必须准备虹のかけら还有加工费ecoin10,000我才帮你做$R;", "ローウェン");
                return;
            }
            Say(pc, 0, "如你所見、我喜愛書本。$R;" +
            "$R書架上只排列好我喜愛的書$R;" +
            "大概會咧呀咧呀地笑呢。$R;" +
            "$P那麼、今日要讀哪本呢？$R;", "露依斯");
            //
            /*
            Say(pc, 0, "見ての通り、僕は本が好きなんだ。$R;" +
            "$R本棚に好きな書籍が並んでいるだけで$R;" +
            "ニヤニヤしてしまえるぐらいにね。$R;" +
            "$Pさて、今日は何を読むかな？$R;", "ローウェン");
            */
        }
    }
}