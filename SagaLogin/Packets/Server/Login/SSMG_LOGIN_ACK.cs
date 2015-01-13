using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaLogin.Packets.Server
{
    public class SSMG_LOGIN_ACK : Packet
    {
        /*
         * result 
           00000000: 成功 
           fffffffe: GAME_SMSG_LOGIN_ERR_UNKNOWN_ACC,"IDまたはパスワードが違います" 
           fffffffd: GAME_SMSG_LOGIN_ERR_BADPASS,"IDまたはパスワードが違います" 
           fffffffc: GAME_SMSG_LOGIN_ERR_BFALOCK,"このアカウントは認証機能がロックされています" 
           fffffffb: GAME_SMSG_LOGIN_ERR_ALREADY,"既にログインしています$r現在のログイン状態をリセットいたします" 
           fffffffa: GAME_SMSG_LOGIN_ERR_IPBLOCK,"現在メンテナンス中です" 
           fffffff5: GAME_SMSG_GHLOGIN_ERR_101,"ゲーム料金が未払いか、利用期間切れです。$r簡単登録の方は正式登録をお済ませください。" 
           fffffff4: GAME_SMSG_GHLOGIN_ERR_102,"認証されていない、または利用停止されたID です。" 
           fffffff3: GAME_SMSG_GHLOGIN_ERR_103,"認証されていない、または利用停止されたID です。" 
           fffffff2: GAME_SMSG_GHLOGIN_ERR_104,"認証されていない、または利用停止されたID です。" 
           fffffff1: GAME_SMSG_GHLOGIN_ERR_105,"ECOは正式サービスとなりました。$rガンホーのアトラクションセンターで、アトラクションIDの紐付け処理をお願いします。" 
           fffffff0: GAME_SMSG_GHLOGIN_ERR_106,"βサービスは終了しました。正式サービス開始までお待ちください。" 
           ffffffef: GAME_SMSG_GHLOGIN_ERR_107,"お試し期間は終了しました。$r改めてアトラクションセンターでIDの作成をお願いします。" 
           ffffffee: AME_SMSG_GHLOGIN_ERR_108,"ご入力された「お試しID」はクローズドベータテストの定員数、$r先着20,000名様の登録終了後に発行されたIDです。$r恐れ入りますが、ご入力された「お試しID」はそのまま所持していただき、$r次回実施予定のベータテストをお待ちください。$r（次回実施予定のベータテストはECO公式サイトでご案内いたします。）" 
           ffffffed: GAME_SMSG_GHLOGIN_ERR_109,"認証予備エラー109" 
           ffffffec: GAME_SMSG_GHLOGIN_ERR_110,"認証予備エラー110" 
           それ以外: GAME_SMSG_LOGIN_ERR_ERR,"不明なエラー(%d)" 
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
            this.data = new byte[18];
            this.offset = 14;
            this.ID = 0x20;            
        }

        public Result LoginResult
        {
            set
            {
                this.PutUInt((uint)value, 2);
            }
        }

        public uint AccountID
        {
            set
            {
                this.PutUInt(value, 6);
            }
        }

        /// <summary>
        /// ゲストID残り時間         
        /// </summary>
        public uint RestTestTime
        {
            set
            {
                this.PutUInt(value, 10);
            }
        }

        /// <summary>
        /// ゲストID期限　(1970年1月1日0時0分0秒からの秒数）08/01/11より 
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

