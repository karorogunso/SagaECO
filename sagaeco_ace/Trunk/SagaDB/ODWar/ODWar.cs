using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SagaDB.ODWar
{
    /// <summary>
    /// 都市攻防战
    /// </summary>
    public class ODWar
    {
        public class Symbol
        {
            public int id;
            public uint mobID;
            public byte x;
            public byte y;
            public uint actorID;
            public bool broken = false;
        }

        public class Wave
        {
            public int DEMChamp;
            public int DEMNormal;
        }

        uint mapID;
        Dictionary<int,int> startTime=new Dictionary<int,int>();
        Dictionary<int, Symbol> symbols = new Dictionary<int, Symbol>();
        Dictionary<uint, int> score = new Dictionary<uint, int>();
        uint symbolTrash;
        List<uint> demChamp = new List<uint>();
        List<uint> demNormal = new List<uint>();
        List<uint> boss = new List<uint>();
        Wave waveStrong;
        Wave waveWeak;
        bool started;
        
        /// <summary>
        /// 攻防战地图ID
        /// </summary>
        public uint MapID { get { return this.mapID; } set { this.mapID = value; } }

        /// <summary>
        /// 攻防战的时间
        /// </summary>
        public Dictionary<int, int> StartTime { get { return this.startTime; } }

        /// <summary>
        /// 象征MobID和位置
        /// </summary>
        public Dictionary<int, Symbol> Symbols { get { return this.symbols; } }

        /// <summary>
        /// 象征残骸MobID
        /// </summary>
        public uint SymbolTrash { get { return this.symbolTrash; } set { this.symbolTrash = value; } }

        /// <summary>
        /// DEM Champ怪列表
        /// </summary>
        public List<uint> DEMChamp { get { return this.demChamp; } }

        /// <summary>
        /// DEM 普通怪列表
        /// </summary>
        public List<uint> DEMNormal { get { return this.demNormal; } }

        /// <summary>
        /// Boss怪物列表
        /// </summary>
        public List<uint> Boss { get { return this.boss; } }

        /// <summary>
        /// 敌人比较强的攻击波
        /// </summary>
        public Wave WaveStrong { get { return this.waveStrong; } set { this.waveStrong = value; } }

        /// <summary>
        /// 敌人比较弱的攻击波
        /// </summary>
        public Wave WaveWeak { get { return this.waveWeak; } set { this.waveWeak = value; } }

        /// <summary>
        /// 攻城战是否开始
        /// </summary>
        public bool Started { get { return this.started; } set { this.started = value; } }

        /// <summary>
        /// 玩家的成绩
        /// </summary>
        public Dictionary<uint, int> Score { get { return this.score; } }
    }
}
