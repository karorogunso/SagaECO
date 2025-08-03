using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SagaLib
{
    /// <summary>
    /// 原子掩码类的泛型封装
    /// </summary>
    /// <typeparam name="T">一个枚举类型</typeparam>
    public class BitMask<T>
    {
        BitMask ori;
        public BitMask()
        {
            this.ori = new BitMask();
        }

        public BitMask(BitMask ori)
        {
            this.ori = ori;
        }

        /// <summary>
        /// 检测某个标识
        /// </summary>
        /// <param name="Mask">标识</param>
        /// <returns>值</returns>
        public bool Test(T Mask)
        {
            return ori.Test(Mask);
        }

        /// <summary>
        /// 设定某标识的值
        /// </summary>
        /// <param name="bitmask">标识</param>
        /// <param name="val">真值</param>
        public void SetValue(T bitmask, bool val)
        {
            ori.SetValue(bitmask, val);
        }

        /// <summary>
        /// 此子掩码32位整数值
        /// </summary>
        public int Value
        {
            get
            {
                return ori.Value;
            }
            set
            {
                ori.Value = value;
            }
        }

        public static implicit operator BitMask<T>(BitMask ori)
        {
            return new BitMask<T>(ori);
        }
    }

    /// <summary>
    /// 子掩码标识类
    /// </summary>
    [Serializable]
    public class BitMask
    {
        int value;

        public int Value { get { return this.value; } set { this.value = value; } }

        public BitMask()
        {
            value = 0;
        }

        /// <summary>
        /// 由int32初始化子掩码
        /// </summary>
        /// <param name="value">值</param>
        public BitMask(int value)
        {
            this.value = value;
        }

        /// <summary>
        /// 由Boolean初始化子掩码 (32Boolean)
        /// </summary>
        /// <param name="values">真值</param>
        public BitMask(bool[] values)
        {
            this.value = 0;
            for (byte i = 0; i < values.Length; i++)
            {
                if (i >= 32)
                    break;
                this.SetValue(2 ^ i, values[i]);
            }
        }

        /// <summary>
        /// 检测某个标识
        /// </summary>
        /// <param name="Mask">标识</param>
        /// <returns>值</returns>        
        public bool Test(object Mask)
        {
            return Test((int)Mask);
        }

        /// <summary>
        /// 检测某个标识
        /// </summary>
        /// <param name="Mask">标识</param>
        /// <returns>值</returns>        
        public bool Test(int Mask)
        {
            return (value & Mask) != 0;
        }

        /// <summary>
        /// 设定某标识的值
        /// </summary>
        /// <param name="bitmask">标识</param>
        /// <param name="val">真值</param>        
        public void SetValue(object bitmask, bool val)
        {
            SetValue((int)bitmask, val);
        }

        /// <summary>
        /// 设定某标识的值
        /// </summary>
        /// <param name="bitmask">标识</param>
        /// <param name="val">真值</param>
        public void SetValue(int bitmask, bool val)
        {
            if (this.Test(bitmask) != val)
            {
                if (val)
                    value = value | bitmask;
                else
                    value = value ^ bitmask;
            }
        }
    }
}
