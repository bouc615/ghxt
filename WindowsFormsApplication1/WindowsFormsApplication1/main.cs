using System;
using System.Data;
using System.Windows.Forms;
using System.Speech.Synthesis;

using SpeechLib;
using System.Speech.Recognition;
using System.Timers;

namespace WindowsFormsApplication1
{
    public partial class main : Form
    {
        public main()
        {
            InitializeComponent();
        }
        //窗体加载
        private void main_Load(object sender, EventArgs e)
        {
            day.Format = DateTimePickerFormat.Custom;
            day.CustomFormat = "yyyy-MM-dd";
        }
        //显示打印窗口
        private void button7_Click(object sender, EventArgs e)
        {

        }

        /****************查询添加病人信息***************************************/
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        string idcard;
        string nameV;
        string gender;
        string ageV;
        string adress;
        string phone;
        string socialNumber;
        string birthday;
        //查询病人
        private void query(string idValue)
        {
            String idText = idValue;

            if (idText == "")
            {
                MessageBox.Show("请输入有效的ID号");
                return;
            }
            string sql = "select * from patient where IDcard='" + idText + "\'";
            ds = sqlHelper.ExecuteDataSet(sql);
            dt = ds.Tables[0];
            //判断列是否为空
            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("空");
                return;
            }
            dataGrid.DataSource = dt;
            dataGrid.Columns[0].HeaderCell.Value = "ID";
            dataGrid.Columns[1].HeaderCell.Value = "姓名";
            dataGrid.Columns[2].HeaderCell.Value = "性别";
            dataGrid.Columns[3].HeaderCell.Value = "年龄";
            dataGrid.Columns[4].HeaderCell.Value = "地址";
            dataGrid.Columns[5].HeaderCell.Value = "电话";
            dataGrid.Columns[6].HeaderCell.Value = "过敏药物";
            dataGrid.Columns[7].HeaderCell.Value = "身份证号";
            dataGrid.Columns[8].HeaderCell.Value = "出生日期";
            dataGrid.Columns[9].HeaderCell.Value = "过往病史";
            idcard = dataGrid.Rows[0].Cells[0].Value.ToString();
            nameV = dataGrid.Rows[0].Cells[1].Value.ToString();
            gender = dataGrid.Rows[0].Cells[2].Value.ToString();
            ageV = dataGrid.Rows[0].Cells[3].Value.ToString();
            adress = dataGrid.Rows[0].Cells[4].Value.ToString();
            phone = dataGrid.Rows[0].Cells[5].Value.ToString();
            socialNumber = dataGrid.Rows[0].Cells[7].Value.ToString();
            birthday = dataGrid.Rows[0].Cells[8].Value.ToString();

            a.Text = idcard;
            b.Text = nameV;
            c.Text = gender;
            d.Text = ageV;
            dizhi.Text = adress;
            f.Text = phone;
            g.Text = socialNumber;
            h.Text = birthday;
            y.Text = idcard;
        }

