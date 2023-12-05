using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TeamProject_test_v1
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
            textBoxID.Text = Properties.Settings.Default.myID;
            textBoxPW.Text = Properties.Settings.Default.myPW;
            checkBoxRemember.Checked = Properties.Settings.Default.myCheck;
            panelLogin.Region = Region.FromHrgn(CreateRoundRectRgn(2, 2, panelLogin.Width, panelLogin.Height, 15, 15));
            buttonLogin.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, buttonLogin.Width, buttonLogin.Height, 15, 15));
        }

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]            //Dll임포트
        private static extern IntPtr CreateRoundRectRgn                            //파라미터
        (
            int nLeftRect,      // x-coordinate of upper-left corner
            int nTopRect,       // y-coordinate of upper-left corner
            int nRightRect,     // x-coordinate of lower-right corner
            int nBottomRect,    // y-coordinate of lower-right corner   
            int nWidthEllipse,  // height of ellipse
            int nHeightEllipse  // width of ellipse  

        );

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(textBoxID.Text))
            {
                MessageBox.Show("아이디를 입력해 주시기 바랍니다.");
                return;
            }
            if (String.IsNullOrEmpty(textBoxPW.Text))
            {
                MessageBox.Show("비밀번호를 입력해 주시기 바랍니다.");
                return;
            }

            string id = textBoxID.Text;
            string pw = security.getinstance_().getpassword(textBoxPW.Text);
            string query = $"SELECT * FROM 로그인 WHERE 사원번호 = '{id}' and 비밀번호 = '{pw}';" + $" SELECT 출근시간 FROM s5584720.출근부 WHERE 사원번호 = '{id}' AND 퇴근시간 IS NULL OR 사원번호 = '{id}' AND DATE(출근시간) = DATE(now());";
            DBManager.GetDBManager().OpenConnection();
            MySqlDataReader reader = DBManager.GetDBManager().SetQuery(query).ExecuteReader();
            if (reader.Read())
            {
                Properties.Settings.Default.myID = textBoxID.Text;
                Properties.Settings.Default.myPW = textBoxPW.Text;
                if (checkBoxRemember.Checked) //로그인 정보 저장
                {
                    Properties.Settings.Default.myCheck = true;
                    Properties.Settings.Default.Save();
                }

                //출근시간은 찍혀있지만 퇴근시간이 안 찍혀 있을 때 또는 오늘 출근시간이 안 찍혀 있을 때
                reader.NextResult();
                if (!reader.Read())
                {
                    DBManager.GetDBManager().CloseConnection();
                    query = $"INSERT INTO s5584720.출근부(사원번호, 출근시간, 결재여부) VALUES ('{id}', now(), 0)";
                    int result = DBManager.GetDBManager().SetQuery(query).ExecuteNonQuery();
                    if (result == -1)
                    {
                        MessageBox.Show("쿼리 실패");
                        return;
                    }
                }
                else { DBManager.GetDBManager().CloseConnection(); }          

                Hide();
                MainForm form1 = new MainForm(id);
                form1.ShowDialog();
                return;
            }
            else
            {
                MessageBox.Show("입력하신 아이디 또는 비밀번호가 틀립니다. \r 아이디와 비밀번호를 확인 후 다시 입력해 주시기 바랍니다.");
            }
            DBManager.GetDBManager().CloseConnection();
        }

        private void LoginForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (!checkBoxRemember.Checked)
            {
                Properties.Settings.Default.myID = string.Empty;
                Properties.Settings.Default.myPW = string.Empty;
                Properties.Settings.Default.myCheck = false;
                Properties.Settings.Default.Save();
            }
            Application.Exit();
        }

        private void checkBoxRemember_CheckedChanged(object sender, EventArgs e)
        {
            if (!checkBoxRemember.Checked)
            {
                Properties.Settings.Default.myID = string.Empty;
                Properties.Settings.Default.myPW = string.Empty;
                Properties.Settings.Default.myCheck = false;
                Properties.Settings.Default.Save();
            }
        }
    }
}
