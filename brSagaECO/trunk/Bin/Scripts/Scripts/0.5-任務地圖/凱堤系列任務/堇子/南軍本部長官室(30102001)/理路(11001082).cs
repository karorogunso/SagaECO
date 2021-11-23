using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaDB.Item;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30102001
{
    public class S11001082 : Event
    {
        public S11001082()
        {
            this.EventID = 11001082;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<Neko_03> Neko_03_amask = pc.AMask["Neko_03"];
            BitMask<Neko_03> Neko_03_cmask = pc.CMask["Neko_03"];

            if (Neko_03_amask.Test(Neko_03.堇子任務開始) &&
                !Neko_03_amask.Test(Neko_03.堇子任務完成) &&
                Neko_03_cmask.Test(Neko_03.帶理路離開) &&
                !Neko_03_cmask.Test(Neko_03.再次與破多加對話))
            {
                Neko_03_cmask.SetValue(Neko_03.再次與破多加對話, true);
                //MUSIC 1141 0 0 100
                Wait(pc, 1000);
                Say(pc, 11001092, 131, "這短劍…$R是我跟那女子分手的時候$R留給她的信物阿$R;" +
                    "$P而且頭髮、眼珠…$R真的跟她很像阿$R;" +
                    "$R你真的是我……$R;");
                Say(pc, 11001083, 131, "爸…爸爸…$R;" +
                    "$R您…是…我…我爸?$R;");
                Say(pc, 11001089, 131, "理路……$R;" +
                    "$R爸爸能夠見到你$R實在太好了$R;");
                Say(pc, 11001092, 131, "…原來我有孩子…$R;" +
                    "$R分手的時候$R一句話都沒說…$R;" +
                    "$R…以前不知道…真是…對不起…$R這些年來，很孤單吧…?$R;");
                Say(pc, 11001083, 131, "嗯$R…不過還有監護人，$R;" +
                    "$R所以不太…孤單$R;");
                Say(pc, 11001089, 131, "??$R;" +
                    "$R監護人?$R;");
                Say(pc, 0, 131, "喵$R;", "凱堤?");
                if (pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.PET))
                {
                    if (pc.Inventory.Equipments[EnumEquipSlot.PET].ItemID == 10017905)
                    {
                        Say(pc, 0, 131, "哎！?$R;" +
                            "$R原來真是這樣阿！?$R;", "凱堤（山吹）");
                    }
                }
                else if (CountItem(pc, 10017905) >= 1)
                {
                    Say(pc, 0, 131, "哎！?$R;" +
                        "$R原來真是這樣阿！?$R;", "凱堤（山吹）");
                }
                if (pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.PET))
                {
                    if (pc.Inventory.Equipments[EnumEquipSlot.PET].ItemID == 10017900)
                    {
                        Say(pc, 0, 131, "哎！…果然…$R;", "凱堤（桃）");
                    }
                }
                else if (CountItem(pc, 10017900) >= 1)
                {
                    Say(pc, 0, 131, "哎！…果然…$R;", "凱堤（桃）");
                }
                if (pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.PET))
                {
                    if (pc.Inventory.Equipments[EnumEquipSlot.PET].ItemID == 10017902)
                    {
                        Say(pc, 0, 131, "果然……$R;" +
                            "$R…姐姐……菫姐姐?$R;", "凱堤（緑）");
                    }
                }
                else if (CountItem(pc, 10017902) >= 1)
                {
                    Say(pc, 0, 131, "果然……$R;" +
                        "$R…姐姐……菫姐姐?$R;", "凱堤（緑）");
                }
                if (pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.PET))
                {
                    if (pc.Inventory.Equipments[EnumEquipSlot.PET].ItemID == 10017903)
                    {
                        Say(pc, 0, 131, "哎！…果然…$R;" +
                            "$R很明顯是姐姐的事阿$R;", "凱堤（藍）");
                    }
                }
                else if (CountItem(pc, 10017903) >= 1)
                {
                    Say(pc, 0, 131, "哎！…果然…$R;" +
                        "$R很明顯是姐姐的事阿$R;", "凱堤（藍）");
                }
                if (pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.PET))
                {
                    if (pc.Inventory.Equipments[EnumEquipSlot.PET].ItemID == 10017905)
                    {
                        if (pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.PET))
                        {
                            if (pc.Inventory.Equipments[EnumEquipSlot.PET].ItemID == 10017900)
                            {
                                Say(pc, 0, 131, "桃子！桃子?$R;" +
                                    "$R姐姐！菫姐姐！！$R;" +
                                    "$P??$R…桃子?$R;" +
                                    "$R……幹嘛那麼害羞?$R臉都紅了?$R;", "凱堤（山吹）");
                                Say(pc, 0, 131, "嗯！?……所以…那個…$R;" +
                                    "$R姐姐…$R菫姐姐……?$R;", "凱堤（桃）");
                                得到堇子(pc);
                                return;
                            }
                        }
                        if (CountItem(pc, 10017900) >= 1)
                        {
                            Say(pc, 0, 131, "桃子！桃子?$R;" +
                                "$R姐姐！菫姐姐！！$R;" +
                                "$P??$R…桃子?$R;" +
                                "$R……幹嘛那麼害羞?$R臉都紅了?$R;", "凱堤（山吹）");
                            Say(pc, 0, 131, "嗯！?……所以…那個…$R;" +
                                "$R姐姐…$R菫姐姐……?$R;", "凱堤（桃）");
                            得到堇子(pc);
                            return;
                        }
                    }
                }
                if (CountItem(pc, 10017905) >= 1)
                {
                    if (pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.PET))
                    {
                        if (pc.Inventory.Equipments[EnumEquipSlot.PET].ItemID == 10017900)
                        {
                            Say(pc, 0, 131, "桃子！桃子?$R;" +
                                "$R姐姐！菫姐姐！！$R;" +
                                "$P??$R…桃子?$R;" +
                                "$R……幹嘛那麼害羞?$R臉都紅了?$R;", "凱堤（山吹）");
                            Say(pc, 0, 131, "嗯！?……所以…那個…$R;" +
                                "$R姐姐…$R菫姐姐……?$R;", "凱堤（桃）");
                            得到堇子(pc);
                            return;
                        }
                    }
                    if (CountItem(pc, 10017900) >= 1)
                    {
                        Say(pc, 0, 131, "桃子！桃子?$R;" +
                            "$R姐姐！菫姐姐！！$R;" +
                            "$P??$R…桃子?$R;" +
                            "$R……幹嘛那麼害羞?$R臉都紅了?$R;", "凱堤（山吹）");
                        Say(pc, 0, 131, "嗯！?……所以…那個…$R;" +
                            "$R姐姐…$R菫姐姐……?$R;", "凱堤（桃）");
                        得到堇子(pc);
                        return;
                    }
                }
                if (pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.PET))
                {
                    if (pc.Inventory.Equipments[EnumEquipSlot.PET].ItemID == 10017902)
                    {
                        if (pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.PET))
                        {
                            if (pc.Inventory.Equipments[EnumEquipSlot.PET].ItemID == 10017900)
                            {
                                Say(pc, 0, 131, "姐姐…$R是菫姐姐吧…?$R;", "凱堤（桃）");
                                Say(pc, 0, 131, "姐姐…$R…在吧?$R;" +
                                    "$R…菫姐姐…回答吧$R;", "凱堤（緑）");
                                得到堇子(pc);
                                return;
                            }
                        }
                        if (CountItem(pc, 10017900) >= 1)
                        {
                            Say(pc, 0, 131, "姐姐…$R是菫姐姐吧…?$R;", "凱堤（桃）");
                            Say(pc, 0, 131, "姐姐…$R…在吧?$R;" +
                                "$R…菫姐姐…回答吧$R;", "凱堤（緑）");
                            得到堇子(pc);
                            return;
                        }
                    }
                }
                if (CountItem(pc, 10017902) >= 1)
                {
                    if (pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.PET))
                    {
                        if (pc.Inventory.Equipments[EnumEquipSlot.PET].ItemID == 10017900)
                        {
                            Say(pc, 0, 131, "姐姐…$R是菫姐姐吧…?$R;", "凱堤（桃）");
                            Say(pc, 0, 131, "姐姐…$R…在吧?$R;" +
                                "$R…菫姐姐…回答吧$R;", "凱堤（緑）");
                            得到堇子(pc);
                            return;
                        }
                    }
                    if (CountItem(pc, 10017900) >= 1)
                    {
                        Say(pc, 0, 131, "姐姐…$R是菫姐姐吧…?$R;", "凱堤（桃）");
                        Say(pc, 0, 131, "姐姐…$R…在吧?$R;" +
                            "$R…菫姐姐…回答吧$R;", "凱堤（緑）");
                        得到堇子(pc);
                        return;
                    }
                }

                if (pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.PET))
                {
                    if (pc.Inventory.Equipments[EnumEquipSlot.PET].ItemID == 10017903)
                    {
                        if (pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.PET))
                        {
                            if (pc.Inventory.Equipments[EnumEquipSlot.PET].ItemID == 10017900)
                            {
                                Say(pc, 0, 131, "姐姐……桃子姐姐！$R;" +
                                    "$R菫姐姐對吧?$R;" +
                                    "$P真是$R桃子姐姐臉都通紅了$R;" +
                                    "$R原來有點賢熟?$R;", "凱堤（藍）");
                                Say(pc, 0, 131, "真是！藍！太過份了…$R;" +
                                    "$R菫姐姐…在吧…?$R;", "凱堤（桃）");
                                得到堇子(pc);
                                return;
                            }
                        }
                        if (CountItem(pc, 10017900) >= 1)
                        {
                            Say(pc, 0, 131, "姐姐……桃子姐姐！$R;" +
                                "$R菫姐姐對吧?$R;" +
                                "$P真是$R桃子姐姐臉都通紅了$R;" +
                                "$R原來有點賢熟?$R;", "凱堤（藍）");
                            Say(pc, 0, 131, "真是！藍！太過份了…$R;" +
                                "$R菫姐姐…在吧…?$R;", "凱堤（桃）");
                            得到堇子(pc);
                            return;
                        }
                    }
                }
                if (CountItem(pc, 10017903) >= 1)
                {
                    if (pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.PET))
                    {
                        if (pc.Inventory.Equipments[EnumEquipSlot.PET].ItemID == 10017900)
                        {
                            Say(pc, 0, 131, "姐姐……桃子姐姐！$R;" +
                                "$R菫姐姐對吧?$R;" +
                                "$P真是$R桃子姐姐臉都通紅了$R;" +
                                "$R原來有點賢熟?$R;", "凱堤（藍）");
                            Say(pc, 0, 131, "真是！藍！太過份了…$R;" +
                                "$R菫姐姐…在吧…?$R;", "凱堤（桃）");
                            得到堇子(pc);
                            return;
                        }
                    }
                    if (CountItem(pc, 10017900) >= 1)
                    {
                        Say(pc, 0, 131, "姐姐……桃子姐姐！$R;" +
                            "$R菫姐姐對吧?$R;" +
                            "$P真是$R桃子姐姐臉都通紅了$R;" +
                            "$R原來有點賢熟?$R;", "凱堤（藍）");
                        Say(pc, 0, 131, "真是！藍！太過份了…$R;" +
                            "$R菫姐姐…在吧…?$R;", "凱堤（桃）");
                        得到堇子(pc);
                        return;
                    }
                }
                if (pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.PET))
                {
                    if (pc.Inventory.Equipments[EnumEquipSlot.PET].ItemID == 10017900)
                    {
                        Say(pc, 0, 131, "姐姐…$R;" +
                            "$R菫姐姐對吧……?$R;", "凱堤（桃）");
                        得到堇子(pc);
                        return;
                    }
                }
                else if (CountItem(pc, 10017900) >= 1)
                {
                    Say(pc, 0, 131, "姐姐…$R;" +
                        "$R菫姐姐對吧……?$R;", "凱堤（桃）");
                    得到堇子(pc);
                    return;
                }
                if (pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.PET))
                {
                    if (pc.Inventory.Equipments[EnumEquipSlot.PET].ItemID == 10017905)
                    {
                        Say(pc, 0, 131, "姐姐…$R;" +
                            "$R…菫姐姐……?$R;", "凱堤（山吹）");
                        得到堇子(pc);
                        return;
                    }
                }
                else if (CountItem(pc, 10017905) >= 1)
                {
                    Say(pc, 0, 131, "姐姐…$R;" +
                        "$R…菫姐姐……?$R;", "凱堤（山吹）");
                    得到堇子(pc);
                    return;
                }
                if (pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.PET))
                {
                    if (pc.Inventory.Equipments[EnumEquipSlot.PET].ItemID == 10017902)
                    {
                        Say(pc, 0, 131, "姐姐…$R…在那裡吧?$R;" +
                            "$R…菫姐姐…回答吧$R;", "凱堤（緑）");
                        得到堇子(pc);
                        return;
                    }
                }
                else if (CountItem(pc, 10017902) >= 1)
                {
                    Say(pc, 0, 131, "姐姐…$R…在那裡吧?$R;" +
                        "$R…菫姐姐…回答吧$R;", "凱堤（緑）");
                    得到堇子(pc);
                    return;
                }
                if (pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.PET))
                {
                    if (pc.Inventory.Equipments[EnumEquipSlot.PET].ItemID == 10017903)
                    {
                        Say(pc, 0, 131, "姐姐……菫姐姐！$R;", "凱堤（藍）");
                        得到堇子(pc);
                        return;
                    }
                }
                else if (CountItem(pc, 10017903) >= 1)
                {
                    Say(pc, 0, 131, "姐姐……菫姐姐！$R;", "凱堤（藍）");
                    得到堇子(pc);
                    return;
                }
                Say(pc, 0, 131, "哦?難道……$R;" +
                    "$R凱堤附身在理路身上了…?$R;");
                return;
            }
            Say(pc, 11001082, 131, "爸爸……$R;");
        }

        void 得到堇子(ActorPC pc)
        {
            BitMask<Neko_03> Neko_03_amask = pc.AMask["Neko_03"];
            BitMask<Neko_03> Neko_03_cmask = pc.CMask["Neko_03"];

            if (CheckInventory(pc, 10017906, 1))
            {
                GiveItem(pc, 10017906, 1);
                Neko_03_amask.SetValue(Neko_03.堇子任務完成, true);
                Neko_03_cmask.SetValue(Neko_03.得到堇子, true);
                Say(pc, 0, 131, "喵嗷！！$R;");
                Wait(pc, 1000);
                PlaySound(pc, 4012, false, 100, 50);
                ShowEffect(pc, 4131);
                Wait(pc, 6000);
                Say(pc, 0, 131, "得到『凱堤（菫）』！$R;");
                Say(pc, 11001092, 131, "哦?$R;" +
                    "$R呀呀！$R原來凱堤在旁邊看守著呢！$R;" +
                    "$R一直照顧著理路…謝謝唷$R;");
                Say(pc, 11001083, 131, "哎！?凱堤$R;" +
                    "$R走了嗎?怎麼樣了?$R;");
                if (pc.Gender == PC_GENDER.FEMALE)
                {
                    Say(pc, 11001092, 131, "理路是說這姐姐吧$R她帶著其他凱堤同伴呢$R;" +
                        "$R讓我可以跟同伴一起吧$R把我送走$R你身邊就沒有我幫忙了$R;");
                }
                else
                {
                    Say(pc, 11001092, 131, "理路！是說這哥哥吧$R還有其他凱堤同伴$R;" +
                        "$R讓我可以跟同伴一起吧$R把我送走$R你身邊就沒有我幫忙了$R;");
                }
                Say(pc, 11001083, 131, "…嗯$R知道了$R;" +
                    "$R謝謝您了…那麼…再見了$R;");
                Say(pc, 0, 131, "喵……$R;", "凱堤（菫）");
                Say(pc, 0, 131, "…哎！果然…$R;" +
                    "$R到底…會增加到多少隻呀…$R;");
                if (pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.PET))
                {
                    if (pc.Inventory.Equipments[EnumEquipSlot.PET].ItemID == 10017900)
                    {
                        對話分支2(pc);
                        return;
                    }
                }
                if (CountItem(pc, 10017900) >= 1)
                {
                    對話分支2(pc);
                    return;
                }
                if (pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.PET))
                {
                    if (pc.Inventory.Equipments[EnumEquipSlot.PET].ItemID == 10017902)
                    {
                        Say(pc, 0, 131, "…菫姐姐…$R;", "凱堤（緑）");
                        Say(pc, 0, 131, "一直很想你喔！緑！$R;" +
                            "$R現在開始…$R我就可以跟菫常常在一起了$R;", "凱堤（菫）");
                        return;
                    }
                }
                if (CountItem(pc, 10017902) >= 1)
                {
                    Say(pc, 0, 131, "…菫姐姐…$R;", "凱堤（緑）");
                    Say(pc, 0, 131, "一直很想你喔！緑！$R;" +
                        "$R現在開始…$R我就可以跟菫常常在一起了$R;", "凱堤（菫）");
                    return;
                }
                if (pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.PET))
                {
                    if (pc.Inventory.Equipments[EnumEquipSlot.PET].ItemID == 10017903)
                    {
                        Say(pc, 0, 131, "…菫姐姐…$R;" +
                            "$R藍非常開心唷$R;", "凱堤（藍）");
                        Say(pc, 0, 131, "我也很開心！$R;" +
                            "$R現在開始…$R我就可以跟菫常常在一起了$R;", "凱堤（菫）");
                        return;
                    }
                }
                if (CountItem(pc, 10017903) >= 1)
                {
                    Say(pc, 0, 131, "…菫姐姐…$R;" +
                        "$R藍非常開心唷$R;", "凱堤（藍）");
                    Say(pc, 0, 131, "我也很開心！$R;" +
                        "$R現在開始…$R我就可以跟菫常常在一起了$R;", "凱堤（菫）");
                    return;
                }
                if (pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.PET))
                {
                    if (pc.Inventory.Equipments[EnumEquipSlot.PET].ItemID == 10017905)
                    {
                        Say(pc, 0, 131, "…菫姐姐！真的是菫姐姐?$R;", "凱堤（山吹）");
                        Say(pc, 0, 131, "嗯！呵呵！$R山吹，你真是的！！$R;" +
                            "$R現在開始…$R我就可以跟菫常常在一起了$R;", "凱堤（菫）");
                        return;
                    }
                }
                if (CountItem(pc, 10017905) >= 1)
                {
                    Say(pc, 0, 131, "…菫姐姐！真的是菫姐姐?$R;", "凱堤（山吹）");
                    Say(pc, 0, 131, "嗯！呵呵！$R山吹，你真是的！！$R;" +
                        "$R現在開始…$R我就可以跟菫常常在一起了$R;", "凱堤（菫）");
                    return;
                }
                return;
            }
            Wait(pc, 1000);
            PlaySound(pc, 4012, false, 100, 50);
            ShowEffect(pc, 4131);
            Wait(pc, 6000);
            Say(pc, 0, 131, "喵喵…$R;", "凱堤（菫）");
            Say(pc, 0, 131, "??$R真是！?$R;" +
                "$R行李都滿了$R;");
            Say(pc, 0, 131, "喵…$R;", "凱堤（菫）");
        }

        void 對話分支1(ActorPC pc)
        {
            Say(pc, 0, 131, "桃子哭了……$R;" +
                "$R本來是個開朗的孩子呢$R;", "凱堤（山吹）");
            if (pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.PET))
            {
                if (pc.Inventory.Equipments[EnumEquipSlot.PET].ItemID == 10017902)
                {
                    Say(pc, 0, 131, "……桃子姐姐$R真的辛苦了$R;" +
                        "$R對我們來說$R姐姐努力工作也很辛苦吧$R;", "凱堤（緑）");
                    Say(pc, 0, 131, "哎…原來是這樣…$R;", "凱堤（山吹）");
                }
            }
            if (CountItem(pc, 10017902) >= 1)
            {
                Say(pc, 0, 131, "……桃子姐姐$R真的辛苦了$R;" +
                    "$R對我們來說$R姐姐努力工作也很辛苦吧$R;", "凱堤（緑）");
                Say(pc, 0, 131, "哎…原來是這樣…$R;", "凱堤（山吹）");
            }
            if (pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.PET))
            {
                if (pc.Inventory.Equipments[EnumEquipSlot.PET].ItemID == 10017903)
                {
                    Say(pc, 0, 131, "桃子姐姐為了我們…$R;" +
                        "$R一直表現的這麼開朗$R;", "凱堤（藍）");
                    Say(pc, 0, 131, "…原來是這樣…$R;", "凱堤（山吹）");
                }
            }
            if (CountItem(pc, 10017903) >= 1)
            {
                Say(pc, 0, 131, "桃子姐姐為了我們…$R;" +
                    "$R一直表現的這麼開朗$R;", "凱堤（藍）");
                Say(pc, 0, 131, "…原來是這樣…$R;", "凱堤（山吹）");
            }
            Say(pc, 0, 131, "…原來…桃子…$R為了我們$R一直裝著堅强開朗……$R;", "凱堤（山吹）");
        }

        void 對話分支2(ActorPC pc)
        {

            Say(pc, 0, 131, "姐姐……$R;" +
                "$R嗚嗚…$R;", "凱堤（桃）");
            Say(pc, 0, 131, "桃子?怎麼了?$R;" +
                "$R為什麼哭了！$R不像是你阿！$R;", "凱堤（菫）");
            Say(pc, 0, 131, "姐姐…$R菫姐姐…$R;" +
                "$R菫姐姐……$R;", "凱堤（桃）");
            if (pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.PET))
            {
                if (pc.Inventory.Equipments[EnumEquipSlot.PET].ItemID == 10017905)
                {
                    對話分支1(pc);
                }
            }
            if (CountItem(pc, 10017905) >= 1)
            {
                對話分支1(pc);
            }
            if (pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.PET))
            {
                if (pc.Inventory.Equipments[EnumEquipSlot.PET].ItemID == 10017903)
                {
                    Say(pc, 0, 131, "…桃子姐姐哭了……$R;" +
                        "$R所…所以…嗚嗚$R;", "凱堤（藍）");
                    if (pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.PET))
                    {
                        if (pc.Inventory.Equipments[EnumEquipSlot.PET].ItemID == 10017902)
                        {
                            Say(pc, 0, 131, "……桃子姐姐常常開心的笑著$R;" +
                                "$R這樣的姐姐，還是第一次見到…$R;", "凱堤（緑）");
                        }
                    }
                    if (CountItem(pc, 10017902) >= 1)
                    {
                        Say(pc, 0, 131, "……桃子姐姐常常開心的笑著$R;" +
                            "$R這樣的姐姐，還是第一次見到…$R;", "凱堤（緑）");
                    }
                }
            }
            if (CountItem(pc, 10017903) >= 1)
            {
                Say(pc, 0, 131, "…桃子姐姐哭了……$R;" +
                    "$R所…所以…嗚嗚$R;", "凱堤（藍）");
                if (pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.PET))
                {
                    if (pc.Inventory.Equipments[EnumEquipSlot.PET].ItemID == 10017902)
                    {
                        Say(pc, 0, 131, "……桃子姐姐常常開心的笑著$R;" +
                            "$R這樣的姐姐，還是第一次見到…$R;", "凱堤（緑）");
                    }
                }
                if (CountItem(pc, 10017902) >= 1)
                {
                    Say(pc, 0, 131, "……桃子姐姐常常開心的笑著$R;" +
                        "$R這樣的姐姐，還是第一次見到…$R;", "凱堤（緑）");
                }
            }
            if (pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.PET))
            {
                if (pc.Inventory.Equipments[EnumEquipSlot.PET].ItemID == 10017902)
                {
                    Say(pc, 0, 131, "……桃子姐姐常常開心的笑著$R;" +
                        "$R這樣的姐姐，還是第一次見到…$R;", "凱堤（緑）");
                }
            }
            if (CountItem(pc, 10017902) >= 1)
            {
                Say(pc, 0, 131, "……桃子姐姐常常開心的笑著$R;" +
                    "$R這樣的姐姐，還是第一次見到…$R;", "凱堤（緑）");
            }
            Say(pc, 0, 131, "別哭了…$R;" +
                "$R現在開始…$R我就可以跟菫常常在一起了$R;", "凱堤（菫）");
        }
    }
}