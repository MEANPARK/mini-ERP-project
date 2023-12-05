using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.ComponentModel.Design.ObjectSelectorEditor;
using static System.Net.Mime.MediaTypeNames;
using System.Windows.Forms;
using System.Data;
using System.Collections;
using MySql.Data.MySqlClient;
using Google.Protobuf.WellKnownTypes;
using MySqlX.XDevAPI.Common;

namespace TeamProject_test_v1
{
    internal class Additional_Allowance
    {
        private static Additional_Allowance aa = new Additional_Allowance();

        private Additional_Allowance()
        {
        }

        public static Additional_Allowance getInstance() { return aa; }

        public string sendQuery(string query)
        {
            DataTable dt = new DataTable();

            try
            {
                DBManager.GetDBManager().OpenConnection();
                using (MySqlDataReader reader = DBManager.GetDBManager().SetQuery(query).ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            dt.Columns.Add(reader.GetName(i), reader.GetFieldType(i));
                        }

                        while (reader.Read())
                        {
                            DataRow row = dt.NewRow();
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                row[i] = reader[i];
                            }
                            dt.Rows.Add(row);
                        }
                    }
                }
            }
            catch (Exception ex) { }
            finally
            {
                DBManager.GetDBManager().CloseConnection();
            }

            return dt.Rows[0][0].ToString();
        }

        public string startTime()
        {
            string query = $"SELECT 출근시간 FROM s5584720.출근부 WHERE 사원번호 = '{Properties.Settings.Default.myID}' AND 퇴근시간 IS NULL;";
            string[] words = sendQuery(query).Split(' ');
            return words[0];
        }

        private void endTask() // 퇴근시간 저장
        {
            var confirmResult2 = MessageBox.Show("퇴근하시겠습니까?", "확인창", MessageBoxButtons.YesNo);
            if (confirmResult2 == DialogResult.Yes)
            {
                string query = $"UPDATE s5584720.출근부 SET 퇴근시간 = now() WHERE 사원번호 = '{Properties.Settings.Default.myID}' AND 퇴근시간 IS NULL ";
                int result = DBManager.GetDBManager().SetQuery(query).ExecuteNonQuery();
                if (result == -1)
                {
                    MessageBox.Show("쿼리 실패");
                }
            }
        }

        public void autoAccepte()
        {
            string start = startTime();
            string num = Properties.Settings.Default.myID;
            endTask();
            string nigth = nightWork(num, start);
            string holly = holidayWork(num, start);

            int normal = Convert.ToInt32(normalWork(num, start));
            int total = normal + Convert.ToInt32(nigth); // 총 근무시간에 휴일이 있으면 어짜피 밑에서 걸러짐 즉, 휴일이 아니라는 가정하에 계산 (일반시간 + 야간시간)
            
            string over = overtimeWork(num, start, total.ToString()); // 일반 연장근무 (48시간 이상 근무)


            if (nigth == "0" && holly == "0" && over == "0")
            {
                string query = $"UPDATE s5584720.출근부 SET 일반근로시간 = '@nomalWork', 연장근로시간 = '0', 야간근로시간 = '0', 휴일근로시간 = '0', 총근로시간 = '@nomalWork', 결재여부 = 1 WHERE 사원번호 = '{Properties.Settings.Default.myID}' AND 출근시간 LIKE '@start%';";
                query = query.Replace("@nomalWork", normal.ToString());
                query = query.Replace("@start", start);
                DBManager.GetDBManager().SetQuery(query).ExecuteNonQuery();
            }
        }

        public string normalWork(string num, string start) // 통상 근무시간 계산
        {
            string query = "SELECT TIMESTAMPDIFF(MINUTE, 출근시간, 퇴근시간) AS 일반근무시간 FROM s5584720.출근부 WHERE 사원번호 = '@num' AND 출근시간 LIKE '@end%';"; // 퇴근시간 - 출근시간
            query = query.Replace("@num", num);
            query = query.Replace("@end", start);

            string result = sendQuery(query);

            query = "SELECT date_format(출근시간, '%Y-%m-%d %H:%i:%s') FROM s5584720.출근부 WHERE 사원번호 = '@num' AND 출근시간 LIKE '@end%';";
            query = query.Replace("@num", num);
            query = query.Replace("@end", start);
            string st = sendQuery(query);

            query = "SELECT date_format(퇴근시간, '%Y-%m-%d %H:%i:%s') FROM s5584720.출근부 WHERE 사원번호 = '@num' AND 출근시간 LIKE '@end%';";
            query = query.Replace("@num", num);
            query = query.Replace("@end", start);
            string ed = sendQuery(query);

            int count = 0;
            if (Convert.ToDateTime(st).ToString("yyyy-MM-dd") == Convert.ToDateTime(ed).ToString("yyyy-MM-dd")) // 같은 날 퇴근
            {
                if (Convert.ToInt32(Convert.ToDateTime(st).ToString("HH")) < 6) // 6시 이전 출근 (야근)
                {
                    if (Convert.ToInt32(Convert.ToDateTime(ed).ToString("HH")) < 6) // 6시 이전 퇴근 (야근)
                    {
                        count = 0;
                    }
                    else // 그 이후 퇴근
                    {
                        if (Convert.ToInt32(Convert.ToDateTime(ed).ToString("HH")) >= 13) // 13시 이후 퇴근
                        {
                            if (Convert.ToInt32(Convert.ToDateTime(ed).ToString("HH")) >= 18) // 18시 이후 퇴근 (야근은 따로 계산)
                            {
                                query = "SELECT TIMESTAMPDIFF(MINUTE, '@end 06:00:00', '@end 18:00:00') AS 일반근무시간 FROM s5584720.출근부 WHERE 사원번호 = '@num' AND 출근시간 LIKE '@end%';";
                                query = query.Replace("@num", num);
                                string[] words = st.Split(' ');
                                query = query.Replace("@end", words[0]);
                                result = sendQuery(query);
                                count = Convert.ToInt32(result) - 60; // 점심시간 제외
                            }
                            else // 13시 이후 18시 이내 퇴근
                            {
                                count = Convert.ToInt32(result) - 60; // 점심시간 제외
                            }
                        }
                        else if (Convert.ToInt32(Convert.ToDateTime(ed).ToString("HH")) < 12) // 12시 이전 퇴근
                        {
                            query = "SELECT TIMESTAMPDIFF(MINUTE, '@end 06:00:00', 퇴근시간) AS 일반근무시간 FROM s5584720.출근부 WHERE 사원번호 = '@num' AND 출근시간 LIKE '@end%';";
                            query = query.Replace("@num", num);
                            string[] words = st.Split(' ');
                            query = query.Replace("@end", words[0]);
                            result = sendQuery(query);
                            count = Convert.ToInt32(result);
                        }
                        else  // 12시에서 13시 사이에 퇴근
                        {
                            query = "SELECT TIMESTAMPDIFF(MINUTE, '@end 06:00:00', 퇴근시간) AS 일반근무시간 FROM s5584720.출근부 WHERE 사원번호 = '@num' AND 출근시간 LIKE '@end%';";
                            query = query.Replace("@num", num);
                            string[] words = st.Split(' ');
                            query = query.Replace("@end", words[0]);
                            result = sendQuery(query);
                            count = Convert.ToInt32(result) - Convert.ToInt32(Convert.ToDateTime(ed).ToString("mm"));
                        }
                    }
                }
                else if (Convert.ToInt32(Convert.ToDateTime(st).ToString("HH")) < 12) // 12시 이전 출근
                {
                    if (Convert.ToInt32(Convert.ToDateTime(ed).ToString("HH")) >= 13) // 13시 이후 퇴근
                    {
                        if (Convert.ToInt32(Convert.ToDateTime(ed).ToString("HH")) >= 18) // 18시 이후 퇴근 (야근은 따로 계산)
                        {
                            query = "SELECT TIMESTAMPDIFF(MINUTE, 출근시간, '@end 18:00:00') AS 일반근무시간 FROM s5584720.출근부 WHERE 사원번호 = '@num' AND 출근시간 LIKE '@end%';";
                            query = query.Replace("@num", num);
                            string[] words = st.Split(' ');
                            query = query.Replace("@end", words[0]);
                            result = sendQuery(query);
                            count = Convert.ToInt32(result) - 60; // 점심시간 제외
                        }
                        else // 13시 이후 18시 이내 퇴근
                        {
                            count = Convert.ToInt32(result) - 60; // 점심시간 제외
                        }
                    }
                    else if (Convert.ToInt32(Convert.ToDateTime(ed).ToString("HH")) < 12) // 12시 이전 퇴근
                    {
                        count = Convert.ToInt32(result);
                    }
                    else  // 12시에서 13시 사이에 퇴근
                    {
                        count = Convert.ToInt32(result) - Convert.ToInt32(Convert.ToDateTime(ed).ToString("mm"));
                    }
                }
                else if (Convert.ToInt32(Convert.ToDateTime(st).ToString("HH")) >= 13) // 13시 이후 출근 
                {
                    if (Convert.ToInt32(Convert.ToDateTime(st).ToString("HH")) < 18) // 18시 이전 출근
                    {
                        if (Convert.ToInt32(Convert.ToDateTime(ed).ToString("HH")) < 18) // 18시 이전 퇴근
                        {
                            count = Convert.ToInt32(result); // 오후 출근 오후 퇴근
                        }
                        else // 18시 이후 퇴근 (야근은 따로 계산)
                        {
                            query = "SELECT TIMESTAMPDIFF(MINUTE, 출근시간, '@end 18:00:00') AS 일반근무시간 FROM s5584720.출근부 WHERE 사원번호 = '@num' AND 출근시간 LIKE '@end%';";
                            query = query.Replace("@num", num);
                            string[] words = st.Split(' ');
                            query = query.Replace("@end", words[0]);
                            result = sendQuery(query);
                            count = Convert.ToInt32(result) - 60; // 점심시간 제외
                        }
                    }
                    else // 18시 이후 출근 (야근은 따로 계산)
                    {
                        count = 0; // 정기 근무 없음
                    }
                }
                else // 12시 이후 출근 13시 이전 출근
                {
                    if (Convert.ToInt32(Convert.ToDateTime(ed).ToString("HH")) >= 18) // 19시 이후 퇴근 (야근은 따로 계산)
                    {
                        query = "SELECT TIMESTAMPDIFF(MINUTE, '@end 13:00:00', '@end 18:00:00') AS 일반근무시간 FROM s5584720.출근부 WHERE 사원번호 = '@num' AND 출근시간 LIKE '@end%';";
                        query = query.Replace("@num", num);
                        string[] words = st.Split(' ');
                        query = query.Replace("@end", words[0]);
                        result = sendQuery(query);
                        count = Convert.ToInt32(result);
                    }
                    else if (Convert.ToInt32(Convert.ToDateTime(ed).ToString("HH")) < 13) // 13시 이전 퇴근
                    {
                        count = 0;
                    }
                    else // 13시에서 19시 사이 퇴근
                    {
                        query = "SELECT TIMESTAMPDIFF(MINUTE, '@end 13:00:00', 퇴근시간) AS 일반근무시간 FROM s5584720.출근부 WHERE 사원번호 = '@num' AND 출근시간 LIKE '@end%';";
                        query = query.Replace("@num", num);
                        string[] words = st.Split(' ');
                        query = query.Replace("@end", words[0]);
                        result = sendQuery(query);
                        count = Convert.ToInt32(result);
                    }
                }
            }
            else // 다른날 퇴근 (야근은 따로 계산 야근 이전까지만)
            {
                if (Convert.ToInt32(Convert.ToDateTime(st).ToString("HH")) < 6) // 6시 이전 출근
                {
                    query = "SELECT TIMESTAMPDIFF(MINUTE, '@end 06:00:00', '@end 18:00:00') AS 일반근무시간 FROM s5584720.출근부 WHERE 사원번호 = '@num' AND 출근시간 LIKE '@end%';";
                    query = query.Replace("@num", num);
                    string[] words = st.Split(' ');
                    query = query.Replace("@end", words[0]);
                    result = sendQuery(query);
                    count = Convert.ToInt32(result) - 60; // 점심시간 제외
                }
                else if (Convert.ToInt32(Convert.ToDateTime(st).ToString("HH")) < 12) // 12시 이전 출근
                {
                    query = "SELECT TIMESTAMPDIFF(MINUTE, 출근시간, '@end 18:00:00') AS 일반근무시간 FROM s5584720.출근부 WHERE 사원번호 = '@num' AND 출근시간 LIKE '@end%';";
                    query = query.Replace("@num", num);
                    string[] words = st.Split(' ');
                    query = query.Replace("@end", words[0]);
                    result = sendQuery(query);
                    count = Convert.ToInt32(result) - 60; // 점심시간 제외
                }
                else if (Convert.ToInt32(Convert.ToDateTime(st).ToString("HH")) < 18) // 18시 이전 출근
                {
                    if (Convert.ToInt32(Convert.ToDateTime(st).ToString("HH")) < 13) // 13시 이전 출근
                    {
                        query = "SELECT TIMESTAMPDIFF(MINUTE, '@end 13:00:00', '@end 18:00:00') AS 일반근무시간 FROM s5584720.출근부 WHERE 사원번호 = '@num' AND 출근시간 LIKE '@end%';";
                        query = query.Replace("@num", num);
                        string[] words = st.Split(' ');
                        query = query.Replace("@end", words[0]);
                        result = sendQuery(query);
                        count = Convert.ToInt32(result);
                    }
                    else // 그외 (13시 이후 출근)
                    {
                        query = "SELECT TIMESTAMPDIFF(MINUTE, 출근시간, '@end 18:00:00') AS 일반근무시간 FROM s5584720.출근부 WHERE 사원번호 = '@num' AND 출근시간 LIKE '@end%';";
                        query = query.Replace("@num", num);
                        string[] words = st.Split(' ');
                        query = query.Replace("@end", words[0]);
                        result = sendQuery(query);
                        count = Convert.ToInt32(result);
                    }
                }
                else // 18시 이후 출근 (야근)
                {
                    result = "0";
                    count = Convert.ToInt32(result);
                }
            }
            return count.ToString();
        }

        public string nightWork(string num, string start) //저녁 10시~ 새벽 6시 통상 임금의 50% 가산
        {
            string query = "SELECT date_format(출근시간, '%Y-%m-%d %H:%i:%s') FROM s5584720.출근부 WHERE 사원번호 = '@num' AND 출근시간 LIKE '@end%';";
            query = query.Replace("@num", num);
            query = query.Replace("@end", start);
            string st = sendQuery(query);

            query = "SELECT date_format(퇴근시간, '%Y-%m-%d %H:%i:%s') FROM s5584720.출근부 WHERE 사원번호 = '@num' AND 출근시간 LIKE '@end%';";
            query = query.Replace("@num", num);
            query = query.Replace("@end", start);
            string ed = sendQuery(query);

            string result = "0";

            if (Convert.ToDateTime(st).ToString("yyyy-MM-dd") == Convert.ToDateTime(ed).ToString("yyyy-MM-dd")) // 같은 날 퇴근
            {
                if (Convert.ToInt32(Convert.ToDateTime(st).ToString("HH")) < 6) // 6시 이전 출근
                {
                    if (Convert.ToInt32(Convert.ToDateTime(ed).ToString("HH")) < 6) // 6시 이전 퇴근
                    {
                        query = "SELECT TIMESTAMPDIFF(MINUTE, 출근시간, 퇴근시간) AS 야간근무시간 FROM s5584720.출근부 WHERE 사원번호 = '@num' AND 출근시간 LIKE '@end%';"; // 퇴근시간 - 출근시간
                        query = query.Replace("@num", num);
                        query = query.Replace("@end", start);
                        result = sendQuery(query);
                    }
                    else if (Convert.ToInt32(Convert.ToDateTime(ed).ToString("HH")) >= 22) // 22시 이후 퇴근
                    {
                        query = "SELECT TIMESTAMPDIFF(MINUTE, 출근시간, '@end 06:00:00') AS 일반근무시간 FROM s5584720.출근부 WHERE 사원번호 = '@num' AND 출근시간 LIKE '@end%';";
                        query = query.Replace("@num", num);
                        string[] words = st.Split(' ');
                        query = query.Replace("@end", words[0]);
                        result = sendQuery(query);

                        query = "SELECT TIMESTAMPDIFF(MINUTE, '@end 22:00:00', 퇴근시간) AS 일반근무시간 FROM s5584720.출근부 WHERE 사원번호 = '@num' AND 출근시간 LIKE '@end%';";
                        query = query.Replace("@num", num);
                        query = query.Replace("@end", words[0]);
                        result = (Convert.ToInt32(result) + Convert.ToInt32(sendQuery(query))).ToString();
                    }
                    else // 6시에서 22시 사이에 퇴근
                    {
                        query = "SELECT TIMESTAMPDIFF(MINUTE, 출근시간, '@end 06:00:00') AS 일반근무시간 FROM s5584720.출근부 WHERE 사원번호 = '@num' AND 출근시간 LIKE '@end%';";
                        query = query.Replace("@num", num);
                        string[] words = st.Split(' ');
                        query = query.Replace("@end", words[0]);
                        result = sendQuery(query);
                    }
                }
                else if (Convert.ToInt32(Convert.ToDateTime(st).ToString("HH")) >= 22) // 22시 이후 출근이라면
                {
                    query = "SELECT TIMESTAMPDIFF(MINUTE, 출근시간, 퇴근시간) AS 일반근무시간 FROM s5584720.출근부 WHERE 사원번호 = '@num' AND 출근시간 LIKE '@end%';"; // 퇴근시간 - 출근시간
                    query = query.Replace("@num", num);
                    query = query.Replace("@end", start);
                    result = sendQuery(query);
                }
                else // 22시 이전 출근이라면 22시부터 퇴근시간까지 계산
                {
                    query = "SELECT TIMESTAMPDIFF(MINUTE, '@start 22:00:00', 퇴근시간) AS 일반근무시간 FROM s5584720.출근부 WHERE 사원번호 = '@num' AND 출근시간 LIKE '@start%';";
                    query = query.Replace("@num", num);
                    string[] words = st.Split(' ');
                    query = query.Replace("@start", words[0]);
                    result = sendQuery(query);

                    if (Convert.ToInt32(result) < 0) // 야간을 안했으면 0
                    {
                        return "0";
                    }
                }
            }
            else // 다른 날 퇴근
            {
                if (Convert.ToInt32(Convert.ToDateTime(st).ToString("HH")) < 6) // 6시 이전 출근
                {
                    query = "SELECT TIMESTAMPDIFF(MINUTE, 출근시간, '@end 06:00:00') AS 일반근무시간 FROM s5584720.출근부 WHERE 사원번호 = '@num' AND 출근시간 LIKE '@end%';";
                    query = query.Replace("@num", num);
                    string[] words = st.Split(' ');
                    query = query.Replace("@end", words[0]);
                    result = sendQuery(query);

                    query = "SELECT TIMESTAMPDIFF(MINUTE, '@end 22:00:00', 퇴근시간) AS 일반근무시간 FROM s5584720.출근부 WHERE 사원번호 = '@num' AND 출근시간 LIKE '@end%';";
                    query = query.Replace("@num", num);
                    query = query.Replace("@end", words[0]);
                    result = (Convert.ToInt32(result) + Convert.ToInt32(sendQuery(query))).ToString();

                }
                else if (Convert.ToInt32(Convert.ToDateTime(st).ToString("HH")) >= 22) // 22시 이후 출근이라면
                {
                    if (Convert.ToInt32(Convert.ToDateTime(ed).ToString("HH")) < 6) // 다음날 6시 이전 퇴근이라면
                    {
                        query = "SELECT TIMESTAMPDIFF(MINUTE, 출근시간, 퇴근시간) AS 일반근무시간 FROM s5584720.출근부 WHERE 사원번호 = '@num' AND 출근시간 LIKE '@end%';"; // 퇴근시간 - 출근시간
                        query = query.Replace("@num", num);
                        string[] words = st.Split(' ');
                        query = query.Replace("@end", words[0]);
                        result = sendQuery(query);
                    }
                    else if (Convert.ToInt32(Convert.ToDateTime(ed).ToString("HH")) < 22) // 다음날 22시 이전 퇴근이라면
                    {
                        query = "SELECT TIMESTAMPDIFF(MINUTE, 출근시간, '@ed 06:00:00') AS 일반근무시간 FROM s5584720.출근부 WHERE 사원번호 = '@num' AND 출근시간 LIKE '@st%';"; // 퇴근시간 - 출근시간
                        query = query.Replace("@num", num);

                        string[] startTime = st.Split(' ');
                        query = query.Replace("@st", startTime[0]);

                        string[] endTime = ed.Split(' ');
                        query = query.Replace("@ed", endTime[0]);

                        result = sendQuery(query);
                    }
                    else // 다음날 22시 이후 퇴근이라면
                    {
                        query = "SELECT TIMESTAMPDIFF(MINUTE, 출근시간, '@ed 06:00:00') AS 일반근무시간 FROM s5584720.출근부 WHERE 사원번호 = '@num' AND 출근시간 LIKE '@st%';"; // 전날 22시 -> 다음날 6시
                        query = query.Replace("@num", num);

                        string[] startTime = st.Split(' ');
                        query = query.Replace("@st", startTime[0]);

                        string[] endTime = ed.Split(' ');
                        query = query.Replace("@ed", endTime[0]);

                        result = sendQuery(query);

                        query = "SELECT TIMESTAMPDIFF(MINUTE, '@ed 22:00:00', 퇴근시간) AS 일반근무시간 FROM s5584720.출근부 WHERE 사원번호 = '@num' AND 출근시간 LIKE '@st%';"; // 다음날 22시 -> 퇴근시간
                        query = query.Replace("@num", num);
                        query = query.Replace("@st", startTime[0]);
                        query = query.Replace("@ed", endTime[0]);

                        result = (Convert.ToInt32(result) + Convert.ToInt32(sendQuery(query))).ToString();
                    }
                }
                else // 22시 이전 출근이라면 22시부터 퇴근시간까지 계산
                {
                    if (Convert.ToInt32(Convert.ToDateTime(ed).ToString("HH")) < 6) // 다음날 6시 이전 퇴근이라면
                    {
                        query = "SELECT TIMESTAMPDIFF(MINUTE, '@end 22:00:00', 퇴근시간) AS 일반근무시간 FROM s5584720.출근부 WHERE 사원번호 = '@num' AND 출근시간 LIKE '@end%';"; // 22시 부터 다음날 퇴근까지
                        query = query.Replace("@num", num);
                        string[] words = st.Split(' ');
                        query = query.Replace("@end", words[0]);
                        result = sendQuery(query);
                    }
                    else if (Convert.ToInt32(Convert.ToDateTime(ed).ToString("HH")) < 22) // 다음날 22시 이전 퇴근이라면
                    {
                        query = "SELECT TIMESTAMPDIFF(MINUTE, '@st 22:00:00', '@ed 06:00:00') AS 일반근무시간 FROM s5584720.출근부 WHERE 사원번호 = '@num' AND 출근시간 LIKE '@st%';"; // 전날 22시부터 다음날 6시까지
                        query = query.Replace("@num", num);

                        string[] startTime = st.Split(' ');
                        query = query.Replace("@st", startTime[0]);

                        string[] endTime = ed.Split(' ');
                        query = query.Replace("@ed", endTime[0]);

                        result = sendQuery(query);
                    }
                    else // 다음날 22시 이후 퇴근이라면
                    {
                        query = "SELECT TIMESTAMPDIFF(MINUTE, '@st 22:00:00', '@ed 06:00:00') AS 일반근무시간 FROM s5584720.출근부 WHERE 사원번호 = '@num' AND 출근시간 LIKE '@st%';"; // 전날 22시 -> 다음날 6시
                        query = query.Replace("@num", num);

                        string[] startTime = st.Split(' ');
                        query = query.Replace("@st", startTime[0]);

                        string[] endTime = ed.Split(' ');
                        query = query.Replace("@ed", endTime[0]);

                        result = sendQuery(query);

                        query = "SELECT TIMESTAMPDIFF(MINUTE, '@ed 22:00:00', 퇴근시간) AS 일반근무시간 FROM s5584720.출근부 WHERE 사원번호 = '@num' AND 출근시간 LIKE '@st%';"; // 다음날 22시 -> 퇴근시간
                        query = query.Replace("@num", num);
                        query = query.Replace("@st", startTime[0]);
                        query = query.Replace("@ed", endTime[0]);

                        result = (Convert.ToInt32(result) + Convert.ToInt32(sendQuery(query))).ToString();
                    }
                }
            }

            return result;
        }

        public string holidayWork(string num, string start) // 휴일 근무
        {
            string date = start.Replace("-", "");
            if (PublicAPI.getInstance().GetAPI(date) == true || checkWeekend(Convert.ToDateTime(start)) == 1)
            {
                int normal = Convert.ToInt32(normalWork(num, start));
                int over = Convert.ToInt32(betweenOvertimeWork(num, start));
                int night = Convert.ToInt32(nightWork(num, start));
                return (normal + over + night).ToString();
            }

            return "0";
        }

        public DataTable sendQuery_overTime(string query)
        {
            DataTable dt = new DataTable();

            try
            {
                DBManager.GetDBManager().OpenConnection();
                using (MySqlDataReader reader = DBManager.GetDBManager().SetQuery(query).ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            dt.Columns.Add(reader.GetName(i), reader.GetFieldType(i));
                        }

                        while (reader.Read())
                        {
                            DataRow row = dt.NewRow();
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                row[i] = reader[i];
                            }
                            dt.Rows.Add(row);
                        }
                    }
                }
            }
            catch (Exception ex) { }
            finally
            {
                DBManager.GetDBManager().CloseConnection();
            }

            return dt;
        }

        public string betweenOvertimeWork(string num, string start)
        {
            string query = "SELECT date_format(출근시간, '%Y-%m-%d %H:%i:%s') FROM s5584720.출근부 WHERE 사원번호 = '@num' AND 출근시간 LIKE '@end%';";
            query = query.Replace("@num", num);
            query = query.Replace("@end", start);
            string st = sendQuery(query);

            query = "SELECT date_format(퇴근시간, '%Y-%m-%d %H:%i:%s') FROM s5584720.출근부 WHERE 사원번호 = '@num' AND 출근시간 LIKE '@end%';";
            query = query.Replace("@num", num);
            query = query.Replace("@end", start);
            string ed = sendQuery(query);

            if (Convert.ToDateTime(st).ToString("yyyy-MM-dd") == Convert.ToDateTime(ed).ToString("yyyy-MM-dd")) // 같은 날 퇴근
            {
                if (Convert.ToInt32(Convert.ToDateTime(st).ToString("HH")) < 19) // 19시 이전에 출근을 하고
                {
                    if (Convert.ToInt32(Convert.ToDateTime(ed).ToString("HH")) >= 22) // 22시 이후에 퇴근을 하면
                    {
                        return "180"; // 120분 연장근무 (당일 기록 연장수당)
                    }
                    else if (Convert.ToInt32(Convert.ToDateTime(ed).ToString("HH")) < 19) // 18시 이전에 퇴근하면
                    {
                        return "0"; // 연장근무 X
                    }
                    else // 19시에서 22시 사이 퇴근
                    {
                        query = "SELECT TIMESTAMPDIFF(MINUTE, '@end 19:00:00', 퇴근시간) AS 일반근무시간 FROM s5584720.출근부 WHERE 사원번호 = '@num' AND 출근시간 LIKE '@end%';";
                        query = query.Replace("@num", num);
                        query = query.Replace("@end", start);
                        return sendQuery(query);
                    }
                }
                else if (Convert.ToInt32(Convert.ToDateTime(st).ToString("HH")) < 22) // 22시 이전에 출근
                {
                    if (Convert.ToInt32(Convert.ToDateTime(ed).ToString("HH")) >= 22) // 22시 이후에 퇴근을 하면
                    {
                        query = "SELECT TIMESTAMPDIFF(MINUTE, 출근시간, '@end 22:00:00') AS 일반근무시간 FROM s5584720.출근부 WHERE 사원번호 = '@num' AND 출근시간 LIKE '@end%';";
                        query = query.Replace("@num", num);
                        query = query.Replace("@end", start);
                        return sendQuery(query);
                    }
                    else // 22시 이전에 퇴근하면
                    {
                        query = "SELECT TIMESTAMPDIFF(MINUTE, 출근시간, 퇴근시간) AS 일반근무시간 FROM s5584720.출근부 WHERE 사원번호 = '@num' AND 출근시간 LIKE '@end%';";
                        query = query.Replace("@num", num);
                        query = query.Replace("@end", start);
                        return sendQuery(query);
                    }
                }
                else // 22시 이후 출근
                {
                    return "0";
                }
            }
            else // 다른날 퇴근
            {
                if (Convert.ToInt32(Convert.ToDateTime(st).ToString("HH")) < 19) // 18시 이전에 출근을 하고
                {
                    return "180"; // 120분 연장근무 (당일 기록 연장수당)
                }
                else if (Convert.ToInt32(Convert.ToDateTime(st).ToString("HH")) < 22) // 22시 이전에 출근
                {
                    query = "SELECT TIMESTAMPDIFF(MINUTE, 출근시간, '@end 22:00:00') AS 일반근무시간 FROM s5584720.출근부 WHERE 사원번호 = '@num' AND 출근시간 LIKE '@end%';";
                    query = query.Replace("@num", num);
                    query = query.Replace("@end", start);
                    return sendQuery(query);
                }
                else // 그 이후 출근
                {
                    return "0";
                }
            }
        }

        public string overtimeWork(string num, string start, string total) // 주 48시간(법정근로시간)을 초과하는 근로시간 통상 임금의 50% // 일주일
        {
            string query = "SELECT 총근로시간, 연장근로시간 FROM s5584720.출근부 WHERE 사원번호 = '@num' AND 출근시간 BETWEEN '@between 00:00:00' AND '@end 23:59:59';"; // 그주 총 일한 시간 계산 (당일 제외)
            query = query.Replace("@num", num);

            string[] end = addDate(Convert.ToDateTime(start)).Split(' ');
            query = query.Replace("@end", end[0]);

            string[] between = subDate(Convert.ToDateTime(start)).Split(' ');
            query = query.Replace("@between", between[0]);

            DataTable dt = new DataTable();
            dt = sendQuery_overTime(query);

            int result = 0; // 총 근로시간
            int sub = 0; // 측정된 총 연장근무시간

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                result = result + Convert.ToInt32(dt.Rows[i][0].ToString());
                sub = sub + Convert.ToInt32(dt.Rows[i][1].ToString());
            }

            result = result + Convert.ToInt32(total); // 당일 일한 시간 추가
            result = result - 2880; // 48시간 제외
            result = result - sub; // 계산된 시간 제외

            if (result > 0) // 48시간 및 계산된 시간을 제외했을때, 총 근로 시간이 많다면 연장근무를 한것이다.
            {
                if (Convert.ToInt32(total) < result) // 당일 근로시간과 비교, 당일 근로시간보다 연장 근무시간이 크면 당일 근로시간 출력
                {
                    return total.ToString();
                }
                else // 당일 근로시간보다 연장 근무 시간이 작으면 연장 근무 시간 출력
                {
                    return result.ToString();
                }
            }
            else  // 48시간 안넘었으면 0 출력
            {
                return "0";
            }

        }

        private string subDate(DateTime date)
        {
            // 빼고 싶은 날짜 수를 지정합니다. 음수 값을 사용하여 뺄 수 있습니다.
            int daysToSubtract = checkWeek(date); // 예: 7일 전으로 이동

            // 현재 날짜에서 지정된 날짜 수를 뺍니다.
            DateTime newDate = date.AddDays(-daysToSubtract);

            return newDate.ToString();
        }

        private string addDate(DateTime date)
        {
            int daysToSubtract = checkWeek(date); // 6 - n 일 후로 이동

            DateTime newDate = date.AddDays(6 - daysToSubtract);

            return newDate.ToString();
        }

        private int checkWeek(DateTime dt) // 연장근무 위해 지금으로 부터 몇일 전 데이터 부터 불러와야되는지 계산
        {
            int week = 0;

            switch (dt.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    week = 0;
                    break;
                case DayOfWeek.Tuesday:
                    week = 1;
                    break;
                case DayOfWeek.Thursday:
                    week = 2;
                    break;
                case DayOfWeek.Wednesday:
                    week = 3;
                    break;
                case DayOfWeek.Friday:
                    week = 4;
                    break;
                case DayOfWeek.Saturday:
                    week = 5;
                    break;
                case DayOfWeek.Sunday:
                    week = 6;
                    break;
            }

            return week;
        }

        private int checkWeekend(DateTime dt) // 평일/주말 판별
        {
            int weekend = 0;

            switch (dt.DayOfWeek)
            {
                case DayOfWeek.Saturday:
                    weekend = 1;
                    break;
                case DayOfWeek.Sunday:
                    weekend = 1;
                    break;
                default:
                    weekend = 0;
                    break;
            }

            return weekend;
        }


        // 여기서부터는 관리자 화면 같은 코드

        public void check_salary(string num)
        {
            string query = "SELECT 직급번호 FROM s5584720.사원 where s5584720.사원.사원번호 = '@num';";
            query = query.Replace("@num", num);

            if (sendQuery(query) == "0") // 사장이면 확인
            {
                checkTenth();
            }
        }

        private DataTable collection_number()
        {
            string query = "SELECT distinct 사원번호 FROM s5584720.출근부 where Month(출근시간) = Month(now()) - 1;";
            return sendQuery_overTime(query);
        }

        private void checkTenth() // 날짜가 10일이면 정산
        {
            DataTable dt = new DataTable();
            dt = collection_number(); // 전 사원 사원 번호 확인

            DateTime currentDate = DateTime.Now;
            int dayOfMonth = currentDate.Day;

            if (dayOfMonth == 10) // 현재 날짜로 되어있기 때문에 시연할때 날짜 변경이나 수치 임의로 주입 필요
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    추가수당.getInstance().moneyTable(dt.Rows[i][0].ToString()); // 순차적으로 지급
                }
            }
        }
    }
}
