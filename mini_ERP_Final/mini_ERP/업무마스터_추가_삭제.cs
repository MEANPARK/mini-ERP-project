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

namespace TeamProject_test_v1
{
    public partial class 업무마스터_추가_삭제 : Form
    {
        public 업무마스터_추가_삭제()
        {
            InitializeComponent();
        }

        private void save_button_Click(object sender, EventArgs e) //대분류_중분류_소분류 저장
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
                    //대분류 값을 추가하는 경우
                    if (big_textbox.Text != "")
                    {
                        //대분류값을 테이블에 저장
                        string query1 = $"INSERT IGNORE INTO 대분류 (대분류_name) VALUES ('{big_textbox.Text}');";
                        DBManager.GetDBManager().SetQuery(query1).ExecuteNonQuery();
                    }
                    //중분류 값을 추가하는 경우
                    if (mid_textbox.Text != "")
                    {
                        string query2 = $"INSERT IGNORE INTO 중분류 (중분류_name, 대분류_id) VALUES ('{mid_textbox.Text}', (SELECT 대분류_id FROM 대분류 WHERE 대분류_name = '{big_textbox.Text}'));";
                        DBManager.GetDBManager().SetQuery(query2).ExecuteNonQuery();
                    }

                    if (small_textbox.Text != "") //대분류-중분류-소분류 값을 추가하는 경우
                    {

                        string query3 = $"INSERT INTO 소분류 (소분류_name, 중분류_id) VALUES ('{small_textbox.Text}', (SELECT 중분류_id FROM 중분류 WHERE 중분류_name = '{mid_textbox.Text}' AND 대분류_id = (SELECT 대분류_id FROM 대분류 WHERE 대분류_name = '{big_textbox.Text}')));";
                        DBManager.GetDBManager().SetQuery(query3).ExecuteNonQuery();

                    }
                    MessageBox.Show("추가 완료");
                    clear();
                }
            }
        }

        private void delete_button_Click(object sender, EventArgs e) //대분류_중분류_소분류 삭제
        {
            if (formcheck() == true)
            {
                //작성 양식에 오류가 있을시 실행안함
            }
            else
            {
                if (taskcheck() == true)
                {
                    MessageBox.Show("해당 값을 가진 업무 내역이 있어 삭제가 불가능합니다.");
                }
                else if (checkduplicate() == false)
                {
                    MessageBox.Show("해당 값이 테이블에 없어 삭제가 불가능합니다.");
                }
                else
                {
                    //대분류, 중분류, 소분류 값들을 테이블에 저장
                    string query1 = $"DELETE FROM 소분류 WHERE 소분류_name = '{small_textbox.Text}' AND 중분류_id IN (SELECT 중분류_id FROM 중분류 WHERE 중분류_name = '{mid_textbox.Text}' AND 대분류_id IN (SELECT 대분류_id FROM 대분류 WHERE 대분류_name = '{big_textbox.Text}'));";
                    string query2 = $"DELETE FROM 중분류 WHERE 중분류_name = '{mid_textbox.Text}' AND 중분류_id NOT IN (SELECT 중분류_id FROM 소분류) AND  대분류_id IN (SELECT 대분류_id FROM 대분류 WHERE 대분류_name = '{big_textbox.Text}');";
                    string query3 = $"DELETE FROM 대분류 WHERE 대분류_name = '{big_textbox.Text}' AND 대분류_id NOT IN (SELECT 대분류_id FROM 중분류);";
                    int rowsDeleted1 = DBManager.GetDBManager().SetQuery(query1).ExecuteNonQuery();
                    int rowsDeleted2 = DBManager.GetDBManager().SetQuery(query2).ExecuteNonQuery();
                    int rowsDeleted3 = DBManager.GetDBManager().SetQuery(query3).ExecuteNonQuery();
                    if (rowsDeleted1 == 0 && rowsDeleted2 == 0 && rowsDeleted3 == 0) //삭제된 값이 없다면
                    {
                        MessageBox.Show("삭제 할 수 없는 값입니다.");
                    }
                    else
                    {
                        MessageBox.Show("삭제 완료");
                        clear();
                    }
                }
            }
        }
        public Boolean checkduplicate() //중복 값 체크
        {
            string query = "";
            if (mid_textbox.Text == "")
            {
                query = $"SELECT * FROM 대분류 WHERE 대분류_name = '{big_textbox.Text}'";

            }
            else if (small_textbox.Text == "")
            {
                query = $"SELECT * FROM 대분류,중분류 WHERE 대분류.대분류_id = 중분류.대분류_id AND 대분류_name = '{big_textbox.Text}' AND 중분류_name = '{mid_textbox.Text}'";
            }
            //테이블에 해당 값이 들어있는지 검사하는 함수
            else
            {
                query = $"SELECT * FROM 대분류,중분류,소분류 WHERE 대분류.대분류_id = 중분류.대분류_id AND 중분류.중분류_id = 소분류.중분류_id AND 대분류_name = '{big_textbox.Text}' AND 중분류_name = '{mid_textbox.Text}' AND 소분류_name = '{small_textbox.Text}'";
            }
            Boolean flag = false;
            DBManager.GetDBManager().OpenConnection();
            MySqlDataReader reader = DBManager.GetDBManager().SetQuery(query).ExecuteReader();
            if (reader.Read())
            {
                flag = true;
            }
            DBManager.GetDBManager().CloseConnection();
            return flag;
        }
        public Boolean taskcheck() //해당 값이 업무 내역으로 사용중인지 체크
        {
            //엄무테이블에 해당 소분류_index를 가진 값이 있는지 확인 -> 해당 소분류_index 업무가 있다면 삭제 진행 X
            Boolean flag = false;
            string query = $"SELECT * FROM 업무 WHERE 소분류_id = (SELECT 소분류_id FROM 대분류,중분류,소분류 WHERE 대분류.대분류_id = 중분류.대분류_id AND 중분류.중분류_id = 소분류.중분류_id AND 대분류_name = '{big_textbox.Text}' AND 중분류_name = '{mid_textbox.Text}' AND 소분류_name = '{small_textbox.Text}')";
            DBManager.GetDBManager().OpenConnection();
            MySqlDataReader reader = DBManager.GetDBManager().SetQuery(query).ExecuteReader();
            if (reader.Read())
            {
                flag = true;
            }
            DBManager.GetDBManager().CloseConnection();
            return flag;
        }
        public Boolean formcheck() //해당 값이 업무 내역으로 사용중인지 체크
        {
            if (big_textbox.Text == "" && mid_textbox.Text == "" && small_textbox.Text == "")
            {
                MessageBox.Show("작성해주십시오");
                return true;
            }
            if (big_textbox.Text != "")
            {
                if (String.IsNullOrEmpty(mid_textbox.Text) && !String.IsNullOrEmpty(small_textbox.Text)) //대분류와 소분류만 있는 경우 예외처리
                {
                    MessageBox.Show("중분류를 입력해주세요");
                    return true;
                }
            }
            //중분류 값을 추가하는 경우
            if (mid_textbox.Text != "")
            {
                if (String.IsNullOrEmpty(big_textbox.Text))
                {
                    MessageBox.Show("대분류를 입력해주세요");
                    return true;
                }
            }

            if (small_textbox.Text != "") //대분류-중분류-소분류 값을 추가하는 경우
            {
                if (String.IsNullOrEmpty(big_textbox.Text) && String.IsNullOrEmpty(mid_textbox.Text))
                {
                    MessageBox.Show("대분류와 중분류를 입력해주세요");
                    return true;
                }
            }
            return false;
        }
        public void clear()
        {
            big_textbox.Text = "";
            mid_textbox.Text = "";
            small_textbox.Text = "";
        }
    }
}
