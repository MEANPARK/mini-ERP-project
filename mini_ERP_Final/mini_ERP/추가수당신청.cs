using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TeamProject_test_v1
{
    public partial class 추가수당신청 : Form
    {
        string num = string.Empty;

        public 추가수당신청()
        {
            InitializeComponent();
        }

        private void 추가수당신청_Load(object sender, EventArgs e)
        {
            this.num = Properties.Settings.Default.myID; // 임시로 사용 실제로는 밑에 코드에서 로드 필요할듯
            출근시간가져오기();
        }

        private void 출근시간가져오기()
        {
            string query = $"SELECT 출근시간 FROM s5584720.출근부 where 사원번호 = '{num}' and 결재여부 = 0 AND 퇴근시간 IS NOT NULL";
            DBManager.GetDBManager().OpenConnection();
            MySqlDataReader reader = DBManager.GetDBManager().SetQuery(query).ExecuteReader();
            while (reader.Read())
            {
                출근시간_comboBox.Items.Add(Convert.ToDateTime(reader["출근시간"]).ToString("yyyy-MM-dd HH:mm:ss"));
            }
            DBManager.GetDBManager().CloseConnection();
        }

        private Boolean check_holiday(string word)
        {
            if (Additional_Allowance.getInstance().holidayWork(num, word) == "0")
            {
                return false;
            }
            else { return true; }
        }

        private string information(string normal, string night, string holiy, string over, string totalWork)
        {
            string information = "출근시간 = @start, 퇴근시간 = @end, 일반근무시간 = @normal, 야간근무시간 = @night, 휴일근무시간 = @holiy, 연장근무시간 = @over, 총 근무시간 = @total";
            information = information.Replace("@start", 출근시간_comboBox.Text);
            information = information.Replace("@end", end_textBox.Text);
            information = information.Replace("@normal", normal);
            information = information.Replace("@night", night);
            information = information.Replace("@holiy", holiy);
            information = information.Replace("@over", over);
            information = information.Replace("@total", totalWork);
            return information;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(end_textBox.Text)) //출근 시간 값이 비어 있을 경우 예외 처리
            {
                MessageBox.Show("출근 시간을 선택해 주세요");
                return;
            }
            string[] words = 출근시간_comboBox.Text.Split(' '); // 출근 시간의 날짜 추출
            int total = 0;
            string normal = Additional_Allowance.getInstance().normalWork(num, words[0]);
            string over = string.Empty;
            string night = Additional_Allowance.getInstance().nightWork(num, words[0]);
            string holiy = string.Empty;
            string totalWork = string.Empty;
            int cnt = 0;

            int subNormal = Convert.ToInt32(Additional_Allowance.getInstance().betweenOvertimeWork(num, words[0])); // 당일 연장근무 시간

            string query = "UPDATE s5584720.출근부 SET 일반근로시간 = '@nomalWork', 연장근로시간 = '@overtimeWork', 야간근로시간 = '@nigthWork', 휴일근로시간 = '@holidayWork', 총근로시간 = '@totalWork', 결재여부 = 0 WHERE 사원번호 = '@num' AND 출근시간 LIKE '@start%';";

            query = query.Replace("@num", num);
            query = query.Replace("@start", words[0]);

            if (check_holiday(words[0])) // 휴일근무 -> 근무 총 시간은 휴일근무
            {
                query = query.Replace("@nomalWork", "0");
                totalWork = Additional_Allowance.getInstance().holidayWork(num, words[0]);
                holiy = Additional_Allowance.getInstance().holidayWork(num, words[0]);
                over = Additional_Allowance.getInstance().overtimeWork(num, words[0], Additional_Allowance.getInstance().holidayWork(num, words[0]));
            }
            else // 휴일근무 X -> 근무 총 시간은 일반근무 + 야간근무
            {
                holiy = "0";
                total = Convert.ToInt32(Additional_Allowance.getInstance().normalWork(num, words[0])) + Convert.ToInt32(Additional_Allowance.getInstance().nightWork(num, words[0]));
                over = Additional_Allowance.getInstance().overtimeWork(num, words[0], total.ToString());
                totalWork = total.ToString();
                cnt = Convert.ToInt32(over) - Convert.ToInt32(night); // 연장 근무 - 야간근무
            }

            if (cnt > 0)
            {
                normal = (Convert.ToInt32(normal) - cnt).ToString();
            }

            query = query.Replace("@holidayWork", holiy);
            query = query.Replace("@nomalWork", normal);
            query = query.Replace("@nigthWork", night);
            query = query.Replace("@overtimeWork", over);
            query = query.Replace("@totalWork", totalWork);

            DBManager.GetDBManager().SetQuery(query).ExecuteNonQuery();
            MoneyToAccept.getInstance().SendAdditionalAllowance(num, information(normal, night, holiy, over, totalWork));

            this.Close();
        }

        private void Cancle_button_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void send_EndTime(string start)
        {
            string query = "UPDATE s5584720.출근부 SET 퇴근시간 = '@end' WHERE 사원번호 = '@num' AND 출근시간 LIKE '@start%';";
            query = query.Replace("@start", start);
            query = query.Replace("@num", num);
            query = query.Replace("@end", end_textBox.Text);

            DBManager.GetDBManager().SetQuery(query).ExecuteNonQuery();
        }

        private void show_work()
        {
            Regex regex = new Regex(@"\d{4}-(0[1-9]|1[012])-(0[1-9]|[12][0-9]|3[01]) (0[0-9]|1[0-9]|2[0-3]):(0[0-9]|[1-5][0-9]):(0[0-9]|[1-5][0-9])");
            if (String.IsNullOrEmpty(출근시간_comboBox.Text)) //출근 시간 값이 비어 있을 경우 예외 처리
            {
                MessageBox.Show("출근 시간을 선택해 주세요");
            }
            else if (!regex.IsMatch(end_textBox.Text))
            {
                MessageBox.Show("퇴근 시간 형식을 확인해주세요");
            }
            else
            {
                if(Convert.ToDateTime(출근시간_comboBox.Text) >= Convert.ToDateTime(end_textBox.Text))
                {
                    MessageBox.Show("출근시간과 퇴근시간이 올바르지 않습니다.");
                    return;
                }
                string[] words = 출근시간_comboBox.Text.Split(' '); // 출근 시간의 날짜 추출
                string totalTime = string.Empty;
                int cnt = 추가수당.getInstance().hourlyRate(num);
                send_EndTime(words[0]);

                double totalMoney = (Convert.ToInt32(Additional_Allowance.getInstance().normalWork(num, words[0])) / 60) * cnt; // 여기까지 일반 근무 수당

                totalMoney = totalMoney + (((Convert.ToInt32(Additional_Allowance.getInstance().nightWork(num, words[0])) / 60) * cnt) * 1.5); // 야간 근무 수당
                night_textBox.Text = Additional_Allowance.getInstance().nightWork(num, words[0]) + "분";

                int subNormal = Convert.ToInt32(Additional_Allowance.getInstance().betweenOvertimeWork(num, words[0])); // 당일 연장근무 시간

                if (check_holiday(words[0])) // 휴일근무 -> 근무 총 시간은 휴일근무
                {
                    holiday_textBox.Text = Additional_Allowance.getInstance().holidayWork(num, words[0]) + "분";
                    totalTime = Additional_Allowance.getInstance().holidayWork(num, words[0]);
                    totalMoney = totalMoney + (((Convert.ToInt32(Additional_Allowance.getInstance().holidayWork(num, words[0])) / 60) * cnt) * 0.5); ; // 일반 + 야간 + 당일 연장 + 휴일(일반과 야간이 있기에 휴일은 0.5배)
                }
                else // 휴일근무 X -> 근무 총 시간은 일반근무 + 야간근무 + 당일 연장근무
                {
                    holiday_textBox.Text = "0분";
                    totalTime = (Convert.ToInt32(Additional_Allowance.getInstance().normalWork(num, words[0])) + Convert.ToInt32(Additional_Allowance.getInstance().nightWork(num, words[0])) + Convert.ToInt32(Additional_Allowance.getInstance().betweenOvertimeWork(num, words[0]))).ToString();
                    totalMoney = totalMoney + ((subNormal / 60) * cnt); // 당일 연장 시간 추가 (계산은 밑에서)
                }

                int overTime = Convert.ToInt32(Additional_Allowance.getInstance().overtimeWork(num, words[0], totalTime)); // 연장근무 표기 (주 48시간 연장근무) /당일 연장근무는 위에 휴일에서 합산 완료

                overtime_textBox.Text = overTime.ToString() + "분"; // 연장근무 수당
                totalMoney = totalMoney + (((Convert.ToInt32(Additional_Allowance.getInstance().overtimeWork(num, words[0], totalTime)) / 60) * cnt) * 0.5); // 일반 + 야간 + 휴일 + 연장 (연장도 0.5배)

                total_Money_textBox.Text = totalMoney.ToString() + "원";
            }
        }

        private void select_button_Click(object sender, EventArgs e)
        {
            show_work();
        }

        private void 출근시간_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (출근시간_comboBox.SelectedIndex == -1) return;
            string query = $"SELECT 퇴근시간 FROM 출근부 WHERE 사원번호 = '{num}' AND 출근시간 = '{출근시간_comboBox.SelectedItem.ToString()}'";
            DBManager.GetDBManager().OpenConnection();
            MySqlDataReader reader = DBManager.GetDBManager().SetQuery(query).ExecuteReader();
            if (reader.Read())
            {
                end_textBox.Text = Convert.ToDateTime(reader["퇴근시간"]).ToString("yyyy-MM-dd HH:mm:ss");
            }
            DBManager.GetDBManager().CloseConnection();
        }
    }
}
