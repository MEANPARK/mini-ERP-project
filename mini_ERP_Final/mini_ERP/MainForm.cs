using MySql.Data.MySqlClient;
using System.Timers;

namespace TeamProject_test_v1
{
    public partial class MainForm : Form
    {
        EmployeeInfo employeeInfo;
        private System.Timers.Timer timer;

        public MainForm()
        {
            InitializeComponent();
            this.MaximizeBox = false;
            SetTreeViewData();
            employeeInfo = new EmployeeInfo();
        }

        public MainForm(string username)
        {
            InitializeComponent();
            this.MaximizeBox = false;
            SetTreeViewData();
            employeeInfo = new EmployeeInfo();
            employeeInfo = employeeInfo.setEmployeeInfo(username);
            사용자매니저.GetInstance().Set_사용자매니저(employeeInfo.이름, employeeInfo.사원번호, employeeInfo.부서번호.ToString(), employeeInfo.직급, employeeInfo.직급번호.ToString());
            //setEmployee(username);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            showEmployeeInfo();
            퇴근여부체크();
            RealTimeMailManager mailManager = RealTimeMailManager.GetTimer();
            timerSetting();
            if (사용자매니저.GetInstance().Get_부서코드() == "-1")
            {
                button퇴근.Enabled = false;
            }
        }

        private void timerSetting()
        {
            timer = new System.Timers.Timer();
            timer.Elapsed += checkUnreadMail;
            timer.AutoReset = true;
            timer.Enabled = true;
        }

        //안읽은 쪽지 있는 경우 NEW, 쪽지(안읽은 개수), 상단에 ~에게 쪽지 왔다고 알림 띄우는 타이머
        private void checkUnreadMail(Object source, ElapsedEventArgs e)
        {
            try
            {
                this.Invoke((MethodInvoker)delegate
                {
                    Label notificationLabel = panel2.Controls["mailNotification_Label"] as Label;
                    bool newMailStatus = RealTimeMailManager.GetTimer().getNewMailStatus();
                    if (newMailStatus)
                    {
                        notificationLabel.Text = RealTimeMailManager.GetTimer().getNotificationMessage();
                        button쪽지.Text = "쪽지(" + RealTimeMailManager.GetTimer().getUnreadMailNum() + ")";
                        안읽은쪽지여부_라벨.Visible = true;
                    }
                    else
                    {
                        button쪽지.Text = "쪽지";
                        안읽은쪽지여부_라벨.Visible = false;
                    }
                });
            }
            catch { }
        }

        /// <summary>
        /// 로그아웃 버튼 옆에 로그인한 직원 정보 라벨 텍스트 변환시키는 함수
        /// </summary>
        private void showEmployeeInfo()
        {
            labelName.Text = employeeInfo.ToString();
        }

        public void SetTreeViewData()
        {
            TreeNode workNode = new TreeNode("업무관리", 0, 0);
            workNode.Nodes.Add("WS", "업무검색", 0, 0);

            TreeNode attNode = new TreeNode("출퇴근관리", 0, 0);
            attNode.Nodes.Add("AL", "출퇴근기록", 0, 0);
            attNode.Nodes.Add("AC", "출퇴근현황", 0, 0);

            TreeNode hrNode = new TreeNode("인사관리", 0, 0);
            hrNode.Nodes.Add("ES", "사원검색", 0, 0);
            hrNode.Nodes.Add("ER", "사원등록", 0, 0);

            TreeNode dmNode = new TreeNode("부서관리", 0, 0);
            dmNode.Nodes.Add("DR", "부서등록", 0, 0);

            TreeNode payNode = new TreeNode("결재관리", 0, 0);
            payNode.Nodes.Add("NP", "결재조회", 0, 0);

            TreeNode accountNode = new TreeNode("급여관리", 0, 0);
            accountNode.Nodes.Add("ST", "급여대장", 0, 0);

            this.treeViewMenu.Nodes.Add(workNode);
            this.treeViewMenu.Nodes.Add(attNode);
            this.treeViewMenu.Nodes.Add(hrNode);
            this.treeViewMenu.Nodes.Add(dmNode);
            this.treeViewMenu.Nodes.Add(payNode);
            this.treeViewMenu.Nodes.Add(accountNode);
            this.treeViewMenu.ItemHeight = 30;
        }

