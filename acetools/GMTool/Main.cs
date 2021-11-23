using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using SagaDB;
using SagaDB.Actor;
using SagaDB.Item;
using System.Linq.Expressions;
using System.Collections;
using System.Threading;
using System.Xml.Linq;
using System.Text.RegularExpressions;
using SagaDB.Mob;
using SagaLib.VirtualFileSystem;
using System.Data.SQLite;
using System.IO;
using System.Diagnostics;

namespace GMTool
{
    public partial class Main : Form
    {
        ~Main()
        {
            try
            {
                Conn.Close();
            }
            catch (Exception) { }

            try
            {
                File.Delete(DBName);
            }
            catch (Exception) { }
        }

        enum Local
        {
            Chinese,
            TChinese,
            Japanese,
            English,
        }
        //private 
        private string user = "root";
        private string pass = "saga";
        private string database = "sagaeco";
        private int port = 3306;
        private string host = "127.0.0.1";
        public DataTable Items;
        private delegate void InvokeUpdateState(string s);
        private delegate void InvokeOnInitFinish();
        Local local = Local.TChinese;

        private bool hideLinqDesc = false;
        public Main()
        {
            if (Program.culture != null)
            {
                Thread.CurrentThread.CurrentUICulture = Program.culture;
                Program.culture = null;
            }
            InitializeComponent();

            if (Thread.CurrentThread.CurrentUICulture.ToString().ToLower().IndexOf("chs") != -1)
            {
                local = Local.Chinese;
                lan_CHS.Checked = true;
            }
            if (Thread.CurrentThread.CurrentUICulture.ToString().ToLower().IndexOf("jp") != -1)
            {
                local = Local.Japanese;
                lan_JP.Checked = true;
            }
            if (Thread.CurrentThread.CurrentUICulture.ToString().ToLower().IndexOf("tw") != -1 || Thread.CurrentThread.CurrentUICulture.ToString().ToLower().IndexOf("cht") != -1)
            {
                local = Local.TChinese;
                lan_CHT.Checked = true;
            }
            if (Thread.CurrentThread.CurrentUICulture.ToString().ToLower().IndexOf("hk") != -1)
            {
                local = Local.TChinese;
                lan_CHT.Checked = true;
            }
            if (Thread.CurrentThread.CurrentUICulture.ToString().ToLower().IndexOf("en") != -1)
            {
                local = Local.TChinese;
                lan_EN.Checked = true;
            }

            LoadConfig("./Config/SagaLogin.xml");
            switch (local)
            {
                case Local.TChinese:
                    Log("讀取資料中......");
                    Log("在讀取完成前請勿執行查詢");
                    break;
                case Local.Chinese:
                    Log("读取资料中......");
                    Log("在读取完成前请勿执行查询");
                    break;
                case Local.Japanese:
                    Log("データを読み込み中です......");
                    Log("データを完全にロードした前に検索をしないでください");
                    break;
            }
            Thread t = new Thread(new ThreadStart(this.Init));
            t.Start();
        }
        public void LoadConfig(string File)
        {
            try
            {
                XDocument doc = XDocument.Load(File);
                host = doc.Root.Element("dbhost").Value;
                database = doc.Root.Element("dbname").Value;
                port = int.Parse(doc.Root.Element("dbport").Value);
                user = doc.Root.Element("dbuser").Value;
                pass = doc.Root.Element("dbpass").Value;
            }
            catch (Exception)
            {
                MessageBox.Show("讀取設定時發生錯誤，可能無法正確執行，請檢查SagaLogin的設定檔");
            }
        }
        private void tabPage1_Click(object sender, EventArgs e)
        {
        }
        public void Log(string format, params object[] p)
        {
            Log(string.Format(format, p));
        }
        public void Log(string s)
        {
            if (this.LstMsg.InvokeRequired)
            {
                this.Invoke(
                  new InvokeUpdateState(this.Log), new object[] { s }
                );
            }
            else
            {
                LstMsg.Items.Add(s);
                LstMsg.TopIndex = LstMsg.Items.Count - 1;
            }
        }
        public void OnInitFinish()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(
                  new InvokeOnInitFinish(this.OnInitFinish), new object[] { }
                );
            }
            else
            {
                //foreach (var k in QueryManager.Instance.Querys)
                //{
                //    cboQuerys.Items.Add(k.Key);
                //}
                switch (local)
                {
                    case Local.TChinese:
                        Log("資料讀取完成....");
                        break;
                    case Local.Chinese:
                        Log("资料读取完成....");
                        break;
                    case Local.Japanese:
                        Log("データのロードは完了しました....");
                        break;

                }
                btnSimpleQuery.Enabled = true;
                //btnAdvanceSearch.Enabled = true;
                //btnLinqQuery.Enabled = true;
            }
        }
        string DBName = "GMTool.db";
        string ConnectionString;
        SQLiteConnection Conn;

        private string EscapeParameter(string p)
        {
            return p.Replace("'", "''");
        }

        public void Init()
        {
            ConnectionString = "Data Source=" + DBName;
            cbSQLSrc.SelectedIndex = 0;
            //連接至資料庫中 - 取回資料
            ////取得所有UserName
            DataTable dt;
            string sqlstr;
            try
            {
                sqlstr = "SELECT `char_id`,`account_id`,`name`,`race`,`gender`,`job` FROM `char`";
                DataSet tmp = this.GetDataFromDatabase(sqlstr);
                dt = tmp.Tables[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show("資料庫連線失敗，請檢查SagaLogin的設定檔");
                MessageBox.Show(ex.ToString());
                System.Environment.Exit(System.Environment.ExitCode);
                return;
            }
            //轉至SqlLite
            try
            {
                File.Delete(DBName);
            }
            catch (Exception) { }
            Conn = new SQLiteConnection(ConnectionString);
            Conn.Open();

            var cmd = new SQLiteCommand(Conn);
            try
            {
                cmd.CommandText = string.Format("Create Table `char` ({0});", string.Join(",", (from DataColumn c in dt.Columns select c.ColumnName).ToArray()));
                cmd.ExecuteNonQuery();

                SQLiteTransaction trans = Conn.BeginTransaction();
                cmd.Transaction = trans;
                foreach (DataRow row in dt.Rows)
                {
                    cmd.CommandText = string.Format("INSERT INTO `char` Values ('{0}');", string.Join("','", (from object o in row.ItemArray select EscapeParameter(o.ToString())).ToArray()));
                    cmd.ExecuteNonQuery();
                }
                trans.Commit();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }


            //讀取物品資料庫
            try
            {
                VirtualFileSystemManager.Instance.Init(FileSystems.Real, ".");
                ItemFactory.Instance.Init("./DB/item.csv", System.Text.Encoding.UTF8);
                switch (local)
                {
                    case Local.TChinese:
                        Log("已讀取{0}筆物品資料", ItemFactory.Instance.Items.Count);
                        break;
                    case Local.Chinese:
                        Log("已读取{0}笔物品资料", ItemFactory.Instance.Items.Count);
                        break;
                    case Local.Japanese:
                        Log("アイテムデータを{0}つロードしました", ItemFactory.Instance.Items.Count);
                        break;
                }
                //讀取完成物品資料後，新增至AutoComplete
                AddAutoComplete();
            }
            catch (Exception)
            {
                Log("Connot Load ItemDB");
            }

            cmd.CommandText = string.Format("Create Table `Item` ({0});", string.Join(",", new string[] { "CharID", "CharName", "ItemID", "ItemName", "Count", "Durability", "Volume", "Weight", "Slot", "EnhanceCount", "SlotCount", "SlotID" }));
            cmd.ExecuteNonQuery();

            try
            {
                ////讀取進來
                MySQLActorDB ActorDB = new MySQLActorDB(host, port, database, user, pass);
                SQLiteTransaction trans = Conn.BeginTransaction();
                cmd.Transaction = trans;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow r = dt.Rows[i];
                    try
                    {
                        ActorPC pc = ActorDB.GetChar((uint)(r["char_id"]), false);
                        //ActorDB.GetItem(pc);
                        foreach (var IItems in pc.Inventory.Items)
                        {
                            foreach (var tItem in IItems.Value)
                            {
                                if (tItem.Stack != 0)
                                {
                                    cmd.CommandText = string.Format("INSERT INTO `Item` Values ('{0}');", string.Join("','", new string[] { pc.CharID.ToString(), pc.Name, tItem.ItemID.ToString(), tItem.BaseData.name, tItem.Stack.ToString(), tItem.Durability.ToString(), tItem.BaseData.volume.ToString(), tItem.BaseData.weight.ToString(), IItems.Key.ToString(), tItem.Refine.ToString(), tItem.CurrentSlot.ToString(), tItem.Slot.ToString() }));
                                    cmd.ExecuteNonQuery();
                                }
                            }
                        }
                        foreach (var IItems in pc.Inventory.WareHouse)
                        {
                            foreach (var tItem in IItems.Value)
                            {
                                if (tItem.Stack != 0)
                                {
                                    cmd.CommandText = string.Format("INSERT INTO `Item` Values ('{0}');", string.Join("','", new string[] { pc.CharID.ToString(), pc.Name, tItem.ItemID.ToString(), tItem.BaseData.name, tItem.Stack.ToString(), tItem.Durability.ToString(), tItem.BaseData.volume.ToString(), tItem.BaseData.weight.ToString(), IItems.Key.ToString(), tItem.Refine.ToString(), tItem.CurrentSlot.ToString(), tItem.Slot.ToString() }));
                                    cmd.ExecuteNonQuery();
                                }
                            }
                        }
                        foreach (var IItems in pc.Inventory.Equipments)
                        {
                            var tItem = IItems.Value;
                            if (tItem.Stack != 0)
                            {
                                cmd.CommandText = string.Format("INSERT INTO `Item` Values ('{0}');", string.Join("','", new string[] { pc.CharID.ToString(), pc.Name, tItem.ItemID.ToString(), tItem.BaseData.name, tItem.Stack.ToString(), tItem.Durability.ToString(), tItem.BaseData.volume.ToString(), tItem.BaseData.weight.ToString(), IItems.Key.ToString(), tItem.Refine.ToString(), tItem.CurrentSlot.ToString(), tItem.Slot.ToString() }));
                                cmd.ExecuteNonQuery();
                            }
                        }

                        if (i % 100 == 0)
                        {
                            switch (local)
                            {
                                case Local.TChinese:
                                    Log("已讀取{0}/{1}筆玩家資料", i, dt.Rows.Count);
                                    break;
                                case Local.Chinese:
                                    Log("已读取{0}/{1}笔玩家资料", i, dt.Rows.Count);
                                    break;
                                case Local.Japanese:
                                    Log("プレーヤデータを{0}/{1}つロードしました", i, dt.Rows.Count);
                                    break;
                            }
                        }
                    }
                    catch (Exception)
                    { }
                }
                trans.Commit();
            }
            catch (Exception)
            {
                MessageBox.Show("Player data load failed.");
                return;
            }

            switch (local)
            {
                case Local.TChinese:
                    Log("已讀取{0}筆玩家資料", dt.Rows.Count);
                    break;
                case Local.Chinese:
                    Log("已读取{0}笔玩家资料", dt.Rows.Count);
                    break;
                case Local.Japanese:
                    Log("プレーヤデータを{0}つロードしました", dt.Rows.Count);
                    break;
            }

            //讀取所有的QueryScripts
            try
            {
                LoadScripts("./QueryScripts");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            OnInitFinish();
        }

        private void AddAutoComplete()
        {
            foreach (var i in ItemFactory.Instance.Items)
            {
                tbItemCharItemIDName.AutoCompleteCustomSource.Add(i.Value.name);
                tbItemCharItemIDName.AutoCompleteCustomSource.Add(i.Key.ToString());
            }
            Log("已增加{0}筆物品資料至自動完成", tbItemCharItemIDName.AutoCompleteCustomSource.Count);

        }
        public void LoadScripts(String path)
        {
            QueryManager.Instance.Querys.Clear();
            int c = QueryManager.Instance.LoadScript(path);
            Log("已讀取：{0} 個 Query", QueryManager.Instance.Querys.Count);
        }

        private void gvResult_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void gvResult_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private DataTable GetDataTable(string format, params object[] pars)
        {
            return GetDataTable(string.Format(format, pars));
        }
        private DataTable GetDataTable(string sql)
        {
            SQLiteCommand cmd = new SQLiteCommand(sql, Conn);
            SQLiteDataAdapter adpt = new SQLiteDataAdapter(cmd);
            DataTable ret = new DataTable();
            adpt.Fill(ret);
            return ret;
        }

        private void btnSimpleQuery_Click(object sender, EventArgs e)
        {
            if (tbSimpleQuery.Text == "")
            {
                MessageBox.Show("請輸入關鍵字，可以為ItemID、CharID、CharName、ItemName");
                return;
            }
            try
            {
                int qi = 0;
                try
                {
                    qi = int.Parse(tbSimpleQuery.Text);
                }
                catch (Exception)
                {
                }

                ShowResult(GetDataTable("SELECT * FROM `Item` WHERE ItemID = '{0}' OR CharName LIKE '%{1}%' OR ItemName LIKE '%{1}%' OR CharID = '{0}'", qi, tbSimpleQuery.Text).DefaultView);
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }

        }

        public void ShowResult(DataView result)
        {
            gvResult.DataSource = result;
            Log(string.Format("查詢執行完畢，取回：{0} 筆資料", result.Count));
        }

        public void ShowResult(IEnumerable result)
        {
            if (result.GetType().ToString().StartsWith("System.Data.EnumerableRowCollection"))
            {
                DataTable dt = new DataTable();
                //群組
                try
                {
                    foreach (var x in result)
                    {
                        DataRow r = (DataRow)x;
                        if (dt.Rows.Count == 0)
                        {
                            foreach (DataColumn c in r.Table.Columns)
                            {
                                dt.Columns.Add(c.ColumnName, c.DataType);
                            }
                        }
                        dt.Rows.Add(r.ItemArray);
                    }
                }
                catch (Exception e) { MessageBox.Show(e.ToString()); }
                gvResult.DataSource = dt;
                Log(string.Format("查詢執行完畢，取回：{0} 筆資料", gvResult.RowCount));
                return;
            }
            bindingSource1.DataSource = result;
            gvResult.DataSource = bindingSource1;
            Log(string.Format("查詢執行完畢，取回：{0} 筆資料", gvResult.RowCount));
        }

        private void bwInit_DoWork(object sender, DoWorkEventArgs e)
        {
            Init();
        }





        private void tbLinq_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void Main_Load(object sender, EventArgs e)
        {

        }

        public DataSet GetDataFromDatabase(string sql)
        {
            MySqlConnection db = new MySqlConnection(string.Format("Server={1};Port={2};Uid={3};Pwd={4};Database={0};Charset=utf8;", database, host, port, user, pass));
            db.Open();
            DataSet ret = MySqlHelper.ExecuteDataset(db, sql);
            db.Close();
            return ret;
        }

        public int ExecuteSqlToDatabase(string sql)
        {
            MySqlConnection db = new MySqlConnection(string.Format("Server={1};Port={2};Uid={3};Pwd={4};Database={0};Charset=utf8;", database, host, port, user, pass));
            db.Open();
            int ret = MySqlHelper.ExecuteNonQuery(db, sql, null);
            db.Close();
            return ret;
        }

        private void tabPage4_Click(object sender, EventArgs e)
        {

        }

        private void tabPage4_Enter(object sender, EventArgs e)
        {
            gvResult.DataSource = LoadMoneyData(chbOrderByMoney.Checked).Tables[0];
        }
        private DataSet LoadMoneyData(bool Orderby)
        {
            return LoadMoneyData("", Orderby);
        }
        private DataSet LoadMoneyData(string WhereQuery, bool Orderby)
        {
            string sql = "SELECT * FROM (SELECT `account_id` AS ID,`username` AS Name,`Bank` AS Money,'Bank' AS Type From `login` UNION " +
                "SELECT `char_id` AS ID,`name` AS Name,`Gold` AS Money,'CHARACTER' AS Type From `char`) AS MoneyTable {0}" + (Orderby ? " ORDER BY Money DESC" : "");
            string sqlstr = string.Format(sql, WhereQuery == "" ? "" : " WHERE " + WhereQuery);
            return GetDataFromDatabase(sqlstr);
        }

        private void btnSQLQuery_Click(object sender, EventArgs e)
        {
            string sql = tbSQLQuery.Text.Trim();
            try
            {
                if (cbSQLSrc.SelectedIndex == 0)
                {
                    if (sql.ToLower().IndexOf("select") == 0)
                    {
                        DataSet ret = GetDataFromDatabase(sql);
                        if (ret.Tables.Count > 0)
                        {
                            gvResult.DataSource = ret.Tables[0];
                            Log(string.Format("查詢執行完畢，取回：{0} 筆資料", ret.Tables[0].Rows.Count));
                        }
                        else
                        {
                            Log(string.Format("查無結果資料"));
                        }
                    }
                    else
                    {
                        int r = ExecuteSqlToDatabase(sql);
                        Log(string.Format("查詢執行完畢，有 {0} 筆資料受影響", r));
                    }
                }
                else
                {
                    if (sql.ToLower().IndexOf("select") != 0)
                    {
                        MessageBox.Show("select only!");
                        return;
                    }
                    ShowResult(GetDataTable(sql).DefaultView);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnMoneyQuery_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet ret = LoadMoneyData(tbMoneyQuery.Text, chbOrderByMoney.Checked);
                if (ret.Tables.Count > 0)
                {
                    gvResult.DataSource = ret.Tables[0];
                    Log(string.Format("查詢執行完畢，取回：{0} 筆資料", ret.Tables[0].Rows.Count));
                }
                else
                {
                    Log(string.Format("查無結果資料"));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private DataSet LoadItemCharName(string WhereQuery)
        {
            string sql = "SELECT `char_id`,`name` FROM `char`";
            if (WhereQuery != "")
            {
                sql = sql + " WHERE `name` LIKE '%" + WhereQuery + "%'";
            }
            return GetDataFromDatabase(sql);

        }

        private DataTable LoadCharItem(uint CharID)
        {
            return GetDataTable("SELECT * FROM `Item` WHERE `CharID` = '" + CharID + "'");
        }

        private void DeleteCharItems(uint CharID, List<CItemDelData> ItemDel)
        {
            MySQLActorDB ActorDB = new MySQLActorDB(host, port, database, user, pass);
            ActorPC pc = ActorDB.GetChar(CharID, false);
            ActorDB.GetItem(pc);
            //Key=ItemID
            //Value=Count
            foreach (var itm in ItemDel)
            {
                if (itm.slot == ContainerType.WAREHOUSE)
                {
                    pc.Inventory.DeleteWareItem(itm.place, itm.SlotID, itm.count);
                }
                else
                {
                    pc.Inventory.DeleteItem(itm.SlotID, itm.count);
                }
            }
            ActorDB.SaveItem(pc);

            //刪除SQLite
            SQLiteCommand cmd = new SQLiteCommand(Conn);
            var trans = Conn.BeginTransaction();
            try
            {
                foreach (var itm in ItemDel)
                {
                    cmd.CommandText = string.Format("DELETE FROM `Item` WHERE CharID='{0}' AND SlotID='{1}'", CharID, itm.SlotID);
                    cmd.ExecuteNonQuery();
                }
                trans.Commit();
            }
            catch (Exception ex)
            {
                trans.Rollback();
                MessageBox.Show(ex.ToString());
            }

        }

        private void lstItemUserName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstItemUserName.SelectedIndex >= 0)
            {
                btnItemCharAdd.Enabled = true;
                ReflashCharItem();
            }
        }

        private void btnItemSearch_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet ret = LoadItemCharName(tbItemNameQuery.Text);
                if (ret.Tables.Count > 0)
                {
                    lstItemUserName.ValueMember = "char_id";
                    lstItemUserName.DisplayMember = "name";
                    lstItemUserName.DataSource = ret.Tables[0];
                    Log(string.Format("查詢執行完畢，取回：{0} 筆資料", ret.Tables[0].Rows.Count));
                }
                else
                {
                    Log(string.Format("查無結果資料"));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void gvResult_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                switch (tabControl1.SelectedIndex)
                {
                    case 3:
                        ItemCharMenu.Show((Control)sender, e.Location);
                        break;
                    case 1:
                        MoneyMenu.Show((Control)sender, e.Location);
                        break;
                }
            }
        }
        private void DeleteItemMenu()
        {
            if (gvResult.SelectedRows.Count == 0)
            {
                MessageBox.Show("請以行為單位選擇要刪除的物品，以防止刪錯物品");
                return;
            }
            StringBuilder sb = new StringBuilder("請問您要刪除的是：");
            for (int i = 0; i < gvResult.SelectedRows.Count; i++)
            {
                sb.Append(string.Format("\n{0} x{1} ({2})", gvResult.SelectedRows[i].Cells[3].Value, gvResult.SelectedRows[i].Cells[4].Value, gvResult.SelectedRows[i].Cells[2].Value));
            }
            var ans = MessageBox.Show(sb.ToString(), "確認", MessageBoxButtons.YesNo);
            if (ans == DialogResult.Yes)
            {
                List<CItemDelData> DelLst = new List<CItemDelData>();
                for (int i = 0; i < gvResult.SelectedRows.Count; i++)
                {
                    DelLst.Add(new CItemDelData(gvResult.SelectedRows[i].Cells[8].Value.ToString(), uint.Parse(gvResult.SelectedRows[i].Cells[2].Value.ToString()), int.Parse(gvResult.SelectedRows[i].Cells[4].Value.ToString()), uint.Parse(gvResult.SelectedRows[i].Cells[11].Value.ToString())));
                }
                DeleteCharItems((uint)lstItemUserName.SelectedValue, DelLst);
                Log(string.Format("執行完畢，刪除：{0} 項物品", gvResult.SelectedRows.Count));
                ReflashCharItem();
            }
        }

        private void 刪除道具ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            DeleteItemMenu();
        }

        private void ReflashCharItem()
        {
            var ret = LoadCharItem((uint)lstItemUserName.SelectedValue);
            gvResult.DataSource = ret;
            Log(string.Format("查詢執行完畢，取回：{0} 筆資料", ret.Rows.Count));
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            gvResult.DataSource = null;
        }

        private void tbItemCharItemIDName_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnItemCharAdd_Click(object sender, EventArgs e)
        {
            uint ItemID = 0;
            int Count;
            if (Regex.Match(tbItemCharItemIDName.Text, "^[\\d]+$").Success)
            {
                try
                {
                    ItemID = uint.Parse(tbItemCharItemIDName.Text);
                }
                catch (Exception) { }
            }
            else
            {
                var item = (from KeyValuePair<uint, Item.ItemData> itm in ItemFactory.Instance.Items
                            where itm.Value.name == tbItemCharItemIDName.Text
                            select itm.Value);
                if (item.Count() > 0)
                {

                    ItemID = item.First().id;
                }
            }
            if (ItemID == 0)
            {
                MessageBox.Show("請輸入道具");
                tbItemCharItemIDName.Text = "";
                return;
            }
            try
            {
                Count = int.Parse(tbItemCharItemCount.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("請輸入數量");
                tbItemCharItemCount.Text = "";
                return;
            }

            AddItem(uint.Parse(lstItemUserName.SelectedValue.ToString()), new CItemDelData(ContainerType.BODY, ItemID, Count, 0));
            Log(string.Format("執行完畢，新增了：{0} 共：{1}個", tbItemCharItemIDName.Text, Count));
            ReflashCharItem();
        }

        private void AddItem(uint charid, CItemDelData items)
        {
            List<uint> chs = new List<uint>();
            chs.Add(charid);
            List<CItemDelData> itms = new List<CItemDelData>();
            itms.Add(items);
            AddItem(chs, itms);
        }
        private void AddItem(List<uint> charid, List<CItemDelData> items)
        {
            MySQLActorDB ActorDB = new MySQLActorDB(host, port, database, user, pass);
            foreach (uint id in charid)
            {
                ActorPC pc = ActorDB.GetChar(id, false);
                ActorDB.GetItem(pc);
                foreach (CItemDelData itm in items)
                {
                    Item item = ItemFactory.Instance.GetItem(itm.ItemID);
                    if (item.Stackable)
                    {
                        item.Stack = (ushort)itm.count;
                        pc.Inventory.AddItem(ContainerType.BODY, item);
                    }
                    else
                    {
                        for (int i = 0; i < itm.count; i++)
                        {
                            item.Stack = 1;
                            pc.Inventory.AddItem(ContainerType.BODY, item);
                        }
                    }
                }
                ActorDB.SaveItem(pc);
            }
        }

        private void gvResult_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                DeleteItemMenu();
            }
        }

        private void btnItemDataSearch_Click(object sender, EventArgs e)
        {
            var r = from KeyValuePair<uint, Item.ItemData> itm in ItemFactory.Instance.Items
                    where itm.Value.name.IndexOf(tbItemDataQuery.Text) >= 0 || itm.Value.id.ToString().IndexOf(tbItemDataQuery.Text) >= 0 || itm.Value.itemType.ToString().IndexOf(tbItemDataQuery.Text) >= 0
                    select itm.Value;
            DataTable Items = new DataTable();
            ////ItemID
            Items.Columns.Add("ItemID", typeof(int));
            ////ItemName
            Items.Columns.Add("ItemName", typeof(string));
            ////ItemType
            Items.Columns.Add("ItemType", typeof(string));
            ////ItemPrice
            Items.Columns.Add("Price", typeof(int));
            ////Weight
            Items.Columns.Add("Weight", typeof(string));
            ////Volume
            Items.Columns.Add("Volume", typeof(string));
            ////EquipVolume
            Items.Columns.Add("EquipVolume", typeof(string));
            ////Event
            Items.Columns.Add("Event", typeof(string));
            ////発動Skill
            Items.Columns.Add("発動Skill", typeof(string));
            ////使用可能Skill
            Items.Columns.Add("使用可能Skill", typeof(string));
            ////パッシブスキル
            Items.Columns.Add("パッシブスキル", typeof(string));
            ////憑依時可能Skill
            Items.Columns.Add("憑依時可能Skill", typeof(string));
            ////憑依パッシブSkill
            Items.Columns.Add("憑依パッシブSkill", typeof(string));

            foreach (var itm in r)
            {
                Object[] o = { itm.id, itm.name, itm.itemType, itm.price, itm.weight, itm.volume, itm.equipVolume, itm.eventID, 
                                 itm.activateSkill, itm.possibleSkill ,itm.possessionSkill,itm.possessionPassiveSkill  };
                Items.Rows.Add(o);
            }
            gvResult.DataSource = Items;
            Log(string.Format("查詢執行完畢，取回：{0} 筆資料", Items.Rows.Count));
        }

        private void btnItemDataAll_Click(object sender, EventArgs e)
        {
            var r = from KeyValuePair<uint, Item.ItemData> itm in ItemFactory.Instance.Items
                    select itm.Value;
            DataTable Items = new DataTable();
            ////ItemID
            Items.Columns.Add("ItemID", typeof(int));
            ////ItemName
            Items.Columns.Add("ItemName", typeof(string));
            ////ItemType
            Items.Columns.Add("ItemType", typeof(string));
            ////ItemPrice
            Items.Columns.Add("Price", typeof(int));
            ////Weight
            Items.Columns.Add("Weight", typeof(int));
            ////Volume
            Items.Columns.Add("Volume", typeof(int));
            ////EquipVolume
            Items.Columns.Add("EquipVolume", typeof(int));
            ////Event
            Items.Columns.Add("Event", typeof(string));
            ////発動Skill
            Items.Columns.Add("発動Skill", typeof(string));
            ////使用可能Skill
            Items.Columns.Add("使用可能Skill", typeof(string));
            ////パッシブスキル
            Items.Columns.Add("パッシブスキル", typeof(string));
            ////憑依時可能Skill
            Items.Columns.Add("憑依時可能Skill", typeof(string));
            ////憑依パッシブSkill
            Items.Columns.Add("憑依パッシブSkill", typeof(string));

            foreach (var itm in r)
            {
                Object[] o = { itm.id, itm.name, itm.itemType, itm.price, itm.weight, itm.volume, itm.equipVolume, itm.eventID, 
                                 itm.activateSkill, itm.possibleSkill ,itm.possessionSkill,itm.possessionPassiveSkill  };
                Items.Rows.Add(o);
            }
            gvResult.DataSource = Items;
            Log(string.Format("查詢執行完畢，取回：{0} 筆資料", Items.Rows.Count));
        }

        private void tabPage8_Enter(object sender, EventArgs e)
        {
            if (MobFactory.Instance.Mobs.Count == 0)
            {
                try
                {
                    Log("怪物資料讀取中...");
                    MobFactory.Instance.Init("./DB/monster.csv", System.Text.Encoding.UTF8);
                    MobFactory.Instance.InitPet("./DB/pet.csv", System.Text.Encoding.UTF8);
                    Log(string.Format("怪物資料讀取完畢，取得：{0} 筆怪物資料", MobFactory.Instance.Mobs.Count));
                }
                catch (Exception)
                {
                    MessageBox.Show("怪物資料讀取失敗");
                }
            }

        }

        private void btnMobAll_Click(object sender, EventArgs e)
        {
            DataTable Mobs = new DataTable();
            ////MobID
            Mobs.Columns.Add("MobID", typeof(int));
            ////MobName
            Mobs.Columns.Add("MobName", typeof(string));
            ////MobType
            Mobs.Columns.Add("MobType", typeof(string));
            ////Lv
            Mobs.Columns.Add("Lv", typeof(int));
            ////Weight
            Mobs.Columns.Add("HP", typeof(int));
            ////経験値Base
            Mobs.Columns.Add("経験値Base", typeof(int));
            ////経験値Job
            Mobs.Columns.Add("経験値Job", typeof(int));
            ////Item1&機率
            Mobs.Columns.Add("Item1&機率", typeof(string));
            ////Item2&機率
            Mobs.Columns.Add("Item2&機率", typeof(string));
            ////Item3&機率
            Mobs.Columns.Add("Item3&機率", typeof(string));
            ////Item4&機率
            Mobs.Columns.Add("Item4&機率", typeof(string));
            ////Item5&機率
            Mobs.Columns.Add("Item5&機率", typeof(string));
            ////Item6&機率
            Mobs.Columns.Add("Item6&機率", typeof(string));
            ////Item7&機率
            Mobs.Columns.Add("Item7&機率", typeof(string));
            ////Item8&機率
            Mobs.Columns.Add("Item8&機率", typeof(string));

            var r = from KeyValuePair<uint, MobData> mob in MobFactory.Instance.Mobs
                    select mob.Value;
            string ItemName;

            foreach (var m in r)
            {
                List<Object> o = new List<object>();
                o.Add(m.id);
                o.Add(m.name);
                o.Add(m.mobType);
                o.Add(m.level);
                o.Add(m.hp);
                o.Add(m.baseExp);
                o.Add(m.jobExp);
                for (int i = 0; i < 8; i++)
                {
                    if (i < m.dropItems.Count)
                    {
                        try
                        {
                            ItemName = ItemFactory.Instance.Items[m.dropItems[i].ItemID].name;
                        }
                        catch (Exception)
                        {
                            ItemName = m.dropItems[i].ItemID.ToString();
                        }
                        o.Add(string.Format("{0}({1}):{2,3}%", ItemName, m.dropItems[i].ItemID, m.dropItems[i].Rate / 100f));
                    }
                    else
                    {
                        o.Add(null);
                    }
                }
                Mobs.Rows.Add(o.ToArray());
            }
            gvResult.DataSource = Mobs;
            Log(string.Format("查詢執行完畢，取回：{0} 筆資料", Mobs.Rows.Count));
        }

        private void btnMobSearch_Click(object sender, EventArgs e)
        {
            DataTable Mobs = new DataTable();
            ////MobID
            Mobs.Columns.Add("MobID", typeof(int));
            ////MobName
            Mobs.Columns.Add("MobName", typeof(string));
            ////MobType
            Mobs.Columns.Add("MobType", typeof(string));
            ////Lv
            Mobs.Columns.Add("Lv", typeof(int));
            ////Weight
            Mobs.Columns.Add("HP", typeof(int));
            ////経験値Base
            Mobs.Columns.Add("経験値Base", typeof(int));
            ////経験値Job
            Mobs.Columns.Add("経験値Job", typeof(int));
            ////Item1&機率
            Mobs.Columns.Add("Item1&機率", typeof(string));
            ////Item2&機率
            Mobs.Columns.Add("Item2&機率", typeof(string));
            ////Item3&機率
            Mobs.Columns.Add("Item3&機率", typeof(string));
            ////Item4&機率
            Mobs.Columns.Add("Item4&機率", typeof(string));
            ////Item5&機率
            Mobs.Columns.Add("Item5&機率", typeof(string));
            ////Item6&機率
            Mobs.Columns.Add("Item6&機率", typeof(string));
            ////Item7&機率
            Mobs.Columns.Add("Item7&機率", typeof(string));
            ////Item8&機率
            Mobs.Columns.Add("Item8&機率", typeof(string));

            var r = from KeyValuePair<uint, MobData> mob in MobFactory.Instance.Mobs
                    where mob.Value.name.IndexOf(tbMobQuery.Text) >= 0 || mob.Value.id.ToString().IndexOf(tbMobQuery.Text) >= 0 || mob.Value.mobType.ToString().IndexOf(tbMobQuery.Text) >= 0
                    select mob.Value;
            string ItemName;

            foreach (var m in r)
            {
                List<Object> o = new List<object>();
                o.Add(m.id);
                o.Add(m.name);
                o.Add(m.mobType);
                o.Add(m.level);
                o.Add(m.hp);
                o.Add(m.baseExp);
                o.Add(m.jobExp);
                for (int i = 0; i < 8; i++)
                {
                    if (i < m.dropItems.Count)
                    {
                        try
                        {
                            ItemName = ItemFactory.Instance.Items[m.dropItems[i].ItemID].name;
                        }
                        catch (Exception)
                        {
                            ItemName = m.dropItems[i].ItemID.ToString();
                        }
                        o.Add(string.Format("{0}({1}):{2,3}%", ItemName, m.dropItems[i].ItemID, m.dropItems[i].Rate / 100f));
                    }
                    else
                    {
                        o.Add(null);
                    }
                }
                Mobs.Rows.Add(o.ToArray());
            }
            gvResult.DataSource = Mobs;
            Log(string.Format("查詢執行完畢，取回：{0} 筆資料", Mobs.Rows.Count));
        }

        private void lan_CHS_Click(object sender, EventArgs e)
        {
            Program.culture = new System.Globalization.CultureInfo("zh-CHS");
            this.Close();
        }

        private void lan_CHT_Click(object sender, EventArgs e)
        {
            Program.culture = new System.Globalization.CultureInfo("zh-CHT");
            this.Close();
        }

        private void lan_JP_Click(object sender, EventArgs e)
        {
            Program.culture = new System.Globalization.CultureInfo("ja-jp");
            this.Close();
        }

        private void lan_EN_Click(object sender, EventArgs e)
        {
            Program.culture = new System.Globalization.CultureInfo("en");
            this.Close();
        }

        private void 封鎖帳號ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (gvResult.SelectedRows.Count == 0)
            {
                MessageBox.Show("請以行為單位選擇要封鎖的帳號(人物)，以防止封鎖錯誤");
                return;
            }
            StringBuilder sb = new StringBuilder("請問您要封鎖的是：");
            for (int i = 0; i < gvResult.SelectedRows.Count; i++)
            {
                sb.Append(string.Format("\n{0} ({1})", gvResult.SelectedRows[i].Cells[1].Value, gvResult.SelectedRows[i].Cells[3].Value));
            }
            var ans = MessageBox.Show(sb.ToString(), "確認", MessageBoxButtons.YesNo);
            if (ans == DialogResult.Yes)
            {
                List<int> id = new List<int>();
                List<CharDataType> types = new List<CharDataType>();
                for (int i = 0; i < gvResult.SelectedRows.Count; i++)
                {
                    id.Add(int.Parse(gvResult.SelectedRows[i].Cells[0].Value.ToString()));
                    if (gvResult.SelectedRows[i].Cells[3].Value.ToString() == "Bank")
                    {
                        types.Add(CharDataType.Account);
                    }
                    else
                    {
                        types.Add(CharDataType.Character);
                    }
                }
                BannedUser(id, types);
                Log(string.Format("執行完畢，封鎖了：{0} 個帳號", gvResult.SelectedRows.Count));
                //    ReflashCharItem();
            }

        }

        private enum CharDataType
        {
            Account,
            Character
        }

        private void BannedUser(List<int> data, List<CharDataType> type)
        {
            for (int i = 0; i < data.Count; i++)
            {
                switch (type[i])
                {
                    case CharDataType.Account:
                        BannedUser(data[i]);
                        break;
                    case CharDataType.Character:
                        BannedUser(GetAccountByCharID(data[i]));
                        break;
                }
            }
        }

        private int GetAccountByCharID(int CharID)
        {
            try
            {
                string sql = string.Format("SELECT account_id from `char` where char_id={0}", CharID);
                return int.Parse(GetDataFromDatabase(sql).Tables[0].Rows[0]["account_id"].ToString());
            }
            catch (Exception)
            {
                return -1;
            }
        }

        private void BannedUser(int Account)
        {
            try
            {
                string sql = string.Format("UPDATE `login` SET banned=1 where account_id={0}", Account);
                //Log(sql);
                ExecuteSqlToDatabase(sql);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }


    }

    /// <summary>
    /// 刪除物品的資料
    /// </summary>
    class CItemDelData
    {
        public CItemDelData(ContainerType slot, uint ItemID, int count, uint SlotID)
        {
            this.count = count;
            this.slot = slot;
            this.ItemID = ItemID;
            this.SlotID = SlotID;
        }

        public CItemDelData(string slot, uint ItemID, int count, uint SlotID)
        {
            this.count = count;
            try
            {
                this.slot = (ContainerType)Enum.Parse(typeof(ContainerType), slot, true);
            }
            catch (Exception)
            {
                this.slot = ContainerType.WAREHOUSE;
                place = (WarehousePlace)Enum.Parse(typeof(WarehousePlace), slot, true);
            }
            this.ItemID = ItemID;
            this.SlotID = SlotID;
        }

        public ContainerType slot;
        public WarehousePlace place;
        public uint ItemID;
        public uint SlotID;
        public int count;
    }
}
