using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaDB.Item;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
namespace SagaScript.M50063000
{
    public class S12002102 : Event
    {
        public S12002102()
        {
            this.EventID = 12002102;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<Neko_09> Neko_09_mask = new BitMask<Neko_09>(pc.CMask["Neko_09"]);
            //int selection;
            if (Neko_09_mask.Test(Neko_09.去军舰岛))
            {
                Say(pc, 0, 131, "ネコマタの気配はない……。$R;", " ");
                return;
            }
            if (pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.PET))
            {
                if (pc.Inventory.Equipments[EnumEquipSlot.PET].ItemID == 10017900)
                {
                    对话(pc);
                    return;
                }
                if (pc.Inventory.Equipments[EnumEquipSlot.PET].ItemID == 10017903)
                {
                    对话(pc);
                    return;
                }
                if (pc.Inventory.Equipments[EnumEquipSlot.PET].ItemID == 10017905)
                {
                    对话(pc);
                    return;
                }
                if (pc.Inventory.Equipments[EnumEquipSlot.PET].ItemID == 10017906)
                {
                    对话(pc);
                    return;
                }
                if (pc.Inventory.Equipments[EnumEquipSlot.PET].ItemID == 10017907)
                {
                    对话(pc);
                    return;
                }
                if (pc.Inventory.Equipments[EnumEquipSlot.PET].ItemID == 10017908)
                {
                    对话(pc);
                    return;
                }
                if (pc.Inventory.Equipments[EnumEquipSlot.PET].ItemID == 10017910)
                {
                    对话(pc);
                    return;
                }
                if (pc.Inventory.Equipments[EnumEquipSlot.PET].ItemID == 10017912)
                {
                    对话(pc);
                    return;
                }

            }

        }
        void 对话(ActorPC pc)
        {
            BitMask<Neko_09> Neko_09_mask = new BitMask<Neko_09>(pc.CMask["Neko_09"]);
            //int selection;
            if (Neko_09_mask.Test(Neko_09.DEM消失))
            {
                if (CountItem(pc, 10002002) > 0)
                {
                    if (Select(pc, "黒の聖杯をつかいますか？", "", "いいえ", "はい") == 2)
                    {
                        ShowEffect(pc, 5019);
                        Wait(pc, 2970);
                        ShowEffect(pc, 12002102, 4145);
                        Wait(pc, 990);
                        ShowEffect(pc, 12002102, 4145);
                        Wait(pc, 990);
                        ShowEffect(pc, 12002102, 4145);
                        Wait(pc, 1980);
                        ShowEffect(pc, 12002102, 5013);
                        Wait(pc, 660);
                        TakeItem(pc, 10002002, 1);

                        NPCChangeView(pc, 12002102, 11001687);
                        Wait(pc, 1980);
                        Say(pc, 131, "……ん……ご主人、……朝？$R;", "ネコマタ（緑）");
                        if (pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.PET))
                        {
                            if (pc.Inventory.Equipments[EnumEquipSlot.PET].ItemID == 10017900)
                            {
                                Neko_09_mask.SetValue(Neko_09.去军舰岛, true);
                                桃(pc);
                                return;
                            }
                            if (pc.Inventory.Equipments[EnumEquipSlot.PET].ItemID == 10017903)
                            {
                                Neko_09_mask.SetValue(Neko_09.去军舰岛, true);
                                蓝(pc);
                                return;
                            }
                            if (pc.Inventory.Equipments[EnumEquipSlot.PET].ItemID == 10017905)
                            {
                                Neko_09_mask.SetValue(Neko_09.去军舰岛, true);
                                山吹(pc);
                                return;
                            }
                            if (pc.Inventory.Equipments[EnumEquipSlot.PET].ItemID == 10017906)
                            {
                                Neko_09_mask.SetValue(Neko_09.去军舰岛, true);
                                菫(pc);
                                return;
                            }
                            if (pc.Inventory.Equipments[EnumEquipSlot.PET].ItemID == 10017907)
                            {
                                Neko_09_mask.SetValue(Neko_09.去军舰岛, true);
                                茜(pc);
                                return;
                            }
                            if (pc.Inventory.Equipments[EnumEquipSlot.PET].ItemID == 10017908)
                            {
                                Neko_09_mask.SetValue(Neko_09.去军舰岛, true);
                                杏(pc);
                                return;
                            }
                            if (pc.Inventory.Equipments[EnumEquipSlot.PET].ItemID == 10017910)
                            {
                                Neko_09_mask.SetValue(Neko_09.去军舰岛, true);
                                空(pc);
                                return;
                            }
                            if (pc.Inventory.Equipments[EnumEquipSlot.PET].ItemID == 10017912)
                            {
                                Neko_09_mask.SetValue(Neko_09.去军舰岛, true);
                                胡桃若菜(pc);
                                return;
                            }

                        }
                    }
                    return;
                }
            }
            Say(pc, 0, 131, "んにゃっ！？$R;", "ネコマタ");

