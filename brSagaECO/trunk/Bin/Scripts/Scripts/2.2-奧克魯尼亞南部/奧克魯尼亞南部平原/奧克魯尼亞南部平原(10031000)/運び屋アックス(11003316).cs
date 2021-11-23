using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;

namespace SagaScript.M10031000
{
    public class S11003316 : Event
    {
        public S11003316()
        {
            this.EventID = 11003316;
        }

        public override void OnEvent(ActorPC pc)
        {
            /*開發中 進度
            基本資料O
            原文搬運O
            翻譯校對O
            細節修正X
            */
            BitMask<ExpandedArea> ExpandedArea_mask = pc.CMask["ExpandedArea"];
            //int selection
            if (ExpandedArea_mask.Test(ExpandedArea.已得到油))
            {
                Say(pc, 0, "噢噢、是你呀。$R;" +
                "雖然我現在要回到$R;" +
                "西方拓荒區的村裡、你也要乘搭嗎？$R;", "運び屋アックス");
                /*
                Say(pc, 0, "おぉ、あんたか。$R;"+
                "これからウェストー開拓地の村まで$R;" +
                "戻るとこだが、あんたも乗ってくか？$R;", "運び屋アックス");
                */
                switch (Select(pc, "要甚樣做？", "", "麻煩你了", "不要前往"))
                //switch (Select(pc, "どうする？", "", "お願いします", "やめる"))
                {
                    case 1:

                        Say(pc, 0, "OK、唔那麼$R;" +
                        "由於以最高速度噴射跳躍$R;" +
                        "要緊緊地抓緊喔！$R;", "運び屋アックス");
                        /*
                        Say(pc, 0, "オゥケー、んじゃ$R;" +
                            "マックスピードでブッ飛ばすから$R;" +
                            "しっかりかまつとけよ！$R;", "運び屋アックス");
                        */
                        NPCShow(pc, 11003317);
                        Warp(pc, 10005000, 114, 83);
                        break;
                }
            }
            if ((ExpandedArea_mask.Test(ExpandedArea.已說明需要油)) & (!ExpandedArea_mask.Test(ExpandedArea.已得到油)))
            {
                if (CountItem(pc, 10000701) >= 1)
                {
                    switch (Select(pc, "要甚樣做？", "", "把油交給他", "甚麼也不做"))
                    {
                        case 1:
                            int count = 0;
                            foreach (SagaDB.Item.Item i in NPCTrade(pc))
                            {
                                if (i.ItemID == 10000701)
                                    count += i.Stack;
                            }
                            if (count > 0)
                            {
                                ExpandedArea_mask.SetValue(ExpandedArea.已得到油, true);
                                TakeItem(pc, 10000701, 1);
                                Say(pc, 0, "交給了1個油");
                                Say(pc, 0, "ーー那麼$R;" +
                                    "そいつぁ、這不是油嗎！？$R;" +
                                    "$R難道為了我$R;" +
                                    "$R去入手了ってぇのか？$R;", "運び屋アックス");
                                /*
                                Say(pc, 0, "ーーって$R;" +
                                    "そいつぁ、オイルじゃねぇか！？$R;" +
                                    "$Rまさか俺のために$R;" +
                                    "$R手に入れてきてくれたってぇのか？$R;", "運び屋アックス");
                                    */
                                Say(pc, 0, "OK、唔那麼$R;" +
                                    "由於以最高速度噴射跳躍$R;" +
                                    "要緊緊地抓緊喔！$R;", "運び屋アックス");
                                Wait(pc, 500);
                                NPCShow(pc, 11003317);
                                Warp(pc, 10005000, 114, 83);
                            }
                            break;
                        case 2:
                            Say(pc, 0, "そうつぁ、ありがたいんだが$R;" +
                                "弱った事に、燃料切らしちまった上に$R;" +
                                "財布もスカンビン。$R;" +
                                "$Rオイルが1個ありゃいいだが……$R;", "運び屋アックス");
                            return;
                    }
                }
                else
                {
                    Say(pc, 0, "そうつぁ、ありがたいんだが$R;" +
                        "弱った事に、燃料切らしちまった上に$R;" +
                        "財布もスカンビン。$R;" +
                        "$Rオイルが1個ありゃいいだが……$R;", "運び屋アックス");
                }
            }

            if ((ExpandedArea_mask.Test(ExpandedArea.已跟開拓師團長對話)) & (!ExpandedArea_mask.Test(ExpandedArea.已說明需要油)) & (ExpandedArea_mask.Test(ExpandedArea.已跟南平原運送員對話)))
            {
                ExpandedArea_mask.SetValue(ExpandedArea.已說明需要油, true);
                Say(pc, 0, "そうつぁ、ありがたいんだが$R;" +
                    "弱った事に、燃料切らしちまった上に$R;" +
                    "財布もスカンビン。$R;" +
                    "$Rオイルが1個ありゃいいだが……$R;", "運び屋アックス");
            }

            if ((!ExpandedArea_mask.Test(ExpandedArea.已跟南平原運送員對話)) || (!ExpandedArea_mask.Test(ExpandedArea.已跟開拓師團長對話)))
            {
                ExpandedArea_mask.SetValue(ExpandedArea.已跟南平原運送員對話, true);
                Say(pc, 0, "不管怎樣最近這附近的$R;" +
                    "速遞的工作減少了$R;" +
                    "不行哩$R;", "運び屋アックス");
                /*
                Say(pc, 0, "どうにも最近ここいらじゃ$R;" +
                    "運び屋の仕事減ってきてて$R;" +
                    "いけねぇ$R;", "運び屋アックス");
                */
            }
        }
    }
}
