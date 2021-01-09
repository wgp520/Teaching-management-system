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
    public partial class QueryTeacher : Form
    {
        LinkSQL linkSql;

        public QueryTeacher()
        {
            InitializeComponent();
        }

        private void QueryTeacher_Load(object sender, EventArgs e)
        {
            ChooseTeacher();
            linkSql = new LinkSQL();
            this.dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void ChooseTeacher()
        {
            comboBox2.Items.Insert(0, "----请选择----");
            comboBox2.SelectedIndex = 0;
            string[] facultyArray = null;
            if (Program.isTeacher)
            {
                facultyArray = new string[]{
                                                "工号",
                                                "姓名",
                                                "性别",
                                                "所属院系",
                                                "职称",
                                                "年龄",
                                                "基本工资"
                                           };  
            }
            else
            {
                facultyArray = new string[]{
                                                "姓名",
                                                "性别",
                                                "所属院系",
                                                "职称",
                                           };
            }
            foreach (var item in facultyArray)
            {
                comboBox2.Items.Add(item);
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            string sql;
            if (Program.isTeacher)
            {
                sql = "select tno as '工号',tname as '姓名',tsex as '性别',tdept as '所属院系',title as '职称',tage as '年龄',wage as '基本工资' from dbo.teacher";
            }
            else
            {
                sql = "select tname as '姓名',tsex as '性别',tdept as '所属院系',title as '职称' from dbo.teacher";
            }
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

                if (Program.isTeacher)
                {
                    switch (comboBox2.SelectedIndex)
                    {
                        case 1://工号
                            sql = string.Format("select tno as '工号',tname as '姓名',tsex as '性别',tdept as '所属院系',title as '职称',tage as '年龄',wage as '基本工资' from dbo.teacher where tno = '{0}'", textBox1.Text);
                            break;
                        case 2://姓名
                            sql = string.Format("select tno as '工号',tname as '姓名',tsex as '性别',tdept as '所属院系',title as '职称',tage as '年龄',wage as '基本工资' from dbo.teacher where tname = '{0}'", textBox1.Text);
                            break;
                        case 3://性别
                            sql = string.Format("select tno as '工号',tname as '姓名',tsex as '性别',tdept as '所属院系',title as '职称',tage as '年龄',wage as '基本工资' from dbo.teacher where tsex = '{0}'", textBox1.Text);
                            break;
                        case 4://所属院系
                            sql = string.Format("select tno as '工号',tname as '姓名',tsex as '性别',tdept as '所属院系',title as '职称',tage as '年龄',wage as '基本工资' from dbo.teacher where tdept = '{0}'", textBox1.Text);
                            break;
                        case 5://职称
                            sql = string.Format("select tno as '工号',tname as '姓名',tsex as '性别',tdept as '所属院系',title as '职称',tage as '年龄',wage as '基本工资' from dbo.teacher where title = '{0}'", textBox1.Text);
                            break;
                        case 6://年龄
                            sql = string.Format("select tno as '工号',tname as '姓名',tsex as '性别',tdept as '所属院系',title as '职称',tage as '年龄',wage as '基本工资' from dbo.teacher where tage = {0}", textBox1.Text);
                            break;
                        case 7://基本工资
                            sql = string.Format("select tno as '工号',tname as '姓名',tsex as '性别',tdept as '所属院系',title as '职称',tage as '年龄',wage as '基本工资' from dbo.teacher where wage = {0}", textBox1.Text);
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    switch (comboBox2.SelectedIndex)
                    {
                        case 1://姓名
                            sql = string.Format("select tno as '工号',tname as '姓名',tsex as '性别',tdept as '所属院系',title as '职称',tage as '年龄',wage as '基本工资' from dbo.teacher where tname = '{0}'", textBox1.Text);
                            break;
                        case 2://性别
                            sql = string.Format("select tno as '工号',tname as '姓名',tsex as '性别',tdept as '所属院系',title as '职称',tage as '年龄',wage as '基本工资' from dbo.teacher where tsex = '{0}'", textBox1.Text);
                            break;
                        case 3://所属院系
                            sql = string.Format("select tno as '工号',tname as '姓名',tsex as '性别',tdept as '所属院系',title as '职称',tage as '年龄',wage as '基本工资' from dbo.teacher where tdept = '{0}'", textBox1.Text);
                            break;
                        case 4://职称
                            sql = string.Format("select tno as '工号',tname as '姓名',tsex as '性别',tdept as '所属院系',title as '职称',tage as '年龄',wage as '基本工资' from dbo.teacher where title = '{0}'", textBox1.Text);
                            break;
                        default:
                            break;
                    }
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
