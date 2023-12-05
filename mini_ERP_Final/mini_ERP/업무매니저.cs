using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TeamProject_test_v1
{
    internal class 업무매니저
    {
        private static 업무매니저 instance = new 업무매니저();
        public static 업무매니저 GetInstance() { return instance; }
        private 업무매니저() { }


        // return Dictionary<'업무_id', '업무_content'>
        public Dictionary<string, string> Get_업무리스트()
        {
            Dictionary<string, string> dic = null;
            try
            {
                dic = new Dictionary<string, string>();
                DBManager.GetDBManager().OpenConnection();
                using (var rdr = DBManager.GetDBManager().SetQuery($"select 업무_id, 업무_content from s5584720.업무 where 업무담당자_id = " +
                    $"{사용자매니저.GetInstance().Get_사원번호()};"
                    ).CreateCommand().ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        dic.Add(rdr["업무_id"].ToString(), rdr["업무_content"].ToString());
                    }
                }
                DBManager.GetDBManager().CloseConnection();
                return dic;
            }
            catch (Exception ex) 
            {
                DBManager.GetDBManager().CloseConnection();
                MessageBox.Show("Get_업무리스트 예외 in 업무 매니저\n"+ex.Message);
            }
            return dic;
        }
    }
}
