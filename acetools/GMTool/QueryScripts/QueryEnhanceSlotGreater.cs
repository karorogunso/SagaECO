using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data;


namespace GMTool.bin.Debug.QueryScripts
{
    /// <summary>
    /// 查詢類別
    /// </summary>
    public class QueryEnhanceSlotGreater : Query 
    {
        /// <summary>
        /// 初始化資料
        /// </summary>
        /// <returns>查詢時使用的參數物件</returns>
        public override object Initial()
        {
            this.QueryTitle = "Item with enhance/slot count";
            //設定選中時，顯示在下方的說明文字
            this.QueryDescription = "It's a script for searching item with\n slot/enhance count greater than n";
            //建立參數物件，使用者介面將能夠修改此物件內容
            ParamObject o = new ParamObject();
            //回傳給主程式負責呈現
            return o;
        }
        /// <summary>
        /// 執行查詢時會被呼叫的函數
        /// </summary>
        /// <param name="Items">來源資料，為一DataSet</param>
        /// <param name="arg">在上面Initial回傳的參數物件，經過使用者設定後傳入</param>
        /// <returns>查詢之結果</returns>
        public override System.Collections.IEnumerable Run(DataTable  Items,Object arg)
        {
            //將參數物件由Object型態轉回原始型態，以便利用
            ParamObject p = (ParamObject)arg;
            //進行查詢，使用Linq語法
            var ret = from r in Items.AsEnumerable()
                      where r.Field<int>("EnhanceCount") >= p.Enhance && r.Field<int>("SlotCount") >= p.SlotCount 
                      select r;
            //將查詢完之結果回傳，讓主程式呈現結果
            return ret;
        }
        /// <summary>
        /// 參數物件類別
        /// </summary>
        public class ParamObject
        {
            private int enhance;
            private int slot;
            /// <summary>
            /// 物品編號的Property
            /// </summary>
            public int Enhance { get { return this.enhance; } set { this.enhance = value; } }
            /// <summary>
            /// 物品數量的Property
            /// </summary>
            public int SlotCount { get { return this.slot; } set { this.slot = value; } }

            //因為主程式只會顯示Public的Property，所以必須建立Property，使用者界面才能存取這個值
        }
    }    
}
