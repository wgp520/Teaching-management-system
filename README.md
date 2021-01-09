# 高校教学管理系统



## 一、前言


教学管理系统对于学校的决策者和管理者来说都至关重要,所以应该能够为用户提供充足的信息和快捷的查询手段。但一直以来人们使用传统人工的方式管理文件档案，这种管理方式存在着许多缺点,如:效率低、保密性差,另外时间一长,将产生大量的文件和数据 ,这对于查找、更新和维护都带来了不少的困难。
  
教学管理系统主要提供成绩查询和更新平台，学生可以通过该系统查询到自己各科目的学习成绩以及学籍信息等。利用该系统，教师可以清晰全面地了解学生的学习情况和档案，对系统的不同部分设置各自不同的权限。可以最大限度的利用计算机的各种优势，具有手工管理所无法比拟的优点.例如:检索迅速、查找方便、可靠性高、存储量大、保密性好、寿命长、成本低等，又可以利用计算机系统对数据的处理能力，方便分析和数据规整。

1、**项目背景**：数据库原理课程设计  
2、**编写目的**：掌握数据库设计原理及相关软件的使用  
3、**软件定义**：高校教学管理系统  
4、**开发环境**：Visual Studio 2017，SQL Server Management Studio2008。

## 二、系统设计
###  （一）系统功能设计
教师模块 教师模块是用来管理教师的信息，其中包含教授的管理信息，对管理教师的一些信息有作用，用来对教师进行记录和分配。在网上的信息查询中，教师的系统有着更多的功能，可以浏览学生的一些信息和成绩更正，对学生进行管理以及对学校的一些信息的浏览。
 
学生模块 学生模块是用来管理学生的信息，可以将其中分为两个模块，一个是学籍管理模块，一个是课程管理模块，这个模块可以对学生的成绩进行查询和对课程的查询，对学生进行统一的管理，在网上可以通过此模块对一些信息进行确定和接受一些的学校的信息，对此可以进行一些必要的操作。

###  （二）系统功能模块设计

##  三、数据库设计
###  （一）需求分析
教学管理是所有高校都应具备的最基本的管理功能。高校教学管理系统可实现高等院校的简单教学管理，包括学生人学登记、学生选课、教师登记考试成绩、补考处理、学生成绩统计、教师教学工作量统计，可随时查询院系、教师、学生、课程、选课、成绩等情况。

###  （二）概念结构设计

####  （1）将各个实体转换为关系模式：
学院（编号、院系名、负责人）
教师（工号、姓名、性别、所属院系、职称、年龄、 出生年月、基本工资）
学生（学号、姓名、性别、年龄、所属院系）
课程（课程代号、课程名、课时数、课程类型、学分）

####  （2）实体间的联系做以下处理：
1、学院与学生间的1：n 属于联系：与学生关系模式合并，将学院号加入学生关系
学生（学号、姓名、性别、年龄、所属院系，学院编号）

2、学院与教师间的1：n 属于联系：与教师关系模式合并，将学院号加入教师生关系
教师（工号、姓名、性别、所属院系、职称、年龄、 出生年月、基本工资，学院编号）

3、课程与教师间的1：1 负责联系：与课程关系模式合并，将教师工号加入课程关系
课程（课程代号、课程名、课时数、课程类型、学分，负责教师工号）

4、课程与教师间的m：n 参与联系，转换成一个独立的关系模式:
教师参与课程（课程号，参与教师工号）
####  （3）该ER图转换成以下关系模式：
学院（编号、院系名、负责人）
学生（学号、姓名、性别、年龄、所属院系，学院编号）
教师（工号、姓名、性别、所属院系、职称、年龄、 出生年月、基本工资，学院编号）
课程（课程代号、课程名、课时数、课程类型、学分，负责教师工号）
教师参与课程（课程代号，参与教师工号）
##  四、数据库实现
数据库由以下表组成： 
Student：学生信息表 
Teacher：教职工信息表 
Depart ： 系信息表 
Class ：课程信息表 
Sc  ： 选课信息表
Tc  : 教师课程表