        private void treeViewMenu_AfterSelect(object sender, TreeViewEventArgs e)
        {
            string nodeKey = e.Node.Name;
            if (!string.IsNullOrEmpty(nodeKey))
            {
                if (사용자매니저.GetInstance().Get_부서코드() == "-1")
                {
                    MessageBox.Show("현재 등록된 부서가 없습니다.\n새로운 부서가 발령될 때까지 기다려주세요.");
                    return;
                }
                button쪽지.Enabled = true;
                treeViewMenu.SelectedNode.NodeFont = new Font(treeViewMenu.Font, FontStyle.Bold);
                //MessageBox.Show("선택된 노드 키: " + nodeKey);
                if (nodeKey == "WS")
                {
                    //ChildForm1 childForm1 = new ChildForm1("name");
                    업무 workForm = null;
                    if (this.ActiveMdiChild != null)
                    {
                        if (this.ActiveMdiChild != workForm)
                        {
                            this.ActiveMdiChild.Close();
                        }
                        workForm = ShowActiveForm(workForm, typeof(업무)) as 업무;
                    }
                    else
                    {
                        workForm = ShowActiveForm(workForm, typeof(업무)) as 업무;
                    }
                }

                if (nodeKey == "AL")
                {
                    출퇴근기록 alForm = null;
                    if (this.ActiveMdiChild != null)
                    {
                        if (this.ActiveMdiChild != alForm)
                        {
                            this.ActiveMdiChild.Close();
                        }
                        alForm = ShowActiveForm(alForm, typeof(출퇴근기록)) as 출퇴근기록;
                    }
                    else
                    {
                        alForm = ShowActiveForm(alForm, typeof(출퇴근기록)) as 출퇴근기록;
                    }
                }

                if (nodeKey == "AC")
                {
                    if (employeeInfo.직급 != "사장")
                    {
                        MessageBox.Show("권한이 없습니다.");
                        treeViewMenu.SelectedNode.NodeFont = null;
                        treeViewMenu.SelectedNode = null;
                        return;
                    }
                    출퇴근현황 acForm = null;
                    if (this.ActiveMdiChild != null)
                    {
                        if (this.ActiveMdiChild != acForm)
                        {
                            this.ActiveMdiChild.Close();
                        }
                        acForm = ShowActiveForm(acForm, typeof(출퇴근현황)) as 출퇴근현황;
                    }
                    else
                    {
                        acForm = ShowActiveForm(acForm, typeof(출퇴근현황)) as 출퇴근현황;
                    }
                }

                if (nodeKey == "ES")
                {
                    //ChildForm1 childForm1 = new ChildForm1("name");
                    EmployeeSearchForm employeeSearchForm = null;
                    if (this.ActiveMdiChild != null)
                    {
                        if (this.ActiveMdiChild != employeeSearchForm)
                        {
                            this.ActiveMdiChild.Close();
                        }
                        employeeSearchForm = ShowActiveForm(employeeSearchForm, typeof(EmployeeSearchForm)) as EmployeeSearchForm;
                    }
                    else
                    {
                        employeeSearchForm = ShowActiveForm(employeeSearchForm, typeof(EmployeeSearchForm)) as EmployeeSearchForm;
                    }
                }

                if (nodeKey == "ER")
                {
                    if (employeeInfo.직급 != "부서장" && employeeInfo.직급 != "사장")
                    {
                        MessageBox.Show("권한이 없습니다.");
                        treeViewMenu.SelectedNode.NodeFont = null;
                        treeViewMenu.SelectedNode = null;
                        return;
                    }
                    EmployeeAddForm employeeAddForm = null;
                    if (this.ActiveMdiChild != null)
                    {
                        if (this.ActiveMdiChild != employeeAddForm)
                        {
                            this.ActiveMdiChild.Close();
                        }
                        employeeAddForm = ShowActiveForm(employeeAddForm, typeof(EmployeeAddForm)) as EmployeeAddForm;
                    }
                    else
                    {
                        employeeAddForm = ShowActiveForm(employeeAddForm, typeof(EmployeeAddForm)) as EmployeeAddForm;
                    }
                }

                if (nodeKey == "DR")
                {
                    부서목록 dmForm = null;
                    if (this.ActiveMdiChild != null)
                    {
                        if (this.ActiveMdiChild != dmForm)
                        {
                            this.ActiveMdiChild.Close();
                        }
                        dmForm = ShowActiveForm(dmForm, typeof(부서목록)) as 부서목록;
                    }
                    else
                    {
                        dmForm = ShowActiveForm(dmForm, typeof(부서목록)) as 부서목록;
                    }
                }

                if (nodeKey == "NP")
                {
                    결재_메인 npForm = null;
                    if (this.ActiveMdiChild != null)
                    {
                        if (this.ActiveMdiChild != npForm)
                        {
                            this.ActiveMdiChild.Close();
                        }
                        npForm = ShowActiveForm(npForm, typeof(결재_메인)) as 결재_메인;
                    }
                    else
                    {
                        npForm = ShowActiveForm(npForm, typeof(결재_메인)) as 결재_메인;
                    }
                }

                if (nodeKey == "ST")
                {
                    if (employeeInfo.직급 != "사장")
                    {
                        MessageBox.Show("권한이 없습니다.");
                        treeViewMenu.SelectedNode.NodeFont = null;
                        treeViewMenu.SelectedNode = null;
                        return;
                    }
                    급여대장 stForm = null;
                    if (this.ActiveMdiChild != null)
                    {
                        if (this.ActiveMdiChild != stForm)
                        {
                            this.ActiveMdiChild.Close();
                        }
                        stForm = ShowActiveForm(stForm, typeof(급여대장)) as 급여대장;
                    }
                    else
                    {
                        stForm = ShowActiveForm(stForm, typeof(급여대장)) as 급여대장;
                    }
                }
            }
        }

