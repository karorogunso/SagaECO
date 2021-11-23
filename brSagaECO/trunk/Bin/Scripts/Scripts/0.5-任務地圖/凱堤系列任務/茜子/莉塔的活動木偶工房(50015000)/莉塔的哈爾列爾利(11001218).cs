using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaDB.Item;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
namespace SagaScript.M50015000
{
    public class S11001218 : Event
    {
        public S11001218()
        {
            this.EventID = 11001218;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<Neko_05> Neko_05_amask = pc.AMask["Neko_05"];
            BitMask<Neko_05> Neko_05_cmask = pc.CMask["Neko_05"];
            
            if (Neko_05_amask.Test(Neko_05.茜子任务开始) &&
                Neko_05_cmask.Test(Neko_05.把電腦唯讀記憶體給哈爾列爾利) &&
                !Neko_05_cmask.Test(Neko_05.飛空庭完成))
            {
                Say(pc, 11001218, 131, "一起去我的飛空庭嗎?$R;");
                去哈爾列爾利的飛空庭(pc);
                return;
            }
            if (Neko_05_amask.Test(Neko_05.茜子任务开始) &&
                Neko_05_cmask.Test(Neko_05.进入光塔) &&
                !Neko_05_cmask.Test(Neko_05.把電腦唯讀記憶體給哈爾列爾利) &&
                CountItem(pc, 10057900) >= 1)
            {
                Wait(pc, 333);
                ShowEffect(pc, 11001218, 4131);
                TakeItem(pc, 10057900, 1);
                TakeItem(pc, 10057380, 1);
                Say(pc, 0, 131, "把『電腦唯讀記憶體』給哈爾列爾利了！$R;");
                Wait(pc, 1000);
                Neko_05_cmask.SetValue(Neko_05.把電腦唯讀記憶體給哈爾列爾利, true);
                Say(pc, 11001218, 131, "媽媽！！我回來了！！$R;");
                Say(pc, 11001219, 131, "回來了?$R;" +
                    "$R因為回來晚了~讓我擔心了呢…$R你去了光之塔?$R;" +
                    "$R瑪莎跟我說的$R;" +
                    "$P真的謝謝啊！$R;");
                Say(pc, 11001218, 131, "嗯！因為「客人」的幫忙$R『電腦唯讀記憶體』也找到了！$R;" +
                    "$R這次，我的飛空庭終於可以飛了♪$R;" +
                    "$P來吧！「客人」！！到我的飛空庭吧！！$R;" +
                    "$R來來！媽媽也一起吧！$R;");
                Say(pc, 11001219, 131, "嗨…哈爾列爾利啊…$R;" +
                    "$R有重要的話要跟你說……$R;");
                Say(pc, 11001218, 131, "在飛空庭說也是可以的阿♪$R;");
                去哈爾列爾利的飛空庭(pc);
                return;
            }
            if (Neko_05_amask.Test(Neko_05.茜子任务开始) &&
                Neko_05_cmask.Test(Neko_05.去哈爾列爾利的飛空庭) &&
                !Neko_05_cmask.Test(Neko_05.尋找瑪莎的蹤跡))
            {
                判断所携带猫灵2(pc);
                return;
            }
            if (Neko_05_amask.Test(Neko_05.茜子任务开始) &&
                Neko_05_cmask.Test(Neko_05.收到碎紙) &&
                !Neko_05_cmask.Test(Neko_05.去哈爾列爾利的飛空庭))
            {
                判断所携带猫灵1(pc);
                return;
            }
            if (!Neko_05_amask.Test(Neko_05.茜子任务结束) &&
                Neko_05_cmask.Test(Neko_05.开始交谈) &&
                !Neko_05_cmask.Test(Neko_05.开始指导))
            {
                Say(pc, 11001218, 131, "……?$R;" +
                    "$R可以把我帶到有飛空庭工匠的$R地方嗎?$R;");
                指導哈爾列爾利(pc);
                return;
            }
            if (pc.Level > 29 && pc.Fame > 19 &&
                !Neko_05_amask.Test(Neko_05.茜子任务结束) &&
                !Neko_05_cmask.Test(Neko_05.开始交谈))
            {
                Say(pc, 11001218, 131, "還有……$R;" +
                    "$R壓力機的頂篷部位好像有問題$R;" +
                    "$P因為承受不了高壓$R回轉軸的支撐帶損傷很快$R;" +
                    "$R怎麼辦才好啊?媽媽?$R;");
                Say(pc, 11001219, 131, "哈爾列爾利！聽好$R;" +
                    "$R這位媽媽是木偶制作大師$R不是飛空庭的技師！$R;");
                switch (Select(pc, "怎麼了嗎?", "", "交談", "放棄"))
                {
                    case 1:
                        Neko_05_cmask.SetValue(Neko_05.开始交谈, true);
                        Say(pc, 11001219, 131, "不好意思…解釋的有點晚了！$R;" +
                            "$R這孩子是石像「哈爾列爾利」$R一直夢想著自己作為活動木偶石像$R有天也可以成為飛空庭工匠$R;" +
                            "$P這孩子決定要靠自己的雙手$R製造出飛空庭引擎…$R;" +
                            "$R唐卡這裡到處都是修理好的探索引擎$R還是這樣固執$R;");
                        Say(pc, 11001218, 131, "我說過啊！$R我想用自己做的飛空庭引擎$R在天空中翱翔傲視大地！！$R;" +
                            "$P……?$R;" +
                            "$R這位是媽媽的朋友嗎?$R;");
                        Say(pc, 11001219, 131, "不是！是來找媽媽的客人$R;" +
                            "$R媽媽現在要幫客人工作$R不能淘氣唷！$R;");
                        Say(pc, 11001218, 131, "那…$R想請問這位「客人」一下♪$R;" +
                            "$R外部引擎的活塞軸心過份磨損時…$R;");
                        Say(pc, 11001219, 131, "哈爾列爾利！夠了！$R不能問客人這些！會打擾人家的$R;" +
                            "$R關於飛空庭的消息$R要問飛空庭的…工匠啊！$R;" +
                            "$P不好意思$R;" +
                            "$R…這孩子不是故意的$R只是太喜愛飛空庭了，所以…$R;");
                        Say(pc, 11001218, 131, "那可以請「客人」…$R把我帶到飛空庭工匠那嗎?$R;");
                        Say(pc, 11001219, 131, "哈爾列爾利！夠了！$R;" +
                            "$R客人怎麼會答應無理的請求啊！$R;");
                        指導哈爾列爾利(pc);
                        break;
                    case 2:
                        break;
                }
                return;
            }
            if (Neko_05_amask.Test(Neko_05.茜子任务开始) &&
                !Neko_05_cmask.Test(Neko_05.开始指导) &&
                !Neko_05_cmask.Test(Neko_05.去哈爾列爾利的飛空庭))
            {
                Say(pc, 11001218, 111, "……$R;");
                Say(pc, 0, 131, "沒有回答…$R;" +
                    "$R睡著了…?$R;");
                Say(pc, 0, 111, "那是我的面具$R;" +
                    "$R我現在在「客人」的口袋裡呢$R快進來一起玩吧♪$R;", "行李裡的哈爾列爾利");
                Say(pc, 11001219, 131, "真的！不要嚇我！$R;");
                指導哈爾列爾利(pc);
                return;
            }
            Say(pc, 11001218, 131, "媽媽！製造飛空庭好累喔$R;");
        }