            Say(pc, 0, 131, "（あれは！！ＤＥＭ種族！？）$R;", " ");
            Wait(pc, 1980);

            Say(pc, 11001686, 65535, "……。$R;" +
            "$Pカミオ……ヂロ？$R;", "");

            Say(pc, 0, 131, "フニャーーッ！！！$R;", "ネコマタ");

            Say(pc, 11001686, 65535, "……。$R;" +
            "$Pおまえ……だれ？$R;", "");

            Say(pc, 0, 131, "フーーッ、フーーッ！！$R;", "ネコマタ");

            Say(pc, 11001686, 65535, "……そんなに、こわがるな。$R;" +
            "$Pわたし、攻撃しない。$R;" +
            "$Rママに、言われていないから$R;" +
            "戦うこと…したくない。$R;", "");

            Say(pc, 0, 131, "フーーッ！！！$R;", "ネコマタ");

            Say(pc, 11001686, 65535, "それより、聞きたいことある。$R;" +
            "$Rここにいた、ネコ、知らないか？$R;", "");

            Say(pc, 0, 131, "フーーッ、……にゃ？$R;", "ネコマタ");

            Say(pc, 11001686, 65535, "お前に、にている。$R;", "");

            Say(pc, 0, 131, "にゃにゃにゃん？？？$R;", "ネコマタ");

            Say(pc, 11001686, 65535, "前に、ママに言われて$R;" +
            "おまえたちの様子$R;" +
            "調べに来たとき、見つけた。$R;" +
            "$Rとても、弱っていた。$R;" +
            "$Pおまえたち$R;" +
            "だれも気がつかない……。$R;" +
            "$Pわたし、気になって$R;" +
            "ママに黙って、ここに来た。$R;", "");

            Say(pc, 0, 131, "……にゃにゃ～？$R;", "ネコマタ");

            Say(pc, 11001686, 65535, "……死んだのか？$R;", "");

            Say(pc, 0, 131, "にゃっ！$Pにゃんにゃにゃにゃ！$R;" +
            "$Rにゃおん、にゃんにゃん！$R;" +
            "にゃ～ん、にゃにゃにゃんにゃ！$R;", "ネコマタ");
            Wait(pc, 1485);

            Say(pc, 11001686, 65535, "……そうか、助かるのか。$R;" +
            "$Rなら、いい……。$R;", "");
            NPCMotion(pc, 11001686, 342, false, 10);
            ShowEffect(pc, 11001686, 8016);
            Wait(pc, 825);
            ShowEffect(pc, 11001686, 5045);
            Wait(pc, 330);

            NPCChangeView(pc, 11001686, 10770000);

            Say(pc, 0, 131, "にゃ？$R;", "ネコマタ");

            Say(pc, 0, 131, "ＤＥＭは消え去った……。$R;", " ");
            Wait(pc, 1980);

            Say(pc, 0, 131, "にゃっっ！！$R;" +
            "にゃー、にゃー、にゃ！！！$R;", "ネコマタ");

