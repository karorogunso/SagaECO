
using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;
using SagaMap.Scripting;
using SagaMap;
using SagaMap.Mob;
using SagaMap.Skill;
using SagaDB.Mob;
using SagaMap.Manager;
using SagaMap.Network.Client;
using SagaMap.ActorEventHandlers;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30210000
{
    public class S60000022 : Event
    {
        public S60000022()
        {
            this.EventID = 60000022;
        }
        public override void OnEvent(ActorPC pc)
        {
            if (pc.AInt["初始搭档入手3"] == 1)
            {
                TitleProccess(pc, 6, 1);//称号补领
            }
            BitMask<天宫希> 天宫希S = pc.CMask["天宫希"];//从pc.CMask["天宫希"]里取得任务标识，并保存在“天宫希S”中。这是另一种记录方法
            if (!天宫希S.Test(天宫希.初次觸發腳本))//检查“天宫希S”中，是否标记过 初次觸發腳本
            {
                第一次與對話(pc, 天宫希S);//跳转至 第一次與對話 方法，并把 “pc” 和 “天宫希S” 参数传过去给它用
                return;
            }
            else//否则
            {
                if (pc.Level >= 15 && pc.AInt["初始搭档入手3"] == 0)//若等级大于等于15且未拿过初始搭档，则触发此部分任务
                {
                    新手部分(pc); //跳转至 新手部分 方法
                    return;
                }
                if (pc.Level >= 15 && pc.Partner == null)//若大于等于15级且没有装备搭档
                {
                    没有搭档(pc); //跳转至 没有搭档
                    return;
                }
                if (pc.Level < 15)
                {
                    等级不足(pc); //跳转至 等级不足 
                    return;
                }
                if (pc.Level >= 15)
                {
                    RANK突破(pc); //跳转至 RANK突破
                    return;
                }
            }
        }
        void 第一次與對話(ActorPC pc, BitMask<天宫希> 天宫希S)//定一个方法的，并指定会接收 “pc” 和 “天宫希S” 两个参数
        {
            Say(pc, 190, "你终于来了啊！$R我们的飞空艇在接近目的地ECO城的时候$R发生了异变，$R一头巨大的鲸鱼吞噬了整个城市中$R一半的空间，$R我们在空中也受到了波及，$R飞空艇坠落到了这座岛屿附近。", "天宫希");
            Wait(pc, 1000);
            Say(pc, 190, "你说你掉到了扭曲虚空中？这可真是不得了，$R$R不过既然你现在平安无事地站在这里，$R说明你还是克服了险境呢，$R$R不愧是 " + pc.Name + " 啊。$R看起来你很有赢得奖金的实力啊！", "天宫希");
            Wait(pc, 1000);
            Say(pc, 190, "那头怪兽的出现似乎是异常情况，$R它所吞噬的区域中包括了ECO城中$R唯一的安全区，$R$R也就是说托它的福，$R我们现在暂时没有办法下线了。", "天宫希");
            Select(pc, "……", "", "无法下线？要等多久才能解决呢？");
            Wait(pc, 1000);
            Say(pc, 190, "谁知道呢，$R大概运营那边很快就能修复吧。$R$R在这之前，$R我们不妨在这里找点事做做？", "天宫希");
            Say(pc, 190, "首先找那边的老板接些委托，$R锻炼锻炼身手吧！", "天宫希");
            Say(pc, 190, "等你更厉害一点时，再来我这吧！$R我会给你好东西的！", "天宫希");
            Say(pc, 190, "（人物等级达到15时$R可来这里触发新剧情）", "提示");
            pc.JobLevel3 = 1;
            GiveItem(pc, 100000001, 1);
            GiveItem(pc, 60073900, 1);
            GiveItem(pc, 60073901, 1);
            GiveItem(pc, 50137601, 1);
            GiveItem(pc, 50107500, 1);
            GiveItem(pc, 50085100, 1);
            GiveItem(pc, 50128600, 1);
            GiveItem(pc, 50132100, 1);
            GiveItem(pc, 50127400, 1);
            GiveItem(pc, 50090114, 1);
            GiveItem(pc, 16017300, 1);
            GiveItem(pc, 16017500, 1);
            GiveItem(pc, 16017600, 1);
            GiveItem(pc, 16017700, 1);
            GiveItem(pc, 60071250, 1);
            GiveItem(pc, 60011200, 1);
            GiveItem(pc, 60011204, 1);
            GiveItem(pc, 60011205, 1);
            GiveItem(pc, 60011206, 1);
            GiveItem(pc, 60040000, 1);
            GiveItem(pc, 60060250, 1);
            GiveItem(pc, 60090050, 1);
            Say(pc, 0, "（得到了一大堆物品）", "");
            天宫希S.SetValue(天宫希.初次觸發腳本, true);//在“天宫希S”中，标记 初次觸發腳本
            return;
        }
        void 新手部分(ActorPC pc)
        {
            Say(pc, 190, "诶呀，你来了。", "天宫希");
            Say(pc, 190, "之前我说过要给你好东西对吧？$R看来你已经做好准备去接受它了呢~", "天宫希");
            Select(pc, " ", "", "什么东西？Kuji吗？");
            Say(pc, 190, "才不是枯寂那种无聊的东西！", "天宫希");
            Say(pc, 190, "在Ygg的世界里$R有一种叫做搭档的系统！", "天宫希");
            Say(pc, 190, "简单的说就是把灵魂$R通过具现化的方式召唤出来，$R成为你的战力！", "天宫希");
            Say(pc, 190, "不过因为再怎么说也是灵魂……$R所以实际上并不能参加战斗呢，$R只能通过灵魂共鸣的方式增加你的能力。", "天宫希");
            Say(pc, 190, "嘛~$R但是在旅途中有个人陪伴$R总比一个人好不是吗！？$R无聊的时候和搭档们聊聊天也是好的呀！", "天宫希");
            Select(pc, " ", "", "……");
            Say(pc, 190, "咳咳，回归正题！$R我这里有三个“灵魂”，$R他们还没找到属于自己的搭档。$R你可以选一只走哦~$R$R我觉得你一定会照顾好他们的！", "天宫希");
            Say(pc, 190, "那么你是喜欢草系的仙人掌，$R水系的皮露露，$R还是火系的小陀螺呢？", "天宫希");
            switch (Select(pc, "请选择作为你搭档的灵魂：", "", "仙人掌·阿鲁玛", "皮露露·阿鲁玛", "杀人机械·阿鲁玛"))
            {
                case 1:
                    GiveItem(pc, 110128500, 1);//获得仙人掌
                    pc.AInt["初始搭档入手3"] = 1;
                    SInt["雨伞架仙人掌领取人数"]++;
                    ShowEffect(pc, 5455);
                    Wait(pc, 3500);
                    ShowEffect(pc, 5186);
                    break;
                case 2:
                    GiveItem(pc, 110132000, 1);//获得皮露露
                    pc.AInt["初始搭档入手3"] = 1;
                    SInt["雨伞架皮露露领取人数"]++;
                    ShowEffect(pc, 5455);
                    Wait(pc, 3500);
                    ShowEffect(pc, 5161);
                    break;
                case 3:
                    GiveItem(pc, 110165300, 1);//获得杀人机械
                    pc.AInt["初始搭档入手3"] = 1;
                    SInt["雨伞架杀人机械领取人数"]++;
                    ShowEffect(pc, 5455);
                    Wait(pc, 3000);
                    ShowEffect(pc, 5226);
                    break;
            }
            TitleProccess(pc, 6, 1);
            Say(pc, 190, "嗯！$R那么就交给你了！$R为什么不试着召唤看看呢？", "天宫希");
            Say(pc, 190, "对了，$R我暂时会一直待在这里，$R有关于搭档的任何事情都可以来找我！", "天宫希");
            Say(pc, 190, "那么我就预祝你早日打败四大天王，$R成为下一任宝可梦联盟冠军啦！（划掉）", "天宫希");
            return;
        }

        private void 没有搭档(ActorPC pc)
        {
            Say(pc, 190, "你有没有好好照顾她呢？", "天宫希");
            return;
        }
        private void 等级不足(ActorPC pc)
        {
            Say(pc, 190, "首先找那边的老板接些委托，$R锻炼锻炼身手吧！", "天宫希");
            return;
        }

        private void RANK突破(ActorPC pc)
        {
            ActorPartner pn = pc.Partner;
            int xdgold = pn.Level * 1000;
            Say(pc, 190, "你好啊，我们又见面了。$R找我有什么事吗？", "天宫希");
            switch (Select(pc, "怎么办呢？", "", "给搭档换名字(需要50000G)", "提升搭档的RANK", "为宠物洗点(需要" + xdgold.ToString() + "G)", "宠物转生(需要信赖:绿)", "转生外观", "离开"))
            {
                case 1:
                    if (pc.Gold > 50000)
                    {
                        string name = InputBox(pc, "取个COOOOOOL的名字！", InputType.PetRename);
                        if (pc.Partner != null)
                        {
                            if (pc.Gold > 50000 && pc.Partner != null)
                            {
                                pc.Gold -= 50000;
                                pc.Partner.Name = name;
								pc.Inventory.Equipments[EnumEquipSlot.PET].Name = name;
                                MapClient.FromActorPC(pc).SendPetBasicInfo();
                                MapClient.FromActorPC(pc).SendPetDetailInfo();
                                SagaMap.Manager.MapManager.Instance.GetMap(pc.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.CHAR_INFO_UPDATE, null, pc.Partner, true);
								SagaMap.Network.Client.MapClient.FromActorPC(pc).SendItemInfo(pc.Inventory.Equipments[EnumEquipSlot.PET]);
                            }
                            else
                                Say(pc, 190, "你似乎没带够钱呢", "天宫希");
                        }
                    }
                    else
                        Say(pc, 190, "你似乎没带够钱呢", "天宫希");
                    break;
                case 2:
                    Say(pc, 190, "如果你有重复的【搭档召唤物】，$R或者对应稀有度的【突破石】的话，$R$R我可以为你提升搭档的RANK哦", "天宫希");
                    if (pc.Partner == null)
                    {
                        Say(pc, 190, "你似乎没有带上搭档来哦", "天宫希");
                        return;
                    }
                    if (pc.Partner.rank >= 9)
                    {
                        Say(pc, 190, "你的搭档的RANK已经满了哦", "天宫希");
                        return;
                    }
                    Say(pc, 190, "你是否要为你的搭档$R$R【" + pc.Partner.Name + "】$R$R提升rank呢？");
                    byte b = 1;
                    switch (pc.Partner.BaseData.base_rank)
                    {
                        case 71:
                            b = 2;
                            break;
                        case 81:
                            b = 3;
                            break;
                        case 91:
                            b = 5;
                            break;
                        case 101:
                            b = 8;
                            break;
                    }
                    int gold = pc.Partner.rank * (10000 + pc.Partner.rank * 1000) * b + 50000;
                    switch (Select(pc, "请选择，需要:" + gold.ToString() + "G", "", "使用相同的召唤物提升Rank", "使用突破石提升Rank", "离开"))
                    {
                        case 1:
                            if (pc.Gold < gold)
                            {
                                Say(pc, 190, "你似乎不够钱呢", "天宫希");
                                return;
                            }
                            uint itemid = pc.Inventory.Equipments[EnumEquipSlot.PET].BaseData.id;
                            if (pc.Inventory.Equipments[EnumEquipSlot.PET].BaseData.petID != pc.Partner.BaseData.id) return;
                            List<SagaDB.Item.Item> pets = GetItem(pc, itemid);
                            if (pets.Count < 1)
                            {
                                Say(pc, 190, "你似乎没有相同的搭档召唤物呢。", "天宫希");
                                return;
                            }
                            Say(pc, 190, "我需要你的：$R$R一个相同的搭档召唤物$R金币：" + gold.ToString(), "天宫希");
                            if (Select(pc, "确定要提升搭档的RANK吗？", "", "确定", "还是不要了") == 1)
                            {
                                if (pets.Count < 1)
                                {
                                    Say(pc, 190, "你似乎没有相同的搭档召唤物呢。", "天宫希");
                                    return;
                                }
                                if (pc.Gold < gold)
                                {
                                    Say(pc, 190, "你似乎不够钱呢", "天宫希");
                                    return;
                                }
                                if (pc.Partner != null)
                                {
                                    if (pc.Partner.rank < 9)
                                    {
                                        MapClient client = MapClient.FromActorPC(pc);
                                        pc.Gold -= gold;
                                        client.DeleteItem(pets[0].Slot, 1, true);
                                        pc.Partner.rank++;

                                    }
                                    MapClient.FromActorPC(pc).SendPetBasicInfo();
                                    MapClient.FromActorPC(pc).SendPetDetailInfo();
                                    MapClient.FromActorPC(pc).SendSystemMessage(pc.Partner.Name + " 的Rank提升到了" + pc.Partner.rank.ToString());
                                    SkillHandler.Instance.ShowEffectOnActor(pc.Partner, 5446);
                                }
                            }
                            break;
                        case 2:
                            if (pc.Gold < gold)
                            {
                                Say(pc, 190, "你似乎不够钱呢", "天宫希");
                                return;
                            }
                            uint StoneID = 0;
                            switch (pc.Partner.BaseData.base_rank)
                            {
                                case 81://S
                                    StoneID = 950000027;
                                    break;
                                case 91://SS
                                    StoneID = 950000028;
                                    break;
                                case 101://SSS
                                    StoneID = 950000029;
                                    break;
                            }
                            if (StoneID == 0)
                            {
                                Say(pc, 190, "你的搭档不能用突破石提升呢。", "天宫希");
                                return;
                            }
                            if (CountItem(pc, StoneID) < 1)
                            {
                                Say(pc, 190, "你似乎没有带来突破石呢。", "天宫希");
                                return;
                            }
                            Say(pc, 190, "我需要你的：$R$R一个对应的突破石$R金币：" + gold.ToString(), "天宫希");
                            if (Select(pc, "确定要提升搭档的RANK吗？", "", "确定", "还是不要了") == 1)
                            {
                                if (CountItem(pc, StoneID) < 1)
                                {
                                    Say(pc, 190, "你似乎没有相同的搭档召唤物呢。", "天宫希");
                                    return;
                                }
                                if (pc.Gold < gold)
                                {
                                    Say(pc, 190, "你似乎不够钱呢", "天宫希");
                                    return;
                                }
                                if (pc.Partner != null)
                                {
                                    if (pc.Partner.rank < 9)
                                    {
                                        pc.Gold -= gold;
                                        TakeItem(pc, StoneID, 1);
                                        pc.Partner.rank++;
                                    }
                                    MapClient.FromActorPC(pc).SendPetBasicInfo();
                                    MapClient.FromActorPC(pc).SendPetDetailInfo();
                                    MapClient.FromActorPC(pc).SendSystemMessage(pc.Partner.Name + " 的Rank提升到了" + pc.Partner.rank.ToString());
                                    SkillHandler.Instance.ShowEffectOnActor(pc.Partner, 5446);
                                }
                            }
                            break;
                    }
                    break;
                case 3:
                    xdgold = pn.Level * 1000;
                    if (Select(pc, "需要" + xdgold.ToString() + "G，确定洗点？", "", "确定", "还是算了") == 1)
                    {
                        if (pc.Gold >= xdgold)
                        {
                            pc.Gold -= xdgold;
                            pc.Partner.perk0 = 0;
                            pc.Partner.perk1 = 0;
                            pc.Partner.perk2 = 0;
                            pc.Partner.perk3 = 0;
                            pc.Partner.perk4 = 0;
                            pc.Partner.perk5 = 0;
                            pc.Partner.perkpoint = pc.Partner.Level;
                            MapClient.FromActorPC(pc).SendPetBasicInfo();
                            MapClient.FromActorPC(pc).SendPetDetailInfo();
                            SagaMap.PC.StatusFactory.Instance.CalcStatus(pc);
                            ShowEffect(pc, pc.Partner, 9913);
                            Wait(pc, 2000);
                            Say(pc, 190, pc.Partner.Name + "的能力重置了");
                        }
                        else
                        {
                            Say(pc, 190, "你似乎不够钱呢", "天宫希");
                            return;
                        }
                    }
                    break;
                case 4:
                    Say(pc, 131, "如果你的搭档的信赖度达到【绿色】,$R并且携带对应稀有度的【转生石】的话$R$R搭档就可以进行转生哦。$R当然，我还要收取一定的费用的啦。", "天宫希");
                    switch (Select(pc, "怎么办呢？", "", "我要为搭档转生", "查看价目表", "转生有什么好处？", "算了"))
                    {
                        case 1:
                            if (pc.Partner.rebirth)
                            {
                                Say(pc, 131, "你的搭档似乎转生过了呢？", "天宫希");
                                return;
                            }
                            if (pc.Partner.reliability < 4)
                            {
                                Say(pc, 131, "你的搭档信赖度似乎还不够哦？", "天宫希");
                                return;
                            }
                            int cost = 5000;
                            uint ItemRe = 950000035;
                            switch (pc.Partner.BaseData.base_rank)
                            {
                                case 71:
                                    cost = 10000;
                                    ItemRe = 950000036;
                                    break;
                                case 81:
                                    cost = 50000;
                                    ItemRe = 950000037;
                                    break;
                                case 91:
                                    cost = 200000;
                                    ItemRe = 950000038;
                                    break;
                                case 101:
                                    cost = 600000;
                                    ItemRe = 950000039;
                                    break;
                            }
                            SagaDB.Item.Item item = ItemFactory.Instance.GetItem(ItemRe);
                            Say(pc, 0, "为" + pc.Partner.Name + "转生需要：$R$R -" + cost.ToString() + "G$R -" + item.BaseData.name + "一个");
                            if (Select(pc, "确定要为她转生吗？", "", "确定", "还是算了") == 1)
                            {
                                cost = 5000;
                                ItemRe = 950000035;
                                switch (pc.Partner.BaseData.base_rank)
                                {
                                    case 71:
                                        cost = 10000;
                                        ItemRe = 950000036;
                                        break;
                                    case 81:
                                        cost = 50000;
                                        ItemRe = 950000037;
                                        break;
                                    case 91:
                                        cost = 200000;
                                        ItemRe = 950000038;
                                        break;
                                    case 101:
                                        cost = 600000;
                                        ItemRe = 950000039;
                                        break;
                                }
                                if (CountItem(pc, ItemRe) < 1)
                                {
                                    Say(pc, 190, "你似乎没有转生石呢。", "天宫希");
                                    return;
                                }
                                if (pc.Gold < cost)
                                {
                                    Say(pc, 190, "你似乎不够钱呢", "天宫希");
                                    return;
                                }
                                pc.Gold -= cost;
                                TakeItem(pc, ItemRe, 1);
                                pc.Partner.rebirth = true;
								pc.Inventory.Equipments[EnumEquipSlot.PET].PartnerRebirth = 1;
                                MapClient.FromActorPC(pc).SendPetBasicInfo();
                                MapClient.FromActorPC(pc).SendPetDetailInfo();
                                MapClient.FromActorPC(pc).SendSystemMessage(pc.Partner.Name + " 转生了！");
								SagaMap.Network.Client.MapClient.FromActorPC(pc).SendItemInfo(pc.Inventory.Equipments[EnumEquipSlot.PET]);
                                SkillHandler.Instance.ShowEffectOnActor(pc.Partner, 5446);
                            }
                            break;
                        case 2:
                            Say(pc, 131, "根据稀有度的不同，价格如下：$R -B级:5000G $R -A级:10000G $R -S级:50000G $R -SS级:200000G $R -SSS级:600000G", "天宫希");
                            break;
                        case 3:
                            Say(pc, 131, "转生后的搭档，$R信赖度可以继续提升到【桃色】。$R并且在装备转手的搭档后，$R可以获得变身为搭档的技能。$R$R除此之外，搭档每一级RANK的等级上限将会提升【双倍】。$R$R※比如SS级搭档，转生前每RANK提升4级$R转生后每RANK提升8级", "天宫希");
                            break;
                    }
                    break;
                case 5:
                    if (!pc.Partner.rebirth)
                    {
                        Say(pc,0,"你的搭档似乎还没有转生哦？", "天宫希");
                        return;
                    }
                    Say(pc, 131, "使用转生外观是免费的哦。$R$R由于我们无法确定哪些搭档拥有转生外观，$R所以如果您在使用转生外观后，$R发现外观有问题的话，$R是可以随时切换回去的。", "天宫希");
                    switch(Select(pc,"怎么办呢？","","使用转生外观","复原初始外观","离开"))
                    {
                        case 1:
                            uint pid = pc.Inventory.Equipments[EnumEquipSlot.PET].BaseData.petID + 1;
                            if (SagaDB.Partner.PartnerFactory.Instance.PartnerPictList.Contains(pid))
                            {
                                pc.Inventory.Equipments[EnumEquipSlot.PET].PictID = pid;
                                Say(pc, 0, "您的搭档已经有转生外观了，$R请重新召唤搭档确认是否可用。");
                                MapClient.FromActorPC(pc).SendSystemMessage(pc.Partner.Name + " 使用了转生外观。");
                            }
                            else
                            {
                                Say(pc, 0, "你的搭档似乎没有转生外观哦。", "天宫希");
                                return;
                            }
                            break;
                        case 2:
                            pc.Inventory.Equipments[EnumEquipSlot.PET].PictID = 0;
                            MapClient.FromActorPC(pc).SendSystemMessage(pc.Partner.Name + " 复原成初始外观了。");
                            break;
                        case 3:
                            break;
                    }
                    break;
            }
        }
    }
}