```sql
create table depart
(
dno char(3),
dname char(20),
principal char(20)
)

create table student
(
sno char(12) primary key,
sname char(20) not null,
ssex char(2),
sage int,
sdept char(20)
)

create table teacher
(
tno char(3) primary key,
tname char(20) not null,
tsex char(2),
tdept char(20),
title char(20),
tage int,
tbirthday date,
wage int
)

create table class
(
cno char(3) primary key, 
cname char(20) not null, 
chours int, 
ctype char(20), 
credit float
)

create table sc
(
sno char(12),
cno char(3),
grade int,
primary key(sno,cno),
foreign key(sno) references student(sno),
foreign key(cno) references class(cno)
)

create table tc
(
tno char(12),
cno char(3),
chours smallint,
primary key(tno,cno),
foreign key(tno) references teacher(sno),
foreign key(cno) references class(cno)
)

create view t_workload_view
as
select tno,SUM(chours) as t_workload
from tc
group by tno

```
##  五、系统实现
###  （一）查询函数

```csharp
//查询，返回DataTable
public DataTable QuerySQL(string sql)
{
    DataSet ds = new DataSet();
    using (SqlConnection conn = new SqlConnection())
    {
        conn.ConnectionString = @"server=DESKTOP-5T68OT4\SQLEXPRESS;database=jxgl;integrated security=true";
        conn.Open();
        using (SqlCommand command = conn.CreateCommand())
        {
            command.CommandText = sql;
            SqlDataAdapter da = new SqlDataAdapter(command);
            try
            {
                da.Fill(ds);
            }
            catch
            {
                
            }
        }
    }
    DataTable dt = new DataTable();
    try
    {
        dt = ds.Tables[0].Copy();
    }
    catch
    {
        
    }
    return dt;
}

```
###  （二）修改函数

```csharp
//修改数据
public void AlterSQL(string sql)
{
    // 连接数据库
    SqlConnection conn = new SqlConnection();
    conn.ConnectionString = @"server=DESKTOP-5T68OT4\SQLEXPRESS;database=jxgl;integrated security=true";
    conn.Open();

    //将数据存入数据库
    SqlCommand cmd = new SqlCommand();
    cmd.CommandType = CommandType.Text;
    cmd.CommandText = sql;
    cmd.Connection = conn;
    cmd.ExecuteNonQuery();

    //断开连接，释放资源
    conn.Close();
}

```
###  （三）学分统计

```csharp
string sql3 = string.Format("select SUM(credit) from class where cno in (select cno from sc where sno = '{0}' and grade >= 60)",Program.loginId);
dt3 = linkSql.QuerySQL(sql3);
int loginCredit = 0;
try
{
    loginCredit = Convert.ToInt32(dt3.Rows[0][0].ToString());
}
catch
{

}
label1.Text = string.Format("您已选修学分为{0}学分，最高可选修120学分", loginCredit);
//如果学分大于120，则不能选课
if(loginCredit > 120)
{
    //将复选框关闭
    listView1.CheckBoxes = false;
}

```
###  （四）选课人数统计

