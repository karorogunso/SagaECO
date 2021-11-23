using SagaDB.Item;

namespace SagaMap.Packets.Server
{
    public class SSMG_PLAYER_EQUIP_INFO : HasItemDetail
    {
        public SSMG_PLAYER_EQUIP_INFO()
        {
            this.data = new byte[215];
            this.offset = 2;
            this.ID = 0x0265;   
        }

        public Item Item
        {
            set
            {

                this.offset = 7;
                this.ItemDetail = value;
                this.PutByte((byte)(data.Length - 3), 2);
            }
        }
        public uint InventorySlot
        {
            set
            {
                this.PutUInt(value, 3);
            }
        }

        public ContainerType Container
        {
            set
            {
                this.PutByte((byte)value, 15);
            }
        }


    }
}

