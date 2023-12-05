using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamProject_test_v1
{
    /// <summary>
    /// 사용방법
    /// INSERT, DELETE, UPDATE 문
    /// DBManager.GetDBManager().SetQuery(query).ExecuteNonQuery()
    /// 
    /// SELECT 문
    /// DBManager.GetDBManager().OpenConnection()
    /// MySqlDataReader reader = DBManager.GetDBManager().SetQuery(query).ExecuteReader();
    /// DBManager.GetDBManager().CloseConnection()
    /// </summary>
    internal class DBManager
    {
        private static DBManager instance = new DBManager();

        private string connection_string; //MySQL DB 연결 정보

        private MySqlConnection connection; //연결

        private string query; //쿼리문

        public static DBManager GetDBManager() { return instance; }

        /// <summary>
        /// connecting_string에는 DB 연결 정보
        /// </summary>
        private DBManager()
        {
            connection_string = "Data Source = 115.85.181.212; Database = s5584720; Uid = s5584720; Pwd = s5584720; CharSet = utf8";
            try
            {
                connection = new MySqlConnection(connection_string);
            }
            catch (Exception ex)
            {
                MessageBox.Show("MySQL 연결에 실패했습니다.");
            }
        }


        /// <summary>
        /// SELECT문을 사용할 때 ExecuteReader를 사용하면 OpenConnection()으로 DB 연결
        /// </summary>
        public void OpenConnection()
        {
            if (connection.State == System.Data.ConnectionState.Closed)
            {
                connection.Open();
            }
        }

        /// <summary>
        /// OpenConnection()을 실행하고 사용이 끝나면 CloseConnection()으로 DB 닫기
        /// </summary>
        public void CloseConnection()
        {
            if (connection.State == System.Data.ConnectionState.Open)
            {
                connection.Close();
            }
        }
        
        /// <summary>
        /// 쿼리 넣기
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public DBManager SetQuery(string query)
        {
            this.query = query;
            //MessageBox.Show(query);
            return this;
        }

        /// <summary>
        /// INSERT, DELETE, UPDATE 등은 ExecuteNonQuery()사용 OpenConnection(), CloseConnection() 불필요
        /// </summary>
        /// <returns>성공: 0 이상, 실패: -1 값리턴</returns>
        public int ExecuteNonQuery()
        {
            int result = -1;
            OpenConnection();
            try
            {
                MySqlCommand cmd = CreateCommand();
                result = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("쿼리 실패"+ex);
                CloseConnection();
                return result;
            }
            CloseConnection();
            return result;
        }

        /// <summary>
        /// SELECT
        /// </summary>
        /// <returns></returns>
        public MySqlDataReader ExecuteReader()
        {
            using (var cmd = CreateCommand())
            {
                return cmd.ExecuteReader();
            }
        }


        public MySqlDataAdapter GetAdapter()
        {
            return new MySqlDataAdapter(query, connection);
        }

        public MySqlCommand CreateCommand()
        {
            return new MySqlCommand(this.query, connection);
        }
    }
}
