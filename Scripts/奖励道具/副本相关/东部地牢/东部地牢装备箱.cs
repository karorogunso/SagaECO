
using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30210000
{
    public class S953000000 : Event
    {
        public S953000000()
        {
            this.EventID = 953000000;
        }

        public override void OnEvent(ActorPC pc)
        {
            if (CountItem(pc, 953000000) >= 1)
            {
                TakeItem(pc, 953000000, 1);
                奖励(pc);
            }
        }
        void 奖励(ActorPC pc)
        {
            List<uint> itemIDs = new List<uint>() { 250094700, 260135901, 250011595, 250229800, 260015201, 260021500, 260052801, 260052701, 260029100, 260062102, 260063100, 260012000, 260028500, 260045701, 260046400, 260073801, 260074902, 229006000, 261040600, 260096104, 260096120, 260097001, 260095900, 260092300, 261030100 };
            uint ItemID = itemIDs[Global.Random.Next(0, itemIDs.Count - 1)];
            SagaDB.Item.Item item = ItemFactory.Instance.GetItem(ItemID);
            item.Refine = 1;
            //item.Refine_Vitality = 1;
            item.Str = (short)Global.Random.Next(0, 3);//STR随机(0 ~ 3)
            item.Int = (short)Global.Random.Next(0, 3);//INT随机(0 ~ 3)
            item.Dex = (short)Global.Random.Next(0, 3);//DEX随机(0 ~ 3)
            item.Mag = (short)Global.Random.Next(0, 3);//MAG随机(0 ~ 3)
            item.Vit = (short)Global.Random.Next(0, 3);//VIT随机(0 ~ 3)
            item.Agi = (short)Global.Random.Next(0, 3);//AGI随机(0 ~ 3)
            item.WeightUp = (short)Global.Random.Next(0, 200);//加算物理攻击随机(0 ~ 20)
            item.VolumeUp = (short)Global.Random.Next(0, 200);//加算魔法攻击随机(0 ~ 20)

            if(pc.Account.GMLevel > 200)
            {
                item.Str = 3;//STR随机(0 ~ 3)
                item.Int = 3;//INT随机(0 ~ 3)
                item.Dex = 3;//DEX随机(0 ~ 3)
                item.Mag = 3;//MAG随机(0 ~ 3)
                item.Vit = 3;//VIT随机(0 ~ 3)
                item.Agi = 3;//AGI随机(0 ~ 3)
                item.WeightUp = 200;//加算物理攻击随机(0 ~ 20)
                item.VolumeUp = 200;//加算魔法攻击随机(0 ~ 20)
            }
            if (item.EquipSlot[0] == EnumEquipSlot.UPPER_BODY)//血量随机仅上衣
            {
                item.HP = (short)Global.Random.Next(0, 500);//HP随机(0 ~ 500)
				if(item.HP > 450)
				{
					item.Name = "丰饶的" + item.BaseData.name;
				}
                if (pc.Account.GMLevel > 200)
                {
                    item.HP = 500;//HP随机(0 ~ 500)
                }
                }
            if (item.EquipSlot[0] == EnumEquipSlot.CHEST_ACCE)//速度随机仅项链
            {
                item.ASPD = (short)Global.Random.Next(0, 20);//攻击速度随机(0 ~ 20)
                item.CSPD = (short)Global.Random.Next(0, 20);//吟唱速度随机(0 ~ 20)
				if(item.ASPD > 18 || item.CSPD > 18)
				{
					item.Name = "迅捷的" + item.BaseData.name;
				}
                if (pc.Account.GMLevel > 200)
                {
                    item.ASPD = 20;//攻击速度随机(0 ~ 20)
                    item.CSPD = 20;//吟唱速度随机(0 ~ 20)
                }
                }
            if (item.EquipSlot[0] == EnumEquipSlot.RIGHT_HAND || item.EquipSlot[0] == EnumEquipSlot.LEFT_HAND)//固定提升仅右手武器
            {
                item.Atk1 = 2;//乘算物理攻击力固定提升：2
                item.Atk2 = 2;//乘算物理攻击力固定提升：2
                item.Atk3 = 2;//乘算物理攻击力固定提升：2
                item.MAtk = 2;//乘算魔法攻击力固定提升：2
            }
			
			if(item.Str +  item.Int +  item.Dex +  item.Mag +  item.Vit +  item.Agi >= 15)
            {
				item.Name = "漆黑的" + item.BaseData.name;
				if(item.WeightUp > 180 || item.VolumeUp > 180)
				{
					item.Name = "深渊的" + item.BaseData.name;
				}
			}
			
			
			
            SInt[pc.Name + "抽取地牢装备箱次数"]++;
            GiveItem(pc, item);
        }
    }
}


