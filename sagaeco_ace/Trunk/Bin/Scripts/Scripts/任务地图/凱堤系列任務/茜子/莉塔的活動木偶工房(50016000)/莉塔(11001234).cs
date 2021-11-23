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
            Say(pc, 11001234, 131, "茜…还有其他的猫灵…$R;" +
                "$R不要太责怪…哈利路亚了$R;");

        }

        void 对话1(ActorPC pc)
        {
            Say(pc, 11001234, 131, "茜…$R;");
            Say(pc, 0, 131, "啊…$R茜…?$R;" +
                "$R别再哭了…跟我们在一起吧$R;" +
                "$P我们主人他一定会…$R;" +
                "$R一定会带我们到哈利路亚去的$R「通往天空的塔」的$R;", "猫灵（桃子）");
            Say(pc, 0, 131, "呜呜…$R;" +
                "$R没办法啊…呜呜…$R现在…呜呜…没有…$R其他方法…呜呜…$R姐姐们…呜呜…会在身边的…$R;" +
                "$P不要搞错了…！$R这是为了回到哈利路亚的身边！！$R;" +
                "$R不是因为孤单！！！$R;", "猫灵（茜子）");
            Say(pc, 0, 131, "呜呜…嗯…知道了！$R;" +
                "$R不要担心了！茜$R;", "猫灵（桃子）");
            对话6(pc); 
        }
        void 对话2(ActorPC pc)
        {
            Say(pc, 11001234, 131, "茜…$R;");
            Say(pc, 0, 131, "啊…茜…$R;" +
                "$R别再哭了…跟我们在一起吧$R;" +
                "$P我们主人他一定会…$R;" +
                "$R一定会带我们到哈利路亚去的$R「通往天空的塔」的$R;", "猫灵（菫子）");
            Say(pc, 0, 131, "呜呜…$R;" +
                "$R没办法啊…呜呜…$R现在…呜呜…没有…$R其他方法…呜呜…$R姐姐们…呜呜…会在身边的…$R;" +
                "$P不要搞错了…！$R这是为了回到哈利路亚的身边！！$R;" +
                "$R不是因为孤单！！！$R;", "猫灵（茜子）");
            Say(pc, 0, 131, "嗯…我知道！$R;" +
                "$R不要担心了！茜$R;", "猫灵（菫子）");
            对话6(pc);
        }
        void 对话3(ActorPC pc)
        {
            Say(pc, 11001234, 131, "茜…$R;");
            Say(pc, 0, 131, "茜…$R;" +
                "$R别再哭了…跟我们在一起吧$R;" +
                "$P我们主人他一定会…$R;" +
                "$R一定会带我们到哈利路亚去的$R「通往天空的塔」的$R;", "猫灵（绿子）");
            Say(pc, 0, 131, "呜呜…$R;" +
                "$R没办法啊…呜呜…$R现在…呜呜…没有…$R其他方法…呜呜…$R姐姐们…呜呜…会在身边的…$R;" +
                "$P不要搞错了…！$R这是为了回到哈利路亚的身边！！$R;" +
                "$R不是因为孤单！！！$R;", "猫灵（茜子）");
            Say(pc, 0, 131, "嗯…我知道了！$R;" +
                "$R茜…$R;", "猫灵（绿子）");
            对话6(pc);
        }
        void 对话4(ActorPC pc)
        {
            Say(pc, 11001234, 131, "茜…$R;");
            Say(pc, 0, 131, "啊啊…茜…$R;" +
                "$R别再哭了…跟我们在一起吧$R;" +
                "$P我们主人他一定会…$R;" +
                "$R一定会带我们到哈利路亚去的$R「通往天空的塔」的$R;", "猫灵（蓝子）");
            Say(pc, 0, 131, "呜呜…$R;" +
                "$R没办法啊…呜呜…$R现在…呜呜…没有…$R其他方法…呜呜…$R姐姐们…呜呜…会在身边的…$R;" +
                "$P不要搞错了…！$R这是为了回到哈利路亚的身边！！$R;" +
                "$R不是因为孤单！！！$R;", "猫灵（茜子）");
            Say(pc, 0, 131, "嗯…我知道！$R;" +
                "$R茜…$R;", "猫灵（蓝子）");
            对话6(pc);
        }
        void 对话5(ActorPC pc)
        {
            Say(pc, 11001234, 131, "茜……$R;");
            Say(pc, 0, 131, "那个…茜…$R;" +
                "$R加油…我是说…我…我会陪你的$R;" +
                "$P我们主人…我想主人…$R;" +
                "$R一定会带我们到哈利路亚去的$R「通往天空的塔」的$R;" +
                "$R说不定可能成为一门生意…$R;", "猫灵（山吹）");
            Say(pc, 0, 131, "呜呜…$R;" +
                "$R没办法啊…呜呜…$R现在…呜呜…没有…$R其他方法…呜呜…$R姐姐们…呜呜…会在身边的…$R;" +
                "$P不要搞错了…！$R这是为了回到哈利路亚的身边！！$R;" +
                "$R不是因为孤单！！！$R;", "猫灵（茜子）");
            Say(pc, 0, 131, "嗯…知道了！$R;" +
                "$R茜…加油!$R;", "猫灵（山吹）");
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
                Say(pc, 0, 131, "得到$R『猫灵（茜子）』$R;");
                Say(pc, 11001234, 131, "客人！$R;" +
                    "哈利路亚…那孩子的执着$R把愿望都实现了，真的谢谢您啊！$R;" +
                    "$R那…茜就拜托您了！$R;");
                return;
            }
            Say(pc, 0, 131, "喵嗷，喵嗷，喵…$R;", "猫灵");
            Say(pc, 0, 131, "猫灵它们好吵啊…「茜」要来吗?$R;" +
                "$R哇！行李好多啊…$R;");
        }
    }
}