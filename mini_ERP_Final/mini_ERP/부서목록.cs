using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Button = System.Windows.Forms.Button;

namespace TeamProject_test_v1
{
    public partial class 부서목록 : Form
    {
        public int now_selecting = -1;
        DataTable at = new DataTable();
        public string query;
        private EmployeeInfo emp;
        public 부서목록()
        {
            InitializeComponent();
            emp = new EmployeeInfo();
            emp.setEmployeeInfo(Properties.Settings.Default.myID);
        }
        private void Form2_Load(object sender, EventArgs e)
        {
            this.query = "SELECT  b.부서코드,  b.부서명,  CONCAT(m.이름, ' (', m.사원번호, ')') AS 부서장 FROM  s5584720.부서 b LEFT JOIN  s5584720.사원 m ON b.부서코드 = m.부서번호 and m.직급번호<2 or m.직급번호 is null ORDER BY b.부서코드 ASC";
            DBManager.GetDBManager().OpenConnection();
            MySqlDataReader reader = DBManager.GetDBManager().SetQuery(query).ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(reader);
            DBManager.GetDBManager().CloseConnection();
            dataGridView1.DataSource = dt;
        }
        private void operate_button_click(object sender, EventArgs e)
        {
            if (emp.직급 != "사장")
            {
                MessageBox.Show("권한이 없습니다.");
                return;
            }

            DataGridViewRow row;
            
            int seletcting_ = 0;
            Button btn = (Button)sender;
            string values = btn.Tag as string;
            switch (values)
            {
                case "등록":
                    adder();
                    break;
                case "수정":
                    if (dataGridView1.SelectedRows.Count == 0)
                    {
                        MessageBox.Show("부서를 선택해주세요");
                        return;
                    }
                    row = dataGridView1.SelectedRows[0];
                    if (row.Cells[0].Value.ToString() == "0")
                    {
                        MessageBox.Show("수정 및 삭제할 수 없습니다.");
                        return;
                    }
                    seletcting_ = Convert.ToInt32(dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells[0].Value.ToString());
                    editer(seletcting_);
                    break;
                case "삭제":
                    if (dataGridView1.SelectedRows.Count == 0)
                    {
                        MessageBox.Show("부서를 선택해주세요");
                        return;
                    }
                    row = dataGridView1.SelectedRows[0];
                    if (row.Cells[0].Value.ToString() == "0")
                    {
                        MessageBox.Show("수정 및 삭제할 수 없습니다.");
                        return;
                    }
                    deleteer();
                    break;
                case "추가":
                    newmember_click();//추가버튼활성화
                    break;
                case "수정완료":
                    editer_click();//수정버튼활성화
                    break;
                case "취소":
                    if (new_groupBox.Visible == true) new_groupBox.Visible = false;
                    if (edit_groupbox.Visible == true) edit_groupbox.Visible = false;
                    break;
            }
            Form2_Load(sender, e);
            dataGridView1.ClearSelection();
        }
        private void adder()
        {
            DataTable dt = new DataTable();
            new_groupBox.Visible = true;
            this.query = "SELECT concat(이름,'(',사원번호,')') as '책임자' FROM s5584720.사원 where not 직급번호 = 0 or 직급번호 is NULL";
            DBManager.GetDBManager().OpenConnection();
            MySqlDataReader reader = DBManager.GetDBManager().SetQuery(this.query).ExecuteReader();
            dt.Load(reader);
            DBManager.GetDBManager().CloseConnection();
            member_combobox.Items.Clear();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                member_combobox.Items.Add(dt.Rows[i][0]);
            }

            this.query = "SELECT MAX(부서코드) FROM 부서";
            DBManager.GetDBManager().OpenConnection();
            reader = DBManager.GetDBManager().SetQuery(this.query).ExecuteReader();
            reader.Read();
            newcode_textBox.Text = (Convert.ToInt32(reader["MAX(부서코드)"]) + 1).ToString();
            DBManager.GetDBManager().CloseConnection();
        }

