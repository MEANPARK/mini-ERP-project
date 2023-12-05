using MySql.Data.MySqlClient;
using System;
using System.Collections;
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
    public partial class 업무마스터_수정 : Form
    {
        public delegate void FormClosedEventHandler(object sender, EventArgs e);
        public event FormClosedEventHandler Form2Closed;

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);
            Form2Closed?.Invoke(this, EventArgs.Empty);
        }
        public 업무마스터_수정()
        {
            InitializeComponent();
            settingBigCategoryCombobox();
        }
        public void settingBigCategoryCombobox()
        {
            //대분류 콤보박스 세팅 함수
            BigCategory.getInstance().setBigCategory(before_big_combo);
        }
        private void big_category_combobox_Click(object sender, EventArgs e)
        {
            //대분류 콤보박스 클릭시
            settingBigCategoryCombobox();
        }
        private void big_category_combobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            //대분류 박스 선택시 해당 값에 해당하는 중분류 값들을 중분류 박스에 추가
            BigCategory.getInstance().setMidCategory(before_big_combo.Text, before_mid_combo);
            before_mid_combo.Text = "";
            before_small_combo.Text = "";
        }

        private void mid_category_combobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            //중분류 박스 선택시 해당 값에 해당하는 소분류 값들을 소분류 박스에 추가
            BigCategory.getInstance().setSmallCategory(before_big_combo.Text, before_mid_combo.Text, before_small_combo);
            before_small_combo.Text = "";
        }
        public Boolean checkduplicate() //중복 검사
        {
            string query = "";
            if (after_mid_textbox.Text == "")
            {
                query = $"SELECT COUNT(*) FROM 대분류 WHERE 대분류.대분류_name = '{after_big_textbox.Text}';";

            }
            else if (after_small_textbox.Text == "")
            {
                query = $"SELECT COUNT(*) FROM 중분류, 대분류 WHERE 대분류.대분류_id = 중분류.대분류_id  AND 대분류_name = '{after_big_textbox.Text}' AND 중분류_name = '{after_mid_textbox.Text}';";
            }
            //테이블에 해당 값이 들어있는지 검사하는 함수
            else
            {
                query = $"SELECT COUNT(*) FROM 소분류, 중분류, 대분류 WHERE 대분류.대분류_id = 중분류.대분류_id AND 중분류.중분류_id = 소분류.중분류_id AND 대분류_name = '{after_big_textbox.Text}' AND 중분류_name = '{after_mid_textbox.Text}' AND 소분류_name = '{after_small_textbox.Text}';";
            }
            Boolean flag = false;
            DBManager.GetDBManager().OpenConnection();
            MySqlDataReader reader = DBManager.GetDBManager().SetQuery(query).ExecuteReader();
            if (reader.Read())
            {
                int count = reader.GetInt32(0);
                if (count == 0)
                {
                    flag = false; // 중복이 없음
                }
                else
                {
                    flag = true; // 중복이 있음
                }
            }
            DBManager.GetDBManager().CloseConnection();
            return flag;
        }
        private void modify_button_Click(object sender, EventArgs e) //수정 버튼 클릭시
        {
            if (formcheck() == true)
            {
                //작성 양식에 오류가 있을시 실행안함
            }
            else
            {
                if (checkduplicate() == true)
                {
                    MessageBox.Show("중복된 값이 있어 추가가 불가능합니다.");
                }
                else
                {
                    if (after_mid_textbox.Text == "") //대분류만 수정하는 경우
                    {
                        // 새로운 대분류를 추가-> 기존 대분류를 외래키로 하는 중분류를 찾아 새로운 대분류를 외래키 하도록 설정 -> 기존 대분류 삭제
                        int big_id = 0;
                        int new_id = 0;
                        //대분류 추가
                        string query1 = $"INSERT IGNORE INTO 대분류 (대분류_name) VALUES ('{after_big_textbox.Text}');";
                        DBManager.GetDBManager().SetQuery(query1).ExecuteNonQuery();
                        //중분류(대분류_id) 수정
                        string query2 = $"SELECT 대분류_id FROM 대분류 WHERE 대분류_name = '{before_big_combo.Text}'"; // 1)기존 대분류_id 가져오기
                        DBManager.GetDBManager().OpenConnection();
                        MySqlDataReader reader1 = DBManager.GetDBManager().SetQuery(query2).ExecuteReader();
                        while (reader1.Read())
                        {
                            big_id = Convert.ToInt32(reader1["대분류_id"]);
                        }
                        DBManager.GetDBManager().CloseConnection();
                        string query3 = $"SELECT 대분류_id FROM 대분류 WHERE 대분류_name = '{after_big_textbox.Text}'"; // 1)새로운 대분류_id 가져오기
                        DBManager.GetDBManager().OpenConnection();
                        MySqlDataReader reader2 = DBManager.GetDBManager().SetQuery(query3).ExecuteReader();
                        while (reader2.Read())
                        {
                            new_id = Convert.ToInt32(reader2["대분류_id"]);
                        }
                        DBManager.GetDBManager().CloseConnection();
                        string query4 = $"UPDATE 중분류 SET 대분류_id = {new_id} WHERE 대분류_id = {big_id};"; //해당 중분류_id로 가서 대분류_id를 업데이트 시키기
                        DBManager.GetDBManager().SetQuery(query4).ExecuteNonQuery();
                        //삭제하기
                        string query5 = $"DELETE FROM 대분류 WHERE 대분류_id = '{big_id}';"; //기존 대분류 삭제
                        DBManager.GetDBManager().SetQuery(query5).ExecuteNonQuery();
                    }
                    else if (after_small_textbox.Text == "") //대분류-중분류만 수정하는 경우
                    {
                        Boolean deletebig = false;
                        //새로운 대분류-중분류 추가 -> 기존에 대분류-중분류를 외래키로 사용하는 값을 찾아 새로운 대분류-중분류를 외래키로 설정 -> 기존의 대분류-중분류를 삭제
                        int mid_id = 0;
                        int new_id = 0;
                        //대분류-중분류 추가
                        string query1 = $"INSERT IGNORE INTO 대분류 (대분류_name) VALUES ('{after_big_textbox.Text}');";
                        DBManager.GetDBManager().SetQuery(query1).ExecuteNonQuery();
                        string query2 = $"INSERT IGNORE INTO 중분류 (중분류_name, 대분류_id) VALUES ('{after_mid_textbox.Text}', (SELECT 대분류_id FROM 대분류 WHERE 대분류_name = '{after_big_textbox.Text}'));";
                        DBManager.GetDBManager().SetQuery(query2).ExecuteNonQuery();
                        //대분류-중분류 수정
                        string query3 = $"SELECT 중분류.중분류_id FROM 대분류,중분류 WHERE 대분류.대분류_id = 중분류.대분류_id  AND 대분류_name = '{before_big_combo.Text}' AND 중분류_name = '{before_mid_combo.Text}'"; // 1)기존 중분류의 중분류_id 가져오기
                        DBManager.GetDBManager().OpenConnection();
                        MySqlDataReader reader1 = DBManager.GetDBManager().SetQuery(query3).ExecuteReader();
                        while (reader1.Read())
                        {
                            mid_id = Convert.ToInt32(reader1["중분류_id"]);
                        }
                        DBManager.GetDBManager().CloseConnection();
                        string query4 = $"SELECT 중분류.중분류_id FROM 대분류,중분류 WHERE 대분류.대분류_id = 중분류.대분류_id AND 대분류_name = '{after_big_textbox.Text}' AND 중분류_name = '{after_mid_textbox.Text}'"; // 1)새로운 중분류_id 가져오기
                        DBManager.GetDBManager().OpenConnection();
                        MySqlDataReader reader2 = DBManager.GetDBManager().SetQuery(query4).ExecuteReader();
                        while (reader2.Read())
                        {
                            new_id = Convert.ToInt32(reader2["중분류_id"]);
                        }
                        DBManager.GetDBManager().CloseConnection();
                        string query5 = $"UPDATE IGNORE 소분류 SET 중분류_id = {new_id} WHERE 중분류_id = {mid_id};"; //해당 소분류 기존 중분류_id로 가서 새로운 중분류_id로 업데이트 시키기
                        DBManager.GetDBManager().SetQuery(query5).ExecuteNonQuery();
                        //삭제하기
                        string query6 = $"DELETE FROM 중분류 WHERE 중분류_id = '{mid_id}';"; //기존 중분류 삭제
                        DBManager.GetDBManager().SetQuery(query6).ExecuteNonQuery();
                        DBManager.GetDBManager().CloseConnection();
                        string query7 = $"SELECT 중분류.대분류_id FROM 대분류,중분류 WHERE 대분류.대분류_id = 중분류.대분류_id AND 대분류_name = '{before_big_combo.Text}' GROUP BY 중분류.대분류_id"; // 1)중분류를 가지는 대분류가 있는지 판단
                        DBManager.GetDBManager().OpenConnection();
                        MySqlDataReader reader3 = DBManager.GetDBManager().SetQuery(query7).ExecuteReader();
                        if (reader3.Read())
                        {
                            deletebig = true;
                        }
                        DBManager.GetDBManager().CloseConnection();
                        if (deletebig == false)
                        {
                            string query8 = $"DELETE FROM 대분류 WHERE 대분류_name= '{before_big_combo.Text}';"; //중분류를 가지고 있지 않은 대분류라면 삭제
                            DBManager.GetDBManager().SetQuery(query8).ExecuteNonQuery();
                        }
                    }
                    else //대분류-중분류-소분류 수정
                    {
                        Boolean deletebig = false;
                        Boolean deletemid = false;
                        int small_id = 0;
                        int new_id = 0;
                        //대분류-중분류-소분류추가
                        string query1 = $"INSERT IGNORE INTO 대분류 (대분류_name) VALUES ('{after_big_textbox.Text}');";
                        DBManager.GetDBManager().SetQuery(query1).ExecuteNonQuery();
                        string query2 = $"INSERT IGNORE INTO 중분류 (중분류_name, 대분류_id) VALUES ('{after_mid_textbox.Text}', (SELECT 대분류_id FROM 대분류 WHERE 대분류_name = '{after_big_textbox.Text}'));";
                        DBManager.GetDBManager().SetQuery(query2).ExecuteNonQuery();
                        string query3 = $"INSERT INTO 소분류 (소분류_name, 중분류_id) VALUES ('{after_small_textbox.Text}', (SELECT 중분류_id FROM 중분류 WHERE 중분류_name = '{after_mid_textbox.Text}' AND 대분류_id = (SELECT 대분류_id FROM 대분류 WHERE 대분류_name = '{after_big_textbox.Text}')));";
                        DBManager.GetDBManager().SetQuery(query3).ExecuteNonQuery();

                        //대분류-중분류-소분류 수정

                        string query4 = $"SELECT 소분류.소분류_id FROM 대분류,중분류,소분류 WHERE 대분류.대분류_id = 중분류.대분류_id AND 중분류.중분류_id = 소분류.중분류_id AND 대분류_name = '{before_big_combo.Text}' AND 중분류_name = '{before_mid_combo.Text}' AND 소분류_name = '{before_small_combo.Text}'"; // 1)기존 소분류의 대분류_id 가져오기
                        DBManager.GetDBManager().OpenConnection();
                        MySqlDataReader reader1 = DBManager.GetDBManager().SetQuery(query4).ExecuteReader();
                        while (reader1.Read())
                        {
                            small_id = Convert.ToInt32(reader1["소분류_id"]);
                        }
                        DBManager.GetDBManager().CloseConnection();
                        string query5 = $"SELECT 소분류.소분류_id FROM 대분류,중분류,소분류 WHERE 대분류.대분류_id = 중분류.대분류_id AND 중분류.중분류_id = 소분류.중분류_id AND 대분류_name = '{after_big_textbox.Text}' AND 중분류_name = '{after_mid_textbox.Text}' AND 소분류_name = '{after_small_textbox.Text}'"; // 1)새로운 대분류_id 가져오기
                        DBManager.GetDBManager().OpenConnection();
                        MySqlDataReader reader2 = DBManager.GetDBManager().SetQuery(query5).ExecuteReader();
                        while (reader2.Read())
                        {
                            new_id = Convert.ToInt32(reader2["소분류_id"]);
                        }
                        DBManager.GetDBManager().CloseConnection();
                        string query6 = $"UPDATE 업무 SET 소분류_id = '{new_id}' WHERE 소분류_id = {small_id};"; //해당 업무 소분류_id로 가서 새로운 소분류_id를 업데이트 시키기
                        DBManager.GetDBManager().SetQuery(query6).ExecuteNonQuery();
                        //삭제하기
                        string query7 = $"DELETE FROM 소분류 WHERE 소분류_id = {small_id}"; //기존 소분류 id 삭제
                        DBManager.GetDBManager().SetQuery(query7).ExecuteNonQuery();
                        DBManager.GetDBManager().CloseConnection();
                        string query8 = $"SELECT 소분류.중분류_id FROM 소분류,중분류,대분류 WHERE 대분류.대분류_id = 중분류.대분류_id AND 중분류.중분류_id = 소분류.중분류_id AND 대분류_name = '{before_big_combo.Text}' AND 중분류_name = '{before_mid_combo.Text}' GROUP BY 소분류.중분류_id"; // 1)소분류를 가지는 중분류가 있는지 판단
                        DBManager.GetDBManager().OpenConnection();
                        MySqlDataReader reader3 = DBManager.GetDBManager().SetQuery(query8).ExecuteReader();
                        if (reader3.Read())
                        {
                            deletemid = true;
                        }
                        DBManager.GetDBManager().CloseConnection();
                        if (deletemid == false)
                        {
                            string query9 = $"DELETE 중분류 FROM 중분류,대분류 WHERE 대분류.대분류_id = 중분류.대분류_id AND 대분류_name = '{before_big_combo.Text}' AND 중분류_name= '{before_mid_combo.Text}' ;"; //중분류를 가지고 있지 않은 대분류라면 삭제
                            DBManager.GetDBManager().SetQuery(query9).ExecuteNonQuery();
                        }
                        DBManager.GetDBManager().CloseConnection();
                        string query10 = $"SELECT 중분류.대분류_id FROM 대분류,중분류 WHERE 대분류.대분류_id = 중분류.대분류_id AND 대분류_name = '{before_big_combo.Text}' GROUP BY 중분류.대분류_id"; // 1)중분류를 가지는 대분류가 있는지 판단
                        DBManager.GetDBManager().OpenConnection();
                        MySqlDataReader reader4 = DBManager.GetDBManager().SetQuery(query10).ExecuteReader();
                        if (reader4.Read())
                        {
                            deletebig = true;
                        }
                        DBManager.GetDBManager().CloseConnection();
                        if (deletebig == false)
                        {
                            string query11 = $"DELETE FROM 대분류 WHERE 대분류_name= '{before_big_combo.Text}';"; //중분류를 가지고 있지 않은 대분류라면 삭제
                            DBManager.GetDBManager().SetQuery(query11).ExecuteNonQuery();
                        }

                    }
                    MessageBox.Show("수정을 완료하였습니다");
                    clear();
                }
            }
        }

        private void 업무마스터_수정_FormClosing(object sender, FormClosingEventArgs e)
        {

        }
        public void clear()
        {
            after_big_textbox.Text = "";
            after_mid_textbox.Text = "";
            after_small_textbox.Text = "";
            before_big_combo.Items.Clear();
            before_mid_combo.Items.Clear();
            before_small_combo.Items.Clear();
        }
        public Boolean formcheck()
        {
            if (before_big_combo.Text == "")
            {
                MessageBox.Show("대분류값이 선택되어 있지 않습니다.");
                return true;
            }
            if (before_big_combo.Text != "" && before_mid_combo.Text == "") //대분류값만 설정되어 있을시 수정 후 대분류값 이외에 수정후 중분류, 소분류 값이 적혀 있을 시 예외 처리
            {
                if (after_big_textbox.Text == "")
                {
                    MessageBox.Show("수정 후의 대분류 값을 입력해주십시오.");
                    return true;
                }
                if (after_mid_textbox.Text != "")
                {
                    MessageBox.Show("수정 후의 중분류 값을 지워주십시오.");
                    return true;
                }
                if (after_small_textbox.Text != "")
                {
                    MessageBox.Show("수정 후의 소분류 값을 지워주십시오.");
                    return true;
                }
            }
            if (before_big_combo.Text != "" && before_mid_combo.Text != "" && before_small_combo.Text == "")
            {
                if (after_big_textbox.Text == "")
                {
                    MessageBox.Show("수정 후의 대분류 값을 입력해주십시오.");
                    return true;
                }
                if (after_mid_textbox.Text == "")
                {
                    MessageBox.Show("수정 후의 중분류 값을 입력해주십시오.");
                    return true;
                }
                if (after_small_textbox.Text != "")
                {
                    MessageBox.Show("수정 후의 소분류 값을 지워주십시오.");
                    return true;
                }
            }
            if (before_big_combo.Text != "" && before_mid_combo.Text != "" && before_small_combo.Text != "")
            {
                if (after_big_textbox.Text == "")
                {
                    MessageBox.Show("수정 후의 대분류 값을 입력해주십시오.");
                    return true;
                }
                if (after_mid_textbox.Text == "")
                {
                    MessageBox.Show("수정 후의 중분류 값을 입력해주십시오.");
                    return true;
                }
                if (after_small_textbox.Text == "")
                {
                    MessageBox.Show("수정 후의 소분류 값을 입력해주십시오.");
                    return true;
                }
            }
            return false;
        }
    }
}
