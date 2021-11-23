using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
using SagaDB.Item;

using SagaLib;
using SagaScript.Chinese.Enums;
//所在地圖:賢者首領之家(30022000) NPC基本信息:賢者首領(11000044) X:3 Y:1
namespace SagaScript.M30022000
{
    public class S11000044 : Event
    {
        public S11000044()
        {
            this.EventID = 11000044;

            //任務服務台相關設定
            this.questTransportSource = "來了嗎?!$R;" +
                                        "你能幫我把這個東西轉交給別人嗎?$R;" +
                                        "$R不好意思，$R;" +
                                        "因為實在是太忙了，$R;" +
                                        "所以沒時間親自拿過去。$R;" +
                                        "$P提醒你一下，$R;" +
                                        "這是非常重要的道具，$R;" +
                                        "小心點不要把它弄壞唷!$R;";

            this.transport = "轉交人是我從前的弟子。$R;" +
                                             "$R聽說現在居住在「諾頓」那裡。$R;" +
                                             "$P她是個十分優秀的小子，$R;" +
                                             "但是說什麼要繼承父業，$R;" +
                                             "成為一名優秀的政治家。$R;" +
                                             "$R還真是浪費了她優秀的資質。$R;";

            this.questTransportCompleteSrc = "轉交給對方了嗎?$R;" +
                                             "實在是太感謝了!!$R;" +
                                             "$R報酬請到「任務服務台」領取吧!;";
        }

        public override void OnEvent(ActorPC pc)
        {                                                                    //任務：賢者首領的請求

            if (pc.Job != PC_JOB.NOVICE &&
                pc.Job != PC_JOB.SWORDMAN &&
                pc.Job != PC_JOB.FENCER &&
                pc.Job != PC_JOB.SCOUT &&
                pc.Job != PC_JOB.ARCHER &&
                pc.Job != PC_JOB.WIZARD &&
                pc.Job != PC_JOB.SHAMAN &&
                pc.Job != PC_JOB.VATES &&
                pc.Job != PC_JOB.WARLOCK &&
                pc.Job != PC_JOB.TATARABE &&
                pc.Job != PC_JOB.FARMASIST &&
                pc.Job != PC_JOB.RANGER &&
                pc.Job != PC_JOB.MERCHANT &&
                CountItem(pc, 10053800) > 0)
            {
                Say(pc, 131, "$R您好像拿著『禁書』呢$R;" +
                    "$R想轉職嗎？$R;");
                轉職(pc);
                return;
            }
            正常對話(pc);
        }

        void 正常對話(ActorPC pc)
        {
            BitMask<Magic_Book> Magic_Book_mask = new BitMask<Magic_Book>(pc.CMask["Magic_Book"]);

            if (!Magic_Book_mask.Test(Magic_Book.賢者首領給予知識之書) ||
                !Magic_Book_mask.Test(Magic_Book.賢者首領給予魔法之書))
            {
                賢者首領的請求(pc);
                return;
            }

            switch (Select(pc, "想要做什麼事情啊?", "", "看右側書櫃", "看左側書櫃", "看書櫃上面", "『秘方書』是什麼?", "什麼也不做"))
            {
                case 1:
                    Say(pc, 11000044, 131, "好的，自己過去看看吧!$R;" +
                                           "$R我很忙的，$R;" +
                                           "請不要再來打擾我。$R;", "賢者首領");

                    OpenShopBuy(pc, 102);
                    break;

                case 2:
                    Say(pc, 11000044, 131, "好的，自己過去看看吧!$R;" +
                                           "$R我很忙的，$R;" +
                                           "請不要再來打擾我。$R;", "賢者首領");

                    OpenShopBuy(pc, 103);
                    break;

                case 3:
                    Say(pc, 11000044, 131, "你這小子…倒懂人情世固…$R;" +
                                           "$R明白了!$R;" +
                                           "不賣你怎麼可以呢?$R;" +
                                           "除非你不認同我說的話!$R;", "賢者首領");

                    OpenShopBuy(pc, 187);
                    break;

                case 4:
                    Say(pc, 11000044, 131, "呵呵…$R;" +
                                           "對『秘方書』有興趣啊?$R;" +
                                           "$P這是記錄「合成秘方」的『秘方收集本』。$R;" +
                                           "$R是按照技能類別分著的書，$R;" +
                                           "購買有興趣的技能書也不錯喔!$R;" +
                                           "$P就算沒有技能，$R;" +
                                           "技能書中也記載了一些基本的秘方。$R;" +
                                           "$P剛開始只能看到很少的秘方，$R;" +
                                           "多做幾次合成的話，$R;" +
                                           "能看到的秘方就會慢慢增加。$R;" +
                                           "$P啊…是啊!!$R;" +
                                           "最後甚至會出現，$R;" +
                                           "文獻中沒記載的隱藏秘方呢!!" +
                                           "$R那試試看也沒壞處啊!$R;" +
                                           "$R努力的做合成試試看吧！$R;", "賢者首領");
                    break;

                case 5:
                    Say(pc, 11000044, 131, "幹嘛? 別妨礙我休息啊!$R;" +
                                           "這副身體很累耶!$R;", "賢者首領");
                    break;
            }
        }

