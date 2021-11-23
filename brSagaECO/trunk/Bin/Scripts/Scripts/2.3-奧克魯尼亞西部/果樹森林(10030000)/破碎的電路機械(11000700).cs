using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Item;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10030000
{
    public class S11000700 : Event
    {
        public S11000700()
        {
            this.EventID = 11000700;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<Neko_02> Neko_02_amask = pc.AMask["Neko_02"];
            BitMask<Neko_02> Neko_02_cmask = pc.CMask["Neko_02"];
            BitMask<Neko_06> Neko_06_amask = pc.AMask["Neko_06"];
            BitMask<Neko_06> Neko_06_cmask = pc.CMask["Neko_06"];

            if (Neko_06_amask.Test(Neko_06.杏子任务开始) &&
                !Neko_06_amask.Test(Neko_06.获得杏子) &&
                Neko_02_amask.Test(Neko_02.藍任務結束) &&
                Neko_06_cmask.Test(Neko_06.獲知恢復的方法) &&
                !Neko_06_cmask.Test(Neko_06.獲得青子的碎片))
            {
                Wait(pc, 990);
                ShowEffect(pc, 234, 131, 5054);
                Wait(pc, 990);
                ShowEffect(pc, 234, 131, 5179);
                Wait(pc, 1980);

                Say(pc, 0, 131, "（……あっ$R;" +
                "$Rなんだか懐かしい気配がする…？$R;" +
                "$Rこの気配は………藍！）$R;", " ");

                Say(pc, 0, 131, "にゃお～～～ん！$R;", " ");
                Wait(pc, 990);
                PlaySound(pc, 4012, false, 100, 50);
                ShowEffect(pc, 4131);
                Wait(pc, 5940);

                Say(pc, 0, 131, "『背負い魔・ネコマタ（藍）』$Rの心を取り戻した！$R;", " ");

                Say(pc, 0, 131, "お姉ちゃん！$R;", "ネコマタ（杏）");

                Say(pc, 0, 131, "まあっ杏！？$R;" +
                "$R…わたくし…$R;" +
                "$Rどうしてここに……？$R;", "ネコマタ（藍）");

                Say(pc, 0, 131, "（ほっ…$Rやっぱりここだったか……よかった。）$R;", " ");
                Neko_06_cmask.SetValue(Neko_06.獲得青子的碎片, true);
                return;
            }

            if (Neko_02_amask.Test(Neko_02.藍任務結束))
            {
                if (pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.PET))
                {
                    if (pc.Inventory.Equipments[EnumEquipSlot.PET].ItemID == 10017903)
                    {
                        Say(pc, 0, 131, "主人…？是來哀悼的吧$R;" +
                            "$R藍很開心$R;", "凱堤(藍)");

                        Say(pc, 0, 131, "主人！我很喜歡你唷!$R;", "凱堤(藍)");
                        return;
                    }
                }
            }
            if (!Neko_02_amask.Test(Neko_02.藍任務結束) && 
                Neko_02_cmask.Test(Neko_02.獲知原始的事情) && 
                !Neko_02_cmask.Test(Neko_02.得到藍) &&
                CountItem(pc, 10017904) == 0)
            {
                Say(pc, 0, 131, "「原始」起來了！$R;");
                Say(pc, 363, "破壞破壞破壞…？！！$R;");
                switch (Select(pc, "想做什麼呢？", "", "停止啟動「原始」", "不停止"))
                {
                    case 1:
                        貓靈選擇6(pc);
                        break;
                    case 2:
                        break;
                }
                return;
            }
            if (!Neko_02_amask.Test(Neko_02.藍任務結束) &&
                Neko_02_cmask.Test(Neko_02.獲知原始的事情) &&
                !Neko_02_cmask.Test(Neko_02.得到藍))
            {
                貓靈選擇3(pc);
                return;
            }
            if (!Neko_02_amask.Test(Neko_02.藍任務結束) &&
                Neko_02_cmask.Test(Neko_02.得知維修方法) && 
                !Neko_02_cmask.Test(Neko_02.開始維修) &&
                CountItem(pc, 10030003) >= 1 &&
                CountItem(pc, 10047200) >= 1 &&
                CountItem(pc, 90000043) >= 1 &&
                CountItem(pc, 90000044) >= 1 &&
                CountItem(pc, 90000045) >= 1 &&
                CountItem(pc, 90000046) >= 1)
            {
                Neko_02_cmask.SetValue(Neko_02.開始維修, true);
                TakeItem(pc, 10030003, 1);
                TakeItem(pc, 10047200, 1);
                TakeItem(pc, 90000043, 1);
                TakeItem(pc, 90000044, 1);
                TakeItem(pc, 90000045, 1);
                TakeItem(pc, 90000046, 1);
                Say(pc, 0, 131, "在塔依本體上安裝了$R『洋鐵的主幹』和『洋鐵的心』$R;");
                Say(pc, 363, "卡卡…$R;");
                Say(pc, 0, 131, "在塔依本體上安裝了4個精緻的結晶$R;");
                Wait(pc, 2000);
                ShowEffect(pc, 11000700, 5049);
                Wait(pc, 2000);
                Say(pc, 0, 363, "…嚓阿…嚓嘎…嚓嘎阿…$R;" +
                    "$P塔依開始自動恢復了$R;");
                Say(pc, 0, 131, "喵…喵喵$R;", "凱堤(桃子)");
                貓靈選擇2(pc);
                return;
            }
            if (!Neko_02_amask.Test(Neko_02.藍任務結束) &&
                Neko_02_cmask.Test(Neko_02.獲知原始的事情) &&
                !Neko_02_cmask.Test(Neko_02.得到藍))
            {
                貓靈選擇2(pc);
                return;
            }
            if (!Neko_02_amask.Test(Neko_02.藍任務結束) && 
                Neko_02_cmask.Test(Neko_02.聽取建議) &&
                !Neko_02_cmask.Test(Neko_02.獲知原始的事情))
            {
                貓靈選擇2(pc);
                return;
            }
            if (!Neko_02_amask.Test(Neko_02.藍任務結束) &&
                Neko_02_cmask.Test(Neko_02.開始維修) &&
                !Neko_02_cmask.Test(Neko_02.聽取建議))
            {
                貓靈選擇2(pc);
                return;
            }
            if (!Neko_02_cmask.Test(Neko_02.藍任務開始) &&
                !Neko_02_cmask.Test(Neko_02.得到三角巾) &&
                !Neko_02_cmask.Test(Neko_02.藍任務失敗) &&
                !Neko_02_amask.Test(Neko_02.藍任務結束) &&
                pc.Fame > 19 &&
                pc.Level > 29)
            {
                if (pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.PET))
                {
                    if (pc.Inventory.Equipments[EnumEquipSlot.PET].ItemID == 10017900 ||
                        pc.Inventory.Equipments[EnumEquipSlot.PET].ItemID == 10017902)
                    {
                        Neko_02_cmask.SetValue(Neko_02.藍任務開始, true);
                        Say(pc, 131, "！？$R;" +
                            "$P喵！？$R;" +
                            "$P喵～！！喵嗷！！$R喵…$R;");
                        貓靈選擇1(pc);
                        return;
                    }
                }
            }
            if (Neko_02_cmask.Test(Neko_02.藍任務失敗))
            {
                Say(pc, 131, "「原始」安靜的睡著了…$R;");
                return;
            }
            if (Neko_02_amask.Test(Neko_02.藍任務結束))
            {
                Say(pc, 131, "「原始」安靜的睡著了…$R;");
                return;
            }
            Say(pc, 131, "！？$R;" +
                "$R好像是碎掉的活動木偶阿…$R;");
        }

        void 貓靈選擇1(ActorPC pc)
        {
            Say(pc, 131, "…？$R;" +
                "$R凱堤好像有點奇怪啊…？$R;");
            if (pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.PET))
            {
                if (pc.Inventory.Equipments[EnumEquipSlot.PET].ItemID == 10017900)
                {
                    Say(pc, 0, 131, "藍！是藍！$R絕對不會錯的！$R;", "凱堤(桃子)");
                    Say(pc, 0, 131, "藍？$R;");
                    Say(pc, 0, 131, "喵～～喵～～$R;", "凱堤(桃子)");
                    Say(pc, 0, 131, "真是…$R;" +
                        "不知道在說什麼$R;" +
                        "如果能理解凱堤說的話就好了…$R;");
                    return;
                }
                if (pc.Inventory.Equipments[EnumEquipSlot.PET].ItemID == 10017902)
                {
                    Say(pc, 0, 131, "藍？…是藍嗎？$R;", "凱堤(綠子)");
                    Say(pc, 0, 131, "藍？$R;");
                    Say(pc, 0, 131, "喵喵～～～～$R;", "凱堤(綠子)");
                    Say(pc, 0, 131, "真是…$R;" +
                        "不知道在說什麼啊$R;" +
                        "要是能夠聽得懂凱堤的話…$R;");
                    return;
                }
            }
        }

        void 貓靈選擇2(ActorPC pc)
        {
            if (pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.PET))
            {
                if (pc.Inventory.Equipments[EnumEquipSlot.PET].ItemID == 10017900)
                {
                    Say(pc, 0, 131, "嗯~藍很高興呢！$R;" +
                        "$P…？？$R;" +
                        "$R活動木偶…剛剛沒有說什麼嗎？$R;", "凱堤(桃子)");
                    Say(pc, 363, "…破…壞…破壞吧…$R;");
                    Say(pc, 0, 131, "好像真的在說什麼…$R;" +
                        "$R這個活動木偶…好像有點奇怪啊…$R;", "凱堤(桃子)");
                    return;
                }
                if (pc.Inventory.Equipments[EnumEquipSlot.PET].ItemID == 10017902)
                {
                    Say(pc, 0, 131, "…？$R…很高興喔！$R;" +
                        "$P…活動木偶…剛剛沒有說什麼嗎？$R;", "凱堤(綠子)");
                    Say(pc, 363, "…破…壞…破壞吧…$R;");
                    Say(pc, 0, 131, "好像真的在說什麼耶…$R有點奇怪啊…$R;", "凱堤(綠子)");
                    return;
                }
            }
            if (CountItem(pc, 10017900) >= 1)
            {
                Say(pc, 0, 131, "嗯~藍很高興呢！$R;" +
                    "$P…？？$R;" +
                    "$R活動木偶…剛剛沒有說什麼嗎？$R;", "凱堤(桃子)");
                Say(pc, 363, "…破…壞…破壞吧…$R;");
                Say(pc, 0, 131, "好像真的在說什麼…$R;" +
                    "$R這個活動木偶…好像有點奇怪啊…$R;", "凱堤(桃子)");
                return;
            }
            if (CountItem(pc, 10017902) >= 1)
            {
                Say(pc, 0, 131, "…？$R…很高興喔！$R;" +
                    "$P…活動木偶…剛剛沒有說什麼嗎？$R;", "凱堤(綠子)");
                Say(pc, 363, "…破…壞…破壞吧…$R;");
                Say(pc, 0, 131, "好像真的在說什麼耶…$R有點奇怪啊…$R;", "凱堤(綠子)");
                return;
            }
            Say(pc, 363, "…破…壞…破壞吧…$R;");
            Say(pc, 0, 131, "嗯？？？活動木偶說什麼了嗎？$R;");
        }

        void 貓靈選擇3(ActorPC pc)
        {
            if (pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.PET))
            {
                if (pc.Inventory.Equipments[EnumEquipSlot.PET].ItemID == 10017900)
                {
                    Say(pc, 363, "…破壞破壞破壞破壞……$R;");
                    Say(pc, 0, 131, "啊啊…$R塔依……！$R;", "凱堤(桃子)");
                    停止原始1(pc);
                    return;
                }
                if (pc.Inventory.Equipments[EnumEquipSlot.PET].ItemID == 10017902)
                {
                    Say(pc, 363, "…破壞破壞破壞破壞……$R;");
                    Say(pc, 0, 131, "…哎…塔依……？$R;", "凱堤(綠子)");
                    停止原始1(pc);
                    return;
                }
            }
            if (CountItem(pc, 10017900) >= 1)
            {
                Say(pc, 363, "…破壞破壞破壞破壞……$R;");
                Say(pc, 0, 131, "啊啊…$R塔依……！$R;", "凱堤(桃子)");
                停止原始1(pc);
                return;
            }
            if (CountItem(pc, 10017902) >= 1)
            {
                Say(pc, 363, "…破壞破壞破壞破壞……$R;");
                Say(pc, 0, 131, "…哎…塔依……？$R;", "凱堤(綠子)");
                停止原始1(pc);
                return;
            }
        }

        void 停止原始1(ActorPC pc)
        {
            switch (Select(pc, "想做什麼呢？", "", "停止", "不停止"))
            {
                case 1:
                    Say(pc, 0, 131, "「原始」起來了！$R;");
                    Say(pc, 363, "破壞破壞破壞…$R破壞吧！！$R;");
                    貓靈選擇4(pc);
                    break;
                case 2:
                    break;
            }
        }

        void 貓靈選擇4(ActorPC pc)
        {
            if (pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.PET))
            {
                if (pc.Inventory.Equipments[EnumEquipSlot.PET].ItemID == 10017900)
                {
                    Say(pc, 0, 131, "藍！快逃啊！！$R快阿！！$R;", "凱堤(桃子)");
                    Say(pc, 0, 131, "喵嗷～～～喵阿阿！！$R;", "凱堤(桃子)");
                    Say(pc, 0, 363, "把「原始」的黃色栓塞拔掉了！！$R;");
                    Wait(pc, 1000);
                    ShowEffect(pc, 11000700, 5146);
                    Wait(pc, 1000);
                    Say(pc, 363, "嗚～嗚…嗚～嗚…$R;" +
                        "$R……$R;");
                    Say(pc, 0, 131, "…「原始」停止了…$R;");
                    Say(pc, 0, 131, "藍…？藍！？$R藍！！…不行！！$R;", "凱堤(桃子)");
                    獲得藍(pc);
                    return;
                }
                if (pc.Inventory.Equipments[EnumEquipSlot.PET].ItemID == 10017902)
                {
                    Say(pc, 0, 131, "藍…！快逃阿！$R;", "凱堤(綠子)");
                    Say(pc, 0, 131, "把「原始」的黃色栓塞拔掉了！！$R;");
                    Wait(pc, 1000);
                    ShowEffect(pc, 11000700, 5146);
                    Wait(pc, 1000);
                    Say(pc, 363, "嗚～嗚…嗚～嗚…$R;" +
                        "$R……$R;");
                    Say(pc, 0, 131, "「原始」停止了…$R;");
                    Say(pc, 0, 131, "藍…？！藍！！$R;", "凱堤(綠子)");
                    獲得藍(pc);
                    return;
                }
            }
            if (CountItem(pc, 10017900) >= 1)
            {
                Say(pc, 0, 131, "藍！快逃啊！！$R快阿！！$R;", "凱堤(桃子)");
                Say(pc, 0, 131, "喵嗷～～～喵阿阿！！$R;", "凱堤(桃子)");
                Say(pc, 0, 363, "把「原始」的黃色栓塞拔掉了！！$R;");
                Wait(pc, 1000);
                ShowEffect(pc, 11000700, 5146);
                Wait(pc, 1000);
                Say(pc, 363, "嗚～嗚…嗚～嗚…$R;" +
                    "$R……$R;");
                Say(pc, 0, 131, "…「原始」停止了…$R;");
                Say(pc, 0, 131, "藍…？藍！？$R藍！！…不行！！$R;", "凱堤(桃子)");
                獲得藍(pc);
                return;
            }
            if (CountItem(pc, 10017902) >= 1)
            {
                Say(pc, 0, 131, "藍…！快逃阿！$R;", "凱堤(綠子)");
                Say(pc, 0, 131, "把「原始」的黃色栓塞拔掉了！！$R;");
                Wait(pc, 1000);
                ShowEffect(pc, 11000700, 5146);
                Wait(pc, 1000);
                Say(pc, 363, "嗚～嗚…嗚～嗚…$R;" +
                    "$R……$R;");
                Say(pc, 0, 131, "「原始」停止了…$R;");
                Say(pc, 0, 131, "藍…？！藍！！$R;", "凱堤(綠子)");
                獲得藍(pc);
                return;
            }
        }

        void 獲得藍(ActorPC pc)
        {
            BitMask<Neko_02> Neko_02_amask = pc.AMask["Neko_02"];
            BitMask<Neko_02> Neko_02_cmask = pc.CMask["Neko_02"];

            TakeItem(pc, 10017904, 1);
            GiveItem(pc, 10017903, 1);
            Neko_02_cmask.SetValue(Neko_02.得到藍, true);
            Neko_02_amask.SetValue(Neko_02.藍任務結束, true);
            Neko_02_cmask.SetValue(Neko_02.藍任務開始, false);
            Neko_02_cmask.SetValue(Neko_02.與裁縫阿姨第一次對話, false);
            Neko_02_cmask.SetValue(Neko_02.得知維修方法, false);
            Neko_02_cmask.SetValue(Neko_02.開始維修, false);
            Neko_02_cmask.SetValue(Neko_02.聽取建議, false);
            Neko_02_cmask.SetValue(Neko_02.獲知原始的事情, false);
            Say(pc, 0, 131, "咪阿嗷～～！！$R;");
            Say(pc, 0, 131, "『裁縫師阿姨的三角巾』不見了$R;");
            Wait(pc, 1000);
            ShowEffect(pc, 5116);
            Wait(pc, 2000);
            Say(pc, 0, 131, "得到了『凱堤(藍)』！$R;");
            貓靈選擇5(pc);
        }

        void 貓靈選擇5(ActorPC pc)
        {
            if (pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.PET))
            {
                if (pc.Inventory.Equipments[EnumEquipSlot.PET].ItemID == 10017900)
                {
                    Say(pc, 0, 131, "藍…$R剛剛真是好險啊…$R;", "凱堤(桃子)");
                    Say(pc, 0, 131, "真是…？…？？$R！！$R主人主人…！！$R;", "凱堤(藍)");
                    Say(pc, 0, 131, "…？$R;", "破碎的塔依");
                    Say(pc, 0, 131, "藍…？你是說主人阿…$R主人他走了…$R;", "凱堤(桃子)");
                    Say(pc, 0, 131, "主人…$R;", "凱堤(藍)");
                    Say(pc, 0, 131, "藍看這裡！$R;" +
                        "$R是這個人救了你$R所以他是你的新主人唷$R;", "凱堤(桃子)");
                    Say(pc, 0, 131, "…？新…主人？$R;", "凱堤(藍)");
                    Say(pc, 0, 131, "是阿！跟新主人打聲招呼吧$R;", "凱堤(桃子)");
                    Say(pc, 0, 131, "…？$R;" +
                        "$P…我叫…藍…請多多關照…$R;", "凱堤(藍)");
                    return;
                }
                if (pc.Inventory.Equipments[EnumEquipSlot.PET].ItemID == 10017902)
                {
                    Say(pc, 0, 131, "藍…$R剛剛真是好險啊…$R;", "凱堤(綠子)");
                    Say(pc, 0, 131, "真是…？…？？$R！！$R主人！！主人…！！$R;", "凱堤(藍)");
                    Say(pc, 0, 131, "……？$R;", "破碎的塔依");
                    Say(pc, 0, 131, "藍…！主人…走了$R;", "凱堤(綠子)");
                    Say(pc, 0, 131, "主人…$R;", "凱堤(藍)");
                    Say(pc, 0, 131, "藍看這裡！$R;" +
                        "$R是這個人救了你$R所以他是你的新主人唷$R;", "凱堤(綠子)");
                    Say(pc, 0, 131, "…？新…主人？$R;", "凱堤(藍)");
                    Say(pc, 0, 131, "是的$R;", "凱堤(綠子)");
                    Say(pc, 0, 131, "…？$R;" +
                        "$P…我叫…藍…請多多關照…$R;", "凱堤(藍)");
                    return;
                }
            }
            if (CountItem(pc, 10017900) >= 1)
            {
                Say(pc, 0, 131, "藍…$R剛剛真是好險啊…$R;", "凱堤(桃子)");
                Say(pc, 0, 131, "真是…？…？？$R！！$R主人主人…！！$R;", "凱堤(藍)");
                Say(pc, 0, 131, "…？$R;", "破碎的塔依");
                Say(pc, 0, 131, "藍…？你是說主人阿…$R主人他走了…$R;", "凱堤(桃子)");
                Say(pc, 0, 131, "主人…$R;", "凱堤(藍)");
                Say(pc, 0, 131, "藍看這裡！$R;" +
                    "$R是這個人救了你$R所以他是你的新主人唷$R;", "凱堤(桃子)");
                Say(pc, 0, 131, "…？新…主人？$R;", "凱堤(藍)");
                Say(pc, 0, 131, "是阿！跟新主人打聲招呼吧$R;", "凱堤(桃子)");
                Say(pc, 0, 131, "…？$R;" +
                    "$P…我叫…藍…請多多關照…$R;", "凱堤(藍)");
                return;
            }
            if (CountItem(pc, 10017902) >= 1)
            {
                Say(pc, 0, 131, "藍…$R剛剛真是好險啊…$R;", "凱堤(綠子)");
                Say(pc, 0, 131, "真是…？…？？$R！！$R主人！！主人…！！$R;", "凱堤(藍)");
                Say(pc, 0, 131, "……？$R;", "破碎的塔依");
                Say(pc, 0, 131, "藍…！主人…走了$R;", "凱堤(綠子)");
                Say(pc, 0, 131, "主人…$R;", "凱堤(藍)");
                Say(pc, 0, 131, "藍看這裡！$R;" +
                    "$R是這個人救了你$R所以他是你的新主人唷$R;", "凱堤(綠子)");
                Say(pc, 0, 131, "…？新…主人？$R;", "凱堤(藍)");
                Say(pc, 0, 131, "是的$R;", "凱堤(綠子)");
                Say(pc, 0, 131, "…？$R;" +
                    "$P…我叫…藍…請多多關照…$R;", "凱堤(藍)");
                return;
            }
        }

        void 貓靈選擇6(ActorPC pc)
        {
            BitMask<Neko_02> Neko_02_cmask = pc.CMask["Neko_02"];

            Neko_02_cmask.SetValue(Neko_02.藍任務開始, false);
            Neko_02_cmask.SetValue(Neko_02.與裁縫阿姨第一次對話, false);
            Neko_02_cmask.SetValue(Neko_02.得知維修方法, false);
            Neko_02_cmask.SetValue(Neko_02.開始維修, false);
            Neko_02_cmask.SetValue(Neko_02.聽取建議, false);
            Neko_02_cmask.SetValue(Neko_02.獲知原始的事情, false);

            if (pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.PET))
            {
                if (pc.Inventory.Equipments[EnumEquipSlot.PET].ItemID == 10017900)
                {
                    Neko_02_cmask.SetValue(Neko_02.藍任務失敗, true);
                    Say(pc, 0, 131, "把「原始」的黃色栓塞拔掉了！！$R;");
                    Wait(pc, 1000);
                    ShowEffect(pc, 11000700, 5146);
                    Wait(pc, 1000);
                    Say(pc, 363, "嗚～嗚…嗚～嗚…$R;" +
                        "$R……$R;");
                    Say(pc, 0, 131, "…「原始」停止了…$R;");
                    Say(pc, 0, 131, "藍…？藍！？$R藍！！…不行！！$R;" +
                        "$P沒有藍的蹤影了$R;" +
                        "$P太過分了！！主人真的太過分了！！$R;", "凱堤(桃子)");
                    return;
                }
                if (pc.Inventory.Equipments[EnumEquipSlot.PET].ItemID == 10017902)
                {
                    Neko_02_cmask.SetValue(Neko_02.藍任務失敗, true);
                    Say(pc, 0, 131, "把「原始」的黃色栓塞拔掉了！！$R;");
                    Wait(pc, 1000);
                    ShowEffect(pc, 11000700, 5146);
                    Wait(pc, 1000);
                    Say(pc, 363, "嗚～嗚…嗚～嗚…$R;" +
                        "$R……$R;");
                    Say(pc, 0, 131, "…「原始」停止了…$R;");
                    Say(pc, 0, 131, "藍…？藍！？$R;" +
                        "$P沒有藍的蹤影了$R;" +
                        "$P…真是太過分了！$R;", "凱堤(綠子)");
                    return;
                }
            }
            if (CountItem(pc, 10017900) >= 1)
            {
                Neko_02_cmask.SetValue(Neko_02.藍任務失敗, true);
                Say(pc, 0, 131, "把「原始」的黃色栓塞拔掉了！！$R;");
                Wait(pc, 1000);
                ShowEffect(pc, 11000700, 5146);
                Wait(pc, 1000);
                Say(pc, 363, "嗚～嗚…嗚～嗚…$R;" +
                    "$R……$R;");
                Say(pc, 0, 131, "…「原始」停止了…$R;");
                Say(pc, 0, 131, "藍…？藍！？$R藍！！…不行！！$R;" +
                    "$P沒有藍的蹤影了$R;" +
                    "$P太過分了！！主人真的太過分了！！$R;", "凱堤(桃子)");
                return;
            }
            if (CountItem(pc, 10017902) >= 1)
            {
                Neko_02_cmask.SetValue(Neko_02.藍任務失敗, true);
                Say(pc, 0, 131, "把「原始」的黃色栓塞拔掉了！！$R;");
                Wait(pc, 1000);
                ShowEffect(pc, 11000700, 5146);
                Wait(pc, 1000);
                Say(pc, 363, "嗚～嗚…嗚～嗚…$R;" +
                    "$R……$R;");
                Say(pc, 0, 131, "…「原始」停止了…$R;");
                Say(pc, 0, 131, "藍…？藍！？$R;" +
                    "$P沒有藍的蹤影了$R;" +
                    "$P…真是太過分了！$R;", "凱堤(綠子)");
                return;
            }
            Neko_02_cmask.SetValue(Neko_02.藍任務失敗, true);
            Say(pc, 0, 131, "把「原始」的黃色栓塞拔掉了！！$R;");
            Wait(pc, 1000);
            ShowEffect(pc, 11000700, 5146);
            Wait(pc, 1000);
            Say(pc, 363, "咚！！$R嗚…？嗚…？$R;" +
                "$R…$R;");
            Say(pc, 0, 131, "…「原始」停止了…$R;");
            Say(pc, 0, 131, "喵啊嗷…$R;" +
                "……?$R;", "凱堤(桃子)");
            Say(pc, 0, 131, "聽見…凱堤的聲音…$R;");
        }
    }
}
