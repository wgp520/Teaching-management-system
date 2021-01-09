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
    public partial class WorkloadStatistics : Form
    {
        LinkSQL linkSql;

        public WorkloadStatistics()
        {
            InitializeComponent();
        }

        private void WorkloadStatistics_Load(object sender, EventArgs e)
        {
            linkSql = new LinkSQL();
            string sql = "select teacher.tno as '工号',tname as '姓名',t_workload as '工作量(课时)' from t_workload_view, teacher where t_workload_view.tno = teacher.tno";
            this.dataGridView1.DataSource = linkSql.QuerySQL(sql);
            this.dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }
    }
}
