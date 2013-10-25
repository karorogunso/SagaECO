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
            this.data = new byte[161];
            this.offset = 14;
            this.ID = 0x0029;

            this.PutByte(0xD, 2);
            this.PutByte(0xD, 55);
            this.PutByte(0xD, 108);
        }


        public List<ActorPC> Characters
        {
            set
            {
                for (int i = 0; i < 3; i++)
                {
                    var pcs =
                        from p in value
                        where p.Slot == i
                        select p;
                    if (pcs.Count() == 0)
                        continue;
                    ActorPC pc = pcs.First();                    
                    for (int j = 0; j < 13; j++)
                    {
                        if (pc.Inventory.Equipments.ContainsKey((EnumEquipSlot)j))
                        {
                            Item item = pc.Inventory.Equipments[(EnumEquipSlot)j];
                            this.PutUInt(item.ItemID, (ushort)(3 + i * 53 + j * 4));
                        }
                    }
                }
            }
        }
    }
}

