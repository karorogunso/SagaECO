using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaDB.Item;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
namespace SagaScript.M50016000
{
    public class S11001234 : Event
    {
        public S11001234()
        {
            this.EventID = 11001234;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<Neko_05> Neko_05_amask = pc.AMask["Neko_05"];
            BitMask<Neko_05> Neko_05_cmask = pc.CMask["Neko_05"];

            if (Neko_05_amask.Test(Neko_05.茜子任务开始) && Neko_05_cmask.Test(Neko_05.飛空庭完成) && !Neko_05_cmask.Test(Neko_05.得到茜子))
            {
                if (pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.PET))
                {
                    if (pc.Inventory.Equipments[EnumEquipSlot.PET].ItemID == 10017900)
                    {
                        对话1(pc);
                        return;
                    }
                    if (pc.Inventory.Equipments[EnumEquipSlot.PET].ItemID == 10017906)
                    {
                        对话2(pc);
                        return;
                    }
                    if (pc.Inventory.Equipments[EnumEquipSlot.PET].ItemID == 10017902)
                    {
                        对话3(pc);
                        return;
                    }
                    if (pc.Inventory.Equipments[EnumEquipSlot.PET].ItemID == 10017903)
                    {
                        对话4(pc);
                        return;
                    }
                    if (pc.Inventory.Equipments[EnumEquipSlot.PET].ItemID == 10017905)
                    {
                        对话5(pc);
                        return;
                    }
                }
                if (CountItem(pc, 10017900) >= 1)
                {
                    对话1(pc);
                    return;
                }
                if (CountItem(pc, 10017906) >= 1)
                {
                    对话2(pc);
                    return;
                }
                if (CountItem(pc, 10017902) >= 1)
                {
                    对话3(pc);
                    return;
                }
                if (CountItem(pc, 10017903) >= 1)
                {
                    对话4(pc);
                    return;
                }
                if (CountItem(pc, 10017905) >= 1)
                {
                    对话5(pc);
                    return;
                }
            }
            Say(pc, 11001234, 131, "茜…還有其他的凱堤…$R;" +
                "$R不要太責怪…哈爾列爾利了$R;");

        }

