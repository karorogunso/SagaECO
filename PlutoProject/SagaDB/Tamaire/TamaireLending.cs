using SagaDB.Actor;
using SagaLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SagaDB.Tamaire
{
    public class TamaireLending
    {
        uint lender;
        byte baselv;
        byte jobtype;
        string comment;
        List<uint> renters = new List<uint>();
        DateTime postdue;

        public uint Lender { get { return this.lender; } set { this.lender = value; } }
        public byte Baselv { get { return this.baselv; } set { this.baselv = value; } }
        public byte JobType { get { return this.jobtype; } set { this.jobtype = value; } }
        public string Comment { get { return this.comment; } set { this.comment = value; } }
        public List<uint> Renters
        {
            get { return this.renters; }
            set { this.renters = value; }
        }
        public DateTime PostDue { get { return this.postdue; } set { this.postdue = value; } }

        public byte maxLendings=4;
        /// <summary>
        /// 玩家最大可借出的"心"的數量
        /// </summary>
        public byte MaxLendings { get { return this.maxLendings; } set { this.maxLendings = value; } }
    }
}