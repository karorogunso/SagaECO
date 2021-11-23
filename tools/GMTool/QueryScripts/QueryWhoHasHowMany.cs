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
    public class QueryWhoHasHowMany : Query 
    {
        /// <summary>
        /// 初始化資料
        /// </summary>
        /// <returns>查詢時使用的參數物件</returns>
        public override object Initial()
        {
            //設定顯示的標題，此處不能跟其他的Query重複
            this.QueryTitle = "某道具超過某數量的玩家";
            //設定選中時，顯示在下方的說明文字
            this.QueryDescription = "查詢擁有某道具(參數1)\n超過某數量(參數2)\n的所有玩家";
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
                      where r.Field<int>("ItemID") == p.ItemID && r.Field<int>("Count") >= p.Count 
                      select r;
            //將查詢完之結果回傳，讓主程式呈現結果
            return ret;
        }
    }
    /// <summary>
    /// 參數物件類別
    /// </summary>
    public class ParamObject
    {
        /// <summary>
        /// 物品編號
        /// </summary>
        private int tItemID;
        /// <summary>
        /// 物品數量
        /// </summary>
        private int tCount;
        /// <summary>
        /// 物品編號的Property
        /// </summary>
        public int ItemID { get { return this.tItemID; } set { this.tItemID = value; } }
        /// <summary>
        /// 物品數量的Property
        /// </summary>
        public int Count { get { return this.tCount; } set { this.tCount = value; } }

        //因為主程式只會顯示Public的Property，所以必須建立Property，使用者界面才能存取這個值
    }
}
