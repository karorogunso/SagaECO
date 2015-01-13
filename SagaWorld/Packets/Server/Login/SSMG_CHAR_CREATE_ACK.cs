using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaWorld.Packets.Server
{
    public class SSMG_CHAR_CREATE_ACK : Packet
    {
        /*
         * 0x00000000: 成功 
        0xffffffa0: GAME_SMSG_CHRCREATE_E_NAME_BADCHAR,"キャラクター名に使用できない文字が使われています" 
        0xffffff9f: GAME_SMSG_CHRCREATE_E_NAME_TOO_SHORT,"キャラクター名が短すぎます" 
        0xffffff9e: GAME_SMSG_CHRCREATE_E_NAME_CONFLICT,"既に同じ名前のキャラクターが存在します" 
        0xffffff9d: GAME_SMSG_CHRCREATE_E_ALREADY_SLOT,"既にキャラクターが存在します" 
        0xffffff9c: GAME_SMSG_CHRCREATE_E_NAME_TOO_LONG,"キャラクター名が長すぎます" 
        ソレ以外　: GAME_SMSG_CHRCREATE_E_ERROR,"不明なキャラ作成エラー(%d)" 

        */
        public enum Result
        {
            OK = 0,
            GAME_SMSG_CHRCREATE_E_NAME_BADCHAR = -96,
            GAME_SMSG_CHRCREATE_E_NAME_CONFLICT = -98,
            GAME_SMSG_CHRCREATE_E_ALREADY_SLOT = -99
        }

        public SSMG_CHAR_CREATE_ACK()
        {
            this.data = new byte[6];
            this.offset = 2;
            this.ID = 0xA1;
        }

        public Result CreateResult
        {
            set
            {
                this.PutUInt((uint)value, 2);
            }
        }
    }
}

