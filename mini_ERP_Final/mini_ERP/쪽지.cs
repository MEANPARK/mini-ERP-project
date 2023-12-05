using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace TeamProject_test_v1
{
    public partial class 쪽지 : Form
    {
        private string userid;
        BindingSource 받은쪽지_bs = new BindingSource();
        BindingSource 보낸쪽지_bs = new BindingSource();
        public 쪽지()
        {
            InitializeComponent();
            InitializeSettings();
        }

        private void InitializeSettings()
        {
            받은쪽지검색_콤보박스.Items.Add("보낸 사람");
            받은쪽지검색_콤보박스.Items.Add("제목");
            받은쪽지검색_콤보박스.Items.Add("내용");
            보낸쪽지검색_콤보박스.Items.Add("받는 사람");
            보낸쪽지검색_콤보박스.Items.Add("제목");
            보낸쪽지검색_콤보박스.Items.Add("내용");
            받은쪽지검색_콤보박스.SelectedIndex = 0;
            보낸쪽지검색_콤보박스.SelectedIndex = 0;
            쪽지수신자부서_콤보박스.SelectedIndex = 0;


            MySqlDataReader reader;
            string query = "SELECT DISTINCT 부서명 AS 부서 FROM 사원;";
            DBManager.GetDBManager().OpenConnection();
            using (reader = DBManager.GetDBManager().SetQuery(query).ExecuteReader())
            {
                while (reader.Read())
                {
                    쪽지수신자부서_콤보박스.Items.Add(reader["부서"].ToString());
                }
            }
            DBManager.GetDBManager().CloseConnection();

            query = $"SELECT 직급번호 FROM 사원 WHERE 사원번호 = '{Properties.Settings.Default.myID}'";
            userid = Properties.Settings.Default.myID; //로그인한 사원 번호 받기
            int userRank = Convert.ToInt32(사용자매니저.GetInstance().Get_직급번호()); //로그인한 사원 직급번호 받기
            DBManager.GetDBManager().OpenConnection();
            reader = DBManager.GetDBManager().SetQuery(query).ExecuteReader();
            reader.Read();
            userRank = Convert.ToInt32(reader["직급번호"]);
            DBManager.GetDBManager().CloseConnection();

            if (userRank < 2)
            {
                쪽지전체송신_체크박스.Enabled = true;
            }
            받은쪽지데이터그리드뷰불러오기();
        }

        private void 쪽지_탭컨트롤_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedIndex = 쪽지_탭컨트롤.SelectedIndex;
            DBManager.GetDBManager().OpenConnection();

            switch (selectedIndex)
            {
                case 0:
                    {
                        받은쪽지데이터그리드뷰불러오기();
                        break;
                    }

                case 1:
                    {
                        보낸쪽지데이터그리드뷰불러오기();
                        break;
                    }
            }
            DBManager.GetDBManager().CloseConnection();
        }

        private void 받은쪽지데이터그리드뷰불러오기()
        {

            string query = $"SELECT 쪽지.쪽지_Read AS 읽음, 쪽지.쪽지_title AS 제목, concat(송신자.부서명,'_',송신자.직급,'_',송신자.이름) AS 송신자,쪽지.쪽지_Content AS 내용,쪽지.쪽지_CreateDate AS 날짜,쪽지.쪽지_id AS 쪽지번호 FROM 쪽지 join 사원 AS 송신자 on 쪽지.송신자_사원번호=송신자.사원번호 where '{userid}'=쪽지.수신자_사원번호 order by 날짜 DESC;";
            MySqlDataAdapter adapter = DBManager.GetDBManager().SetQuery(query).GetAdapter();
            DataTable dt = new DataTable();

            adapter.Fill(dt);
            받은쪽지검색결과_데이터그리드뷰.DataSource = 받은쪽지_bs;
            받은쪽지_bs.DataSource = dt;
            받은쪽지검색결과_데이터그리드뷰.Columns[0].Width = 40;
            받은쪽지검색결과_데이터그리드뷰.Columns[1].Width = 150;
        }

        private void 보낸쪽지데이터그리드뷰불러오기()
        {
            string query = $"SELECT  쪽지.쪽지_Read AS 읽음, 쪽지.쪽지_title AS 제목,concat(수신자.부서명,'_',수신자.직급,'_',수신자.이름) AS 수신자, 쪽지.쪽지_Content AS 내용,쪽지.쪽지_CreateDate AS 날짜,쪽지.쪽지_id AS 쪽지번호 FROM 쪽지 join 사원 AS 수신자 on 쪽지.수신자_사원번호=수신자.사원번호 where '{userid}'=쪽지.송신자_사원번호 order by 날짜 DESC;";
            MySqlDataAdapter adapter = DBManager.GetDBManager().SetQuery(query).GetAdapter();
            DataTable dt = new DataTable();

            adapter.Fill(dt);
            보낸쪽지검색결과_데이터그리드뷰.DataSource = 보낸쪽지_bs;
            보낸쪽지_bs.DataSource = dt;
            보낸쪽지검색결과_데이터그리드뷰.Columns[0].Width = 40;
            보낸쪽지검색결과_데이터그리드뷰.Columns[1].Width = 150;
        }

        private void 받은쪽지검색_버튼_Click(object sender, EventArgs e)
        {
            string serachCondition = 받은쪽지검색_콤보박스.Text;
            string query = null;
            switch (serachCondition)
            {
                case "보낸 사람":
                    {
                        query = $"SELECT  쪽지.쪽지_Read AS 읽음,concat(송신자.부서명,'_',송신자.직급,'_',송신자.이름) AS 송신자, 쪽지.쪽지_title AS 제목, 쪽지.쪽지_Content AS 내용,쪽지.쪽지_CreateDate AS 날짜,쪽지.쪽지_id AS 쪽지번호 FROM 쪽지 join 사원 AS 송신자 on 쪽지.송신자_사원번호=송신자.사원번호 where '{userid}'=쪽지.수신자_사원번호 AND concat(송신자.부서명,'_',송신자.직급,'_',송신자.이름) LIKE '%{받은쪽지검색_텍스트박스.Text}%' order by 날짜 DESC;";
                        break;
                    }
                case "제목":
                    {
                        query = $"SELECT  쪽지.쪽지_Read AS 읽음,concat(송신자.부서명,'_',송신자.직급,'_',송신자.이름) AS 송신자, 쪽지.쪽지_title AS 제목, 쪽지.쪽지_Content AS 내용,쪽지.쪽지_CreateDate AS 날짜,쪽지.쪽지_id AS 쪽지번호 FROM 쪽지 join 사원 AS 송신자 on 쪽지.송신자_사원번호=송신자.사원번호 where '{userid}'=쪽지.수신자_사원번호 AND  쪽지.쪽지_title LIKE '%{받은쪽지검색_텍스트박스.Text}%' order by 날짜 DESC;";
                        break;
                    }
                case "내용":
                    {
                        query = $"SELECT  쪽지.쪽지_Read AS 읽음,concat(송신자.부서명,'_',송신자.직급,'_',송신자.이름) AS 송신자, 쪽지.쪽지_title AS 제목, 쪽지.쪽지_Content AS 내용,쪽지.쪽지_CreateDate AS 날짜,쪽지.쪽지_id AS 쪽지번호 FROM 쪽지 join 사원 AS 송신자 on 쪽지.송신자_사원번호=송신자.사원번호 where '{userid}'=쪽지.수신자_사원번호 AND  쪽지.쪽지_Content LIKE '%{받은쪽지검색_텍스트박스.Text}%' order by 날짜 DESC;";
                        break;
                    }
                default:
                    {
                        MessageBox.Show("검색 조건을 선택해주세요");
                        break;
                    }
            }
            if (query != null)
            {
                DBManager.GetDBManager().OpenConnection();
                MySqlDataAdapter adapter = DBManager.GetDBManager().SetQuery(query).GetAdapter();
                DataTable dt = new DataTable();

                adapter.Fill(dt);
                받은쪽지검색결과_데이터그리드뷰.DataSource = 받은쪽지_bs;
                받은쪽지_bs.DataSource = dt;
                받은쪽지검색결과_데이터그리드뷰.Columns[0].Width = 40;
                받은쪽지검색결과_데이터그리드뷰.Columns[1].Width = 150;
                DBManager.GetDBManager().CloseConnection();
            }
        }

        private void 보낸쪽지검색_버튼_Click(object sender, EventArgs e)
        {
            string serachCondition = 보낸쪽지검색_콤보박스.Text;
            string query = null;
            switch (serachCondition)
            {
                case "받는 사람":
                    {
                        query = $"SELECT  쪽지.쪽지_Read AS 읽음, 쪽지.쪽지_title AS 제목,concat(수신자.부서명,'_',수신자.직급,'_',수신자.이름) AS 수신자, 쪽지.쪽지_Content AS 내용,쪽지.쪽지_CreateDate AS 날짜,쪽지.쪽지_id AS 쪽지번호 FROM 쪽지 join 사원 AS 수신자 on 쪽지.수신자_사원번호=수신자.사원번호 where '{userid}'=쪽지.송신자_사원번호 AND concat(수신자.부서명,'_',수신자.직급,'_',수신자.이름) LIKE '%{보낸쪽지검색_텍스트박스.Text}%' order by 날짜 DESC;";
                        break;
                    }
                case "제목":
                    {
                        query = $"SELECT  쪽지.쪽지_Read AS 읽음, 쪽지.쪽지_title AS 제목,concat(수신자.부서명,'_',수신자.직급,'_',수신자.이름) AS 수신자, 쪽지.쪽지_Content AS 내용,쪽지.쪽지_CreateDate AS 날짜,쪽지.쪽지_id AS 쪽지번호 FROM 쪽지 join 사원 AS 수신자 on 쪽지.수신자_사원번호=수신자.사원번호 where '{userid}'=쪽지.송신자_사원번호 AND 쪽지.쪽지_title LIKE '%{보낸쪽지검색_텍스트박스.Text}%' order by 날짜 DESC;";
                        break;
                    }
                case "내용":
                    {
                        query = $"SELECT  쪽지.쪽지_Read AS 읽음, 쪽지.쪽지_title AS 제목,concat(수신자.부서명,'_',수신자.직급,'_',수신자.이름) AS 수신자, 쪽지.쪽지_Content AS 내용,쪽지.쪽지_CreateDate AS 날짜,쪽지.쪽지_id AS 쪽지번호 FROM 쪽지 join 사원 AS 수신자 on 쪽지.수신자_사원번호=수신자.사원번호 where '{userid}'=쪽지.송신자_사원번호 AND 쪽지.쪽지_Content LIKE '%{보낸쪽지검색_텍스트박스.Text}%' order by 날짜 DESC;";
                        break;
                    }
                default:
                    {
                        MessageBox.Show("검색 조건을 선택해주세요");
                        break;
                    }
            }
            if (query != null)
            {
                DBManager.GetDBManager().OpenConnection();
                MySqlDataAdapter adapter = DBManager.GetDBManager().SetQuery(query).GetAdapter();
                DataTable dt = new DataTable();

                adapter.Fill(dt);
                보낸쪽지검색결과_데이터그리드뷰.DataSource = 보낸쪽지_bs;
                보낸쪽지_bs.DataSource = dt;
                보낸쪽지검색결과_데이터그리드뷰.Columns[0].Width = 40;
                보낸쪽지검색결과_데이터그리드뷰.Columns[1].Width = 150;
                DBManager.GetDBManager().CloseConnection();
            }
        }

        private void initsettings()
        {
            MessageBox.Show("송신 완료");
            쪽지제목작성란_텍스트박스.Text = "";
            쪽지내용작성란_리치텍스트박스.Text = "";
            쪽지수신자부서_콤보박스.SelectedIndex = 0;
            쪽지수신자직급이름_콤보박스.SelectedIndex = -1;
            쪽지수신자직급이름_콤보박스.Enabled = false;
        }

        private void 쪽지보내기_버튼_Click(object sender, EventArgs e)
        {
            string title, content, receiver, query = null;
            List<string> members = new List<string>();

            title = 쪽지제목작성란_텍스트박스.Text;
            content = 쪽지내용작성란_리치텍스트박스.Text;
            receiver = 쪽지수신자직급이름_콤보박스.Text;
            if (쪽지전체송신_체크박스.Checked)
            {
                if (쪽지제목작성란_텍스트박스.Text.Trim() == "" || 쪽지내용작성란_리치텍스트박스.Text.Trim() == "")
                {
                    MessageBox.Show("제목 및 내용을 작성해주세요");
                    return;
                }
                query = "SELECT 사원.사원번호 FROM 사원;";
                DBManager.GetDBManager().OpenConnection();
                using (MySqlDataReader reader = DBManager.GetDBManager().SetQuery(query).ExecuteReader())
                {
                    while (reader.Read())
                    {
                        members.Add(reader["사원번호"].ToString());
                    }
                }
                foreach (string member in members)
                {
                    쪽지보내기(member);
                }
                DBManager.GetDBManager().CloseConnection();
                initsettings();
            }
            else
            {
                if (쪽지수신자직급이름_콤보박스.SelectedItem != null && 쪽지제목작성란_텍스트박스.Text.Trim() != "" && 쪽지내용작성란_리치텍스트박스.Text.Trim() != "")
                {
                    string input = 쪽지수신자직급이름_콤보박스.SelectedItem.ToString();

                    int startIndex = input.IndexOf('[');
                    int endIndex = input.IndexOf(']');


                    if (startIndex != -1 && endIndex != -1)
                    {
                        string index = input.Substring(startIndex + 1, endIndex - startIndex - 1);
                        if (쪽지보내기(index))
                        {
                            initsettings();
                        }
                    }
                }
                else if (쪽지수신자직급이름_콤보박스.SelectedItem == null)
                {
                    MessageBox.Show("수신자를 골라주세요");
                }
                else if (쪽지제목작성란_텍스트박스.Text.Trim() == "")
                {
                    MessageBox.Show("제목을 작성해주세요");
                }
                else if (쪽지내용작성란_리치텍스트박스.Text.Trim() == "")
                {
                    MessageBox.Show("내용을 작성해주세요");
                }
            }
        }

        private bool 쪽지보내기(string receiver)
        {
            if (receiver != userid) //송신자 수신자 같은지 확인하기
            {
                string query = $"INSERT INTO 쪽지(송신자_사원번호,수신자_사원번호,쪽지_title,쪽지_Content,쪽지_CreateDate) values('{userid}','{receiver}','{쪽지제목작성란_텍스트박스.Text}','{쪽지내용작성란_리치텍스트박스.Text}',Now());";
                DBManager.GetDBManager().SetQuery(query).ExecuteNonQuery();
                return true;
            }
            else if (!쪽지전체송신_체크박스.Checked)
            {
                MessageBox.Show("자신에게 보낼 수 없습니다.");
                return false;
            }
            return false;
        }

        private void 받은쪽지검색결과_데이터그리드뷰_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                DataGridViewRow selectedRow = 받은쪽지검색결과_데이터그리드뷰.Rows[e.RowIndex];

                받은쪽지제목_텍스트박스.Text = selectedRow.Cells["제목"].Value.ToString();
                받은쪽지송신자_텍스트박스.Text = selectedRow.Cells["송신자"].Value.ToString();
                받은쪽지날짜_라벨.Text = selectedRow.Cells["날짜"].Value.ToString();
                받은쪽지내용_리치텍스트박스.Text = selectedRow.Cells["내용"].Value.ToString();
                받은쪽지검색결과_데이터그리드뷰.Rows[e.RowIndex].Cells["읽음"].Value = true;

                int index = (int)selectedRow.Cells["쪽지번호"].Value;
                string query = $"UPDATE 쪽지 SET 쪽지_Read=1 WHERE 쪽지_id={index};";
                DBManager.GetDBManager().SetQuery(query).ExecuteNonQuery();
            }
        }

        private void 보낸쪽지검색결과_데이터그리드뷰_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                DataGridViewRow selectedRow = 보낸쪽지검색결과_데이터그리드뷰.Rows[e.RowIndex];

                보낸쪽지제목_텍스트박스.Text = selectedRow.Cells["제목"].Value.ToString();
                보낸쪽지송신자_텍스트박스.Text = selectedRow.Cells["수신자"].Value.ToString();
                보낸쪽지날짜_라벨.Text = selectedRow.Cells["날짜"].Value.ToString();
                보낸쪽지내용_리치텍스트박스.Text = selectedRow.Cells["내용"].Value.ToString();
            }
        }

        private void 쪽지수신자부서_콤보박스_SelectedIndexChanged(object sender, EventArgs e)
        {
            쪽지수신자직급이름_콤보박스.Enabled = true;
            쪽지수신자직급이름_콤보박스.Items.Clear();
            string selectedDepartment = 쪽지수신자부서_콤보박스.SelectedItem.ToString();
            string query = $"SELECT concat(직급,'_',이름,'[',사원번호,']') AS 사원명 FROM 사원 WHERE 부서명='{selectedDepartment}';";
            DBManager.GetDBManager().OpenConnection();
            using (MySqlDataReader reader = DBManager.GetDBManager().SetQuery(query).ExecuteReader())
            {
                while (reader.Read())
                {
                    쪽지수신자직급이름_콤보박스.Items.Add(reader["사원명"].ToString());
                }
            }
            DBManager.GetDBManager().CloseConnection();
        }

        private void 받은쪽지전달_버튼_Click(object sender, EventArgs e)
        {
            if (받은쪽지제목_텍스트박스.Text.Trim() != "" && 받은쪽지내용_리치텍스트박스.Text.Trim() != "")
            {
                쪽지제목작성란_텍스트박스.Text = "[전달]" + 받은쪽지제목_텍스트박스.Text.Trim();
                쪽지내용작성란_리치텍스트박스.Text = $"[전달-{받은쪽지송신자_텍스트박스.Text}]\n" + 받은쪽지내용_리치텍스트박스.Text.Trim();
            }
            else
            {
                MessageBox.Show("전달할 쪽지를 선택해주세요");
            }
        }

        private void 보낸쪽지전달_버튼_Click(object sender, EventArgs e)
        {
            if (보낸쪽지제목_텍스트박스.Text.Trim() != "" && 보낸쪽지내용_리치텍스트박스.Text.Trim() != "")
            {
                쪽지제목작성란_텍스트박스.Text = "[전달]" + 보낸쪽지제목_텍스트박스.Text.Trim();
                쪽지내용작성란_리치텍스트박스.Text = $"[전달-{보낸쪽지송신자_텍스트박스.Text}]\n" + 보낸쪽지내용_리치텍스트박스.Text.Trim();
            }
            else
            {
                MessageBox.Show("전달할 쪽지를 선택해주세요");
            }
        }

        private void 받은쪽지_새로고침_버튼_Click(object sender, EventArgs e)
        {
            받은쪽지데이터그리드뷰불러오기();
        }

        private void 보낸쪽지_새로고침_버튼_Click(object sender, EventArgs e)
        {
            보낸쪽지데이터그리드뷰불러오기();
        }

        private void 받은쪽지_안읽은쪽지_체크박스_CheckedChanged(object sender, EventArgs e)
        {
            if (받은쪽지_안읽은쪽지_체크박스.Checked)
            {
                받은쪽지_bs.Filter = "읽음 = 0";
            }
            else
            {
                받은쪽지_bs.Filter = "";
            }
        }

        private void 보낸쪽지_안읽은쪽지_체크박스_CheckedChanged(object sender, EventArgs e)
        {
            if (보낸쪽지_안읽은쪽지_체크박스.Checked)
            {
                보낸쪽지_bs.Filter = "읽음 = 0";
            }
            else
            {
                보낸쪽지_bs.Filter = "";
            }
        }
    }
}