        private void queryBtn_Click(object sender, EventArgs e)
        {
            query(id.Text.Trim());
        }
        //添加完后清空病人信息
        private void Pclear()
        {
            hosipitalCard.Clear();
            name.Clear();
            age.Clear();
            idCard.Clear();
            address.Clear();
            medicine.Clear();
            telphone.Clear();
            sex.Text = "";
        }
        //添加病人信息
        private void addBtn_Click(object sender, EventArgs e)
        {
            bool b1 = hosipitalCard.Text != "" && name.Text != "" && sex.Text != "" &&
                age.Text != "" && idCard.Text != "" && address.Text != "" &&
                medicine.Text != "" && telphone.Text != "" && birthDay.Text != "";//判断项是否为空         
            int i = 0;
            try
            {

                //这里写sql语句 ，ExecuteNoquery()会返回受影响的行数，为int类型
                string sql = " insert into patient (IDcard, Name, Gender, Age, SocialSecurityNumber, DataOfBirth, Allergicdrug, Phone, Adress) values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}')";
                string s = string.Format(sql, hosipitalCard.Text, name.Text, sex.Text, age.Text, idCard.Text, birthDay.Text, medicine.Text, telphone.Text, address.Text);
                i = sqlHelper.ExecuteNoQuery(s);
            }
            catch (Exception)
            {

            }
            if (i > 0)
            {
                MessageBox.Show("添加成功");
                query(hosipitalCard.Text.Trim());
                Pclear();
            }
            else
            {

                MessageBox.Show(hosipitalCard.Text);
            }
        }
        //自动生成病历号
        private void getIdCard()
        {
            string date = DateTime.Now.ToString("yyyyMMdd");
            Random random = new Random();
            string rs = Convert.ToString(random.Next(100));
            hosipitalCard.Text = date + rs;
        }
        //填写姓名时自动获取ID号
        private void name_TextChanged(object sender, EventArgs e)
        {
            /*思想：获取病历表中的最大ID，+1；  */
            string sql = "select MAX(IDcard) as id from patient";
            ds = sqlHelper.ExecuteDataSet(sql);
            string val = ds.Tables[0].Rows[0]["id"].ToString();
            int maxnum = int.Parse(val);
            maxnum++;
            string s = Convert.ToString(maxnum);
            hosipitalCard.Text = s;

        }

        string serialNum;
        /****************挂号***************************************/
        private void button5_Click(object sender, EventArgs e)
        {
            int remain = getRemainNum();
            if (remain <= 0)
            {
                MessageBox.Show("该医生今日已满号");
            }
            else
            {


                serialNum = CreateSerialNum.GetSerialNumber(); //流水号
                string buyRecord = "否";
                if (buy.Checked)
                {
                    buyRecord = "是";
                }
                else
                {
                    buyRecord = "否";
                }

                int totalInt = int.Parse(total.Text);
                int realInt = Convert.ToInt32(real.Text);
                int chargeInt = Convert.ToInt32(charge.Text);
                string methodD = method.Text;
                int i = 0;
                int num = 0;
                int sub = 0;
                try
                {

                    //这里写sql语句 ，ExecuteNoquery()会返回受影响的行数，为int类型
                    string s = string.Format("insert into RegisteredInformation(SerialNumber, Name,RegisteredLevel,RegisteredDepartment,Classes,DoctorName,YMDtime,HMStime,bugRecord,totalMoney,getMoney,backMoney,paymethod) values('{0}', '{1}','{2}', '{3}','{4}', '{5}','{6}', '{7}','{8}', '{9}','{10}', '{11}','{12}')", serialNum, nameV, level.Text, room.Text, classes.Text, doctor.Text, day.Text, halfday.Text, buyRecord, totalInt, realInt, chargeInt, methodD);
                    i = sqlHelper.ExecuteNoQuery(s);
                    //将挂号数-1
                    string subStr = string.Format("update   DoctorSchedule  set Remainnum = Remainnum-1 where JobNumber =(select JobNumber from doctorInformation where Name = '{0}')", doctor.Text);
                    sub = sqlHelper.ExecuteNoQuery(subStr);
                }
                catch (Exception)
                {

                }
                if (i > 0)
                {

                }
                else
                {

                    MessageBox.Show("添加失败");
                }
                if (sub > 0)
                {
                    MessageBox.Show("挂号成功");
                }
                else
                {

                    MessageBox.Show("挂号失败");
                }
            }
        }
        //计算找零
        private void computed()
        {
            string l = level.Text;
            string c = classes.Text;
            int count = 0;
            switch (l)
            {
                case "普通":
                    count += 1;
                    break;
                case "主任医师":
                    count += 5;
                    break;
                case "教授":
                    count += 5;
                    break;
                case "副教授":
                    count += 5;
                    break;
                default:

                    break;
            }
            switch (c)
            {
                case "门诊":
                    count += 2;
                    break;
                case "急诊":
                    count += 3;
                    break;
                case "特诊":
                    count += 10;
                    break;
                default:

                    break;
            }
            if (buy.Checked)
            {
                count += 1;
            }
            total.Text = count.ToString();
            int result = 0;
            if (real.Text != "")
            {
                int t = int.Parse(total.Text);
                int r = int.Parse(real.Text);
                result = r - t;
                charge.Text = result.ToString();
            }
            else
            {
                return;
            }

        }
        private void total_MouseLeave(object sender, EventArgs e)
        {
            computed();
        }
        private void real_MouseLeave(object sender, EventArgs e)
        {
            computed();
        }
        //查询剩余号
        private int getRemainNum()
        {
            if (level.Text == "")
            {
                return 0;
            }
            string sql = string.Format("select  Remainnum from DoctorSchedule where JobNumber =(select JobNumber from doctorInformation where Name = '{0}' and YMDtime='{1}' AND HMStime='{2}')", doctor.Text, day.Text, halfday.Text);
            ds = sqlHelper.ExecuteDataSet(sql);

            string val = ds.Tables[0].Rows[0]["Remainnum"].ToString();
            return int.Parse(val);
        }

