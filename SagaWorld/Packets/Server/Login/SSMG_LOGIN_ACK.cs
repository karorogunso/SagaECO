using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaWorld.Packets.Server
{
    public class SSMG_LOGIN_ACK : Packet
    {
        /*
         * result 
           00000000: ≥…π¶ 
           fffffffe: GAME_SMSG_LOGIN_ERR_UNKNOWN_ACC,"ID§ﬁ§ø§œ•—•π•ÅE`•…§¨ﬂ`§§§ﬁ§π" 
           fffffffd: GAME_SMSG_LOGIN_ERR_BADPASS,"ID§ﬁ§ø§œ•—•π•ÅE`•…§¨ﬂ`§§§ﬁ§π" 
           fffffffc: GAME_SMSG_LOGIN_ERR_BFALOCK,"§≥§Œ•¢•´•¶•?»§œ’J‘^ôCƒ‹§¨•Ì•√•Ø§µ§ÅE∆§§§ﬁ§π" 
           fffffffb: GAME_SMSG_LOGIN_ERR_ALREADY,"º»§À•Ì•∞•§•?∑§∆§§§ﬁ§π$r¨F‘⁄§Œ•Ì•∞•§•?¥ëB§?ÅEª•√•»§§§ø§∑§ﬁ§π" 
           fffffffa: GAME_SMSG_LOGIN_ERR_IPBLOCK,"¨F‘⁄•·•?∆• •?π÷–§«§π" 
           fffffff5: GAME_SMSG_GHLOGIN_ERR_101,"•≤©`•‡¡œΩ?¨Œ¥íB§§§´°¢¿Ó∑√∆⁄Èg«–§ÅE«§π°£$r∫ÅEgµ«Âh§Œ∑Ω§œ’? Ωµ«Âh§?™úg§ﬁ§ª§Ø§¿§µ§§°£" 
           fffffff4: GAME_SMSG_GHLOGIN_ERR_102,"’J‘^§µ§ÅE∆§§§ §§°¢§ﬁ§ø§œ¿Ó∑√Õ£÷π§µ§ÅEøID §«§π°£" 
           fffffff3: GAME_SMSG_GHLOGIN_ERR_103,"’J‘^§µ§ÅE∆§§§ §§°¢§ﬁ§ø§œ¿Ó∑√Õ£÷π§µ§ÅEøID §«§π°£" 
           fffffff2: GAME_SMSG_GHLOGIN_ERR_104,"’J‘^§µ§ÅE∆§§§ §§°¢§ﬁ§ø§œ¿Ó∑√Õ£÷π§µ§ÅEøID §«§π°£" 
           fffffff1: GAME_SMSG_GHLOGIN_ERR_105,"ECO§œ’? Ω•µ©`•”•π§»§ §Í§ﬁ§∑§ø°£$r•¨•?€©`§Œ•¢•»•È•Ø•∑•Á•?ª•?ø©`§«°¢•¢•»•È•Ø•∑•Á•?D§Œº~∏∂§±ÑI¿Ì§?™Óä§§§∑§ﬁ§π°£" 
           fffffff0: GAME_SMSG_GHLOGIN_ERR_106,"¶¬•µ©`•”•π§œΩK¡À§∑§ﬁ§∑§ø°£’? Ω•µ©`•”•πÈ_ º§ﬁ§«§™¥?§¡§Ø§¿§µ§§°£" 
           ffffffef: GAME_SMSG_GHLOGIN_ERR_107,"§™‘ÅE∑∆⁄Èg§œΩK¡À§∑§ﬁ§∑§ø°£$r∏ƒ§·§∆•¢•»•È•Ø•∑•Á•?ª•?ø©`§«ID§Œ◊?…§?™Óä§§§∑§ﬁ§π°£" 
           ffffffee: AME_SMSG_GHLOGIN_ERR_108,"§¥»ÅE¶§µ§ÅEø°∏§™‘ÅE∑ID°π§œ•Ø•Ì©`•∫•…•Ÿ©`•ø•∆•π•»§Œ∂®ÅE ?°¢$rœ»◊≈20,000√Ó{î§Œµ«ÂhΩK¡À··§À∞k––§µ§ÅEøID§«§π°£$rø÷§ÅEÅEÍ§ﬁ§π§¨°¢§¥»ÅE¶§µ§ÅEø°∏§™‘ÅE∑ID°π§œ§Ω§Œ§ﬁ§ﬁÀ?÷§∑§∆§§§ø§¿§≠°¢$r¥Œªÿåg ©”Ë∂®§Œ•Ÿ©`•ø•∆•π•»§?™¥?§¡§Ø§¿§µ§§°£$r£®¥Œªÿåg ©”Ë∂®§Œ•Ÿ©`•ø•∆•π•»§œECOπ´ Ω•µ•§•»§«§¥∞∏ƒ⁄§§§ø§∑§ﬁ§π°££©" 
           ffffffed: GAME_SMSG_GHLOGIN_ERR_109,"’J‘^”ËÇ‰•®•È©`109" 
           ffffffec: GAME_SMSG_GHLOGIN_ERR_110,"’J‘^”ËÇ‰•®•È©`110" 
           §Ω§ÅE‘ÕÅE GAME_SMSG_LOGIN_ERR_ERR,"≤ª√? •®•È©`(%d)" 
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
        /// •≤•π•»ID≤–§ÍïrÈg         
        /// </summary>
        public uint RestTestTime
        {
            set
            {
                this.PutUInt(value, 10);
            }
        }
        
        /// <summary>
        /// •≤•π•»ID∆⁄œﬁ°°(1970ƒÅE‘¬1»’0ïr0∑÷0√ÅE´§È§Œ√ÅE?£©08/01/11§Ë§ÅE
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