        void 賢者首領的請求(ActorPC pc)
        {
            BitMask<Magic_Book> Magic_Book_mask = new BitMask<Magic_Book>(pc.CMask["Magic_Book"]);                                                                      //任務：賢者首領的請求

            switch (Select(pc, "想要做什麼事情啊?", "", "看右側書櫃", "看左側書櫃", "看書櫃上面", "『秘方書』是什麼?", "講故事", "什麼也不做"))
            {
                case 1:
                    Say(pc, 11000044, 131, "好的，自己過去看看吧!$R;" +
                                           "$R我很忙的，$R;" +
                                           "請不要再來打擾我。$R;", "賢者首領");

                    OpenShopBuy(pc, 102);
                    break;

                case 2:
                    Say(pc, 11000044, 131, "好的，自己過去看看吧!$R;" +
                                           "$R我很忙的，$R;" +
                                           "請不要再來打擾我。$R;", "賢者首領");

                    OpenShopBuy(pc, 103);
                    break;

                case 3:
                    Say(pc, 11000044, 131, "你這小子…倒懂人情世固…$R;" +
                                           "$R明白了!$R;" +
                                           "不賣你怎麼可以呢?$R;" +
                                           "除非你不認同我說的話!$R;", "賢者首領");

                    OpenShopBuy(pc, 187);
                    break;

                case 4:
                    Say(pc, 11000044, 131, "呵呵…$R;" +
                                           "對『秘方書』有興趣啊?$R;" +
                                           "$P這是記錄「合成秘方」的『秘方收集本』。$R;" +
                                           "$R是按照技能類別分著的書，$R;" +
                                           "購買有興趣的技能書也不錯喔!$R;" +
                                           "$P就算沒有技能，$R;" +
                                           "技能書中也記載了一些基本的秘方。$R;" +
                                           "$P剛開始只能看到很少的秘方，$R;" +
                                           "多做幾次合成的話，$R;" +
                                           "能看到的秘方就會慢慢增加。$R;" +
                                           "$P啊…是啊!!$R;" +
                                           "最後甚至會出現，$R;" +
                                           "文獻中沒記載的隱藏秘方呢!!" +
                                           "$R那試試看也沒壞處啊!$R;" +
                                           "$R努力的做合成試試看吧！$R;", "賢者首領");
                    break;

                case 5:
                    if (!Magic_Book_mask.Test(Magic_Book.賢者首領給予魔法之書) &&
                        pc.Level >= 25)
                    {
                        賢者首領給予魔法之書(pc);
                        return;
                    }

                    if (!Magic_Book_mask.Test(Magic_Book.賢者首領給予知識之書) &&
                        pc.Level >= 15)
                    {
                        賢者首領給予知識之書(pc);
                        return;
                    }

                    Say(pc, 11000044, 131, "幹嘛? 別妨礙我休息啊!$R;" +
                                           "這副身體很累耶!$R;", "賢者首領");
                    break;

                case 6:
                    Say(pc, 11000044, 131, "幹嘛? 別妨礙我休息啊!$R;" +
                                           "這副身體很累耶!$R;", "賢者首領");
                    break;
            }
        }

