using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Server
{
    public class SSMG_POSSESSION_RESULT : Packet
    {
        public SSMG_POSSESSION_RESULT()
        {
            this.data = new byte[11];
            this.offset = 2;
            this.ID = 0x177B;   
        }
        //S18 觀測到過圖時發0x177B, 格式為uint fromID, byte PossessionPosition, uint ToID
        public uint FromID
        {
            set
            {
                this.PutUInt(value, 2);
            }
        }

        public uint ToID
        {
            set
            {
                this.PutUInt(value, 6);
            }
        }
        /// <summary>
        /// GAME_SMSG_TRANCE_TRANCEERR1,";憑依失敗 : 憑依中です";
        /// GAME_SMSG_TRANCE_TRANCEERR2,";憑依失敗 : 宿主です";
        /// GAME_SMSG_TRANCE_TRANCEERR3,";憑依失敗 : プレイヤーのみ憑依可能です";
        /// GAME_SMSG_TRANCE_TRANCEERR4,";憑依失敗 : レベルが離れすぎです";
        /// GAME_SMSG_TRANCE_TRANCEERR5,";憑依失敗 : 装備がありません";
        /// GAME_SMSG_TRANCE_TRANCEERR6,";憑依失敗 : 誰かが憑依しています";
        /// GAME_SMSG_TRANCE_TRANCEERR7,";憑依失敗 : 憑依不可能なアイテムです";
        /// GAME_SMSG_TRANCE_TRANCEERR8,";憑依失敗 : 憑依容量オーバーです";
        /// GAME_SMSG_TRANCE_TRANCEERR9,";憑依失敗 : 満員宿主です";
        /// GAME_SMSG_TRANCE_TRANCEERR10,";憑依失敗 : 攻撃されました";
        /// GAME_SMSG_TRANCE_TRANCEERR11,";憑依失敗 : 相手がいなくなりました";
        /// GAME_SMSG_TRANCE_TRANCEERR12,";憑依失敗 : 装備がはずされました";
        /// GAME_SMSG_TRANCE_TRANCEERR13,";憑依失敗 : 装備が変更されました";
        /// GAME_SMSG_TRANCE_TRANCEERR14,";憑依失敗 : 憑依容量オーバーです";
        /// GAME_SMSG_TRANCE_TRANCEERR15,";憑依失敗 : 状態異常中です";
        /// GAME_SMSG_TRANCE_TRANCEERR16,";憑依失敗 : 相手は憑依中です";
        /// GAME_SMSG_TRANCE_TRANCEERR17,";憑依失敗 : 相手はGetReadyPossession中です";
        /// GAME_SMSG_TRANCE_TRANCEERR18,";憑依失敗 : 相手は行動不能状態です";
        /// GAME_SMSG_TRANCE_TRANCEERR19,";憑依失敗 : イベント中です";
        /// GAME_SMSG_TRANCE_TRANCEERR20,";憑依失敗 : 相手とマップが違います";
        /// GAME_SMSG_TRANCE_TRANCEERR21,";憑依失敗 : 相手と離れすぎています";
        /// GAME_SMSG_TRANCE_TRANCEERR22,";憑依失敗 : 相手が憑依不許可設定中です";
        /// GAME_SMSG_TRANCE_TRANCEERR23,";憑依失敗 : 相手が闘技場モードでありません";
        /// GAME_SMSG_TRANCE_TRANCEERR24,";憑依失敗 : ヒーロー状態です";
        /// GAME_SMSG_TRANCE_TRANCEERR25,";憑依失敗 : 所属軍が違います";
        /// GAME_SMSG_TRANCE_TRANCEERR26,";憑依失敗 : 「メタモーバトル」中です";
        /// GAME_SMSG_TRANCE_TRANCEERR27,";憑依失敗 : 騎乗中です";
        /// GAME_SMSG_TRANCE_TRANCEERR28,"憑依失敗 : 憑依不可能なマップです"
        /// GAME_SMSG_TRANCE_TRANCEERR29,"憑依失敗 : 憑依できない状態です"
        /// GAME_SMSG_TRANCE_TRANCEERR30,"憑依失敗 : 相手がチャンプバトルに参戦していません"
        /// GAME_SMSG_TRANCE_TRANCEERR31,"憑依失敗 : マシナフォームのＤＥＭキャラクターに憑依することはできません"
        /// GAME_SMSG_TRANCE_TRANCEERR99,";";
        /// GAME_SMSG_TRANCE_TRANCEERR100,";憑依失敗 : 何らかの原因で出来ませんでした";
        /// </summary>
        public int Result
        {
            set
            {
                this.PutByte((byte)value, 10);
            }
        }
    }
}

