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
using ZstdSharp.Unsafe;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace TeamProject_test_v1
{
    public partial class EmployeeUpdateForm : Form
    {
        EmployeeInfo employeeInfo; //현재 데이터그리드뷰에서 선택한 사원의 정보
        List<int> departmentNumbers = new List<int>();
        List<int> positioinNumbers = new List<int>();

        public EmployeeUpdateForm()
        {
            InitializeComponent();
            employeeInfo = new EmployeeInfo();
        }

        public EmployeeUpdateForm(string username)
        {
            InitializeComponent();
            employeeInfo = new EmployeeInfo();
            employeeInfo = employeeInfo.setEmployeeInfo(username);
            setInfo();
        }

        //주소를 텍스트박스에 분리하여 넣기
        private void setAdress()
        {
            int count = 0;
            string input = employeeInfo.주소;
            Regex regex = new Regex(@"\((.*?)\)|([^\(\)]+)");
            MatchCollection matches = regex.Matches(input);

            foreach (Match match in matches)
            {
                if (!string.IsNullOrWhiteSpace(match.Value))
                {
                    if (count == 0)
                    {
                        textBoxAddress1.Text = match.Value.TrimStart('(').TrimEnd(')');
                    }
                    if (count == 1)
                    {
                        textBoxAddress2.Text = match.Value;
                    }
                    if (count == 2)
                    {
                        textBoxAddress3.Text = match.Value.TrimStart('(').TrimEnd(')');
                    }
                }
                count++;
            }
        }

        private void setInfo()
        {
            textBoxNum.Text = employeeInfo.사원번호;
            textBoxName.Text = employeeInfo.이름;
            dateTimePicker1.Value = Convert.ToDateTime(employeeInfo.생년월일);
            comboBoxGender.SelectedIndex = comboBoxGender.FindString(employeeInfo.성별);
            textBoxPhone.Text = employeeInfo.전화번호;
            if (!String.IsNullOrEmpty(employeeInfo.주소))
            {
                setAdress();
            }
            textBoxEmail.Text = employeeInfo.이메일;
            textBoxAccount.Text = employeeInfo.계좌번호;

            setDepartmentComboBox();
            setPositionComboBox();

            if (employeeInfo.직급 == "사장")
            {
                comboBoxDepartment.Items.Add(employeeInfo.부서명);
                comboBoxPosition.Items.Add(employeeInfo.직급);
            }
            if (employeeInfo.사원번호 == 사용자매니저.GetInstance().Get_사원번호())
            {
                comboBoxDepartment.Enabled = false;
                comboBoxPosition.Enabled = false;
            }
            comboBoxDepartment.SelectedIndex = comboBoxDepartment.FindString(employeeInfo.부서명);
            comboBoxPosition.SelectedIndex = comboBoxPosition.FindString(employeeInfo.직급);
        }

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

        private bool checkRule()
        {
            if (String.IsNullOrEmpty(textBoxName.Text) || String.IsNullOrEmpty(textBoxAge.Text) || comboBoxGender.SelectedIndex == -1 || String.IsNullOrEmpty(textBoxPhone.Text))
            {
                MessageBox.Show("필수 정보를 입력해주세요.");
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
            if (String.IsNullOrEmpty(textBoxAddress1.Text) && String.IsNullOrEmpty(textBoxAddress2.Text) && !String.IsNullOrEmpty(textBoxAddress3.Text))
            {
                MessageBox.Show("주소를 확인해주세요.");
                return false;
            }
            return true;
        }

        private void buttonConfirm_Click(object sender, EventArgs e)
        {
            if (!checkRule()) return;

            //변경할 직급과 부서의 정보
            string query = String.Empty;
            string 부서명 = comboBoxDepartment.SelectedItem.ToString();
            string 직급 = comboBoxPosition.SelectedItem.ToString();

            int 부서번호 = 0;
            int 직급번호 = 0;
            if (employeeInfo.직급 != "사장")
            {
                부서번호 = departmentNumbers[comboBoxDepartment.SelectedIndex];
                직급번호 = positioinNumbers[comboBoxPosition.SelectedIndex];
            }

            bool checkPos = false;
            if (사용자매니저.GetInstance().Get_사원번호() == employeeInfo.사원번호 || (employeeInfo.부서명 == 부서명 && employeeInfo.직급 == 직급)) checkPos = true;


            //부서장으로 변경 시 해당 부서에 부서장이 존재하는지 확인
            if (직급 == "부서장" && !checkPos)
            {
                query = $"SELECT 부서장_id FROM 부서 WHERE 부서명 = '{부서명}'";
                DBManager.GetDBManager().OpenConnection();
                MySqlDataReader reader = DBManager.GetDBManager().SetQuery(query).ExecuteReader();
                reader.Read();
                if (!String.IsNullOrEmpty(reader.GetString(0)))
                {
                    MessageBox.Show("해당 부서에 부서장이 존재합니다. \n부서장 자리 확인 후 진행해주시기 바랍니다.");
                    DBManager.GetDBManager().CloseConnection();
                    return;
                }
                DBManager.GetDBManager().CloseConnection();
            }

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

            query = $"UPDATE 사원 SET 생년월일 = '{textBoxAge.Text}', 성별 = '{comboBoxGender.SelectedItem}', 전화번호 = '{textBoxPhone.Text}', 주소 = '{address}', 이메일 = '{textBoxEmail.Text}', " +
                $"계좌번호 = '{textBoxAccount.Text}', 부서번호 = {부서번호}, 부서명 = '{부서명}', 직급번호 = {직급번호}, 직급 = '{직급}' WHERE 사원번호 = '{employeeInfo.사원번호}'";
            var confirmResult = MessageBox.Show("수정하시겠습니까?", "확인창", MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                int result = DBManager.GetDBManager().SetQuery(query).ExecuteNonQuery();
                if (result == -1)
                {
                    MessageBox.Show("쿼리 실패");
                }
                else MessageBox.Show("수정되었습니다.");

                //부서장이 일반사원으로 변경됬다면
                if (employeeInfo.직급 == "부서장" && 직급 == "일반사원")
                {
                    query = $"UPDATE 부서 SET 부서장_id = '' WHERE 부서명 = '{employeeInfo.부서명}'";
                    result = DBManager.GetDBManager().SetQuery(query).ExecuteNonQuery();
                    if (result == -1)
                    {
                        MessageBox.Show("쿼리 실패");
                    }
                    MessageBox.Show($"현재 {employeeInfo.부서명}의 부서장 자리가 공석입니다.");
                }

                //일반사원이 부서장으로 임명됬다면
                if (employeeInfo.직급 == "일반사원" && 직급 == "부서장")
                {
                    query = $"UPDATE 부서 SET 부서장_id = '{employeeInfo.사원번호}' WHERE 부서명 = '{employeeInfo.부서명}'";
                    result = DBManager.GetDBManager().SetQuery(query).ExecuteNonQuery();
                    if (result == -1)
                    {
                        MessageBox.Show("쿼리 실패");
                    }
                }
                this.Close();
            }
        }
        private void buttonCencel_Click(object sender, EventArgs e)
        {
            this.Close();
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
