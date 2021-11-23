
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
    public class S953000002 : Event
    {
        public S953000002()
        {
            this.EventID = 953000002;
        }

        public override void OnEvent(ActorPC pc)
        {
            if (CountItem(pc, 953000002) >= 1)
            {
                TakeItem(pc, 953000002, 1);
                奖励(pc);
            }
        }
        void 奖励(ActorPC pc)
        {
            List<uint> itemIDs = new List<uint>() { 250134402, 250086100, 250243602, 250132502 };
            uint ItemID = itemIDs[Global.Random.Next(0, itemIDs.Count - 1)];
            SagaDB.Item.Item item = ItemFactory.Instance.GetItem(ItemID);
            item.Refine = 1;
            if (item.EquipSlot[0] == EnumEquipSlot.HEAD_ACCE)//头部
            {
                item.Str = (short)Global.Random.Next(0, 1);//STR随机(0 ~ 3)
                item.Int = (short)Global.Random.Next(0, 1);//INT随机(0 ~ 3)
                item.Dex = (short)Global.Random.Next(0, 1);//DEX随机(0 ~ 3)
                item.Mag = (short)Global.Random.Next(0, 1);//MAG随机(0 ~ 3)
                item.Vit = (short)Global.Random.Next(0, 1);//VIT随机(0 ~ 3)
                item.Agi = (short)Global.Random.Next(0, 1);//AGI随机(0 ~ 3)

                if (Global.Random.Next(0, 100) > 50)
                    item.Str = (short)Global.Random.Next(0, 2);//STR随机(0 ~ 3)
                if (Global.Random.Next(0, 100) > 50)
                    item.Int = (short)Global.Random.Next(0, 2);//INT随机(0 ~ 3)
                if (Global.Random.Next(0, 100) > 50)
                    item.Dex = (short)Global.Random.Next(0, 2);//DEX随机(0 ~ 3)
                if (Global.Random.Next(0, 100) > 50)
                    item.Mag = (short)Global.Random.Next(0, 2);//MAG随机(0 ~ 3)
                if (Global.Random.Next(0, 100) > 50)
                    item.Vit = (short)Global.Random.Next(0, 2);//VIT随机(0 ~ 3)
                if (Global.Random.Next(0, 100) > 50)
                    item.Agi = (short)Global.Random.Next(0, 2);//AGI随机(0 ~ 3)

                if (Global.Random.Next(0, 100) > 80)
                    item.Str = (short)Global.Random.Next(0, 3);//STR随机(0 ~ 3)
                if (Global.Random.Next(0, 100) > 80)
                    item.Int = (short)Global.Random.Next(0, 3);//INT随机(0 ~ 3)
                if (Global.Random.Next(0, 100) > 80)
                    item.Dex = (short)Global.Random.Next(0, 3);//DEX随机(0 ~ 3)
                if (Global.Random.Next(0, 100) > 80)
                    item.Mag = (short)Global.Random.Next(0, 3);//MAG随机(0 ~ 3)
                if (Global.Random.Next(0, 100) > 80)
                    item.Vit = (short)Global.Random.Next(0, 3);//VIT随机(0 ~ 3)
                if (Global.Random.Next(0, 100) > 80)
                    item.Agi = (short)Global.Random.Next(0, 3);//AGI随机(0 ~ 3)
				
				if(item.Str +  item.Int +  item.Dex +  item.Mag +  item.Vit +  item.Agi >= 15)
				{
					item.Name = "漆黑的" + item.BaseData.name;
				}
			
                if (pc.Account.GMLevel > 200)
                {
                    item.Str = 3;
                    item.Int = 3;
                    item.Dex = 3;
                    item.Mag = 3;
                    item.Vit = 3;
                    item.Agi = 3;
                }
            }
            if (item.EquipSlot[0] == EnumEquipSlot.BACK)//背部
            {
                item.Str = (short)Global.Random.Next(0, 1);//STR随机(0 ~ 3)
                item.Int = (short)Global.Random.Next(0, 1);//INT随机(0 ~ 3)
                item.Dex = (short)Global.Random.Next(0, 1);//DEX随机(0 ~ 3)
                item.Mag = (short)Global.Random.Next(0, 1);//MAG随机(0 ~ 3)
                item.Vit = (short)Global.Random.Next(0, 1);//VIT随机(0 ~ 3)
                item.Agi = (short)Global.Random.Next(0, 1);//AGI随机(0 ~ 3)

                if (Global.Random.Next(0, 100) > 50)
                    item.Str = (short)Global.Random.Next(0, 2);//STR随机(0 ~ 3)
                if (Global.Random.Next(0, 100) > 50)
                    item.Int = (short)Global.Random.Next(0, 2);//INT随机(0 ~ 3)
                if (Global.Random.Next(0, 100) > 50)
                    item.Dex = (short)Global.Random.Next(0, 2);//DEX随机(0 ~ 3)
                if (Global.Random.Next(0, 100) > 50)
                    item.Mag = (short)Global.Random.Next(0, 2);//MAG随机(0 ~ 3)
                if (Global.Random.Next(0, 100) > 50)
                    item.Vit = (short)Global.Random.Next(0, 2);//VIT随机(0 ~ 3)
                if (Global.Random.Next(0, 100) > 50)
                    item.Agi = (short)Global.Random.Next(0, 2);//AGI随机(0 ~ 3)

                if (Global.Random.Next(0, 100) > 80)
                    item.Str = (short)Global.Random.Next(0, 3);//STR随机(0 ~ 3)
                if (Global.Random.Next(0, 100) > 80)
                    item.Int = (short)Global.Random.Next(0, 3);//INT随机(0 ~ 3)
                if (Global.Random.Next(0, 100) > 80)
                    item.Dex = (short)Global.Random.Next(0, 3);//DEX随机(0 ~ 3)
                if (Global.Random.Next(0, 100) > 80)
                    item.Mag = (short)Global.Random.Next(0, 3);//MAG随机(0 ~ 3)
                if (Global.Random.Next(0, 100) > 80)
                    item.Vit = (short)Global.Random.Next(0, 3);//VIT随机(0 ~ 3)
                if (Global.Random.Next(0, 100) > 80)
                    item.Agi = (short)Global.Random.Next(0, 3);//AGI随机(0 ~ 3)
				
				if(item.Str +  item.Int +  item.Dex +  item.Mag +  item.Vit +  item.Agi >= 15)
				{
					item.Name = "漆黑的" + item.BaseData.name;
				}
                if (pc.Account.GMLevel > 200)
                {
                    item.Str = 3;
                    item.Int = 3;
                    item.Dex = 3;
                    item.Mag = 3;
                    item.Vit = 3;
                    item.Agi = 3;
                }
            }
            if (item.EquipSlot[0] == EnumEquipSlot.FACE_ACCE)//脸部
            {
                item.WeightUp = (short)Global.Random.Next(0, 100);//加算物理攻击随机(0 ~ 20)
                item.VolumeUp = (short)Global.Random.Next(0, 100);//加算魔法攻击随机(0 ~ 20)

                if (Global.Random.Next(0, 100) > 80)
                    item.WeightUp = (short)Global.Random.Next(0, 200);//加算物理攻击随机(0 ~ 20)
                if (Global.Random.Next(0, 100) > 80)
                    item.VolumeUp = (short)Global.Random.Next(0, 200);//加算魔法攻击随机(0 ~ 20)

				
				short atk = 0;
                short matk = 0;
				
                if (Global.Random.Next(0, 100) > 50)
                    atk = (short)Global.Random.Next(0, 1);
                if (Global.Random.Next(0, 100) > 50)
                    matk = (short)Global.Random.Next(0, 1);

                if (Global.Random.Next(0, 100) > 80)
                    atk = (short)Global.Random.Next(0, 2);
                if (Global.Random.Next(0, 100) > 80)
                    matk = (short)Global.Random.Next(0, 2);
                item.Atk1 = atk;
                item.Atk2 = atk;
                item.Atk3 = atk;
                item.MAtk = matk;
				
				if(atk>=2 || matk>= 2)
				{
					item.Name = "漆黑的" + item.BaseData.name;
					if(item.WeightUp > 180 || item.VolumeUp > 180)
					{
						item.Name = "深渊的" + item.BaseData.name;
					}
				}

                if (pc.Account.GMLevel > 200)
                {
                    item.Atk1 = 2;
                    item.Atk2 = 2;
                    item.Atk3 = 2;
                    item.MAtk = 2;
					item.WeightUp = 200;
					item.VolumeUp = 200;
                }
            }
            if (item.EquipSlot[0] == EnumEquipSlot.LOWER_BODY)//下衣
            {
                item.HP = (short)Global.Random.Next(0, 200);
                item.ASPD = (short)Global.Random.Next(0, 5);
                item.CSPD = (short)Global.Random.Next(0, 5);
                if (Global.Random.Next(0, 100) > 50)
                    item.HP = (short)Global.Random.Next(0, 500);
                if (Global.Random.Next(0, 100) > 90)
                    item.HP = (short)Global.Random.Next(0, 1000);
                if (Global.Random.Next(0, 100) > 50)
                    item.ASPD = (short)Global.Random.Next(0, 10);
                if (Global.Random.Next(0, 100) > 50)
                    item.CSPD = (short)Global.Random.Next(0, 10);
                if (Global.Random.Next(0, 100) > 80)
                    item.ASPD = (short)Global.Random.Next(0, 20);
                if (Global.Random.Next(0, 100) > 80)
                    item.CSPD = (short)Global.Random.Next(0, 20);
				
				if(item.HP > 800)
				{
					item.Name = "丰饶的" + item.BaseData.name;
				}
				if(item.ASPD > 18 || item.CSPD > 18)
				{
					item.Name = "迅捷的" + item.BaseData.name;
				}
                if (pc.Account.GMLevel > 200)
                {
                    item.HP = 1000;
                    item.ASPD = 20;
                    item.CSPD = 20;
                }
            }
            GiveItem(pc, item);
        }
    }
}


