using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SagaDB.EnhanceTable
{
    public class EnhanceTable
    {
        int count, baserate, matsuri, recycle, explodeprotect, resetprotect, crystal, enhancecrystal, spcrystal, kingcrystal, okugi, shinzui;



        /// <summary>
        /// 次數
        /// </summary>
        public int Count { get { return this.count; } set { this.count = value; } }

        /// <summary>
        /// 基本機率
        /// </summary>
        public int BaseRate { get { return this.baserate; } set { this.baserate = value; } }

        /// <summary>
        /// 強化祭加成
        /// </summary>
        public int Matsuri { get { return this.matsuri; } set { this.matsuri = value; } }

        /// <summary>
        /// 回收加成
        /// </summary>
        public int Recycle { get { return this.recycle; } set { this.recycle = value; } }

        /// <summary>
        /// 防止重設加成
        /// </summary>
        public int ResetProtect { get { return this.resetprotect; } set { this.resetprotect = value; } }

        /// <summary>
        /// 防止損毀加成
        /// </summary>
        public int ExplodeProtect { get { return this.explodeprotect; } set { this.explodeprotect = value; } }

        /// <summary>
        /// 一般水晶加成
        /// </summary>
        public int Crystal { get { return this.crystal; } set { this.crystal = value; } }

        /// <summary>
        /// 強化水晶加成
        /// </summary>
        public int EnhanceCrystal { get { return this.enhancecrystal; } set { this.enhancecrystal = value; } }

        /// <summary>
        /// 超強化水晶加成
        /// </summary>
        public int SPCrystal { get { return this.spcrystal; } set { this.spcrystal = value; } }

        /// <summary>
        /// 強化王加成
        /// </summary>
        public int KingCrystal { get { return this.kingcrystal; } set { this.kingcrystal = value; } }

        /// <summary>
        /// 奧義加成
        /// </summary>
        public int Okugi { get { return this.okugi; } set { this.okugi = value; } }

        /// <summary>
        /// 神髓加成
        /// </summary>
        public int Shinzui { get { return this.shinzui; } set { this.shinzui = value; } }

    }
}
