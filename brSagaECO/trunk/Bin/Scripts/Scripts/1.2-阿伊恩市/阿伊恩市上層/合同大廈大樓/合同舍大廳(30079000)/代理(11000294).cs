using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30079000
{
    public class S11000294 : Event
    {
        public S11000294()
        {
            this.EventID = 11000294;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<Knights> Knights_mask = pc.CMask["Knights"];
            switch (Select(pc, "什麼事呢？", "", "凱特靈炮的銷售許可", "我不喜歡政府！"))
            {
                case 1:
                    Say(pc, 131, "您說是凱特靈炮的銷售許可嗎？$R;" +
                        "稍等$R;" +
                        "$P…$R;" +
                        "$P按照阿伊恩薩烏斯聯邦法第36條$R;" +
                        "凱特靈炮只可販賣給南軍$R;" +
                        "$R…是這樣寫著的$R;" +
                        "如果是南軍的話，應該擁有$R;" +
                        "『阿伊恩薩烏斯騎士團證』吧？$R;" +
                        "$R請出示證件阿$R;");
                    switch (Select(pc, "出示證件嗎？", "", "是的", "不了"))
                    {
                        case 1:
                            if (CountItem(pc, 10041500) >= 1 && !Knights_mask.Test(Knights.加入南軍騎士團))
                            {
                                Say(pc, 131, "那麼讓我查証一下$R;" +
                                    "確認期間，請稍等$R;" +
                                    "$P…$R;" +
                                    "$P…$R;" +
                                    "南軍登記簿上，沒有您的名字呢。$R;" +
                                    "$P這個是誰的許可証啊？$R;" +
                                    "這樣我們不能辦理通行喔，$R;" +
                                    "$R反正就是不行$R;" +
                                    "不能淮許其他所屬軍隊通過$R;" +
                                    "回去吧$R;");
                                return;
                            }
                            if (CountItem(pc, 10041500) >= 1 && Knights_mask.Test(Knights.加入南軍騎士團))
                            {
                                Say(pc, 131, "那麼讓我查証一下$R;" +
                                    "確認期間，請稍等$R;" +
                                    "$P…$R;" +
                                    "$P…$R;" +
                                    "$R確認完畢$R;" +
                                    "凱特靈購入稅5000金幣$R;" +
                                    "凱特靈登記稅3000金幣$R;" +
                                    "凱特靈持有稅1200金幣$R;" +
                                    "$R凱特靈稅金一共9200金幣。$R;" +
                                    "沒關係嗎？$R;");
                                switch (Select(pc, "支付凱特靈稅金9200金幣嗎？", "", "支付", "不支付"))
                                {
                                    case 1:
                                        if (pc.Gold > 9199)
                                        {
                                            if (CheckInventory(pc, 10048000, 1))
                                            {
                                                GiveItem(pc, 10048000, 1);
                                                pc.Gold -= 9200;
                                                Say(pc, 131, "給您許可証$R;");
                                                return;
                                            }
                                            Say(pc, 131, "行李太多了，整理後再來吧$R;");
                                            return;
                                        }
                                        Say(pc, 131, "錢好像不夠啊$R;");
                                        break;
                                    case 2:
                                        Say(pc, 131, "下一位$R;");
                                        break;
                                }
                                return;
                            }
                            Say(pc, 131, "沒有證件的話，是不能通行的。$R;" +
                                "$R拿到阿伊恩薩烏斯騎士團證$R;" +
                                "再來吧。$R;" +
                                "『阿伊恩薩烏斯騎士團證』吧？$R;");
                            break;
                        case 2:
                            Say(pc, 131, "下一位$R;");
                            break;
                    }
                    break;
                case 2:
                    Say(pc, 131, "下一位$R;");
                    break;
            }
        }
    }
}
