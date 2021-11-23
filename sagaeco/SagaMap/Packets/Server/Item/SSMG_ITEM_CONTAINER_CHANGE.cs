using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Item;

namespace SagaMap.Packets.Server
{
    public class SSMG_ITEM_CONTAINER_CHANGE : Packet
    {
        public SSMG_ITEM_CONTAINER_CHANGE()
        {
            this.data = new byte[8];
            this.offset = 2;
            this.ID = 0x09E3;   
        }
       
        public uint InventorySlot
        {
            set
            {
                this.PutUInt(value, 2);
            }
        }

        /// <summary>
        /// GAME_SMSG_ITEM_MOVEERR1,";存在しないアイテムです";
        /// GAME_SMSG_ITEM_MOVEERR2,";アイテム数が不足しています";
        /// GAME_SMSG_ITEM_MOVEERR3,";アイテムを移動することが出来ません";
        /// GAME_SMSG_ITEM_MOVEERR4,";憑依中は装備を解除することが出来ません";
        /// GAME_SMSG_ITEM_MOVEERR5,";これ以上アイテムを所持することはできません";
        /// GAME_SMSG_ITEM_MOVEERR6,";憑依者待機中は装備を解除することが出来ません";
        /// GAME_SMSG_ITEM_MOVEERR7,";トレード中はアイテムを移動出来ません";
        /// GAME_SMSG_ITEM_MOVEERR8,";イベント中はアイテムを移動できません";
        /// </summary>
        public int Result
        {
            set
            {
                this.PutByte((byte)value, 6);
            }
        }

        public ContainerType Target
        {
            set
            {
                this.PutByte((byte)value, 7);
            }
        }

        
    }
}