        private void treeViewMenu_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            if (treeViewMenu.SelectedNode != null)
                treeViewMenu.SelectedNode.NodeFont = new Font(treeViewMenu.Font, FontStyle.Regular);

        }

        private Form ShowActiveForm(Form form, Type type)
        {
            if (form == null)
            {
                Point parentPoint = this.Location;
                form = (Form)Activator.CreateInstance(type);
                form.MdiParent = this;
                form.StartPosition = FormStartPosition.Manual;
                form.Location = new Point(146, 50);
                form.Show();
            }
            else
            {
                if (form.IsDisposed)
                {
                    Point parentPoint = this.Location;
                    form = (Form)Activator.CreateInstance(type);
                    form.MdiParent = this;
                    form.StartPosition = FormStartPosition.Manual;
                    form.Location = new Point(146, 50);
                    form.Show();
                }
                else
                {
                    form.Activate();
                }
            }
            return form;
        }

        //로그아웃 버튼을 눌렀는지 윈폼의 x버튼을 눌렀는지 판단하는 변수 (0 = 윈폼 / 1 = 로그아웃)
        private int check = 0;
        private void buttonLogout_Click(object sender, EventArgs e)
        {
            if (!퇴근여부체크() && 사용자매니저.GetInstance().Get_부서코드() != "-1")
            {
                var confirmResult = MessageBox.Show("퇴근시간이 입력되지 않았습니다. \n퇴근이라면 퇴근 버튼을 클릭해주세요.\n로그아웃 하시겠습니까?", "확인창", MessageBoxButtons.YesNo);
                if (confirmResult == DialogResult.Yes)
                {
                    timer.Stop();
                    timer.Dispose();
                    LoginForm loginForm = new LoginForm();
                    loginForm.Show();
                    check = 1;
                    this.Close();
                }
                else return;
            }
            else
            {
                timer.Stop();
                timer.Dispose();
                LoginForm loginForm = new LoginForm();
                loginForm.Show();
                check = 1;
                this.Close();
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (check == 0)
            {
                if (!퇴근여부체크() && 사용자매니저.GetInstance().Get_부서코드() != "-1")
                {
                    var confirmResult1 = MessageBox.Show("퇴근시간이 입력되지 않았습니다. \n퇴근이라면 퇴근 버튼을 클릭해주세요.\n종료 하시겠습니까?", "확인창", MessageBoxButtons.YesNo);
                    if (confirmResult1 == DialogResult.Yes)
                    {
                        timer.Stop();
                        timer.Dispose(); //추가
                        check = 1;
                        Application.Exit();
                    }
                    else
                    {
                        e.Cancel = true;
                        return;
                    }
                }

                var confirmResult2 = MessageBox.Show("종료하시겠습니까?", "확인창", MessageBoxButtons.YesNo);
                if (confirmResult2 == DialogResult.Yes)
                {
                    timer.Stop();
                    timer.Dispose(); //추가
                    check = 1;
                    Application.Exit();
                }
                else
                {
                    e.Cancel = true;
                    return;
                }
            }
        }

        private void button쪽지_Click(object sender, EventArgs e)
        {
            if (사용자매니저.GetInstance().Get_부서코드() == "-1")
            {
                MessageBox.Show("현재 등록된 부서가 없습니다.\n새로운 부서가 발령될 때까지 기다려주세요.");
                return;
            }
            if (treeViewMenu.SelectedNode != null) { treeViewMenu.SelectedNode.NodeFont = null; }
            treeViewMenu.SelectedNode = null;

            쪽지 pmForm = null;
            if (this.ActiveMdiChild != null)
            {
                if (this.ActiveMdiChild != pmForm)
                {
                    this.ActiveMdiChild.Close();
                }
                pmForm = ShowActiveForm(pmForm, typeof(쪽지)) as 쪽지;
            }
            else
            {
                pmForm = ShowActiveForm(pmForm, typeof(쪽지)) as 쪽지;
            }
            button쪽지.Enabled = false;
        }

        private void button급여_Click(object sender, EventArgs e)
        {
            if (사용자매니저.GetInstance().Get_부서코드() == "-1")
            {
                MessageBox.Show("현재 등록된 부서가 없습니다.\n새로운 부서가 발령될 때까지 기다려주세요.");
                return;
            }

            if (treeViewMenu.SelectedNode != null) { treeViewMenu.SelectedNode.NodeFont = null; }
            treeViewMenu.SelectedNode = null;

            salary 급여Form = new salary();
            급여Form.ShowDialog();
        }

        public bool 퇴근여부체크()
        {
            string query = $"SELECT 출근시간 FROM 출근부 WHERE 사원번호 = '{Properties.Settings.Default.myID}' AND 퇴근시간 IS NULL";
            DBManager.GetDBManager().OpenConnection();
            MySqlDataReader reader = DBManager.GetDBManager().SetQuery(query).ExecuteReader();
            if (!reader.Read())
            {
                button퇴근.Enabled = false;
                DBManager.GetDBManager().CloseConnection();
                return true;
            }
            else
            {
                DBManager.GetDBManager().CloseConnection();
                return false;
            }
        }

        private void button퇴근_Click(object sender, EventArgs e)
        {
            Additional_Allowance.getInstance().autoAccepte();
            퇴근여부체크();
        }
    }
}