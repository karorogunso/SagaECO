using System;
using System.Collections.Generic;
using System.Text;

namespace SagaLib
{
    public enum PossessionPosition
    {
        RIGHT_HAND,
        LEFT_HAND,
        NECK,
        CHEST,
        NONE = 0xFF,
    }

    public enum MoveType
    {
        CHANGE_DIR = 1,
        WALK = 6,
        RUN = 7,
        FORCE_MOVEMENT = 8,
        WARP = 14,
    }

    public enum MotionType
    {
        TIRED = 0x70,
        BYE = 0x71,
        SPEAK = 0x83,
        NOD = 0x84,
        JOY = 0x85,
        NO_NO = 0x86,
        /// <summary>
        /// Only for logout
        /// </summary>
        SIT = 0x87,
        RELAX = 0x88,
        YAHOO = 0x89,
        JUMP = 0x8A,
        TURN1 = 0x8C,
        TURN2 = 0x8D,
        DOGEZA = 0x99,
        DISAPPOINT = 0x9e,
        BOW = 0x9f,
        SLUMP = 0xa0,
        CHAIR = 0xbe,
        BREAK = 0xd1,
        PON = 0xd2,
    }

    public enum TargetType
    {
        NONE,
        SELF,
        ONE,
        COORDINATES,
        CIRCLE,
    }

    public enum ActiveType
    {
        NONE,
        CIRCLE,
        ONE,
        WALL,
        DOUGHNUT,
    }

    public enum Elements
    {
        Neutral,
        Fire,
        Water,
        Wind,
        Earth,
        Holy,
        Dark,
    }

    public enum AbnormalStatus
    {
        None,
        Poisen,
        Stone,
        Paralyse,
        Sleep,
        Stun,
        SlowDown,
        Confused,
        Freeze,
        Faint,
    }

    public enum Country
    {
        East,
        West,
        South,
        North,
    }

    //**************************************************************
    //following code are added for mono supporting
    //**************************************************************
    public static class Conversion
    {
        public static string Hex(byte Number)
        {
            return Number.ToString("X");
        }

        public static string Hex(uint Number)
        {
            return Number.ToString("X");
        }
    }
    public static class Conversions
    {
        public static byte ToByte(string Value)
        {
            if (Value == null)
            {
                return 0;
            }
           
                long num2;
                num2 = Convert.ToInt64(Value, 0x10);
                return (byte)num2;         
           
        }

        public static int ToInteger(string Value)
        {
            if (Value == null)
            {
                return 0;
            }
               long num2;
                num2 = Convert.ToInt64(Value, 0x10);
                return (int)num2;

            
           
        }
        public static string bytes2HexString(byte[] b)
        {
            string tmp = "";
            int i;
            for (i = 0; i < b.Length; i++)
            {
                string tmp2 = Conversion.Hex(b[i]);
                if (tmp2.Length == 1) tmp2 = "0" + tmp2;
                tmp = tmp + tmp2;
            }
            return tmp;
        }
       public static string uint2HexString(uint[] b)
        {
            string tmp = "";
            int i;
            if (b == null) return "";
            for (i = 0; i < b.Length; i++)
            {
                string tmp2 = Conversion.Hex(b[i]);
                if (tmp2.Length != 8)
                {
                    for (int j = 0; j < 8 - tmp2.Length; j++)
                    {
                        tmp2 = "0" + tmp2;
                    }
                }
                tmp = tmp + tmp2;
            }
            return tmp;
        }

        public static byte[] HexStr2Bytes(string s)
        {
            byte[] b = new byte[s.Length / 2];
            int i;
            for (i = 0; i < s.Length / 2; i++)
            {
                //b[i] = Conversions.ToByte( "&H" + s.Substring( i * 2, 2 ) );
                b[i] = Conversions.ToByte(s.Substring(i * 2, 2));
            }
            return b;
        }

        public static uint[] HexStr2uint(string s)
        {
            uint[] b = new uint[s.Length / 8];
            int i;
            for (i = 0; i < s.Length / 8; i++)
            {
                //b[i] = (uint)Conversions.ToInteger("&H" + s.Substring(i * 8, 8));
                b[i] = (uint)Conversions.ToInteger(s.Substring(i * 8, 8));
            }
            return b;
        }
    }
    /// <summary>
    /// The global class contains objects that can be usefull throughout the entire application.
    /// </summary>
    public static class Global
    {
        public class RandomF
        {
            public Random random = new Random();

            public int Next(int min, int max)
            {

                int value = random.Next(min, max);
                if (value == 0)
                {
                    value = random.Next(min, max);
                    if (value == 0)
                    {
                        random = new Random();
                        value = random.Next(1, max);
                        if (value == 0) Logger.ShowDebug("Random function returning 0 for three times!", null);
                    }
                }
                return value;

            }

            public int Next()
            {

                return random.Next();

            }

            public int Next(int max)
            {

                return Next(0, max);

            }
        }
        /// <summary>
        /// Unicode encoder to encode en decode from bytes to string and visa versa.
        /// </summary>
        public static Encoding Unicode = System.Text.Encoding.UTF8;

        /// <summary>
        /// A random number generator.
        /// </summary>
        public static RandomF Random = new RandomF();

        /// <summary>
        /// Make sure the length of a string doesn't exceed a given maximum length.
        /// </summary>
        /// <param name="s">String to process.</param>
        /// <param name="length">Maximum length for the string.</param>
        /// <returns>The string trimmed to a given size.</returns>
        public static string SetStringLength(string s, int length)
        {
            // if it's too big cut it
            if (s.Length > length)
            {
                // FIX: I guess this should be s.Remove(0,length) (start from the beginning of the string)
                s = s.Remove(length, s.Length - length);
            }

            return s;
        }

        public static byte PosX16to8(short pos)
        {
            return (byte)((pos / 100) + 128);
        }

        public static byte PosY16to8(short pos)
        {
            return (byte)((-pos / 100) + 128);
        }

        public static short PosX8to16(byte pos)
        {
            return (short)((pos - 128) * 100 + 5);
        }

        public static short PosY8to16(byte pos)
        {
            return (short)(-(pos - 128) * 100 + 5);
        }


        public static uint MAX_SIGHT_RANGE = 10000; //this should be ok

        public static uint MakeSightRange(uint range)
        {
            if(range > MAX_SIGHT_RANGE) range = MAX_SIGHT_RANGE;

            return range;
        }       

        /// <summary>
        /// The global clientmananger.
        /// </summary>
        public static ClientManager clientMananger;


        /// <summary>
        /// Convert hours into task delay time
        /// </summary>
        public static int MakeHourDelay(int hours)
        {
            return 1000 * 60 * 60 * hours;
        }


        /// <summary>
        /// Convert minutes into task delay time
        /// </summary>
        public static int MakeMinDelay(int minutes)
        {
            return 1000 * 60 * minutes;
        }

        /// <summary>
        /// Convert seconds into task delay time
        /// </summary>
        public static int MakeSecDelay(int seconds)
        {
            return 1000 * seconds;
        }

        /// <summary>
        /// Convert milliseconds into task delay time
        /// </summary>
        public static int MakeMilliDelay(int milliseconds)
        {
            return milliseconds;
        }


    }
}