        private void doctor_MouseHover(object sender, EventArgs e)
        {
            doctors();
        }
        private void level_MouseClick(object sender, MouseEventArgs e)
        {

        }
        //查询所预约时间段值班的医生
        private void doctors()
        {

            DataSet dsDoctor = new DataSet();
            DataTable dtDoctor = new DataTable();
            string department = room.Text;

            string d = day.Text.ToString();
            string h = halfday.Text;
            string sql = string.Format("select name from doctorInformation where JobNumber in( select JobNumber from DoctorSchedule where DepartmentID in ( select DepartmentID from sDepartment where DepartmentNum in ( select DepartmentNum from fDepartment where Name = '{0}' ) ) AND YMDtime = '{1}' AND HMStime = '{2}')", department, d, h);
            dsDoctor = sqlHelper.ExecuteDataSet(sql);
            if (dsDoctor.Tables.Count > 0)
            {
                doctor.DataSource = dsDoctor.Tables[0];
                doctor.DisplayMember = "name";
                doctor.ValueMember = "name";
            }
            else
            {
                MessageBox.Show("当前时间段没有医生排班");
            }
        }



        /****************查询排班***************************************/
        private void button6_Click(object sender, EventArgs e)
        {
            if (doctorName.Text == "")
            {
                return;
            }
            string n = doctorName.Text.Trim();


            string sql = string.Format("select JobNumber as '工号', DepartmentName as '科室',YMDtime as '日期', HMStime as '时间', Maxnum as '最大挂号数',Remainnum as '剩余号数'  from DoctorSchedule  where  JobNumber in (select JobNumber from doctorInformation where Name='{0}')", n);
            ds = sqlHelper.ExecuteDataSet(sql);
            dt = ds.Tables[0];
            dataGridView1.DataSource = dt;
        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {
        }

        private void 一肿瘤科ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string sql = string.Format("select * from DoctorSchedule WHERE DepartmentID='005001'");
            ds = sqlHelper.ExecuteDataSet(sql);
            dt = ds.Tables[0];
            dataGridView2.DataSource = dt;
        }

