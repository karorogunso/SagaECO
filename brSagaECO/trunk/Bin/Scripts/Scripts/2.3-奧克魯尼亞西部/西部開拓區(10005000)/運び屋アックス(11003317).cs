using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;

namespace SagaScript.M10005000
{
    public class S11003317 : Event
    {
        public S11003317()
        {
            this.EventID = 11003317;
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
                Say(pc, 0, "仕事の紹介感謝するぜ！$R;" +
                "$R俺はしばらくこの辺をに$R;" +
                "仕事してるから、何かあれば$R;" +
                "遠慮なく言ってくれ。$R;", "運び屋アックス");
                /*
                Say(pc, 0, "仕事の紹介感謝するぜ！$R;"+
                "$R俺はしばらくこの辺をに$R;" +
                "仕事してるから、何かあれば$R;" +
                "遠慮なく言ってくれ。$R;", "運び屋アックス");
                */
                switch (Select(pc, "要甚樣做？", "", "乘坐到奧克魯尼亞東部平原", "乘坐到北方海角的村子", "乘坐到東方海角的村子", "乘坐到南方海角的村子", "乘坐到廢礦坑的村子", "特に用事はない"))
                {
                    case 1:
                        Say(pc, 0, "南アクロニア平原的話$R" +
                            "丁度これから荷物を届けに行く所だ。$R" +
                            "ついでだし、タダで乗せてやるよ。$R" +
                            "$Rマックスピードでッ飛ばすから$R" +
                            "しっかりかまつとけよ！$R", "運び屋アックス");
                        /*
                        Say(pc, 0, "南アクロニア平原なら$R"+
                            "丁度これから荷物を届けに行く所だ。$R" +
                            "ついでだし、タダで乗せてやるよ。$R" +
                            "$Rマックスピードでッ飛ばすから$R" +
                            "しっかりかまつとけよ！$R", "運び屋アックス");
                            */
                        Warp(pc, 10031000, 135, 129);
                        break;
                    case 2:
                        Say(pc, 0, "ノーザリン岬の村か。$R" +
                            "３６０ゴールド、燃料代に貰えるなら$R" +
                            "構わないけどどうする？$R", "運び屋アックス");
                        switch (Select(pc, "要支付嗎", "", "支付", "不支付"))
                        {
                            case 1:
                                if (pc.Gold > 360)
                                {
                                    pc.Gold -= 360;
                                    Warp(pc, 10001000, 100, 24);
                                }
                                else
                                    Say(pc, 0, "所持金額不足$R", "運び屋アックス");
                                break;
                            case 2:
                                return;
                        }
                        break;
                    case 3:
                        Say(pc, 0, "イストー岬の村か。$R" +
                            "３８０ゴールド、燃料代に貰えるなら$R" +
                            "構わないけどどうする？$R", "運び屋アックス");
                        switch (Select(pc, "要支付嗎", "", "支付", "不支付"))
                        {
                            case 1:
                                if (pc.Gold > 380)
                                {
                                    pc.Gold -= 380;
                                    Warp(pc, 10018100, 195, 66);
                                }
                                else
                                    Say(pc, 0, "所持金額不足$R", "運び屋アックス");
                                break;
                            case 2:
                                return;
                        }
                        break;
                    case 4:
                        Say(pc, 0, "サウスリン岬の村か。$R" +
                            "４６０ゴールド、燃料代に貰えるなら$R" +
                            "構わないけどどうする？$R", "運び屋アックス");
                        switch (Select(pc, "要支付嗎", "", "支付", "不支付"))
                        {
                            case 1:
                                        if (pc.Gold > 460)
                                        {
                                            pc.Gold -= 460;
                                            Warp(pc, 10046000, 164, 216);
                                        }
                                        else
                                            Say(pc, 0, "所持金額不足$R", "運び屋アックス");
                                        break;
                            case 2:
                                return;
                        }
                        break;
                    case 5:
                        Say(pc, 0, "廃炭鉱の村か。$R" +
                            "３７０ゴールド、燃料代に貰えるなら$R" +
                            "構わないけどどうする？$R", "運び屋アックス");
                        switch (Select(pc, "要支付嗎", "", "支付", "不支付"))
                        {
                            case 1:
                                if (pc.Gold > 370)
                                {
                                    pc.Gold -= 370;
                                    Warp(pc, 10035000, 49, 179);
                                }
                                else
                                    Say(pc, 0, "所持金額不足$R", "運び屋アックス");
                                break;
                            case 2:
                                return;
                        }
                        break;
                }
            }
        }
    }
}
        