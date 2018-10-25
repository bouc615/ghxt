using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Print : Form
    {
        private string name;
        public Print(string t)
        {
            InitializeComponent();
            name = t;

        }
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        private void Print_Load(object sender, EventArgs e)
        {
            try{

           
             string sql = "select * from RegisteredInformation where SerialNumber='" + name+"'";
            ds = sqlHelper.ExecuteDataSet(sql);
            string SerialNumber = ds.Tables[0].Rows[0]["SerialNumber"].ToString();
            string Name = ds.Tables[0].Rows[0]["Name"].ToString();
            string RegisteredLevel = ds.Tables[0].Rows[0]["RegisteredLevel"].ToString();
            string RegisteredDepartment = ds.Tables[0].Rows[0]["RegisteredDepartment"].ToString();
            string Classes = ds.Tables[0].Rows[0]["Classes"].ToString();
            string DoctorName = ds.Tables[0].Rows[0]["DoctorName"].ToString();
            string YMDtime = ds.Tables[0].Rows[0]["YMDtime"].ToString();
            string HMStime = ds.Tables[0].Rows[0]["HMStime"].ToString();
            string paymethod = ds.Tables[0].Rows[0]["paymethod"].ToString();
            string bugRecord = ds.Tables[0].Rows[0]["bugRecord"].ToString();
            string totalMoney = ds.Tables[0].Rows[0]["totalMoney"].ToString();
            string getMoney = ds.Tables[0].Rows[0]["getMoney"].ToString();
            string backMoney = ds.Tables[0].Rows[0]["backMoney"].ToString();
          
            lsh.Text = SerialNumber;
            hzxm.Text = Name;
            jb.Text = RegisteredLevel;
            ks.Text = RegisteredDepartment;
            hl.Text = Classes;
            ysxm.Text = DoctorName;
            rq.Text = YMDtime;
            sjd.Text = HMStime;
            jffs.Text = paymethod;
            bl.Text = bugRecord;
            ze.Text = totalMoney;
            ss.Text = getMoney;
            zl.Text = backMoney;
            }
            catch(Exception)
            {
                MessageBox.Show("无信息");
                return;
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
