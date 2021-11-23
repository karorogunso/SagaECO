using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30134001
{
    public class S11000762 : Event
    {
        public S11000762()
        {
            this.EventID = 11000762;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<PSTFlags> mask = pc.CMask["PST"];
            if (mask.Test(PSTFlags.獲得牛牛))//_5a80)
            {
                Say(pc, 131, "噢噢！您來了！$R;" +
                    "小花的孩子怎樣？健康嗎？$R;");
                return;
            }
            if (mask.Test(PSTFlags.獲得牛牛的對話))//_5a79)
            {
                if (CheckInventory(pc, 10013002, 1))
                {
                    mask.SetValue(PSTFlags.獲得牛牛, true);
                    mask.SetValue(PSTFlags.獲得牛牛的對話, false);
                    //_5a80 = true;
                    //_5a79 = false;
                    GiveItem(pc, 10052100, 1);
                    PlaySound(pc, 2040, false, 100, 50);
                    Say(pc, 131, "收到『牛牛』！$R;", "");
                    Say(pc, 131, "好好珍惜吧！$R;" +
                        "偶爾要過來喔！$R;" +
                        "$R知道嗎？$R;");
                    Say(pc, 11000763, 364, "哞嗚！$R;");
                    return;
                }
                Say(pc, 131, "行李太多了$R;");
                return;
            }
            if (mask.Test(PSTFlags.給予甜草))//_5a78)
            {
                int c = Global.Random.Next(1, 5);
                if (c <= 2)
                {
                    Say(pc, 131, "還没有生啊？$R;" +
                        "…我現在也累了$R;");
                    return;
                }
                if (c <= 4)
                {
                    Say(pc, 131, "最近看小花的樣子$R;" +
                        "好像快生了！$R;");
                    return;
                }
                Say(pc, 131, "既然這樣，最好您在的時候$R;" +
                    "生出來就好了$R;");
                Say(pc, 11000763, 331, "哞！哞~！$R;");
                Say(pc, 131, "哦！$R;" +
                    "還好嗎？小花！！$R;");
                Say(pc, 11000763, 331, "哞~！哞~！！$R;");
                Say(pc, 131, "加油啊！$R;" +
                    "加油！小花！！$R;" +
                    "小花！！$R;");
                Say(pc, 11000763, 331, "哞嗚嗚~~!!$R;");
                PlaySound(pc, 4001, false, 100, 50); //FADE OUT WHITE
                Wait(pc, 6000);
                //FADE IN
                Say(pc, 11000763, 331, "哞！哞！$R;");
                Say(pc, 131, "噢噢噢！看那邊！生出來了！$R;" +
                    "$P…$R;" +
                    "$P起來!得站起來!$R;" +
                    "$P…起來了!$R;" +
                    "什麽？用兩隻腳站著？$R;");
                PlaySound(pc, 4009, false, 100, 50);
                Say(pc, 131, "牛牛是剛出生的時候就開始$R;" +
                    "兩隻腳走路的動物啊…？$R;");
                Say(pc, 11000763, 131, "哞嗚！$R;");
                mask.SetValue(PSTFlags.獲得牛牛的對話, true);
                mask.SetValue(PSTFlags.給予甜草, false);
                //_5a79 = true;
                //_5a78 = false;
                Say(pc, 131, "順利生下真是太幸運了~$R;" +
                    "我也累了，您已經很努力了$R;" +
                    "$P光說謝謝好像不夠，$R;" +
                    "不然您能不能負責這孩子？$R;" +
                    "$R老實説…一個小花也夠我受的了$R;" +
                    "如果是給您養，也不錯啊$R;" +
                    "不不…如果是您的話$R可以安心的交給您$R;" +
                    "$R是不是？小花~？$R;");
                Say(pc, 11000763, 131, "哞嗚~！$R;");
                return;
            }
            if (mask.Test(PSTFlags.尋找甜草))//_5a77)
            {
                if (CountItem(pc, 10004906) >= 1)
                {
                    mask.SetValue(PSTFlags.給予甜草, true);
                    mask.SetValue(PSTFlags.尋找甜草, false);
                    //_5a78 = true;
                    //_5a77 = false;
                    TakeItem(pc, 10004906, 1);
                    Say(pc, 131, "但現在無法離開小花！$R;");
                    Say(pc, 131, "這個真是謝謝了！$R;" +
                        "應該是我親自去的採的$R;" +
                        "但現在無法離開小花！$R;");
                    return;
                }
                Say(pc, 131, "「甜草」還没有好嗎？$R;" +
                    "小花也在等著呢！$R;");
                Say(pc, 11000763, 131, "哞哞！$R;");
                return;
            }
            if (mask.Test(PSTFlags.給予香草))//_5a76)
            {
                int b = Global.Random.Next(1, 10);
                if (b == 1)
                {
                    mask.SetValue(PSTFlags.尋找甜草, true);
                    mask.SetValue(PSTFlags.給予香草, false);
                    //_5a77 = true;
                    //_5a76 = false;
                    Say(pc, 131, "您是不是很無聊啊?$R;" +
                        "我在這裡是工作…$R;" +
                        "$P無聊的話去採採$R;" +
                        "給小花的飼料怎樣？$R;" +
                        "對了！這次「甜草」不錯啊!$R;" +
                        "「甜草」真的很不錯的啊!$R;" +
                        "$R呐!快去快回!$R;");
                    return;
                }
                if (b <= 5)
                {
                    Say(pc, 131, "小花最近變得好安靜！$R;" +
                        "好像快到時間了$R;");
                    return;
                }
                Say(pc, 131, "我再説一遍!$R小花絶…對…不會給別人的$R;");
                return;
            }
            if (mask.Test(PSTFlags.尋找香草))//_5a75)
            {
                if (CountItem(pc, 10004902) >= 1)
                {
                    mask.SetValue(PSTFlags.給予香草, true);
                    mask.SetValue(PSTFlags.尋找香草, false);
                    //_5a76 = true;
                    //_5a75 = false;
                    TakeItem(pc, 10004902, 1);
                    Say(pc, 131, "給他香草$R;", "");
                    Say(pc, 131, "真感謝您$R;" +
                        "應該是我去採的$R;" +
                        "但是，我好擔心小花$R没辦法離開牠身邊$R;" +
                        "$R太感謝了$R;");
                    return;
                }
                Say(pc, 131, "「香草」還没有找到嗎?$R;" +
                    "小花在等著呢~$R;");
                Say(pc, 11000763, 131, "哞嗚$R;");
                return;
            }
            if (mask.Test(PSTFlags.給予健康營養飲料))//_5a74)
            {
                int a = Global.Random.Next(1, 10);
                if (a == 1)
                {
                    mask.SetValue(PSTFlags.尋找香草, true);
                    mask.SetValue(PSTFlags.給予健康營養飲料, false);
                    //_5a75 = true;
                    //_5a74 = false;
                    Say(pc, 131, "没關係！不要慌！$R;" +
                        "$R没有那麽快産子…$R;" +
                        "$P無聊的話去弄弄$R;" +
                        "給小花的食物如何？$R;" +
                        "$R是呀！營養豐富的$R;" +
                        "「香草」是不錯啊!$R;" +
                        "$R快點去吧！$R;");
                    return;
                }
                if (a <= 6)
                {
                    Say(pc, 131, "嗯？$R;" +
                        "那麽喜歡小花嗎？$R;" +
                        "$R但不要那麽貪心啊$R;" +
                        "我才不會把小花給任何人的！$R;");
                    return;
                }
                Say(pc, 131, "真認真！$R;" +
                    "慢慢等等看看…$R;");
                return;
            }
            if (pc.Fame > 10)
            {
                Say(pc, 131, "怎樣？$R;" +
                    "我小時候就開始養的牛牛！$R;" +
                    "叫做小花！$R;" +
                    "$R漂亮嗎?$R;" +
                    "$P再過一陣子就生産了！$R;" +
                    "要忙於準備囉！$R;" +
                    "$R得充分吸收營養呢~$R;" +
                    "有没有什麽好東西？$R;");
                if (CountItem(pc, 10000305) >= 1)
                {
                    Say(pc, 131, "什麽事？$R;");
                    switch (Select(pc, "怎麼辦?", "", "用這個吧！", "看起來可口的乳牛啊"))
                    {
                        case 1:
                            mask.SetValue(PSTFlags.給予健康營養飲料, true);
                            //_5a74 = true;
                            Say(pc, 131, "給他健康營養飲料$R;", "");
                            Say(pc, 131, "這是什麽？$R;" +
                                "您也擔心小花啊？$R;" +
                                "$R無論如何，都很感謝您!$R;" +
                                "一個人受累了，謝謝!$R;" +
                                "$P但是!這個給動物也可以嗎？$R;" +
                                "…可以吧…$R;" +
                                "$P這個或許也是什麽緣分$R;" +
                                "以後經常過來吧$R;" +
                                "$R小花應該也會很高興的！$R;" +
                                "是吧？小花？$R;");
                            Say(pc, 11000763, 364, "牛牛！！$R;");
                            TakeItem(pc, 10000305, 1);
                            break;
                        case 2:
                            Say(pc, 131, "什麽？$R;" +
                                "説什麽呢！$R;" +
                                "$R那個啊…$R應該是蠻好吃的，但是…$R;");
                            Say(pc, 11000763, 331, "牛牛！！$R;");
                            Say(pc, 131, "什麽！？$R;" +
                                "快點出去吧！！$R;");
                            break;
                    }
                    return;
                }
                return;
            }
            Say(pc, 131, "怎樣？$R;" +
                "我小時候就開始養的牛牛！$R;" +
                "叫做小花！$R;" +
                "$R漂亮嗎?$R;" +
                "$P現在已經懷了孩子$R;" +
                "希望快點出生…$R;");
        }
    }
}