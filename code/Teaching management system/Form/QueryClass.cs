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
    public partial class QueryClass : Form
    {
        LinkSQL linkSql;

        public QueryClass()
        {
            InitializeComponent();
        }

        private void QueryClass_Load(object sender, EventArgs e)
        {
            ChooseClass();
            linkSql = new LinkSQL();
            this.dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void ChooseClass()
        {
            comboBox2.Items.Insert(0, "----请选择----");
            comboBox2.SelectedIndex = 0;
            string[] facultyArray = {
                                        "课程代号",
                                        "课程名",
                                        "课时数",
                                        "课程类型",
                                        "学分"
                                    };
            foreach (var item in facultyArray)
            {
                comboBox2.Items.Add(item);
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            string sql = "select cno as '课程代号',cname as '课程名',chours as '课时数',ctype as '课程类型',credit as '学分' from dbo.class";
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
                    case 1://课程代号
                        sql = string.Format("select cno as '课程代号',cname as '课程名',chours as '课时数',ctype as '课程类型',credit as '学分' from dbo.class where cno = '{0}'", textBox1.Text);
                        break;
                    case 2://课程名
                        sql = string.Format("select cno as '课程代号',cname as '课程名',chours as '课时数',ctype as '课程类型',credit as '学分' from dbo.class where cname = '{0}'", textBox1.Text);
                        break;
                    case 3://课时数
                        sql = string.Format("select cno as '课程代号',cname as '课程名',chours as '课时数',ctype as '课程类型',credit as '学分' from dbo.class where chours = {0}", textBox1.Text);
                        break;
                    case 4://课程类型
                        sql = string.Format("select cno as '课程代号',cname as '课程名',chours as '课时数',ctype as '课程类型',credit as '学分' from dbo.class where ctype = '{0}'", textBox1.Text);
                        break;
                    case 5://学分
                        sql = string.Format("select cno as '课程代号',cname as '课程名',chours as '课时数',ctype as '课程类型',credit as '学分' from dbo.class where credit = {0}", textBox1.Text);
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