        void 賢者首領給予知識之書(ActorPC pc)
        {
            BitMask<Magic_Book> Magic_Book_mask = new BitMask<Magic_Book>(pc.CMask["Magic_Book"]);                                                                      //任務：賢者首領的請求

            Say(pc, 11000044, 131, "大事不妙!! 『紋章紙』都沒了啊…$R;" +
                                   "$P怎麼辦?$R;" +
                                   "$P如果拿100張『紋章紙』過來給我，$R;" +
                                   "我會用我寫的『知識之書』跟你交換的。$R;", "賢者首領");

            if (CountItem(pc, 10024900) >= 100)
            {
                switch (Select(pc, "想不想交換『知識之書』呢?", "", "交換", "不交換"))
                {
                    case 1:
                        Magic_Book_mask.SetValue(Magic_Book.賢者首領給予知識之書, true);

                        TakeItem(pc, 10024900, 100);
                        GiveItem(pc, 60080000, 1);
                        PlaySound(pc, 2030, false, 100, 50);
                        Say(pc, 0, 0, "得到『知識之書』!$R;", " ");

                        Say(pc, 11000044, 131, "好啊好啊…$R;" +
                                               "$R我很忙的啊! 不要再來妨礙了。$R;", "賢者首領");
                        break;

                    case 2:
                        break;
                }
            }
        }

        void 賢者首領給予魔法之書(ActorPC pc)
        {
            BitMask<Magic_Book> Magic_Book_mask = new BitMask<Magic_Book>(pc.CMask["Magic_Book"]);                                                                      //任務：賢者首領的請求

            Say(pc, 11000044, 131, "大事不妙!! 『紋章紙』都沒了啊…$R;" +
                                   "$P怎麼辦?$R;" +
                                   "$P如果拿200張『紋章紙』過來給我，$R;" +
                                   "我會用我寫的『魔法之書』跟你交換的。$R;", "賢者首領");

            if (CountItem(pc, 10024900) >= 200)
            {
                switch (Select(pc, "想不想交換『魔法之書』?", "", "交換", "不交換"))
                {
                    case 1:
                        Magic_Book_mask.SetValue(Magic_Book.賢者首領給予魔法之書, true);

                        TakeItem(pc, 10024900, 200);
                        GiveItem(pc, 60080100, 1);
                        PlaySound(pc, 2030, false, 100, 50);
                        Say(pc, 0, 0, "得到『魔法之書』!$R;", " ");

                        Say(pc, 11000044, 131, "好啊好啊…$R;" +
                                               "$R我很忙的啊! 不要再來妨礙了。$R;", "賢者首領");
                        break;

                    case 2:
                        break;
                }
            }
        }

