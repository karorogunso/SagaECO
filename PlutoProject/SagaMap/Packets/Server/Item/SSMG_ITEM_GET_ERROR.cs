using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Item;

namespace SagaMap.Packets.Server
{
    public class SSMG_ITEM_GET_ERROR : Packet
    {
        public SSMG_ITEM_GET_ERROR()
        {
            this.data = new byte[7];
            this.offset = 2;
            this.ID = 0x07E6;   
        }

        public uint ActorID
        {
            set
            {
                this.PutUInt(value, 2);
            }
        }
        /// <summary>
        ///   0, GAME_SMSG_ITEM_PICKUPERR0,";アイテムを拾うことが出来ません";
        /// - 1, GAME_SMSG_ITEM_PICKUPERR1,";存在しないアイテムです";
        /// - 2, GAME_SMSG_ITEM_PICKUPERR2,";行動不能時はアイテムを拾うことが出来ません";
        /// - 3, GAME_SMSG_ITEM_PICKUPERR3,";憑依中はアイテムを拾うことが出来ません";
        /// - 4, GAME_SMSG_ITEM_PICKUPERR4,";憑依者とレベルが離れすぎているので装備出来ません";
        /// - 5, GAME_SMSG_ITEM_PICKUPERR5,";装備箇所は既に装備中です";
        /// - 6, GAME_SMSG_ITEM_PICKUPERR6,";憑依装備は定員オーバーです";
        /// - 7, GAME_SMSG_ITEM_PICKUPERR7,";憑依アイテムは装備出来ません";
        /// - 8, GAME_SMSG_ITEM_PICKUPERR8,";トレード中はアイテムを拾うことが出来ません";
        /// - 9, GAME_SMSG_ITEM_PICKUPERR9,";イベント中はアイテムを拾うことが出来ません";
        /// -10, GAME_SMSG_ITEM_PICKUPERR10,";取得権限がありません";
        /// -11, GAME_SMSG_ITEM_PICKUPERR11,";これ以上アイテムを所持することはできません";
        /// -12, GAME_SMSG_ITEM_PICKUPERR12,";憑依者取得中です";
        /// -13, GAME_SMSG_ITEM_PICKUPERR13,";拾える状態ではありません";
        /// -14, GAME_SMSG_ITEM_PICKUPERR14,";憑依アイテムは闘技場モードでないと拾えません";
        /// GAME_SMSG_ITEM_PICKUPERR15,"憑依アイテムはチャンプバトルに参戦していないと拾えません"
        /// GAME_SMSG_ITEM_PICKUPERR16,"マシナフォーム中は憑依アイテムを拾うことができません"
        /// </summary>
        public int ErrorID
        {
            set
            {
                this.PutByte((byte)value, 6);
            }
        }        
    }
}