        void 对话1(ActorPC pc)
        {
            Say(pc, 11001234, 131, "茜…$R;");
            Say(pc, 0, 131, "啊…$R茜…?$R;" +
                "$R別再哭了…跟我們在一起吧$R;" +
                "$P我們主人他一定會…$R;" +
                "$R一定會帶我們到哈爾列爾利去的$R「通往天空的塔」的$R;", "凱堤（蘋果)");
            Say(pc, 0, 131, "嗚嗚…$R;" +
                "$R沒辦法啊…嗚嗚…$R現在…嗚嗚…沒有…$R其他方法…嗚嗚…$R姐姐們…嗚嗚…會在身邊的…$R;" +
                "$P不要搞錯了…！$R這是為了回到哈爾列爾利的身邊！！$R;" +
                "$R不是因為孤單！！！$R;", "凱堤(茜)");
            Say(pc, 0, 131, "嗚嗚…嗯…知道了！$R;" +
                "$R不要擔心了！茜$R;", "凱堤（桃）");
            对话6(pc); 
        }
        void 对话2(ActorPC pc)
        {
            Say(pc, 11001234, 131, "茜…$R;");
            Say(pc, 0, 131, "啊…茜…$R;" +
                "$R別再哭了…跟我們在一起吧$R;" +
                "$P我們主人他一定會…$R;" +
                "$R一定會帶我們到哈爾列爾利去的$R「通往天空的塔」的$R;", "凱堤(菫)");
            Say(pc, 0, 131, "嗚嗚…$R;" +
                "$R沒辦法啊…嗚嗚…$R現在…嗚嗚…沒有…$R其他方法…嗚嗚…$R姐姐們…嗚嗚…會在身邊的…$R;" +
                "$P不要搞錯了…！$R這是為了回到哈爾列爾利的身邊！！$R;" +
                "$R不是因為孤單！！！$R;", "凱堤(茜)");
            Say(pc, 0, 131, "嗯…我知道！$R;" +
                "$R不要擔心了！茜$R;", "凱堤(菫)");
            对话6(pc);
        }
        void 对话3(ActorPC pc)
        {
            Say(pc, 11001234, 131, "茜…$R;");
            Say(pc, 0, 131, "茜…$R;" +
                "$R別再哭了…跟我們在一起吧$R;" +
                "$P我們主人他一定會…$R;" +
                "$R一定會帶我們到哈爾列爾利去的$R「通往天空的塔」的$R;", "凱堤(緑)");
            Say(pc, 0, 131, "嗚嗚…$R;" +
                "$R沒辦法啊…嗚嗚…$R現在…嗚嗚…沒有…$R其他方法…嗚嗚…$R姐姐們…嗚嗚…會在身邊的…$R;" +
                "$P不要搞錯了…！$R這是為了回到哈爾列爾利的身邊！！$R;" +
                "$R不是因為孤單！！！$R;", "凱堤(茜)");
            Say(pc, 0, 131, "嗯…我知道了！$R;" +
                "$R茜…$R;", "凱堤(緑)");
            对话6(pc);
        }
        void 对话4(ActorPC pc)
        {
            Say(pc, 11001234, 131, "茜…$R;");
            Say(pc, 0, 131, "啊啊…茜…$R;" +
                "$R別再哭了…跟我們在一起吧$R;" +
                "$P我們主人他一定會…$R;" +
                "$R一定會帶我們到哈爾列爾利去的$R「通往天空的塔」的$R;", "凱堤(雜梅)");
            Say(pc, 0, 131, "嗚嗚…$R;" +
                "$R沒辦法啊…嗚嗚…$R現在…嗚嗚…沒有…$R其他方法…嗚嗚…$R姐姐們…嗚嗚…會在身邊的…$R;" +
                "$P不要搞錯了…！$R這是為了回到哈爾列爾利的身邊！！$R;" +
                "$R不是因為孤單！！！$R;", "凱堤(茜)");
            Say(pc, 0, 131, "嗯…我知道！$R;" +
                "$R茜…$R;", "凱堤(雜梅)");
            对话6(pc);
        }
        void 对话5(ActorPC pc)
        {
            Say(pc, 11001234, 131, "茜……$R;");
            Say(pc, 0, 131, "那個…茜…$R;" +
                "$R加油…我是說…我…我會陪你的$R;" +
                "$P我們主人…我想主人…$R;" +
                "$R一定會帶我們到哈爾列爾利去的$R「通往天空的塔」的$R;" +
                "$R說不定可能成為一門生意…$R;", "凱堤(山吹)");
            Say(pc, 0, 131, "嗚嗚…$R;" +
                "$R沒辦法啊…嗚嗚…$R現在…嗚嗚…沒有…$R其他方法…嗚嗚…$R姐姐們…嗚嗚…會在身邊的…$R;" +
                "$P不要搞錯了…！$R這是為了回到哈爾列爾利的身邊！！$R;" +
                "$R不是因為孤單！！！$R;", "凱堤(茜)");
            Say(pc, 0, 131, "嗯…知道了！$R;" +
                "$R茜…加油!$R;", "凱堤(山吹)");
            对话6(pc);
        }
        void 对话6(ActorPC pc)
        {
            BitMask<Neko_05> Neko_05_amask = pc.AMask["Neko_05"];
            BitMask<Neko_05> Neko_05_cmask = pc.CMask["Neko_05"];
            if (CheckInventory(pc, 10017907, 1))
            {
                GiveItem(pc, 10017907, 1);
                Neko_05_cmask.SetValue(Neko_05.得到茜子, true);
                Neko_05_amask.SetValue(Neko_05.茜子任务结束, true);
                Neko_05_amask.SetValue(Neko_05.茜子任务开始, true);
                //_2b20 = true;
                Wait(pc, 1000);
                PlaySound(pc, 4012, false, 100, 50);
                ShowEffect(pc, 4131);
                Wait(pc, 6000);
                Say(pc, 0, 131, "得到$R『凱堤（茜）』$R;");
                Say(pc, 11001234, 131, "客人！$R;" +
                    "哈爾列爾利…那孩子的執著$R把願望都實現了，真的謝謝您啊！$R;" +
                    "$R那…茜就拜託您了！$R;");
                return;
            }
            Say(pc, 0, 131, "喵嗷，喵嗷，喵…$R;", "凱堤");
            Say(pc, 0, 131, "凱堤牠們好吵啊…「茜」要來嗎?$R;" +
                "$R哇！行李好多啊…$R;");
        }
    }
}