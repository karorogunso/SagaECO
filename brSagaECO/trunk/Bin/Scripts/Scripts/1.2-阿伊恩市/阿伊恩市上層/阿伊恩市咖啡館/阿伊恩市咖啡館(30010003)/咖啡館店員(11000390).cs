using System;
using System.Collections.Generic;
using System.Text;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaLib;

using SagaScript.Chinese.Enums;
namespace SagaScript.M30010003
{
    public class S11000390 : Event
    {
        public S11000390()
        {
            this.EventID = 11000390;

        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<AYEFlags> mask = new BitMask<AYEFlags>(pc.CMask["AYE"]);
            if (pc.Level > 44) //ME.JOB > 90
                               //ME.JOB < 101
            {
                if (pc.Job == PC_JOB.FARMASIST
                 || pc.Job == PC_JOB.ALCHEMIST
                 || pc.Job == PC_JOB.MARIONEST)
                {
                    if (mask.Test(AYEFlags.收集任務結束))//_2a76)
                    {
                        Say(pc, 190, "咯吱咯吱…$R;" +
                            "口感真是……$R;" +
                            "咯吱咯吱…$R;" +
                            "好鬆軟…嗯…好吃……$R;" +
                            "咯吱咯吱…$R;");
                        Say(pc, 131, "光是看著就已經飽了似的。$R;");
                        return;
                    }
                    if (mask.Test(AYEFlags.收集結束))//_2a77)
                    {
                        if (pc.Gender == PC_GENDER.FEMALE)
                        {
                            if (CheckInventory(pc, 50023500, 1))
                            {
                                mask.SetValue(AYEFlags.收集任務開始, true);
                                //_2a76 = true;
                                GiveItem(pc, 50023500, 1);
                                Say(pc, 131, "這是我的心意，$R;" +
                                    "是我自己做的稻草帽呢。$R;" +
                                    "$R是媽媽教我做的$R;" +
                                    "希望您會喜歡唷$R;");
                                return;
                            }
                            Say(pc, 131, "要給您謝禮，$R;" +
                                "去整理一下行李吧$R;");
                            return;
                        }
                        if (CheckInventory(pc, 50062750, 1))
                        {
                            mask.SetValue(AYEFlags.收集任務開始, true);
                            //2a76 = true;
                            GiveItem(pc, 50062750, 1);
                            Say(pc, 131, "這是我的心意，$R;" +
                                "希望您會喜歡唷$R;");
                            return;
                        }
                        Say(pc, 131, "要給您謝禮，$R;" +
                            "先去整理一下行李吧$R;");
                        return;
                    }
                    if (mask.Test(AYEFlags.收集任務開始))//_2a75)
                    {
                        //#フルーツジュース
                        if (CountItem(pc, 10001600) >= 1 //#ベーコン
                         && CountItem(pc, 10004500) >= 1 //#ゆで卵
                         && CountItem(pc, 10007750) >= 1 //#きのこスープ
                         && CountItem(pc, 10002208) >= 1 //#えんどう豆のポタージュ
                         && CountItem(pc, 10002205) >= 1 //#きのこパン
                         && CountItem(pc, 10006150) >= 1 //#上等なパンの実
                         && CountItem(pc, 10006200) >= 1 //#ジャム
                         && CountItem(pc, 10033900) >= 1 //#高級なジャム
                         && CountItem(pc, 10033902) >= 1 //#高級フルーツジュース
                         && CountItem(pc, 10001700) >= 1 //#おいしい串焼き肉
                         && CountItem(pc, 10006550) >= 1 //#スペシャルシチュー
                         && CountItem(pc, 10008850) >= 1 //#おいしいカレー
                         && CountItem(pc, 10008950) >= 1 //#にんじんスティック
                         && CountItem(pc, 10034400) >= 1 //#じゃがサラダ
                         && CountItem(pc, 10004050) >= 1 //#レタス
                         && CountItem(pc, 10006800) >= 1 //#チーズ
                         && CountItem(pc, 10008451) >= 1 //#まつたけさま
                         && CountItem(pc, 10007100) >= 1 //#チョコチップビスケット
                         && CountItem(pc, 10009450) >= 1 //#カルシウムビスケット
                         && CountItem(pc, 10037450) >= 1 //#砂糖入りコーヒー
                         && CountItem(pc, 10002350) >= 1 //#マシュマロ
                         && CountItem(pc, 10009300) >= 1)
                        {
                            mask.SetValue(AYEFlags.收集結束, true);
                            //_2a77 = true;
                            TakeItem(pc, 10001600, 1);
                            TakeItem(pc, 10004500, 1);
                            TakeItem(pc, 10007750, 1);
                            TakeItem(pc, 10002208, 1);
                            TakeItem(pc, 10002205, 1);
                            TakeItem(pc, 10006150, 1);
                            TakeItem(pc, 10006200, 1);
                            TakeItem(pc, 10033900, 1);
                            TakeItem(pc, 10033902, 1);
                            TakeItem(pc, 10001700, 1);
                            TakeItem(pc, 10006550, 1);
                            TakeItem(pc, 10008850, 1);
                            TakeItem(pc, 10008950, 1);
                            TakeItem(pc, 10034400, 1);
                            TakeItem(pc, 10004050, 1);
                            TakeItem(pc, 10006800, 1);
                            TakeItem(pc, 10008451, 1);
                            TakeItem(pc, 10007100, 1);
                            TakeItem(pc, 10009450, 1);
                            TakeItem(pc, 10037450, 1);
                            TakeItem(pc, 10002350, 1);
                            TakeItem(pc, 10009300, 1);
                            TakeItem(pc, 10020101, 1);
                            Say(pc, 131, "啊，聞到香味了。$R;" +
                                "食物都準備好了$R;" +
                                "謝謝！$R;");
                            Say(pc, 131, "送上食物$R;");
                            if (pc.Gender == PC_GENDER.FEMALE)
                            {
                                if (CheckInventory(pc, 50023500, 1))
                                {
                                    mask.SetValue(AYEFlags.收集任務開始, true);
                                    //_2a76 = true;
                                    GiveItem(pc, 50023500, 1);
                                    Say(pc, 131, "這是我的心意，$R;" +
                                        "是我自己做的稻草帽呢。$R;" +
                                        "$R是媽媽教我做的$R;" +
                                        "希望您會喜歡唷$R;");
                                    return;
                                }
                                Say(pc, 131, "要給您謝禮，$R;" +
                                    "去整理一下行李吧$R;");
                                return;
                            }
                            if (CheckInventory(pc, 50062750, 1))
                            {
                                mask.SetValue(AYEFlags.收集任務開始, true);
                                //_2a76 = true;
                                GiveItem(pc, 50062750, 1);
                                Say(pc, 131, "這是我的心意，$R;" +
                                    "希望您會喜歡唷$R;");
                                return;
                            }
                            Say(pc, 131, "要給您謝禮，$R;" +
                                "先去整理一下行李吧$R;");
                            return;
                        }
                        Say(pc, 190, "喂，$R;" +
                            "點的食物還要很久才好嗎？$R;");
                        Say(pc, 131, "是是..請稍候片刻！$R;" +
                            "$R…求求您$R;" +
                            "麻煩您快點拿過來吧$R;");
                        return;
                    }
                    Say(pc, 190, "喂，$R;" +
                        "點的食物還要很久才好嗎？$R;");
                    Say(pc, 131, "吃那麼多的人，才第一次見呢$R;" +
                        "請幫幫忙$R;" +
                        "拜託您幫我把菜單上的食物拿過來，$R;" +
                        "$R我們的倉庫已經空了。$R;");
                    switch (Select(pc, "怎麼辦呢？", "", "不要", "好的"))
                    {
                        case 1:
                            break;
                        case 2:
                            if (CheckInventory(pc, 10020101, 1))
                            {
                                mask.SetValue(AYEFlags.收集任務開始, true);
                                //_2a75 = true;
                                GiveItem(pc, 10020101, 1);
                                Say(pc, 131, "謝謝$R;" +
                                    "$R這裡是那人點的菜$R;" +
                                    "那…拜託了！$R;");
                                return;
                            }
                            Say(pc, 131, "謝謝$R;" +
                                "$R那麼，給您菜單$R;" +
                                "把行李整理一下吧$R;");
                            break;
                    }
                    return;
                }
            }
            Say(pc, 190, "咯吱咯吱…$R;" +
                "口感真是……$R;" +
                "咯吱咯吱…$R;" +
                "好鬆軟…嗯…好吃……$R;" +
                "咯吱咯吱…$R;");
            Say(pc, 131, "光是看著就已經飽了似的。$R;");
        }
    }
}