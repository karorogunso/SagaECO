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

            this.notEnoughQuestPoint = "现在不需要帮忙$R;" +
    "不如先去冒个险吧$R;";
            this.leastQuestPoint = 1;
            this.questFailed = "失败了吗…$R;" +
    "$P如果觉得做不来$R;" +
    "还是别接吧$R;";
            this.alreadyHasQuest = "任务顺利吗?$R;";
            this.gotNormalQuest = "那么就拜托您了$R;" +
    "$R任务完成了$R;" +
    "再来跟我回报吧$R;";
            this.gotTransportQuest = "是啊，道具太重了吧$R;" +
                "所以不能一次传送的话$R;" +
                "分几次给就可以！;";
            this.questCompleted = "做的好！任务成功了！$R;" +
    "领取报酬吧！$R;";
            this.transport = "都集齐了吗？;";
            this.questCanceled = "…现在还开玩笑？哼！$R;";
            this.questTooEasy = "唔…但是对你来说$R;" +
                "说不定是太简单的事情$R;" +
                "$R那样也没关系嘛?$R;";
            this.questTooHard = "接受难度更高的任务$R;" +
    "好吗？$R;";
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
            switch (Select(pc, "…在店里别闹事", "", "买东西", "卖东西", "任务服务台", "接受情报", "什么也不做"))
            {
                case 1:
                    OpenShopBuy(pc, 04);
                    Say(pc, 131, "再来啊！$R;");
                    break;
                case 2:
                    OpenShopSell(pc, 04);
                    break;
                case 3:
                    if (mask.Test(MZTYSFlags.第一次任務))
                    {
                        Say(pc, 131, "想参与任务吗？？$R;" +
                            "在酒馆，$R不论您的职业是什么，有多少经验，$R;" +
                            "都可以参与各种任务$R;" +
                            "$R当中也有些很艰难的$R;" +
                            "要小心喔$R;");
                        mask.SetValue(MZTYSFlags.第一次任務, true);
                    }
                    HandleQuest(pc, 6);
                    break;
                case 4:
                    int a = Global.Random.Next(1, 2);
                    if (a == 1)
                    {
                        Say(pc, 131, "这个岛南边有一个黑暗之城。$R;" +
                            "$R从前好像是个美丽的城市$R;" +
                            "可是突然灭亡了……$R;" +
                            "$P好像是说什么仪式失败了$R;" +
                            "所以变成这样…$R;" +
                            "不过只是传闻，是真是假也不知道$R;" +
                            "$R据说现在还会有亡女的幽灵出没$R;" +
                            "$R所以不要随便进去啊$R;");
                        return;
                    }
                    Say(pc, 131, "听说城市东边的毒丛$R;" +
                        "有秘密的洞口$R;" +
                        "$R听说那个洞口$R;" +
                        "是可以通往丛林圣地的秘密洞口$R;" +
                        "要亲自去看看吗？$R;");
                    break;
            }
        }
    }
}