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
    public class BitMask_Long<T>
    {
        BitMask_Long ori;
        public BitMask_Long()
        {
            this.ori = new BitMask_Long();
        }

        public BitMask_Long(BitMask_Long ori)
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
        /// 此子掩码64位整数值
        /// </summary>
        public ulong Value
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

        public static implicit operator BitMask_Long<T>(BitMask_Long ori)
        {
            return new BitMask_Long<T>(ori);
        }
    }

    /// <summary>
    /// 子掩码标识类
    /// </summary>
    [Serializable]
    public class BitMask_Long
    {
        ulong value;

        public ulong Value { get { return this.value; } set { this.value = value; } }

        public BitMask_Long()
        {
            value = 0;
        }

        public BitMask_Long(ulong value)
        {
            this.value = value;
        }

        /// <summary>
        /// 检测某个标识
        /// </summary>
        /// <param name="Mask">标识</param>
        /// <returns>值</returns>        
        public bool Test(object Mask)
        {
            return Test((ulong)Mask);
        }

        /// <summary>
        /// 检测某个标识
        /// </summary>
        /// <param name="Mask">标识</param>
        /// <returns>值</returns>        
        public bool Test(ulong Mask)
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
            SetValue((ulong)bitmask, val);
        }

        /// <summary>
        /// 根据数值设置2^(n-1)位标识的值
        /// </summary>
        /// <param name="n">n</param>
        /// <param name="val">真值</param>        
        public void SetValueForNum(double n, bool val)
        {
            ulong bitmask = (ulong)Math.Pow(2, (n - 1));
            SetValue(bitmask, val);
        }

        /// <summary>
        /// 设定某标识的值
        /// </summary>
        /// <param name="bitmask">标识</param>
        /// <param name="val">真值</param>
        public void SetValue(ulong bitmask, bool val)
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
