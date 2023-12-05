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

namespace TeamProject_test_v1
{
    public partial class salary : Form
    {
        public string query;
        public salary()
        {
            InitializeComponent();
        }

        private void salary_Load(object sender, EventArgs e)
        {
            //month_combobox.SelectedValue = DateTime.Now.AddMonths(-1).Month.ToString();
            member_load();
            money_load();
            tax_load();
            time_load();
            work_month();
            total_load();
            total_time_load();
        }
        private void member_load()
        {
            this.query = $"select 이름 aS '성 명', 사원번호 as '사번', 부서명 AS '부 서', 직급 as '직 급' from s5584720.사원 where 사원번호='{Properties.Settings.Default.myID}'";
            DBManager.GetDBManager().OpenConnection();
            MySqlDataReader reader = DBManager.GetDBManager().SetQuery(this.query).ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(reader);
            DBManager.GetDBManager().CloseConnection();
            member_show_datagridview.DataSource = dt;
        }
        private void time_load()
        {
            this.query = $"select date(출근시간) as 출근일, floor((sum(일반근로시간)/60)) as 정상근무시간,floor((sum(연장근로시간)/60)) as 연장근무, floor((sum(야간근로시간)/60)) as 야간근무, floor((sum(휴일근로시간)/60)) as 휴일근무, floor((sum(총근로시간)/60)) as 일일근무시간 from s5584720.출근부 where 사원번호='{Properties.Settings.Default.myID}' and year(출근시간)={Convert.ToInt32(year_combobox.SelectedItem)} and month(출근시간)={Convert.ToInt32(month_combobox.SelectedItem)} group by 출근시간";
            DBManager.GetDBManager().OpenConnection();
            MySqlDataReader time_reader = DBManager.GetDBManager().SetQuery(this.query).ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(time_reader);
            DBManager.GetDBManager().CloseConnection();
            time_datagridview.DataSource = dt;
        }
        private void total_time_load()
        {
            this.query = $"select floor((sum(일반근로시간)/60)) as 근무시간,floor((sum(연장근로시간)/60)) as 연장근로, floor((sum(야간근로시간)/60)) as 야간근로, floor((sum(휴일근로시간)/60)) as 휴일근로, floor((sum(총근로시간)/60)) as 총근로시간 from s5584720.출근부 where 사원번호='{Properties.Settings.Default.myID}' and year(출근시간)={Convert.ToInt32(year_combobox.SelectedItem)} and month(출근시간)={Convert.ToInt32(month_combobox.SelectedItem)}";
            DBManager.GetDBManager().OpenConnection();
            MySqlDataReader reader = DBManager.GetDBManager().SetQuery(this.query).ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(reader);
            DBManager.GetDBManager().CloseConnection();
            totol_time_dataGridView.DataSource = dt;
        }
        private void money_load()
        {
            this.query = $"SELECT '기본급' AS 항목, 기본급 AS '금액(만)' FROM s5584720.급여내역 WHERE 사원번호 = '{Properties.Settings.Default.myID}' AND year(지급날짜)={Convert.ToInt32(year_combobox.SelectedItem)} and month(지급날짜)={Convert.ToInt32(month_combobox.SelectedItem) + 1} " +
                $"UNION SELECT '추가수당' AS 항목, (야간근로수당 + 연장근로수당 + 휴일근로수당) AS '금액(만)' FROM s5584720.급여내역 WHERE 사원번호='{Properties.Settings.Default.myID}' AND year(지급날짜)={Convert.ToInt32(year_combobox.SelectedItem)} and month(지급날짜)={Convert.ToInt32(month_combobox.SelectedItem) + 1}";
            //this.query = $"select 기본급, (야간근로수당+연장근로수당+휴일근로수당) as 추가수당 from s5584720.급여내역 where 사원번호='{login_Settings.Default.local_id}' and year(지급날짜)={Convert.ToInt32(year_combobox.SelectedItem)} and month(지급날짜)={Convert.ToInt32(month_combobox.SelectedItem)}";
            DBManager.GetDBManager().OpenConnection();
            MySqlDataReader reader = DBManager.GetDBManager().SetQuery(this.query).ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(reader);
            DBManager.GetDBManager().CloseConnection();
            salary_datagridview.DataSource = dt;
        }
        private void tax_load()
        {
            this.query = $"SELECT '국민연금' AS 공제항목, CASE WHEN 사원번호 = '{Properties.Settings.Default.myID}' THEN 국민연금 ELSE NULL END AS 금액 FROM s5584720.급여내역 WHERE 사원번호 = '{Properties.Settings.Default.myID}' AND year(지급날짜)={Convert.ToInt32(year_combobox.SelectedItem)} and month(지급날짜)={Convert.ToInt32(month_combobox.SelectedItem) + 1}" +
                $" UNION SELECT '국민건강보험' AS 공제항목, CASE WHEN 사원번호 = '{Properties.Settings.Default.myID}' THEN 국민건강보험 ELSE NULL END AS 금액 FROM s5584720.급여내역 WHERE 사원번호 = '{Properties.Settings.Default.myID}' AND year(지급날짜)={Convert.ToInt32(year_combobox.SelectedItem)} and month(지급날짜)={Convert.ToInt32(month_combobox.SelectedItem) + 1}" +
                $" UNION SELECT '장기요양국민건강보험' AS 공제항목, CASE WHEN 사원번호 = '{Properties.Settings.Default.myID}' THEN 장기요양국민건강보험 ELSE NULL END AS 금액 FROM s5584720.급여내역 WHERE 사원번호 = '{Properties.Settings.Default.myID}' AND year(지급날짜)={Convert.ToInt32(year_combobox.SelectedItem)} and month(지급날짜)={Convert.ToInt32(month_combobox.SelectedItem) + 1}" +
                $" UNION SELECT '고용보험료' AS 공제항목, CASE WHEN 사원번호 = '{Properties.Settings.Default.myID}' THEN 고용보험료 ELSE NULL END AS 금액 FROM s5584720.급여내역 WHERE 사원번호 = '{Properties.Settings.Default.myID}' AND year(지급날짜)={Convert.ToInt32(year_combobox.SelectedItem)} and month(지급날짜)={Convert.ToInt32(month_combobox.SelectedItem) + 1}";
            DBManager.GetDBManager().OpenConnection();
            MySqlDataReader reader = DBManager.GetDBManager().SetQuery(this.query).ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(reader);
            DBManager.GetDBManager().CloseConnection();
            tax_dataGridView.DataSource = dt;
        }
        private void work_month()
        {
            if (month_combobox.Items.Count == 0 && year_combobox.Items.Count == 0)
            {
                DBManager.GetDBManager().OpenConnection();
                this.query = $"select distinct month(지급날짜)-1 from s5584720.급여내역 where 사원번호='{Properties.Settings.Default.myID}' group by 지급날짜 DESC; select distinct year(지급날짜) from s5584720.급여내역 where 사원번호='{Properties.Settings.Default.myID}' group by 지급날짜 DESC;";
                MySqlDataReader reader = DBManager.GetDBManager().SetQuery(this.query).ExecuteReader();
                while (reader.Read())
                {
                    month_combobox.Items.Add(reader.GetString(0));
                }
                reader.NextResult();
                while (reader.Read())
                {
                    year_combobox.Items.Add(reader.GetString(0));
                }
                DBManager.GetDBManager().CloseConnection();
            }
        }
        private void total_load()
        {
            this.query = $"select (기본급+야간근로수당+연장근로수당+휴일근로수당) as 지급합계, (국민연금+국민건강보험+장기요양국민건강보험+고용보험료) as 공제합계, ((기본급+야간근로수당+연장근로수당+휴일근로수당)-(국민연금+국민건강보험+장기요양국민건강보험+고용보험료))실수령액 from s5584720.급여내역 where 사원번호='{Properties.Settings.Default.myID}' and year(지급날짜)={Convert.ToInt32(year_combobox.SelectedItem)} and month(지급날짜)={Convert.ToInt32(month_combobox.SelectedItem) + 1}";
            DBManager.GetDBManager().OpenConnection();
            MySqlDataReader reader = DBManager.GetDBManager().SetQuery(this.query).ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(reader);
            DBManager.GetDBManager().CloseConnection();
            total_datagridview.DataSource = dt;
        }

        private void search_button_Click(object sender, EventArgs e)
        {
            money_load();
            tax_load();
            time_load();
            work_month();
            total_load();
            total_time_load();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string text = "세금공제에는 근로소득공제, 종합소득공제, 세액공제 총 3가지가 있음. \n근로소득공제 : 총 급여액에서 일정 금액을 기본적으로 공제하는 것\n종합소득공제 : 특정 항목의 지출 내역 일부를 총 급여액에서 제외하는 것\n 세액공제 : 산출된 세액에서 특정 항목을 차감해주는 것.\n    ex) 연금계좌와 보험료, 의료비, 교육비, 기부금, 월세, 자녀 세액공제 등\n";
            MessageBox.Show(text);
        }
    }
}
