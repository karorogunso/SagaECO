using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

using SagaLib;
using SagaDB;
using SagaDB.Actor;
using SagaDB.Item;

namespace SagaLogin.Packets.Server
{
    public class SSMG_CHAR_EQUIP : Packet
    {
        public SSMG_CHAR_EQUIP()
        {
            if (Configuration.Instance.Version >= SagaLib.Version.Saga10)
                this.data = new byte[230];
            else
                this.data = new byte[161];
            this.offset = 14;
            this.ID = 0x0029;

            this.PutByte(0xE, 2);
            this.PutByte(0xE, 59);
            this.PutByte(0xE, 116);
            if (Configuration.Instance.Version >= SagaLib.Version.Saga10)
            {
                this.PutByte(0xE, 173);
            }
        }


        public List<ActorPC> Characters
        {
            set
            {
                int count;
                if (Configuration.Instance.Version >= SagaLib.Version.Saga10)
                    count = 4;
                else
                    count = 3;
                for (int i = 0; i < count; i++)
                {
                    var pcs =
                        from p in value
                        where p.Slot == i
                        select p;
                    if (pcs.Count() == 0)
                        continue;
                    ActorPC pc = pcs.First();                    
                    for (int j = 0; j < 15; j++)
                    {
                        if (pc.Inventory.Equipments.ContainsKey((EnumEquipSlot)j))
                        {
                            Item item = pc.Inventory.Equipments[(EnumEquipSlot)j];
                            if (item.PictID == 0)
                                this.PutUInt(item.ItemID, (ushort)(3 + i * 57 + j * 4));
                            else if (item.BaseData.itemType != ItemType.PET_NEKOMATA && item.BaseData.itemType != ItemType.PARTNER && item.BaseData.itemType != ItemType.RIDE_PARTNER)
                            this.PutUInt(item.PictID, (ushort)(3 + i * 57 + j * 4));
                        }
                    }
                }
            }
        }
    }
}

