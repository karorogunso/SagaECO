using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;

namespace SagaMap.Packets.Server
{
    public class SSMG_GOLEM_SHOP_SELL_ANSWER : Packet
    {
        public SSMG_GOLEM_SHOP_SELL_ANSWER()
        {
            this.data = new byte[6];
            this.offset = 2;
            this.ID = 0x1804;
        }

        public int Result
        {
            set
            {
                this.PutByte((byte)value, 2);
            }
        }
        /*
        GAME_SMSG_GOLEM_SHOPDEALERR1,"なんらかの原因で失敗しました"
        GAME_SMSG_GOLEM_SHOPDEALERR2,"ゴーレムに出品されている商品が無くなりました"
        GAME_SMSG_GOLEM_SHOPDEALERR3,"自分のゴーレムからは買えません"
        GAME_SMSG_GOLEM_SHOPDEALERR4,"指定アイテムがありませんでした"
        GAME_SMSG_GOLEM_SHOPDEALERR5,"指定アイテムが足りません"
        GAME_SMSG_GOLEM_SHOPDEALERR6,"販売数量の変化が起こった為キャンセルされました"
        GAME_SMSG_GOLEM_SHOPDEALERR7,"所持金が足りません"
        GAME_SMSG_GOLEM_SHOPDEALERR8,"これ以上アイテムを所持することはできません"
        GAME_SMSG_GOLEM_SHOPDEALERR9,"ゴーレムの所持金が上限に達したためキャンセルされました"
        */
    }
}

