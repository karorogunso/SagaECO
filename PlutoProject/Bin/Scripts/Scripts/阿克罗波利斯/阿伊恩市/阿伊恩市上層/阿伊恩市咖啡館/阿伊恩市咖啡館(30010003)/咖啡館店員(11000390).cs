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
                            "好松软…嗯…好吃……$R;" +
                            "咯吱咯吱…$R;");
                        Say(pc, 131, "光是看着就已经饱了似的。$R;");
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
                                Say(pc, 131, "这是我的心意，$R;" +
                                    "是我自己做的稻草帽呢。$R;" +
                                    "$R是妈妈教我做的$R;" +
                                    "希望您会喜欢$R;");
                                return;
                            }
                            Say(pc, 131, "要给您谢礼，$R;" +
                                "去整理一下行李吧$R;");
                            return;
                        }
                        if (CheckInventory(pc, 50062750, 1))
                        {
                            mask.SetValue(AYEFlags.收集任務開始, true);
                            //2a76 = true;
                            GiveItem(pc, 50062750, 1);
                            Say(pc, 131, "这是我的心意，$R;" +
                                "希望您会喜欢$R;");
                            return;
                        }
                            Say(pc, 131, "要给您谢礼，$R;" +
                                "去整理一下行李吧$R;");
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
                            Say(pc, 131, "啊，闻到香味了。$R;" +
                                "食物都准备好了$R;" +
                                "谢谢！$R;");
                            Say(pc, 131, "送上食物$R;");
                            if (pc.Gender == PC_GENDER.FEMALE)
                            {
                                if (CheckInventory(pc, 50023500, 1))
                                {
                                    mask.SetValue(AYEFlags.收集任務開始, true);
                                    //_2a76 = true;
                                    GiveItem(pc, 50023500, 1);
                                    Say(pc, 131, "这是我的心意，$R;" +
                                        "是我自己做的稻草帽呢。$R;" +
                                        "$R是妈妈教我做的$R;" +
                                        "希望您会喜欢$R;");
                                    return;
                                }
                                Say(pc, 131, "要给您谢礼，$R;" +
                                    "去整理一下行李吧$R;");
                                return;
                            }
                            if (CheckInventory(pc, 50062750, 1))
                            {
                                mask.SetValue(AYEFlags.收集任務開始, true);
                                //_2a76 = true;
                                GiveItem(pc, 50062750, 1);
                                Say(pc, 131, "这是我的心意，$R;" +
                                    "希望您会喜欢$R;");
                                return;
                            }
                            Say(pc, 131, "要给您谢礼，$R;" +
                                "先去整理一下行李吧$R;");
                            return;
                        }
                        Say(pc, 190, "喂，$R;" +
                            "点的食物还要很久才好吗？$R;");
                        Say(pc, 131, "是是..请稍候片刻！$R;" +
                            "$R…求求您$R;" +
                            "麻烦您快点拿过来吧$R;");
                        return;
                    }
                    Say(pc, 190, "喂，$R;" +
                        "点的食物还要很久才好吗？$R;");
                    Say(pc, 131, "吃那么多的人，才第一次见呢$R;" +
                        "请帮帮忙$R;" +
                        "拜托您帮我把菜单上的食物拿过来，$R;" +
                        "$R我们的仓库已经空了。$R;");
                    switch (Select(pc, "怎么办呢？", "", "不要", "好的"))
                    {
                        case 1:
                            break;
                        case 2:
                            if (CheckInventory(pc, 10020101, 1))
                            {
                                mask.SetValue(AYEFlags.收集任務開始, true);
                                //_2a75 = true;
                                GiveItem(pc, 10020101, 1);
                                Say(pc, 131, "谢谢$R;" +
                                    "$R这里是那人点的菜$R;" +
                                    "那…拜托了！$R;");
                                return;
                            }
                            Say(pc, 131, "谢谢$R;" +
                                "$R那么，给您菜单$R;" +
                                "把行李整理一下吧$R;");
                            break;
                    }
                    return;
                }
            }
            Say(pc, 190, "咯吱咯吱…$R;" +
                "口感真是……$R;" +
                "咯吱咯吱…$R;" +
                "好松软…嗯…好吃……$R;" +
                "咯吱咯吱…$R;");
            Say(pc, 131, "光是看著就已经饱了似的。$R;");
        }
    }
}