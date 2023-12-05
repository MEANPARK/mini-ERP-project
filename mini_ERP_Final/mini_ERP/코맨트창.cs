using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TeamProject_test_v1
{
    public partial class 코맨트창 : Form
    {

        private DataGridView 결재해야할_내역_그리드뷰;
        private string 결재종류;
        private string 코맨트종류;

        public 코맨트창(DataGridView 결재해야할_내역_그리드뷰, string 결재종류, string 코맨트종류)
        {
            InitializeComponent();
            this.결재해야할_내역_그리드뷰 = 결재해야할_내역_그리드뷰;
            this.결재종류 = 결재종류;
            this.코맨트종류 = 코맨트종류;
            헤드라벨.Text = 코맨트종류 + "를 입력해주세요.";
        }


        private void 확인_버튼_Click(object sender, EventArgs e)
        {
            if(코맨트종류 == "반려")
            {
                if (승인반려매니저.GetInstance().결재_반려(그리드뷰_셸매니저.GetInstance().Get_결재id(), 코맨트_텍스트박스.Text))
                {
                    결재해야할_내역_그리드뷰.DataSource = 승인반려매니저.GetInstance().결재해야할_내역_불러오기(결재종류);
                }
                else
                {
                    MessageBox.Show("다시 시도해주세요.");
                }
            } else if(코맨트종류 == "승인")
            {
                if (승인반려매니저.GetInstance().결재_승인(그리드뷰_셸매니저.GetInstance().Get_결재id(), 코맨트_텍스트박스.Text))
                {
                    결재해야할_내역_그리드뷰.DataSource = 승인반려매니저.GetInstance().결재해야할_내역_불러오기(결재종류);
                }
                else
                {
                    MessageBox.Show("다시 시도해주세요.");
                }
            }
            this.Close();
        }

        private void 취소_버튼_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

