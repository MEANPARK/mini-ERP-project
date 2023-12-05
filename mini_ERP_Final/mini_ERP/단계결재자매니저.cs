using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MySql.Data.MySqlClient;

namespace TeamProject_test_v1
{
    internal class 단계결재자매니저
    {
        private static 단계결재자매니저 instance = new 단계결재자매니저();
        public static 단계결재자매니저 GetInstance() { return instance; }
        private 단계결재자매니저() { }


        // 결재자_단계 == 1 ( 부서장 ) return Dictionary< '1단계 결재자 사원번호', '1단계 결재자 이름' >
        // 결재자_단계 == 2 ( 사장 ) return Dictionary< '2단계 결재자 사원번호', '2단계 결재자 이름' >

        // 결재자_단계 == 1 query 
        // select 사원번호, 이름 from s5584720.사원 left outer join s5584720.부서 on s5584720.사원.부서번호 = s5584720.부서.부서코드
        // where 직급 = '부서장' and 부서코드 = '<로그인 된 사용자의 부서코드>';

        // 결재자_단계 == 2 query 
        // select 사원번호, 이름 from s5584720.사원 where 직급 = '사장';
        public Dictionary<string, string> Get_결재자리스트(int 결재자_단계)
        {
            Dictionary<string, string> 결재자리스트 = new Dictionary<string, string>();
            // 결재자가 1단계 결재자일 때 즉 부서장일 때
            if (결재자_단계 == 1)
            {
                try
                {

                    DBManager.GetDBManager().OpenConnection();
                    MySqlDataReader rdr = DBManager.GetDBManager().SetQuery(
                        $"select 사원번호, 이름 \nfrom s5584720.사원 left outer join s5584720.부서 \non s5584720.사원.부서번호 = s5584720.부서.부서코드\n" +
                        $"where 직급 = \"부서장\" and 부서코드 = \"{사용자매니저.GetInstance().Get_부서코드()}\";"
                        ).CreateCommand().ExecuteReader();
                    while (rdr.Read())
                    {
                        결재자리스트.Add(rdr["사원번호"].ToString(), rdr["이름"].ToString());
                        
                    }
                    DBManager.GetDBManager().CloseConnection();
                }
                catch (Exception ex)
                {
                    DBManager.GetDBManager().CloseConnection();
                    MessageBox.Show("결재자 기능 : Get_결재자리스트 : 결재자 1단계\n" + ex.Message);
                }
            }
            //결재자 단계가 2단계일 때 즉 사장일 때
            //사원 테이블에 있는 모든 사장을 찾아서 딕셔너리에 값을 담음.
            else
            {
                try
                {
                    DataTable result = new DataTable();
                    MySqlDataAdapter adapter = new MySqlDataAdapter();
                    DBManager.GetDBManager().OpenConnection();
                    adapter.SelectCommand = DBManager.GetDBManager().SetQuery(
                        $"select 사원번호, 이름 from s5584720.사원 where 직급 = \"사장\";"
                        ).CreateCommand();
                    adapter.Fill(result);
                    DBManager.GetDBManager().CloseConnection();

                    for (int i = 0; i < result.Rows.Count; i++)
                    {
                        결재자리스트[result.Rows[i]["사원번호"].ToString()] = result.Rows[i]["이름"].ToString();
                    }
                }
                catch (Exception ex)
                {
                    DBManager.GetDBManager().CloseConnection();
                    MessageBox.Show("결재자 기능 : Get_결재자리스트 : 결재자 2단계\n" + ex.Message);
                }
            }
            return 결재자리스트;
        }
    }
}
