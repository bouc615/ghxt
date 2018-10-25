using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
      
        private void Form1_Load(object sender, EventArgs e)
        {
            load();
        }
        /**************DataGridView控件绑定数据*********************************************/
        private void load()
        {
            string sql = "select * from login";
            ds = sqlHelper.ExecuteDataSet(sql);
            dt = ds.Tables[0];
            dataGrid.DataSource = dt;          
        }

        /**********************添加数据*************************************/
        private void add_Click_1(object sender, EventArgs e)
        {
                int i = 0;
                try
                {
                //这里写sql语句 ，ExecuteNoquery()会返回受影响的行数，为int类型
                    i = sqlHelper.ExecuteNoQuery("insert into login(account,password)values('" + textBox1.Text + "','" + textBox2.Text + "')");
                }
                catch (Exception)
                {
                    MessageBox.Show("添加失败");
                }
            if (i > 0)
            {
                MessageBox.Show("添加成功");
                load();
            }
            else
            {
                MessageBox.Show("添加失败");
            }
        }
        
        //查询login表的第1行account列的值
        private void delete_Click(object sender, EventArgs e)
        {
            
            string sql = "select * from login";
            ds = sqlHelper.ExecuteDataSet(sql);
            dt = ds.Tables[0];
            dataGrid.DataSource = dt;
            string val = ds.Tables[0].Rows[0]["account"].ToString();
            MessageBox.Show(val);
        }

        private void dataGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
