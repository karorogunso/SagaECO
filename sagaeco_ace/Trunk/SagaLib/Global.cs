using System;
using System.Collections.Generic;
using System.Text;

namespace SagaLib
{
    public enum Version
    {
        Saga6,
        Saga9,
        Saga9_2,
        Saga9_Iris,
        Saga10,
        Saga11,
        Saga12,
        Saga13,
        Saga14,
        Saga14_2,
        Saga17,
    }

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
        NONE = 0,//0,1,2,3,4,5,19,20不移动

        CHANGE_DIR = 1,
        WALK = 6,//走
        RUN = 7,//跑
        FORCE_MOVEMENT = 8,//被击退
        TURN_ANTICLOCKWISE = 9, //逆时针绕圈
        TURN_CLOCKWISE = 10,//顺时针绕圈
        JUMP = 11,//空翻
        MOVE = 12,//平移，无走路动作
        SMOKE = 13,//烟雾效果瞬移
        WARP = 14,//wiz的传送
        WARP2 = 15,//传送！
        QUICKEN = 16,//加速跑
        BATTLE_MOTION = 17,//战斗姿势移动
        VANISH = 18,//没有特效的瞬移

        WALK2 = 21,//也是走,0x15
        RUN2 = 22,//也是跑
        VANISH2 = 23,//也是没有特效的瞬移
    }

    public enum MotionType
    {
        NONE = 0,
        STAND = 0x6f,
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
        DEAD = 0x16b,
    }

    public enum TargetType
    {
        NONE,
        SELF,
        ONE,
        COORDINATES,
        CIRCLE,
        NOE,
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
        Poisen,
        Stone,
        Paralyse,
        Sleep,
        Silence,
        MoveSpeedDown,
        Confused,
        Frosen,
        Stun,
    }

    public enum Country
    {
        East = 1,
        West,
        South,
        North,
    }

    public enum AttackFlag
    {
        NONE = 0,
        HP_DAMAGE = 1,
        MP_DAMAGE = 2,
        SP_DAMAGE = 4,
        NO_DAMAGE = 0x8,
        HP_HEAL = 0x11,
        MP_HEAL = 0x22,
        SP_HEAL = 0x44,
        CRITICAL = 0x100,
        MISS = 0x200,
        AVOID = 0x400,
        AVOID2 = 0x800,
        GUARD = 0x1000,
        ATTACK_EFFECT = 0x2000,
        DIE = 0x4000,
        UNKNOWN1 = 0x80,
        UNKNOWN2 = 0x10000,
        UNKNOWN3 = 0x400000,
        UNKNOWN4 = 0x800000,
        UNKNOWN5 = 0x1000000,
        UNKNOWN6 = 0x2000000,

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
                string tmp2 = b[i].ToString("X2");
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
            public Random random = new Random(DateTime.Now.Millisecond);
            int last = 0;
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.Synchronized)]
            public int Next(int min, int max)
            {
                if (max != int.MaxValue)
                    max++;
                int value = random.Next(min, max);
                if (value == last)
                {
                    value = random.Next(min, max);
                    if (value == last)
                    {
                        value = random.Next(min, max);
                        if (value == last)
                            if (last == 0)
                            {
                                //Logger.ShowDebug("Random function(min:" + min.ToString() + ",max:" + max + ") returning value:" + last.ToString() + " for three times!", null);
                                random = new Random(DateTime.Now.Millisecond);
                            }
                            else
                                last = value;
                    }
                    else
                        last = value;
                }
                else
                    last = value;
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

            public double NextDouble()
            {
                return random.NextDouble();
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

        public static byte PosX16to8(short pos, ushort width)
        {
            double val = ((double)width / 2);
            double tmp = (((float)(pos - 50) / 100) + val);
            if (tmp < 0)
                tmp = 0;
            return (byte)tmp;
        }

        public static byte PosY16to8(short pos, ushort height)
        {
            double val = ((double)height / 2);
            double tmp = (((float)-(pos + 50) / 100) + val);
            if (tmp < 0)
                tmp = 0;
            return (byte)tmp;
        }

        public static short PosX8to16(byte pos, ushort width)
        {
            return (short)((pos - ((double)width / 2)) * 100 + 50);
        }

        public static short PosY8to16(byte pos, ushort height)
        {
            return (short)(-(pos - ((double)height / 2)) * 100 - 50);
        }


        public static uint MAX_SIGHT_RANGE = 4000; //this should be ok

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
