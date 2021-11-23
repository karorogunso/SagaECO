using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data;

namespace GMTool
{
    /// <summary>
    /// 查詢用類別
    /// </summary>
    public class Query
    {
        /// <summary>
        /// 標題，將顯示於下拉式選單
        /// </summary>
        public string QueryTitle = "";
        /// <summary>
        /// 說明，將顯示於說明區塊
        /// </summary>
        public string QueryDescription = "";
        /// <summary>
        /// 蒐尋的參數物件
        /// </summary>
        public Object QueryParameter;

        public Query()
        {
            QueryParameter=Initial();
        }
        /// <summary>
        /// 初始化參數，請記得設定QueryTitle
        /// </summary>
        /// <returns>參數物件，需為一Class物件，可自訂，將會顯示在畫面中</returns>
        public virtual object Initial()
        {
            return null;
        }
        /// <summary>
        /// 執行查詢
        /// </summary>
        /// <param name="Items">資料來源</param>
        /// <returns>查詢結果</returns>
        public virtual IEnumerable Run(DataTable Items, Object arg)
        {
            return null;
        }
    }
}
