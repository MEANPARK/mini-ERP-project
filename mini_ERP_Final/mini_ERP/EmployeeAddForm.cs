using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace TeamProject_test_v1
{
    public partial class EmployeeAddForm : Form
    {
        List<int> departmentNumbers = new List<int>();
        List<int> positioinNumbers = new List<int>();
        public EmployeeAddForm()
        {
            InitializeComponent();
            setting();
        }

        private void setting()
        {
            //가장 마지막 사원번호를 받아온다.
            string query = "SELECT 사원번호 FROM s5584720.사원 order by 사원번호 DESC LIMIT 1";
            DBManager.GetDBManager().OpenConnection();
            MySqlDataReader reader = DBManager.GetDBManager().SetQuery(query).ExecuteReader();
            reader.Read();
            textBoxNum.Text = (Convert.ToInt32(reader["사원번호"]) + 1).ToString("D8"); //D8 = 8자리
            DBManager.GetDBManager().CloseConnection();
            textBoxName.Clear();
            comboBoxGender.SelectedIndex = -1;
            textBoxAge.Clear();
            textBoxPhone.Clear();
            textBoxAddress1.Clear();
            textBoxAddress2.Clear();
            textBoxAddress3.Clear();
            textBoxEmail.Clear();
            textBoxAccount.Clear();
            setDepartmentComboBox();
            setPositionComboBox();
        }

        //부서 콤보박스
        private void setDepartmentComboBox()
        {
            comboBoxDepartment.Items.Clear();
            DataSet ds = new DataSet();
            string query = "SELECT 부서코드, 부서명 FROM 부서 WHERE NOT 부서명 = '사장실'";
            DBManager.GetDBManager().OpenConnection();
            MySqlDataReader reader = DBManager.GetDBManager().SetQuery(query).ExecuteReader();
            while (reader.Read())
            {
                departmentNumbers.Add(Convert.ToInt32(reader["부서코드"]));
                comboBoxDepartment.Items.Add(reader["부서명"].ToString());
            }
            DBManager.GetDBManager().CloseConnection();
        }

        //직급 콤보박스
        private void setPositionComboBox()
        {
            comboBoxPosition.Items.Clear();
            DataSet ds = new DataSet();
            string query = "SELECT 직급번호, 직급 FROM 직급 WHERE NOT 직급 = '사장'";
            DBManager.GetDBManager().OpenConnection();
            MySqlDataReader reader = DBManager.GetDBManager().SetQuery(query).ExecuteReader();
            while (reader.Read())
            {
                positioinNumbers.Add(Convert.ToInt32(reader["직급번호"]));
                comboBoxPosition.Items.Add(reader["직급"].ToString());
            }
            DBManager.GetDBManager().CloseConnection();
        }

        //필수 입력 정보 체크
        private bool checkRule()
        {
            if (String.IsNullOrEmpty(textBoxName.Text) || String.IsNullOrEmpty(textBoxAge.Text) || comboBoxGender.SelectedIndex == -1 || String.IsNullOrEmpty(textBoxPhone.Text))
            {
                MessageBox.Show("필수 정보를 입력해주세요.");
                return false;
            }
            if (comboBoxDepartment.SelectedIndex == -1 || comboBoxPosition.SelectedIndex == -1)
            {
                MessageBox.Show("부서와 직급을 선택해주세요.");
                return false;
            }
            if (!String.IsNullOrEmpty(textBoxPhone.Text))
            {
                Regex phoneRegex = new Regex(@"01{1}[016789]{1}-[0-9]{3,4}-[0-9]{4}");
                if (!phoneRegex.IsMatch(textBoxPhone.Text))
                {
                    MessageBox.Show("전화번호를 다시 확인해주세요.");
                    return false;
                }
            }
            if (!String.IsNullOrEmpty(textBoxEmail.Text))
            {
                Regex emailRegex = new Regex(@"^([0-9a-zA-Z]+)@([0-9a-zA-Z]+)(\.[0-9a-zA-Z]+){1,}$");
                if (!emailRegex.IsMatch(textBoxEmail.Text))
                {
                    MessageBox.Show("이메일을 다시 확인해주세요.");
                    return false;
                }
            }

            if(String.IsNullOrEmpty(textBoxAddress1.Text) && String.IsNullOrEmpty(textBoxAddress2.Text) && !String.IsNullOrEmpty(textBoxAddress3.Text))
            {
                MessageBox.Show("주소를 확인해주세요.");
                return false;
            }

            if (comboBoxPosition.SelectedItem.ToString() == "부서장")
            {
                string query = $"SELECT 부서장_id FROM 부서 WHERE 부서명 = '{comboBoxDepartment.SelectedItem.ToString()}'";
                DBManager.GetDBManager().OpenConnection();
                MySqlDataReader reader = DBManager.GetDBManager().SetQuery(query).ExecuteReader();
                reader.Read();
                if (!String.IsNullOrEmpty(reader.GetString(0)))
                {
                    MessageBox.Show("해당 부서에 부서장이 존재합니다. \n부서장 자리 확인 후 진행해주시기 바랍니다.");
                    DBManager.GetDBManager().CloseConnection();
                    return false;
                }
                DBManager.GetDBManager().CloseConnection();
            }
            return true;
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (!checkRule()) return;

            string 부서명 = comboBoxDepartment.SelectedItem.ToString();
            int 부서번호 = departmentNumbers[comboBoxDepartment.SelectedIndex];
            string 직급 = comboBoxPosition.SelectedItem.ToString();
            int 직급번호 = positioinNumbers[comboBoxPosition.SelectedIndex];

            //주소 => (우편번호)주소(상세주소)로 저장
            string address = $"({textBoxAddress1.Text}){textBoxAddress2.Text}";
            if (!String.IsNullOrEmpty(textBoxAddress3.Text))
            {
                address += $"({textBoxAddress3.Text})";
            }
            if (String.IsNullOrEmpty(textBoxAddress1.Text))
            {
                address = "";
            }
            //사원번호 이름 성별 생년월일 전화번호 주소 이메일 계좌번호 부서번호 부서명 직급번호 직급
            string query = $"INSERT INTO 사원 VALUES('{textBoxNum.Text}', '{textBoxName.Text}', '{comboBoxGender.SelectedItem}', '{textBoxAge.Text}', '{textBoxPhone.Text}', '{address}', '{textBoxEmail.Text}', '{textBoxAccount.Text}', {부서번호}, '{부서명}', {직급번호}, '{직급}')";
            int result = DBManager.GetDBManager().SetQuery(query).ExecuteNonQuery();
            if (result == -1)
            {
                MessageBox.Show("등록 실패");
                return;
            }
            query = $"INSERT INTO 로그인 VALUES('{textBoxNum.Text}', '{security.getinstance_().getpassword(textBoxNum.Text)}')";
            result = DBManager.GetDBManager().SetQuery(query).ExecuteNonQuery();
            if (result == -1)
            {
                MessageBox.Show("등록 실패");
                return;
            }
            else MessageBox.Show("등록되었습니다.");
            setting();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            textBoxAge.Text = dateTimePicker1.Value.ToString("yyyy-MM-dd");
        }

        private void buttonAdrSearch_Click(object sender, EventArgs e)
        {
            주소검색Form form = new 주소검색Form();
            form.ShowDialog();

            textBoxAddress1.Text = form.gstrZipCode;
            textBoxAddress2.Text = form.gstrAddress1;
        }
    }
}
