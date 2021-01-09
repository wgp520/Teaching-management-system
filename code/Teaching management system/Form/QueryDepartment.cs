using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Teaching_management_system
{
    public partial class QueryDepartment : Form
    {
        LinkSQL linkSql;

        public QueryDepartment()
        {
            InitializeComponent();
        }

        private void QueryDepartment_Load(object sender, EventArgs e)
        {
            ChooseDepartment();
            linkSql = new LinkSQL();
            this.dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void ChooseDepartment()
        {
            comboBox2.Items.Insert(0, "----请选择----");
            comboBox2.SelectedIndex = 0;
            string[] facultyArray = {
                                        "院系编号",
                                        "院系名",
                                        "负责人"
                                    };
            foreach (var item in facultyArray)
            {
                comboBox2.Items.Add(item);
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            string sql = "select dno as '院系编号',dname as '院系名',principal as '负责人' from dbo.depart";
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
                    case 1://院系编号
                        sql = string.Format("select dno as '院系编号',dname as '院系名',principal as '负责人' from dbo.depart where dno = '{0}'", textBox1.Text);
                        break;
                    case 2://院系名
                        sql = string.Format("select dno as '院系编号',dname as '院系名',principal as '负责人' from dbo.depart where dname = '{0}'", textBox1.Text);
                        break;
                    case 3://负责人
                        sql = string.Format("select dno as '院系编号',dname as '院系名',principal as '负责人' from dbo.depart where principal = '{0}'", textBox1.Text);
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
