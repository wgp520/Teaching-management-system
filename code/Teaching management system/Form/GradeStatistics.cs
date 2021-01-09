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
    public partial class GradeStatistics : Form
    {
        DataTable dt = new DataTable();
        LinkSQL linkSql = new LinkSQL();

        public GradeStatistics()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            comboBox2.SelectedIndex = 0;
            dataGridView1.DataSource = null;
        }

        private void GradeStatistics_Load(object sender, EventArgs e)
        {
            string sql = "select cno,cname from class";
            dt = linkSql.QuerySQL(sql);
            comboBox2.DataSource = dt; //将表绑定到控件
            comboBox2.DisplayMember = "cname";//定义要显示的内容为列名为x的内容
            comboBox2.ValueMember = "cno";//定义要映射的值为y的值
            this.dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string sql = string.Format("select student.sno as '学号',sname as '姓名',grade as '成绩' from dbo.student,dbo.sc where dbo.student.sno = dbo.sc.sno and cno = '{0}' order by grade DESC", comboBox2.SelectedValue);
            //绑定到datagridview中显示
            this.dataGridView1.DataSource = linkSql.QuerySQL(sql);
        }
    }
}
