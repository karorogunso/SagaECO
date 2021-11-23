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
    public class S11001092 : Event
    {
        public S11001092()
        {
            this.EventID = 11001092;
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
                Say(pc, 11001092, 131, "这短剑…$R是我跟那女子分手的时候$R留给她的纪念啊$R;" +
                    "$P而且头发、眼珠…$R真的跟她很像阿$R;" +
                    "$R你真的是我……$R;");
                Say(pc, 11001083, 131, "爸…爸爸…$R;" +
                    "$R您…是…我…我爸?$R;");
                Say(pc, 11001089, 131, "理路……$R;" +
                    "$R爸爸能够见到你$R实在太好了$R;");
                Say(pc, 11001092, 131, "…原来我有孩子…$R;" +
                    "$R分手的时候$R一句话都没说…$R;" +
                    "$R…以前不知道…真是…对不起…$R这些年来，很孤单吧…?$R;");
                Say(pc, 11001083, 131, "嗯$R…不过还有监护人，$R;" +
                    "$R所以不太…孤单$R;");
                Say(pc, 11001089, 131, "??$R;" +
                    "$R监护人?$R;");
            }
            else if (Neko_03_amask.Test(Neko_03.堇子任務開始) &&
                !Neko_03_amask.Test(Neko_03.堇子任務完成) &&
                Neko_03_cmask.Test(Neko_03.再次與破多加對話) &&
                !Neko_03_cmask.Test(Neko_03.得到堇子))
            {
            }
            else
            {
                Say(pc, 11001092, 131, "是吗！？$R原来叫理路啊$R;" +
                    "$R是妈妈起的名字吧$R;");
                return;
            }
            Say(pc, 0, 131, "喵$R;", "猫灵?");
            if (pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.PET))
            {
                if (pc.Inventory.Equipments[EnumEquipSlot.PET].ItemID == 10017905)
                {
                    Say(pc, 0, 131, "哎！?$R;" +
                        "$R原来真是这样阿！?$R;", "猫灵（山吹）");
                }
            }
            else if (CountItem(pc, 10017905) >= 1)
            {
                Say(pc, 0, 131, "哎！?$R;" +
                    "$R原来真是这样啊！?$R;", "猫灵（山吹）");
            }
            if (pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.PET))
            {
                if (pc.Inventory.Equipments[EnumEquipSlot.PET].ItemID == 10017900)
                {
                    Say(pc, 0, 131, "哎！…果然…$R;", "猫灵（桃子）");
                }
            }
            else if (CountItem(pc, 10017900) >= 1)
            {
                Say(pc, 0, 131, "哎！…果然…$R;", "猫灵（桃子）");
            }
            if (pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.PET))
            {
                if (pc.Inventory.Equipments[EnumEquipSlot.PET].ItemID == 10017902)
                {
                    Say(pc, 0, 131, "果然……$R;" +
                        "$R…姐姐……菫姐姐?$R;", "凯堤（绿子）");
                }
            }
            else if (CountItem(pc, 10017902) >= 1)
            {
                Say(pc, 0, 131, "果然……$R;" +
                    "$R…姐姐……菫姐姐?$R;", "凯堤（绿子）");
            }
            if (pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.PET))
            {
                if (pc.Inventory.Equipments[EnumEquipSlot.PET].ItemID == 10017903)
                {
                    Say(pc, 0, 131, "哎！…果然…$R;" +
                        "$R很明显是姐姐的事啊$R;", "凯堤（蓝子）");
                }
            }
            else if (CountItem(pc, 10017903) >= 1)
            {
                Say(pc, 0, 131, "哎！…果然…$R;" +
                    "$R很明显是姐姐的事啊$R;", "凯堤（蓝子）");
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
                                "$R……干嘛那么害羞?$R脸都红了?$R;", "猫灵（山吹）");
                            Say(pc, 0, 131, "嗯！?……所以…那个…$R;" +
                                "$R姐姐…$R菫姐姐……?$R;", "猫灵（桃）");
                            得到堇子(pc);
                            return;
                        }
                    }
                    if (CountItem(pc, 10017900) >= 1)
                    {
                        Say(pc, 0, 131, "桃子！桃子?$R;" +
                            "$R姐姐！菫姐姐！！$R;" +
                            "$P??$R…桃子?$R;" +
                            "$R……干嘛那么害羞?$R脸都紅了?$R;", "猫灵（山吹）");
                        Say(pc, 0, 131, "嗯！?……所以…那个…$R;" +
                            "$R姐姐…$R菫姐姐……?$R;", "猫灵（桃子）");
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
                            "$R……干嘛那么害羞?$R脸都红了?$R;", "猫灵（山吹）");
                        Say(pc, 0, 131, "嗯！?……所以…那个…$R;" +
                            "$R姐姐…$R菫姐姐……?$R;", "猫灵（桃子）");
                        得到堇子(pc);
                        return;
                    }
                }
                if (CountItem(pc, 10017900) >= 1)
                {
                    Say(pc, 0, 131, "桃子！桃子?$R;" +
                        "$R姐姐！菫姐姐！！$R;" +
                        "$P??$R…桃子?$R;" +
                        "$R……干嘛那么害羞?$R脸都红了?$R;", "猫灵（山吹）");
                    Say(pc, 0, 131, "嗯！?……所以…那个…$R;" +
                        "$R姐姐…$R菫姐姐……?$R;", "猫灵（桃子）");
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
                            Say(pc, 0, 131, "姐姐…$R是菫姐姐吧…?$R;", "猫灵（桃子）");
                            Say(pc, 0, 131, "姐姐…$R…在吧?$R;" +
                                "$R…菫姐姐…回答吧$R;", "猫灵（绿子）");
                            得到堇子(pc);
                            return;
                        }
                    }
                    if (CountItem(pc, 10017900) >= 1)
                    {
                        Say(pc, 0, 131, "姐姐…$R是菫姐姐吧…?$R;", "猫灵（桃子）");
                        Say(pc, 0, 131, "姐姐…$R…在吧?$R;" +
                            "$R…菫姐姐…回答吧$R;", "猫灵（绿子）");
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
                        Say(pc, 0, 131, "姐姐…$R是菫姐姐吧…?$R;", "猫灵（桃子）");
                        Say(pc, 0, 131, "姐姐…$R…在吧?$R;" +
                            "$R…菫姐姐…回答吧$R;", "猫灵（绿子）");
                        得到堇子(pc);
                        return;
                    }
                }
                if (CountItem(pc, 10017900) >= 1)
                {
                    Say(pc, 0, 131, "姐姐…$R是菫姐姐吧…?$R;", "猫灵（桃子）");
                    Say(pc, 0, 131, "姐姐…$R…在吧?$R;" +
                        "$R…菫姐姐…回答吧$R;", "猫灵（绿子）");
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
                                "$R菫姐姐对吧?$R;" +
                                "$P真是$R桃子姐姐脸都通红了$R;" +
                                "$R原来有点贤熟?$R;", "猫灵（蓝子）");
                            Say(pc, 0, 131, "真是！蓝！太过份了…$R;" +
                                "$R菫姐姐…在吧…?$R;", "猫灵（桃子）");
                            得到堇子(pc);
                            return;
                        }
                    }
                    if (CountItem(pc, 10017900) >= 1)
                    {
                        Say(pc, 0, 131, "姐姐……桃子姐姐！$R;" +
                            "$R菫姐姐对吧?$R;" +
                            "$P真是$R桃子姐姐脸都通红了$R;" +
                            "$R原来有点贤熟?$R;", "猫灵（蓝）");
                        Say(pc, 0, 131, "真是！蓝！太过份了…$R;" +
                            "$R菫姐姐…在吧…?$R;", "猫灵（桃子）");
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
                            "$R菫姐姐对吧?$R;" +
                            "$P真是$R桃子姐姐脸都通红了$R;" +
                            "$R原来有点贤熟?$R;", "猫灵（蓝子）");
                        Say(pc, 0, 131, "真是！蓝！太过份了…$R;" +
                            "$R菫姐姐…在吧…?$R;", "猫灵（桃子）");
                        得到堇子(pc);
                        return;
                    }
                }
                if (CountItem(pc, 10017900) >= 1)
                {
                    Say(pc, 0, 131, "姐姐……桃子姐姐！$R;" +
                        "$R菫姐姐对吧?$R;" +
                        "$P真是$R桃子姐姐脸都通红了$R;" +
                        "$R原来有点贤熟?$R;", "猫灵（蓝子）");
                    Say(pc, 0, 131, "真是！蓝！太过份了…$R;" +
                        "$R菫姐姐…在吧…?$R;", "猫灵（桃子）");
                    得到堇子(pc);
                    return;
                }
            }
            if (pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.PET))
            {
                if (pc.Inventory.Equipments[EnumEquipSlot.PET].ItemID == 10017900)
                {
                    Say(pc, 0, 131, "姐姐…$R;" +
                        "$R菫姐姐对吧……?$R;", "猫灵（桃子）");
                    得到堇子(pc);
                    return;
                }
            }
            else if (CountItem(pc, 10017900) >= 1)
            {
                Say(pc, 0, 131, "姐姐…$R;" +
                    "$R菫姐姐对吧……?$R;", "猫灵（桃子）");
                得到堇子(pc);
                return;
            }
            if (pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.PET))
            {
                if (pc.Inventory.Equipments[EnumEquipSlot.PET].ItemID == 10017905)
                {
                    Say(pc, 0, 131, "姐姐…$R;" +
                        "$R…菫姐姐……?$R;", "猫灵（山吹）");
                    得到堇子(pc);
                    return;
                }
            }
            else if (CountItem(pc, 10017905) >= 1)
            {
                Say(pc, 0, 131, "姐姐…$R;" +
                    "$R…菫姐姐……?$R;", "猫灵（山吹）");
                得到堇子(pc);
                return;
            }
            if (pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.PET))
            {
                if (pc.Inventory.Equipments[EnumEquipSlot.PET].ItemID == 10017902)
                {
                    Say(pc, 0, 131, "姐姐…$R…在那里吧?$R;" +
                        "$R…菫姐姐…回答吧$R;", "猫灵（绿子）");
                    得到堇子(pc);
                    return;
                }
            }
            else if (CountItem(pc, 10017902) >= 1)
            {
                Say(pc, 0, 131, "姐姐…$R…在那里吧?$R;" +
                    "$R…菫姐姐…回答吧$R;", "猫灵（绿子）");
                得到堇子(pc);
                return;
            }
            if (pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.PET))
            {
                if (pc.Inventory.Equipments[EnumEquipSlot.PET].ItemID == 10017903)
                {
                    Say(pc, 0, 131, "姐姐……菫姐姐！$R;", "猫灵（蓝子）");
                    得到堇子(pc);
                    return;
                }
            }
            else if (CountItem(pc, 10017903) >= 1)
            {
                Say(pc, 0, 131, "姐姐……菫姐姐！$R;", "猫灵（蓝子）");
                得到堇子(pc);
                return;
            }
            Say(pc, 0, 131, "哦?难道……$R;" +
                "$R猫灵附身在理路身上了…?$R;");
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
                Say(pc, 0, 131, "得到『猫灵（菫子）』！$R;");
                Say(pc, 11001092, 131, "哦?$R;" +
                    "$R呀呀！$R原来猫灵在旁边看守着呢！$R;" +
                    "$R一直照顾着理路…谢谢$R;");
                Say(pc, 11001083, 131, "哎！?猫灵$R;" +
                    "$R走了吗?怎么样了?$R;");
                if (pc.Gender == PC_GENDER.FEMALE)
                {
                    Say(pc, 11001092, 131, "理路是说这姐姐吧$R她带著其他猫灵同伴呢$R;" +
                        "$R让我可以跟同伴一起吧$R把我送走$R你身边就没有我帮忙了$R;");
                }
                else
                {
                    Say(pc, 11001092, 131, "理路是说这哥哥吧$R还有其他猫灵同伴$R;" +
                        "$R让我可以跟同伴一起吧$R把我送走$R你身边就没有我帮忙了$R;");
                }
                Say(pc, 11001083, 131, "…嗯$R知道了$R;" +
                    "$R谢谢您了…那么…再见了$R;");
                Say(pc, 0, 131, "喵……$R;", "猫灵（菫子）");
                Say(pc, 0, 131, "…哎！果然…$R;" +
                    "$R到底…会增加到多少只呀…$R;");
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
                        Say(pc, 0, 131, "…菫姐姐…$R;", "猫灵（绿子）");
                        Say(pc, 0, 131, "一直很想你喔！绿！$R;" +
                            "$R现在开始…$R我就可以跟绿常常在一起了$R;", "猫灵（菫子）");
                        return;
                    }
                }
                if (CountItem(pc, 10017902) >= 1)
                {
                    Say(pc, 0, 131, "…菫姐姐…$R;", "猫灵（绿子）");
                    Say(pc, 0, 131, "一直很想你喔！绿！$R;" +
                        "$R现在开始…$R我就可以跟绿常常在一起了$R;", "猫灵（菫子）");
                    return;
                }
                if (pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.PET))
                {
                    if (pc.Inventory.Equipments[EnumEquipSlot.PET].ItemID == 10017903)
                    {
                        Say(pc, 0, 131, "…菫姐姐…$R;" +
                            "$R蓝非常开心哦$R;", "凯堤（蓝子）");
                        Say(pc, 0, 131, "我也很开心！$R;" +
                            "$R现在开始…$R我就可以跟蓝常常在一起了$R;", "猫灵（菫子）");
                        return;
                    }
                }
                if (CountItem(pc, 10017903) >= 1)
                {
                    Say(pc, 0, 131, "…菫姐姐…$R;" +
                        "$R蓝非常开心哦$R;", "凯堤（蓝子）");
                    Say(pc, 0, 131, "我也很开心！$R;" +
                        "$R现在开始…$R我就可以跟蓝常常在一起了$R;", "猫灵（菫子）");
                    return;
                }
                if (pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.PET))
                {
                    if (pc.Inventory.Equipments[EnumEquipSlot.PET].ItemID == 10017905)
                    {
                        Say(pc, 0, 131, "…菫姐姐！真的是菫姐姐?$R;", "猫灵（山吹）");
                        Say(pc, 0, 131, "嗯！呵呵！$R山吹，你真是的！！$R;" +
                            "$R现在开始…$R我就可以跟山吹常常在一起了$R;", "猫灵（菫子）");
                        return;
                    }
                }
                if (CountItem(pc, 10017905) >= 1)
                {
                    Say(pc, 0, 131, "…菫姐姐！真的是菫姐姐?$R;", "猫灵（山吹）");
                    Say(pc, 0, 131, "嗯！呵呵！$R山吹，你真是的！！$R;" +
                        "$R现在开始…$R我就可以跟山吹常常在一起了$R;", "猫灵（菫子）");
                    return;
                }
                return;
            }
            Wait(pc, 1000);
            PlaySound(pc, 4012, false, 100, 50);
            ShowEffect(pc, 4131);
            Wait(pc, 6000);
            Say(pc, 0, 131, "喵喵…$R;", "猫灵（菫子）");
            Say(pc, 0, 131, "??$R真是！?$R;" +
                "$R行李都满了$R;");
            Say(pc, 0, 131, "喵…$R;", "猫灵（菫子）");
        }

        void 對話分支1(ActorPC pc)
        {
            Say(pc, 0, 131, "桃子哭了……$R;" +
                "$R本来是个开朗的孩子呢$R;", "猫灵（山吹）");
            if (pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.PET))
            {
                if (pc.Inventory.Equipments[EnumEquipSlot.PET].ItemID == 10017902)
                {
                    Say(pc, 0, 131, "……桃子姐姐$R真的辛苦了$R;" +
                        "$R对我们来说$R姐姐努力工作也很辛苦吧$R;", "猫灵（绿子）");
                    Say(pc, 0, 131, "哎…原来是这样…$R;", "猫灵（山吹）");
                }
            }
            if (CountItem(pc, 10017902) >= 1)
            {
                Say(pc, 0, 131, "……桃子姐姐$R真的辛苦了$R;" +
                    "$R对我们来说$R姐姐努力工作也很辛苦吧$R;", "猫灵（绿子）");
                Say(pc, 0, 131, "哎…原来是这样…$R;", "猫灵（山吹）");
            }
            if (pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.PET))
            {
                if (pc.Inventory.Equipments[EnumEquipSlot.PET].ItemID == 10017903)
                {
                    Say(pc, 0, 131, "桃子姐姐为了我们…$R;" +
                        "$R一直表现的这么开朗$R;", "猫灵（蓝子）");
                    Say(pc, 0, 131, "…原来是这样…$R;", "猫灵（山吹）");
                }
            }
            if (CountItem(pc, 10017903) >= 1)
            {
                Say(pc, 0, 131, "桃子姐姐为了我们…$R;" +
                    "$R一直表现的这么开朗$R;", "猫灵（蓝子）");
                Say(pc, 0, 131, "…原来是这样…$R;", "猫灵（山吹）");
            }
            Say(pc, 0, 131, "…原来…桃子…$R为了我们$R一直装著坚强开朗……$R;", "猫灵（山吹）");
        }

        void 對話分支2(ActorPC pc)
        {

            Say(pc, 0, 131, "姐姐……$R;" +
                "$R呜呜…$R;", "猫灵（桃子）");
            Say(pc, 0, 131, "桃子?怎么了?$R;" +
                "$R为什么哭了！$R不像是你啊！$R;", "猫灵（菫子）");
            Say(pc, 0, 131, "姐姐…$R菫姐姐…$R;" +
                "$R菫姐姐……$R;", "猫灵（桃子）");
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
                        "$R所…所以…呜呜$R;", "猫灵（蓝子）");
                    if (pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.PET))
                    {
                        if (pc.Inventory.Equipments[EnumEquipSlot.PET].ItemID == 10017902)
                        {
                            Say(pc, 0, 131, "……桃子姐姐常常开心的笑着$R;" +
                                "$R这样的姐姐，还是第一次见到…$R;", "猫灵（绿子）");
                        }
                    }
                    if (CountItem(pc, 10017902) >= 1)
                    {
                        Say(pc, 0, 131, "……桃子姐姐常常开心的笑着$R;" +
                            "$R这样的姐姐，还是第一次见到…$R;", "猫灵（绿子）");
                    }
                }
            }
            if (CountItem(pc, 10017903) >= 1)
            {
                Say(pc, 0, 131, "…桃子姐姐哭了……$R;" +
                    "$R所…所以…呜呜$R;", "猫灵（蓝子）");
                if (pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.PET))
                {
                    if (pc.Inventory.Equipments[EnumEquipSlot.PET].ItemID == 10017902)
                    {
                        Say(pc, 0, 131, "……桃子姐姐常常开心的笑着$R;" +
                            "$R这样的姐姐，还是第一次见到…$R;", "猫灵（绿子）");
                    }
                }
                if (CountItem(pc, 10017902) >= 1)
                {
                    Say(pc, 0, 131, "……桃子姐姐常常开心的笑着$R;" +
                        "$R这样的姐姐，还是第一次见到…$R;", "猫灵（绿子）");
                }
            }
            if (pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.PET))
            {
                if (pc.Inventory.Equipments[EnumEquipSlot.PET].ItemID == 10017902)
                {
                    Say(pc, 0, 131, "……桃子姐姐常常开心的笑着$R;" +
                        "$R这样的姐姐，还是第一次见到…$R;", "猫灵（绿子）");
                }
            }
            if (CountItem(pc, 10017902) >= 1)
            {
                Say(pc, 0, 131, "……桃子姐姐常常开心的笑着$R;" +
                    "$R这样的姐姐，还是第一次见到…$R;", "猫灵（绿子）");
            }
            Say(pc, 0, 131, "别哭了…$R;" +
                "$R现在开始…$R我们就可以常常在一起了$R;", "猫灵（绿子）");
        }
    }
}