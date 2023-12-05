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
    public partial class 상세정보창 : UserControl
    {
        public 상세정보창()
        {
            InitializeComponent();
        }

        public void Set_상세정보창(DataGridViewRow 행)
        {
            if (행 == null) return;
            결재자1_이름_텍스트박스.Text = 행.Cells["1단계_결재자"].Value.ToString();
            결재자1_승인여부_텍스트박스.Text = 행.Cells["1단계결재자_승인여부"].Value.ToString();
            결재자1_코맨트_텍스트박스.Text = 행.Cells["1단계결재자_Comment"].Value.ToString();
            결재자1_결재일_텍스트박스.Text = 행.Cells["1단계결재자_결재시간"].Value.ToString();

            결재자2_이름_텍스트박스.Text = 행.Cells["2단계_결재자"].Value.ToString();
            결재자2_승인여부_텍스트박스.Text = 행.Cells["2단계결재자_승인여부"].Value.ToString();
            결재자2_코맨트_텍스트박스.Text = 행.Cells["2단계결재자_Comment"].Value.ToString();
            결재자2_결재일_텍스트박스.Text = 행.Cells["2단계결재자_결재시간"].Value.ToString();

            기안자_텍스트박스.Text = 행.Cells["기안자"].Value.ToString();
            결재제목_텍스트박스.Text = 행.Cells["결재_title"].Value.ToString();
            최종_텍스트박스.Text = 행.Cells["최종승인여부"].Value.ToString();
        }
        public void Set_상세정보창()
        {
            결재자1_이름_텍스트박스.Text = "";
            결재자1_승인여부_텍스트박스.Text = "";
            결재자1_코맨트_텍스트박스.Text = "";
            결재자1_결재일_텍스트박스.Text = "";

            결재자2_이름_텍스트박스.Text = "";
            결재자2_승인여부_텍스트박스.Text = "";
            결재자2_코맨트_텍스트박스.Text = "";
            결재자2_결재일_텍스트박스.Text = "";

            기안자_텍스트박스.Text = "";
            결재제목_텍스트박스.Text = "";
            최종_텍스트박스.Text = "";
        }
    }
}
