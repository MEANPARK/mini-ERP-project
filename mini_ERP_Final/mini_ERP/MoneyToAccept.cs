using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamProject_test_v1
{
    internal class MoneyToAccept
    {
        public MoneyToAccept() { }
        private static MoneyToAccept ma = new MoneyToAccept();
        public static MoneyToAccept getInstance() { return ma; }

        private string ChooseHeader(string num)
        {
            string query = "SELECT 부서번호 FROM s5584720.사원 where 사원번호 = '@num';";
            query = query.Replace("@num", num);

            string department = Additional_Allowance.getInstance().sendQuery(query);

            query = "SELECT 부서장_id FROM s5584720.부서 where 부서코드 = '@dep';";
            query = query.Replace("@dep", department);
            return Additional_Allowance.getInstance().sendQuery(query);
        }

        public void SendAdditionalAllowance(string num, string money)
        {
            if (!등록관리매니저.GetInstance().결재_등록(
                "추가 수당 신청",
                "추가 수당 신청",
                "999", // 이부분 외래키 문제
                ChooseHeader(num), // 자기 부서 받아서 부서장 사원번호 제공
                "00000000", // 2단계 결재자 사장으로 고정 (사유 : 월급 제공자)
                money, // 돈 계산 로직 쿼리로 넘겨주기
                "null"))
            {
                MessageBox.Show("결재등록에 실패했습니다.");
                return;
            }

            MessageBox.Show("결재가 등록되었습니다.");
        }
    }
}
