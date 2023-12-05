using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TeamProject_test_v1
{
    // 선택된 행의 id와 행을 제공해주는 싱글톤 패턴의 클래스
    internal class 그리드뷰_셸매니저
    {
        private static 그리드뷰_셸매니저 instance = new 그리드뷰_셸매니저();
        private string 결재_id = null;
        private DataGridViewRow 행 = null;
        public static 그리드뷰_셸매니저 GetInstance() { return instance; }
        private 그리드뷰_셸매니저() { }

        public void Set_행_결재id(DataGridView dataGridView, object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int rowIndex = e.RowIndex;

                if (rowIndex < 0) 
                {
                    결재_id = null;
                    행 = null;
                    return;
                }
                
                // 클릭된 셀의 행 얻기
                결재_id = dataGridView.Rows[rowIndex].Cells["결재_id"].Value.ToString();
                if (결재_id == "") 결재_id = null;
                행 = dataGridView.Rows[rowIndex];

            } catch (Exception ex)
            {
                MessageBox.Show("Set_행_결재_index in 그리드뷰_셸매니저\n" + ex.Message);
                결재_id = null;
                행 = null;
                return;
            }         
        }

        // 행의 설정을 null로 초기화
        public void Set_행_결재id()
        {
            행 = null;
            결재_id = null;
        }
        public string Get_결재id() { return 결재_id; }
        public DataGridViewRow Get_행() { return 행; }
    }
}
