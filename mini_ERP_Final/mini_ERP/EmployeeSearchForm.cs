using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TeamProject_test_v1
{
    public partial class EmployeeSearchForm : Form
    {
        public EmployeeSearchForm()
        {
            InitializeComponent();
            buttonUpdate.Enabled = true;
            buttonDelete.Enabled = false;
            checkAuthority();
            setDepartmentComboBox();
            dataGridViewEmployee.ReadOnly = true; //데이터그리드뷰 편집 불가능
        }
        public EmployeeSearchForm(string username)
        {
            InitializeComponent();
            buttonUpdate.Enabled = true;
            buttonDelete.Enabled = false;
            checkAuthority();
            setDepartmentComboBox();
            dataGridViewEmployee.ReadOnly = true; //데이터그리드뷰 편집 불가능
            MessageBox.Show(username);
        }

        int authority = -1;
        string 직급 = string.Empty;
        /// <summary>
        /// 사원번호로 직급을 읽어서 권한 확인하고 직급 변수값 변경 0 = '사장 | 1 = '부서장' | 2 = '일반사원'
        /// </summary>
        private void checkAuthority()
        {
            string query = $"SELECT 직급 FROM 사원 WHERE 사원번호 = '{Properties.Settings.Default.myID}'";
            DBManager.GetDBManager().OpenConnection();
            MySqlDataReader reader = DBManager.GetDBManager().SetQuery(query).ExecuteReader();
            if (reader.Read())
            {
                직급 = reader["직급"].ToString();
                if (reader["직급"].ToString() == "사장")
                {
                    authority = 0;
                    buttonDelete.Enabled = true;
                }
                else if (reader["직급"].ToString() == "부서장")
                {
                    authority = 1;
                    buttonDelete.Enabled = true;
                }
                else
                {
                    authority = 2;
                }
            }
            DBManager.GetDBManager().CloseConnection();
        }
        /// <summary>
        /// 모든 부서 콤보박스에 값 추가
        /// </summary>
        private void setDepartmentComboBox()
        {
            comboBoxDepartment.Items.Clear();
            DataSet ds = new DataSet();
            string query = "SELECT 부서명 FROM 부서";
            DBManager.GetDBManager().OpenConnection();
            MySqlDataReader reader = DBManager.GetDBManager().SetQuery(query).ExecuteReader();
            while (reader.Read())
            {
                comboBoxDepartment.Items.Add(reader["부서명"].ToString());
            }
            DBManager.GetDBManager().CloseConnection();
        }

        /// <summary>
        /// 부서명, 이름, 나이에 따른 where문 작성하기
        /// </summary>
        /// <returns></returns>
        private string setWhere()
        {
            string where = "WHERE ";
            DateTime today = DateTime.Today;
            if (comboBoxDepartment.SelectedIndex != -1)
            {
                where += $"부서명 = '{comboBoxDepartment.SelectedItem.ToString()}'";
                if (!String.IsNullOrEmpty(textBoxName.Text))
                {
                    where += $" and 이름 = '{textBoxName.Text}'";
                    if (!String.IsNullOrEmpty(textBoxAge.Text))
                    {
                        where += $" and year(생년월일) = {today.Year - Convert.ToInt32(textBoxAge.Text)}";
                    }
                }
                else if (!String.IsNullOrEmpty(textBoxAge.Text))
                {
                    where += $" and year(생년월일) = {today.Year - Convert.ToInt32(textBoxAge.Text)}";
                }

            }
            else if (!String.IsNullOrEmpty(textBoxName.Text))
            {
                where += $"이름 = '{textBoxName.Text}'";
                if (!String.IsNullOrEmpty(textBoxAge.Text))
                {
                    where += $" and year(생년월일) = {today.Year - Convert.ToInt32(textBoxAge.Text)}";
                }
            }
            else if (!String.IsNullOrEmpty(textBoxAge.Text))
            {
                where += $"year(생년월일) = {today.Year - Convert.ToInt32(textBoxAge.Text)}";
            }

            //"WHERE"만 있다면 비우기
            if (where == "WHERE ") where = string.Empty;
            return where;
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            Regex regex = new Regex(@"^[0-9]+$");
            if (!String.IsNullOrEmpty(textBoxAge.Text) && !regex.IsMatch(textBoxAge.Text))
            {
                MessageBox.Show("나이를 확인해주세요");
                return;
            }
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            MySqlDataAdapter adpt = new MySqlDataAdapter();
            string query = string.Empty;
            string where = setWhere();

            if (authority == 0)
            {
                query = $"SELECT * FROM 사원 {where}";
                adpt = DBManager.GetDBManager().SetQuery(query).GetAdapter();
                adpt.Fill(ds);
                dt = ds.Tables[0];
            }
            else if (authority == 1)
            {
                query = $"SELECT * FROM 사원 {where}";
                adpt = DBManager.GetDBManager().SetQuery(query).GetAdapter();
                adpt.Fill(ds);
                dt = ds.Tables[0];
            }
            else
            {
                query = $"SELECT 사원번호, 이름, 생년월일, 성별, 이메일, 부서번호, 부서명, 직급번호, 직급 FROM 사원 {where}";
                adpt = DBManager.GetDBManager().SetQuery(query).GetAdapter();
                adpt.Fill(ds);
                dt = ds.Tables[0];
            }
            dt.DefaultView.Sort = "부서번호";
            dataGridViewEmployee.DataSource = dt;
        }

        private void buttonReset_Click(object sender, EventArgs e)
        {
            comboBoxDepartment.SelectedIndex = -1;
            textBoxName.Clear();
            textBoxAge.Clear();
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            if (dataGridViewEmployee.SelectedRows.Count == 0 || dataGridViewEmployee.SelectedRows.Count > 1) return; //여러개 선택 후 수정 불가

            int idx = 0;
            if (authority == 0 || authority == 1)
            {
                idx = 11;
            }
            else if (authority == 2) idx = 8;

            DataGridViewRow row = dataGridViewEmployee.SelectedRows[0];
            if (row.Cells[0].Value.ToString() == Properties.Settings.Default.myID)
            {
                EmployeeUpdateForm form = new EmployeeUpdateForm(row.Cells[0].Value.ToString());
                form.ShowDialog();
                buttonSearch.PerformClick(); //검색하기 자동실행
            }
            else
            {
                if (authority == 2)
                {
                    MessageBox.Show("권한이 없습니다.");
                    return;
                }
                if (row.Cells[idx].Value.ToString() == "사장" || row.Cells[idx].Value.ToString() == "부서장")
                {
                    if (Properties.Settings.Default.myID != "00000000")
                    {
                        MessageBox.Show("권한이 없습니다.");
                        return;
                    }
                }
                EmployeeUpdateForm form = new EmployeeUpdateForm(row.Cells[0].Value.ToString());
                form.ShowDialog();
                buttonSearch.PerformClick(); //검색하기 자동실행
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (dataGridViewEmployee.SelectedRows.Count == 0 || dataGridViewEmployee.SelectedRows.Count > 1) return; //여러개 선택 후 삭제 불가

            DataGridViewRow row = dataGridViewEmployee.SelectedRows[0];

            //일단 사장 삭제 불가능
            if (row.Cells[11].Value.ToString() == "사장")
            {
                return;
            }

            //사장또는 부서장 삭제는 사장만 가능
            if (row.Cells[11].Value.ToString() == "사장" || row.Cells[11].Value.ToString() == "부서장")
            {
                if (Properties.Settings.Default.myID != "00000000")
                {
                    MessageBox.Show("권한이 없습니다.");
                    return;
                }
            }

            string query = string.Empty;

            query = $"DELETE FROM 사원 WHERE 사원번호 = '{row.Cells[0].Value.ToString()}'"; //사원테이블에서 삭제
            var confirmResult = MessageBox.Show($"{row.Cells[9].Value.ToString()} {row.Cells[1].Value.ToString()}님을 삭제하시겠습니까?", "확인창", MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                int result = DBManager.GetDBManager().SetQuery(query).ExecuteNonQuery();
                if (result == -1)
                {
                    MessageBox.Show("삭제 실패");
                    return; //삭제 실패 출력 후 else문으로 삭제되었습니다가 진행되어서 수정하였습니다.
                }

                if (row.Cells[11].Value.ToString() == "부서장")
                {
                    query = $"UPDATE 부서 SET 부서장_id = '' WHERE 부서장_id = '{row.Cells[0].Value.ToString()}'"; //부서테이블에서 삭제
                    result = DBManager.GetDBManager().SetQuery(query).ExecuteNonQuery();
                    if (result == -1)
                    {
                        MessageBox.Show("삭제 실패");
                    }
                }

                query = $"DELETE FROM 로그인 WHERE 사원번호 = '{row.Cells[0].Value.ToString()}'"; //로그인테이블에서도 삭제
                result = DBManager.GetDBManager().SetQuery(query).ExecuteNonQuery();
                if (result == -1)
                {
                    MessageBox.Show("삭제 실패");
                }
                else MessageBox.Show("삭제되었습니다.");
            }
            buttonSearch.PerformClick(); //검색하기 자동실행
        }
    }
}
