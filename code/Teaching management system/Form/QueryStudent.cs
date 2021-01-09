using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Teaching_management_system
{
    public partial class QueryStudent : Form
    {
        LinkSQL linkSql;

        public QueryStudent()
        {
            InitializeComponent();
        }

        private void QueryStudent_Load(object sender, EventArgs e)
        {
            ChooseStudent();
            linkSql = new LinkSQL();
            this.dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void ChooseStudent()
        {
            comboBox2.Items.Insert(0, "----请选择----");
            comboBox2.SelectedIndex = 0;
            string[] facultyArray = {
                                        "学号",
                                        "姓名",
                                        "性别",
                                        "年龄",
                                        "所属院系"
                                    };
            foreach (var item in facultyArray)
            {
                comboBox2.Items.Add(item);
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            string sql = "select sno as '学号',sname as '姓名',ssex as '性别',sage as '年龄',sdept as '所属院系' from dbo.student";
            //绑定到datagridview中显示
            this.dataGridView1.DataSource = linkSql.QuerySQL(sql);
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || comboBox2.SelectedIndex == 0)
            {
                MessageBox.Show("请输入查询方式和查询内容！");
            }
            else
            {
                string sql = null;
                switch (comboBox2.SelectedIndex)
                {
                    case 1://学号
                        sql = string.Format("select sno as '学号',sname as '姓名',ssex as '性别',sage as '年龄',sdept as '所属院系' from dbo.student where sno = '{0}'", textBox1.Text);
                        break;
                    case 2://姓名
                        sql = string.Format("select sno as '学号',sname as '姓名',ssex as '性别',sage as '年龄',sdept as '所属院系' from dbo.student where sname = '{0}'", textBox1.Text);
                        break;
                    case 3://性别
                        sql = string.Format("select sno as '学号',sname as '姓名',ssex as '性别',sage as '年龄',sdept as '所属院系' from dbo.student where ssex = '{0}'", textBox1.Text);
                        break;
                    case 4://年龄
                        sql = string.Format("select sno as '学号',sname as '姓名',ssex as '性别',sage as '年龄',sdept as '所属院系' from dbo.student where sage = {0}", textBox1.Text);
                        break;
                    case 5://所属院系
                        sql = string.Format("select sno as '学号',sname as '姓名',ssex as '性别',sage as '年龄',sdept as '所属院系' from dbo.student where sdept = '{0}'", textBox1.Text);
                        break;
                    default:
                        break;
                }
                //绑定到datagridview中显示
                this.dataGridView1.DataSource = linkSql.QuerySQL(sql);
            }
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            textBox1.Text = "";
            comboBox2.SelectedIndex = 0;
            dataGridView1.DataSource = null;
        }
    }
}
