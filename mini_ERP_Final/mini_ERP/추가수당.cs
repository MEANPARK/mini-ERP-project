using Google.Protobuf.WellKnownTypes;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamProject_test_v1
{
    internal class 추가수당
    {
        public 추가수당() { }

        private static 추가수당 am = new 추가수당();

        public static 추가수당 getInstance() { return am; }

        private string lastMonth() // 지난 달 확인 (다음달 10일에 그 전달의 월급 지불)
        {
            string query = "SELECT DATE_FORMAT(DATE_SUB(NOW(), INTERVAL 1 MONTH), '%m') AS last_month;";
            return Additional_Allowance.getInstance().sendQuery(query);
        }

        private string month() // 현재달 확인(기록시 저장용)
        {
            string query = "SELECT DATE_FORMAT(NOW(), '%Y-%m');";
            return Additional_Allowance.getInstance().sendQuery(query);
        }

        public int hourlyRate(string num)
        {
            string query = "SELECT s5584720.직급.시급 FROM s5584720.직급 INNER join s5584720.사원 ON s5584720.직급.직급번호 = s5584720.사원.직급번호 where s5584720.사원.사원번호 = '@num';";
            query = query.Replace("@num", num);
            return Convert.ToInt32(Additional_Allowance.getInstance().sendQuery(query));
        }

        private int totalNormal(string num) // 통상 노동시간
        {
            string query = "SELECT sum(일반근로시간) FROM s5584720.출근부 WHERE MONTH(출근시간) = @month AND 사원번호 = '@num';";
            query = query.Replace("@num", num);
            query = query.Replace("@month", lastMonth());

            int result = Convert.ToInt32(Additional_Allowance.getInstance().sendQuery(query)) / 60; // 단위는 시간 (시급으로 계산하기 때문에)

            if (result % 60 >= 40) // 만약 40분을 넘게 일했다면 1시간으로 쳐줌
            {
                result = result + 1;
            }

            return result;
        }

        private string totalNight(string num) // 야간 노동시간
        {
            string query = "SELECT sum(야간근로시간) FROM s5584720.출근부 WHERE MONTH(출근시간) = 11 AND 사원번호 = '00000001' GROUP BY 일반근로시간;";
            query = query.Replace("@num", num);
            query = query.Replace("@month", lastMonth());

            DataTable dt = new DataTable();
            dt = Additional_Allowance.getInstance().sendQuery_overTime(query);

            int holly = Convert.ToInt32(dt.Rows[0][0].ToString());
            int normal = Convert.ToInt32(dt.Rows[1][0].ToString());
            int cnt = hourlyRate(num);

            holly = holly / 60; // 휴일 야간 근로는 휴일 시간에 통합되기 때문에 50% 가산만 계산
            if (holly % 60 >= 40) // 만약 40분을 넘게 일했다면 1시간으로 쳐줌
            {
                holly = holly + 1;
            }

            normal = normal / 60; // 평일 야간 근로는 별도로 계산되기 때문에 50% 가산을 더하여 150%를 계산
            if (normal % 60 >= 40) // 만약 40분을 넘게 일했다면 1시간으로 쳐줌
            {
                normal = normal + 1;
            }

            double result = (holly * (cnt / 2)) + (normal * (cnt * 1.5));

            return result.ToString();
        }

        private int totalLongWork(string num) // 연장 노동시간
        {
            string query = "SELECT sum(연장근로시간) FROM s5584720.출근부 WHERE MONTH(출근시간) = @month AND 사원번호 = '@num';";
            query = query.Replace("@num", num);
            query = query.Replace("@month", lastMonth());

            int result = Convert.ToInt32(Additional_Allowance.getInstance().sendQuery(query)) / 60; // 단위는 시간 (시급으로 계산하기 때문에)

            if (result % 60 >= 40) // 만약 40분을 넘게 일했다면 1시간으로 쳐줌
            {
                result = result + 1;
            }

            return result;
        }

        private int totalholiy(string num) // 연장 노동시간
        {
            string query = "SELECT sum(연장근로시간) FROM s5584720.출근부 WHERE MONTH(출근시간) = @month AND 사원번호 = '@num';";
            query = query.Replace("@num", num);
            query = query.Replace("@month", lastMonth());

            int result = Convert.ToInt32(Additional_Allowance.getInstance().sendQuery(query)) / 60; // 단위는 시간 (시급으로 계산하기 때문에)

            if (result % 60 >= 40) // 만약 40분을 넘게 일했다면 1시간으로 쳐줌
            {
                result = result + 1;
            }

            return result;
        }

        public void moneyTable(string num)
        {
            int money = hourlyRate(num); // 시급
            int half = money / 2;
            int normal = (money * totalNormal(num)); // 기본급
            int night = Convert.ToInt32(totalNight(num)); // 야간근로수당
            int longWork = (half * totalLongWork(num)); // 연장근로수당
            int holiy = ((money + half) * totalholiy(num)); // 휴일근로수당

            double total_work = Convert.ToDouble(normal) + Convert.ToDouble(night) + Convert.ToDouble(longWork) + Convert.ToDouble(holiy); // 총 금액 (실 수령액 X)

            int NP = Convert.ToInt32((total_work / 100) * 3.5); // 국민연금 (소득의 9%, 단 업주랑 분할)
            int NHI = Convert.ToInt32((total_work / 100) * 3.545);  // 건강보험료 (소득의 7.09%, 단 업주랑 분할)
            int LCNHI = Convert.ToInt32((total_work / 100) * 0.4541);  // 건강보험료(장기) (소득의 0.9082%, 단 업주랑 분할)
            int EIP = Convert.ToInt32((total_work / 100) * 0.9);  // 고용보험 (소득의 7.09%, 단 업주랑 분할)

            double total = total_work - NP - NHI - LCNHI - EIP;


            // 단위 정렬 (만단위)
            normal = normal / 10000;
            night = night / 10000;
            longWork = longWork / 10000;
            holiy = holiy / 10000;
            NP = NP / 10000;
            NHI = NHI / 10000;
            LCNHI = LCNHI / 10000;
            EIP = EIP / 10000;
            total = total / 10000;


            string query = $"DELETE FROM s5584720.급여내역 WHERE 사원번호 = '{num}' and 지급날짜 = '{month()}-10 00:00:00'";
            DBManager.GetDBManager().SetQuery(query).ExecuteNonQuery();

            query = "INSERT INTO `s5584720`.`급여내역` (`사원번호`, `기본급`, `야간근로수당`, `연장근로수당`, `휴일근로수당`, `국민연금`, `국민건강보험`, `장기요양국민건강보험`, `고용보험료`, `실수령액`, `지급날짜`) VALUES ('@num', '@money', '@night', '@longWork', '@holiy', '@NP', '@NHI', '@LCNHI', '@EIP', '@total', '@month-10 00:00:00');";
            query = query.Replace("@num", num);
            query = query.Replace("@money", normal.ToString());
            query = query.Replace("@night", night.ToString());
            query = query.Replace("@longWork", longWork.ToString());
            query = query.Replace("@holiy", holiy.ToString());
            query = query.Replace("@NP", NP.ToString());
            query = query.Replace("@NHI", NHI.ToString());
            query = query.Replace("@LCNHI", LCNHI.ToString());
            query = query.Replace("@EIP", EIP.ToString());
            query = query.Replace("@total", total.ToString());
            query = query.Replace("@month", month());

            DBManager.GetDBManager().SetQuery(query).ExecuteNonQuery();
        }
    }
}
