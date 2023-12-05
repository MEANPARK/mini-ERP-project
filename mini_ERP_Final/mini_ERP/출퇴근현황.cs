using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.ComponentModel.Design.ObjectSelectorEditor;

namespace TeamProject_test_v1
{
    public partial class 출퇴근현황 : Form
    {
        string num = string.Empty;
        public 출퇴근현황()
        {
            InitializeComponent();
            this.num = Properties.Settings.Default.myID;
        }

        private void check_work()
        {
            string query = "SELECT s5584720.사원.이름, s5584720.출근부.출근시간, s5584720.출근부.퇴근시간 FROM s5584720.출근부 INNER JOIN s5584720.사원 ON s5584720.출근부.사원번호 = s5584720.사원.사원번호 WHERE s5584720.출근부.사원번호 != '00000000';";

            DataTable dt = new DataTable();

            try
            {
                DBManager.GetDBManager().OpenConnection();
                using (MySqlDataReader reader = DBManager.GetDBManager().SetQuery(query).ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            dt.Columns.Add(reader.GetName(i), reader.GetFieldType(i));
                        }

                        while (reader.Read())
                        {
                            DataRow row = dt.NewRow();
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                row[i] = reader[i];
                            }
                            dt.Rows.Add(row);
                        }
                    }
                }
            }
            catch (Exception ex) { }
            finally
            {
                DBManager.GetDBManager().CloseConnection();
            }

            check_work_dataGridView.DataSource = dt;
        }

        private void check_work_button_Click(object sender, EventArgs e)
        {
            // 이 부분 사원 번호 받아와야 되는데 로그인이나 아니면 메인 화면에서 사원 번호 받아오는걸로 쓰면 될듯
            check_work();
            Additional_Allowance.getInstance().check_salary(num);
        }
    }
}
