using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TeamProject_test_v1
{
    internal class EmployeeInfo
    {
        public string 사원번호 { get; set; }
        public string 이름 { get; set; }
        public string 생년월일 { get; set; }
        public string 성별 { get; set; }
        public string 전화번호 { get; set; }
        public string 주소 { get; set; }
        public string 이메일 { get; set; }
        public string 계좌번호 { get; set; }
        public int 부서번호 { get; set; }
        public string 부서명 { get; set; }
        public int 직급번호 { get; set; }
        public string 직급 { get; set; }

        public EmployeeInfo()
        {
            사원번호 = string.Empty;
            이름 = string.Empty;
            생년월일 = string.Empty;
            성별 = string.Empty;
            전화번호 = string.Empty;
            주소 = string.Empty;
            이메일 = string.Empty;
            계좌번호 = string.Empty;
            부서번호 = -1;
            부서명 = string.Empty;
            직급번호 = -1;
            직급 = string.Empty;
        }

        public EmployeeInfo setEmployeeInfo(string username)
        {
            string query = $"SELECT * FROM 사원 WHERE 사원번호 = '{username}'";
            DBManager.GetDBManager().OpenConnection();
            MySqlDataReader reader = DBManager.GetDBManager().SetQuery(query).ExecuteReader();
            if (reader.Read())
            {
                사원번호 = reader["사원번호"].ToString();
                이름 = reader["이름"].ToString();
                생년월일 = Convert.ToDateTime(reader["생년월일"]).ToString("yyyy-MM-dd");
                성별 = reader["성별"].ToString();
                전화번호 = reader["전화번호"].ToString();
                주소 = reader["주소"].ToString();
                이메일 = reader["이메일"].ToString();
                계좌번호 = reader["계좌번호"].ToString();

                if (String.IsNullOrEmpty(reader["부서번호"].ToString()))
                {
                    부서번호 = -1;
                }
                else 부서번호 = Convert.ToInt32(reader["부서번호"]);

                부서명 = reader["부서명"].ToString();
                직급번호 = Convert.ToInt32(reader["직급번호"]);
                직급 = reader["직급"].ToString();
            }
            DBManager.GetDBManager().CloseConnection();
            return this;
        }

        public override string ToString()
        {
            return $"{this.부서명} {this.직급} {this.이름}";
        }
    }
}