            Say(pc, 0, 131, "そうだ、黒の聖杯！！$R;", " ");
            Neko_09_mask.SetValue(Neko_09.DEM消失, true);
        }
        void 桃(ActorPC pc)
        {
            Say(pc, 0, 131, "緑ちゃん！！！$R;" +
            "$Rよかった～～～～！！！！$R;" +
            "どう、どこも苦しくない？$R;", "ネコマタ（桃）");

            Say(pc, 131, "……桃、ねえさん？$R;" +
            "$P……どうして、桃ねえさんが？$R;", "ネコマタ（緑）");

            Say(pc, 0, 131, "緑ちゃん、消えそうだったんだよ！$R;" +
            "もうっ、心配したんだから！$R;", "ネコマタ（桃）");

            Say(pc, 131, "……あ、わた…し……。$R;" +
            "$P……。$R;" +
            "$Pお願い……確かめたいことがあるの。$R;" +
            "$P軍艦島に、連れて行って……。$R;", "ネコマタ（緑）");

            Say(pc, 0, 131, "えっ、緑ちゃん、軍艦島って！？$R;", "ネコマタ（桃）");

            Say(pc, 131, "崖の…下……に、あるはずだから……。$R;", "ネコマタ（緑）");
            ShowEffect(pc, 12002102, 5015);
            ShowEffect(pc, 5013);
            Wait(pc, 330);

            NPCChangeView(pc, 12002102, 12002101);
            Wait(pc, 1980);

            Say(pc, 0, 131, "緑の三角巾に、何か宿ったようだ……。$R;", " ");

            Say(pc, 0, 131, "緑ちゃん、緑ちゃん！？$R;" +
            "$P……。$R;" +
            "$P……大丈夫。眠っただけみたい。$R;", "ネコマタ（桃）");
            ShowEffect(pc, 12002102, 5016);

            Say(pc, 0, 131, "緑のいた場所に何か落ちている……。$R;", " ");
            PlaySound(pc, 2040, false, 100, 50);
            GiveItem(pc, 10065200, 1);
            Say(pc, 0, 131, "『マナの守護』を入手した。$R;", " ");


        }
        void 蓝(ActorPC pc)
        {
            Say(pc, 0, 131, "緑姉さま！！！$R;" +
            "$Rよかった～～～～！！！！$R;" +
            "ご気分のほうはいかがですか？$R;", "ネコマタ（藍）");

            Say(pc, 131, "……藍？$R;" +
            "$P……どうして、藍が？$R;", "ネコマタ（緑）");

            Say(pc, 0, 131, "緑姉さま、消えそうだったんだよ！$R;" +
            "ほんに、心配いたしました…….$R;", "ネコマタ（藍）");

            Say(pc, 131, "……あ、わた…し……。$R;" +
            "$P……。$R;" +
            "$Pお願い……確かめたいことがあるの。$R;" +
            "$P軍艦島に、連れて行って……。$R;", "ネコマタ（緑）");

            Say(pc, 0, 131, "えっ、緑姉さま、軍艦島って！？$R;", "ネコマタ（藍）");

            Say(pc, 131, "崖の…下……に、あるはずだから……。$R;", "ネコマタ（緑）");
            ShowEffect(pc, 12002102, 5015);
            ShowEffect(pc, 5013);
            Wait(pc, 330);

            NPCChangeView(pc, 12002102, 12002101);
            Wait(pc, 1980);

            Say(pc, 0, 131, "緑の三角巾に、何か宿ったようだ……。$R;", " ");

            Say(pc, 0, 131, "緑姉さま、緑姉さま！？$R;" +
            "$P……。$R;" +
            "$P……大丈夫。眠られただけのようです。$R;", "ネコマタ（藍）");
            ShowEffect(pc, 12002102, 5016);

            Say(pc, 0, 131, "緑のいた場所に何か落ちている……。$R;", " ");
            PlaySound(pc, 2040, false, 100, 50);
            GiveItem(pc, 10065200, 1);
            Say(pc, 0, 131, "『マナの守護』を入手した。$R;", " ");


        }
        void 山吹(ActorPC pc)
        {
            Say(pc, 0, 131, "緑姉さま！！！$R;" +
            "$Rよかった～～～～！！！！$R;" +
            "どう、苦しかったりせえへん？$R;", "ネコマタ（山吹）");

            Say(pc, 131, "……山吹？$R;" +
            "$P……どうして、山吹が？$R;", "ネコマタ（緑）");

            Say(pc, 0, 131, "緑姉やん、死にそうだったんだよ！$R;" +
            "だから、うち、もう心配で……。$R;", "ネコマタ（山吹）");

            Say(pc, 131, "……あ、わた…し……。$R;" +
            "$P……。$R;" +
            "$Pお願い……確かめたいことがあるの。$R;" +
            "$P軍艦島に、連れて行って……。$R;", "ネコマタ（緑）");

            Say(pc, 0, 131, "えっ、緑姉やん、軍艦島って！？$R;", "ネコマタ（山吹）");

            Say(pc, 131, "崖の…下……に、あるはずだから……。$R;", "ネコマタ（緑）");
            ShowEffect(pc, 12002102, 5015);
            ShowEffect(pc, 5013);
            Wait(pc, 330);

            NPCChangeView(pc, 12002102, 12002101);
            Wait(pc, 1980);

            Say(pc, 0, 131, "緑の三角巾に、何か宿ったようだ……。$R;", " ");

            Say(pc, 0, 131, "姉やん、緑姉やん！？$R;" +
            "$P……。$R;" +
            "$P……大丈夫。眠っただけのようや。$R;", "ネコマタ（山吹）");
            ShowEffect(pc, 12002102, 5016);

            Say(pc, 0, 131, "緑のいた場所に何か落ちている……。$R;", " ");
            PlaySound(pc, 2040, false, 100, 50);
            GiveItem(pc, 10065200, 1);
            Say(pc, 0, 131, "『マナの守護』を入手した。$R;", " ");


        }
        void 菫(ActorPC pc)
        {
            Say(pc, 0, 131, "緑！！！$R;" +
            "$Rよかった～～～～！！！！$R;" +
            "どう、どこか苦しいところはない？$R;", "ネコマタ（菫）");

            Say(pc, 131, "……菫、ねえさん？$R;" +
            "$P……どうして、菫ねえさんが？$R;", "ネコマタ（緑）");

            Say(pc, 0, 131, "緑、あなた消えそうだったのよ！？$R;" +
            "もうっ、本当によかった……。！$R;", "ネコマタ（菫）");

            Say(pc, 131, "……あ、わた…し……。$R;" +
            "$P……。$R;" +
            "$Pお願い……確かめたいことがあるの。$R;" +
            "$P軍艦島に、連れて行って……。$R;", "ネコマタ（緑）");

            Say(pc, 0, 131, "えっ、緑、軍艦島って！？$R;", "ネコマタ（菫）");

            Say(pc, 131, "崖の…下……に、あるはずだから……。$R;", "ネコマタ（緑）");
            ShowEffect(pc, 12002102, 5015);
            ShowEffect(pc, 5013);
            Wait(pc, 330);

            NPCChangeView(pc, 12002102, 12002101);
            Wait(pc, 1980);

            Say(pc, 0, 131, "緑の三角巾に、何か宿ったようだ……。$R;", " ");

            Say(pc, 0, 131, "緑、緑！？$R;" +
            "$P……。$R;" +
            "$P……大丈夫。眠っただけのようね。$R;", "ネコマタ（菫）");
            ShowEffect(pc, 12002102, 5016);

            Say(pc, 0, 131, "緑のいた場所に何か落ちている……。$R;", " ");
            PlaySound(pc, 2040, false, 100, 50);
            GiveItem(pc, 10065200, 1);
            Say(pc, 0, 131, "『マナの守護』を入手した。$R;", " ");


        }
        void 茜(ActorPC pc)
        {
            Say(pc, 0, 131, "緑姉さん！$R;" +
            "$Rよかった…….$R;" +
            "もう、大丈夫だからね……姉さん！！$R;", "ネコマタ（茜）");

            Say(pc, 131, "……茜？$R;" +
            "$P……どうして、茜が？$R;", "ネコマタ（緑）");

            Say(pc, 0, 131, "緑姉さん、死にそうだったのよ！$R;" +
            "だから、私、もう心配で……。$R;", "ネコマタ（茜）");

            Say(pc, 131, "……あ、わた…し……。$R;" +
            "$P……。$R;" +
            "$Pお願い……確かめたいことがあるの。$R;" +
            "$P軍艦島に、連れて行って……。$R;", "ネコマタ（緑）");

            Say(pc, 0, 131, "えっ、緑姉さん、軍艦島って！？$R;", "ネコマタ（茜）");

            Say(pc, 131, "崖の…下……に、あるはずだから……。$R;", "ネコマタ（緑）");
            ShowEffect(pc, 12002102, 5015);
            ShowEffect(pc, 5013);
            Wait(pc, 330);

            NPCChangeView(pc, 12002102, 12002101);
            Wait(pc, 1980);

            Say(pc, 0, 131, "緑の三角巾に、何か宿ったようだ……。$R;", " ");

            Say(pc, 0, 131, "姉さん、緑姉さん！？$R;" +
            "$P……。$R;" +
            "$P……大丈夫。眠っただけみたいね。$R;"+
            "$P　まったくもう、姉さんの……バカ！$R:"+
            "$P驚かせないでよ……。$R:", "ネコマタ（茜）");
            ShowEffect(pc, 12002102, 5016);

            Say(pc, 0, 131, "緑のいた場所に何か落ちている……。$R;", " ");
            PlaySound(pc, 2040, false, 100, 50);
            GiveItem(pc, 10065200, 1);
            Say(pc, 0, 131, "『マナの守護』を入手した。$R;", " ");


        }
        void 杏(ActorPC pc)
        {
            Say(pc, 0, 131, "緑お姉ちゃん！$R;" +
            "$Rよかった…….$R;" +
            "心配したんだよ！$R;", "ネコマタ（杏）");

            Say(pc, 131, "……杏？$R;" +
            "$P……どうして、杏が？$R;", "ネコマタ（緑）");

            Say(pc, 0, 131, "緑お姉ちゃん倒れてたんだよ…！$R;" +
            "だから…心配で……。$R;", "ネコマタ（杏）");

            Say(pc, 131, "……あ、わた…し……。$R;" +
            "$P……。$R;" +
            "$Pお願い……確かめたいことがあるの。$R;" +
            "$P軍艦島に、連れて行って……。$R;", "ネコマタ（緑）");

            Say(pc, 0, 131, "えっ、緑お姉ちゃん、軍艦島って！？$R;", "ネコマタ（杏）");

            Say(pc, 131, "崖の…下……に、あるはずだから……。$R;", "ネコマタ（緑）");
            ShowEffect(pc, 12002102, 5015);
            ShowEffect(pc, 5013);
            Wait(pc, 330);

            NPCChangeView(pc, 12002102, 12002101);
            Wait(pc, 1980);

            Say(pc, 0, 131, "緑の三角巾に、何か宿ったようだ……。$R;", " ");

            Say(pc, 0, 131, "お姉ちゃん、緑お姉ちゃん！？$R;" +
            "$P……。$R;" +
            "$P……大丈夫。眠ってるみたい。$R;", "ネコマタ（杏）");
            ShowEffect(pc, 12002102, 5016);

            Say(pc, 0, 131, "緑のいた場所に何か落ちている……。$R;", " ");
            PlaySound(pc, 2040, false, 100, 50);
            GiveItem(pc, 10065200, 1);
            Say(pc, 0, 131, "『マナの守護』を入手した。$R;", " ");


        }
        void 空(ActorPC pc)
        {
            Say(pc, 0, 131, "よかったー……。$R;" +
            "どうだ、苦しいところはないか？$R;", "ネコマタ（空）");

            Say(pc, 131, "……空ねえさん？$R;" +
            "$P……どうして、空ねえさんが？$R;", "ネコマタ（緑）");

            Say(pc, 0, 131, "おまえ、消えそうだったんだぞ！$R;" +
            "安心しろ、ずっとそばにいるから……。$R;", "ネコマタ（空）");

            Say(pc, 131, "……あ、わた…し……。$R;" +
            "$P……。$R;" +
            "$Pお願い……確かめたいことがあるの。$R;" +
            "$P軍艦島に、連れて行って……。$R;", "ネコマタ（緑）");

            Say(pc, 0, 131, "おい、緑、軍艦島って！？$R;", "ネコマタ（空）");

            Say(pc, 131, "崖の…下……に、あるはずだから……。$R;", "ネコマタ（緑）");
            ShowEffect(pc, 12002102, 5015);
            ShowEffect(pc, 5013);
            Wait(pc, 330);

            NPCChangeView(pc, 12002102, 12002101);
            Wait(pc, 1980);

            Say(pc, 0, 131, "緑の三角巾に、何か宿ったようだ……。$R;", " ");

            Say(pc, 0, 131, "緑、緑ってば！？$R;" +
            "$P……。$R;" +
            "$P……なんだ、寝ただけか。$R;" +
            "$Pびっくりしたなぁ……。$R:", "ネコマタ（空）");
            ShowEffect(pc, 12002102, 5016);

            Say(pc, 0, 131, "緑のいた場所に何か落ちている……。$R;", " ");
            PlaySound(pc, 2040, false, 100, 50);
            GiveItem(pc, 10065200, 1);
            Say(pc, 0, 131, "『マナの守護』を入手した。$R;", " ");


        }
        void 胡桃若菜(ActorPC pc)
        {
            Say(pc, 0, 131, "みどりおねえさん！！！。$R;" +
            "よかったー……。$R;"+
            "どうですか？苦しくないですか？$R:", "ネコマタ（胡桃）");

            Say(pc, 131, "胡桃、それに若菜?$R;" +
            "$P……どうして、２人がここに？$R;", "ネコマタ（緑）");

            Say(pc, 0, 131, "ん……、緑おねえちゃん$R;" +
            "もうすこしで。$R;"+
            "消えちゃうところだったんだって。$R:", "ネコマタ（若菜）");

            Say(pc, 131, "……あ、わた…し……。$R;" +
            "$P……。$R;" +
            "$Pお願い……確かめたいことがあるの。$R;" +
            "$P軍艦島に、連れて行って……。$R;", "ネコマタ（緑）");

            Say(pc, 0, 131, "えっ、緑おねえさん、軍艦島って！？$R;", "ネコマタ（胡桃）");

            Say(pc, 131, "崖の…下……に、あるはずだから……。$R;", "ネコマタ（緑）");
            ShowEffect(pc, 12002102, 5015);
            ShowEffect(pc, 5013);
            Wait(pc, 330);

            NPCChangeView(pc, 12002102, 12002101);
            Wait(pc, 1980);

            Say(pc, 0, 131, "緑の三角巾に、何か宿ったようだ……。$R;", " ");

            Say(pc, 0, 131, "あ～～ん！！！$R;" +
            "$Pせっかく、会えたのに、おねえさん！$R;" +
            "$Pひっく…ひっく……！！！$R;", "ネコマタ（胡桃）");
            Say(pc, 0, 131, "胡桃ちゃん……。$R:" +
             "おねえちゃん、おねんねしてるみたい。$R:" +
             "大丈夫……ね？$R:", "ネコマタ（若菜）");
            Say(pc, 0, 131, "あっ、ああああ……。。$R:" +
             "本当だ……。？$R:", "ネコマタ（胡桃）");
            ShowEffect(pc, 12002102, 5016);

            Say(pc, 0, 131, "緑のいた場所に何か落ちている……。$R;", " ");
            PlaySound(pc, 2040, false, 100, 50);
            GiveItem(pc, 10065200, 1);
            Say(pc, 0, 131, "『マナの守護』を入手した。$R;", " ");


        }

    }

}
            
            
        
     
    