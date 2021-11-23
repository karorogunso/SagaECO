using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Item;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30131001
{
    public class S11000768 : Event
    {
        public S11000768()
        {
            this.EventID = 11000768;
            this.leastQuestPoint = 1;
            this.notEnoughQuestPoint = "咦？$R;" +
    "$R任務點數好像不夠喔$R;" +
    "不好意思，可以明天再來嗎？$R;";
            this.questFailed = "失敗了？嗚嗚嗚嗚$R;";
            this.questCompleted = "做得好！$R;" +
    "看來您挺有天份啊。$R;";
            this.questCanceled = "取消？嗚嗚嗚！$R;";
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<MZTYSFlags> mask = pc.CMask["MZTYS"];
            if (!mask.Test(MZTYSFlags.交給謎語團面具))//_6a31)
            {
                if (mask.Test(MZTYSFlags.幫忙送謎語團面具))//_6a27)
                {
                    if (CountItem(pc, 10000306) == 0)
                    {
                        Say(pc, 131, "什麼？把藥丟了？$R;" +
                            "真是個糊塗的傢伙$R;" +
                            "給您，這次不要再丟了$R;");
                        if (CheckInventory(pc, 10000306, 1))
                        {
                            GiveItem(pc, 10000306, 1);
                            mask.SetValue(MZTYSFlags.幫忙送藥, true);
                            //_6a26 = true;
                            Say(pc, 131, "那我就拜託您了$R;" +
                                "$R一定要送到聖女島的約克那裡喔$R;");
                            return;
                        }
                        Say(pc, 131, "但您的行李好像太多了$R;" +
                            "這樣不行喔$R;");
                    }

                    Say(pc, 131, "怎麼了？去聖女島$R;" +
                        "可以從那邊的秘密通道過去$R;" +
                        "$R拜託了$R;");
                    return;
                }
                if (mask.Test(MZTYSFlags.收到謎語團面具))//_6a30)
                {
                    mask.SetValue(MZTYSFlags.幫忙送謎語團面具, true);
                    //_6a27 = true;
                    TakeItem(pc, 50025080, 1);
                    GiveItem(pc, 10000306, 1);
                    Say(pc, 131, "啊，就是這個$R;" +
                        "我的『謎語團面具』！！$R;" +
                        "幸好有它，這下好了。$R;" +
                        "$R現在不用擔心壞掉了$R;" +
                        "可以出去了$R;" +
                        "$P因為擔心在沒有面具的情況下$R;" +
                        "和海盜見面$R;" +
                        "所以我一直不敢出去呢$R;" +
                        "$P對了，不用再給約克送一次藥嗎？$R;" +
                        "$R約克對我來說，是個非常重要的人$R;" +
                        "如果不保持健康的話，就麻煩了！$R;");
                    return;
                }
                if (mask.Test(MZTYSFlags.交給藥))//_6a28)
                {
                    Say(pc, 131, "已經給我送過去了嗎?$R;" +
                        "真是感恩呢$R;" +
                        "$R那，約克沒有讓您給我帶點什麼嗎？$R;" +
                        "$P什麼…沒有？$R;" +
                        "$R那真是不好意思，您能不能$R;" +
                        "到約克在的地方再問一次$R;" +
                        "有沒有要給我的東西呢？$R;");
                    return;
                }
                if (mask.Test(MZTYSFlags.幫忙送藥))//_6a26)
                {
                    if (CountItem(pc, 10000306) == 0)
                    {
                        Say(pc, 131, "什麼？把藥丟了？$R;" +
                            "真是個糊塗的傢伙$R;" +
                            "給您，這次不要再丟了$R;");
                        if (CheckInventory(pc, 10000306, 1))
                        {
                            GiveItem(pc, 10000306, 1);
                            mask.SetValue(MZTYSFlags.幫忙送藥, true);
                            //_6a26 = true;
                            Say(pc, 131, "那我就拜託您了$R;" +
                                "$R一定要送到聖女島的約克那裡喔$R;");
                            return;
                        }
                        Say(pc, 131, "但您的行李好像太多了$R;" +
                            "這樣不行喔$R;");
                    }
                    Say(pc, 131, "怎麼了？去聖女島$R;" +
                        "可以從那邊的秘密通道過去$R;" +
                        "$R拜託了$R;");
                    return;
                }
                Say(pc, 131, "啊，這可怎麼辦啊？$R;" +
                    "約克身體又出問題了$R;" +
                    "$P現在那個也沒了啊$R;" +
                    "$R那個要是破碎了，$R;" +
                    "那我該怎麼辦啊？$R;" +
                    "$P哦，是您，太好了！$R;" +
                    "$R您把這藥$R;" +
                    "轉交給住在聖女島的約克好嗎？$R;" +
                    "$P因為我現在不能出去喔$R;" +
                    "$R拜託了$R;");
                switch (Select(pc, "怎麼辦呢？", "", "做", "不做"))
                {
                    case 1:
                        Say(pc, 131, "您真的會轉交給他吧$R;" +
                            "真是不錯的傢伙呢$R;" +
                            "$R這樣險惡的世界上$R;" +
                            "還讓我遇到您這樣的好人$R;" +
                            "呵呵$R;");
                        if (CheckInventory(pc, 10000306, 1))
                        {
                            GiveItem(pc, 10000306, 1);
                            mask.SetValue(MZTYSFlags.幫忙送藥, true);
                            //_6a26 = true;
                            Say(pc, 131, "那我就拜託您了$R;" +
                                "$R一定要送到聖女島的約克那裡喔$R;");
                            return;
                        }
                        Say(pc, 131, "但您的行李好像太多了$R;" +
                            "這樣不行喔$R;");
                        break;
                }
                return;
            }


            if (pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.HEAD))
            {
                if (pc.Inventory.Equipments[EnumEquipSlot.HEAD].ItemID == 50025000 ||
                    pc.Inventory.Equipments[EnumEquipSlot.HEAD].ItemID == 50026800)
                {
                    int a = Global.Random.Next(1, 2);

                    if (a == 1)
                    {
                        Say(pc, 131, "是來見團長的嗎？$R;" +
                            "$R團長現在不在$R;" +
                            "他跟別的幹部出去巡查了$R;");
                        return;
                    }
                    Say(pc, 131, "同志啊!$R;" +
                        "來得正好，快進去歇會兒吧！$R;");
                    return;
                }
            }
            Say(pc, 131, "呵呵$R;" +
                "$R帶著神秘的面具，$R;" +
                "堅持信念活下去的，就是謎語團$R;" +
                "$R我是謎語團的幹部佈魯。$R;");

            if (!mask.Test(MZTYSFlags.詢問謎團2))//_6a24)
            {
                if (mask.Test(MZTYSFlags.詢問謎團1))//_6a23)
                {
                    mask.SetValue(MZTYSFlags.詢問謎團1, true);
                    //_6a23 = true;
                    Say(pc, 131, "呵呵，$R;" +
                        "好像對我們謎語團略知一二，$R;" +
                        "那麼要不要來參加我們的活動？$R;");

                    switch (Select(pc, "參加嗎？", "", "參加", "不參加"))
                    {
                        case 1:
                            mask.SetValue(MZTYSFlags.詢問謎團2, true);
                            //_6a24 = true;
                            Say(pc, 131, "我們是熱愛自由的謎語團$R;" +
                                "$R不可以受到束縛！$R;" +
                                "規則是用來打破的!$R;" +
                                "$R這就是謎語團存在的意義！$R;" +
                                "$P或許您知道『和古代民族的約定』吧？$R;" +
                                "$R那是什麼呢？$R;" +
                                "那就是，大海是瑪歐斯的領域$R;" +
                                "我們不能進去的約定$R;" +
                                "$P就是因為這個約定，$R;" +
                                "這個世界就沒了進大海的習慣$R;" +
                                "所以船也不用$R;" +
                                "也不能去大海暢泳$R;" +
                                "穿泳裝的女人也沒有$R;" +
                                "$P所以我們決定破壞那個約定$R;" +
                                "一定可以的$R;" +
                                "$P但……$R;" +
                                "$P問題就是$R;" +
                                "有人比我們更早的破壞了那個約定$R;" +
                                "$R他們就是『地獄的海盜』$R;" +
                                "$P嗚嗚……那些海盜傢伙$R;" +
                                "$R絕對不能饒恕！絕對不能饒恕！$R;" +
                                "竟然比我們早破壞約定$R;" +
                                "太令人氣憤了！$R;" +
                                "$P所以我們$R;" +
                                "正在為了佔據大海，和海盜戰鬥$R;" +
                                "$P所以希望您們能夠幫我們打仗$R;" +
                                "$R具體內容呢$R;" +
                                "等到您們接受任務再說吧！$R;");
                            任務(pc);
                            break;
                    }
                    return;
                }
                Say(pc, 131, "來到這裡$R;" +
                    "是不是你也想進謎語團？$R;");
                switch (Select(pc, "想進謎語團嗎？", "", "不想進", "想進"))
                {
                    case 2:
                        if (pc.Level <= 63)
                        {
                            Say(pc, 131, "對不起，你還是回去吧$R;" +
                                "謎語團是以實力來甄選成員的$R;" +
                                "我們不需要弱者$R;");
                            return;
                        }

                        Say(pc, 131, "不好意思，你不可以入團$R;" +
                            "$R加入謎語團，要通過$R;" +
                            "嚴格的規則和考試，還有血統、$R;" +
                            "政治理由、人脈、錢……$R;" +
                            "$P反正，有很多條件，所以不行！！$R;" +
                            "$P但我想知道$R;" +
                            "你到底知不知道謎語團$R;" +
                            "是怎樣的團體啊？$R;");

                        switch (Select(pc, "你知道嗎?", "", "知道", "不知道"))
                        {
                            case 1:
                                ShowEffect(pc, 11000768, 4520);
                                //NPCMOTION_ONE 361 0 11000768
                                Wait(pc, 2000);
                                Say(pc, 131, "什麼？$R;" +
                                    "怎麼回事？難道是泄密了？$R;" +
                                    "不可能吧？$R;");
                                break;
                            case 2:
                                mask.SetValue(MZTYSFlags.詢問謎團1, true);
                                //_6a23 = true;
                                Say(pc, 131, "呵呵，$R;" +
                                    "好像對我們謎語團略知一二，$R;" +
                                    "那麼要不要來參加我們的活動？$R;");

                                switch (Select(pc, "參加嗎？", "", "參加", "不參加"))
                                {
                                    case 1:
                                        mask.SetValue(MZTYSFlags.詢問謎團2, true);
                                        //_6a24 = true;
                                        Say(pc, 131, "我們是熱愛自由的謎語團$R;" +
                                            "$R不可以受到束縛！$R;" +
                                            "規則是用來打破的!$R;" +
                                            "$R這就是謎語團存在的意義！$R;" +
                                            "$P或許您知道『和古代民族的約定』吧？$R;" +
                                            "$R那是什麼呢？$R;" +
                                            "那就是，大海是瑪歐斯的領域$R;" +
                                            "我們不能進去的約定$R;" +
                                            "$P就是因為這個約定，$R;" +
                                            "這個世界就沒了進大海的習慣$R;" +
                                            "所以船也不用$R;" +
                                            "也不能去大海暢泳$R;" +
                                            "穿泳裝的女人也沒有$R;" +
                                            "$P所以我們決定破壞那個約定$R;" +
                                            "一定可以的$R;" +
                                            "$P但……$R;" +
                                            "$P問題就是$R;" +
                                            "有人比我們更早的破壞了那個約定$R;" +
                                            "$R他們就是『地獄的海盜』$R;" +
                                            "$P嗚嗚……那些海盜傢伙$R;" +
                                            "$R絕對不能饒恕！絕對不能饒恕！$R;" +
                                            "竟然比我們早破壞約定$R;" +
                                            "太令人氣憤了！$R;" +
                                            "$P所以我們$R;" +
                                            "正在為了佔據大海，和海盜戰鬥$R;" +
                                            "$P所以希望您們能夠幫我們打仗$R;" +
                                            "$R具體內容呢$R;" +
                                            "等到您們接受任務再說吧！$R;");
                                        任務(pc);
                                        break;
                                }
                                break;
                        }
                        break;
                }
                return;
            }
            任務(pc);
        }

        void 任務(ActorPC pc)
        {
            switch (Select(pc, "有什麼事嗎?", "", "任務服務台", "什麼也不做"))
            {
                case 1:
                    HandleQuest(pc, 27);

                    if (pc.Quest != null)
                    {
                        if (pc.Quest.ID == 10031400 || pc.Quest.ID == 10031401)
                        {

                            Say(pc, 131, "這個要塞的東北方$R;" +
                                "有一個叫中立之島的島嶼$R;" +
                                "$R為了佔據那個島$R;" +
                                "希望你能夠鏟除那裡的妖怪$R;" +
                                "$P可是，海盜們$R;" +
                                "好像也在打那個島的主意呢$R;" +
                                "$R如果發現那些傢伙$R;" +
                                "一定要好好教訓他們啊！$R;");
                        }

                        if (pc.Quest.ID == 10031402 || pc.Quest.ID == 10031403)
                        {

                            Say(pc, 131, "這個要塞的西北方$R;" +
                                "有個叫海盜島的島嶼$R;" +
                                "是我們死對頭的島$R;" +
                                "$R到那些傢伙們的地盤$R;" +
                                "教訓他們一頓再回來吧!!$R;");

                        }

                        if (pc.Quest.ID == 10031404)
                        {

                            Say(pc, 131, "那些傢伙偶爾會掉一些『血板』$R;" +
                                "$R那就是海盜的盟誓證明書$R;" +
                                "就像我們謎語團的面具一樣$R;" +
                                "$P很明顯，這對那些傢伙很重要的$R;" +
                                "去把那個搶回來吧!$R;" +
                                "$R拜託了$R;");
                        }

                        Say(pc, 131, "那麼就拜託了$R;" +
                            "使用裡面的秘密道路吧!$R;");
                    }
                    break;
            }
        }
    }
}