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
    public partial class registration : Form
    {
        [DllImport("user32.dll")]//拖动无窗体的控件
        public static extern bool ReleaseCapture();
        [DllImport("user32.dll")]
        public static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);
        public const int WM_SYSCOMMAND = 0x0112;
        public const int SC_MOVE = 0xF010;
        public const int HTCAPTION = 0x0002;

        private bool isFind;

        public registration()
        {
            InitializeComponent();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void registration_Load(object sender, EventArgs e)
        {
            //label2字体背景颜色设为透明
            label2.BackColor = Color.Transparent;
            label2.Parent = pictureBox1;//将pictureBox1设为标签的父控件
            label2.Location = new Point(161, 37);//重新设定标签的位置，这个位置时相对于父控件的左上角

            //comboBox显示
            ChooseSex();
            ChooseFaculty();
            ChooseAge();
            ChooseTitle();
            ChooseBirthday();
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
        }


        private void label2_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
        }

        //性别选择
        public void ChooseSex()
        {
            comboBox1.Items.Insert(0, "----请选择----");
            comboBox1.SelectedIndex = 0;
            comboBox1.Items.Add("男");
            comboBox1.Items.Add("女");
            comboBox4.Items.Insert(0, "----请选择----");
            comboBox4.SelectedIndex = 0;
            comboBox4.Items.Add("男");
            comboBox4.Items.Add("女");
        }

        //选择所属院系
        public void ChooseFaculty()
        {
            comboBox2.Items.Insert(0, "----请选择----");
            comboBox2.SelectedIndex = 0;
            comboBox5.Items.Insert(0, "----请选择----");
            comboBox5.SelectedIndex = 0;
            string[] facultyArray = {
                                        "理学院",
                                        "矿业工程学院" ,
                                        "冶金与能源学院" ,
                                        "机械工程学院" ,
                                        "化学工程学院" ,
                                        "建筑工程学院" ,
                                        "基础医学院" ,
                                        "临床医学院" ,
                                        "公共卫生学院" ,
                                        "生命科学学院" ,
                                        "材料科学与工程学院" ,
                                        "电气工程学院" ,
                                        "管理学院" ,
                                        "经济学院" ,
                                        "护理与康复学院" ,
                                        "中医学院" ,
                                        "口腔医学院" ,
                                        "药学院" ,
                                        "信息工程学院" ,
                                        "外国语学院" ,
                                        "人文法律学院" ,
                                        "艺术学院" ,
                                        "心理学院"
                                    };
            foreach (var item in facultyArray)
            {
                comboBox2.Items.Add(item);
                comboBox5.Items.Add(item);
            }
        }

        //年龄选择
        public void ChooseAge()
        {
            comboBox3.Items.Insert(0, "----请选择----");
            comboBox3.SelectedIndex = 0;
            comboBox6.Items.Insert(0, "----请选择----");
            comboBox6.SelectedIndex = 0;
            for (int i = 10; i <= 80; i++)
            {
                comboBox3.Items.Add(i);
                comboBox6.Items.Add(i);
            }
        }

        //职称选择
        private void ChooseTitle()
        {
            comboBox7.Items.Insert(0, "----请选择----");
            comboBox7.SelectedIndex = 0;
            string[] titleArray = {
                                        "助教",
                                        "讲师",
                                        "副教授",
                                        "教授"
                                    };
            foreach (var item in titleArray)
            {
                comboBox7.Items.Add(item);
            }
        }

        //出生年月选择
        private void ChooseBirthday()
        {
            comboBox8.Items.Insert(0, "---");
            comboBox8.SelectedIndex = 0;
            comboBox9.Items.Insert(0, "---");
            comboBox9.SelectedIndex = 0;
            comboBox10.Items.Insert(0, "---");
            comboBox10.SelectedIndex = 0;
            for (int i = 1939; i <= 2019; i++)
            {
                comboBox8.Items.Add(i);
            }
            for (int i = 1; i <= 12; i++)
            {
                comboBox9.Items.Add(i);
            }
            for (int i = 1; i <= 31; i++)
            {
                comboBox10.Items.Add(i);
            }
        }

        //清空输入栏
        private void ClearStudentBox()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            comboBox3.SelectedIndex = 0;
        }

        private void ClearTeacherBox()
        {
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            comboBox4.SelectedIndex = 0;
            comboBox5.SelectedIndex = 0;
            comboBox6.SelectedIndex = 0;
            comboBox7.SelectedIndex = 0;
            comboBox8.SelectedIndex = 0;
            comboBox9.SelectedIndex = 0;
            comboBox10.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.textBox1.Text == "" || this.textBox2.Text == "" || this.comboBox1.SelectedIndex == 0 || this.comboBox2.SelectedIndex == 0 || this.comboBox3.SelectedIndex == 0)
            {
                MessageBox.Show("请输入完整的学生信息！");
            }
            else
            {
                //连接数据库
                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = @"server=DESKTOP-5T68OT4\SQLEXPRESS;database=jxgl;integrated security=true";
                conn.Open();

                string sql = "select sno from dbo.student";
                SqlCommand comm = new SqlCommand(sql);
                comm.Connection = conn;
                SqlDataReader sdreader = comm.ExecuteReader();

                if (sdreader.HasRows)//判断是否有数据
                {
                    while (sdreader.Read())
                    {
                        string sno = sdreader.GetString(0).Replace(" ", "");
                        if (sno.Equals(textBox1.Text))
                        {
                            isFind = true;
                            MessageBox.Show("该学号已存在！");
                            break;
                        }
                    }
                }
                //关闭sdreader
                sdreader.Close();

                if (!isFind)
                {
                    //将数据存入数据库
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = string.Format("insert into student(sno, sname, ssex, sage, sdept) values('{0}','{1}','{2}',{3},'{4}')", textBox1.Text, textBox2.Text, comboBox1.Text, comboBox3.Text, comboBox2.Text);
                    cmd.Connection = conn;
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("添加成功");
                    ClearStudentBox();//清空输入栏
                }

                //断开连接，释放资源
                conn.Close();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

            if (this.textBox3.Text == "" || this.textBox4.Text == "" || this.textBox5.Text == "" || this.comboBox4.SelectedIndex == 0 || this.comboBox5.SelectedIndex == 0 || this.comboBox6.SelectedIndex == 0 || this.comboBox7.SelectedIndex == 0 || this.comboBox8.SelectedIndex == 0 || this.comboBox9.SelectedIndex == 0 || this.comboBox10.SelectedIndex == 0)
            {
                MessageBox.Show("请输入完整的教师信息！");
            }
            else
            {
                //连接数据库
                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = @"server=DESKTOP-5T68OT4\SQLEXPRESS;database=jxgl;integrated security=true";
                conn.Open();

                string sql = "select tno from dbo.teacher";
                SqlCommand comm = new SqlCommand(sql);
                comm.Connection = conn;
                SqlDataReader sdreader = comm.ExecuteReader();

                if (sdreader.HasRows)//判断是否有数据
                {
                    while (sdreader.Read())
                    {
                        string tno = sdreader.GetString(0).Replace(" ", "");
                        if (tno.Equals(textBox1.Text))
                        {
                            isFind = true;
                            MessageBox.Show("该工号已存在！");
                            break;
                        }
                    }
                }
                //关闭sdreader
                sdreader.Close();

                if (!isFind)
                {
                    //将数据存入数据库
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = string.Format("insert into teacher(tno, tname, tsex, tdept, title, tage, tbirthday, wage)values('{0}', '{1}', '{2}', '{3}', '{4}', {5}, '{6}-{7}-{8}', {9})",
                        textBox3.Text, textBox4.Text, comboBox4.Text, comboBox5.Text, comboBox7.Text,
                        comboBox6.Text, comboBox8.Text, comboBox9.Text, comboBox10.Text, textBox5.Text);
                    cmd.Connection = conn;
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("添加成功");
                    ClearTeacherBox();//清空输入栏
                }

                //断开连接，释放资源
                conn.Close();
            }
        }
    }
}
