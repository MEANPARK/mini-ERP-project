using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamProject_test_v1
{
    internal class BigCategory
    {
        List<string> categories = new List<string>();
        private static BigCategory instance = new BigCategory();
        private BigCategory() { }
        public static BigCategory getInstance()
        {
            return instance;
        }
        public void setBigCategory(ComboBox comboBox)
        {
            categories.Clear();
            string query = "SELECT 대분류_name FROM 대분류 WHERE not 대분류_name = '추가수당'";
            DBManager.GetDBManager().OpenConnection();
            MySqlDataReader reader = DBManager.GetDBManager().SetQuery(query).ExecuteReader();
            while (reader.Read())
            {
                categories.Add(reader["대분류_name"].ToString()); //콤보박스 값 추가
            }
            DBManager.GetDBManager().CloseConnection();
            comboBox.Items.Clear();
            for (int i = 0; i < categories.Count; i++)
            {

                comboBox.Items.Add(categories[i]);
            }
        }
        public void setMidCategory(string bigcategory, ComboBox comboBox)
        {
            comboBox.Items.Clear();
            string query = $"SELECT 중분류_name FROM 중분류 WHERE 대분류_id = (SELECT 대분류_id FROM 대분류 WHERE 대분류_name = '{bigcategory}')";
            DBManager.GetDBManager().OpenConnection();
            MySqlDataReader reader = DBManager.GetDBManager().SetQuery(query).ExecuteReader();
            while (reader.Read())
            {
                comboBox.Items.Add(reader["중분류_name"]); //콤보박스 값 추가
            }
            DBManager.GetDBManager().CloseConnection();
        }
        public void setSmallCategory(string bigcategory, string midcategory, ComboBox comboBox)
        {
            comboBox.Items.Clear();
            string query = $"SELECT 소분류_name FROM 소분류 WHERE 중분류_id = (SELECT 중분류_id FROM 중분류 WHERE 중분류_name = '{midcategory}' AND 대분류_id = (SELECT 대분류_id FROM 대분류 WHERE 대분류_name = '{bigcategory}')) ";
            DBManager.GetDBManager().OpenConnection();
            MySqlDataReader reader = DBManager.GetDBManager().SetQuery(query).ExecuteReader();
            while (reader.Read())
            {
                comboBox.Items.Add(reader["소분류_name"]); //콤보박스 값 추가
            }
            DBManager.GetDBManager().CloseConnection();
        }
        public List<string> getBigCategories() 
        {
            return categories;
        }

    }
}
