﻿using System;
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
    public partial class t_mainForm : Form
    {
        QueryDepartment queryDepartment;
        QueryTeacher queryTeacher;
        QueryStudent queryStudent;
        QueryClass queryClass;
        QueryGrade queryGrade;
        RegistrationGrade registrationGrade;
        GradeStatistics gradeStatistics;
        WorkloadStatistics workloadStatistics;
        RetakeTest retakeTest;

        public t_mainForm()
        {
            InitializeComponent();
        }

        private void t_mainForm_Load(object sender, EventArgs e)
        {
            this.Owner.Hide();
            //将窗体背景默认设为灰色
            this.BackColor = Color.FromArgb(211, 211, 211);
            //显示登录名
            label1.Text = "您好，" + Program.loginName + "老师";
            label1.BackColor = Color.FromArgb(255, 255, 255);

            queryDepartment = new QueryDepartment();
            queryTeacher = new QueryTeacher();
            queryStudent = new QueryStudent();
            queryClass = new QueryClass();
            queryGrade = new QueryGrade();
            registrationGrade = new RegistrationGrade();
            gradeStatistics = new GradeStatistics();
            retakeTest = new RetakeTest();
            workloadStatistics = new WorkloadStatistics();
        }

        private void 退出登录ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            login Login = new login();
            Login.ShowDialog();
            this.Close();
        }

        private void SetPanel1(Form f)
        {
            this.panel1.Controls.Clear();  // 清空原有的控件
            f.TopLevel = false;  // 非顶级窗口
            f.FormBorderStyle = FormBorderStyle.None;  // 不显示标题栏
            f.Dock = System.Windows.Forms.DockStyle.Fill;  // 填充panel
            this.panel1.Controls.Add(f);  // 添加f窗体
            f.Show();
        }

        private void 查询院系ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetPanel1(queryDepartment);
        }

        private void 查询教师ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetPanel1(queryTeacher);
        }

        private void 查询学生ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetPanel1(queryStudent);
        }

        private void 查询课程ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetPanel1(queryClass);
        }

        private void 登记成绩ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetPanel1(registrationGrade);
        }

        private void 学生成绩统计ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetPanel1(gradeStatistics);
        }

        private void 补考处理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetPanel1(retakeTest);
        }

        private void 教学工作量统计ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetPanel1(workloadStatistics);
        }
    }
}