        void 指導哈爾列爾利(ActorPC pc)
        {
            BitMask<Neko_05> Neko_05_amask = pc.AMask["Neko_05"];
            BitMask<Neko_05> Neko_05_cmask = pc.CMask["Neko_05"];
            switch (Select(pc, "要指導哈爾列爾利嗎?", "", "給他指導", "放棄"))
            {
                case 1:
                    Say(pc, 11001219, 131, "哎呀…真的嗎?$R;" +
                        "$R關於飛空庭的消息…$R要問唐卡西北部飛空庭工廠的$R工匠比較好$R;" +
                        "$P請送哈爾列爾利到那個地方吧…$R;" +
                        "$R真的很感謝您…$R;" +
                        "$P哈爾列爾利！你要向客人道謝唷！$R;" +
                        "$R不要給客人帶來麻煩$R變成道具一起去吧！$R;");
                    Say(pc, 11001218, 131, "啊！啊啊！$R謝謝您啊！「客人」♪$R;" +
                        "$R那我就變成道具吧！$R;");
                    if (CheckInventory(pc, 10057380, 1))
                    {
                        Wait(pc, 1000);
                        ShowEffect(pc, 11001218, 4131);
                        Wait(pc, 1000);
                        PlaySound(pc, 2040, false, 100, 50);
                        GiveItem(pc, 10057380, 1);
                        Neko_05_cmask.SetValue(Neko_05.开始指导, true);
                        Neko_05_amask.SetValue(Neko_05.茜子任务开始, true);
                        Say(pc, 0, 131, "收到了『莉塔的哈爾列爾利（活動）』！$R;");
                        Say(pc, 11001218, 111, "那就拜託您了♪$R;");
                        return;
                    }
                    Say(pc, 11001218, 131, "……$R;" +
                        "$R「客人」的行李滿了…$R;");
                    break;
                case 2:
                    break;
            }
        }

