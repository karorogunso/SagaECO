using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10057000
{
    public class S11001186 : Event
    {
        public S11001186()
        {
            this.EventID = 11001186;
        }

        public override void OnEvent(ActorPC pc)
        {

            BitMask<Sinker> mask = pc.AMask["Sinker"];
            if (mask.Test(Sinker.寶石商人給予詩迪的項鏈墜))//_2b02)
            {
                Say(pc, 159, "您好！$R;" +
                    "用昨天看到的合金，試做了項鏈墜！$R;" +
                    "不介意的話，分給大家吧！$R;");
                OpenShopBuy(pc, 198);
                return;
            }
            if (mask.Test(Sinker.獲得別針) && mask.Test(Sinker.看過告示牌))//_2b00 && _7a91)
            {
                Say(pc, 159, "您好！$R;" +
                    "今天的天氣真好呢！$R;");
                return;
            }
            if (mask.Test(Sinker.未收到別針))//_7a99)
            {
                if (CheckInventory(pc, 10038101, 1))
                {
                    mask.SetValue(Sinker.未收到別針, false);
                    mask.SetValue(Sinker.獲得別針, true);
                    //_7a99 = false;
                    //_2b00 = true;
                    GiveItem(pc, 10038101, 1);
                    PlaySound(pc, 2040, false, 100, 50);
                    Say(pc, 131, "收到『別針』！$R;");
                    Say(pc, 131, "拿到寶石商人那裡$R;" +
                        "説不定可以做成什麽呢$R;" +
                        "$P反正這次真的謝謝您$R;" +
                        "如果有機會的話，再委託您$R;");
                    mask.SetValue(Sinker.收到合成藥, false);
                    mask.SetValue(Sinker.拒絕幫忙, false);
                    mask.SetValue(Sinker.收到不明的合金, false);
                    //_7a93 = false;
                    //_7a94 = false;
                    //_7a96 = false;
                    return;
                }
                Say(pc, 131, "行李太多了，無法給您$R;");
                mask.SetValue(Sinker.未收到別針, true);
                //_7a99 = true;
                return;
            }
            if (CountItem(pc, 10020762) >= 1)
            {
                Say(pc, 131, "轉交了！$R;" +
                    "謝謝！$R;");
                PlaySound(pc, 2040, false, 100, 50);
                Say(pc, 131, "給他『合成測試報告』$R;");
                TakeItem(pc, 10020762, 1);
                Say(pc, 131, "您拿著的那個？$R;" +
                    "是這次合成試驗時使用的合金啊！$R;" +
                    "$R真的是很漂亮的合金$R;" +
                    "報告書上寫著『初合金』$R;" +
                    "$P加工使用起來有點小$R;" +
                    "但加工成飾品應該可以$R;" +
                    "$R啊!這裡剛好有『別針』$R;" +
                    "給您這個吧$R;");
                if (CheckInventory(pc, 10038101, 1))
                {
                    mask.SetValue(Sinker.未收到別針, false);
                    mask.SetValue(Sinker.獲得別針, true);
                    //_7a99 = false;
                    //_2b00 = true;
                    GiveItem(pc, 10038101, 1);
                    PlaySound(pc, 2040, false, 100, 50);
                    Say(pc, 131, "收到『別針』！$R;");
                    Say(pc, 131, "拿到寶石商人那裡$R;" +
                        "説不定可以做成什麽呢$R;" +
                        "$P反正這次真的謝謝您$R;" +
                        "如果有機會的話，再委託您$R;");
                    mask.SetValue(Sinker.收到合成藥, false);
                    mask.SetValue(Sinker.拒絕幫忙, false);
                    mask.SetValue(Sinker.收到不明的合金, false);
                    //_7a93 = false;
                    //_7a94 = false;
                    //_7a96 = false;
                    return;
                }
                Say(pc, 131, "行李太多了，無法給您$R;");
                mask.SetValue(Sinker.未收到別針, true);
                //_7a99 = true;
                return;
            }
            if (mask.Test(Sinker.收到合成藥) && mask.Test(Sinker.看過告示牌))//_7a93 && _7a91)
            {
                Say(pc, 131, "把『合成藥』交給鐵廠老闆之前$R;" +
                    "都要小心翼翼的放好$R;");
                return;
            }
            if (mask.Test(Sinker.未收到合成藥))//_7a92)
            {
                if (CheckInventory(pc, 10000510, 1))
                {
                    mask.SetValue(Sinker.未收到合成藥, false);
                    mask.SetValue(Sinker.收到合成藥, true);
                    //_7a92 = false;
                    //_7a93 = true;
                    GiveItem(pc, 10000510, 1);
                    PlaySound(pc, 2040, false, 100, 50);
                    Say(pc, 131, "收到『合成藥』!$R;");
                    Say(pc, 131, "剛才也説過了，一定要小心翼翼阿$R;" +
                        "對衝撃很敏感，所以盡量不要$R;" +
                        "使用施工的鑰匙，拜託了！$R;" +
                        "$P考慮到準備回家鄉的路上$R;" +
                        "會有無法預料的事情發生$R;" +
                        "把歸還的地點改為帕斯特市吧$R;" +
                        "$R那麽就...拜託了!$R;");

                    byte x, y;
                    x = (byte)Global.Random.Next(7, 17);
                    y = (byte)Global.Random.Next(120, 126);

                    SetHomePoint(pc, 10057000, x, y);
                    //PARAM ME.SAVEID = 422
                    Say(pc, 131, "儲存點，變更為『帕斯特市』！$R;");
                    return;
                }
                Say(pc, 131, "行李太多了，無法給您$R;");
                mask.SetValue(Sinker.未收到合成藥, true);
                //_7a92 = true;
                return;
            }
            if (pc.Job == PC_JOB.WIZARD
                || pc.Job == PC_JOB.SORCERER
                || pc.Job == PC_JOB.SAGE
                || pc.Job == PC_JOB.SHAMAN
                || pc.Job == PC_JOB.ELEMENTER
                || pc.Job == PC_JOB.ENCHANTER
                || pc.Job == PC_JOB.VATES
                || pc.Job == PC_JOB.DRUID
                || pc.Job == PC_JOB.BARD
                || pc.Job == PC_JOB.WARLOCK
                || pc.Job == PC_JOB.GAMBLER
                || pc.Job == PC_JOB.NECROMANCER)
            {
                if (pc.Level > 34 && !mask.Test(Sinker.看過告示牌))//_7a91)
                {
                    Say(pc, 159, "您好！是看到阿高普路斯市的$R;" +
                        "委託告示板過來的嗎？$R;");
                    return;
                }
            }
            if (mask.Test(Sinker.看過告示牌))//_7a91)
            {
                Say(pc, 159, "看了委託告示板過來的吧！$R;" +
                    "遠道而來!辛苦了$R;" +
                    "$R這次是向生產系拜託事情的$R;");
                Say(pc, 131, "工作的內容歸內容$R;" +
                    "要是戰士系或魔法系的勇者$R;" +
                    "可以幫忙的話…當然最好不過$R;" +
                    "$R截頭去尾，簡單説就是幫我搬東西$R;" +
                    "$P把昨天剛完成的合成藥$R;" +
                    "轉交給阿伊恩薩烏斯的鐵廠老闆$R;" +
                    "$R是從食物中抽取的草本精華$R;" +
                    "調和而成的$R;" +
                    "因此要很小心搬運的東西$R;" +
                    "$P作為生產系的您$R;" +
                    "相信您會安全的幫我搬運$R;" +
                    "$R可以拜託您嗎?$R;");
                switch (Select(pc, "怎麼辦?", "", "知道了", "困難呀"))
                {
                    case 1:
                        Say(pc, 131, "謝謝！$R;" +
                            "那麽就拜託了$R;");
                        if (CheckInventory(pc, 10000510, 1))
                        {
                            mask.SetValue(Sinker.未收到合成藥, false);
                            mask.SetValue(Sinker.收到合成藥, true);
                            //_7a92 = false;
                            //_7a93 = true;
                            GiveItem(pc, 10000510, 1);
                            PlaySound(pc, 2040, false, 100, 50);
                            Say(pc, 131, "收到『合成藥』!$R;");
                            Say(pc, 131, "剛才也説過了，一定要小心翼翼阿$R;" +
                                "對衝撃很敏感，所以盡量不要$R;" +
                                "使用施工的鑰匙，拜託了！$R;" +
                                "$P考慮到準備回家鄉的路上$R;" +
                                "會有無法預料的事情發生$R;" +
                                "把歸還的地點改為帕斯特市吧$R;" +
                                "$R那麽就...拜託了!$R;");

                            byte x, y;
                            x = (byte)Global.Random.Next(7, 17);
                            y = (byte)Global.Random.Next(120, 126);

                            SetHomePoint(pc, 10057000, x, y);
                            //PARAM ME.SAVEID = 422
                            Say(pc, 131, "儲存點，變更為『帕斯特市』！$R;");
                            return;
                        }
                        Say(pc, 131, "行李太多了，無法給您$R;");
                        mask.SetValue(Sinker.未收到合成藥, true);
                        //_7a92 = true;
                        break;
                }
                return;
            }
            
            Say(pc, 159, "您好！$R;" +
                "今天的天氣真好呢！$R;");
        }
    }
}