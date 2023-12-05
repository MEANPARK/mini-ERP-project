using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamProject_test_v1
{
    internal class 사용자매니저
    {
        private string 사원_이름;
        private string 사원_번호;
        private string 부서코드;
        private string 직급;
        private string 직급번호;

        private static 사용자매니저 instance = new 사용자매니저();
        public static 사용자매니저 GetInstance() { return instance; }
        private 사용자매니저() { }
        public void Set_사용자매니저(string 사원_이름,string 사원_번호, string 부서코드, string 직급, string 직급번호)
        {
            this.사원_이름 = 사원_이름;
            this.사원_번호 = 사원_번호;
            this.부서코드 = 부서코드;
            this.직급 = 직급;
            this.직급번호 = 직급번호;
        }
        public string Get_사원이름() { return 사원_이름; }
        public string Get_사원번호() { return 사원_번호; }
        public string Get_부서코드() { return 부서코드; }
        public string Get_직급() { return 직급; }
        public string Get_직급번호() { return 직급번호; }
    }
}