        void 判断所携带猫灵1(ActorPC pc)
        {
            BitMask<Neko_05> Neko_05_amask = pc.AMask["Neko_05"];
            BitMask<Neko_05> Neko_05_cmask = pc.CMask["Neko_05"];
            Wait(pc, 1000);
            ShowEffect(pc, 11001218, 4131);
            Wait(pc, 1000);
            Neko_05_cmask.SetValue(Neko_05.去哈爾列爾利的飛空庭, true);
            Say(pc, 11001218, 131, "媽！我回來囉！$R;");
            Say(pc, 11001219, 131, "哎呀！…回來啦?$R;" +
                "$R這麼晚了!我好擔心呢…$R去到奧克魯尼亞大陸了?$R;" +
                "$P真的…非常謝謝您…$R;");
            Say(pc, 11001218, 131, "嗯！！都是托「客人」福！！$R;" +
                "$P「客人」帶我去了奧克魯尼亞大陸$R我終於可以製造很好的飛空庭啊♪$R;" +
                "$R多謝您了！「客人」！$R;");
            if (pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.PET))
            {
                if (pc.Inventory.Equipments[EnumEquipSlot.PET].ItemID == 10017905)
                {
                    Say(pc, 0, 131, "啊啊！我忍不下去了！！$R;" +
                        "$R主人的名字不是「客人」！！$R;" +
                        pc.Name + "!!!$R;", "\"凱堤(山吹)\"");
                    判断猫灵携带3(pc);
                    return;
                }
                if (pc.Inventory.Equipments[EnumEquipSlot.PET].ItemID == 10017900)
                {
                    Say(pc, 0, 131, "啊啊！我忍不下去了！！$R;" +
                        "$R主人的名字不是「客人」！！$R;" +
                        pc.Name + "!!!!$R;", "凱堤（桃）");
                    判断猫灵携带3(pc);
                    return;
                }
                if (pc.Inventory.Equipments[EnumEquipSlot.PET].ItemID == 10017906)
                {
                    Say(pc, 0, 131, "哈爾列爾利一直弄錯喔$R;" +
                        "$R主人的名字不是「客人」！！$R;" +
                        pc.Name + "!$R;", "凱堤(菫)");
                    判断猫灵携带3(pc);
                    return;
                }
                if (pc.Inventory.Equipments[EnumEquipSlot.PET].ItemID == 10017903)
                {
                    Say(pc, 0, 131, "哈爾列爾利先生！名字錯了！$R;" +
                        "$R主人的名字不是「客人」！！$R;" +
                        pc.Name + "!!$R;", "凱堤(雜梅)");
                    判断猫灵携带3(pc);
                    return;
                }
                if (pc.Inventory.Equipments[EnumEquipSlot.PET].ItemID == 10017902)
                {
                    Say(pc, 0, 131, "不對…！我們主人的名字是……$R;" +
                        pc.Name + "!!!$R;", "凱堤(緑)");
                    判断猫灵携带3(pc);
                    return;
                }
            }
            //*/
        }
        void 判断所携带猫灵2(ActorPC pc)
        {
            Say(pc, 11001218, 131, "啊啊！是「客人」！！$R;" +
                "$R上次真的很感謝您♪$R;");
            if (pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.PET))
            {
                if (pc.Inventory.Equipments[EnumEquipSlot.PET].ItemID == 10017905)
                {
                    Say(pc, 0, 131, "啊啊！我忍不下去了！！$R;" +
                        "$R主人的名字不是「客人」！！$R;" +
                        pc.Name + "!!!$R;", "\"凱堤(山吹)\"");
                    判断猫灵携带3(pc);
                    return;
                }
                if (pc.Inventory.Equipments[EnumEquipSlot.PET].ItemID == 10017900)
                {
                    Say(pc, 0, 131, "啊啊！我忍不下去了！！$R;" +
                        "$R主人的名字不是「客人」！！$R;" +
                        pc.Name + "!!!!$R;", "凱堤（桃）");
                    判断猫灵携带3(pc);
                    return;
                }
                if (pc.Inventory.Equipments[EnumEquipSlot.PET].ItemID == 10017906)
                {
                    Say(pc, 0, 131, "哈爾列爾利一直弄錯喔$R;" +
                        "$R主人的名字不是「客人」！！$R;" +
                        pc.Name + "!$R;", "凱堤(菫)");
                    判断猫灵携带3(pc);
                    return;
                }
                if (pc.Inventory.Equipments[EnumEquipSlot.PET].ItemID == 10017903)
                {
                    Say(pc, 0, 131, "哈爾列爾利先生！名字錯了！$R;" +
                        "$R主人的名字不是「客人」！！$R;" +
                        pc.Name + "!!$R;", "凱堤(雜梅)");
                    判断猫灵携带3(pc);
                    return;
                }
                if (pc.Inventory.Equipments[EnumEquipSlot.PET].ItemID == 10017902)
                {
                    Say(pc, 0, 131, "不對…！我們主人的名字是……$R;" +
                        pc.Name + "!!!$R;", "凱堤(緑)");
                    判断猫灵携带3(pc);
                    return;
                }
            }
        }

        void 判断猫灵携带3(ActorPC pc)
        {
            BitMask<Neko_05> Neko_05_amask = pc.AMask["Neko_05"];
            BitMask<Neko_05> Neko_05_cmask = pc.CMask["Neko_05"];
            Say(pc, 11001218, 131, "凱堤說話了?$R;" +
                "$R啊！對了！$R「客人」！要不要到我的飛空庭看看阿?$R;" +
                "$P我會特別招待您的！$R凱堤(茜)也在等你唷！$R;");
            if (CountItem(pc, 10017900) >= 1)
            {
                Say(pc, 0, 131, "什麼?茜??$R;", "凱堤（桃）");
            }
            if (CountItem(pc, 10017906) >= 1)
            {
                Say(pc, 0, 131, "等一下！！$R你剛剛是說「茜」…?$R;", "凱堤(菫)");
            }
            if (CountItem(pc, 10017903) >= 1)
            {
                Say(pc, 0, 131, "茜…?$R;", "凱堤(雜梅)");
            }
            if (CountItem(pc, 10017902) >= 1)
            {
                Say(pc, 0, 131, "…茜…$R;", "凱堤(緑)");
            }
            if (CountItem(pc, 10017905) >= 1)
            {
                Say(pc, 0, 131, "什麼…?誰??$R;", "凱堤(山吹)");
            }
            if (CountItem(pc, 10017900) >= 1)
            {
                Say(pc, 0, 131, "茜！是茜！$R;", "凱堤（桃）");
            }
            if (CountItem(pc, 10017906) >= 1)
            {
                Say(pc, 0, 131, "哈爾列爾利的飛空庭裡有茜！$R;" +
                    "$R啊啊！這都多久以前的事了！！$R;", "凱堤(菫)");
            }
            if (CountItem(pc, 10017905) >= 1)
            {
                Say(pc, 0, 131, "…?茜?它是誰啊?$R;" +
                    "$R我不認識……$R;", "凱堤(山吹)");
            }
            if (CountItem(pc, 10017903) >= 1)
            {
                Say(pc, 0, 131, "茜……?$R;" +
                    "$R是我們姊妹之一嗎?$R;", "凱堤(雜梅)");
            }
            if (CountItem(pc, 10017906) >= 1)
            {
                Say(pc, 0, 131, "茜很小的時候就跟我們分開了$R茜所有的事都不記得了$R;" +
                    "$R蘋果你還記得嗎?$R;", "凱堤(菫)");
            }
            if (CountItem(pc, 10017900) >= 1)
            {
                Say(pc, 0, 131, "是啊！你不記得了嗎?$R茜是我們的姊妹♪$R;" +
                    "$R是個安靜柔弱，容易寂寞的孩子$R;", "凱堤（桃）");
            }
            if (CountItem(pc, 10017906) >= 1)
            {
                Say(pc, 0, 131, "茜……在飛空庭裡自己待著嗎?$R;" +
                    "$R有點擔心阿…可能因為寂寞正在哭呢$R;", "凱堤(菫)");
            }
            switch (Select(pc, "要去哈爾列爾利的飛空庭嗎?", "", "去", "不去"))
            {
                case 1:
                    Say(pc, 11001218, 131, "來~出發了！$R;" +
                        "$R都準備好了?$R;");
                    switch (Select(pc, "要去哈爾列爾利的飛空庭嗎?", "", "去", "不去"))
                    {
                        case 1:
                            pc.CInt["Neko_05_Map_02"] = CreateMapInstance(50018000, 10062000, 110, 88);
                            Warp(pc, (uint)pc.CInt["Neko_05_Map_02"], 7, 12);
                            /*
                            //EVENTMAP_IN 18 1 7 12 4
                            if(//ME.WORK0 = -1
                            )
                            {
            Say(pc, 11001218, 131, "什麼?…呼叫不了飛空庭?$R;" +
                "$R稍等一下再呼叫看看?$R;");
                                return;
                            }
                            //EVENTEND*/
                            break;
                        case 2:
                            Say(pc, 11001218, 131, "嗯?不去?$R;" +
                                "$R那想過來的時候再來吧！$R;");
                            break;
                    }
                    break;
                case 2:
                    Say(pc, 11001218, 131, "嗯?不去?$R;" +
                        "$R那想過來的時候再來吧！$R;");
                    break;
            }//*/
        }
        void 去哈爾列爾利的飛空庭(ActorPC pc)
        {

            switch (Select(pc, "要去哈爾列爾利的飛空庭嗎?", "", "去", "不去"))
            {
                case 1:
                    //EVENTMAP_IN 19 1 7 12 4
                    /*
                    if(//ME.WORK0 = -1
                    )
                    {
            Say(pc, 11001218, 131, "什麼?…呼叫不了飛空庭?$R;" +
                "$R稍等一下再呼叫看看?$R;");
                        return;
                    }
                    //*/
                    break;
                case 2:
                    Say(pc, 11001218, 131, "嗯?那麼想去的時候再來吧！$R;");
                    break;
            }
        }
    }
}
