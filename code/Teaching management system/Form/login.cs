using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Teaching_management_system
{
    public partial class login : Form
    {
        [DllImport("user32.dll")]//拖动无窗体的控件
        public static extern bool ReleaseCapture();
        [DllImport("user32.dll")]
        public static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);
        public const int WM_SYSCOMMAND = 0x0112;
        public const int SC_MOVE = 0xF010;
        public const int HTCAPTION = 0x0002;

        private bool isFind = false;

        public login()
        {
            InitializeComponent();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            registration Registration = new registration();
            Registration.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //连接数据库
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = @"server=DESKTOP-5T68OT4\SQLEXPRESS;database=jxgl;integrated security=true";
            conn.Open();

            if (textBox1.Text != "" && textBox2.Text != "") //判断是否有输入
            {
                if (radioButton1.Checked == false && radioButton2.Checked == false)
                {
                    MessageBox.Show("请选择登录类型！");
                }
                else
                {
                    //学生被选中
                    if(radioButton1.Checked == true)
                    {
                        string sql = "select sno,sname from dbo.student";
                        Program.isTeacher = false;
                        LogIn(sql, conn, Program.isTeacher);
                    }
                    //教师被选中
                    else if(radioButton2.Checked == true)
                    {
                        string sql = "select tno,tname from dbo.teacher";
                        Program.isTeacher = true;
                        LogIn(sql, conn, Program.isTeacher);
                    }
                }
            }
            else//没有输入ID或密码
            {
                MessageBox.Show("请输入ID和姓名！");
            }
        }

        //与数据库进行匹配，若相等则跳转页面
        private void LogIn(string sql, SqlConnection conn, bool isTeacher)
        {
            SqlCommand comm = new SqlCommand(sql);
            comm.Connection = conn;
            SqlDataReader sdreader = comm.ExecuteReader();

            if (sdreader.HasRows)//判断是否有数据
            {
                while (sdreader.Read())
                {
                    string sno = sdreader.GetString(0).Replace(" ", "");
                    string sname = sdreader.GetString(1).Replace(" ", "");
                    if (sno.Equals(textBox1.Text) && sname.Equals(textBox2.Text))
                    {
                        isFind = true;
                        //记录ID和姓名
                        Program.loginId = textBox1.Text;
                        Program.loginName = textBox2.Text;

                        if (isTeacher)
                        {
                            //跳转页面
                            t_mainForm t_MainForm = new t_mainForm();
                            t_MainForm.ShowDialog(this);
                        }
                        else
                        {
                            //跳转页面
                            s_mainForm s_MainForm = new s_mainForm();
                            s_MainForm.ShowDialog(this);
                        }
                        
                        this.Close();
                        break;
                    }
                }
            }

            if (!isFind)
            {
                textBox1.Text = ("");
                textBox2.Text = ("");
                MessageBox.Show("ID或姓名错误！");
            }

            //断开连接，释放资源
            sdreader.Close();
            conn.Close();
        }

        private void login_Load(object sender, EventArgs e)
        {
            //label2字体背景颜色设为透明
            label2.BackColor = Color.Transparent;
            label2.Parent = pictureBox1;//将pictureBox1设为标签的父控件
            label2.Location = new Point(65, 56);//重新设定标签的位置，这个位置时相对于父控件的左上角
        }

        //拖动窗口
        private void login_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);    
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
        }
    }
}
