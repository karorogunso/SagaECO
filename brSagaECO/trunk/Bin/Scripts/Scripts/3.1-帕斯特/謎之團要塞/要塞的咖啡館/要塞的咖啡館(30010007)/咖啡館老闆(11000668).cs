using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30010007
{
    public class S11000668 : Event
    {
        public S11000668()
        {
            this.EventID = 11000668;

            this.notEnoughQuestPoint = "現在不需要幫忙$R;" +
    "不如先去冒個險吧$R;";
            this.leastQuestPoint = 1;
            this.questFailed = "失敗了嗎…$R;" +
    "$P如果覺得做不來$R;" +
    "還是別接吧$R;";
            this.alreadyHasQuest = "任務順利嗎?$R;";
            this.gotNormalQuest = "那麼就拜託您了$R;" +
    "$R任務完成了$R;" +
    "再來跟我回報吧$R;";
            this.gotTransportQuest = "是阿，道具太重了吧$R;" +
                "所以不能一次傳送的話$R;" +
                "分幾次給就可以！;";
            this.questCompleted = "做的好！任務成功了！$R;" +
    "領取報酬吧！$R;";
            this.transport = "都集齊了嗎？;";
            this.questCanceled = "…現在還開玩笑？哼！$R;";
            this.questTooEasy = "唔…但是對你來說$R;" +
                "說不定是太簡單的事情$R;" +
                "$R那樣也沒關係嘛?$R;";
            this.questTooHard = "接受難度更高的任務$R;" +
    "好嗎？$R;";
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<MZTYSFlags> mask = pc.CMask["MZTYS"];
            /*
            if (pc.Level > 49 && _5a70 && !_5a73)
            {
                if (_5a72)
                {
                    if (CheckInventory(pc, 10000503, 5))
                    {
                        _5a73 = true;
                        GiveItem(pc, 10000503, 5);
                        GiveItem(pc, 10000604, 1);
                        Say(pc, 131, "您好像成功了喔$R;" +
                            "利益代表那裡來消息了$R;" +
                            "$R這個就是報酬了$R;" +
                            "來收下吧$R;");
                        PlaySound(pc, 2040, false, 100, 50);
                        Say(pc, 131, "得到『復活藥水』$R;" +
                            "和『高級治癒藥水』$R;");
                        Say(pc, 131, "來…又要拜託您了$R;");
                        return;
                    }
                    Say(pc, 131, "您好像成功了喔$R;" +
                        "利益代表那裡來消息了$R;" +
                        "$R我會給您報酬的$R;" +
                        "把行李減輕一點，再來吧$R;");
                    return;
                }
                if (_5a71)
                {
                    _5a71 = true;
                    Say(pc, 131, "去諾頓宮殿左門守衛那，拿點東西$R;" +
                        "$R然後轉交給$R;" +
                        "行會宮殿的塔妮亞族利益代表好嗎？$R;" +
                        "$P這是關乎城市的大事$R;" +
                        "別跟人說喔$R;");
                    //GOTO EVT1100066851
                    return;
                }
                //SWITCH END
                Say(pc, 131, "哦，來得正好。$R;" +
                    "剛好有要拜託您的事情。$R;" +
                    "$R您能不能馬上去一趟諾頓宮殿呢？$R;");
                switch (Select(pc, "怎麼辦？", "", "接受", "不接受"))
                {
                    case 1:
                        Say(pc, 131, "是嗎？沒辦法吧$R;");
                        //GOTO EVT1100066851
                        break;
                    case 2:
                        _5a71 = true;
                        Say(pc, 131, "去諾頓宮殿左門守衛那，拿點東西$R;" +
                            "$R然後轉交給$R;" +
                            "行會宮殿的塔妮亞族利益代表好嗎？$R;" +
                            "$P這是關乎城市的大事$R;" +
                            "別跟人說喔$R;");
                        //GOTO EVT1100066851
                        break;
                }
                return;
            }
            if (pc.Level > 44 && !_5a68)
            {
                if (_5a67)
                {
                    if (CheckInventory(pc, 50037200, 1))
                    {
                        _5a68 = true;
                        GiveItem(pc, 50037200, 1);
                        Say(pc, 131, "這個就是報酬$R;" +
                            "但是不要問是誰給的$R;" +
                            "因為那是我們謎語團的規定$R;");
                        PlaySound(pc, 2040, false, 100, 50);
                        Say(pc, 131, "得到『羊角圓邊帽』$R;");
                        Say(pc, 131, "呀！以後可能還有$R;" +
                            "關於這個城的事情要您幫忙$R;" +
                            "$R到時候要麻煩您啊$R;");
                        return;
                    }
                    Say(pc, 131, "我想給您些報酬$R;" +
                        "去整理一下您的行李吧$R;");
                    return;
                }
                if (_5a66)
                {
                    _5a67 = true;
                    Say(pc, 131, "呵呵，成功了！$R;" +
                        "幸好有您，我才可以活下來$R;" +
                        "$R您說不知道發生了什麼事？$R;" +
                        "$P嗯……$R;" +
                        "我不能對委託我的人失信$R;" +
                        "所以不能跟您說喔$R;" +
                        "$R如果您有興趣的話$R;" +
                        "就去找城裡的吟游詩人$R;" +
                        "$P他好像在以不死之城為題材寫詩呢$R;" +
                        "$R應該會知道一些什麼吧…$R;");
                    if (CheckInventory(pc, 50037200, 1))
                    {
                        _5a68 = true;
                        GiveItem(pc, 50037200, 1);
                        Say(pc, 131, "這個就是報酬$R;" +
                            "但是不要問是誰給的$R;" +
                            "因為那是我們謎語團的規定$R;");
                        PlaySound(pc, 2040, false, 100, 50);
                        Say(pc, 131, "得到『羊角圓邊帽』$R;");
                        Say(pc, 131, "呀！以後可能還有$R;" +
                            "關於這個城的事情要您幫忙$R;" +
                            "$R到時候要麻煩您啊$R;");
                        return;
                    }
                    Say(pc, 131, "我想給您些報酬$R;" +
                        "去整理一下您的行李吧$R;");
                    return;
                }
                if (_5a60)
                {
                    _5a60 = true;
                    Say(pc, 131, "委託者就在不死之城深處$R;" +
                        "俗稱阿卡茲的房間的地方$R;" +
                        "詳情到那個地方後，再了解吧$R;" +
                        "$P這件事一定要保密$R;" +
                        "雖然這個任務挺危險$R;" +
                        "$R但我還是希望您自己一個人去喔$R;");
                    //GOTO EVT1100066851
                    return;
                }

                Say(pc, 131, "喂，就是你~$R;" +
                    "有些危險的任務，$R;" +
                    "$R帕斯特島南邊$R;" +
                    "是不死系總部的黑暗之城$R;" +
                    "你應該知道吧？$R;" +
                    "$R是有關那個城的事情……$R;");
                switch (Select(pc, "怎麼辦？", "", "不接受", "接受"))
                {
                    case 1:
                        Say(pc, 131, "是嗎？沒辦法吧$R;");
                        //GOTO EVT1100066851
                        break;
                    case 2:
                        _5a60 = true;
                        Say(pc, 131, "委託者就在不死之城深處$R;" +
                            "俗稱阿卡茲的房間的地方$R;" +
                            "詳情到那個地方後，再了解吧$R;" +
                            "$P這件事一定要保密$R;" +
                            "雖然這個任務挺危險$R;" +
                            "$R但我還是希望您自己一個人去喔$R;");
                        break;
                }
                return;
            }
            */
            switch (Select(pc, "…在店裡別鬧事", "", "買東西", "賣東西", "任務服務台", "接受情報", "什麼也不做"))
            {
                case 1:
                    OpenShopBuy(pc, 04);
                    Say(pc, 131, "再來啊！$R;");
                    break;
                case 2:
                    OpenShopSell(pc, 04);
                    break;
                case 3:
                    if (mask.Test(MZTYSFlags.第一次任務))
                    {
                        Say(pc, 131, "想參與任務嗎？？$R;" +
                            "在咖啡館，$R不論您的職業是什麼，有多少經驗，$R;" +
                            "都可以參與各種任務$R;" +
                            "$R當中也有些很艱難的$R;" +
                            "要小心喔$R;");
                        mask.SetValue(MZTYSFlags.第一次任務, true);
                    }
                    HandleQuest(pc, 6);
                    break;
                case 4:
                    int a = Global.Random.Next(1, 2);
                    if (a == 1)
                    {
                        Say(pc, 131, "這個島南邊有一個黑暗之城。$R;" +
                            "$R從前好像是個美麗的城市$R;" +
                            "可是突然滅亡了……$R;" +
                            "$P好像是說什麼儀式失敗了$R;" +
                            "所以變成這樣…$R;" +
                            "不過只是傳聞，是真是假也不知道$R;" +
                            "$R據說現在還會有亡女的幽靈出沒$R;" +
                            "$R所以不要隨便進去啊$R;");
                        return;
                    }
                    Say(pc, 131, "聽說城市東邊的毒叢$R;" +
                        "有秘密的洞口$R;" +
                        "$R聽說那個洞口$R;" +
                        "是可以通往叢林聖地的秘密洞口$R;" +
                        "要親自去看看嗎？$R;");
                    break;
            }
        }
    }
}