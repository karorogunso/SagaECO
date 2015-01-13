using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaWorld.Packets.Server
{
    public class SSMG_CHAR_CREATE_ACK : Packet
    {
        /*
         * 0x00000000: 撹孔 
        0xffffffa0: GAME_SMSG_CHRCREATE_E_NAME_BADCHAR,"キャラクタ?`兆に聞喘できない猟忖が聞われています" 
        0xffffff9f: GAME_SMSG_CHRCREATE_E_NAME_TOO_SHORT,"キャラクタ?`兆が玉すぎます" 
        0xffffff9e: GAME_SMSG_CHRCREATE_E_NAME_CONFLICT,"屡に揖じ兆念のキャラクタ?`が贋壓します" 
        0xffffff9d: GAME_SMSG_CHRCREATE_E_ALREADY_SLOT,"屡にキャラクタ?`が贋壓します" 
        0xffffff9c: GAME_SMSG_CHRCREATE_E_NAME_TOO_LONG,"キャラクタ?`兆が?Lすぎます" 
        ソレ參翌　: GAME_SMSG_CHRCREATE_E_ERROR,"音苧なキャラ恬撹エラ?`(%d)" 

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

