using MySql.Data.MySqlClient;
using System.Data;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace TeamProject_test_v1
{
    public partial class 업무 : Form
    {
        public 업무()
        {
            InitializeComponent();
            settingBigCategoryCombobox(); //대분류 콤보박스 아이템 세팅
            loadDataGridView(); //업무 내역 세팅
            taskmaster_add_del_button.Enabled = false;
            taskmaster_modify_button.Enabled = false;
        }

        private void 업무_Load(object sender, EventArgs e)
        {
            권한확인();
        }

        private void 권한확인()
        {
            if (사용자매니저.GetInstance().Get_직급() == "부서장" || 사용자매니저.GetInstance().Get_직급() == "사장")
            {
                taskmaster_add_del_button.Enabled = true;
                taskmaster_modify_button.Enabled = true;
            }

        }

        private Boolean timecheck()
        {
            Boolean flag = false;
            if (start_time_picker.Value >= end_time_picker.Value)
            {
                flag = true;
            }
            return flag;
        }

        private void save_button_Click(object sender, EventArgs e)
        {
            string id = Properties.Settings.Default.myID;
            if (timecheck() == true)
            {
                MessageBox.Show("업무시작시간과 종료시간이 올바르지 안습니다.");
            }
            //업무 테이블 저장 함수
            //업무시간 중복 검사//
            else
            {
                if (checkform() == true)
                {
                    MessageBox.Show("양식을 채워주십시오");
                }
                else
                {
                    Boolean flag = false;
                    string query1 = $"SELECT count(*) FROM 업무 WHERE 업무담당자_id = '{id}' AND (('{start_time_picker.Text}' BETWEEN 시작_time AND 종료_time) OR ('{end_time_picker.Text}' BETWEEN 시작_time AND 종료_time) OR (시작_time BETWEEN '{start_time_picker.Text}' AND '{end_time_picker.Text}') OR (종료_time BETWEEN '{start_time_picker.Text}' AND '{end_time_picker.Text}'))";
                    if (index_textbox.Text != "")
                    {
                        query1 += $" AND 업무_id != {index_textbox.Text}";
                    }
                    query1 += ";";
                    DBManager.GetDBManager().OpenConnection();
                    MySqlDataReader reader1 = DBManager.GetDBManager().SetQuery(query1).ExecuteReader();
                    while (reader1.Read()) //읽을 값이 있다면 실패한 것
                    {
                        if (reader1.GetInt32(0) > 0)
                        { //중복된 값이 있다면 flag를 true로 설정
                            flag = true;
                        }
                    }
                    DBManager.GetDBManager().CloseConnection();
                    //업무시간 중복 검사//
                    if (flag == false) //시간 중복 없을 경우
                    {
                        int small_category_index = 0;
                        //입력한 업무를 테이블에 저장
                        string query2 = $"SELECT 소분류_id FROM 대분류,중분류,소분류 WHERE 대분류.대분류_id = 중분류.대분류_id AND 중분류.중분류_id = 소분류.중분류_id AND 대분류_name = '{big_category_combobox.Text}' AND 중분류_name = '{mid_category_combobox.Text}' AND 소분류_name = '{small_category_combobox.Text}'";
                        DBManager.GetDBManager().OpenConnection();
                        MySqlDataReader reader2 = DBManager.GetDBManager().SetQuery(query2).ExecuteReader();
                        while (reader2.Read())
                        {
                            small_category_index = Convert.ToInt32(reader2["소분류_id"]); //선택한 소분류의 index값을 가져옴
                        }
                        DBManager.GetDBManager().CloseConnection();
                        string query3;
                        if (regist_uesr_textbox.Text != "")
                        {
                            query3 = $"UPDATE 업무 SET 소분류_id = '{small_category_index}', 시작_time = '{start_time_picker.Text}', 종료_time = '{end_time_picker.Text}', 업무_content = '{task_content_textbox.Text}' WHERE 업무_id = '{index_textbox.Text}'";
                        }
                        else
                        {
                            query3 = $"INSERT INTO 업무(업무_content,시작_time,종료_time,소분류_id,업무담당자_id) VALUES ('{task_content_textbox.Text}','{start_time_picker.Text}','{end_time_picker.Text}',{small_category_index},'{id}');";
                        }
                        DBManager.GetDBManager().SetQuery(query3).ExecuteNonQuery();
                        loadDataGridView(); //업무 내역 갱신
                        MessageBox.Show("저장되었습니다.");
                        text_clear();
                    }
                    else
                    {
                        MessageBox.Show("이 업무는 업무시간이 중복되어 저장이 불가능합니다.");
                    }
                }
            }
        }
        public Boolean checkform()
        {
            if (small_category_combobox.Text == "")
            {
                return true;
            }
            if (task_content_textbox.Text == "")
            {
                return true;
            }
            if (start_time_picker.Text == "")
            {
                return true;
            }
            if (end_time_picker.Text == "")
            {
                return true;
            }
            return false;
        }
        private void delete_button_Click(object sender, EventArgs e)
        {
            //업무 내역 삭제 함수
            if (index_textbox.Text != "")
            {
                string query = $"DELETE FROM 업무 WHERE 업무_id = '{index_textbox.Text}';";
                DBManager.GetDBManager().SetQuery(query).ExecuteNonQuery();
                MessageBox.Show("삭제되었습니다");
            }
            else
            {
                MessageBox.Show("저장되어 있지 않은 값이라 삭제가 불가능합니다.");
            }
            text_clear();
            loadDataGridView(); //업무 내역 갱신

        }
        private void text_clear()
        {
            //텍스트 박스 초기화
            big_category_combobox.SelectedIndex = -1;
            mid_category_combobox.SelectedIndex = -1;
            small_category_combobox.SelectedIndex = -1;
            index_textbox.Text = "";
            regist_uesr_textbox.Text = "";
            start_time_picker.Value = DateTime.Now;
            end_time_picker.Value = DateTime.Now;
            task_content_textbox.Text = "";
        }
        private void reset_button_Click(object sender, EventArgs e)
        {
            //모든 텍스트를 빈칸으로 바꿈
            save_button.Enabled = true;
            text_clear();
            MessageBox.Show("필드값이 초기화 되었습니다.");
        }
        private void taskmaster_add_del_button_Click(object sender, EventArgs e)
        {
            //업무마스터 추가, 삭제할 수 있는 Form 생성 후 열기
            업무마스터_추가_삭제 configForm = new 업무마스터_추가_삭제();
            configForm.ShowDialog();
        }
        private void taskmaster_modify_button_Click(object sender, EventArgs e)
        {
            //업무마스터 수정할 수 있는 Form 생성 후 열기

            업무마스터_수정 taskmasterForm = new 업무마스터_수정();
            taskmasterForm.Form2Closed += Form2_Form2Closed; //업무 내역에서 카테고리 바뀐것을 바로 확인하기 위한 이벤트
            taskmasterForm.Show();
        }
        private void Form2_Form2Closed(object sender, EventArgs e)
        {
            settingBigCategoryCombobox(); // 데이터를 갱신
            loadDataGridView(); //업무 내역 갱신
        }
        public void settingBigCategoryCombobox()
        {
            //대분류 콤보박스 세팅 함수
            BigCategory.getInstance().setBigCategory(big_category_combobox);
        }
        private void big_category_combobox_Click(object sender, EventArgs e)
        {
            //대분류 콤보박스 클릭시
            settingBigCategoryCombobox();
        }
        private void big_category_combobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            //대분류 박스 선택시 해당 값에 해당하는 중분류 값들을 중분류 박스에 추가
            BigCategory.getInstance().setMidCategory(big_category_combobox.Text, mid_category_combobox);
            mid_category_combobox.Text = "";
            small_category_combobox.Text = "";
        }

        private void mid_category_combobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            //중분류 박스 선택시 해당 값에 해당하는 소분류 값들을 소분류 박스에 추가
            BigCategory.getInstance().setSmallCategory(big_category_combobox.Text, mid_category_combobox.Text, small_category_combobox);
            small_category_combobox.Text = "";
        }
        private void loadDataGridView()
        {
            //DataGridView에 업무의 모든 내용을 가져옴
            TaskDataGridView.getinstance().setDataGridView("SELECT 업무_id AS '인덱스번호', 대분류_name AS '대분류명', 중분류_name AS '중분류명', 소분류_name AS '소분류명', 시작_time AS '시작시간', 종료_time AS '종료시간', 이름, 업무_content AS '업무내용', 업무담당자_id  FROM 업무, 소분류, 중분류, 대분류,사원 WHERE 업무.업무담당자_id = 사원.사원번호 AND 업무.소분류_id = 소분류.소분류_id AND 소분류.중분류_id = 중분류.중분류_id AND 중분류.대분류_id = 대분류.대분류_id", dataGridView1);
        }
        private void filter_button_Click(object sender, EventArgs e)
        {
            //필터에 맞는 조건만 DataGridView로 반환하는 함수
            string query = $"SELECT 업무_id AS '인덱스번호', 대분류_name AS '대분류명', 중분류_name AS '중분류명', 소분류_name AS '소분류명', 시작_time AS '시작시간', 종료_time AS '종료시간', 이름, 업무_content AS '업무내용', 업무담당자_id FROM 업무, 소분류, 중분류, 대분류,사원 WHERE 업무.업무담당자_id = 사원.사원번호 AND 업무.소분류_id = 소분류.소분류_id AND 소분류.중분류_id = 중분류.중분류_id AND 중분류.대분류_id = 대분류.대분류_id";

            if (start_date_filter_textbox.Text != "")
            {
                query += $" AND 시작_time LIKE'%{start_date_filter_textbox.Text}%'";
            }
            if (end_date_filter_textbox.Text != "")
            {
                query += $" AND 종료_time LIKE'%{end_date_filter_textbox.Text}%'";
            }
            if (task_keword_filter_textbox.Text != "")
            {
                query += $" AND 소분류_name LIKE'%{task_keword_filter_textbox.Text}%'";
            }
            if (register_filter_textbox.Text != "")
            {
                query += $" AND 이름 LIKE'%{register_filter_textbox.Text}%'";
            }
            query += ";";
            TaskDataGridView.getinstance().setDataGridView(query, dataGridView1);
        }
        private void filter_cancel_button_Click(object sender, EventArgs e)
        {
            //필터 해제
            loadDataGridView();
            start_date_filter_textbox.Text = "";
            end_date_filter_textbox.Text = "";
            task_keword_filter_textbox.Text = "";
            register_filter_textbox.Text = "";
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //DataGridView 셀 클릭시 내용이 하단의 등록창에 띄어짐
            text_clear();
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];

                if (selectedRow.Cells[8].Value.ToString() == 사용자매니저.GetInstance().Get_사원번호() || 사용자매니저.GetInstance().Get_직급() == "사장") { delete_button.Enabled = true; save_button.Enabled = true; }
                else { delete_button.Enabled = false; save_button.Enabled = false; }

                string startdateString = selectedRow.Cells[4].Value.ToString();
                if (DateTime.TryParse(startdateString, out DateTime parsedDate))
                {
                    start_time_picker.Value = parsedDate;
                }
                string enddateString = selectedRow.Cells[5].Value.ToString();
                if (DateTime.TryParse(enddateString, out DateTime parsedDate2))
                {
                    end_time_picker.Value = parsedDate2;
                }
                big_category_combobox.Text = selectedRow.Cells[1].Value.ToString();
                mid_category_combobox.Text = selectedRow.Cells[2].Value.ToString();
                small_category_combobox.Text = selectedRow.Cells[3].Value.ToString();
                index_textbox.Text = selectedRow.Cells[0].Value.ToString();

                regist_uesr_textbox.Text = selectedRow.Cells[6].Value.ToString();
                //  if (String.IsNullOrEmpty(regist_uesr_textbox.Text)) regist_uesr_textbox.Text = 사용자매니저.GetInstance().Get_사원이름();

                task_content_textbox.Text = selectedRow.Cells[7].Value.ToString();
            }
        }
    }
}