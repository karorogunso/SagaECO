using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SagaDB.DefWar
{
    public class DefWar
    {
        public class DefWarData
        {
            uint id;
            string title;

            public override string ToString()
            {
                return this.title;
            }

            /// <summary>
            /// ID
            /// </summary>
            public uint ID { get { return this.id; } set { this.id = value; } }

            /// <summary>
            /// 任务标题
            /// </summary>
            public string Title { get { return this.title; } set { this.title = value; } }
        }

        public DefWar(uint id)
        {
            this.ID = id;
        }
        public DefWar(DefWarData baseData)
        {
            this.ID = baseData.ID;
        }

        public uint ID { get; set; }
        /// <summary>
        /// 任务序列
        /// </summary>
        public byte Number { set; get; }

        /// <summary>
        /// 任务信息
        /// </summary>
        public DefWarData Data
        {
            get
            {
                DefWarData baseData = null;
                if (baseData == null)
                {
                    if (DefWarFactory.Instance.Items.ContainsKey(this.ID))
                        baseData = DefWarFactory.Instance.Items[this.ID];
                    else
                        baseData = new DefWarData();
                }
                return baseData;
            }
        }


        /// <summary>
        /// 结果1
        /// </summary>
        public byte Result1 { set; get; }
        /// <summary>
        /// 结果2
        /// </summary>
        public byte Result2 { set; get; }

        public byte unknown1 { set; get; }
        public int unknown2 { set; get; }
        public int unknown3 { set; get; }
        public int unknown4 { set; get; }

    }
}
