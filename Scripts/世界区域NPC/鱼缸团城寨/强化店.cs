
using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.FF
{
    public class S80000008 : Event
    {
        public S80000008()
        {
            this.EventID = 80000008;
        }

        public override void OnEvent(ActorPC pc)
        {
/*            DateTime endDT = new DateTime(2017, 2, 12);//设置结束日期为2017年2月12日
            if (DateTime.Now < endDT)
            {
                if (pc.CInt["CC新春活动"] == 1 && pc.CInt["CC新春活动 天宫希"] == 1)//此段代码为强化娘所用
                {
                    ChangeMessageBox(pc);
                    Say(pc, 0, "绯之迦具土？我没看到也……", "机械狐狸B3");
                    Say(pc, 0, "会不会是她忘在仓库里了呢！！", "机械狐狸B3");
                    Say(pc, 0, "……回去找$CR天宫希$CD吧", pc.Name);
                    pc.CInt["CC新春活动 天宫希"] = 2;
                    return;
                }
            }

            if (pc.CInt["免费25强化次数"] > 0)
            {
                Say(pc, 0, "你还有" + pc.CInt["免费25强化次数"].ToString() + "次免费强25的机会");
                强25(pc);
                return;
            }*/

            Say(pc, 0, "其实我是欧洲非洲人鉴定师...$R$R脸黑不黑我一眼就看得出来！$R$R本店新开张$R要不要试试呢？", "强化店");
            int set = Select(pc, "请选择需要的服务", "", "普通强化", "觉醒强化(强化11级以上，需要觉醒)", "祝福水觉醒", "重置强化属性（免费中）", "强化转移", "移除觉醒", "合成强化石","离开");
            if (set < 3)
            {
                if (set == 1)
                    强化(pc, set, 0);
                if (set == 2)
                {
                    byte Rtype = 0;
                    int RSet = Select(pc, "请选择要使用的『强化石』类型", "", "普通强化石", "高级强化石(成功率小幅提升)", "离开");
                    if (RSet == 1)
                        Rtype = 0;
                    if (RSet == 2)
                        Rtype = 1;
                    if (RSet == 3) return;
                    强化(pc, set, Rtype);
                }
            }
            if (set == 3)
                祝福水觉醒(pc);
            if (set == 4)
                重置强化(pc);
            if (set == 5)
                强化转移(pc);
            if (set == 6)
                移除觉醒(pc);
            if (set == 7)
                合成强化石(pc);
        }
        void 合成强化石(ActorPC pc)
        {
            uint TitemID = 960000003;
            uint NitemID = 960000000;
            uint NCount = 50;
            switch (Select(pc,"请选择要合成的强化石种类","", "高级项链强化石", "高级武器强化石", "高级衣服强化石","还是算了把"))
            {
                case 1:
                    TitemID = 960000003;//高级项链强化石
                    NitemID = 960000000;//需要普通项链强化石
                    NCount = 50;//需要50个
                    break;
                case 2:
                    TitemID = 960000004;
                    NitemID = 960000001;
                    NCount = 50;
                    break;
                case 3:
                    TitemID = 960000005;
                    NitemID = 960000002;
                    NCount = 50;
                    break;
                case 4:
                    return;
            }
            SagaDB.Item.Item Titem = SagaDB.Item.ItemFactory.Instance.GetItem(TitemID);
            SagaDB.Item.Item Nitem = SagaDB.Item.ItemFactory.Instance.GetItem(NitemID);
            Say(pc,131,"合成『"+Titem.BaseData.name +"』需要：$R$R -"+Nitem.BaseData.name+"  "+NCount+"个$R$R确定要合成吗？", "强化店");
            if (Select(pc, "确定要合成『" + Titem.BaseData.name + "』？", "", "是的", "还是算了") != 1) return;
            ushort input = ushort.Parse(InputBox(pc, "请输入要合成的数量(最多100个)", InputType.Bank));
            if (input > 100) return;
            if (input < 1) return;
            ushort count = (ushort)(input * NCount);
            if(CountItem(pc, NitemID) < count)
            {
                Say(pc,131,"所需材料不够哦", "强化店");
                return;
            }
            TakeItem(pc, NitemID, count);
            GiveItem(pc, TitemID, input);
            
        }
        void 移除觉醒(ActorPC pc)
        {
            SagaDB.Item.Item Equip = new SagaDB.Item.Item();
            EnumEquipSlot SltSolt = EnumEquipSlot.CHEST_ACCE;
            string name = "无";
            List<SagaDB.Item.Item> Equips = new List<SagaDB.Item.Item>();
            List<string> names = new List<string>();
            List<EnumEquipSlot> slots = new List<EnumEquipSlot>();
            slots.Add(EnumEquipSlot.CHEST_ACCE);
            slots.Add(EnumEquipSlot.RIGHT_HAND);
            slots.Add(EnumEquipSlot.UPPER_BODY);
            slots.Add(EnumEquipSlot.LEFT_HAND);
            foreach (EnumEquipSlot i in slots)
            {
                if (pc.Inventory.Equipments.ContainsKey(i))
                {
                    SagaDB.Item.Item item = pc.Inventory.Equipments[i];
                    string ts = "武器";
                    if (i == EnumEquipSlot.CHEST_ACCE) ts = "项链";
                    if (i == EnumEquipSlot.UPPER_BODY) ts = "衣服";
                    if (i == EnumEquipSlot.LEFT_HAND)
                    {
                        ts = "副手";
                        if (item.BaseData.itemType != ItemType.BOW && item.BaseData.itemType != ItemType.GUN
                         && item.BaseData.itemType != ItemType.RIFLE && item.BaseData.itemType != ItemType.DUALGUN)
                            continue;
                    }
                    string ss = item.BaseData.name;
                    if (item.BaseData.name.Length > 11)
                        ss = item.BaseData.name.Substring(0, 11);
                    name = "【" + ts + "】" + "【" + ss + "】强化次数：" + item.Refine.ToString();
                    if (item.ChangeMode2)
                    {
                        Equips.Add(item);
                        names.Add(name);
                    }
                }
            }
            if (names.Count < 1)
            {
                Say(pc, 0, "不好意思...$R$R你似乎没有已经觉醒的装备呢。", "强化店");
                return;
            }
            names.Add("离开");
            int set2 = Select(pc, "请选择要【取消觉醒】的部位", "", names.ToArray());
            if (set2 == names.Count) return;
            Equip = Equips[set2 - 1];
            SltSolt = Equip.EquipSlot[0];
            string sss = Equip.BaseData.name;
            if (Equip.BaseData.name.Length > 11)
                sss = Equip.BaseData.name.Substring(0, 11);
            if (Select(pc, "确定要取消【" + sss + "】的觉醒吗？", "", "是的，移除这个装备的觉醒", "还是算了") == 1)
            {
                Equip.ChangeMode2 = false;
                SagaMap.Network.Client.MapClient.FromActorPC(pc).SendSystemMessage("你的【" + Equip.BaseData.name + "】的觉醒移除了。");
                SagaMap.Skill.SkillHandler.Instance.ShowEffectOnActor(pc, 5424);
                SagaMap.Skill.SkillHandler.Instance.ShowEffectOnActor(pc, 5425);
                SagaMap.Skill.SkillHandler.Instance.ShowEffectOnActor(pc, 5424);
                SagaMap.Skill.SkillHandler.Instance.ShowEffectOnActor(pc, 5425);
                SagaMap.Skill.SkillHandler.Instance.ShowEffectOnActor(pc, 5424);
                SagaMap.Skill.SkillHandler.Instance.ShowEffectOnActor(pc, 5425);
                SagaMap.Network.Client.MapClient.FromActorPC(pc).SendItemInfo(Equip);
            }

        }
        /*void 强25(ActorPC pc)
        {
            SagaDB.Item.Item Equip = new SagaDB.Item.Item();
            EnumEquipSlot SltSolt = EnumEquipSlot.CHEST_ACCE;
            string name = "无";
            List<SagaDB.Item.Item> Equips = new List<SagaDB.Item.Item>();
            List<string> names = new List<string>();
            List<EnumEquipSlot> slots = new List<EnumEquipSlot>();
            slots.Add(EnumEquipSlot.CHEST_ACCE);
            slots.Add(EnumEquipSlot.RIGHT_HAND);
            slots.Add(EnumEquipSlot.UPPER_BODY);
            slots.Add(EnumEquipSlot.LEFT_HAND);
            foreach (EnumEquipSlot i in slots)
            {
                if (pc.Inventory.Equipments.ContainsKey(i))
                {
                    SagaDB.Item.Item item = pc.Inventory.Equipments[i];
                    string ts = "武器";
                    if (i == EnumEquipSlot.CHEST_ACCE) ts = "项链";
                    if (i == EnumEquipSlot.UPPER_BODY) ts = "衣服";
                    string ss = item.BaseData.name;
                    if (item.BaseData.name.Length > 11)
                        ss = item.BaseData.name.Substring(0, 11);
                    name = "【" + ts + "】" + "【" + ss + "】强化次数：" + item.Refine.ToString();
                    Equips.Add(item);
                    names.Add(name);
                }
            }

            names.Add("离开");
            int set2 = Select(pc, "请选择要【免费强化25】的部位", "", names.ToArray());
            if (set2 == names.Count) return;
            pc.CInt["免费25强化次数"]--;
            Equip = Equips[set2 - 1];
            SltSolt = Equip.EquipSlot[0];
            Equip.Refine = 25;
            Equip.ChangeMode2 = false;
            PlaySound(pc, 3264, false, 100, 50);
            SagaMap.Network.Client.MapClient.FromActorPC(pc).SendSystemMessage("【" + Equip.BaseData.name + "】+" + Equip.Refine.ToString() + " 强化成功啦！");
            ItemFactory.Instance.CalcRefineBouns(Equip);
            SagaMap.Network.Client.MapClient.FromActorPC(pc).SendItemInfo(Equip);
            SagaMap.PC.StatusFactory.Instance.CalcStatus(pc);
            SagaMap.Network.Client.MapClient.FromActorPC(pc).SendPlayerInfo();
            pc.CInt["洗强化第一次免费2"] = 0;
            重置强化(pc);
        }*/
        void 祝福水觉醒(ActorPC pc)
        {
            Say(pc, 131, "如果你带来一定数量的$R对应等级的祝福水$R$R倒在装备身上就会觉醒哦！", "强化店");
            SagaDB.Item.Item Equip = new SagaDB.Item.Item();
            Dictionary<uint, ushort> stuffs = new Dictionary<uint, ushort>();
            EnumEquipSlot SltSolt = EnumEquipSlot.CHEST_ACCE;
            string name = "无";
            List<SagaDB.Item.Item> Equips = new List<SagaDB.Item.Item>();
            List<string> names = new List<string>();
            List<EnumEquipSlot> slots = new List<EnumEquipSlot>();
            slots.Add(EnumEquipSlot.CHEST_ACCE);
            slots.Add(EnumEquipSlot.RIGHT_HAND);
            slots.Add(EnumEquipSlot.LEFT_HAND);
            slots.Add(EnumEquipSlot.UPPER_BODY);
            foreach (EnumEquipSlot i in slots)
            {
                if (pc.Inventory.Equipments.ContainsKey(i))
                {
                    SagaDB.Item.Item item = pc.Inventory.Equipments[i];
                    string ts = "武器";
                    if (i == EnumEquipSlot.CHEST_ACCE) ts = "项链";
                    if (i == EnumEquipSlot.UPPER_BODY) ts = "衣服";
                    if (i == EnumEquipSlot.LEFT_HAND)
                    {
                        ts = "副手";
                        if (item.BaseData.itemType != ItemType.BOW && item.BaseData.itemType != ItemType.GUN
                         && item.BaseData.itemType != ItemType.RIFLE && item.BaseData.itemType != ItemType.DUALGUN)
                            continue;
                    }
                    string ss = item.BaseData.name;
                    if (item.BaseData.name.Length > 11)
                        ss = item.BaseData.name.Substring(0, 11);
                    name = "【" + ts + "】" + "【" + ss + "】强化次数：" + item.Refine.ToString();
                    if (item.Refine >= 10 && !item.ChangeMode2)
                    {
                        Equips.Add(item);
                        names.Add(name);
                    }
                }
            }
            if (names.Count < 1)
            {
                Say(pc, 0, "不好意思...$R$R你似乎没有未觉醒的装备呢。", "强化店");
                return;
            }
            names.Add("离开");
            int set2 = Select(pc, "请选择要觉醒的部位", "", names.ToArray());
            if (set2 == names.Count) return;
            Equip = Equips[set2 - 1];
            SltSolt = Equip.EquipSlot[0];
            stuffs = GetFeverStuffs(Equip, Equip.Refine);
            string o = "";
            int rate = 100;
            foreach (var item in stuffs)
            {
                SagaDB.Item.Item s = ItemFactory.Instance.GetItem(item.Key);
                o += s.BaseData.name + " " + item.Value + "个$R";
            }
            o += "费用： 0" + "G";
            o += "$R$R成功率约：" + rate.ToString() + "％";

            string sss = Equip.BaseData.name;
            if (Equip.BaseData.name.Length > 11)
                sss = Equip.BaseData.name.Substring(0, 11);
            Say(pc, 0, "觉醒【" + sss + "】需要：$R$R" + o, "强化店");
            foreach (var item in stuffs)
            {
                if (CountItem(pc, item.Key) < item.Value)
                {
                    Say(pc, 131, "唔。。你似乎没带够材料呢", "强化店");
                    return;
                }
            }
            if (Select(pc, "确定要觉醒【" + sss + "】吗？", "", "是的，觉醒它", "还是算了") == 1)
            {
                foreach (var item in stuffs)
                {
                    if (CountItem(pc, item.Key) < item.Value)
                    {
                        Say(pc, 131, "唔。。你似乎没带够材料呢", "强化店");
                        return;
                    }
                    TakeItem(pc, item.Key, item.Value);
                }
                Equip.ChangeMode2 = true;
                SagaMap.Network.Client.MapClient.FromActorPC(pc).SendSystemMessage("你的【" + Equip.BaseData.name + "】的样子突然变得很奇怪！！");
                SagaMap.Skill.SkillHandler.Instance.ShowEffectOnActor(pc, 5266);
                SagaMap.Skill.SkillHandler.Instance.ShowEffectOnActor(pc, 5204);
                SagaMap.Network.Client.MapClient.FromActorPC(pc).SendItemInfo(Equip);
            }

        }
        void 强化转移(ActorPC pc)
        {

            Say(pc, 0, "请先将目标装备装备在身上。$R$R※强化转移只能在$CW同种类型$CD之间$R$CO（上衣和连衣裙可以互转为特例）$CD$R$R※转移后$CC只保留强化次数$CD，$R玩家需要前往强化店$CB重置强化$CD，$R才可以恢复强化属性。$R$R$CM※目标装备的强化次数会被覆盖！！！$CD", "请注意！");
            Say(pc, 0, "※$CR注意！$CD$R$R由于保留原装备的强化转移存在BUG，$R现在转移强化需要$CC消灭原始装备！$CD", "请注意！");
            if (Select(pc, "注意！转移强化会回收原装备", "", "好的，继续转移强化", "还是算了") == 2)
                return;
            SagaDB.Item.Item 目标装备 = new SagaDB.Item.Item();
            Dictionary<uint, ushort> stuffs = new Dictionary<uint, ushort>();
            EnumEquipSlot SltSolt = EnumEquipSlot.CHEST_ACCE;
            string name = "无";
            List<SagaDB.Item.Item> Equips = new List<SagaDB.Item.Item>();
            List<string> names = new List<string>();
            List<EnumEquipSlot> slots = new List<EnumEquipSlot>();
            slots.Add(EnumEquipSlot.CHEST_ACCE);
            slots.Add(EnumEquipSlot.RIGHT_HAND);
            slots.Add(EnumEquipSlot.LEFT_HAND);
            slots.Add(EnumEquipSlot.UPPER_BODY);

            foreach (EnumEquipSlot i in slots)
            {
                if (pc.Inventory.Equipments.ContainsKey(i))
                {
                    SagaDB.Item.Item item = pc.Inventory.Equipments[i];
                    string ts = "武器";
                    if (i == EnumEquipSlot.CHEST_ACCE) ts = "项链";
                    if (i == EnumEquipSlot.UPPER_BODY) ts = "衣服";
                    if (i == EnumEquipSlot.LEFT_HAND)
                    {
                        ts = "副手";
                        if (item.BaseData.itemType != ItemType.BOW && item.BaseData.itemType != ItemType.GUN
                         && item.BaseData.itemType != ItemType.RIFLE && item.BaseData.itemType != ItemType.DUALGUN)
                            continue;
                    }
                    string ss = item.BaseData.name;
                    if (item.BaseData.name.Length > 11)
                        ss = item.BaseData.name.Substring(0, 11);
                    name = "【" + ts + "】" + "【" + ss + "】";
                    if (item.Refine <= 5 && !item.ChangeMode2)
                    {
                        Equips.Add(item);
                        names.Add(name);
                    }
                }
            }

            if (names.Count < 1)
            {
                Say(pc, 0, "身上没有装备符合条件的目标装备", "");
                return;
            }
            names.Add("离开");
            int Set = Select(pc, "请选择要转移到的【目标装备】", "", names.ToArray());
            if (Set == names.Count) return;
            目标装备 = Equips[Set - 1];
            SltSolt = 目标装备.EquipSlot[0];
            ItemType it = 目标装备.BaseData.itemType;


            List<SagaDB.Item.Item> OriEquips = new List<SagaDB.Item.Item>();
            List<string> Orinames = new List<string>();
            foreach (var item in pc.Inventory.Items[ContainerType.BODY])
            {
                if ((it == item.BaseData.itemType ||
                    (it == ItemType.ARMOR_UPPER && item.BaseData.itemType == ItemType.ONEPIECE) || //连衣裙和上衣可以互转，为特例
                    (it == ItemType.ONEPIECE && item.BaseData.itemType == ItemType.ARMOR_UPPER))
                    && item.Refine >= 1 && item.Refine <= 80)
                {
                    OriEquips.Add(item);
                    name = "【" + item.BaseData.name + "】强化次数：" + item.Refine.ToString();
                    Orinames.Add(name);
                }
            }
            Orinames.Add("放弃");
            if (OriEquips.Count < 1)
            {
                Say(pc, 131, "你身上没有可以转移强化的装备", "");
                return;
            }
            int set = Select(pc, "※请选择需要进行【强化转移】的原装备", "", Orinames.ToArray());
            if (set == Orinames.Count) return;

            SagaDB.Item.Item es = OriEquips[set - 1];

            stuffs = GetResetStuffs(es, es.Refine);
            string o = "";
            foreach (var item in stuffs)
            {
                SagaDB.Item.Item s = ItemFactory.Instance.GetItem(item.Key);
                o += s.BaseData.name + " " + item.Value + "个$R";
            }
            o += "成功率：100％";
            Say(pc, 0, "从$R-$CL【" + es.BaseData.name + "】$CD$R转移强化到$R-$CB【" + 目标装备.BaseData.name + "】$CD$R转移强化数：$CR" + es.Refine + "$CD$R$R需要：$R$R" + o, "强化店");
            int sse = Select(pc, "确定要转移吗？", "", "确定", "确定，并使用【外观转移石】", "算了");
            if (sse <= 2)
            {
                foreach (var item in stuffs)
                {
                    if (CountItem(pc, item.Key) < item.Value)
                    {
                        Say(pc, 131, "唔。。你似乎没带够材料呢", "强化店");
                        return;
                    }
                    TakeItem(pc, item.Key, item.Value);
                }
                if (sse == 2)//外观转移石
                {
                    if (CountItem(pc, 960000080) < 1)
                    {
                        Say(pc, 131, "没有【外观转移石】$R就不能进行转移外观的强化转移。", "强化店");
                        return;
                    }
                    if (es.PictID == 0)//原外观没有合成过外观
                    {
                        Say(pc, 131, "原装备似乎没有合成过外观呢。", "强化店");
                        return;
                    }
                    TakeItem(pc, 960000080, 1);
                    目标装备.PictID = es.PictID;//目标装备的外观
                }
                ushort re = es.Refine;
                目标装备.Refine = re;
                PlaySound(pc, 3264, false, 100, 50);
                TakeItemBySlot(pc, es.Slot, 1);
                SagaMap.Network.Client.MapClient.FromActorPC(pc).SendSystemMessage("【" + es.BaseData.name + "】的强化次数转移到" + "【" + 目标装备.BaseData.name + "】上了。");
                SagaMap.Network.Client.MapClient.FromActorPC(pc).SendItemInfo(目标装备);
                SagaMap.PC.StatusFactory.Instance.CalcStatus(pc);
                SagaMap.Network.Client.MapClient.FromActorPC(pc).SendPlayerInfo();

                SagaMap.Network.Client.MapClient.FromActorPC(pc).TitleProccess(pc, 38, 1);
                if (re >= 41)
                    SagaMap.Network.Client.MapClient.FromActorPC(pc).TitleProccess(pc, 39, 1);
                if (re >= 61)
                    SagaMap.Network.Client.MapClient.FromActorPC(pc).TitleProccess(pc, 40, 1);
            }
        }
        void 重置强化(ActorPC pc)
        {
            SagaDB.Item.Item Equip = new SagaDB.Item.Item();
            Dictionary<uint, ushort> stuffs = new Dictionary<uint, ushort>();
            EnumEquipSlot SltSolt = EnumEquipSlot.CHEST_ACCE;
            string name = "无";
            List<SagaDB.Item.Item> Equips = new List<SagaDB.Item.Item>();
            List<string> names = new List<string>();
            List<EnumEquipSlot> slots = new List<EnumEquipSlot>();
            slots.Add(EnumEquipSlot.CHEST_ACCE);
            slots.Add(EnumEquipSlot.RIGHT_HAND);
            slots.Add(EnumEquipSlot.LEFT_HAND);
            slots.Add(EnumEquipSlot.UPPER_BODY);
            foreach (EnumEquipSlot i in slots)
            {
                if (pc.Inventory.Equipments.ContainsKey(i))
                {
                    SagaDB.Item.Item item = pc.Inventory.Equipments[i];
                    string ts = "武器";
                    if (i == EnumEquipSlot.CHEST_ACCE) ts = "项链";
                    if (i == EnumEquipSlot.UPPER_BODY) ts = "衣服";
                    if (i == EnumEquipSlot.LEFT_HAND)
                    {
                        ts = "副手";
                        if (item.BaseData.itemType != ItemType.BOW && item.BaseData.itemType != ItemType.GUN
                         && item.BaseData.itemType != ItemType.RIFLE && item.BaseData.itemType != ItemType.DUALGUN)
                            continue;
                    }

                    string ss = item.BaseData.name;
                    if (item.BaseData.name.Length > 11)
                        ss = item.BaseData.name.Substring(0, 11);
                    name = "【" + ts + "】" + "【" + ss + "】强化次数：" + item.Refine.ToString();
                    if (item.Refine > 1)
                    {
                        Equips.Add(item);
                        names.Add(name);
                    }
                }
            }
            names.Add("离开");
            int set2 = Select(pc, "请选择要【洗强化】的部位", "", names.ToArray());
            if (set2 == names.Count) return;
            Equip = Equips[set2 - 1];
            SltSolt = Equip.EquipSlot[0];
            int fee = 0;
            /*if (pc.CInt["洗强化第一次免费2"] != 1)
            {
                Say(pc, 0, "啊，你是第一次来洗强化呢$R$R为你免费哦。", "强化店");
                fee = 0;
            }
            else
            {
                fee = Equip.Refine * 10;
                fee *= fee;
            }*/
            string sss = Equip.BaseData.name;
            string o = fee.ToString() + "G";
            Say(pc, 0, "洗强化【" + sss + "】需要：$R$R" + o, "强化店");
            if (Select(pc, "确定要洗【" + sss + "】的强化吗？", "", "是的（花费" + o + "）", "还是算啦") == 1)
            {
                if (pc.Gold >= fee)
                {
                    pc.CInt["洗强化第一次免费2"] = 1;
                    pc.Gold -= fee;
                    Equip.Refine_Sharp = 0;
                    Equip.Refine_Enchanted = 0;
                    Equip.Refine_Vitality = 0;
                    Equip.Refine_Regeneration = 0;
                    Equip.Refine_Lucky = 0;
                    Equip.Refine_Dexterity = 0;
                    Equip.Refine_ATKrate = 0;
                    Equip.Refine_MATKrate = 0;
                    Equip.Refine_Def = 0;
                    Equip.Refine_Mdef = 0;

                    int SlotR = 0;
                    for (int i = 1; i <= Equip.Refine; i++)
                    {
                        List<byte> va = new List<byte>();
                        switch (SltSolt)
                        {
                            case EnumEquipSlot.CHEST_ACCE:
                                va = new List<byte>() { 1, 1, 15, 7, 10, 8, 1, 2 };
                                break;
                            case EnumEquipSlot.RIGHT_HAND:
                                va = new List<byte>() { 2, 2, 10, 5, 5, 5, 2, 3 };
                                break;
                            case EnumEquipSlot.LEFT_HAND:
                                va = new List<byte>() { 2, 2, 10, 5, 5, 5, 2, 3 };
                                break;
                            case EnumEquipSlot.UPPER_BODY:
                                va = new List<byte>() { 1, 1, 20, 10, 7, 8, 1, 2 };
                                break;
                        }
                        string t = "武器";
                        if (SltSolt == EnumEquipSlot.CHEST_ACCE) t = "项链";
                        if (SltSolt == EnumEquipSlot.UPPER_BODY) t = "衣服";
                        if (SltSolt == EnumEquipSlot.LEFT_HAND) t = "副手";
                        if (i >= 9 && (i - 1) % 10 == 0)
                        {
                            switch (SltSolt)
                            {
                                case EnumEquipSlot.RIGHT_HAND:
                                    va = new List<byte>() { 4, 4, 1, 1 };
                                    break;
                                case EnumEquipSlot.LEFT_HAND:
                                    va = new List<byte>() { 4, 4, 1, 1 };
                                    break;
                                case EnumEquipSlot.UPPER_BODY:
                                    va = new List<byte>() { 2, 2, 2, 1 };
                                    break;
                                case EnumEquipSlot.CHEST_ACCE:
                                    va = new List<byte>() { 2, 2, 1, 2 };
                                    break;
                            }
                            List<string> status = new List<string>() { "物理攻击力＋" + va[0].ToString() + "％", "魔法攻击力＋" + va[1].ToString() + "％",
                        "物理防御力＋" + va[2].ToString() + "％", "魔法防御力＋" + va[3].ToString() + "％"};
                            SlotR = Select(pc, "请选择第" + i.ToString() + "级的强化！部位【" + t + "】的强化类型?", "", status.ToArray());
                            SlotR += 8;
                        }
                        else
                        {
                            List<string> status = new List<string>() { "打磨   (物理攻击力+"+va[0].ToString()+")","魔化   (魔法攻击力+"+va[1].ToString()+")" ,"生命力 (HP+"+va[2].ToString()+")"
                                ,"再生   (生命恢复力+"+va[5].ToString()+")","幸运   (暴击力+"+va[6].ToString()+")",
                    "灵巧   (速度+"+va[7].ToString()+")"};
                            SlotR = Select(pc, "第" + i.ToString() + "级的强化！请选择部位【" + t + "】的强化类型", "", status.ToArray());
                            //if (SlotR == status.Count)
                            //  return;
                            if (SlotR >= 4)
                                SlotR += 2;
                        }
                        int type = SlotR - 1;
                        switch (type)
                        {
                            case 0:
                                Equip.Refine_Sharp++;
                                break;
                            case 1:
                                Equip.Refine_Enchanted++;
                                break;
                            case 2:
                                Equip.Refine_Vitality++;
                                break;
                            case 5:
                                Equip.Refine_Regeneration++;
                                break;
                            case 6:
                                Equip.Refine_Lucky++;
                                break;
                            case 7:
                                Equip.Refine_Dexterity++;
                                break;
                            case 8:
                                Equip.Refine_ATKrate++;
                                break;
                            case 9:
                                Equip.Refine_MATKrate++;
                                break;
                            case 10:
                                Equip.Refine_Def++;
                                break;
                            case 11:
                                Equip.Refine_Mdef++;
                                break;
                        }
                        PlaySound(pc, 3264, false, 100, 50);
                        SagaMap.Network.Client.MapClient.FromActorPC(pc).SendSystemMessage("【" + Equip.BaseData.name + "】+" + i.ToString() + " 强化成功啦！");
                        ItemFactory.Instance.CalcRefineBouns(Equip);
                        SagaMap.Network.Client.MapClient.FromActorPC(pc).SendItemInfo(Equip);
                        SagaMap.PC.StatusFactory.Instance.CalcStatus(pc);
                        SagaMap.Network.Client.MapClient.FromActorPC(pc).SendPlayerInfo();
                    }
                }
                else
                {
                    Say(pc, 131, "唔。。你似乎没带够钱呢", "强化店");
                    return;
                }
            }
            return;
        }
        void 强化(ActorPC pc, int set,byte Rtype)
        {
            SagaDB.Item.Item Equip = new SagaDB.Item.Item();
            Dictionary<uint, ushort> stuffs = new Dictionary<uint, ushort>();
            EnumEquipSlot SltSolt = EnumEquipSlot.CHEST_ACCE;
            if (set < 3)
            {
                string name = "无";
                List<SagaDB.Item.Item> Equips = new List<SagaDB.Item.Item>();
                List<string> names = new List<string>();
                List<EnumEquipSlot> slots = new List<EnumEquipSlot>();
                slots.Add(EnumEquipSlot.CHEST_ACCE);
                slots.Add(EnumEquipSlot.RIGHT_HAND);
                slots.Add(EnumEquipSlot.UPPER_BODY);
                slots.Add(EnumEquipSlot.LEFT_HAND);
                foreach (EnumEquipSlot i in slots)
                {
                    if (pc.Inventory.Equipments.ContainsKey(i))
                    {
                        SagaDB.Item.Item item = pc.Inventory.Equipments[i];
                        string ts = "武器";
                        if (i == EnumEquipSlot.CHEST_ACCE) ts = "项链";
                        if (i == EnumEquipSlot.UPPER_BODY) ts = "衣服";
                        if (i == EnumEquipSlot.LEFT_HAND)
                        {
                            ts = "副手";
                            if (item.BaseData.itemType != ItemType.BOW && item.BaseData.itemType != ItemType.GUN
                                 && item.BaseData.itemType != ItemType.RIFLE && item.BaseData.itemType != ItemType.DUALGUN)
                                continue;
                        }
                        string ss = item.BaseData.name;
                        if (item.BaseData.name.Length > 11)
                            ss = item.BaseData.name.Substring(0, 11);
                        stuffs = GetStuffs(item, item.Refine,Rtype);
                        name = "【" + ts + "】" + "【" + ss + "】强化次数：" + item.Refine.ToString();
                        if (set == 1 && (item.Refine < 10 || (item.Refine >= 10 && !item.ChangeMode2)))
                        {
                            Equips.Add(item);
                            names.Add(name);
                        }
                        else if (set == 2 && item.Refine >= 10 && (item.ChangeMode2 || pc.Account.GMLevel > 100))
                        {
                            Equips.Add(item);
                            names.Add(name);
                        }
                    }
                }
                if (set == 2 && names.Count < 1)
                {
                    Say(pc, 0, "不好意思...$R$R您似乎没有【觉醒】状态的装备呢。", "强化店");
                    return;
                }
                if (names.Count < 1)
                {
                    Say(pc, 0, "不好意思...$R$R您似乎没有在强化部位穿戴装备呢。", "强化店");
                    return;
                }
                names.Add("离开");
                int set2 = Select(pc, "请选择要强化的部位", "", names.ToArray());
                if (set2 == names.Count) return;
                Equip = Equips[set2 - 1];

                ushort refi = Equip.Refine;
                ushort recount = (ushort)(Equip.Refine_ATKrate + Equip.Refine_Def + Equip.Refine_Dexterity + Equip.Refine_Enchanted + Equip.Refine_Lucky +
                    Equip.Refine_MATKrate + Equip.Refine_Mdef + Equip.Refine_Regeneration + Equip.Refine_Sharp + Equip.Refine_Vitality);
                if (recount > refi)
                {
                    Say(pc, 131, "你的装备存在问题！$R请先重置强化后再来试哦。", "强化店");
                    SInt[pc.Name + "强化异常！" + Equip.BaseData.name + "，强化次数：" + refi] = recount;
                    return;
                }

                SltSolt = Equip.EquipSlot[0];
                //if (Equip.Refine > 10 && !Equip.ChangeMode2) return;
                stuffs = GetStuffs(Equip, Equip.Refine,Rtype);
                int rate = GetRate(Equip.Refine,Rtype);
                if (!Equip.ChangeMode2 && Equip.Refine > 10)
                    rate /= 2;
                string o = "";
                foreach (var item in stuffs)
                {
                    SagaDB.Item.Item s = ItemFactory.Instance.GetItem(item.Key);
                    o += s.BaseData.name + " " + item.Value + "个$R";
                }
                o += "费用： " + calcCost(Equip.Refine).ToString() + "G";
                o += "$R$R成功率约：" + rate.ToString() + "％";

                string sss = Equip.BaseData.name;
                if (Equip.BaseData.name.Length > 11)
                    sss = Equip.BaseData.name.Substring(0, 11);
                Say(pc, 0, "强化【" + sss + "】需要：$R$R" + o, "强化店");

                if (pc.Gold < calcCost(Equip.Refine))
                {
                    Say(pc, 131, "唔。。你似乎没带够钱呢", "强化店");
                    return;
                }

                foreach (var item in stuffs)
                {
                    if (CountItem(pc, item.Key) < item.Value)
                    {
                        Say(pc, 131, "唔。。你似乎没带够材料呢", "强化店");
                        return;
                    }
                }
                int SlotR = 0;

                List<byte> va = new List<byte>();
                switch (SltSolt)
                {
                    case EnumEquipSlot.CHEST_ACCE:
                        va = new List<byte>() { 1, 1, 15, 7, 10, 8, 1, 2 };
                        break;
                    case EnumEquipSlot.RIGHT_HAND:
                        va = new List<byte>() { 2, 2, 10, 5, 5, 5, 2, 3 };
                        break;
                    case EnumEquipSlot.LEFT_HAND:
                        va = new List<byte>() { 2, 2, 10, 5, 5, 5, 2, 3 };
                        break;
                    case EnumEquipSlot.UPPER_BODY:
                        va = new List<byte>() { 1, 1, 20, 10, 7, 8, 1, 2 };
                        break;
                }
                string t = "武器";
                if (SltSolt == EnumEquipSlot.CHEST_ACCE) t = "项链";
                if (SltSolt == EnumEquipSlot.UPPER_BODY) t = "衣服";
                if (SltSolt == EnumEquipSlot.LEFT_HAND) t = "副手";

                if (Equip.Refine >= 10 && Equip.Refine % 10 == 0)
                {
                    switch (SltSolt)
                    {
                        case EnumEquipSlot.RIGHT_HAND:
                            va = new List<byte>() { 4, 4, 1, 1 };
                            break;
                        case EnumEquipSlot.LEFT_HAND:
                            va = new List<byte>() { 4, 4, 1, 1 };
                            break;
                        case EnumEquipSlot.UPPER_BODY:
                            va = new List<byte>() { 2, 2, 2, 1 };
                            break;
                        case EnumEquipSlot.CHEST_ACCE:
                            va = new List<byte>() { 2, 2, 1, 2 };
                            break;
                    }
                    List<string> status = new List<string>() { "物理攻击力＋" + va[0].ToString() + "％", "魔法攻击力＋" + va[1].ToString() + "％",
                        "物理防御力＋" + va[2].ToString() + "％", "魔法防御力＋" + va[3].ToString() + "％","退出"};
                    SlotR = Select(pc, "特殊强化级别！部位【" + t + "】的强化类型?", "", status.ToArray());
                    if (SlotR == status.Count)
                        return;
                    SlotR += 8;
                }
                else
                {
                    List<string> status = new List<string>() { "打磨   (物理攻击力+"+va[0].ToString()+")","魔化   (魔法攻击力+"+va[1].ToString()+")" ,"生命力 (HP+"+va[2].ToString()+")",
                    "精准   (物理命中+"+va[3].ToString()+")[命中提升暴击伤害]（暂时关闭）","过载   (魔法命中+"+va[4].ToString()+")[命中提升暴击伤害]（暂时关闭）","再生   (生命恢复力+"+va[5].ToString()+")","幸运   (暴击+"+va[6].ToString()+")[暴击提升暴击几率]",
                    "灵巧   (速度+"+va[7].ToString()+")[提升攻击/施法速度]","退出"};
                    SlotR = Select(pc, "请选择部位【" + t + "】的强化类型", "", status.ToArray());
                    if (SlotR == 4 || SlotR == 5)
                        return;
                    if (SlotR == status.Count)
                        return;
                }
                //if (Equip.Refine > 10 && !Equip.ChangeMode2) return;
                if (pc.Gold < calcCost(Equip.Refine))
                {
                    Say(pc, 131, "唔。。你似乎没带够钱呢", "强化店");
                    return;
                }
                pc.Gold -= calcCost(Equip.Refine);
                foreach (var item in stuffs)
                {
                    if (CountItem(pc, item.Key) < item.Value)
                    {
                        Say(pc, 131, "唔。。你似乎没带够材料呢", "强化店");
                        return;
                    }
                    TakeItem(pc, item.Key, item.Value);
                }

                ushort res = Equip.Refine;
                if (res == 10)
                    SagaMap.Network.Client.MapClient.FromActorPC(pc).TitleProccess(pc, 41, 1);
                if (res == 20)
                    SagaMap.Network.Client.MapClient.FromActorPC(pc).TitleProccess(pc, 42, 1);
                if (res == 30)
                    SagaMap.Network.Client.MapClient.FromActorPC(pc).TitleProccess(pc, 43, 1);
                if (res == 40)
                    SagaMap.Network.Client.MapClient.FromActorPC(pc).TitleProccess(pc, 44, 1);
                if (res == 50)
                    SagaMap.Network.Client.MapClient.FromActorPC(pc).TitleProccess(pc, 45, 1);
                if (res == 60)
                    SagaMap.Network.Client.MapClient.FromActorPC(pc).TitleProccess(pc, 46, 1);
                if (res == 70)
                    SagaMap.Network.Client.MapClient.FromActorPC(pc).TitleProccess(pc, 47, 1);

                if (Global.Random.Next(1, 100) <= rate)
                {
                    int type = SlotR - 1;
                    switch (type)
                    {
                        case 0:
                            Equip.Refine_Sharp++;
                            break;
                        case 1:
                            Equip.Refine_Enchanted++;
                            break;
                        case 2:
                            Equip.Refine_Vitality++;
                            break;
                        case 5:
                            Equip.Refine_Regeneration++;
                            break;
                        case 6:
                            Equip.Refine_Lucky++;
                            break;
                        case 7:
                            Equip.Refine_Dexterity++;
                            break;
                        case 8:
                            Equip.Refine_ATKrate++;
                            break;
                        case 9:
                            Equip.Refine_MATKrate++;
                            break;
                        case 10:
                            Equip.Refine_Def++;
                            break;
                        case 11:
                            Equip.Refine_Mdef++;
                            break;
                    }
                    if (pc.Account.GMLevel > 200)
                    {
                        if (Select(pc, "是否使用GM权限？", "", "自选强化次数", "不用了") == 1)
                        {
                            ushort re = ushort.Parse(InputBox(pc, "请输入要强化的数量", InputType.Bank));
                            Equip.Refine = re;
                        }
                    }
                    Equip.Refine++;
                    Equip.ChangeMode2 = false;
                    PlaySound(pc, 3264, false, 100, 50);
                    SagaMap.Network.Client.MapClient.FromActorPC(pc).SendItemInfo(Equip);
                    SagaMap.Network.Client.MapClient.FromActorPC(pc).SendSystemMessage("【" + Equip.BaseData.name + "】+" + Equip.Refine.ToString() + " 强化成功啦！");
                }
                else
                {
                    PlaySound(pc, 2863, false, 100, 50);
                    Equip.ChangeMode2 = false;
                    Say(pc, 131, "【" + Equip.BaseData.name + "】$R$R" + "强化失败了");
                    if (Equip.Refine <= 20) GiveItem(pc, 960000010, 1);
                    else if (Equip.Refine <= 30) GiveItem(pc, 960000011, 1);
                    else if (Equip.Refine <= 40) GiveItem(pc, 960000012, 1);
                    else if (Equip.Refine <= 50) GiveItem(pc, 960000013, 1);
                    else if (Equip.Refine <= 60) GiveItem(pc, 960000014, 1);
                    else if (Equip.Refine <= 70) GiveItem(pc, 960000015, 1);
                    else if (Equip.Refine <= 80) GiveItem(pc, 960000016, 1);
                    else if (Equip.Refine <= 90) GiveItem(pc, 960000017, 1);
                    else if (Equip.Refine <= 100) GiveItem(pc, 960000018, 1);
                    else if (Equip.Refine <= 110) GiveItem(pc, 960000019, 1);
                    else if (Equip.Refine <= 120) GiveItem(pc, 960000020, 1);
                }

                ItemFactory.Instance.CalcRefineBouns(Equip);
                SagaMap.Network.Client.MapClient.FromActorPC(pc).SendItemInfo(Equip);
                SagaMap.PC.StatusFactory.Instance.CalcStatus(pc);
                SagaMap.Network.Client.MapClient.FromActorPC(pc).SendPlayerInfo();
            }
            switch (Select(pc, "要不要继续强化？", "", "是的", "不要"))
            {
                case 1:
                    强化(pc, set,Rtype);
                    break;

                case 2:
                    break;
            }
        }
        int GetRate(int refine,byte RType)
        {
            int rate = 100;
            if (refine < 10)
                rate = 100;
            else if (refine < 20)
                rate = 90;
            else if (refine < 30)
                rate = 80;
            else if (refine < 40)
                rate = 68;
            else if (refine < 50)
                rate = 55;
            else if (refine < 60)
                rate = 41;
            else if (refine < 70)
                rate = 30;
            else if (refine < 80)
                rate = 20;
            else if (refine < 90)
                rate = 20;
            else if (refine < 100)
                rate = 12;
            else if (refine < 110)
                rate = 12;
            else if (refine < 120)
                rate = 12;
            if(RType == 1)
            {
                if (refine < 10)
                    rate = 100;
                else if (refine < 20)
                    rate = 100;
                else if (refine < 30)
                    rate = 100;
                else if (refine < 40)
                    rate = 90;
                else if (refine < 50)
                    rate = 70;
                else if (refine < 60)
                    rate = 50;
                else if (refine < 70)
                    rate = 40;
                else if (refine < 80)
                    rate = 30;
                else if (refine < 90)
                    rate = 30;
                else if (refine < 100)
                    rate = 20;
                else if (refine < 110)
                    rate = 20;
                else if (refine < 120)
                    rate = 20;
            }
            return rate;
        }
        bool checkRequirement(SagaDB.Item.Item Equip)
        {

            return true;
        }
        Dictionary<uint, ushort> GetFeverStuffs(SagaDB.Item.Item Equip, int refine)
        {
            Dictionary<uint, ushort> stuff = new Dictionary<uint, ushort>();
            ushort count = (ushort)(refine / 10);
            uint itemID = 960000010;
            if (refine <= 20)
                itemID += 0;
            else if (refine <= 30)
                itemID += 1;
            else if (refine <= 40)
                itemID += 2;
            else if (refine <= 50)
                itemID += 3;
            else if (refine <= 60)
                itemID += 4;
            else if (refine <= 70)
                itemID += 5;
            else if (refine <= 80)
                itemID += 6;
            else if (refine <= 90)
                itemID += 7;
            else if (refine <= 100)
                itemID += 8;
            else if (refine <= 110)
                itemID += 9;
            else if (refine <= 120)
                itemID += 10;
            stuff.Add(itemID, count);
            return stuff;
        }
        Dictionary<uint, ushort> GetResetStuffs(SagaDB.Item.Item Equip, int refine)
        {
            Dictionary<uint, ushort> stuff = new Dictionary<uint, ushort>();
            if (Equip.Refine >= 1 && Equip.Refine <= 40)
                stuff.Add(960000050, 1);
            else if (Equip.Refine >= 41 && Equip.Refine <= 60)
                stuff.Add(960000051, 1);
            else if (Equip.Refine >= 61 && Equip.Refine <= 80)
                stuff.Add(960000052, 1);
            return stuff;
        }
        Dictionary<uint, ushort> GetStuffs(SagaDB.Item.Item Equip, int refine,byte RType)
        {
            Dictionary<uint, ushort> stuff = new Dictionary<uint, ushort>();
            ushort count = 1;
            uint itemID = 10000000;
            if (refine < 10)
                count = 0;
            else if (refine < 20)
                count = 1;
            else if (refine < 30)
                count = 2;
            else if (refine < 40)
                count = 4;
            else if (refine < 50)
                count = 5;
            else if (refine < 60)
                count = 6;
            else if (refine < 70)
                count = 10;
            else if (refine < 80)
                count = 15;
            else if (refine < 90)
                count = 22;
            else if (refine < 100)
                count = 30;
            else if (refine < 110)
                count = 40;
            else if (refine < 120)
                count = 50;
            switch (Equip.EquipSlot[0])
            {
                case EnumEquipSlot.CHEST_ACCE:
                    itemID = 960000000;
                    break;
                case EnumEquipSlot.RIGHT_HAND:
                    itemID = 960000001;
                    break;
                case EnumEquipSlot.LEFT_HAND:
                    itemID = 960000001;
                    break;
                case EnumEquipSlot.UPPER_BODY:
                    itemID = 960000002;
                    break;
            }
            if(RType == 1)
                itemID += 3;
            stuff.Add(itemID, count);
            return stuff;
        }
        int calcCost(int refine)
        {
            int cost = 1000;
            if (refine < 10)
                cost = 1000;
            else if (refine < 20)
                cost = 5000;
            else if (refine < 30)
                cost = 20000;
            else if (refine < 40)
                cost = 100000;
            else if (refine < 50)
                cost = 200000;
            else if (refine < 60)
                cost = 400000;
            else if (refine < 70)
                cost = 1000000;
            else if (refine < 80)
                cost = 2000000;
            else if (refine < 90)
                cost = 4000000;
            else if (refine < 100)
                cost = 10000000;
            else if (refine < 110)
                cost = 15000000;
            else if (refine < 120)
                cost = 20000000;
            return cost;
        }
    }
}