        private void 二肿瘤科ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string sql = string.Format("select * from DoctorSchedule WHERE DepartmentID='005002'");
            ds = sqlHelper.ExecuteDataSet(sql);
            dt = ds.Tables[0];
            dataGridView2.DataSource = dt;
        }

        private void 三肿瘤科ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string sql = string.Format("select * from DoctorSchedule WHERE DepartmentID='005003'");
            ds = sqlHelper.ExecuteDataSet(sql);
            dt = ds.Tables[0];
            dataGridView2.DataSource = dt;
        }
        private void 四肿瘤科ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string sql = string.Format("select * from DoctorSchedule WHERE DepartmentID='005004'");
            ds = sqlHelper.ExecuteDataSet(sql);
            dt = ds.Tables[0];
            dataGridView2.DataSource = dt;
        }
        //打印
        private void button3_Click(object sender, EventArgs e)
        {
            Print p = new Print(serialNum);
            p.Show();

        }
        //加号
        private void add_Click(object sender, EventArgs e)
        {

        }


        private void splitContainer3_SplitterMoved(object sender, SplitterEventArgs e)
        {

        }

        private void splitContainer3_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void splitContainer3_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        private void button1_Click(object sender, EventArgs e)
        {
        }

        private void doctor_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

            if (doctorName.Text == "") { return; }
            MessageBoxButtons messButton = MessageBoxButtons.OKCancel;
            DialogResult dr = MessageBox.Show("确认加号?", "提示", messButton);

            if (dr == DialogResult.OK)//如果点击“确定”按钮

            {
                int a = dataGridView1.CurrentRow.Index;
                if (a < 0)
                {
                    MessageBox.Show("请选中对应医生");
                    return;
                }
                int m = (int)dataGridView1.Rows[a].Cells["最大挂号数"].Value;
                string hms = (string)dataGridView1.Rows[a].Cells["时间"].Value;
                string ymd = (string)dataGridView1.Rows[a].Cells["日期"].Value;
                string jobnumber = (string)dataGridView1.Rows[a].Cells["工号"].Value;


                int i = 0;
                try
                {

                    //这里写sql语句 ，ExecuteNoquery()会返回受影响的行数，为int类型                     
                    string subStr = string.Format("update   DoctorSchedule  set Maxnum = Maxnum+1,Remainnum=Remainnum+1 where JobNumber='{0}' and YMDtime='{1}' and HMStime='{2}'", jobnumber, ymd, hms);

                    i = sqlHelper.ExecuteNoQuery(subStr);
                }
                catch (Exception)
                {

                }
                if (i > 0)
                {
                    MessageBox.Show("加号成功！");
                    string n = doctorName.Text.Trim();
                    string sql = string.Format("select JobNumber as '工号', DepartmentName as '科室',YMDtime as '日期', HMStime as '时间', Maxnum as '最大挂号数',Remainnum as '剩余号数'  from DoctorSchedule  where  JobNumber in (select JobNumber from doctorInformation where Name='{0}')", n);
                    ds = sqlHelper.ExecuteDataSet(sql);
                    dt = ds.Tables[0];
                    dataGridView1.DataSource = dt;

                }
                else
                {

                    MessageBox.Show("添加失败");
                }
            }
            else
            {
                return;
            }
        }
        System.Timers.Timer timer = new System.Timers.Timer();
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            timer.Enabled = true;
            timer.Interval = 1;   
            timer.Start();
            timer.Elapsed += new System.Timers.ElapsedEventHandler(speak);
            for (int i = 0; i < 2; i++)
            {
                timer.Enabled = true;
                timer.Interval = 1000; //执行间隔时间,单位为毫秒; 这里实际间隔为1s 
                timer.Start();
                timer.Elapsed += new System.Timers.ElapsedEventHandler(speak);
            }
            string s = "delete from  line  where id = (select MIN(id) from line)";
           
            try
            {
                //这里写sql语句 ，ExecuteNoquery()会返回受影响的行数，为int类型
              sqlHelper.ExecuteNoQuery(s);
            }
            catch (Exception)
            {

            }


        }
        public  void speak(object source, ElapsedEventArgs e)
        {
            DataSet ds = new DataSet();
            string sql = "select * from line";
            ds = sqlHelper.ExecuteDataSet(sql);
            SpeechVoiceSpeakFlags flag = SpeechVoiceSpeakFlags.SVSFlagsAsync;
            SpVoice voice = new SpVoice();
            string b = ds.Tables[0].Rows[0]["id"].ToString();
            string voice_txt = string.Format("Please register for number {0} ", b);
            voice.Voice = voice.GetVoices(string.Empty, string.Empty).Item(0);
            voice.Speak(voice_txt, flag);
            timer.Stop();

        }
    }

}
