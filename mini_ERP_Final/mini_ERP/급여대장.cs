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
    public partial class 급여대장 : Form
    {
        DataTable dt = new DataTable();
        public string query;
        public 급여대장()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            work_month();
            total_load();
        }
        private void work_month()
        {
            if (month_combobox.Items.Count == 0 && year_combobox.Items.Count == 0)
            {
                DBManager.GetDBManager().OpenConnection();
                this.query = $"select distinct month(지급날짜)-1 from s5584720.급여내역 group by 지급날짜 DESC; select distinct year(지급날짜) from s5584720.급여내역 group by 지급날짜;";
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
        private void search_button_Click(object sender, EventArgs e)
        {
            if (year_combobox.SelectedIndex != -1 && month_combobox.SelectedIndex != -1)
            {
                total_member_dataGridView.Rows.Clear();
                money_total_load();
                total_member_load();
            }
            else
            {
                MessageBox.Show("검색날짜를 선택해 주세요", "주의");
            }
        }
        private void total_member_load()
        {
            this.query = $"select count(*) as '전체인원', sum(지급합계) as '지급합계', sum(공제액) as '공제합계',sum(실수령액) as '차인지급액총합' from(select concat(m.이름, '(', m.사원번호, ')') as '사원', m.직급, m.부서명, s.기본급, s.야간근로수당,s.연장근로수당,s.휴일근로수당,(s.기본급+s.야간근로수당+s.연장근로수당+s.휴일근로수당) as '지급합계', s.국민연금,s.국민건강보험,s.장기요양국민건강보험,s.고용보험료,(s.국민연금+s.국민건강보험+s.장기요양국민건강보험+s.고용보험료) as '공제액', s.실수령액, DATE_FORMAT(s.지급날짜,'%Y년 %m월 %d일') as '지급일' from s5584720.급여내역 s join s5584720.사원 m on s.사원번호=m.사원번호 where year(지급날짜)={Convert.ToInt32(year_combobox.SelectedItem)} and month(지급날짜)={Convert.ToInt32(month_combobox.SelectedItem) + 1}) as money_table;";
            DBManager.GetDBManager().OpenConnection();
            MySqlDataReader time_reader = DBManager.GetDBManager().SetQuery(this.query).ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(time_reader);
            DBManager.GetDBManager().CloseConnection();
            total_member_dataGridView.Rows.Add("전체인원", $"{dt.Rows[0][0]}명", "", "지급단위 : 만(원)");
            total_member_dataGridView.Rows.Add("합계", $"지급합계 : {dt.Rows[0][1]}", $"공제합계 : {dt.Rows[0][2]}", $"지급총합 : {dt.Rows[0][3]}");
            //total_member_dataGridView.DataSource = dt;
        }
        private void money_total_load()
        {
            this.query = $"select concat(m.이름, '(', m.사원번호, ')') as '사원', m.직급, m.부서명, s.기본급, s.야간근로수당,s.연장근로수당,s.휴일근로수당,(s.기본급+s.야간근로수당+s.연장근로수당+s.휴일근로수당) as '지급합계', s.국민연금,s.국민건강보험,s.장기요양국민건강보험,s.고용보험료,(s.국민연금+s.국민건강보험+s.장기요양국민건강보험+s.고용보험료) as '공제액', s.실수령액, DATE_FORMAT(DATE_SUB(s.지급날짜, INTERVAL 1 MONTH),'%m월') as '근무월', DATE_FORMAT(s.지급날짜,'%Y년 %m월 %d일') as '지급일' from s5584720.급여내역 s join s5584720.사원 m on s.사원번호=m.사원번호 where year(지급날짜)={Convert.ToInt32(year_combobox.SelectedItem)} and month(지급날짜)={Convert.ToInt32(month_combobox.SelectedItem) + 1}";
            //this.query = $"select concat(m.이름,'(',s.사원번호,')') as 사원, 기본급,(야간근로수당+연장근로수당+휴일근로수당) as '추가수당', (국민연금+국민건강보험+장기요양국민건강보험+고용보험료) as '공제' from s5584720.급여내역 s join s5584720.사원 m on s.사원번호=m.사원번호 where year(지급날짜)={Convert.ToInt32(year_combobox.SelectedItem)} and month(지급날짜)={Convert.ToInt32(month_combobox.SelectedItem)}";
            DBManager.GetDBManager().OpenConnection();
            MySqlDataReader time_reader = DBManager.GetDBManager().SetQuery(this.query).ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(time_reader);
            DBManager.GetDBManager().CloseConnection();
            money_dataGridView.DataSource = dt;
        }
        private void total_load()
        {
            this.query = $"select count(*) as '전체인원', sum(지급합계) as '지급합계', sum(공제액) as '공제합계',sum(실수령액) as '차인지급액총합' from(select concat(m.이름, '(', m.사원번호, ')') as '사원', m.직급, m.부서명, s.기본급, s.야간근로수당,s.연장근로수당,s.휴일근로수당,(s.기본급+s.야간근로수당+s.연장근로수당+s.휴일근로수당) as '지급합계', s.국민연금,s.국민건강보험,s.장기요양국민건강보험,s.고용보험료,(s.국민연금+s.국민건강보험+s.장기요양국민건강보험+s.고용보험료) as '공제액', s.실수령액, DATE_FORMAT(s.지급날짜,'%Y년 %m월 %d일') as '지급일' from s5584720.급여내역 s join s5584720.사원 m on s.사원번호=m.사원번호) as money_table;";
            string query_ = $"select concat(m.이름, '(', m.사원번호, ')') as '사원', m.직급, m.부서명, s.기본급, s.야간근로수당,s.연장근로수당,s.휴일근로수당,(s.기본급+s.야간근로수당+s.연장근로수당+s.휴일근로수당) as '지급합계', s.국민연금,s.국민건강보험,s.장기요양국민건강보험,s.고용보험료,(s.국민연금+s.국민건강보험+s.장기요양국민건강보험+s.고용보험료) as '공제액', s.실수령액, DATE_FORMAT(s.지급날짜,'%Y년 %m월 %d일') as '지급일' from s5584720.급여내역 s join s5584720.사원 m on s.사원번호=m.사원번호 order by 지급일;";
            DBManager.GetDBManager().OpenConnection();
            MySqlDataReader time_reader = DBManager.GetDBManager().SetQuery(this.query).ExecuteReader();
            DataTable dtt = new DataTable();
            dtt.Load(time_reader);
            total_member_dataGridView.Rows.Add("전체인원", $"{dtt.Rows[0][0]}명", "", "지급단위 : 만(원)");
            total_member_dataGridView.Rows.Add("합계", $"지급합계 : {dtt.Rows[0][1]}", $"공제합계 : {dtt.Rows[0][2]}", $"지급총합 : {dtt.Rows[0][3]}");
            //total_member_dataGridView.DataSource = dtt;
            MySqlDataReader time_reader_ = DBManager.GetDBManager().SetQuery(query_).ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(time_reader_);
            money_dataGridView.DataSource = dt;
            DBManager.GetDBManager().CloseConnection();
        }

        private void buttonRe_Click(object sender, EventArgs e)
        {
            total_member_dataGridView.Rows.Clear();
            year_combobox.SelectedIndex = -1;
            month_combobox.SelectedIndex = -1;
            work_month();
            total_load();
        }
    }
}