        void 轉職(ActorPC pc)
        {
            BitMask<Technique> Technique_mask = pc.CMask["Technique"];

            switch (Select(pc, "轉職嗎？", "", "轉職", "聽聽説明", "轉職時的注意要點", "算了"))
            {
                case 1:
                    if (pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.UPPER_BODY))
                    {
                        if (pc.Inventory.Equipments[EnumEquipSlot.UPPER_BODY].ItemID == 50000063)
                        {
                            開始(pc);
                            return;
                        }
                    }
                    if (pc.Inventory.Equipments.Count == 0)
                    {
                        開始(pc);
                        return;
                    }
                    Say(pc, 131, "$R身上罩著什麼東西就不能轉職了。$R;" +
                        "$R先把身上的脱掉$R;");
                    if (Technique_mask.Test(Technique.給予轉職服裝))
                    {
                        Say(pc, 131, "$R啊，應該給您『轉職服裝』了啊。$R;" +
                            "$R換上以後再和我説話。$R;");
                        return;
                    }
                    if (CountItem(pc, 50000063) >= 1)
                    {
                        Say(pc, 131, "$R您拿著的不是『轉職服裝』嗎？$R;" +
                            "$R換上以後再和我説話。$R;");
                        return;
                    }
                    switch (Select(pc, "脱嗎？", "", "脱吧", "不要"))
                    {
                        case 1:
                            Say(pc, 131, "脱完了再和我説話$R;");
                            break;
                        case 2:
                            Say(pc, 131, "我已經是上了年紀的老人了。$R;" +
                                "$R到底有什麼害羞的？$R;" +
                                "$P知道了啦，給您這個吧$R;");
                            if (CheckInventory(pc, 50000063, 1))
                            {
                                Technique_mask.SetValue(Technique.給予轉職服裝, true);
                                //_6A81 = true;
                                GiveItem(pc, 50000063, 1);
                                Say(pc, 131, "得到『轉職用衣服』。$R;");
                                Say(pc, 131, "$R穿著這個的話就可以直接轉職了。$R;" +
                                    "$R穿上以後再來和我説話吧。$R;");
                                return;
                            }
                            Say(pc, 131, "行李太多了，$R整理完了再來找我$R;");
                            break;
                    }
                    break;
                case 2:
                    Technique_mask.SetValue(Technique.聽取說明, true);
                    //_6A76 = true;
                    Say(pc, 131, "「職業變更」是指$R;" +
                        "使用『禁書』的能力$R將封印的職業能力喚醒$R;" +
                        "$R因為您們原來的『職業』上有$R藴藏著更多樣的能力。$R;" +
                        "$P『禁書』擁有喚醒您們職業$R潛在力量的能力。$R;" +
                        "$P這世上的某個地方$R一定能找到記憶的漏斗。$R;" +
                        "$R它可以喚醒您心裡和身體裡$R的潛在能力$R;" +
                        "$P如果想恢復往日的經驗的話，$R;" +
                        "就使用『記憶的漏斗』吧，$R;" +
                        "使用越多，職業級數的減退就越少。$R;" +
                        "$P如果想恢復現在的經驗$R就選擇要維持的技能吧。$R;" +
                        "$P現在該知道那本書的真正意義了吧$R;" +
                        "$R只要打開那本書就可以轉職了。$R;");
                    轉職(pc);
                    break;
                case 3:
                    Say(pc, 131, "$R變更「職業」$R要注意兩件事情。$R;" +
                        "$P第一，在「轉職」的時候$R一定要選擇預設「保存技能」$R;" +
                        "$P「保存技能」是在「轉職」以後$R繼續維持原有技能的「技能」$R;" +
                        "$R由「技術職業」轉職到「專門職業」$R;" +
                        "或是由「技術職業」轉職到「專門職業」$R都可以帶技能過去$R;" +
                        "$P可以每10級職業LV才可以使用一個$R;" +
                        "$R所以，若想「職業」變更的話，$R最好是在「等級」是10倍數的時候做$R;" +
                        "$P「保存技能」可以在「職業變更窗」的$R轉移技能中選取$R;" +
                        "$P不要在短時間內不停「變更職業」喔$R;" +
                        "$P還有一點要注意的$R;" +
                        "$R變更職業時，$R未達100%的「職業LV」$R經驗值會失去$R;" +
                        "$P即使用「記憶的漏斗」也無法找回$R;" +
                        "$P剛升級時才「職業變更」的話，$R損失的經驗值最少$R;");
                    轉職(pc);
                    break;
                case 4:
                    正常對話(pc);
                    break;
            }
        }

        void 開始(ActorPC pc)
        {
            /*
            if (pc.Job == PC_JOB.TRADER && !_7a17)
            {
                _7a19 = true;
                Say(pc, 131, "$R拜金使是管錢的，$R所以行會相當嚴格呢。$R;" +
                    "$R以我的力量也不能轉職。$R;" +
                    "$P如果一定想轉職的話$R就找商人總管談談吧。$R;");
                return;
            }
            */
            Say(pc, 131, "$R如果想恢復往日的經驗的話，$R就使用『記憶的漏斗』吧，$R使用越多，$R職業級數的減退就越少。$R;" +
                "$R過去和現在聯系在一起$R才是真正的成長呢。$R;");

            if (JobSwitch(pc))
            {
                /*
                if (!_2b45)
                {
                    _2b45 = true;
                    PlaySound(pc, 3087, false, 100, 50);
                    ShowEffect(pc, 4131);
                    Wait(pc, 4000);
                    PlaySound(pc, 4012, false, 100, 50);
                    Say(pc, 131, "轉職$R;");
                    Say(pc, 131, "下次開始$R就主動準備『禁書』吧。$R;");
                    return;
                }
                */
                PlaySound(pc, 3087, false, 100, 50);
                ShowEffect(pc, 4131);
                Wait(pc, 4000);
                PlaySound(pc, 4012, false, 100, 50);
                Say(pc, 131, "給了『禁書』。$R;");
                Say(pc, 131, "變更了職業。$R;");
                TakeItem(pc, 10053800, 1);
            }
        }
    }
}