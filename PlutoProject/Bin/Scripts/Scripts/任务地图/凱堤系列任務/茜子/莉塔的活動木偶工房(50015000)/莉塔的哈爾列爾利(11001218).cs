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
                Say(pc, 11001218, 131, "一起去我的飞空庭吗?$R;");
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
                Say(pc, 0, 131, "把『电脑只读记忆体』给哈利路亚了！$R;");
                Wait(pc, 1000);
                Neko_05_cmask.SetValue(Neko_05.把電腦唯讀記憶體給哈爾列爾利, true);
                Say(pc, 11001218, 131, "妈妈！！我回来了！！$R;");
                Say(pc, 11001219, 131, "回来了?$R;" +
                    "$R因为回来晚了~让我担心了呢…$R你去了光之塔?$R;" +
                    "$R玛莎跟我说的$R;" +
                    "$P真的谢谢啊！$R;");
                Say(pc, 11001218, 131, "嗯！因为「客人」的帮忙$R『电脑只读记忆体』也找到了！$R;" +
                    "$R这次，我的飞空庭终于可以飞了♪$R;" +
                    "$P来吧！「客人」！！到我的飞空庭吧！！$R;" +
                    "$R来来！妈妈也一起吧！$R;");
                Say(pc, 11001219, 131, "…哈利路亚啊…$R;" +
                    "$R有重要的话要跟你说……$R;");
                Say(pc, 11001218, 131, "在飞空庭说也是可以的啊♪$R;");
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
                    "$R可以把我带到有飞空庭工匠的$R地方吗?$R;");
                指導哈爾列爾利(pc);
                return;
            }
            if (pc.Level > 29 && pc.Fame > 19 &&
                !Neko_05_amask.Test(Neko_05.茜子任务结束) &&
                !Neko_05_cmask.Test(Neko_05.开始交谈))
            {
                Say(pc, 11001218, 131, "还有……$R;" +
                    "$R压力机的顶篷部位好像有问题$R;" +
                    "$P因为承受不了高压$R回转轴的支撑带损伤很快$R;" +
                    "$R怎么办才好啊?妈妈?$R;");
                Say(pc, 11001219, 131, "哈利路亚！听好$R;" +
                    "$R这位妈妈是木偶制作大师$R不是飞空庭的技师！$R;");
                switch (Select(pc, "怎么了吗?", "", "交谈", "放弃"))
                {
                    case 1:
                        Neko_05_cmask.SetValue(Neko_05.开始交谈, true);
                        Say(pc, 11001219, 131, "不好意思…解释的有点晚了！$R;" +
                            "$R这孩子是石像「哈利路亚」$R一直梦想着自己作为活动木偶石像$R有天也可以成为飞空庭工匠$R;" +
                            "$P这孩子决定要靠自己的双手$R制造出飞空庭引擎…$R;" +
                            "$R唐卡这里到处都是修理好的探索引擎$R还是这样固执$R;");
                        Say(pc, 11001218, 131, "我说过啊！$R我想用自己做的飞空庭引擎$R在天空中翱翔傲视大地！！$R;" +
                            "$P……?$R;" +
                            "$R这位是妈妈的朋友吗?$R;");
                        Say(pc, 11001219, 131, "不是！是来找妈妈的客人$R;" +
                            "$R妈妈现在要帮客人工作$R不能淘气哦！$R;");
                        Say(pc, 11001218, 131, "那…$R想请问这位「客人」一下♪$R;" +
                            "$R外部引擎的活塞轴心过份磨损时…$R;");
                        Say(pc, 11001219, 131, "哈利路亚！够了！$R不能问客人这些！会打扰人家的$R;" +
                            "$R关于飞空庭的消息$R要问飞空庭的…工匠啊！$R;" +
                            "$P不好意思$R;" +
                            "$R…这孩子不是故意的$R只是太喜爱飞空庭了，所以…$R;");
                        Say(pc, 11001218, 131, "那可以请「客人」…$R把我带到飞空庭工匠那吗?$R;");
                        Say(pc, 11001219, 131, "哈利路亚！够了！$R;" +
                            "$R客人怎么会答应无理的请求啊！$R;");
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
                Say(pc, 0, 131, "没有回答…$R;" +
                    "$R睡着了…?$R;");
                Say(pc, 0, 111, "那是我的面具$R;" +
                    "$R我现在在「客人」的口袋里呢$R快进来一起玩吧♪$R;", "行李里的哈利路亚");
                Say(pc, 11001219, 131, "真的！不要吓我！$R;");
                指導哈爾列爾利(pc);
                return;
            }
            Say(pc, 11001218, 131, "妈妈！制造飞空庭好累喔$R;");
        }

        void 指導哈爾列爾利(ActorPC pc)
        {
            BitMask<Neko_05> Neko_05_amask = pc.AMask["Neko_05"];
            BitMask<Neko_05> Neko_05_cmask = pc.CMask["Neko_05"];
            switch (Select(pc, "要指导哈利路亚吗?", "", "给他指导", "放弃"))
            {
                case 1:
                    Say(pc, 11001219, 131, "哎呀…真的吗?$R;" +
                        "$R关于飞空庭的消息…$R要问唐卡西北部飞空庭工厂的$R工匠比较好$R;" +
                        "$P请送哈利路亚到那个地方吧…$R;" +
                        "$R真的很感谢您…$R;" +
                        "$P哈利路亚！你要向客人道谢哦！$R;" +
                        "$R不要给客人带来麻烦$R变成道具一起去吧！$R;");
                    Say(pc, 11001218, 131, "啊！啊啊！$R谢谢您啊！「客人」♪$R;" +
                        "$R那我就变成道具吧！$R;");
                    if (CheckInventory(pc, 10057380, 1))
                    {
                        Wait(pc, 1000);
                        ShowEffect(pc, 11001218, 4131);
                        Wait(pc, 1000);
                        PlaySound(pc, 2040, false, 100, 50);
                        GiveItem(pc, 10057380, 1);
                        Neko_05_cmask.SetValue(Neko_05.开始指导, true);
                        Neko_05_amask.SetValue(Neko_05.茜子任务开始, true);
                        Say(pc, 0, 131, "收到了『莉塔的哈利路亚（活动）』！$R;");
                        Say(pc, 11001218, 111, "那就拜托您了♪$R;");
                        return;
                    }
                    Say(pc, 11001218, 131, "……$R;" +
                        "$R「客人」的行李满了…$R;");
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
            Say(pc, 11001218, 131, "妈！我回来啰！$R;");
            Say(pc, 11001219, 131, "哎呀！…回来啦?$R;" +
                "$R这么晚了!我好担心呢…$R去到阿克罗尼亚大陆了?$R;" +
                "$P真的…非常谢谢您…$R;");
            Say(pc, 11001218, 131, "嗯！！都是托「客人」福！！$R;" +
                "$P「客人」带我去了阿克罗尼亚大陆$R我终于可以制造很好的飞空庭啊♪$R;" +
                "$R多谢您了！「客人」！$R;");
            if (pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.PET))
            {
                if (pc.Inventory.Equipments[EnumEquipSlot.PET].ItemID == 10017905)
                {
                    Say(pc, 0, 131, "啊啊！我忍不下去了！！$R;" +
                        "$R主人的名字不是「客人」！！$R;" +
                        pc.Name + "!!!$R;", "猫灵（山吹）");
                    判断猫灵携带3(pc);
                    return;
                }
                if (pc.Inventory.Equipments[EnumEquipSlot.PET].ItemID == 10017900)
                {
                    Say(pc, 0, 131, "啊啊！我忍不下去了！！$R;" +
                        "$R主人的名字不是「客人」！！$R;" +
                        pc.Name + "!!!!$R;", "猫灵（桃子）");
                    判断猫灵携带3(pc);
                    return;
                }
                if (pc.Inventory.Equipments[EnumEquipSlot.PET].ItemID == 10017906)
                {
                    Say(pc, 0, 131, "哈利路亚一直弄错了$R;" +
                        "$R主人的名字不是「客人」！！$R;" +
                        pc.Name + "!$R;", "猫灵（菫子）");
                    判断猫灵携带3(pc);
                    return;
                }
                if (pc.Inventory.Equipments[EnumEquipSlot.PET].ItemID == 10017903)
                {
                    Say(pc, 0, 131, "哈利路亚先生！名字错了！$R;" +
                        "$R主人的名字不是「客人」！！$R;" +
                        pc.Name + "!!$R;", "猫灵（蓝子）");
                    判断猫灵携带3(pc);
                    return;
                }
                if (pc.Inventory.Equipments[EnumEquipSlot.PET].ItemID == 10017902)
                {
                    Say(pc, 0, 131, "不对…！我們主人的名字是……$R;" +
                        pc.Name + "!!!$R;", "猫灵（绿子）");
                    判断猫灵携带3(pc);
                    return;
                }
            }
            //*/
        }
        void 判断所携带猫灵2(ActorPC pc)
        {
            Say(pc, 11001218, 131, "啊啊！是「客人」！！$R;" +
                "$R上次真的很感谢您♪$R;");
            if (pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.PET))
            {
                if (pc.Inventory.Equipments[EnumEquipSlot.PET].ItemID == 10017905)
                {
                    Say(pc, 0, 131, "啊啊！我忍不下去了！！$R;" +
                        "$R主人的名字不是「客人」！！$R;" +
                        pc.Name + "!!!$R;", "猫灵（山吹）");
                    判断猫灵携带3(pc);
                    return;
                }
                if (pc.Inventory.Equipments[EnumEquipSlot.PET].ItemID == 10017900)
                {
                    Say(pc, 0, 131, "啊啊！我忍不下去了！！$R;" +
                        "$R主人的名字不是「客人」！！$R;" +
                        pc.Name + "!!!!$R;", "猫灵（桃子）");
                    判断猫灵携带3(pc);
                    return;
                }
                if (pc.Inventory.Equipments[EnumEquipSlot.PET].ItemID == 10017906)
                {
                    Say(pc, 0, 131, "哈利路亚一直弄错了$R;" +
                        "$R主人的名字不是「客人」！！$R;" +
                        pc.Name + "!$R;", "猫灵（菫子）");
                    判断猫灵携带3(pc);
                    return;
                }
                if (pc.Inventory.Equipments[EnumEquipSlot.PET].ItemID == 10017903)
                {
                    Say(pc, 0, 131, "哈利路亚先生！名字错了！$R;" +
                        "$R主人的名字不是「客人」！！$R;" +
                        pc.Name + "!!$R;", "猫灵（蓝子）");
                    判断猫灵携带3(pc);
                    return;
                }
                if (pc.Inventory.Equipments[EnumEquipSlot.PET].ItemID == 10017902)
                {
                    Say(pc, 0, 131, "不对…！我们主人的名字是……$R;" +
                        pc.Name + "!!!$R;", "猫灵（绿子）");
                    判断猫灵携带3(pc);
                    return;
                }
            }
        }

        void 判断猫灵携带3(ActorPC pc)
        {
            BitMask<Neko_05> Neko_05_amask = pc.AMask["Neko_05"];
            BitMask<Neko_05> Neko_05_cmask = pc.CMask["Neko_05"];
            Say(pc, 11001218, 131, "猫灵说话了?$R;" +
                "$R啊！对了！$R「客人」！要不要到我的飞空庭看看阿?$R;" +
                "$P我会特别招待您的！$R茜也在等你哦！$R;");
            if (CountItem(pc, 10017900) >= 1)
            {
                Say(pc, 0, 131, "什么?茜??$R;", "猫灵（桃子）");
            }
            if (CountItem(pc, 10017906) >= 1)
            {
                Say(pc, 0, 131, "等一下！！$R你刚刚是说「茜」…?$R;", "猫灵（菫子）");
            }
            if (CountItem(pc, 10017903) >= 1)
            {
                Say(pc, 0, 131, "茜…?$R;", "猫灵（蓝子）");
            }
            if (CountItem(pc, 10017902) >= 1)
            {
                Say(pc, 0, 131, "…茜…$R;", "猫灵（绿子）");
            }
            if (CountItem(pc, 10017905) >= 1)
            {
                Say(pc, 0, 131, "什么…?谁??$R;", "猫灵（山吹）");
            }
            if (CountItem(pc, 10017900) >= 1)
            {
                Say(pc, 0, 131, "茜！是茜！$R;", "猫灵（桃子）");
            }
            if (CountItem(pc, 10017906) >= 1)
            {
                Say(pc, 0, 131, "哈利路亚的飞空庭里有茜！$R;" +
                    "$R啊啊！这都多久以前的事了！！$R;", "猫灵（菫子）");
            }
            if (CountItem(pc, 10017905) >= 1)
            {
                Say(pc, 0, 131, "…?茜?它是谁啊?$R;" +
                    "$R我不认识……$R;", "猫灵（山吹）");
            }
            if (CountItem(pc, 10017903) >= 1)
            {
                Say(pc, 0, 131, "茜……?$R;" +
                    "$R是我们姊妹之一吗?$R;", "猫灵（蓝子）");
            }
            if (CountItem(pc, 10017906) >= 1)
            {
                Say(pc, 0, 131, "茜很小的时候就跟我们分开了$R茜所有的事都不记得了$R;" +
                    "$R桃子你还记得吗?$R;", "猫灵（菫子）");
            }
            if (CountItem(pc, 10017900) >= 1)
            {
                Say(pc, 0, 131, "是啊！你不记得了吗?$R茜是我们的姐妹♪$R;" +
                    "$R是个安静柔弱，容易寂寞的孩子$R;", "猫灵（桃子）");
            }
            if (CountItem(pc, 10017906) >= 1)
            {
                Say(pc, 0, 131, "茜……在飞空庭里自己待着吗?$R;" +
                    "$R有点担心阿…可能因为寂寞正在哭呢$R;", "猫灵（菫子）");
            }
            switch (Select(pc, "要去哈利路亚的飞空庭吗?", "", "去", "不去"))
            {
                case 1:
                    Say(pc, 11001218, 131, "来~出发了！$R;" +
                        "$R都准备好了?$R;");
                    switch (Select(pc, "要去哈利路亚的飞空庭吗?", "", "去", "不去"))
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
                                "$R那想过来的时候再来吧！$R;");
                            break;
                    }
                    break;
                case 2:
                    Say(pc, 11001218, 131, "嗯?不去?$R;" +
                        "$R那想过来的时候再来吧！$R;");
                    break;
            }//*/
        }
        void 去哈爾列爾利的飛空庭(ActorPC pc)
        {

            switch (Select(pc, "要去哈利路亚的飞空庭吗?", "", "去", "不去"))
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
                    Say(pc, 11001218, 131, "嗯?那么想去的时候再来吧！$R;");
                    break;
            }
        }
    }
}
