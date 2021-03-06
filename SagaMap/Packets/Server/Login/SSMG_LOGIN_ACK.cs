using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Server
{
    public class SSMG_LOGIN_ACK : Packet
    {
        /*
         * result 
           00000000: 撹孔 
           fffffffe: GAME_SMSG_LOGIN_ERR_UNKNOWN_ACC,"IDまたはパスワ�`ドが�`います" 
           fffffffd: GAME_SMSG_LOGIN_ERR_BADPASS,"IDまたはパスワ�`ドが�`います" 
           fffffffc: GAME_SMSG_LOGIN_ERR_BFALOCK,"このアカウントは�J�^�C嬬がロックされています" 
           fffffffb: GAME_SMSG_LOGIN_ERR_ALREADY,"屡にログインしています$r�F壓のログイン彜�Bをリセットいたします" 
           fffffffa: GAME_SMSG_LOGIN_ERR_IPBLOCK,"�F壓メンテナンス嶄です" 
           fffffff5: GAME_SMSG_GHLOGIN_ERR_101,"ゲ�`ム創署が隆�Bいか、旋喘豚�g俳れです。$r���g鞠�hの圭は屎塀鞠�hをお�gませください。" 
           fffffff4: GAME_SMSG_GHLOGIN_ERR_102,"�J�^されていない、または旋喘唯峭されたID です。" 
           fffffff3: GAME_SMSG_GHLOGIN_ERR_103,"�J�^されていない、または旋喘唯峭されたID です。" 
           fffffff2: GAME_SMSG_GHLOGIN_ERR_104,"�J�^されていない、または旋喘唯峭されたID です。" 
           fffffff1: GAME_SMSG_GHLOGIN_ERR_105,"ECOは屎塀サ�`ビスとなりました。$rガンホ�`のアトラクションセンタ�`で、アトラクションIDの�~原け�I尖をお��いします。" 
           fffffff0: GAME_SMSG_GHLOGIN_ERR_106,"βサ�`ビスは�K阻しました。屎塀サ�`ビス�_兵までお棋ちください。" 
           ffffffef: GAME_SMSG_GHLOGIN_ERR_107,"お��し豚�gは�K阻しました。$r個めてアトラクションセンタ�`でIDの恬撹をお��いします。" 
           ffffffee: AME_SMSG_GHLOGIN_ERR_108,"ご秘薦された仝お��しID々はクロ�`ズドベ�`タテストの協�T方、$r枠彭20,000兆��の鞠�h�K阻瘁に�k佩されたIDです。$r訊れ秘りますが、ご秘薦された仝お��しID々はそのまま侭隔していただき、$r肝指�g仏嚠協のベ�`タテストをお棋ちください。$r�┫了��g仏嚠協のベ�`タテストはECO巷塀サイトでご宛坪いたします。��" 
           ffffffed: GAME_SMSG_GHLOGIN_ERR_109,"�J�^嚠�筌┘薊`109" 
           ffffffec: GAME_SMSG_GHLOGIN_ERR_110,"�J�^嚠�筌┘薊`110" 
           それ參翌: GAME_SMSG_LOGIN_ERR_ERR,"音苧なエラ�`(%d)" 
        */
        public enum Result
        {
            OK = 0,
            GAME_SMSG_LOGIN_ERR_UNKNOWN_ACC = -2,
            GAME_SMSG_LOGIN_ERR_BADPASS = -3,
            GAME_SMSG_LOGIN_ERR_BFALOCK = -4,
            GAME_SMSG_LOGIN_ERR_ALREADY = -5,
            GAME_SMSG_LOGIN_ERR_IPBLOCK = -6
        }
        public SSMG_LOGIN_ACK()
        {
            this.data = new byte[12];
            this.offset = 2;
            this.ID = 0x11;

            this.Unknown1 = 0x0100;
            this.Unknown2 = 0x12345678;
        }

        public Result LoginResult
        {
            set
            {
                this.PutUInt((uint)value, 2);
            }
        }

        public ushort Unknown1
        {
            set
            {
                this.PutUShort(value, 6);
            }
        }

        public uint Unknown2
        {
            set
            {
                this.PutUInt(value, 8);
            }
        }
        
        /// <summary>
        /// ゲストID豚�沺�(1970定1埖1晩0�r0蛍0昼からの昼方��08/01/11より 
        /// End time of trial(second count since 1st Jan. 1970)
        /// </summary>
        public uint TestEndTime
        {
            set
            {
                this.PutUInt(value, 14);
            }
        }

    }
}

