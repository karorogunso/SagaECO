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
           00000000: ¬Œ÷ 
           fffffffe: GAME_SMSG_LOGIN_ERR_UNKNOWN_ACC,"ID‚Ü‚½‚ÍƒpƒXƒ[ƒh‚ªˆá‚¢‚Ü‚·" 
           fffffffd: GAME_SMSG_LOGIN_ERR_BADPASS,"ID‚Ü‚½‚ÍƒpƒXƒ[ƒh‚ªˆá‚¢‚Ü‚·" 
           fffffffc: GAME_SMSG_LOGIN_ERR_BFALOCK,"‚±‚ÌƒAƒJƒEƒ“ƒg‚Í”FØ‹@”\‚ªƒƒbƒN‚³‚ê‚Ä‚¢‚Ü‚·" 
           fffffffb: GAME_SMSG_LOGIN_ERR_ALREADY,"Šù‚ÉƒƒOƒCƒ“‚µ‚Ä‚¢‚Ü‚·$rŒ»Ý‚ÌƒƒOƒCƒ“ó‘Ô‚ðƒŠƒZƒbƒg‚¢‚½‚µ‚Ü‚·" 
           fffffffa: GAME_SMSG_LOGIN_ERR_IPBLOCK,"Œ»Ýƒƒ“ƒeƒiƒ“ƒX’†‚Å‚·" 
           fffffff5: GAME_SMSG_GHLOGIN_ERR_101,"ƒQ[ƒ€—¿‹à‚ª–¢•¥‚¢‚©A—˜—pŠúŠÔØ‚ê‚Å‚·B$rŠÈ’P“o˜^‚Ì•û‚Í³Ž®“o˜^‚ð‚¨Ï‚Ü‚¹‚­‚¾‚³‚¢B" 
           fffffff4: GAME_SMSG_GHLOGIN_ERR_102,"”FØ‚³‚ê‚Ä‚¢‚È‚¢A‚Ü‚½‚Í—˜—p’âŽ~‚³‚ê‚½ID ‚Å‚·B" 
           fffffff3: GAME_SMSG_GHLOGIN_ERR_103,"”FØ‚³‚ê‚Ä‚¢‚È‚¢A‚Ü‚½‚Í—˜—p’âŽ~‚³‚ê‚½ID ‚Å‚·B" 
           fffffff2: GAME_SMSG_GHLOGIN_ERR_104,"”FØ‚³‚ê‚Ä‚¢‚È‚¢A‚Ü‚½‚Í—˜—p’âŽ~‚³‚ê‚½ID ‚Å‚·B" 
           fffffff1: GAME_SMSG_GHLOGIN_ERR_105,"ECO‚Í³Ž®ƒT[ƒrƒX‚Æ‚È‚è‚Ü‚µ‚½B$rƒKƒ“ƒz[‚ÌƒAƒgƒ‰ƒNƒVƒ‡ƒ“ƒZƒ“ƒ^[‚ÅAƒAƒgƒ‰ƒNƒVƒ‡ƒ“ID‚Ì•R•t‚¯ˆ—‚ð‚¨Šè‚¢‚µ‚Ü‚·B" 
           fffffff0: GAME_SMSG_GHLOGIN_ERR_106,"ƒÀƒT[ƒrƒX‚ÍI—¹‚µ‚Ü‚µ‚½B³Ž®ƒT[ƒrƒXŠJŽn‚Ü‚Å‚¨‘Ò‚¿‚­‚¾‚³‚¢B" 
           ffffffef: GAME_SMSG_GHLOGIN_ERR_107,"‚¨ŽŽ‚µŠúŠÔ‚ÍI—¹‚µ‚Ü‚µ‚½B$r‰ü‚ß‚ÄƒAƒgƒ‰ƒNƒVƒ‡ƒ“ƒZƒ“ƒ^[‚ÅID‚Ìì¬‚ð‚¨Šè‚¢‚µ‚Ü‚·B" 
           ffffffee: AME_SMSG_GHLOGIN_ERR_108,"‚²“ü—Í‚³‚ê‚½u‚¨ŽŽ‚µIDv‚ÍƒNƒ[ƒYƒhƒx[ƒ^ƒeƒXƒg‚Ì’èˆõ”A$ræ’…20,000–¼—l‚Ì“o˜^I—¹Œã‚É”­s‚³‚ê‚½ID‚Å‚·B$r‹°‚ê“ü‚è‚Ü‚·‚ªA‚²“ü—Í‚³‚ê‚½u‚¨ŽŽ‚µIDv‚Í‚»‚Ì‚Ü‚ÜŠŽ‚µ‚Ä‚¢‚½‚¾‚«A$rŽŸ‰ñŽÀŽ{—\’è‚Ìƒx[ƒ^ƒeƒXƒg‚ð‚¨‘Ò‚¿‚­‚¾‚³‚¢B$riŽŸ‰ñŽÀŽ{—\’è‚Ìƒx[ƒ^ƒeƒXƒg‚ÍECOŒöŽ®ƒTƒCƒg‚Å‚²ˆÄ“à‚¢‚½‚µ‚Ü‚·Bj" 
           ffffffed: GAME_SMSG_GHLOGIN_ERR_109,"”FØ—\”õƒGƒ‰[109" 
           ffffffec: GAME_SMSG_GHLOGIN_ERR_110,"”FØ—\”õƒGƒ‰[110" 
           ‚»‚êˆÈŠO: GAME_SMSG_LOGIN_ERR_ERR,"•s–¾‚ÈƒGƒ‰[(%d)" 
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
        /// ¥²¥¹¥ÈID²Ð¤ê•rég         
        /// </summary>
        public uint RestTestTime
        {
            set
            {
                this.PutUInt(value, 10);
            }
        }
        
        /// <summary>
        /// ¥²¥¹¥ÈIDÆÚÏÞ¡¡(1970ÄEÔÂ1ÈÕ0•r0·Ö0ÃE«¤é¤ÎÃEý£©08/01/11¤è¤E
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

