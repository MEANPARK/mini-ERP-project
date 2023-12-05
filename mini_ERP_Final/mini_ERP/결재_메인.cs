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
    public partial class 결재_메인 : Form
    {
        private Dictionary<string, string> 단계1_결재자;
        private Dictionary<string, string> 단계2_결재자;
        private Dictionary<string, string> 업무_리스트;

        public 결재_메인()
        {
            InitializeComponent();
            결재_메인_Load();
        }

        private void 결재_메인_Load()
        {
            단계1_결재자 = 단계결재자매니저.GetInstance().Get_결재자리스트(1);
            단계2_결재자 = 단계결재자매니저.GetInstance().Get_결재자리스트(2);
            업무_리스트 = 업무매니저.GetInstance().Get_업무리스트();
            Set_ComboBoxes();
            if (사용자매니저.GetInstance().Get_직급() != "일반사원")
            {
                조회_단계1_결재자_코맨트_텍스트박스.ReadOnly = false;
                조회_1단계_결재자코맨트_라벨.Text = "1단계 결재자 코맨트";
            }
            결재_등록_내역_리스트_콤보박스.SelectedIndex = 0;
            결재리스트_선택_콤보박스.SelectedIndex = 0;
        }

        private void Set_ComboBoxes()
        {
            관련_업무_콤보박스.Items.Clear();
            결재자1_콤보박스.Items.Clear();
            결재자2_콤보박스.Items.Clear();
            foreach (string 업무 in 업무_리스트.Values)
            {
                관련_업무_콤보박스.Items.Add(업무);
            }
            foreach (string 결재자1 in 단계1_결재자.Values)
            {
                결재자1_콤보박스.Items.Add(결재자1);
            }
            foreach (string 결재자2 in 단계2_결재자.Values)
            {
                결재자2_콤보박스.Items.Add(결재자2);
            }
        }

        private string Get_Key(Dictionary<string, string> dic, string value)
        {
            string result = "";
            foreach (KeyValuePair<string, string> item in dic)
            {
                if (item.Value == value)
                {
                    result = item.Key;
                    break;
                }
            }
            return result;
        }

        private void 결재_텍스트박스들_초기화()
        {
            결재_제목_텍스트박스.Text = "";
            관련_업무_콤보박스.Text = "";
            결재자1_콤보박스.Text = "";
            결재자2_콤보박스.Text = "";
            등록_결재_상세정보_텍스트박스.Text = "";
            조회_단계1_결재자_코맨트_텍스트박스.Text = "";
            조회_2단계_결재자_코맨트_텍스트박스.Text = "";

            상세정보_제목_텍스트박스.Text = "";
            관련_업무_텍스트박스.Text = "";
            결재자1_텍스트박스.Text = "";
            결재자2_텍스트박스.Text = "";
            결재_코맨트_텍스트박스.Text = "";
            승인_반려_1단계_결재자_코맨트_텍스트박스.Text = "";
            승인_반려_2단계_결재자_코맨트_텍스트박스.Text = "";

            // 행의 id null 설정
            그리드뷰_셸매니저.GetInstance().Set_행_결재id();

            결재_조회_상세정보창.Set_상세정보창();
            승인_반려_상세정보창.Set_상세정보창();
        }

        // ================= 결재 조회 =======================

        private void 불러오기_버튼_Click(object sender, EventArgs e)
        {
            if (결재_등록_내역_리스트_콤보박스.Text == "")
            {
                MessageBox.Show("범위를 선택해주세요.");
                return;
            }
            결재_텍스트박스들_초기화();
            등록관리매니저.GetInstance().범위설정(결재_등록_내역_리스트_콤보박스.Text);
            결재_등록_내역_그리드뷰.DataSource = 등록관리매니저.GetInstance().등록내역_불러오기();

        }



        private void 삭제_버튼_Click(object sender, EventArgs e)
        {
            if (그리드뷰_셸매니저.GetInstance().Get_결재id() == null) MessageBox.Show("결재를 선택해 주세요.");
            else
            {
                if (등록관리매니저.GetInstance().결재_삭제(그리드뷰_셸매니저.GetInstance().Get_결재id()))
                {
                    결재_등록_내역_그리드뷰.DataSource = 등록관리매니저.GetInstance().등록내역_불러오기();
                    결재_텍스트박스들_초기화();
                }
                else
                {
                    MessageBox.Show("결재를 삭제할 수 없습니다.");
                }
            }
        }

        private void 등록_버튼_Click(object sender, EventArgs e)
        {
            if (결재자1_콤보박스.Text == "" || 결재자2_콤보박스.Text == "" || 결재_제목_텍스트박스.Text == "" || 관련_업무_콤보박스.Text == "")
            {
                MessageBox.Show("필수 정보를 입력해주세요");
                return;
            }

            if (등록관리매니저.GetInstance().결재_존재(그리드뷰_셸매니저.GetInstance().Get_결재id()))
            {
                MessageBox.Show("이미 등록된 결재입니다.\n같은 정보로 작성을 원하시면 삭제 후 시도해주세요.");
                return;
            }

            if (!등록관리매니저.GetInstance().결재_등록(
                결재_제목_텍스트박스.Text,
                관련_업무_콤보박스.Text,
                Get_Key(업무_리스트, 관련_업무_콤보박스.Text),
                Get_Key(단계1_결재자, 결재자1_콤보박스.Text),
                Get_Key(단계2_결재자, 결재자2_콤보박스.Text),
                등록_결재_상세정보_텍스트박스.Text,
                조회_단계1_결재자_코맨트_텍스트박스.Text))
            {
                MessageBox.Show("결재등록에 실패했습니다.");
                return;
            }

            MessageBox.Show("결재가 등록되었습니다.");

            결재_등록_내역_리스트_콤보박스.Text = "결재 중";
            등록관리매니저.GetInstance().범위설정(결재_등록_내역_리스트_콤보박스.Text);
            결재_등록_내역_그리드뷰.DataSource = 등록관리매니저.GetInstance().등록내역_불러오기();
            결재_텍스트박스들_초기화();
        }

        private void 결재_내역_그리드뷰_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // 선택된 행과 행의 id를 설정
            그리드뷰_셸매니저.GetInstance().Set_행_결재id(결재_등록_내역_그리드뷰, sender, e);

            // 행의 정보를 UI에 띄워줌
            등록관리매니저.GetInstance().박스설정(
                그리드뷰_셸매니저.GetInstance().Get_행(),
                결재_제목_텍스트박스, 관련_업무_콤보박스, 결재자1_콤보박스, 결재자2_콤보박스,
                등록_결재_상세정보_텍스트박스, 조회_단계1_결재자_코맨트_텍스트박스, 조회_2단계_결재자_코맨트_텍스트박스
                );

            결재_조회_상세정보창.Set_상세정보창(그리드뷰_셸매니저.GetInstance().Get_행());
        }


        private void 초기화_버튼_Click(object sender, EventArgs e)
        {
            결재_텍스트박스들_초기화();
        }

        // ================ 결재해야 할 리스트 ====================



        private void 승인_반려_불러오기_버튼_Click(object sender, EventArgs e)
        {
            if (결재리스트_선택_콤보박스.Text == "")
            {
                MessageBox.Show("범위를 선택해주세요.");
                return;
            }
            if (사용자매니저.GetInstance().Get_직급() == "일반사원")
            {
                MessageBox.Show("일반사원은 사용할 수 없습니다.");
                return;
            }
            결재해야할_내역_그리드뷰.DataSource = 승인반려매니저.GetInstance().결재해야할_내역_불러오기(결재리스트_선택_콤보박스.Text);
            승인반려매니저.GetInstance().Set_리스트_상태(결재리스트_선택_콤보박스.Text);
            결재_텍스트박스들_초기화();
        }


        private void 결재해야할_내역_그리드뷰_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // 승인반려매니저에서 행 정보를 그리드뷰_셸매니저에서 받아옴.
            // 그러므로 설정을 해줘야 함.
            그리드뷰_셸매니저.GetInstance().Set_행_결재id(결재해야할_내역_그리드뷰, sender, e);
            승인반려매니저.GetInstance().텍스트박스설정(
                그리드뷰_셸매니저.GetInstance().Get_행(), 상세정보_제목_텍스트박스, 관련_업무_텍스트박스, 결재자1_텍스트박스,
                결재자2_텍스트박스, 결재_코맨트_텍스트박스, 승인_반려_1단계_결재자_코맨트_텍스트박스, 승인_반려_2단계_결재자_코맨트_텍스트박스
                );

            승인_반려_상세정보창.Set_상세정보창(그리드뷰_셸매니저.GetInstance().Get_행());
        }

        private void 승인_버튼_Click(object sender, EventArgs e)
        {
            if (그리드뷰_셸매니저.GetInstance().Get_결재id() == null)
            {
                MessageBox.Show("결재를 선택해주세요.");
                return;
            }

            if (승인반려매니저.GetInstance().Get_리스트_상태() == "결재 대기")
            {
                코맨트창 승인 = new 코맨트창(결재해야할_내역_그리드뷰, 결재리스트_선택_콤보박스.Text, "승인");
                승인.ShowDialog();
                결재_텍스트박스들_초기화();
            }
            else
            {
                MessageBox.Show("결재 대기 리스트에서 선택해 주세요.");
            }
        }

        private void 반려_버튼_Click(object sender, EventArgs e)
        {
            if (그리드뷰_셸매니저.GetInstance().Get_결재id() == null)
            {
                MessageBox.Show("결재를 선택해주세요.");
                return;
            }
            if (승인반려매니저.GetInstance().Get_리스트_상태() == "결재 대기")
            {
                코맨트창 반려 = new 코맨트창(결재해야할_내역_그리드뷰, 결재리스트_선택_콤보박스.Text, "반려");
                반려.ShowDialog();
                결재_텍스트박스들_초기화();
            }
            else
            {
                MessageBox.Show("결재 대기 리스트에서 선택해 주세요.");
            }
        }
    }
}


