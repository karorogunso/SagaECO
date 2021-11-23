using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Server
{
    public class SSMG_SYSTEM_MESSAGE : Packet
    {
        public enum Messages
        {
            GAME_SMSG_RECV_SAVEPOINT_TEXT,//セーブポイントを更新しました//
            GAME_SMSG_RECV_SHORTOFMONEY_TEXT,//所持金が足りません//
            GAME_SMSG_RECV_SHORTOFDEPOSIT_TEXT,//預金額が足りません//
            GAME_SMSG_RECV_WAREHOUSECROWDED_TEXT,//混雑している為倉庫を開けません//
            GAME_SMSG_RECV_POSTUREBLOW_TEXT,//構えが「叩き」に変更されました//
            GAME_SMSG_RECV_POSTURESLASH_TEXT,//構えが「斬り」に変更されました//
            GAME_SMSG_RECV_POSTURESTAB_TEXT,//構えが「突き」に変更されました//
            GAME_SMSG_RECV_POSTURETHROW_TEXT,//構えが「投げ」に変更されました//
            GAME_SMSG_RECV_POSTUREERROR_TEXT,//構えの変更に失敗しました//
            GAME_SMSG_RECV_SKILLLEARN_SUCCESS_TEXT,//スキルを習得しました//
            GAME_SMSG_RECV_SKILLLEARN_FAILED_TEXT,//スキルの習得に失敗しました//
            GAME_SMSG_RECV_SKILLLEARN_SKILLNOTFOUND_TEXT,//スキル習得情報が存在しません//
            GAME_SMSG_RECV_SKILLLEARN_SLOT_TEXT,//スキルスロットが足りません//
            GAME_SMSG_RECV_SKILLLEARN_LEVEL_TEXT,//LVが条件を満たしていません//
            GAME_SMSG_RECV_SKILLLEARN_RACE_TEXT,//種族が条件を満たしていません//
            GAME_SMSG_RECV_SKILLLEARN_SEX_TEXT,//性別が条件を満たしていません//
            GAME_SMSG_RECV_SKILLLEARN_JOBLV_TEXT,//職業LVが条件を満たしていません//
            GAME_SMSG_RECV_SKILLLEARN_MASTERY_TEXT,//マスタリースキルを複数習得する事はできません//
            GAME_SMSG_RECV_SKILLLEARN_ELEMENT_TEXT,//相反する属性のスキルを習得する事はできません//
            GAME_SMSG_RECV_SKILLLEARN_NEEDSKILL_TEXT,//習得する為に必要なスキルを持っていません//
            GAME_SMSG_RECV_SKILLLEARN_NOTLEARNSKILL_TEXT,//習得していてはいけないスキルを持っています//
            GAME_SMSG_RECV_SKILLLEARN_ALREADY_LEARNED_TEXT,//既に習得しています//
            GAME_SMSG_RECV_SKILLLEARN_JOB_TEXT,//現在の職業では習得する事は出来ません//
            GAME_SMSG_RECV_SHOP_CARDNOTFOUND_TEXT,//クレジットカードを持っていません//
            GAME_SMSG_RECV_SHOP_SHORTOFMONEY_TEXT,//所持金が足りません//
            GAME_SMSG_RECV_SHOP_SHORTOFDEPOSIT_TEXT,//預金額が足りません//
            GAME_SMSG_RECV_BANK_DEPOSITMAX_TEXT,//預金額の上限を超えました//
            GAME_SMSG_RECV_BANK_MONEY_MAX_TEXT,//所持できる金額の上限を超えて指定しました//
            GAME_SMSG_RECV_WAREHOUSE_FULL_TEXT,//倉庫が一杯になりました//
            GAME_SMSG_RECV_PLACE_NOT_USE_TEXT,//指定した格納箇所は使用できません //
            GAME_SMSG_RECV_CAPACITY_OVER_TEXT,//キャパシティーが上限を超えました//
            GAME_SMSG_RECV_PAYLOAD_OVER_TEXT,//ペイロードが上限を超えました//
            GAME_SMSG_RECV_TRANCER_CAPACITY_OVER_TEXT,//憑依者のキャパシティーが上限を超えてます//
            GAME_SMSG_RECV_TRANCER_PAYLOAD_OVER_TEXT,//憑依者のペイロードが上限を超えてます//
            GAME_SMSG_RECV_IDENTIFY_BY_STACK_TEXT,//鑑定済みアイテムを取得した為、同じアイテムが鑑定されました//
            GAME_SMSG_RECV_SHOP_ITEMMAX_TEXT,//これ以上アイテムを所持することはできません//

        }

        public SSMG_SYSTEM_MESSAGE()
        {
            this.data = new byte[4];
            this.offset = 2;
            this.ID = 0x03F2;   
        }

        public Messages Message
        {
            set
            {
                this.PutUShort((ushort)value, 2);
            }
        }
    }
}