```csharp
string sql4 = "select sc.cno,count(distinct sno) as snoCount,count(distinct tno) as tnoCount from sc,tc where sc.cno = tc.cno group by sc.cno";
dt4 = linkSql.QuerySQL(sql4);
foreach(DataRow dr in dt4.Rows)
{
    //选课人数小于教师数*15，不开课
    if (Convert.ToInt32(dr["snoCount"].ToString()) < 15 * Convert.ToInt32(dr["tnoCount"].ToString()))
    {
        Program.startClass = false;
    }
    //选课人数大于教师数*50，无法选课
    else if (Convert.ToInt32(dr["snoCount"].ToString()) > 50 * Convert.ToInt32(dr["tnoCount"].ToString()))
    {
        cnoList.Add(dr["cno"].ToString()); //记录下课程号
    }
    //正常选课，开课
    else
    {
        Program.startClass = true;
    }
}

```
![在这里插入图片描述](https://img-blog.csdnimg.cn/20191205150455728.png?x-oss-process=image/watermark,type_ZmFuZ3poZW5naGVpdGk,shadow_10,text_aHR0cHM6Ly9ibG9nLmNzZG4ubmV0L3FxXzQxNDE1Mjk0,size_16,color_FFFFFF,t_70)
###  （五）成绩登记，保存修改

```csharp
private void button2_Click_1(object sender, EventArgs e)
{
    string SCsno;
    string SCgrade;
    string sql;
    for (int i = 0; i < dt1.Rows.Count; i++)
    {
        SCsno = dt1.Rows[i]["学号"].ToString();
        SCgrade = dt1.Rows[i]["成绩"].ToString();
        sql = string.Format("update dbo.sc set grade = {1} where sno = '{0}' and cno = '{2}'", SCsno, SCgrade, comboBox2.SelectedValue);
        linkSQL.AlterSQL(sql);
    }
    MessageBox.Show("保存成功！");
}

```
![在这里插入图片描述](https://img-blog.csdnimg.cn/20191205150600417.png?x-oss-process=image/watermark,type_ZmFuZ3poZW5naGVpdGk,shadow_10,text_aHR0cHM6Ly9ibG9nLmNzZG4ubmV0L3FxXzQxNDE1Mjk0,size_16,color_FFFFFF,t_70)
###  （六）教师信息查询

```csharp
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

```
###  （七）登录界面

```csharp
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

```
![在这里插入图片描述](https://img-blog.csdnimg.cn/20191205152008106.png?x-oss-process=image/watermark,type_ZmFuZ3poZW5naGVpdGk,shadow_10,text_aHR0cHM6Ly9ibG9nLmNzZG4ubmV0L3FxXzQxNDE1Mjk0,size_16,color_FFFFFF,t_70)
###  （八）注册界面

```csharp
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

```
![在这里插入图片描述](https://img-blog.csdnimg.cn/2019120515211666.png?x-oss-process=image/watermark,type_ZmFuZ3poZW5naGVpdGk,shadow_10,text_aHR0cHM6Ly9ibG9nLmNzZG4ubmV0L3FxXzQxNDE1Mjk0,size_16,color_FFFFFF,t_70)
![在这里插入图片描述](https://img-blog.csdnimg.cn/20191205153158308.png?x-oss-process=image/watermark,type_ZmFuZ3poZW5naGVpdGk,shadow_10,text_aHR0cHM6Ly9ibG9nLmNzZG4ubmV0L3FxXzQxNDE1Mjk0,size_16,color_FFFFFF,t_70)
##  六、结束语
SQL是Structured Query Language(结构化查询语言)的缩写。SQL是专为数据库而建立的操作命令集，是一种功能齐全的数据库语言。在使用它时，只需要发出“做什么”的命令，“怎么做”是不用使用者考虑的。SQL功能强大、简单易学、使用方便，已经成为了数据库操作的基础，并且现在几乎所有的数据库均支持SQL。经过一个学期的数据库课程的学习，我们掌握了创建数据库以及对数据库的操作的基础知识。老师的教学耐心细致，课堂上我们有不理解的地方老师都反复讲解，使我们的基础知识掌握的比较牢固。数据库这门课程涉及到以前的知识不多，是一门从头学起的课程，即使基础不是很好，只要认真听讲、复习功课，还是一门比较容易掌握的课。简单的说下我对数据库的理解吧。我觉得它就是创建一些表格，然后再用一些语句根据他们之间的关系，把它们组合在一起。最基本的就是子查询了。 我的子查询经验就是先写出select * 我们要找什么，然后写条件，我们要找的东西有什么条件，然后在写条件，我们的条件涉及那些表，那些字段，再在这些字段中通过我们学过的简单select语句选出来，有时候还要用到几层子查询，不过无所谓，只要思路是清晰的就没什么问题了。接下来，关联查询之类的，学起来也是不难的， 总之，这是一门很值得学习的课程，自己学过获益匪浅。 
