using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
//所在地圖:奧克魯尼亞東海岸(10034000) NPC基本信息:光之精靈(11000113) X:48 Y:95
namespace SagaScript.M10034000
{
    public class S11000113 : Event
    {
        public S11000113()
        {
            this.EventID = 11000113;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<DHAFlags> mask = new BitMask<DHAFlags>(pc.CMask["DHA"]);
            BitMask<Neko_01> Neko_01_cmask = pc.CMask["Neko_01"];
            BitMask<Neko_01> Neko_01_amask = pc.AMask["Neko_01"];
            BitMask<Crystal> Crystal_mask = pc.CMask["Crystal"];

            int MJ = 0;

            if (Crystal_mask.Test(Crystal.開始收集) &&
                !Crystal_mask.Test(Crystal.光之精靈) &&
                CountItem(pc, 10014300) >= 1)
            {
                if (!Crystal_mask.Test(Crystal.暗之精靈) && 
                    !Crystal_mask.Test(Crystal.炎之精靈) &&
                    !Crystal_mask.Test(Crystal.水之精靈) &&
                    !Crystal_mask.Test(Crystal.土之精靈) &&
                    !Crystal_mask.Test(Crystal.風之精靈) &&
                    CountItem(pc, 10014300) >= 1)
                {
                    Crystal_mask.SetValue(Crystal.風之精靈, true);
                    Say(pc, 131, "您好$R;" +
                        "$R我叫米瑪斯！是光明精靈$R;" +
                        "$R什麼事呢？$R;" +
                        "$P…$R;" +
                        "$R想在『水晶』上注入力量？$R;" +
                        "$R對我來說是很簡單的事情呀$R;" +
                        "$P那麼開始了$R;");
                    PlaySound(pc, 3120, false, 100, 50);
                    ShowEffect(pc, 4037);
                    Say(pc, 131, "『水晶』裡注入力量了$R;" +
                        "…?$R;" +
                        "不能再增加力量了$R;");
                    return;
                }
                Say(pc, 131, "您好$R;" +
                    "$R我叫米瑪斯！是光明精靈$R;" +
                    "$R什麼事呢？$R;" +
                    "$P…$R;" +
                    "$R想在『水晶』上注入力量？$R;" +
                    "哎呀！$R;" +
                    "好像已經有別的精靈力量了$R;" +
                    "$P現在不能注入我的力量了$R;");
                return;
            }

            if (!Neko_01_amask.Test(Neko_01.桃子任務完成) &&
                Neko_01_cmask.Test(Neko_01.光之精靈對話) &&
                !Neko_01_cmask.Test(Neko_01.再次與祭祀對話))
            {
                Say(pc, 131, "不能幫到忙，很抱歉呀$R;");
                return;
            }

            if (!Neko_01_amask.Test(Neko_01.桃子任務完成) &&
                Neko_01_cmask.Test(Neko_01.與祭祀對話) &&
                !Neko_01_cmask.Test(Neko_01.光之精靈對話))
            {
                Say(pc, 131, "您好，我叫米瑪斯！是光明精靈$R;" +
                    "$P哎呀！您身上附著凱堤呀？$R;");
                if (CountItem(pc, 10011210) >= 1)
                {
                    Say(pc, 131, "…？$R想把凱堤趕走嗎？$R;" +
                        "$P在埃米爾和塔妮亞世界裡，$R召喚結束了的生命$R或過分的追逐靈魂是被禁止的。$R;");
                    Say(pc, 131, "只要我能辦到的，我會盡力的$R;" +
                        "$P但是，我會收取相應的報酬的喔$R;" +
                        "$R給我一個您的$R『光明召喚石』可以吧？$R;");
                    switch (Select(pc, "怎麼辦呢?", "", "幫我趕走嗎", "放棄"))
                    {
                        case 1:
                            Neko_01_cmask.SetValue(Neko_01.光之精靈對話, true);
                            TakeItem(pc, 10011210, 1);
                            Say(pc, 131, "知道了$R;" +
                                "$R把凱堤趕走，淨化您的身體吧$R;" +
                                "$P閉上眼睛，平靜心情。$R;" +
                                "$P…$R;");
                            Say(pc, 0, 131, "喵~~嗷$R;", " ");
                            Say(pc, 131, "…?$R;" +
                                "$P哎呀，不能趕走他…$R不知為什麼呀？$R;" +
                                "$P這是怎麼回事呢？$R;");
                            Say(pc, 0, 131, "喵$R;", " ");
                            Say(pc, 131, "唉！怎麼會失敗呢！！$R我的力量為什麼趕不走呢！$R;" +
                                "$R不敢相信…！！$R;" +
                                "$P…對不起…$R失敗了$R;" +
                                "$R凱堤好像很喜歡您$R;" +
                                "$P以我的力量是不能硬把它趕走的$R;");
                            break;
                        case 2:
                            Say(pc, 131, "知道了。不會勉強您的$R;");
                            break;
                    }
                    return;
                }
                Say(pc, 131, "我知道您需要什麼？但是您…$R;" +
                    "$P什麼也沒有帶呀$R;");
                return;
            }

            if (!mask.Test(DHAFlags.光之精靈第一次對話))
            {
                mask.SetValue(DHAFlags.光之精靈第一次對話, true);
                Say(pc, 131, "您好$R;" +
                    "$R我叫米瑪斯！是光明精靈$R;" +
                    "$R什麼事呢？$R;");
            }
            else
            {
                Say(pc, 131, "您好$R;" +
                    "什麼事呢？$R;");
            }
            if (CountItem(pc, 10011210) >= 1 && CountItem(pc, 10026400) >= 1)
            {
                Say(pc, 131, "『光明召喚石』呀$R;" +
                    "$R用召喚石在$R;" +
                    "『木箭』上注入光之力是嗎？$R;");
                switch (Select(pc, "怎麼辦呢?", "", "注入光之力", "放棄"))
                {
                    case 1:
                        PlaySound(pc, 3088, false, 100, 50);
                        Wait(pc, 2000);
                        Say(pc, 131, "『木箭』變成了『光明之箭』$R;");
                        TakeItem(pc, 10011210, 1);
                        while (CountItem(pc, 10026400) >= 1)
                        {
                            MJ++;
                        }
                        TakeItem(pc, 10026400, (ushort)MJ);
                        GiveItem(pc, 10026510, (ushort)MJ);
                        Say(pc, 131, "謝謝您給我『光明召喚石』$R;" +
                            "請再來玩呀！$R;");
                        break;
                    case 2:
                        Say(pc, 131, "啊！我誤會您了$R;");
                        break;
                }
                return;
            }
            Say(pc, 131, "…?$R;" +
                "什麼也沒帶$R;");
        }
    }
}