        private void member_combobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (new_groupBox.Visible == true) newmember_textBox.Text = member_combobox.SelectedItem.ToString();           
        }
        private void editmember_combobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (edit_groupbox.Visible == true) editmember_textBox.Text = editmember_combobox.SelectedItem.ToString();
        }

        private void newmember_click()
        {
            if (String.IsNullOrEmpty(newname_textBox.Text) == true)
            {
                MessageBox.Show("부서명을 입력해주세요.");
                return;
            }
            if (String.IsNullOrEmpty(newmember_textBox.Text) == true && member_combobox.SelectedIndex == -1)
            {
                MessageBox.Show("부서장을 임명해주세요.");
                return;
            }
            Regex regex = new Regex(@"^[ㄱ-ㅎ가-힣a-zA-Z0-9]+\([0-9]{8}\)$");
            if (regex.IsMatch(newmember_textBox.Text) != true)
            {
                MessageBox.Show("형식을 확인해주세요.");
                return;
            }
            var confirmResult = MessageBox.Show("추가하시겠습니까?", "확인창", MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.No)
            {
                return;
            }

            int checking = searching();
            if (checking == 1)
            {
                string[] splitmth = { "(", ")" };
                string[] name_split = newmember_textBox.Text.Split(splitmth, StringSplitOptions.RemoveEmptyEntries);
                if (!String.IsNullOrEmpty(newmember_textBox.Text))
                {
                    this.query = $"update s5584720.부서 set 부서장_id=NULL where 부서장_id='{name_split[1]}'";
                    DBManager.GetDBManager().SetQuery(query).ExecuteNonQuery();
                    this.query = $"insert into s5584720.부서 values({Convert.ToInt32(newcode_textBox.Text)},'{newname_textBox.Text}','{name_split[1]}')";
                    DBManager.GetDBManager().SetQuery(query).ExecuteNonQuery();
                    this.query = $"update s5584720.사원 set 부서번호={Convert.ToInt32(newcode_textBox.Text)}, 부서명='{newname_textBox.Text}', 직급번호=1, 직급='부서장' where 사원번호='{name_split[1]}'";
                    DBManager.GetDBManager().SetQuery(query).ExecuteNonQuery();
                    MessageBox.Show("부서추가완료", "완료");
                    new_groupBox.Visible = false;
                }
                else
                {
                    this.query = $"insert into s5584720.부서(부서코드, 부서명) values({Convert.ToInt32(newcode_textBox.Text)},'{newname_textBox.Text}')";
                    DBManager.GetDBManager().SetQuery(query).ExecuteNonQuery();
                    MessageBox.Show("부서추가완료", "완료");
                    new_groupBox.Visible = false;
                }
                newcode_textBox.Clear();
                newmember_textBox.Clear();
                newname_textBox.Clear();
            }
            else MessageBox.Show("이미 존재하는 부서코드입니다.", "생성실패");
            new_groupBox.Visible = false;
        }
        private int searching()
        {
            DataTable dtt = new DataTable();
            this.query = $"select * from s5584720.부서 where 부서코드={Convert.ToInt32(newcode_textBox.Text)}";
            DBManager.GetDBManager().OpenConnection();
            MySqlDataReader reader = DBManager.GetDBManager().SetQuery(this.query).ExecuteReader();
            dtt.Load(reader);
            DBManager.GetDBManager().CloseConnection();
            if (dtt.Rows.Count == 0)
            {
                return 1;
            }
            else { return 0; }
        }
        private void editer(int seletction)
        {
            now_selecting = seletction;
            edit_groupbox.Visible = true;
            at.Clear();
            this.query = $"SELECT s.부서코드, s.부서명, concat(m.이름,'(',m.사원번호,')') as '책임자' FROM s5584720.부서 s LEFT join s5584720.사원 m on s.부서장_id = m.사원번호 where 부서코드 = {now_selecting}";
            DBManager.GetDBManager().OpenConnection();
            MySqlDataReader reader = DBManager.GetDBManager().SetQuery(this.query).ExecuteReader();
            at.Load(reader);
            DBManager.GetDBManager().CloseConnection();
            editcode_textBox.Text = at.Rows[0][0].ToString();//기존부서코드
            editname_textBox.Text = at.Rows[0][1].ToString();//기존부서명
            editmember_textBox.Text = at.Rows[0][2].ToString();//기존사원 이름+사원번호
            member_show();
        }
        private void member_show()
        {
            DataTable dtt = new DataTable();
            this.query = "SELECT concat(이름,'(',사원번호,')') as '책임자' FROM s5584720.사원 where not 직급번호 = 0 or 직급번호 is NULL";
            DBManager.GetDBManager().OpenConnection();
            MySqlDataReader reader = DBManager.GetDBManager().SetQuery(this.query).ExecuteReader();
            dtt.Load(reader);
            DBManager.GetDBManager().CloseConnection();
            editmember_combobox.Items.Clear();
            for (int i = 0; i < dtt.Rows.Count; i++)
            {
                editmember_combobox.Items.Add(dtt.Rows[i][0]);
            }
        }
        private void editer_click()
        {
            int result;
            if (!int.TryParse(editcode_textBox.Text, out result))
            {
                MessageBox.Show("부서코드를 확인해주세요");
                return;
            }
            if (String.IsNullOrEmpty(editname_textBox.Text) == true)
            {
                MessageBox.Show("부서명을 입력해주세요.");
                return;
            }
            if (String.IsNullOrEmpty(editmember_textBox.Text) == true && editmember_combobox.SelectedIndex == -1)
            {
                MessageBox.Show("부서장을 임명해주세요.");
                return;
            }
            Regex regex = new Regex(@"^[ㄱ-ㅎ가-힣a-zA-Z0-9]+\([0-9]{8}\)$");
            if (regex.IsMatch(editmember_textBox.Text) != true)
            {
                MessageBox.Show("형식을 확인해주세요.");
                return;
            }
            var confirmResult = MessageBox.Show("수정하시겠습니까?", "확인창", MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.No)
            {
                return;
            }

            string[] splitmth = { "(", ")" };
            string[] old_split = at.Rows[0][2].ToString().Split(splitmth, StringSplitOptions.RemoveEmptyEntries);//기존사원
            string[] name_split = editmember_textBox.Text.Split(splitmth, StringSplitOptions.RemoveEmptyEntries);//새로바뀐사원

            this.query = $"update s5584720.부서 set 부서코드={Convert.ToInt32(editcode_textBox.Text)},부서명='{editname_textBox.Text}',부서장_id='{name_split[1]}' where 부서코드={now_selecting}";//기존부서코드를 가지고있는 부서 변경
            DBManager.GetDBManager().SetQuery(this.query).ExecuteNonQuery();

            this.query = $"update s5584720.사원 s join s5584720.부서 m on s.부서번호=m.부서코드 set s.부서명 = m.부서명 where s.부서번호={Convert.ToInt32(editcode_textBox.Text)} ";//기존부서코드에 해당하는 사원들 부서변경
            DBManager.GetDBManager().SetQuery(this.query).ExecuteNonQuery();

            if (!String.IsNullOrEmpty(at.Rows[0][2].ToString()))
            {
                this.query = $"update s5584720.사원 set 부서번호=NULL,부서명=NULL,직급번호=2, 직급= '일반사원' where 사원번호='{old_split[1]}'";//기존사원직급,직책 변경
                DBManager.GetDBManager().SetQuery(this.query).ExecuteNonQuery();
                
            }

            //부서장이 부서장이 됬다면
            EmployeeInfo employee = new EmployeeInfo();
            employee.setEmployeeInfo(name_split[1]);
            if(employee.직급 == "부서장")
            {
                this.query = $"update s5584720.부서 set 부서장_id = '' WHERE 부서장_id = '{employee.사원번호}'";
                DBManager.GetDBManager().SetQuery(this.query).ExecuteNonQuery();
            }

            this.query = $"update s5584720.사원 set 부서번호={Convert.ToInt32(editcode_textBox.Text)},부서명='{editname_textBox.Text}', 직급번호=1, 직급='부서장' where 사원번호='{name_split[1]}'";//새로운 사원코드에 해당하는 인원 직급변경
            DBManager.GetDBManager().SetQuery(this.query).ExecuteNonQuery();

            MessageBox.Show("부서수정완료", "완료");
            MessageBox.Show("해당 부서 인원 변동", "변동");
            now_selecting = -1;
            editcode_textBox.Clear();
            editname_textBox.Clear();
            editmember_textBox.Clear();

            edit_groupbox.Visible = false;
        }
        private void deleteer()
        {
            var confirmResult = MessageBox.Show("정말 삭제하시겠습니까?", "확인창", MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.No)
            {
                return;
            }
            string query_1 = $"update s5584720.사원 set 부서명='', 부서번호=NULL, 직급번호 = 2, 직급 = '일반사원' where 부서번호={Convert.ToInt32(dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells[0].Value.ToString())}";
            string query_2 = $"delete from s5584720.부서 where 부서코드={Convert.ToInt32(dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells[0].Value.ToString())}";
            DBManager.GetDBManager().SetQuery(query_1).ExecuteNonQuery();
            DBManager.GetDBManager().SetQuery(query_2).ExecuteNonQuery();
            MessageBox.Show("부서삭제완료", "삭제");
            MessageBox.Show("삭제부서 팀원 재배치 요망", "해당부서 해체");
        }

        
    }
}
