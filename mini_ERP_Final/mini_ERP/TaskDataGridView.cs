using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamProject_test_v1
{
    internal class TaskDataGridView
    {
        private static TaskDataGridView instance = new TaskDataGridView();
        private TaskDataGridView() { }
        public static TaskDataGridView getinstance()
        {
            return instance;
        }
        public void setDataGridView(string query, DataGridView datagridview)
        {
            DBManager.GetDBManager().OpenConnection();
            MySqlDataReader reader = DBManager.GetDBManager().SetQuery(query).ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(reader);
            DBManager.GetDBManager().CloseConnection();
            datagridview.DataSource = dt;
            dt.DefaultView.Sort = "시작시간 DESC";
            datagridview.Columns[0].Visible = false;
            datagridview.Columns[8].Visible = false; 
        }
    }
}
