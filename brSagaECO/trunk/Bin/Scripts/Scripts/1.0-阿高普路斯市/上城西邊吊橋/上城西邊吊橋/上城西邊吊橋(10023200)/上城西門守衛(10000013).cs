using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
//所在地圖:上城西邊吊橋(10023200) NPC基本信息:上城西門守衛(10000013) X:33 Y:129
namespace SagaScript.M10023200
{
    public class S10000013 : Event
    {
        public S10000013()
        {
            this.EventID = 10000013;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<Knights> Knights_mask = pc.CMask["Knights"];

            if (CountItem(pc, 10042801) >= 1)
            {
                if (pc.Gold < 100)
                {
                    if (pc.PossesionedActors.Count != 0)
                    {
                        PlaySound(pc, 2225, false, 100, 50);
                        Say(pc, 131, "咇!!!!$R;" +
                            "禁止…禁止$R;" +
                            "$R憑依狀態不能通過呀$R;" +
                            "$P嗯……$R;" +
                            "這裡是禁止出入地帶$R;" +
                            "為了不讓憑依通過，$R;" +
                            "設置了檢查憑依的特殊關卡呀$R;" +
                            "$P憑依通過$R指的是一般不能通過的地方，$R;" +
                            "可以在憑依狀態下通過$R;" +
                            "$這是很方便的技術啊，$R;" +
                            "不過……$R;" +
                            "$P唉……$R;" +
                            "知道了就回去吧$R;");
                        return;
                    }
                    Warp(pc, 10023000, 37, 128);
                    return;
                }
                switch (Select(pc, "歡迎來到阿高普路斯唷!", "", "進入上城", "辛苦了", "不進去"))
                {
                    case 1:
                        if (pc.PossesionedActors.Count != 0)
                        {
                            PlaySound(pc, 2225, false, 100, 50);
                            Say(pc, 131, "咇!!!!$R;" +
                                "禁止…禁止$R;" +
                                "$R憑依狀態不能通過呀$R;" +
                                "$P嗯……$R;" +
                                "這裡是禁止出入地帶$R;" +
                                "為了不讓憑依通過，$R;" +
                                "設置了檢查憑依的特殊關卡呀$R;" +
                                "$P憑依通過$R指的是一般不能通過的地方，$R;" +
                                "可以在憑依狀態下通過$R;" +
                                "$這是很方便的技術啊，$R;" +
                                "不過……$R;" +
                                "$P唉……$R;" +
                                "知道了就回去吧$R;");
                            return;
                        }
                        Warp(pc, 10023000, 37, 128);
                        break;
                    case 2:
                        switch (Select(pc, "什麼都知道…", "", "不讓人發現，拿出了100金幣", "不給"))
                        {
                            case 1:
                                pc.Gold -= 100;
                                switch (Select(pc, "走哪條通道呢？", "", "通過東門的後方走道", "通過西門的後方走道", "通過南門的後方走道", "通過北門的後方走道"))
                                {
                                    case 1:
                                        Warp(pc, 10023100, 224, 127);
                                        break;
                                    case 2:
                                        Warp(pc, 10023200, 31, 127);
                                        break;
                                    case 3:
                                        Warp(pc, 10023300, 128, 225);
                                        break;
                                    case 4:
                                        Warp(pc, 10023400, 127, 31);
                                        break;
                                }
                                break;
                            case 2:
                                break;
                        }
                        break;
                    case 3:
                        break;
                }
                return;
            }
            if (Knights_mask.Test(Knights.告知加入騎士團的方法) &&
                !Knights_mask.Test(Knights.取得上城通行證))
            {
                Say(pc, 131, "進入上城的方法是秘密喔，$R;" +
                    "不要告訴別人啊！$R;");
                return;
            }
            if (Knights_mask.Test(Knights.考慮加入騎士團) &&
                !Knights_mask.Test(Knights.告知加入騎士團的方法) && 
                !Knights_mask.Test(Knights.取得上城通行證))
            {
                Say(pc, 131, "真的想加入混城騎士團嗎？$R;" +
                    "$R那當然要加入我們最強的西軍了！$R;" +
                    "$P嗯…$R;" +
                    "那麼告訴您得到許可証的方法吧$R;" +
                    "從這個階段下去$R;" +
                    "就到了阿高普路斯下城$R;" +
                    "知道了嗎？$R;" +
                    "下城中央稍微向南走$R;" +
                    "有一位上了年紀的夫人$R;" +
                    "$P她就是被譽為$R;" +
                    "下城萬物博士的長老唷，$R;" +
                    "找她吧！她會告訴您一些事情的，$R;" +
                    "對您會有幫助的，記住不要失禮呀。$R;");
                Knights_mask.SetValue(Knights.告知加入騎士團的方法, true);
                return;
            }
            if (Knights_mask.Test(Knights.告知無法加入西軍) &&
                !Knights_mask.Test(Knights.告知團長不理你的原因))
            {
                Say(pc, 131, "長官不見您？那當然了$R;" +
                    "$R如果您很有名，也許會見您，$R可能怕您是某個國家的間諜吧$R;" +
                    "$P您想在這裡提高知名度，$R就得先宣傳您的名字阿，$R先從簡單的開始吧$R;" +
                    "$P幫助人，或受委託處理一些任務，$R就可以提高知名度唷$R;" +
                    "$R等您有名氣了，那怕只是一點，$R長官當然會接見您了$R;" +
                    "$P因為騎士團一直缺少人手$R;");
                Knights_mask.SetValue(Knights.告知團長不理你的原因, true);
                return;
            }
            if (!Knights_mask.Test(Knights.取得上城通行證) && 
                Knights_mask.Test(Knights.告知沒有通行證))
            {
                Say(pc, 131, "又是您呀，真煩人$R;" +
                    "這裡只有持有阿高普路斯市上城$R;" +
                    "市民證的人才能進入阿$R;" +
                    "$R等您得到許可証後，再來吧$R;");
                return;
            }
            if (!Knights_mask.Test(Knights.取得上城通行證))
            {
                //WINDOWOPEN 8
                Say(pc, 131, "歡迎來到世界最大的貿易城市$R;" +
                    "阿高普路斯$R;" +
                    "$P這條街的構造有點特別呀，$R;" +
                    "$R第一次來的人容易迷路唷，$R;" +
                    "給您做個簡單說明吧$R;");
                switch (Select(pc, "聽說明嗎？", "", "放棄", "我想聽"))
                {
                    case 1:
                        Say(pc, 131, "對不起，沒有上城許可證$R;" +
                            "不能進入喔，請回去吧$R;");
                        Knights_mask.SetValue(Knights.告知沒有通行證, true);
                        break;
                    case 2:
                        Say(pc, 131, "阿高普路斯座落在巨大的湖上$R;" +
                            "$R東西南北4邊，都有巨大的吊橋$R;" +
                            "您現在站著的地方就是東門喔$R;" +
                            "南、西、北邊也有$R跟這裡差不多的地方$R;" +
                            "看地圖就可以知道，$R這座城市，東西南北互相對稱$R;" +
                            "$P還有分為地上、地下兩層喔$R;" +
                            "$R我守著的這扇門裡$R;" +
                            "寬寬的街道叫上城$R;" +
                            "其地下就是下城唷$R;" +
                            "$P下城怎麼走是嗎？$R;" +
                            "從我的右側或左側的轉角繞過去$R;" +
                            "就會看見一條階梯$R;" +
                            "順著階梯下去就行了$R;" +
                            "$P反正沒有許可證，上城是進不去的$R;" +
                            "就是說您也進不去的$R;" +
                            "$R走吧，回去吧$R;");
                        Knights_mask.SetValue(Knights.告知沒有通行證, true);
                        break;
                }
                return;
            }
            if (pc.Gold < 100)
            {
                if (pc.PossesionedActors.Count != 0)
                {
                    PlaySound(pc, 2225, false, 100, 50);
                    Say(pc, 131, "咇!!!!$R;" +
                        "禁止…禁止$R;" +
                        "$R憑依狀態不能通過呀$R;" +
                        "$P嗯……$R;" +
                        "這裡是禁止出入地帶$R;" +
                        "為了不讓憑依通過，$R;" +
                        "設置了檢查憑依的特殊關卡呀$R;" +
                        "$P憑依通過$R指的是一般不能通過的地方，$R;" +
                        "可以在憑依狀態下通過$R;" +
                        "$這是很方便的技術啊，$R;" +
                        "不過……$R;" +
                        "$P唉……$R;" +
                        "知道了就回去吧$R;");
                    return;
                }
                Warp(pc, 10023000, 37, 128);
                return;
            }
            switch (Select(pc, "歡迎來到阿高普路斯唷!", "", "進入上城", "辛苦了", "不進去"))
            {
                case 1:
                    if (pc.PossesionedActors.Count != 0)
                    {
                        PlaySound(pc, 2225, false, 100, 50);
                        Say(pc, 131, "咇!!!!$R;" +
                            "禁止…禁止$R;" +
                            "$R憑依狀態不能通過呀$R;" +
                            "$P嗯……$R;" +
                            "這裡是禁止出入地帶$R;" +
                            "為了不讓憑依通過，$R;" +
                            "設置了檢查憑依的特殊關卡呀$R;" +
                            "$P憑依通過$R指的是一般不能通過的地方，$R;" +
                            "可以在憑依狀態下通過$R;" +
                            "$這是很方便的技術啊，$R;" +
                            "不過……$R;" +
                            "$P唉……$R;" +
                            "知道了就回去吧$R;");
                        return;
                    }
                    Warp(pc, 10023000, 37, 128);
                    break;
                case 2:
                    switch (Select(pc, "什麼都知道…", "", "不讓人發現，拿出了100金幣", "不給"))
                    {
                        case 1:
                            pc.Gold -= 100;
                            switch (Select(pc, "走哪條通道呢？", "", "通過東門的後方走道", "通過西門的後方走道", "通過南門的後方走道", "通過北門的後方走道"))
                            {
                                case 1:
                                    Warp(pc, 10023100, 224, 127);
                                    break;
                                case 2:
                                    Warp(pc, 10023200, 31, 127);
                                    break;
                                case 3:
                                    Warp(pc, 10023300, 128, 225);
                                    break;
                                case 4:
                                    Warp(pc, 10023400, 127, 31);
                                    break;
                            }
                            break;
                        case 2:
                            break;
                    }
                    break;
                case 3:
                    break;
            }
        }
    }
}
