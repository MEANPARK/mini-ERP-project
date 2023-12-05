using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TeamProject_test_v1
{

    
    internal class 등록관리매니저
    {
        private static 등록관리매니저 instance = new 등록관리매니저();

        public static 등록관리매니저 GetInstance() { return instance; }

        private string query;
        private 등록관리매니저()
        {

        }


        // 자신이 등록한 결재를 DataTable 클래스로 반환하는 메소드
        public DataTable 등록내역_불러오기()
        {
            DataTable result = new DataTable();
            try
            {
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                DBManager.GetDBManager().OpenConnection();
                adapter.SelectCommand = DBManager.GetDBManager().SetQuery(query).CreateCommand();
                adapter.Fill(result);
                DBManager.GetDBManager().CloseConnection();
                return result;
            } catch (Exception ex)
            {
                DBManager.GetDBManager().CloseConnection();
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        public void 범위설정(string 범위)
        {
            query = $"select 사원3.이름 as 기안자, 결재_id, 결재_title, 업무_Content, 결재_상세정보,결재_CreateData, 사원1.이름 as 1단계_결재자, 1단계결재자_승인여부, 1단계결재자_결재시간, 1단계결재자_Comment," +
                    $"사원2.이름 as 2단계_결재자 ,2단계결재자_승인여부, 2단계결재자_결재시간, 2단계결재자_Comment, 최종승인여부\n" +
                    $"from s5584720.결재 left outer join s5584720.사원 사원1 on 1단계결재자_id = 사원1.사원번호 " +
                    $"left outer join s5584720.사원 사원2 on 2단계결재자_id = 사원2.사원번호 \n" +
                    $"left outer join s5584720.사원 사원3 on s5584720.결재.사원번호 = 사원3.사원번호\n" +
                    $"where s5584720.결재.사원번호 = '{사용자매니저.GetInstance().Get_사원번호()}' ";
            if (범위=="결재 중")
            {
                query += "and (최종승인여부 = 'ing' or 최종승인여부 is null)";
            } else if (범위 == "결재 완료")
            {
                query += "and (최종승인여부 = 'false' or 최종승인여부 ='true')";
            }
            query += ";";
        }

        // 결재_id 존재 == return true
        // 결재_id 존재X == return false
        public Boolean 결재_존재(string 결재_id)
        {
            DataTable result = new DataTable();
            if (결재_id == null) return false;
            try
            {
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                DBManager.GetDBManager().OpenConnection();
                adapter.SelectCommand = DBManager.GetDBManager().SetQuery(
                    $"select exists (select * from s5584720.결재 where 결재_id = {결재_id} limit 1) as exist;"
                    ).CreateCommand();
                adapter.Fill(result);
                DBManager.GetDBManager().CloseConnection();
                if (result.Rows[0]["exist"].ToString() != "1")
                    return false;
                else return true;

            }
            catch (Exception ex)
            {
                MessageBox.Show("결재 존재 오류 in 등록관리매니저\n" + ex.Message);
                return false;
            }
        }

        // unique한 결재 id 생성
        public string 결재_id_생성()
        {

            Random randomObj = new Random();
            while (true)
            {
                uint id = uint.Parse(DateTime.Now.ToString("yyyyMMdd")) + ((uint)randomObj.Next(0, 99999999));
                string new_결재_id = Convert.ToString(id);
                if (!결재_존재(new_결재_id)) return new_결재_id;
            }
        }

        // 직급에 따라 로직이 달라져야 한다.
        // 부서장일 때 1단계결재자 = 자기자신, 1단계승인여부 = true
        // 사장일 때 1단계결재자 = 자기자신, 1단계승인여부 = true, 2단계결재자 = 자기자신, 2단계승인여부 = true, 최종승인여부 = true
        public Boolean 결재_등록(string 제목, string 관련_업무,string 업무_id, string 결재자_1_사원번호, string 결재자_2_사원번호, string 결재_상세정보, string 단계1_결재자_코맨트)
        {
            string new_결재_id = 결재_id_생성();
            string 생성일 = DateTime.Now.ToString("yyyy/MM/dd HH:mm");

            string 단계1승인여부 = "null";
            string 단계2승인여부 = "null";
            string 최종승인여부 = "null";
            string 승인1결재일 = "null";
            string 승인2결재일 = "null";

            string 직급 = 사용자매니저.GetInstance().Get_직급();

            if (직급 == "부서장" || 직급 == "사장")
            {
                결재자_1_사원번호 = 사용자매니저.GetInstance().Get_사원번호();
                단계1승인여부 = "'true'";
                승인1결재일 = $"'{생성일}'";
                최종승인여부 = "'ing'";
                if (직급 == "사장")
                {
                    결재자_2_사원번호 = 결재자_1_사원번호;
                    단계2승인여부 = "'true'";
                    최종승인여부 = "'true'";
                    승인2결재일 = $"'{생성일}'";
                    최종승인여부 = "'true'";
                }
            }

            try
            {
                DBManager.GetDBManager().SetQuery(
                    $"insert into s5584720.결재 (결재_id, 업무_id, 사원번호, 결재_title, 업무_Content, 결재_상세정보, 결재_CreateData, 1단계결재자_id," +
                    $"1단계결재자_승인여부, 1단계결재자_결재시간, 1단계결재자_Comment,2단계결재자_id, 2단계결재자_승인여부, 2단계결재자_결재시간, 2단계결재자_Comment, 최종승인여부) " +
                    $"values ({new_결재_id}, {업무_id}, '{사용자매니저.GetInstance().Get_사원번호()}', \"{제목}\", \"{관련_업무}\",\"{결재_상세정보}\",\"{생성일}\",\"{결재자_1_사원번호}\"," +
                    $"{단계1승인여부}, {승인1결재일}, '{단계1_결재자_코맨트}' ,\"{결재자_2_사원번호}\",{단계2승인여부}, {승인2결재일}, null, {최종승인여부});"
                    ).ExecuteNonQuery();
                return true;
            } catch(Exception ex) 
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

       
        public Boolean 결재_삭제(string 결재_id)
        {
            try
            {
                DBManager.GetDBManager().SetQuery(
                    $"delete from s5584720.결재 where 결재_id = {결재_id} and 사원번호 = '{사용자매니저.GetInstance().Get_사원번호()}' and (최종승인여부 is null or 최종승인여부 = 'false');").ExecuteNonQuery();
                
                if (결재_존재(결재_id)) return false;
                return true;
            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }


        public void 박스설정(DataGridViewRow 행, TextBox 결재_title, ComboBox 관련업무, ComboBox 결재자1, ComboBox 결재자2, TextBox 결재_상세정보, TextBox 결재자1_코맨트, TextBox 결재자2_코맨트)
        {
            if (행 == null) return;
            try
            {
                결재_title.Text = 행.Cells["결재_title"].Value.ToString();
                관련업무.Text = 행.Cells["업무_Content"].Value.ToString();
                결재_상세정보.Text = 행.Cells["결재_상세정보"].Value.ToString();
                결재자1.Text = 행.Cells["1단계_결재자"].Value.ToString();
                결재자2.Text = 행.Cells["2단계_결재자"].Value.ToString();
                결재_상세정보.Text = 행.Cells["결재_상세정보"].Value.ToString(); 
                결재자1_코맨트.Text = 행.Cells["1단계결재자_Comment"].Value.ToString();
                결재자2_코맨트.Text = 행.Cells["2단계결재자_Comment"].Value.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }

        
    }
}
