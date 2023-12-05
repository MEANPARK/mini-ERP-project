using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace TeamProject_test_v1
{
    internal class 승인반려매니저
    {
        private static 승인반려매니저 instance = new 승인반려매니저();
        private string query;
        private string 직급;
        private string 리스트_상태;

        // 결재를 등록한 사원번호
        private string 등록자_사원번호;
        public static 승인반려매니저 GetInstance() { return instance; }
        private 승인반려매니저() 
        {

        }

        // 직급 설정
        // select 쿼리를 직급에 맞게 설정함.
        // where n단계결재자_id = 자기사원번호 까지 쿼리문을 작성함.
        // 범위 조건에 따라 그 다음 where절을 작성하면 됨.
        private void 쿼리초기설정()
        {
            직급 = 사용자매니저.GetInstance().Get_직급();
            query = $"select 사원1.이름 as 기안자, s5584720.결재.사원번호 as 등록자_사원번호, 결재_id, 결재_title, 업무_Content,결재_상세정보 ,결재_CreateData," +
                    $"사원2.이름 as 1단계_결재자, 1단계결재자_승인여부,1단계결재자_결재시간, 1단계결재자_Comment, " +
                    $"사원3.이름 as 2단계_결재자 ,2단계결재자_승인여부,2단계결재자_결재시간, 2단계결재자_Comment, 최종승인여부\n" +
                    $"from s5584720.결재 " +
                    $"left outer join s5584720.사원 사원1 on s5584720.결재.사원번호 = 사원1.사원번호 " +
                    $"left outer join s5584720.사원 사원2 on 1단계결재자_id = 사원2.사원번호 \n" +
                    $"left outer join s5584720.사원 사원3 on 2단계결재자_id = 사원3.사원번호\n";
            if (직급 == "부서장")
            {
                query += $"where 1단계결재자_id = '{사용자매니저.GetInstance().Get_사원번호()}' ";
            }
            else if(직급 == "사장")
            {
                query += $"where 2단계결재자_id = '{사용자매니저.GetInstance().Get_사원번호()}' ";
            }
        }

        private void 쿼리범위설정(string 범위)
        {
            if(범위 == "전체")
            {
                query += ";";
            }else if(범위 == "결재 대기")
            {
                if(직급 == "부서장")
                {
                    query += "and (최종승인여부 is null or (1단계결재자_승인여부 = 'true' and 2단계결재자_승인여부 = 'false'));";
                }else if(직급 == "사장")
                {
                    query += "and (1단계결재자_승인여부 = 'true' and 2단계결재자_승인여부 is null and 최종승인여부 = 'ing');";
                }
            }else if(범위 == "결재 중")
            {
                if(직급 == "부서장")
                {
                    query += "and (1단계결재자_승인여부 = 'true' and 최종승인여부 = 'ing' and 2단계결재자_승인여부 is null);";
                }else if(직급 == "사장")
                {
                    query += "and ((2단계결재자_승인여부 = 'false' and 1단계결재자_승인여부 = 'true' and 최종승인여부 = 'ing' )or 최종승인여부 is null);";
                }
            }else if(범위 == "결재 완료")
            {
                query += "and (최종승인여부 ='true' or 최종승인여부 = 'false');";
            }
        }

        public void Set_리스트_상태(string 리스트_상태)
        {
            this.리스트_상태 = 리스트_상태;
        }

        public string Get_리스트_상태()
        {
            return 리스트_상태;
        }



        public DataTable 결재해야할_내역_불러오기(string 범위)
        {
            // 직급이 일반사원이면 리턴 null
            if (직급 == "일반사원") return null;


            DataTable result = new DataTable();
            쿼리초기설정();
            쿼리범위설정(범위);
            try
            {
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                DBManager.GetDBManager().OpenConnection();
                adapter.SelectCommand = DBManager.GetDBManager().SetQuery(query).CreateCommand();
                adapter.Fill(result);
                DBManager.GetDBManager().CloseConnection();
                return result;
            }
            catch (Exception ex)
            {
                DBManager.GetDBManager().CloseConnection();
                MessageBox.Show(ex.Message);
                return null;
            }
        }


        // 등록자_사원번호 설정
        public void 텍스트박스설정(DataGridViewRow 행,TextBox 결재_title, TextBox 관련업무, 
            TextBox 결재자1, TextBox 결재자2, TextBox 결재_상세정보, TextBox 승인_반려_1단계_결재자_코맨트_텍스트박스,
            TextBox 승인_반려_2단계_결재자_코맨트_텍스트박스)
        {
            if (행 == null) return;

            try
            {
                결재_title.Text = 행.Cells["결재_title"].Value.ToString();
                관련업무.Text = 행.Cells["업무_Content"].Value.ToString();
                결재_상세정보.Text = 행.Cells["결재_상세정보"].Value.ToString();
                승인_반려_1단계_결재자_코맨트_텍스트박스.Text = 행.Cells["1단계결재자_Comment"].Value.ToString();
                승인_반려_2단계_결재자_코맨트_텍스트박스.Text = 행.Cells["2단계결재자_Comment"].Value.ToString();

                등록자_사원번호 = 행.Cells["등록자_사원번호"].Value.ToString();
            }
            catch (Exception ex) {
                // System.Windows.MessageBox.Show(ex.Message);
                return;
            }


            if (직급 == "부서장")
            {
                결재자1.Text = 사용자매니저.GetInstance().Get_사원이름().ToString();
                결재자2.Text = 행.Cells["2단계_결재자"].Value.ToString();
            }else if(직급 == "사장")
            {
                결재자1.Text = 행.Cells["1단계_결재자"].Value.ToString();
                결재자2.Text = 사용자매니저.GetInstance().Get_사원이름().ToString();
            }
        }

        // 사장과 부서장에 따라 update문이 달라짐
        public Boolean 결재_승인(string 결재_id, string 승인사유)
        {
            string 결재일 = DateTime.Now.ToString("yyyy/MM/dd HH:mm");
            try
            {
                if(직급 == "부서장")
                {
                    query = $"update s5584720.결재 set 1단계결재자_승인여부 = 'true', 1단계결재자_결재시간 = '{결재일}',1단계결재자_Comment = '{승인사유}', 2단계결재자_승인여부 = null, 최종승인여부 = 'ing'\n" +
                        $"where 1단계결재자_id = '{사용자매니저.GetInstance().Get_사원번호()}' and 결재_id = {결재_id} and 사원번호 = '{등록자_사원번호}';";
                }else if(직급 == "사장")
                {
                    query = $"update s5584720.결재 " +
                        $"set 2단계결재자_승인여부 = 'true', 2단계결재자_결재시간 = '{결재일}', 2단계결재자_Comment = '{승인사유}',최종승인여부 = 'true'\n" +
                           $"where 2단계결재자_id = '{사용자매니저.GetInstance().Get_사원번호()}' and 결재_id = {결재_id} and 사원번호 = '{등록자_사원번호}';";
                    checkAA(결재_id);
                }
                DBManager.GetDBManager().SetQuery(query).ExecuteNonQuery();
                return true;
            } catch (Exception ex)
            {
                MessageBox.Show("결재승인 예외 in 승인반려매니저\n"+ex.Message);
                return false;
            }
        }

        public Boolean 결재_반려(string 결재_id, string 반려사유)
        {
            string 반려일 = DateTime.Now.ToString("yyyy/MM/dd HH:mm");
                try
                {
                    if (직급 == "부서장")
                    {
                        query = $"update s5584720.결재 set 1단계결재자_승인여부 = 'false', 최종승인여부 = 'false', 1단계결재자_결재시간 = '{반려일}', 1단계결재자_Comment = '{반려사유}'\n" +
                            $"where 1단계결재자_id = '{사용자매니저.GetInstance().Get_사원번호()}' and 결재_id = {결재_id} and 사원번호 = '{등록자_사원번호}';";
                    }
                    else if (직급 == "사장")
                    {
                        query = $"update s5584720.결재 " +
                            $"set 2단계결재자_승인여부 = 'false', 2단계결재자_결재시간 = '{반려일}', 2단계결재자_Comment = '{반려사유}'\n" +
                               $"where 2단계결재자_id = '{사용자매니저.GetInstance().Get_사원번호()}' and 결재_id = {결재_id} and 사원번호 = '{등록자_사원번호}';";
                    }
                    DBManager.GetDBManager().SetQuery(query).ExecuteNonQuery();
                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("결재승인 예외 in 승인반려매니저\n" + ex.Message);
                    return false;
                }
        }

        private void checkAA(string 결재_id)
        {
            string query = $"SELECT 업무_id FROM s5584720.결재 where 결재_id = {결재_id};";
            string check = Additional_Allowance.getInstance().sendQuery(query);
            if (check == "999") {
                query = $"SELECT 사원번호 FROM s5584720.결재 where 결재_id = {결재_id};";
                string id = Additional_Allowance.getInstance().sendQuery(query);

                query = $"SELECT 결재_상세정보 FROM s5584720.결재 where 결재_id = {결재_id};";
                string result = Additional_Allowance.getInstance().sendQuery(query);

                string[] word = result.Split(' ');
                query = $"UPDATE s5584720.출근부 SET 결재여부 = 1 WHERE 출근시간 LIKE '{word[2]}%' AND 사원번호 = {id};";
                DBManager.GetDBManager().SetQuery(query).ExecuteNonQuery();
            }
        }
    }
}