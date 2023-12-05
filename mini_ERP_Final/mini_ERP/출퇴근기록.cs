using MySql.Data.MySqlClient;
using System.Data;

namespace TeamProject_test_v1
{
    public partial class 출퇴근기록 : Form
    {
        public 출퇴근기록()
        {
            InitializeComponent();
            load_data(Properties.Settings.Default.myID); // 마찬가지로 로그인 된 사원번호 필요
        }
        private void 출퇴근기록_Load(object sender, EventArgs e)
        {
            if(사용자매니저.GetInstance().Get_직급() == "사장")
            {
                additional_allowance_button.Enabled = false;
            }
        }

        private void load_data(string num)
        {
            string query = "SELECT s5584720.출근부.사원번호, s5584720.사원.이름, s5584720.출근부.출근시간, s5584720.출근부.퇴근시간, s5584720.출근부.일반근로시간, s5584720.출근부.연장근로시간, s5584720.출근부.야간근로시간, s5584720.출근부.휴일근로시간, s5584720.출근부.총근로시간 FROM s5584720.출근부 INNER JOIN s5584720.사원 ON s5584720.출근부.사원번호 = s5584720.사원.사원번호 WHERE s5584720.출근부.사원번호 = '@num' AND s5584720.출근부.결재여부 = 1;";
            query = query.Replace("@num", num);

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

            additional_allowance_dataGridView.DataSource = dt;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            추가수당신청 frm = new 추가수당신청();
            frm.ShowDialog();
        }

        
    }
}