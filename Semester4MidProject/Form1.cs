using MaterialSkin;
using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.ComponentModel.Design.ObjectSelectorEditor;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Status;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolBar;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using PdfSharp.Drawing.Layout;






namespace Semester4MidProject
{
    public partial class Form1 : MaterialForm
    {

        readonly MaterialSkin.MaterialSkinManager materialSkinManager;
        int MYID;
        int CLOID;
        int RubricID;
        int RubricLevelID;
        int AssessmentID;
        int AssessmentComponentID;
        int Student_Status;
        int count;
        int CLOCount;
        int AS_Count;
        int InAS_Count;
        int Rub_Count;
        int RubLevel_Count;
        int Assessment_Count;
        int AssessmentComponent_Count;
        int S_ID;




        public Form1()
        {
            InitializeComponent();
            //Design MaterialSkin
            materialSkinManager = MaterialSkin.MaterialSkinManager.Instance;
            materialSkinManager.EnforceBackcolorOnAllComponents = true;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkin.MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new MaterialSkin.ColorScheme(MaterialSkin.Primary.BlueGrey500, MaterialSkin.Primary.BlueGrey700,
            MaterialSkin.Primary.BlueGrey100, MaterialSkin.Accent.Amber700, TextShade.WHITE);
            ActiveStudentsView();
            InActiveStudentViews();
            CloView();
            RubricView();
            RubricLevelView();
            AssessmentView();
            AssessmentComponentView();
            AttendanceView();
            Evaluation1();
            Evaluation2();
            AssessmentReportView();
            AttendanceGridCheckBoxes();
            Count();


            //PlaceHolder
            if (S_Search_textBox1.Text == "" || Clo_Search_textBox1.Text == "" || Rubric_Search_textBox1.Text == "" || textBox1.Text == "" ||
                AssessmentSearch_textBox7.Text == "" || ASS_ComponentSearch_textBox7.Text == "")
            {
                S_Search_textBox1.Text = "Search Here";
                S_Search_textBox1.ForeColor = SystemColors.GrayText;

                Clo_Search_textBox1.Text = "Search Here";
                Clo_Search_textBox1.ForeColor = SystemColors.GrayText;

                Rubric_Search_textBox1.Text = "Search Here";
                Rubric_Search_textBox1.ForeColor = SystemColors.GrayText;

                textBox1.Text = "Search Here";
                textBox1.ForeColor = SystemColors.GrayText;

                AssessmentSearch_textBox7.Text = "Search Here";
                AssessmentSearch_textBox7.ForeColor = SystemColors.GrayText;

                ASS_ComponentSearch_textBox7.Text = "Search Here";
                ASS_ComponentSearch_textBox7.ForeColor = SystemColors.GrayText;
            }



        }
        private void ActiveStudentsView()
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Select FirstName, LastName, Contact, Email, RegistrationNumber from Student Where Status in (select lookupId from Lookup where Name='Active')", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            Student_dataGridView.DataSource = dt;
        }
        private void InActiveStudentViews()
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd16 = new SqlCommand("Select FirstName, LastName, Contact, Email, RegistrationNumber from Student Where Status in (select lookupId from Lookup where Name='InActive')", con);
            SqlDataAdapter da16 = new SqlDataAdapter(cmd16);
            DataTable dt16 = new DataTable();
            da16.Fill(dt16);
            InActive_dataGridView1.DataSource = dt16;
        }
        private void CloView()
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd2 = new SqlCommand("Select Name, DateCreated, DateUpdated from Clo", con);
            SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
            DataTable dt2 = new DataTable();
            da2.Fill(dt2);
            Clo_dataGridView1.DataSource = dt2;
        }


         private void RubricView()
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd4 = new SqlCommand("Select Details, CloId from Rubric", con);
            SqlDataAdapter da4 = new SqlDataAdapter(cmd4);
            DataTable dt4 = new DataTable();
            da4.Fill(dt4);
            Rubric_dataGridView1.DataSource = dt4;
        }

        private void RubricLevelView()
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd5 = new SqlCommand("Select RubricId,Details,MeasurementLevel from RubricLevel", con);
            SqlDataAdapter da5 = new SqlDataAdapter(cmd5);
            DataTable dt5 = new DataTable();
            da5.Fill(dt5);
            RubricLevel_dataGridView1.DataSource = dt5;
        }

        private void AssessmentView()
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd6 = new SqlCommand("Select Title,DateCreated,TotalMarks,TotalWeightage from Assessment", con);
            SqlDataAdapter da6 = new SqlDataAdapter(cmd6);
            DataTable dt6 = new DataTable();
            da6.Fill(dt6);
            Assessment_dataGridView1.DataSource = dt6;
        }

         private void AssessmentComponentView()
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd7 = new SqlCommand("Select Name,RubricId,TotalMarks,DateCreated,DateUpdated,AssessmentId from AssessmentComponent", con);
            SqlDataAdapter da7 = new SqlDataAdapter(cmd7);
            DataTable dt7 = new DataTable();
            da7.Fill(dt7);
            ASS_Component_dataGridView1.DataSource = dt7;
        }


         private void AttendanceView()
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd8 = new SqlCommand("Select Id, RegistrationNumber, FirstName + ' ' + LastName As Name from Student Where Status IN (Select LookupId from Lookup where Name ='Active') ", con);
            SqlDataAdapter da8 = new SqlDataAdapter(cmd8);
            DataTable dt8 = new DataTable();
            da8.Fill(dt8);
            dataGridView1.DataSource = dt8;
            AddButtonsToDataGridView(dataGridView1);
        }


        private void Evaluation1()
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd17 = new SqlCommand("Select Id,RegistrationNumber, FirstName + ' ' + LastName AS Name from Student where Status IN (Select LookupId from Lookup where Name ='Active') ", con);
            SqlDataAdapter da17 = new SqlDataAdapter(cmd17);
            DataTable dt17 = new DataTable();
            da17.Fill(dt17);
            Evaluation_dataGridView2.DataSource = dt17;
        }

        private void Evaluation2()
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd18 = new SqlCommand("  Select   distinct S.Id, S.RegistrationNumber, S.FirstName+ ''+S.LastName As Name, SR.EvaluationDate, A.Title,SR.AssessmentComponentId, AC.TotalMarks,RL.MeasurementLevel, (Select max(MeasurementLevel) From RubricLevel) AS [Max Level],(convert(float,RL.MeasurementLevel) / MAX(RL.MeasurementLevel) OVER() * AC.TotalMarks) as ObtainedMarks \r\nFROM StudentResult SR\r\nJOIN Student S\r\nON S.Id = SR.StudentId\r\nJOIN AssessmentComponent AC\r\nON Ac.Id = SR.AssessmentComponentId\r\nJOIN Assessment A\r\nOn A.Id = AC.AssessmentId\r\nJOIN RubricLevel RL\r\nON Rl.Id = SR.RubricMeasurementId\r\nJOIN Rubric R\r\nON R.Id = AC.RubricId", con);
            SqlDataAdapter da18 = new SqlDataAdapter(cmd18);
            DataTable dt18 = new DataTable();
            da18.Fill(dt18);
            Evaluation_dataGridView3.DataSource = dt18;
        }



        private void AssessmentReportView()
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd19 = new SqlCommand("WITH NewTable AS (SELECT DISTINCT S.RegistrationNumber, S.FirstName+ ''+S.LastName AS Name, A.Title,A.TotalMarks, A.Totalweightage, (CONVERT(FLOAT,RL.MeasurementLevel) / MAX(RL.MeasurementLevel) OVER() * AC.TotalMarks) AS ObtainedMarks, ((CONVERT(FLOAT,RL.MeasurementLevel) / MAX(RL.MeasurementLevel) OVER() * AC.TotalMarks)/A.TotalMarks * A.TotalWeightage) AS ObtainedWeightage FROM  Student S JOIN StudentResult SR ON S.Id=SR.StudentId JOIN RubricLevel RL ON SR.RubricMeasurementId=RL.Id JOIN Rubric R ON RL.RubricId=R.Id JOIN AssessmentComponent AC ON R.Id=AC.RubricId JOIN Assessment A ON AC.AssessmentId=A.Id WHERE SR.StudentId = [StudentId] AND AC.Id = [AssessmentComponentId] AND A.Title ='" + AssessmentReport_comboBox1.Text + "') SELECT  NewTable.RegistrationNumber, NewTable.Name, NewTable.TotalMarks, SUM(NewTable.ObtainedMarks) AS ObtainedMarks, NewTable.TotalWeightage, SUM(NewTable.ObtainedWeightage) AS ObtainedWeightage FROM  NewTable GROUP BY NewTable.RegistrationNumber, NewTable.Name, NewTable.TotalMarks, NewTable.TotalWeightage", con);
            SqlDataAdapter da19 = new SqlDataAdapter(cmd19);
            DataTable dt19 = new DataTable();
            da19.Fill(dt19);
        }


        private void Count(){
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd9 = new SqlCommand("Select Count(Id) from Student Where status=5", con);
            AS_Count = Convert.ToInt32(cmd9.ExecuteScalar());
            SCount_materialLabel10.Text = "Active Students:" + AS_Count.ToString();

            SqlCommand cmd10 = new SqlCommand("Select Count(Id) from Student Where status=6", con);
            InAS_Count = Convert.ToInt32(cmd10.ExecuteScalar());
            SCountInActive_materialLabel11.Text = "Inactive Students:" + InAS_Count.ToString();

            SqlCommand cmd11 = new SqlCommand("Select Count(Id) from CLO", con);
            CLOCount = Convert.ToInt32(cmd11.ExecuteScalar());
            CLOCount_materialLabel15.Text = "CLO's:" + CLOCount.ToString();

            SqlCommand cmd12 = new SqlCommand("Select Count(Id) from Rubric", con);
            Rub_Count = Convert.ToInt32(cmd12.ExecuteScalar());
            Rubric_materialLabel16.Text = "Rubrics:" + Rub_Count.ToString();

            SqlCommand cmd13 = new SqlCommand("Select Count(Id) from RubricLevel", con);
            RubLevel_Count = Convert.ToInt32(cmd13.ExecuteScalar());
            RubricLevel_materialLabel17.Text = "rubric levels:" + RubLevel_Count.ToString();

            SqlCommand cmd14 = new SqlCommand("Select Count(Id) from Assessment", con);
            Assessment_Count = Convert.ToInt32(cmd14.ExecuteScalar());
            Assessment_materialLabel18.Text = "Assessments:" + Assessment_Count.ToString();

            SqlCommand cmd15 = new SqlCommand("Select Count(Id) from AssessmentComponent", con);
            AssessmentComponent_Count = Convert.ToInt32(cmd15.ExecuteScalar());
            AComponent_materialLabel14.Text = "Assessment Component:" + AssessmentComponent_Count.ToString();

            Report_materialLabel12.Text = "Reports:" + 6;
        }
        public void AttendanceGridCheckBoxes()
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Select Id,RegistrationNumber,FirstName+' '+LastName as Name from Student where Status in (select lookupId from Lookup where Name='Active')", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            DataGridViewCheckBoxColumn checkBox1 = new DataGridViewCheckBoxColumn();
            checkBox1.HeaderText = "Present";
            checkBox1.Width = 50;
            checkBox1.Name = "checkBoxColumn1";
            checkBox1.TrueValue = "Yes";
            checkBox1.FalseValue = "No";

            DataGridViewCheckBoxColumn checkBox2 = new DataGridViewCheckBoxColumn();
            checkBox2.HeaderText = "Absent";
            checkBox2.Width = 50;
            checkBox2.Name = "checkBoxColumn2";
            checkBox2.TrueValue = "On";
            checkBox2.FalseValue = "Off";

            DataGridViewCheckBoxColumn checkBox3 = new DataGridViewCheckBoxColumn();
            checkBox3.HeaderText = "Leave";
            checkBox3.Width = 50;
            checkBox3.Name = "checkBoxColumn3";
            checkBox3.TrueValue = "On";
            checkBox3.FalseValue = "Off";

            DataGridViewCheckBoxColumn checkBox4 = new DataGridViewCheckBoxColumn();
            checkBox4.HeaderText = "Late";
            checkBox4.Width = 50;
            checkBox4.Name = "checkBoxColumn4";
            checkBox4.TrueValue = "On";
            checkBox4.FalseValue = "Off";
            if (count == 0)
            {
                dataGridView1.DataSource = dt;

                dataGridView1.Columns.Add(checkBox1);
                dataGridView1.Columns.Add(checkBox2);
                dataGridView1.Columns.Add(checkBox3);
                dataGridView1.Columns.Add(checkBox4);
                count++;
            }
        }

        private void AddButtonsToDataGridView(DataGridView dataGridView)

        {

        }

        private void Rubric_CLOID_comboBox1_Click(object sender, EventArgs e)
        {
            Rubric_CLOID_comboBox1.Items.Clear();
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd2 = new SqlCommand("Select Id from Clo", con);
            DataTable dt3 = new DataTable();
            SqlDataAdapter da3 = new SqlDataAdapter(cmd2);
            da3.Fill(dt3);
            foreach (DataRow dr in dt3.Rows)
            {
                Rubric_CLOID_comboBox1.Items.Add(dr["Id"].ToString());
            }
        }

        private void RubricLevel_RubricId_comboBox1_Click(object sender, EventArgs e)
        {
            RubricLevel_RubricId_comboBox1.Items.Clear();
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd4 = new SqlCommand("Select Id from Rubric", con);
            DataTable dt6 = new DataTable();
            SqlDataAdapter da6 = new SqlDataAdapter(cmd4);
            da6.Fill(dt6);
            foreach (DataRow dr in dt6.Rows)
            {
                RubricLevel_RubricId_comboBox1.Items.Add(dr["Id"].ToString());
            }
        }

        private void ASS_ComponentRubricID_comboBox1_Click(object sender, EventArgs e)
        {
            ASS_ComponentRubricID_comboBox1.Items.Clear();
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd2 = new SqlCommand("Select Id from Rubric", con);
            DataTable dt3 = new DataTable();
            SqlDataAdapter da3 = new SqlDataAdapter(cmd2);
            da3.Fill(dt3);
            foreach (DataRow dr in dt3.Rows)
            {
                ASS_ComponentRubricID_comboBox1.Items.Add(dr["Id"].ToString());
            }
        }

        private void ASS_ComponentAssID_comboBox2_Click(object sender, EventArgs e)
        {
            ASS_ComponentAssID_comboBox2.Items.Clear();
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd2 = new SqlCommand("Select Id from Assessment", con);
            DataTable dt3 = new DataTable();
            SqlDataAdapter da3 = new SqlDataAdapter(cmd2);
            da3.Fill(dt3);
            foreach (DataRow dr in dt3.Rows)
            {
                ASS_ComponentAssID_comboBox2.Items.Add(dr["Id"].ToString());
            }
        }

        private void StudentWiseReport_comboBox1_Click(object sender, EventArgs e)
        {
            ASS_ComponentAssID_comboBox2.Items.Clear();
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd2 = new SqlCommand("Select RegistrationNumber From Student", con);
            DataTable dt3 = new DataTable();
            SqlDataAdapter da3 = new SqlDataAdapter(cmd2);
            da3.Fill(dt3);
            foreach (DataRow dr in dt3.Rows)
            {
                StudentWiseReport_comboBox1.Items.Add(dr["RegistrationNumber"].ToString());
            }
        }

        private void evaluationRubricMeasurementLevel_comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void evaluationAssessmentComponentID_comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            evaluationRubricMeasurementLevel_comboBox1.Items.Clear();
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd2 = new SqlCommand("Select distinct RL.Id from RubricLevel RL join Rubric R on  R.Id=RL.RubricId join AssessmentComponent AC on R.Id=AC.RubricId Where AC.Id="+ int.Parse(evaluationAssessmentComponentID_comboBox2.Text), con);
            DataTable dt3 = new DataTable();
            SqlDataAdapter da3 = new SqlDataAdapter(cmd2);
            da3.Fill(dt3);

            foreach (DataRow dr in dt3.Rows)
            {
                evaluationRubricMeasurementLevel_comboBox1.Items.Add(dr["Id"].ToString());
            }
        }

        public static bool IsValidName(string name)
        {
            string pattern = "^[a-zA-Z ]+$";
            return Regex.IsMatch(name, pattern);
        }

        public bool IsValidLastName(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return true;
            }
            string pattern = "^[a-zA-Z ]+$";
            return Regex.IsMatch(input, pattern);
        }

        public  bool IsPhoneNumber(string phoneNumber)
        {
            Regex regex = new Regex(@"^(03[0-9]{2}-?[0-9]{7})?$");
            if (!regex.IsMatch(phoneNumber))
            {
                MessageBox.Show("Please Enter Correct Phone number i.e starting with 03 and containing total 11 digits ", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return false;
            }
            return true;
        }
        public bool IsValidEmail(string email)
        {
            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, pattern);
        }
        private bool ValidateRegistrationNumber(string registrationNumber)
        {
            Regex regex = new Regex(@"^(20[0-1][0-9]|202[0-3])-[A-Za-z]{2,}-\d{1,}$");
            if (!regex.IsMatch(registrationNumber))
            {
                MessageBox.Show("Please enter a valid registration number.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
        public static bool ValidateCLOName(string name)
        {
            string pattern = "^[a-zA-Z]+[a-zA-Z0-9]*$";
            return Regex.IsMatch(name, pattern);
        }

        public bool ValidateWeightage(string value)
        {
            try
            {
                int marks = int.Parse(value);
                if (marks > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }


        private void S_AddButton_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(S_RegNo_textBox.Text) || String.IsNullOrEmpty(S_FirstName_textBox.Text) || String.IsNullOrEmpty(S_Email_textBox.Text))
            {

                MessageBox.Show("Please enter all input values", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    
                    var con = Configuration.getInstance().getConnection();
                    SqlCommand command = new SqlCommand("Select count(*) from student where RegistrationNumber ='"+ S_RegNo_textBox.Text + "'", con);
                    int count=Convert.ToInt32(command.ExecuteScalar());
                    SqlCommand command1 = new SqlCommand("Select count(*) from student where Email ='" + S_Email_textBox.Text + "'", con);
                    int count1 = Convert.ToInt32(command1.ExecuteScalar());
                if (count == 0)
                {
                    if (count1 == 0)
                    {
                        SqlCommand cmd = new SqlCommand("Insert into Student values (@FirstName, @LastName, @Contact, @Email, @RegistrationNumber, @Status)", con);
                        if (IsValidName(S_FirstName_textBox.Text) == true)
                        {

                            cmd.Parameters.AddWithValue("@FirstName", S_FirstName_textBox.Text);

                                if (IsValidLastName(S_LastNametextBox.Text) == true)
                                {

                                    cmd.Parameters.AddWithValue("@LastName", S_LastNametextBox.Text);
                                    if (ValidateRegistrationNumber(S_RegNo_textBox.Text) == true)
                                    {
                                        cmd.Parameters.AddWithValue("@RegistrationNumber", S_RegNo_textBox.Text);
                                        if (IsPhoneNumber(S_contact_textBox.Text) == true)
                                        {
                                            cmd.Parameters.AddWithValue("@Contact", S_contact_textBox.Text);
                                            if (IsValidEmail(S_Email_textBox.Text) == true)
                                            {
                                                cmd.Parameters.AddWithValue("@Email", S_Email_textBox.Text);
                                            }
                                            else
                                            {
                                                MessageBox.Show("Please Enter Correct Email i.e containing ..@ and .com", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                            }
                                        }
                                        
                                       
                                        
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Please Enter Correct LastName i.e containing only alphabets", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                        else
                        {
                                MessageBox.Show("Please Enter Correct FirstName i.e containing alphabets", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }





                        SqlCommand cmd2 = new SqlCommand("Select LookupId From Lookup Where Name='Active'", con);
                        Student_Status = (Int32)cmd2.ExecuteScalar();
                        cmd2.ExecuteNonQuery();
                        cmd.Parameters.AddWithValue("@Status", Student_Status);
                        cmd.ExecuteNonQuery();
                        Count();
                        MessageBox.Show("Data has been saved Successfully", "Insertion Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ActiveStudentsView();
                            InActiveStudentViews();
                            CloView();
                            RubricView();
                            RubricLevelView();
                            AssessmentView();
                            AssessmentComponentView();
                            AttendanceView();
                            Evaluation1();
                            Evaluation2();
                            AssessmentReportView();
                            AttendanceGridCheckBoxes();
                            S_RegNo_textBox.Clear();
                        S_FirstName_textBox.Clear();   
                        S_LastNametextBox.Clear();
                        S_contact_textBox.Clear();
                        S_Email_textBox.Clear();
                    }
                    else
                    {
                            MessageBox.Show("Email Duplication is not allowed", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                }
                else
                {
                        MessageBox.Show("Registration Number Duplication is not allowed", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    //MessageBox.Show(ex.Message);
                }
            }

            
        }

        private void S_UpdateButton_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(S_RegNo_textBox.Text) || String.IsNullOrEmpty(S_FirstName_textBox.Text) || String.IsNullOrEmpty(S_Email_textBox.Text))
            {

                MessageBox.Show("Please enter all input values", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {

                    var con = Configuration.getInstance().getConnection();


                    SqlCommand cmd3 = new SqlCommand("select Id from student where Id<>@Id and  RegistrationNumber=@reg", con);
                    cmd3.Parameters.AddWithValue("@Id", MYID);
                    cmd3.Parameters.AddWithValue("@reg", S_RegNo_textBox.Text);
                    
                    int count = Convert.ToInt32(cmd3.ExecuteScalar());
                    SqlCommand cmd4 = new SqlCommand("select Id from student where  Id<>@Id and Email='" + S_Email_textBox.Text + "'", con);
                    cmd4.Parameters.AddWithValue("@Id", MYID);
                    int count1 = Convert.ToInt32(cmd4.ExecuteScalar());

                    if (count == 0)
                    {
                        if (count1 == 0)
                        {
                            SqlCommand cmd = new SqlCommand("UPDATE student SET  FirstName= @FirstName, LastName = @LastName, Contact=@Contact, Email=@Email, RegistrationNumber= @RegistrationNumber, Status=5 WHERE Id=@Id", con);
                            if (IsValidName(S_FirstName_textBox.Text) == true)
                            {

                                cmd.Parameters.AddWithValue("@FirstName",S_FirstName_textBox.Text);
                                if (IsValidLastName(S_LastNametextBox.Text) == true)
                                {

                                    cmd.Parameters.AddWithValue("@LastName", S_LastNametextBox.Text);
                                    if (ValidateRegistrationNumber(S_RegNo_textBox.Text) == true)
                                    {
                                        cmd.Parameters.AddWithValue("@RegistrationNumber", S_RegNo_textBox.Text);
                                        if (IsPhoneNumber(S_contact_textBox.Text) == true)
                                        {
                                            cmd.Parameters.AddWithValue("@Contact", S_contact_textBox.Text);
                                            if (IsValidEmail(S_Email_textBox.Text) == true)
                                            {
                                                cmd.Parameters.AddWithValue("@Email", S_Email_textBox.Text);
                                            }
                                            else
                                            {
                                                MessageBox.Show("");
                                                MessageBox.Show("Please Enter Correct Email i.e containing @ and .com ", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Please Enter Correct LastName i.e containing alphabets ", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                            else
                            {
                                MessageBox.Show("Please Enter Correct FirstName i.e containing alphabets ", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }


                            SqlCommand cmd2 = new SqlCommand("Select LookupId From Lookup Where Name='Active'", con);
                            Student_Status = (Int32)cmd2.ExecuteScalar();
                            cmd2.ExecuteNonQuery();
                            cmd.Parameters.AddWithValue("@Status", Student_Status);
                            cmd.Parameters.AddWithValue("@Id", MYID);
                            cmd.ExecuteNonQuery();
                            Count();
                            MessageBox.Show("Data has been updated Successfully", "Updation Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            S_UpdateButton.Enabled = false;
                            S_DeleteButton.Enabled = false;
                            ActiveStudentsView();
                            InActiveStudentViews();
                            CloView();
                            RubricView();
                            RubricLevelView();
                            AssessmentView();
                            AssessmentComponentView();
                            AttendanceView();
                            Evaluation1();
                            Evaluation2();
                            AssessmentReportView();
                            AttendanceGridCheckBoxes();
                            S_RegNo_textBox.Clear();
                            S_FirstName_textBox.Clear();
                            S_LastNametextBox.Clear();
                            S_contact_textBox.Clear();
                            S_Email_textBox.Clear();
                        }
                        else
                        {
                            MessageBox.Show("Email Duplication is not allowed", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Registration Number Duplication is not allowed", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    //MessageBox.Show(ex.Message);
                }
            }
            




        }

        private void Student_dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void S_DeleteButton_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(S_RegNo_textBox.Text) || String.IsNullOrEmpty(S_FirstName_textBox.Text) || String.IsNullOrEmpty(S_Email_textBox.Text))
            {

                MessageBox.Show("Please enter all input values", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    var con = Configuration.getInstance().getConnection();
                    SqlCommand cmd = new SqlCommand("UPDATE student SET Status=6 WHERE Id=@Id", con);

                    SqlCommand cmd2 = new SqlCommand("Select LookupId From Lookup Where Name='InActive'", con);
                    Student_Status = (Int32)cmd2.ExecuteScalar();
                    cmd2.ExecuteNonQuery();
                    cmd.Parameters.AddWithValue("@Status", Student_Status);
                    cmd.Parameters.AddWithValue("@Id", MYID);
                    cmd.ExecuteNonQuery();
                    Count();
                    MessageBox.Show("Data has been deleted Successfully", "Deletion Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    S_UpdateButton.Enabled = false;
                    S_DeleteButton.Enabled = false;
                    ActiveStudentsView();
                    InActiveStudentViews();
                    CloView();
                    RubricView();
                    RubricLevelView();
                    AssessmentView();
                    AssessmentComponentView();
                    AttendanceView();
                    Evaluation1();
                    Evaluation2();
                    AssessmentReportView();
                    AttendanceGridCheckBoxes();
                    InActiveStudentViews();
                    S_RegNo_textBox.Clear();
                    S_FirstName_textBox.Clear();
                    S_LastNametextBox.Clear();
                    S_contact_textBox.Clear();
                    S_Email_textBox.Clear();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }



        }
        //CLO add
        private void materialButton1_Click(object sender, EventArgs e)
        {
            var con = Configuration.getInstance().getConnection();
            if (String.IsNullOrEmpty(CLOName_textBox2.Text))
    
            {
                // Input textbox is empty
                MessageBox.Show("Please enter all input values", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {

                    SqlCommand command = new SqlCommand("Select count(*) from Clo where Name ='" + CLOName_textBox2.Text + "'", con);
                    int count = Convert.ToInt32(command.ExecuteScalar());

                    if (count == 0)
                    {
                        SqlCommand cmd = new SqlCommand("Insert into Clo values (@Name, @DateCreated, @DateUpdated)", con);
                        if (ValidateCLOName(CLOName_textBox2.Text)== true)
                        {
                            cmd.Parameters.AddWithValue("@Name", CLOName_textBox2.Text);
                        }
                        else
                        {
                            MessageBox.Show("Please enter correct name containing only alphabets or alphabets concatenating with numbers", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                        DateTime date = DateTime.Now;
                        cmd.Parameters.AddWithValue("@DateCreated", date);
                        cmd.Parameters.AddWithValue("@DateUpdated", date);
                        cmd.ExecuteNonQuery();
                        Count();
                        MessageBox.Show("Data has been saved Successfully", "Insertion Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ActiveStudentsView();
                        InActiveStudentViews();
                        CloView();
                        RubricView();
                        RubricLevelView();
                        AssessmentView();
                        AssessmentComponentView();
                        AttendanceView();
                        Evaluation1();
                        Evaluation2();
                        AssessmentReportView();
                        AttendanceGridCheckBoxes();
                        CLOName_textBox2.Clear();
                    }
                    else
                    {
                        MessageBox.Show("CLO name Duplication is not allowed", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                catch (Exception ex)
                {
                    //MessageBox.Show(ex.Message);
                }
            }

            

        }
        //CLO update
        private void materialButton3_Click(object sender, EventArgs e)
        {


            var con = Configuration.getInstance().getConnection();
            if (String.IsNullOrEmpty(CLOName_textBox2.Text))

            {

                MessageBox.Show("Please enter all input values", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    SqlCommand command = new SqlCommand("Select count(*) from Clo where Name ='" + CLOName_textBox2.Text + "'", con);
                    int count = Convert.ToInt32(command.ExecuteScalar());

                    if (count == 0)
                    {

                        SqlCommand cmd = new SqlCommand("UPDATE Clo SET Name=@Name,DateUpdated=@DateUpdated WHERE Id=@Id", con);
                        if (ValidateCLOName(CLOName_textBox2.Text) == true)
                        {
                            cmd.Parameters.AddWithValue("@Name", CLOName_textBox2.Text);
                        }
                        else
                        {
                            MessageBox.Show("Please enter correct name containing only alphabets or alphabets concatenating with numbers", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        DateTime date = DateTime.Now;
                        cmd.Parameters.AddWithValue("@DateUpdated", date);
                        cmd.Parameters.AddWithValue("@Id", CLOID);
                        cmd.ExecuteNonQuery();
                        Count();
                        MessageBox.Show("Data has been updated Successfully", "Updation Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        materialButton3.Enabled = false;
                        materialButton2.Enabled = false;
                        ActiveStudentsView();
                        InActiveStudentViews();
                        CloView();
                        RubricView();
                        RubricLevelView();
                        AssessmentView();
                        AssessmentComponentView();
                        AttendanceView();
                        Evaluation1();
                        Evaluation2();
                        AssessmentReportView();
                        AttendanceGridCheckBoxes();
                        CLOName_textBox2.Clear();

                    }
                    else
                    {
                        MessageBox.Show("Clo duplication is not allowed", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    //MessageBox.Show(ex.Message);
                }
            }

        }
        //Clo delete
        private void materialButton2_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(CLOName_textBox2.Text))

            {

                MessageBox.Show("You have not selected credientials for deletion", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    var con = Configuration.getInstance().getConnection();
                    //SqlCommand temp3 = new SqlCommand("Delete FROM StudentResult WHERE AssessmentComponentId In (Select Id From AssessmentComponent WHERE RubricId In (Select Id From Rubric Where CloId = @Id) )", con);
                    SqlCommand temp3 = new SqlCommand("Delete FROM StudentResult WHERE AssessmentComponentId In (Select Id From AssessmentComponent WHERE RubricId In (Select Id From Rubric Where CloId = @Id) )", con);
                    temp3.Parameters.AddWithValue("@Id", CLOID);
                    temp3.ExecuteNonQuery();
                    SqlCommand temp2 = new SqlCommand("Delete FROM AssessmentComponent WHERE RubricId In (Select Id From Rubric Where CloId = @Id)", con);
                    temp2.Parameters.AddWithValue("@Id", CLOID);
                    temp2.ExecuteNonQuery();
                    SqlCommand temp1 = new SqlCommand("Delete FROM RubricLevel WHERE RubricId In (Select Id From Rubric Where CloId = @Id)", con);
                    temp1.Parameters.AddWithValue("@Id", CLOID);
                    temp1.ExecuteNonQuery();
                    SqlCommand temp = new SqlCommand("Delete FROM Rubric WHERE CloId = @Id", con);
                    temp.Parameters.AddWithValue("@Id", CLOID);
                    temp.ExecuteNonQuery();
                    SqlCommand cmd = new SqlCommand("Delete FROM Clo WHERE Id= @Id", con);
                    cmd.Parameters.AddWithValue("@Id", CLOID);
                    cmd.ExecuteNonQuery();
                    Count();
                    MessageBox.Show("Data has been deleted Successfully", "Deletion Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    materialButton3.Enabled = false;
                    materialButton2.Enabled = false;
                    CLOName_textBox2.Clear();
                    ActiveStudentsView();
                    InActiveStudentViews();
                    CloView();
                    RubricView();
                    RubricLevelView();
                    AssessmentView();
                    AssessmentComponentView();
                    AttendanceView();
                    Evaluation1();
                    Evaluation2();
                    AssessmentReportView();
                    AttendanceGridCheckBoxes();


                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }


 
        }

        private void Clo_dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        //Rubric  
        private void RubricAdd_materialButton4_Click(object sender, EventArgs e)
        {
            var con = Configuration.getInstance().getConnection();
            if (String.IsNullOrEmpty(RubricDetails_richTextBox1.Text) || String.IsNullOrEmpty(Rubric_CLOID_comboBox1.Text))

            {
                // Input textbox is empty
                MessageBox.Show("Please enter all input values", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {

                    SqlCommand temp = new SqlCommand("Select Max(Id) from Rubric", con);
                    int abc = 0;
                    try
                    {
                        abc = (Int32)temp.ExecuteScalar();
                    }
                    catch
                    {
                        abc = 0;
                    }
                    SqlCommand command = new SqlCommand("Select count(*) from Rubric where Details ='" + RubricDetails_richTextBox1.Text + "'", con);
                    int count = Convert.ToInt32(command.ExecuteScalar());
                    if (count == 0)
                    {
                        SqlCommand cmd = new SqlCommand("INSERT INTO Rubric VALUES(@Id,@Details, @CloId);", con);
                        cmd.Parameters.AddWithValue("@Details", RubricDetails_richTextBox1.Text);
                        cmd.Parameters.AddWithValue("@CloId", int.Parse(Rubric_CLOID_comboBox1.Text));
                        cmd.Parameters.AddWithValue("@Id", abc + 1);

                        cmd.ExecuteNonQuery();
                        Count();
                        MessageBox.Show("Data has been saved Successfully", "Insertion Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ActiveStudentsView();
                        InActiveStudentViews();
                        CloView();
                        RubricView();
                        RubricLevelView();
                        AssessmentView();
                        AssessmentComponentView();
                        AttendanceView();
                        Evaluation1();
                        Evaluation2();
                        AssessmentReportView();
                        AttendanceGridCheckBoxes();
                        RubricDetails_richTextBox1.Clear();
                        Rubric_CLOID_comboBox1.Items.Clear();
                        Rubric_CLOID_comboBox1.ResetText();
                    }
                    else
                    {
                        MessageBox.Show("Duplication of rubric is not allowed", "Insertion Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }



        }

        private void RubricUpdate_materialButton6_Click(object sender, EventArgs e)
        {
            var con = Configuration.getInstance().getConnection();
            if (String.IsNullOrEmpty(RubricDetails_richTextBox1.Text) || String.IsNullOrEmpty(Rubric_CLOID_comboBox1.Text))

            {
                MessageBox.Show("Please enter all input values", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    SqlCommand command = new SqlCommand("Select count(*) from Rubric where Details ='" + RubricDetails_richTextBox1.Text + "' and Id!="+RubricID, con);
                    int count = Convert.ToInt32(command.ExecuteScalar());
                    if (count == 0)
                    {
                        SqlCommand cmd = new SqlCommand("UPDATE Rubric SET Details=@Details, CloId=@CloId WHERE Id=@Id", con);
                        cmd.Parameters.AddWithValue("@Details", RubricDetails_richTextBox1.Text);
                        cmd.Parameters.AddWithValue("@CloId", Rubric_CLOID_comboBox1.Text);
                        cmd.Parameters.AddWithValue("@Id", RubricID);
                        cmd.ExecuteNonQuery();
                        Count();
                        MessageBox.Show("Data has been updated Successfully", "Updation Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        RubricUpdate_materialButton6.Enabled = false;
                        RubricDelete_materialButton5.Enabled = false;
                        ActiveStudentsView();
                        InActiveStudentViews();
                        CloView();
                        RubricView();
                        RubricLevelView();
                        AssessmentView();
                        AssessmentComponentView();
                        AttendanceView();
                        Evaluation1();
                        Evaluation2();
                        AssessmentReportView();
                        AttendanceGridCheckBoxes();
                        RubricDetails_richTextBox1.Clear();
                        Rubric_CLOID_comboBox1.Items.Clear();
                        Rubric_CLOID_comboBox1.ResetText();
                    }
                    else
                    {
                        MessageBox.Show("Duplication of Rubric is not allowed", "Updation Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }




        }

        private void Rubric_dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void RubricDelete_materialButton5_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(RubricDetails_richTextBox1.Text) || String.IsNullOrEmpty(Rubric_CLOID_comboBox1.Text))

            {
                MessageBox.Show("You have not selected credientials for deletion", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    var con = Configuration.getInstance().getConnection();

                    SqlCommand temp3 = new SqlCommand("Delete FROM StudentResult WHERE AssessmentComponentId In (Select Id From AssessmentComponent WHERE RubricId In (Select Id From Rubric Where RubricId = @Id) )", con);
                    temp3.Parameters.AddWithValue("@Id", RubricID);
                    temp3.ExecuteNonQuery();
                    SqlCommand temp2 = new SqlCommand("Delete FROM AssessmentComponent WHERE RubricId In (Select Id From Rubric Where RubricId = @Id)", con);
                    temp2.Parameters.AddWithValue("@Id", RubricID);
                    temp2.ExecuteNonQuery();
                    SqlCommand temp1 = new SqlCommand("Delete FROM RubricLevel WHERE RubricId=@RubricId", con);
                    temp1.Parameters.AddWithValue("@RubricId", RubricID);
                    temp1.ExecuteNonQuery();
                    SqlCommand cmd = new SqlCommand("Delete FROM Rubric WHERE Id=@Id", con);
                    cmd.Parameters.AddWithValue("@Id", RubricID);
                    cmd.ExecuteNonQuery();
                    Count();
                    MessageBox.Show("Data has been deleted Successfully", "Deletion Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    RubricUpdate_materialButton6.Enabled = false;
                    RubricDelete_materialButton5.Enabled = false;
                    ActiveStudentsView();
                    InActiveStudentViews();
                    CloView();
                    RubricView();
                    RubricLevelView();
                    AssessmentView();
                    AssessmentComponentView();
                    AttendanceView();
                    Evaluation1();
                    Evaluation2();
                    AssessmentReportView();
                    AttendanceGridCheckBoxes();
                    RubricDetails_richTextBox1.Clear();
                    Rubric_CLOID_comboBox1.Items.Clear();
                    Rubric_CLOID_comboBox1.ResetText();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }


        }

        private void S_Reg_N0_materialLabel_Click(object sender, EventArgs e)
        {

        }

        private void Rubric_CLOID_comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        //Rubric Levels
        //AddRubricLevel
        private void materialButton4_Click(object sender, EventArgs e)
        {
            var con = Configuration.getInstance().getConnection();
            if (String.IsNullOrEmpty(RubricLevel_RubricId_comboBox1.Text) || String.IsNullOrEmpty(RubricLevel_Details_richTextBox1.Text) 
                || String.IsNullOrEmpty(RubricLevel_comboBox2.Text))

            {
                MessageBox.Show("Please enter all input values", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    SqlCommand command1 = new SqlCommand("Select count(*) from RubricLevel where RubricID=" + int.Parse(RubricLevel_RubricId_comboBox1.Text), con);
                    int count1 = Convert.ToInt32(command1.ExecuteScalar());
                    SqlCommand command = new SqlCommand("Select count(*) from RubricLevel where RubricID=" + int.Parse(RubricLevel_RubricId_comboBox1.Text) + " and Details = '" + RubricLevel_Details_richTextBox1.Text + "'", con);
                    int count = Convert.ToInt32(command.ExecuteScalar());

                    if (count1 < 4)
                    {

                        if (count == 0)
                        {
                            SqlCommand cmd = new SqlCommand("INSERT INTO RubricLevel VALUES (@RubricId,@Details, @MeasurementLevel);", con);
                            cmd.Parameters.AddWithValue("@RubricId", RubricLevel_RubricId_comboBox1.Text);
                            cmd.Parameters.AddWithValue("@Details", RubricLevel_Details_richTextBox1.Text);
                            int temp = 0;
                            if (RubricLevel_comboBox2.Text == "Exceptional")
                            {
                                temp = 4;
                                RubricLevel_comboBox2.Text = "4";
                            }
                            if (RubricLevel_comboBox2.Text == "Good")
                            {
                                temp = 3;
                                RubricLevel_comboBox2.Text = "3";
                            }
                            if (RubricLevel_comboBox2.Text == "Fair")
                            {
                                temp = 2;
                                RubricLevel_comboBox2.Text = "2";
                            }
                            if (RubricLevel_comboBox2.Text == "Unsatisfactory")
                            {
                                temp = 1;
                                RubricLevel_comboBox2.Text = "1";
                            }
                            cmd.Parameters.AddWithValue("@MeasurementLevel", temp);
                            cmd.ExecuteNonQuery();
                            Count();
                            MessageBox.Show("Data has been saved Successfully", "Insertion Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ActiveStudentsView();
                            InActiveStudentViews();
                            CloView();
                            RubricView();
                            RubricLevelView();
                            AssessmentView();
                            AssessmentComponentView();
                            AttendanceView();
                            Evaluation1();
                            Evaluation2();
                            AssessmentReportView();
                            AttendanceGridCheckBoxes();
                            RubricLevel_RubricId_comboBox1.Items.Clear();
                            RubricLevel_RubricId_comboBox1.ResetText();
                            RubricLevel_Details_richTextBox1.Clear();
                            RubricLevel_comboBox2.ResetText();
                        }
                        else
                        {
                            MessageBox.Show("Duplication of rubric level details in same rubric is not allowed", "Insertion Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("You cannot add more than four rubric levels in one rubric", "Insertion Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    //MessageBox.Show(ex.Message);
                }
        }


        }
        //UpdateRubricLevel
        private void materialButton6_Click(object sender, EventArgs e)
        {
            var con = Configuration.getInstance().getConnection();
            if (String.IsNullOrEmpty(RubricLevel_RubricId_comboBox1.Text) || String.IsNullOrEmpty(RubricLevel_Details_richTextBox1.Text)
                || String.IsNullOrEmpty(RubricLevel_comboBox2.Text))

            {
                MessageBox.Show("Please enter all input values", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    if (RubricLevel_comboBox2.Text == "Exceptional")
                    {
                        RubricLevel_comboBox2.Text = "4";
                    }
                    if (RubricLevel_comboBox2.Text == "Good")
                    {
                        RubricLevel_comboBox2.Text = "3";
                    }
                    if (RubricLevel_comboBox2.Text == "Fair")
                    {
                        RubricLevel_comboBox2.Text = "2";
                    }
                    if (RubricLevel_comboBox2.Text == "Unsatisfactory")
                    {
                        RubricLevel_comboBox2.Text = "1";
                    }
                    SqlCommand command1 = new SqlCommand("Select count(*) from RubricLevel where RubricID=" + int.Parse(RubricLevel_RubricId_comboBox1.Text) + " and Id!=" + RubricLevelID, con);
                    int count1 = Convert.ToInt32(command1.ExecuteScalar());
                    SqlCommand command = new SqlCommand("Select count(*) from RubricLevel where RubricID=" + int.Parse(RubricLevel_RubricId_comboBox1.Text) + " and Details = '" + RubricLevel_Details_richTextBox1.Text + "' and Id!=" + RubricLevelID, con);
                    int count = Convert.ToInt32(command.ExecuteScalar());
                    if (count1 < 4)
                    {
                        if (count == 0)
                        {
                            SqlCommand cmd = new SqlCommand("UPDATE RubricLevel SET RubricId=@RubricId,Details=@Details, MeasurementLevel=@MeasurementLevel WHERE Id=@Id", con);
                            cmd.Parameters.AddWithValue("@RubricId", RubricLevel_RubricId_comboBox1.Text);
                            cmd.Parameters.AddWithValue("@Details", RubricLevel_Details_richTextBox1.Text);

                            cmd.Parameters.AddWithValue("@MeasurementLevel", int.Parse(RubricLevel_comboBox2.Text));
                            cmd.Parameters.AddWithValue("@Id", RubricLevelID);
                            cmd.ExecuteNonQuery();
                            Count();
                            MessageBox.Show("Data has been updated Successfully", "Updation Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            materialButton6.Enabled = false;
                            materialButton5.Enabled = false;

                            RubricLevel_RubricId_comboBox1.Items.Clear();
                            RubricLevel_RubricId_comboBox1.ResetText();
                            RubricLevel_Details_richTextBox1.Clear();
                            RubricLevel_comboBox2.ResetText();
                            ActiveStudentsView();
                            InActiveStudentViews();
                            CloView();
                            RubricView();
                            RubricLevelView();
                            AssessmentView();
                            AssessmentComponentView();
                            AttendanceView();
                            Evaluation1();
                            Evaluation2();
                            AssessmentReportView();
                            AttendanceGridCheckBoxes();
                        }
                        else
                        {
                            MessageBox.Show("Duplication of rubric details is not allowed", "Updation Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("You cannot add more than four rubric levels in one rubric", "Updation Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

        }
        //DeleteRubricLevel
        private void materialButton5_Click(object sender, EventArgs e)
        {

            var con = Configuration.getInstance().getConnection();
            if (String.IsNullOrEmpty(RubricLevel_RubricId_comboBox1.Text) || String.IsNullOrEmpty(RubricLevel_Details_richTextBox1.Text)
             || String.IsNullOrEmpty(RubricLevel_comboBox2.Text))

            {
                MessageBox.Show("You have not selected credientials for deletion", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    SqlCommand temp3 = new SqlCommand("Delete FROM StudentResult WHERE RubricMeasurementId =@Id", con);
                    temp3.Parameters.AddWithValue("@Id", RubricLevelID);
                    temp3.ExecuteNonQuery();

                    SqlCommand cmd = new SqlCommand("Delete FROM RubricLevel WHERE Id = @Id", con);
                    cmd.Parameters.AddWithValue("@RubricId", RubricLevel_RubricId_comboBox1.Text);
                    cmd.Parameters.AddWithValue("@Details", RubricLevel_Details_richTextBox1.Text);
                    if (RubricLevel_comboBox2.Text == "Exceptional")
                    {
                        RubricLevel_comboBox2.Text = "4";
                    }
                    if (RubricLevel_comboBox2.Text == "Good")
                    {
                        RubricLevel_comboBox2.Text = "3";
                    }
                    if (RubricLevel_comboBox2.Text == "Fair")
                    {
                        RubricLevel_comboBox2.Text = "2";
                    }
                    if (RubricLevel_comboBox2.Text == "Unsatisfactory")
                    {
                        RubricLevel_comboBox2.Text = "1";
                    }
                    cmd.Parameters.AddWithValue("@MeasurementLevel", int.Parse(RubricLevel_comboBox2.Text));
                    cmd.Parameters.AddWithValue("@Id", RubricLevelID);
                    cmd.ExecuteNonQuery();
                    Count();
                    MessageBox.Show("Data has been deleted Successfully", "Deletion Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    materialButton6.Enabled = false;
                    materialButton5.Enabled = false;

                    RubricLevel_RubricId_comboBox1.Items.Clear();
                    RubricLevel_RubricId_comboBox1.ResetText();
                    RubricLevel_Details_richTextBox1.Clear();
                    RubricLevel_comboBox2.Items.Clear();
                    RubricLevel_comboBox2.ResetText();
                    ActiveStudentsView();
                    InActiveStudentViews();
                    CloView();
                    RubricView();
                    RubricLevelView();
                    AssessmentView();
                    AssessmentComponentView();
                    AttendanceView();
                    Evaluation1();
                    Evaluation2();
                    AssessmentReportView();
                    AttendanceGridCheckBoxes();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            
        }

        private void RubricLevel_dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }



        private void tableLayoutPanel18_Paint(object sender, PaintEventArgs e)
        {

        }

        private void S_sorting_materialComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (S_sorting_materialComboBox1.Text == "First Name")
            {
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("Select FirstName, LastName, Contact, Email, RegistrationNumber from Student Where Status= 5 Order By FirstName ", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                Student_dataGridView.DataSource = dt;
            }

            else if (S_sorting_materialComboBox1.Text == "Last Name")
            {
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("Select FirstName, LastName, Contact, Email, RegistrationNumber from Student Where Status= 5 Order By LastName ", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                Student_dataGridView.DataSource = dt;
            }

            else if (S_sorting_materialComboBox1.Text == "Contact")
            {
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("Select FirstName, LastName, Contact, Email, RegistrationNumber from Student Where Status= 5 Order By Contact", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                Student_dataGridView.DataSource = dt;
            }
            else if (S_sorting_materialComboBox1.Text == "Registration Number")
            {
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("Select FirstName, LastName, Contact, Email, RegistrationNumber from Student Where Status= 5 Order By RegistrationNumber ", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                Student_dataGridView.DataSource = dt;
            }
            else
            {
                ActiveStudentsView();
            }


        }

        private void Student_dataGridView_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex >= 0)
            {
                try
                {
                    S_UpdateButton.Enabled = true;
                    S_DeleteButton.Enabled = true;
                    Student_dataGridView.CurrentRow.Selected = true;
                    S_RegNo_textBox.Text = Student_dataGridView.Rows[e.RowIndex].Cells["RegistrationNumber"].Value.ToString();
                    S_FirstName_textBox.Text = Student_dataGridView.Rows[e.RowIndex].Cells["FirstName"].Value.ToString();
                    S_LastNametextBox.Text = Student_dataGridView.Rows[e.RowIndex].Cells["LastName"].Value.ToString();
                    S_contact_textBox.Text = Student_dataGridView.Rows[e.RowIndex].Cells["Contact"].Value.ToString();
                    S_Email_textBox.Text = Student_dataGridView.Rows[e.RowIndex].Cells["Email"].Value.ToString();
                    //MYID = int.Parse(Student_dataGridView.Rows[e.RowIndex].Cells["Id"].Value.ToString());
                    var con = Configuration.getInstance().getConnection();
                    SqlCommand cmd = new SqlCommand("select Id from Student where RegistrationNumber=@RegistrationNumber", con);
                    cmd.Parameters.AddWithValue("RegistrationNumber", S_RegNo_textBox.Text);
                    MYID = Convert.ToInt32(cmd.ExecuteScalar());
                }
                catch (Exception exp)
                {
                    MessageBox.Show(exp.Message);
                }
            }
        }

        private void Clo_dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex >= 0)
            {
                try
                {
                    materialButton3.Enabled = true;
                    materialButton2.Enabled = true;
                    Clo_dataGridView1.CurrentRow.Selected = true;
                    CLOName_textBox2.Text = Clo_dataGridView1.Rows[e.RowIndex].Cells["Name"].Value.ToString();
                    //CLOID = int.Parse(Clo_dataGridView1.Rows[e.RowIndex].Cells["Id"].Value.ToString());

                    var con = Configuration.getInstance().getConnection();
                    SqlCommand cmd = new SqlCommand("select Id from Clo where Name=@Name", con);
                    cmd.Parameters.AddWithValue("Name", CLOName_textBox2.Text);
                    CLOID = Convert.ToInt32(cmd.ExecuteScalar());
                }
                catch (Exception exp)
                {
                    MessageBox.Show(exp.Message);
                }
            }
        }

        private void Rubric_dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex >= 0)
            {
                try
                {
                    RubricUpdate_materialButton6.Enabled = true;
                    RubricDelete_materialButton5.Enabled = true;
                    Rubric_dataGridView1.CurrentRow.Selected = true;
                    RubricDetails_richTextBox1.Text = Rubric_dataGridView1.Rows[e.RowIndex].Cells["Details"].Value.ToString();
                    Rubric_CLOID_comboBox1.Text = Rubric_dataGridView1.Rows[e.RowIndex].Cells["CloId"].Value.ToString();
                    //RubricID = int.Parse(Rubric_dataGridView1.Rows[e.RowIndex].Cells["Id"].Value.ToString());

                    var con = Configuration.getInstance().getConnection();
                    SqlCommand cmd = new SqlCommand("select Id from Rubric where CloId=@CloId and Details=@Details", con);
                    cmd.Parameters.AddWithValue("CloId", Rubric_CLOID_comboBox1.Text);
                    cmd.Parameters.AddWithValue("Details", RubricDetails_richTextBox1.Text);
                    RubricID = Convert.ToInt32(cmd.ExecuteScalar());
                }
                catch (Exception exp)
                {
                    MessageBox.Show(exp.Message);
                }
            }
        }

        private void RubricLevel_dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex >= 0)
            {
                try
                {
                    materialButton6.Enabled = true;
                    materialButton5.Enabled = true;
                    RubricLevel_dataGridView1.CurrentRow.Selected = true;
                    RubricLevel_Details_richTextBox1.Text = RubricLevel_dataGridView1.Rows[e.RowIndex].Cells["Details"].Value.ToString();
                    RubricLevel_comboBox2.Text = RubricLevel_dataGridView1.Rows[e.RowIndex].Cells["MeasurementLevel"].Value.ToString();
                    RubricLevel_RubricId_comboBox1.Text = RubricLevel_dataGridView1.Rows[e.RowIndex].Cells["RubricId"].Value.ToString();
                    //RubricLevelID = int.Parse(RubricLevel_dataGridView1.Rows[e.RowIndex].Cells["Id"].Value.ToString());

                    var con = Configuration.getInstance().getConnection();
                    SqlCommand cmd = new SqlCommand("select Id from RubricLevel where RubricId=@RubricId and Details=@Details", con);
                    cmd.Parameters.AddWithValue("@RubricId", RubricLevel_RubricId_comboBox1.Text);
                    cmd.Parameters.AddWithValue("@Details", RubricLevel_Details_richTextBox1.Text);
                    RubricLevelID = Convert.ToInt32(cmd.ExecuteScalar());
                }
                catch (Exception exp)
                {
                    MessageBox.Show(exp.Message);
                }
            }
        }

        private void Clo_sort_materialComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            var con = Configuration.getInstance().getConnection();
            if (Clo_sort_materialComboBox2.Text == "Name")
            {
                SqlCommand cmd2 = new SqlCommand("Select Name, DateCreated, DateUpdated from Clo Order By Name", con);
                SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
                DataTable dt2 = new DataTable();
                da2.Fill(dt2);
                Clo_dataGridView1.DataSource = dt2;
            }
            else if (Clo_sort_materialComboBox2.Text == "Date Created")
            {
                SqlCommand cmd2 = new SqlCommand("Select Name, DateCreated, DateUpdated from Clo Order By DateCreated", con);
                SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
                DataTable dt2 = new DataTable();
                da2.Fill(dt2);
                Clo_dataGridView1.DataSource = dt2;
            }
            else if (Clo_sort_materialComboBox2.Text == "Date Updated")
            {
                SqlCommand cmd2 = new SqlCommand("Select Name, DateCreated, DateUpdated from Clo Order By DateUpdated", con);
                SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
                DataTable dt2 = new DataTable();
                da2.Fill(dt2);
                Clo_dataGridView1.DataSource = dt2;
            }
            else
            {
                CloView();
            }


        }

        private void Rubric_Sort_materialComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            var con = Configuration.getInstance().getConnection();
            if (Rubric_Sort_materialComboBox2.Text == "Details")
            {
                SqlCommand cmd4 = new SqlCommand("Select Details, CloId from Rubric Order By Details", con);
                SqlDataAdapter da4 = new SqlDataAdapter(cmd4);
                DataTable dt4 = new DataTable();
                da4.Fill(dt4);
                Rubric_dataGridView1.DataSource = dt4;
            }
            else if (Rubric_Sort_materialComboBox2.Text == "CLO ID")
            {
                SqlCommand cmd4 = new SqlCommand("Select Details, CloId from Rubric Order By CloId", con);
                SqlDataAdapter da4 = new SqlDataAdapter(cmd4);
                DataTable dt4 = new DataTable();
                da4.Fill(dt4);
                Rubric_dataGridView1.DataSource = dt4;
            }
            else
            {
                RubricView();
            }

        }

        private void materialComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            var con = Configuration.getInstance().getConnection();
            if (materialComboBox2.Text == "Details")
            {
                SqlCommand cmd5 = new SqlCommand("Select RubricId,Details,MeasurementLevel from RubricLevel Order By Details", con);
                SqlDataAdapter da5 = new SqlDataAdapter(cmd5);
                DataTable dt5 = new DataTable();
                da5.Fill(dt5);
                RubricLevel_dataGridView1.DataSource = dt5;
            }
            else if (materialComboBox2.Text == "Rubric Id")
            {

                SqlCommand cmd5 = new SqlCommand("Select ubricId,Details,MeasurementLevel from RubricLevel Order By RubricId ", con);
                SqlDataAdapter da5 = new SqlDataAdapter(cmd5);
                DataTable dt5 = new DataTable();
                da5.Fill(dt5);
                RubricLevel_dataGridView1.DataSource = dt5;
            }
            else if (materialComboBox2.Text == "Measurement Level")
            {
                SqlCommand cmd5 = new SqlCommand("Select RubricId,Details,MeasurementLevel from RubricLevel Order By MeasurementLevel", con);
                SqlDataAdapter da5 = new SqlDataAdapter(cmd5);
                DataTable dt5 = new DataTable();
                da5.Fill(dt5);
                RubricLevel_dataGridView1.DataSource = dt5;
            }
            else
            {
                RubricLevelView();
            }
        }

        private void S_Search_textBox1_TextChanged(object sender, EventArgs e)
        {

            
            var con = Configuration.getInstance().getConnection();
            string TextForSearching = S_Search_textBox1.Text;
            if (S_Search_materialComboBox2.Text != "")
            {
                if (S_Search_materialComboBox2.Text == "First Name")
                {

                    SqlCommand cmd = new SqlCommand("Select FirstName, LastName, Contact, Email, RegistrationNumber from Student Where Status= 5 AND FirstName" + " LIKE '%" + TextForSearching + "%'", con);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    Student_dataGridView.DataSource = dt;
                }

                if (S_Search_materialComboBox2.Text == "Last Name")
                {
                    SqlCommand cmd = new SqlCommand("Select FirstName, LastName, Contact, Email, RegistrationNumber from Student Where Status= 5 AND LastName" + " LIKE '%" + TextForSearching + "%'", con);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    Student_dataGridView.DataSource = dt;
                }
                if (S_Search_materialComboBox2.Text == "Registration Number")
                {
                    SqlCommand cmd = new SqlCommand("Select FirstName, LastName, Contact, Email, RegistrationNumber from Student Where Status= 5 AND RegistrationNumber" + " LIKE '%" + TextForSearching + "%'", con);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    Student_dataGridView.DataSource = dt;
                }
                if (S_Search_materialComboBox2.Text == "Contact")
                {
                    SqlCommand cmd = new SqlCommand("Select FirstName, LastName, Contact, Email, RegistrationNumber from Student Where Status= 5 AND Contact" + " LIKE '%" + TextForSearching + "%'", con);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    Student_dataGridView.DataSource = dt;
                }
                if (S_Search_materialComboBox2.Text == "Email")
                {
                    SqlCommand cmd = new SqlCommand("Select FirstName, LastName, Contact, Email, RegistrationNumber from Student Where Email" + " LIKE '%" + TextForSearching + "%'", con);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    Student_dataGridView.DataSource = dt;
                }
                if (S_Search_materialComboBox2.Text == "Filter By")
                {
                    ActiveStudentsView();
                }

            }
        }

        private void Clo_Search_textBox1_TextChanged(object sender, EventArgs e)
        {
            var con = Configuration.getInstance().getConnection();
            string TextForSearching = Clo_Search_textBox1.Text;
            if (Clo_Search_materialComboBox1.Text != "")
            {

                if (Clo_Search_materialComboBox1.Text == "Name")
                {
                    SqlCommand cmd = new SqlCommand("Select Name, DateCreated, DateUpdated from Clo Where Name" + " LIKE '%" + TextForSearching + "%'", con);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    Clo_dataGridView1.DataSource = dt;
                }
                if (Clo_Search_materialComboBox1.Text == "Date Created")
                {
                    SqlCommand cmd = new SqlCommand("Select Name, DateCreated, DateUpdated from Clo Where DateCreated" + " LIKE '%" + TextForSearching + "%'", con);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    Clo_dataGridView1.DataSource = dt;
                }
                if (Clo_Search_materialComboBox1.Text == "Date Updated")
                {
                    SqlCommand cmd = new SqlCommand("Select Name, DateCreated, DateUpdated from Clo Where DateUpdated" + " LIKE '%" + TextForSearching + "%'", con);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    Clo_dataGridView1.DataSource = dt;
                }
                if (Clo_Search_materialComboBox1.Text == "Filter By")
                {
                    CloView();
                }

            }
        }

        private void Rubric_Search_textBox1_TextChanged(object sender, EventArgs e)
        {
            var con = Configuration.getInstance().getConnection();
            string TextForSearching = Rubric_Search_textBox1.Text;
            if (Rubric_Search_materialComboBox1.Text != "")
            {
                if (Rubric_Search_materialComboBox1.Text == "CloId")
                {
                    SqlCommand cmd = new SqlCommand("Select Details, CloId from Rubric Where CloId" + " LIKE '%" + TextForSearching + "%'", con);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    Rubric_dataGridView1.DataSource = dt;
                }
                if (Rubric_Search_materialComboBox1.Text == "Details")
                {
                    SqlCommand cmd = new SqlCommand("Select Details, CloId from Rubric Where Details" + " LIKE '%" + TextForSearching + "%'", con);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    Rubric_dataGridView1.DataSource = dt;
                }
                if (Rubric_Search_materialComboBox1.Text=="Filter By")
                {
                    RubricView();
                }
            }
        }
        //search rubric level
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            var con = Configuration.getInstance().getConnection();
            string TextForSearching = textBox1.Text;
            if (materialComboBox1.Text != "")
            {
                if (materialComboBox1.Text == "RubricId")
                {
                    SqlCommand cmd = new SqlCommand("Select RubricId,Details,MeasurementLevel from RubricLevel Where RubricId" + " LIKE '%" + TextForSearching + "%'", con);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    RubricLevel_dataGridView1.DataSource = dt;
                }
                if (materialComboBox1.Text == "Details")
                {
                    SqlCommand cmd = new SqlCommand("Select RubricId,Details,MeasurementLevel from RubricLevel Where Details" + " LIKE '%" + TextForSearching + "%'", con);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    RubricLevel_dataGridView1.DataSource = dt;
                }
                if (materialComboBox1.Text == "Measurement Level")
                {
                    SqlCommand cmd = new SqlCommand("Select RubricId,Details,MeasurementLevel from RubricLevel Where MeasurementLevel" + " LIKE '%" + TextForSearching + "%'", con);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    RubricLevel_dataGridView1.DataSource = dt;
                }
                if (materialComboBox1.Text=="Filter By")
                {
                    RubricLevelView();
                }
            }
        }
       

        private void AssessmentAdd_materialButton7_Click(object sender, EventArgs e)
        {
            var con = Configuration.getInstance().getConnection();
            if (String.IsNullOrEmpty(AssessmentTitle_textBox3.Text) || String.IsNullOrEmpty(AssessmentWeightage_textBox5.Text)
                || String.IsNullOrEmpty(AssessmentMarks_textBox4.Text))

            {
                MessageBox.Show("Please enter all input values", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    SqlCommand command = new SqlCommand("Select count(*) from Assessment where Title ='" + AssessmentTitle_textBox3.Text + "'", con);
                    int count = Convert.ToInt32(command.ExecuteScalar());
                    if (count == 0)
                    {
                        SqlCommand cmd = new SqlCommand("INSERT INTO Assessment VALUES (@Title,@DateCreated,@TotalMarks,@TotalWeightage);", con);
                        if (ValidateCLOName(AssessmentTitle_textBox3.Text) == true)
                        {
                            cmd.Parameters.AddWithValue("@Title", AssessmentTitle_textBox3.Text);

                            DateTime date = DateTime.Now;
                            cmd.Parameters.AddWithValue("@DateCreated", date);
                            if (ValidateWeightage(AssessmentMarks_textBox4.Text) == true)
                            {
                                cmd.Parameters.AddWithValue("@TotalMarks", AssessmentMarks_textBox4.Text);
                                if (ValidateWeightage(AssessmentWeightage_textBox5.Text) == true)
                                {
                                    cmd.Parameters.AddWithValue("@TotalWeightage", AssessmentWeightage_textBox5.Text);
                                    cmd.ExecuteNonQuery();
                                AssessmentTitle_textBox3.Clear();
                                AssessmentWeightage_textBox5.Clear();
                                AssessmentMarks_textBox4.Clear();
                                    Count();
                                    MessageBox.Show("Data has been saved Successfully", "Insertion Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else
                                {
                                    MessageBox.Show("Please Enter Correct Weightage that is not greater than 100 and less than 0", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                            else
                            {
                                MessageBox.Show("Please Enter Correct Marks that are greater than zero", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Please enter correct name that cannot contains specials characters and spaces", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        ActiveStudentsView();
                        InActiveStudentViews();
                        CloView();
                        RubricView();
                        RubricLevelView();
                        AssessmentView();
                        AssessmentComponentView();
                        AttendanceView();
                        Evaluation1();
                        Evaluation2();
                        AssessmentReportView();
                        AttendanceGridCheckBoxes();
                    }
                    else
                    {
                        MessageBox.Show("Duplication of assessment is not allowed", "Insertion Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
            }
                catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
        }

        }

        private void AssessmentUpdate_materialButton9_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(AssessmentTitle_textBox3.Text) || String.IsNullOrEmpty(AssessmentWeightage_textBox5.Text)
                || String.IsNullOrEmpty(AssessmentMarks_textBox4.Text))

            {
                MessageBox.Show("Please enter all input values", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    var con = Configuration.getInstance().getConnection();
                    SqlCommand command = new SqlCommand("Select count(*) from Assessment where Title ='" + AssessmentTitle_textBox3.Text + "' and Id!=" + AssessmentID, con);
                    int count = Convert.ToInt32(command.ExecuteScalar());
                    if (count == 0)
                    {
                        SqlCommand cmd = new SqlCommand("UPDATE Assessment SET  Title=@Title, TotalMarks = @TotalMarks ,TotalWeightage=@TotalWeightage WHERE Id=@Id", con);
                        if (ValidateCLOName(AssessmentTitle_textBox3.Text) == true)
                        {
                            cmd.Parameters.AddWithValue("@Title", AssessmentTitle_textBox3.Text);
                            if (ValidateWeightage(AssessmentMarks_textBox4.Text) == true)
                            {
                                cmd.Parameters.AddWithValue("@TotalMarks", int.Parse(AssessmentMarks_textBox4.Text));
                                if (ValidateWeightage(AssessmentWeightage_textBox5.Text) == true)
                                {
                                    cmd.Parameters.AddWithValue("@TotalWeightage", int.Parse(AssessmentWeightage_textBox5.Text));

                                cmd.Parameters.AddWithValue("@Id", AssessmentID);
                                cmd.ExecuteNonQuery();
                                AssessmentTitle_textBox3.Clear();
                                AssessmentWeightage_textBox5.Clear();
                                AssessmentMarks_textBox4.Clear();
                                    Count();
                                    MessageBox.Show("Data has been updated Successfully", "Updation Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                                else
                                {
                                    MessageBox.Show("Please Enter Correct Weightage that is not greater than 100 and less than 0", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                            else
                            {
                                MessageBox.Show("Please Enter Correct Marks that are greater than zero", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Please enter correct name that cannot contains specials characters and spaces", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }


                        AssessmentUpdate_materialButton9.Enabled = false;
                        AssessmentDelete_materialButton8.Enabled = false;
                        ActiveStudentsView();
                        InActiveStudentViews();
                        CloView();
                        RubricView();
                        RubricLevelView();
                        AssessmentView();
                        AssessmentComponentView();
                        AttendanceView();
                        Evaluation1();
                        Evaluation2();
                        AssessmentReportView();
                        AttendanceGridCheckBoxes();
                    }
                    else
                    {
                        MessageBox.Show("Duplication of assessment is not allowed", "Updation Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
            }
                catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
        }
        }

        private void AssessmentDelete_materialButton8_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(AssessmentTitle_textBox3.Text) || String.IsNullOrEmpty(AssessmentWeightage_textBox5.Text)
                || String.IsNullOrEmpty(AssessmentMarks_textBox4.Text))

            {
                MessageBox.Show("You have not entered credentials for deletion", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    var con = Configuration.getInstance().getConnection();
                    SqlCommand cmd1 = new SqlCommand("Delete from StudentResult where AssessmentComponentId In (select Id from AssessmentComponent where AssessmentId In (Select Id from Assessment where Id=@Id))", con);
                    cmd1.Parameters.AddWithValue("@Id", AssessmentID);
                    cmd1.ExecuteNonQuery();

                    SqlCommand cmd2 = new SqlCommand("Delete from AssessmentComponent WHERE AssessmentId In (Select Id from Assessment where Id=@Id)", con);
                    cmd2.Parameters.AddWithValue("@Id", AssessmentID);
                    cmd2.ExecuteNonQuery();

                    SqlCommand cmd = new SqlCommand("Delete FROM Assessment WHERE Id=@Id", con);
                    cmd.Parameters.AddWithValue("@Id", AssessmentID);
                    cmd.ExecuteNonQuery();
                    Count();
                    MessageBox.Show("Data has been deleted Successfully", "Deletion Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    AssessmentUpdate_materialButton9.Enabled = false;
                    AssessmentDelete_materialButton8.Enabled = false;
                    AssessmentTitle_textBox3.Clear();
                    AssessmentWeightage_textBox5.Clear();
                    AssessmentMarks_textBox4.Clear();
                    ActiveStudentsView();
                    InActiveStudentViews();
                    CloView();
                    RubricView();
                    RubricLevelView();
                    AssessmentView();
                    AssessmentComponentView();
                    AttendanceView();
                    Evaluation1();
                    Evaluation2();
                    AssessmentReportView();
                    AttendanceGridCheckBoxes();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void Assessment_dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex >= 0)
            {
                try
                {

                    AssessmentUpdate_materialButton9.Enabled = true;
                    AssessmentDelete_materialButton8.Enabled = true;
                    Assessment_dataGridView1.CurrentRow.Selected = true;
                    AssessmentTitle_textBox3.Text = Assessment_dataGridView1.Rows[e.RowIndex].Cells["Title"].Value.ToString();
                    AssessmentMarks_textBox4.Text = Assessment_dataGridView1.Rows[e.RowIndex].Cells["TotalMarks"].Value.ToString();
                    AssessmentWeightage_textBox5.Text = Assessment_dataGridView1.Rows[e.RowIndex].Cells["TotalWeightage"].Value.ToString();
                    //AssessmentID = int.Parse(Assessment_dataGridView1.Rows[e.RowIndex].Cells["Id"].Value.ToString());

                    var con = Configuration.getInstance().getConnection();
                    SqlCommand cmd = new SqlCommand("select Id from Assessment where Title=@Title", con);
                    cmd.Parameters.AddWithValue("@Title", AssessmentTitle_textBox3.Text);
                    AssessmentID = Convert.ToInt32(cmd.ExecuteScalar());
                }
                catch (Exception exp)
                {
                    MessageBox.Show(exp.Message);
                }
            }
        }

        private void Evaluation_dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex >= 0)
            {
                materialButton11.Enabled = false;

                Evaluation_textBox2.Text = Evaluation_dataGridView2.Rows[e.RowIndex].Cells["Id"].Value.ToString();
            }
        }

        private void AssessmentSort_materialComboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (AssessmentSort_materialComboBox4.Text== "Title")
            {
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd5 = new SqlCommand("Select Title,DateCreated,TotalMarks,TotalWeightage from Assessment Order By Title", con);
                SqlDataAdapter da5 = new SqlDataAdapter(cmd5);
                DataTable dt5 = new DataTable();
                da5.Fill(dt5);
                Assessment_dataGridView1.DataSource = dt5;
            }
            else if (AssessmentSort_materialComboBox4.Text == "Date Created")
            {
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd5 = new SqlCommand("Select Title,DateCreated,TotalMarks,TotalWeightage from Assessment Order By DateCreated", con);
                SqlDataAdapter da5 = new SqlDataAdapter(cmd5);
                DataTable dt5 = new DataTable();
                da5.Fill(dt5);
                Assessment_dataGridView1.DataSource = dt5;
            }
            else if (AssessmentSort_materialComboBox4.Text == "Total Marks")
            {
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd5 = new SqlCommand("Select Title,DateCreated,TotalMarks,TotalWeightage from Assessment Order By TotalMarks", con);
                SqlDataAdapter da5 = new SqlDataAdapter(cmd5);
                DataTable dt5 = new DataTable();
                da5.Fill(dt5);
                Assessment_dataGridView1.DataSource = dt5;
            }
            else if (AssessmentSort_materialComboBox4.Text == "Total Weightage")
            {
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd5 = new SqlCommand("Select Title,DateCreated,TotalMarks,TotalWeightage from Assessment Order By TotalWeightage", con);
                SqlDataAdapter da5 = new SqlDataAdapter(cmd5);
                DataTable dt5 = new DataTable();
                da5.Fill(dt5);
                Assessment_dataGridView1.DataSource = dt5;
            }
            else
            {
                AssessmentView();
            }
        }

        private void AssessmentSearch_materialComboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            AssessmentSearch_textBox7.Visible = true;
            AssessmentSearch_textBox7.Clear();
        }

        private void AssessmentSearch_textBox7_TextChanged(object sender, EventArgs e)
        {
            string TextForSearching = AssessmentSearch_textBox7.Text;
            //MessageBox.Show(AssessmentSearch_materialComboBox3.Text);
            var con = Configuration.getInstance().getConnection();
            if (AssessmentSearch_materialComboBox3.Text != "")
            {
                //MessageBox.Show(TextForSearching);
                if (AssessmentSearch_materialComboBox3.Text == "Title")
                {
                    SqlCommand cmd = new SqlCommand("Select Title,DateCreated,TotalMarks,TotalWeightage from Assessment Where Title" + " LIKE '%" + TextForSearching + "%'", con);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    Assessment_dataGridView1.DataSource = dt;
                }
                if (AssessmentSearch_materialComboBox3.Text == "Date Created")
                {
                    SqlCommand cmd = new SqlCommand("Select Title,DateCreated,TotalMarks,TotalWeightage from Assessment Where DateCreated" + " LIKE '%" + TextForSearching + "%'", con);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    Assessment_dataGridView1.DataSource = dt;
                }
                if (AssessmentSearch_materialComboBox3.Text == "Total Marks")
                {
                    SqlCommand cmd = new SqlCommand("Select Title,DateCreated,TotalMarks,TotalWeightage from Assessment Where TotalMarks" + " LIKE '%" + TextForSearching + "%'", con);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    Assessment_dataGridView1.DataSource = dt;
                }
                if (AssessmentSearch_materialComboBox3.Text == "Total Weightage")
                {
                    SqlCommand cmd = new SqlCommand("Select Title,DateCreated,TotalMarks,TotalWeightage from Assessment Where TotalWeightage" + " LIKE '%" + TextForSearching + "%'", con);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    Assessment_dataGridView1.DataSource = dt;
                }
                if(AssessmentSearch_materialComboBox3.Text == "Filter By")
                {
                    AssessmentView();
                }
            }
        }

        private void ASS_ComponentAdd_materialButton7_Click(object sender, EventArgs e)
        {
            var con = Configuration.getInstance().getConnection();

            if (String.IsNullOrEmpty(ASS_ComponentName_textBox3.Text) || String.IsNullOrEmpty(ASS_ComponentRubricID_comboBox1.Text)
                || String.IsNullOrEmpty(ASS_ComponentAssID_comboBox2.Text) || String.IsNullOrEmpty(ASS_ComponentMarks_textBox6.Text))

            {
                MessageBox.Show("Please enter all input values", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {

                    int dup = 0;
                    SqlCommand duplicate = new SqlCommand("Select count(*) \r\n  from AssessmentComponent\r\n  Where Name='" + ASS_ComponentName_textBox3.Text + "'Group By AssessmentId\r\n  having AssessmentId = " + ASS_ComponentAssID_comboBox2.Text, con);
                    dup = Convert.ToInt32(duplicate.ExecuteScalar());

                    int InAS_Sum = 0;
                    SqlCommand temp = new SqlCommand("SELECT SUM(AC.TotalMarks)\r\nFROM AssessmentComponent AC\r\nWHERE AC.AssessmentId = " + ASS_ComponentAssID_comboBox2.Text, con);
                    try
                    {
                        InAS_Sum = Convert.ToInt32(temp.ExecuteScalar());
                    }
                    catch { }
                    SqlCommand temp1 = new SqlCommand("SELECT A.TotalMarks\r\nFROM Assessment A\r\nWHERE A.Id = " + ASS_ComponentAssID_comboBox2.Text, con);
                    int total = Convert.ToInt32(temp1.ExecuteScalar());


                    if (dup == 0)
                    {
                    if (ValidateWeightage(ASS_ComponentMarks_textBox6.Text) == true)
                    {
                        if (total - InAS_Sum >= int.Parse(ASS_ComponentMarks_textBox6.Text))
                        {
                            SqlCommand cmd = new SqlCommand("INSERT INTO AssessmentComponent VALUES (@Name,@RubricId,@TotalMarks,@DateCreated,@DateUpdated,@AssessmentId);", con);
                            if (ValidateCLOName(ASS_ComponentName_textBox3.Text) == true)
                            {
                                cmd.Parameters.AddWithValue("@Name", ASS_ComponentName_textBox3.Text);
                                cmd.Parameters.AddWithValue("@RubricId", ASS_ComponentRubricID_comboBox1.Text);
                                cmd.Parameters.AddWithValue("@TotalMarks", ASS_ComponentMarks_textBox6.Text);
                                DateTime date = DateTime.Now;
                                cmd.Parameters.AddWithValue("@DateCreated", date);
                                cmd.Parameters.AddWithValue("@DateUpdated", date);
                                cmd.Parameters.AddWithValue("@AssessmentId", ASS_ComponentAssID_comboBox2.Text);
                                cmd.ExecuteNonQuery();
                                    Count();
                                    MessageBox.Show("Data has been saved Successfully", "Insertion Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                ASS_ComponentName_textBox3.Clear();
                                ASS_ComponentMarks_textBox6.Clear();
                                ASS_ComponentRubricID_comboBox1.Items.Clear();
                                ASS_ComponentRubricID_comboBox1.ResetText();
                                ASS_ComponentAssID_comboBox2.Items.Clear();
                                ASS_ComponentAssID_comboBox2.ResetText();
                            }
                            else
                            {
                                MessageBox.Show("Please enter correct name that will not have special characters and spaces", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }

                        }
                        else
                        {
                            MessageBox.Show("Sum of Marks of Assessment Components is Greater than Assessment Marks saved", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Please enter correct marks that are greater than zero", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                        ActiveStudentsView();
                        InActiveStudentViews();
                        CloView();
                        RubricView();
                        RubricLevelView();
                        AssessmentView();
                        AssessmentComponentView();
                        AttendanceView();
                        Evaluation1();
                        Evaluation2();
                        AssessmentReportView();
                        AttendanceGridCheckBoxes();
                    }

                    else
                    {
                        MessageBox.Show("Duplication of assessment component is not allowed in same assessment", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    //MessageBox.Show(ex.Message);
                }
            }

        }

        private void ASS_ComponentUpdate_materialButton9_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(ASS_ComponentName_textBox3.Text) || String.IsNullOrEmpty(ASS_ComponentRubricID_comboBox1.Text)
                || String.IsNullOrEmpty(ASS_ComponentAssID_comboBox2.Text) || String.IsNullOrEmpty(ASS_ComponentMarks_textBox6.Text))

            {
                MessageBox.Show("Please enter all input values", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                //try
                //{
                    var con = Configuration.getInstance().getConnection();

                    int dup = 0;
               // +"Group By AssessmentId\r\n  having AssessmentId = " + ASS_ComponentAssID_comboBox2.Text
                    SqlCommand duplicate = new SqlCommand("Select count(*) \r\n  from AssessmentComponent\r\n  Where Name='" + ASS_ComponentName_textBox3.Text + "' and Id !="+AssessmentComponentID, con);
                    dup = Convert.ToInt32(duplicate.ExecuteScalar());

                    int InAS_Sum = 0;
                SqlCommand temp = new SqlCommand("SELECT SUM(AC.TotalMarks)\r\nFROM AssessmentComponent AC\r\nWHERE Ac.AssessmentId<>@AssessID and AC.AssessmentId = " + ASS_ComponentAssID_comboBox2.Text, con);
                temp.Parameters.AddWithValue("@AssessID", AssessmentComponentID);
                try
                {
                        InAS_Sum = Convert.ToInt32(temp.ExecuteScalar());
                    }
                    catch { }
                    SqlCommand temp1 = new SqlCommand("SELECT A.TotalMarks\r\nFROM Assessment A\r\nWHERE A.Id = " + ASS_ComponentAssID_comboBox2.Text, con);
                    int total = Convert.ToInt32(temp1.ExecuteScalar());

                if (dup == 0)
                    {
                        if (ValidateWeightage(ASS_ComponentMarks_textBox6.Text) == true)
                        {
                            if (total-InAS_Sum >= int.Parse(ASS_ComponentMarks_textBox6.Text))
                            {
                            int s = total - InAS_Sum;
                            MessageBox.Show(s.ToString());
                            SqlCommand cmd = new SqlCommand("UPDATE AssessmentComponent SET  Name=@Name, RubricId = @RubricId ,TotalMarks=@TotalMarks,DateUpdated=@DateUpdated,AssessmentId=@AssessmentId WHERE Id=@Id", con);
                            if (ValidateCLOName(ASS_ComponentName_textBox3.Text) == true)
                            {
                                cmd.Parameters.AddWithValue("@Name", ASS_ComponentName_textBox3.Text);
                                cmd.Parameters.AddWithValue("@RubricId", ASS_ComponentRubricID_comboBox1.Text);
                                cmd.Parameters.AddWithValue("@TotalMarks", ASS_ComponentMarks_textBox6.Text);
                                DateTime date = DateTime.Now;
                                cmd.Parameters.AddWithValue("@DateCreated", date);
                                cmd.Parameters.AddWithValue("@DateUpdated", date);
                                cmd.Parameters.AddWithValue("@AssessmentId", ASS_ComponentAssID_comboBox2.Text);
                                cmd.Parameters.AddWithValue("@Id", AssessmentComponentID);
                                cmd.ExecuteNonQuery();
                                ASS_ComponentName_textBox3.Clear();
                                ASS_ComponentMarks_textBox6.Clear();
                                ASS_ComponentRubricID_comboBox1.Items.Clear();
                                ASS_ComponentRubricID_comboBox1.ResetText();
                                ASS_ComponentAssID_comboBox2.Items.Clear();
                                ASS_ComponentAssID_comboBox2.ResetText();
                                    Count();
                                    MessageBox.Show("Data has been updated Successfully", "Updation Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show("Please enter correct name that will not have special characters and spaces", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Sum of Marks of Assessment Components is Greater than Assessment Marks saved", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                    }
                    else
                    {
                        MessageBox.Show("Please Enter Correct Marks that are greater than zero", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    ASS_ComponentUpdate_materialButton9.Enabled = false;
                    materialButton8.Enabled = false;
                        ActiveStudentsView();
                        InActiveStudentViews();
                        CloView();
                        RubricView();
                        RubricLevelView();
                        AssessmentView();
                        AssessmentComponentView();
                        AttendanceView();
                        Evaluation1();
                        Evaluation2();
                        AssessmentReportView();
                        AttendanceGridCheckBoxes();
                    }

                    else
                    {
                        MessageBox.Show("Duplication of assessment component in same assignment is not allowed", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                //}

                //catch (Exception ex)
                //{
                //    //MessageBox.Show(ex.Message);
                //}
            }
        }

        //Assessment Component Delete
        private void materialButton8_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(ASS_ComponentName_textBox3.Text) || String.IsNullOrEmpty(ASS_ComponentRubricID_comboBox1.Text)
                || String.IsNullOrEmpty(ASS_ComponentAssID_comboBox2.Text) || String.IsNullOrEmpty(ASS_ComponentMarks_textBox6.Text))

            {
                // Input textbox is empty
                MessageBox.Show("Please enter all input values", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    var con = Configuration.getInstance().getConnection();
                    SqlCommand temp3 = new SqlCommand("Delete FROM StudentResult WHERE AssessmentComponentId =@Id", con);
                    temp3.Parameters.AddWithValue("@Id", AssessmentComponentID);
                    temp3.ExecuteNonQuery();

                    SqlCommand cmd = new SqlCommand("Delete FROM AssessmentComponent WHERE Id=@Id", con);
                    cmd.Parameters.AddWithValue("@Id", AssessmentComponentID);
                    cmd.ExecuteNonQuery();
                    Count();
                    MessageBox.Show("Data has been deleted Successfully", "Deletion Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    materialButton8.Enabled = false;
                    ASS_ComponentUpdate_materialButton9.Enabled = false;
                    ASS_ComponentName_textBox3.Clear();
                    ASS_ComponentMarks_textBox6.Clear();
                    ASS_ComponentRubricID_comboBox1.Items.Clear();
                    ASS_ComponentRubricID_comboBox1.ResetText();
                    ASS_ComponentAssID_comboBox2.Items.Clear();
                    ASS_ComponentAssID_comboBox2.ResetText();
                    ActiveStudentsView();
                    InActiveStudentViews();
                    CloView();
                    RubricView();
                    RubricLevelView();
                    AssessmentView();
                    AssessmentComponentView();
                    AttendanceView();
                    Evaluation1();
                    Evaluation2();
                    AssessmentReportView();
                    AttendanceGridCheckBoxes();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        private void ASS_Component_dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex >= 0)
            {
                try
                {
                    ASS_ComponentUpdate_materialButton9.Enabled = true;
                    materialButton8.Enabled = true;
                    ASS_Component_dataGridView1.CurrentRow.Selected = true;
                    ASS_ComponentName_textBox3.Text = ASS_Component_dataGridView1.Rows[e.RowIndex].Cells["Name"].Value.ToString();
                    ASS_ComponentRubricID_comboBox1.Text = ASS_Component_dataGridView1.Rows[e.RowIndex].Cells["RubricId"].Value.ToString();
                    ASS_ComponentMarks_textBox6.Text = ASS_Component_dataGridView1.Rows[e.RowIndex].Cells["TotalMarks"].Value.ToString();
                    ASS_ComponentAssID_comboBox2.Text = ASS_Component_dataGridView1.Rows[e.RowIndex].Cells["AssessmentId"].Value.ToString();
                    //AssessmentComponentID = int.Parse(ASS_Component_dataGridView1.Rows[e.RowIndex].Cells["Id"].Value.ToString());

                    var con = Configuration.getInstance().getConnection();
                    SqlCommand cmd = new SqlCommand("select Id from AssessmentComponent where RubricId=@RubricId And Name=@Name And AssessmentId=@AssessmentId", con);
                    cmd.Parameters.AddWithValue("@RubricId", ASS_ComponentRubricID_comboBox1.Text);
                    cmd.Parameters.AddWithValue("@Name", ASS_ComponentName_textBox3.Text);
                    cmd.Parameters.AddWithValue("@AssessmentId", ASS_ComponentAssID_comboBox2.Text);

                    AssessmentComponentID = Convert.ToInt32(cmd.ExecuteScalar());
                }
                catch (Exception exp)
                {
                    MessageBox.Show(exp.Message);
                }
            }
        }

        private void ASS_ComponentSort_materialComboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            if (ASS_ComponentSort_materialComboBox4.Text == "Name")
            {
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd5 = new SqlCommand("Select Name,RubricId,TotalMarks,DateCreated,DateUpdated,AssessmentId from AssessmentComponent Order By Name", con);
                SqlDataAdapter da5 = new SqlDataAdapter(cmd5);
                DataTable dt5 = new DataTable();
                da5.Fill(dt5);
                ASS_Component_dataGridView1.DataSource = dt5;
            }
            else if (ASS_ComponentSort_materialComboBox4.Text == "Rubric ID")
            {
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd5 = new SqlCommand("Select Name,RubricId,TotalMarks,DateCreated,DateUpdated,AssessmentId from AssessmentComponent Order By RubricId", con);
                SqlDataAdapter da5 = new SqlDataAdapter(cmd5);
                DataTable dt5 = new DataTable();
                da5.Fill(dt5);
                ASS_Component_dataGridView1.DataSource = dt5;
            }
            else if (ASS_ComponentSort_materialComboBox4.Text == "Assessment ID")
            {
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd5 = new SqlCommand("Select Name,RubricId,TotalMarks,DateCreated,DateUpdated,AssessmentId from AssessmentComponent Order By AssessmentId", con);
                SqlDataAdapter da5 = new SqlDataAdapter(cmd5);
                DataTable dt5 = new DataTable();
                da5.Fill(dt5);
                ASS_Component_dataGridView1.DataSource = dt5;
            }
            else if (ASS_ComponentSort_materialComboBox4.Text == "Total Marks")
            {
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd5 = new SqlCommand("Select Name,RubricId,TotalMarks,DateCreated,DateUpdated,AssessmentId from AssessmentComponent Order By TotalMarks", con);
                SqlDataAdapter da5 = new SqlDataAdapter(cmd5);
                DataTable dt5 = new DataTable();
                da5.Fill(dt5);
                ASS_Component_dataGridView1.DataSource = dt5;
            }
            else
            {
                AssessmentComponentView();
            }
        }

        private void ASS_ComponentSearch_textBox7_TextChanged(object sender, EventArgs e)
        {
            string TextForSearching = ASS_ComponentSearch_textBox7.Text;
            var con = Configuration.getInstance().getConnection();
            if (ASS_ComponentSearch_materialComboBox3.Text != "")
            {
                if (ASS_ComponentSearch_materialComboBox3.Text == "Name")
                {
                    SqlCommand cmd = new SqlCommand("Select Name,RubricId,TotalMarks,DateCreated,DateUpdated,AssessmentId from AssessmentComponent Where Name" + " LIKE '%" + TextForSearching + "%'", con);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    ASS_Component_dataGridView1.DataSource = dt;
                }
                if (ASS_ComponentSearch_materialComboBox3.Text == "Rubric ID")
                {
                    SqlCommand cmd = new SqlCommand("Select Name,RubricId,TotalMarks,DateCreated,DateUpdated,AssessmentId from AssessmentComponent Where RubricId" + " LIKE '%" + TextForSearching + "%'", con);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    ASS_Component_dataGridView1.DataSource = dt;
                }
                if (ASS_ComponentSearch_materialComboBox3.Text == "Assessment ID")
                {
                    SqlCommand cmd = new SqlCommand("Select Name,RubricId,TotalMarks,DateCreated,DateUpdated,AssessmentId from AssessmentComponent Where AssessmentId" + " LIKE '%" + TextForSearching + "%'", con);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    ASS_Component_dataGridView1.DataSource = dt;
                }
                if (ASS_ComponentSearch_materialComboBox3.Text == "Total Marks")
                {
                    SqlCommand cmd = new SqlCommand("Select Name,RubricId,TotalMarks,DateCreated,DateUpdated,AssessmentId from AssessmentComponent Where TotalMarks" + " LIKE '%" + TextForSearching + "%'", con);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    ASS_Component_dataGridView1.DataSource = dt;
                }
                if (ASS_ComponentSearch_materialComboBox3.Text == "Filter By")
                {
                    AssessmentComponentView();
                }

            }
        }

        private void RubricLevel_RubricId_comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void S_Search_textBox1_Click(object sender, EventArgs e)
        {
        }

        private void S_Search_textBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (S_Search_textBox1.Text == "Search Here")
            {
                S_Search_textBox1.Text = "";
                S_Search_textBox1.ForeColor = SystemColors.WindowText;
            }
        }

        private void Clo_Search_textBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (Clo_Search_textBox1.Text == "Search Here")
            {
                Clo_Search_textBox1.Text = "";
                Clo_Search_textBox1.ForeColor = SystemColors.WindowText;
            }
        }

        private void Rubric_Search_textBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (Rubric_Search_textBox1.Text == "Search Here")
            {
                Rubric_Search_textBox1.Text = "";
                Rubric_Search_textBox1.ForeColor = SystemColors.WindowText;
            }
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "Search Here")
            {
                textBox1.Text = "";
                textBox1.ForeColor = SystemColors.WindowText;
            }
        }

        private void AssessmentSearch_textBox7_MouseClick(object sender, MouseEventArgs e)
        {
            if (AssessmentSearch_textBox7.Text == "Search Here")
            {
                AssessmentSearch_textBox7.Text = "";
                AssessmentSearch_textBox7.ForeColor = SystemColors.WindowText;
            }
        }

        private void ASS_ComponentSearch_textBox7_MouseClick(object sender, MouseEventArgs e)
        {
            if (ASS_ComponentSearch_textBox7.Text == "Search Here")
            {
                ASS_ComponentSearch_textBox7.Text = "";
                ASS_ComponentSearch_textBox7.ForeColor = SystemColors.WindowText;
            }
        }

        private void Attendance_dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {


        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void AttendanceAdd_materialButton7_Click(object sender, EventArgs e)
        {
            var con = Configuration.getInstance().getConnection();
            var date = DateTime.Now.ToString("yyyy-MM-dd");
            DateTime dates = Convert.ToDateTime(Attendance_dateTimePicker2.Text);
            SqlCommand cmd1 = new SqlCommand("Select Id from ClassAttendance where AttendanceDate=@Date", con);
            cmd1.Parameters.AddWithValue("@Date", dates);
            int id = Convert.ToInt32(cmd1.ExecuteScalar());
            DateTime d1 = DateTime.Now;
            if (dates<d1)
            {
                if (id == 0)
                {
                    SqlCommand cmd = new SqlCommand("INSERT Into ClassAttendance Values(@AttendanceDate)", con);
                    cmd.Parameters.AddWithValue("@AttendanceDate", dates);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Date has been saved Successfully", "Insertion Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    Attendance_dateTimePicker2.ResetText();
                }
                else
                {
                    MessageBox.Show("Date has Already Added", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("You cannot select date from future", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }




        }

        private void Attendance_dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void A_dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Attendance_dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {


        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            var date = Convert.ToDateTime(Attendance_dateTimePicker2.Text);
            int Id = 0;
            var con = Configuration.getInstance().getConnection();
            try
            {
                SqlCommand cmd = new SqlCommand("Select Id from ClassAttendance Where AttendanceDate=@Date", con);
                cmd.Parameters.AddWithValue("@Date", date);
                Id = Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch
            {
                Id = 0;
            }

            if (Id == 0)
            {
                MessageBox.Show("Date is not added yet", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {

                    string student = dataGridView1.Rows[e.RowIndex].Cells["Id"].Value.ToString();
                    SqlCommand cmd2 = new SqlCommand("Select Id from ClassAttendance where AttendanceDate=@date", con);
                    cmd2.Parameters.AddWithValue("@date", date);
                    int adateid = Convert.ToInt32(cmd2.ExecuteScalar());
                    SqlCommand cmd3 = new SqlCommand("Select StudentId from StudentAttendance where AttendanceId=@CA and StudentId=@Id", con);
                    cmd3.Parameters.AddWithValue("@CA", adateid);
                    cmd3.Parameters.AddWithValue("@Id", student);
                    int scheck = Convert.ToInt32(cmd3.ExecuteScalar());

                    //lookup
                    SqlCommand p = new SqlCommand("Select LookupId From Lookup Where Name='Present'", con);
                    int P_Status = (Int32)p.ExecuteScalar();
                    p.ExecuteNonQuery();

                    SqlCommand A = new SqlCommand("Select LookupId From Lookup Where Name='Absent'", con);
                    int A_Status = (Int32)A.ExecuteScalar();
                    A.ExecuteNonQuery();

                    SqlCommand Le = new SqlCommand("Select LookupId From Lookup Where Name='Leave'", con);
                    int Le_Status = (Int32)Le.ExecuteScalar();
                    Le.ExecuteNonQuery();

                    SqlCommand La = new SqlCommand("Select LookupId From Lookup Where Name='Late'", con);
                    int La_Status = (Int32)La.ExecuteScalar();
                    La.ExecuteNonQuery();


                    if (e.ColumnIndex == 0)
                    {
                        dataGridView1.Rows[e.RowIndex].Cells[1].Value = false;
                        dataGridView1.Rows[e.RowIndex].Cells[2].Value = false;
                        dataGridView1.Rows[e.RowIndex].Cells[3].Value = false;
                        if (scheck == 0)
                        {
                            SqlCommand cmd1 = new SqlCommand("INSERT into StudentAttendance values(@AttendanceId,@StudentId,@AttendanceStatus)", con);
                            cmd1.Parameters.AddWithValue("@AttendanceId", adateid);
                            cmd1.Parameters.AddWithValue("@StudentId", student);
                            cmd1.Parameters.AddWithValue("@AttendanceStatus", P_Status);
                            cmd1.ExecuteNonQuery();
                        }
                        else
                        {
                            SqlCommand cmd1 = new SqlCommand("UPDATE StudentAttendance SET AttendanceStatus=@AttendanceStatus where AttendanceId=@AttendanceId and StudentId=@StudentId", con);
                            cmd1.Parameters.AddWithValue("@AttendanceId", adateid);
                            cmd1.Parameters.AddWithValue("@StudentId", student);
                            cmd1.Parameters.AddWithValue("@AttendanceStatus", P_Status);
                            cmd1.ExecuteNonQuery();
                        }
                    }
                    if (e.ColumnIndex == 1)
                    {
                        dataGridView1.Rows[e.RowIndex].Cells[0].Value = false;
                        dataGridView1.Rows[e.RowIndex].Cells[2].Value = false;
                        dataGridView1.Rows[e.RowIndex].Cells[3].Value = false;
                        if (scheck == 0)
                        {
                            SqlCommand cmd1 = new SqlCommand("INSERT into StudentAttendance values(@AttendanceId,@StudentId,@AttendanceStatus)", con);
                            cmd1.Parameters.AddWithValue("@AttendanceId", adateid);
                            cmd1.Parameters.AddWithValue("@StudentId", student);
                            cmd1.Parameters.AddWithValue("@AttendanceStatus", A_Status);
                            cmd1.ExecuteNonQuery();
                        }
                        else
                        {
                            SqlCommand cmd1 = new SqlCommand("UPDATE StudentAttendance SET AttendanceStatus=@AttendanceStatus where AttendanceId=@AttendanceId and StudentId=@StudentId", con);
                            cmd1.Parameters.AddWithValue("@AttendanceId", adateid);
                            cmd1.Parameters.AddWithValue("@StudentId", student);
                            cmd1.Parameters.AddWithValue("@AttendanceStatus", A_Status);
                            cmd1.ExecuteNonQuery();
                        }
                    }
                    if (e.ColumnIndex == 2)
                    {
                        dataGridView1.Rows[e.RowIndex].Cells[1].Value = false;
                        dataGridView1.Rows[e.RowIndex].Cells[0].Value = false;
                        dataGridView1.Rows[e.RowIndex].Cells[3].Value = false;
                        if (scheck == 0)
                        {
                            SqlCommand cmd1 = new SqlCommand("INSERT into StudentAttendance values(@AttendanceId,@StudentId,@AttendanceStatus)", con);
                            cmd1.Parameters.AddWithValue("@AttendanceId", adateid);
                            cmd1.Parameters.AddWithValue("@StudentId", student);
                            cmd1.Parameters.AddWithValue("@AttendanceStatus", Le_Status);
                            cmd1.ExecuteNonQuery();
                        }
                        else
                        {
                            SqlCommand cmd1 = new SqlCommand("UPDATE StudentAttendance SET AttendanceStatus=@AttendanceStatus where AttendanceId=@AttendanceId and StudentId =@StudentId", con);
                            cmd1.Parameters.AddWithValue("@AttendanceId", adateid);
                            cmd1.Parameters.AddWithValue("@StudentId", student);
                            cmd1.Parameters.AddWithValue("@AttendanceStatus", Le_Status);
                            cmd1.ExecuteNonQuery();
                        }
                    }
                    if (e.ColumnIndex == 3)
                    {
                        dataGridView1.Rows[e.RowIndex].Cells[1].Value = false;
                        dataGridView1.Rows[e.RowIndex].Cells[2].Value = false;
                        dataGridView1.Rows[e.RowIndex].Cells[0].Value = false;
                        if (scheck == 0)
                        {
                            SqlCommand cmd1 = new SqlCommand("INSERT into StudentAttendance values(@AttendanceId,@StudentId,@AttendanceStatus)", con);
                            cmd1.Parameters.AddWithValue("@AttendanceId", adateid);
                            cmd1.Parameters.AddWithValue("@StudentId", student);
                            cmd1.Parameters.AddWithValue("@AttendanceStatus", La_Status);
                            cmd1.ExecuteNonQuery();
                        }
                        else
                        {
                            SqlCommand cmd1 = new SqlCommand("UPDATE StudentAttendance SET AttendanceStatus=@AttendanceStatus where AttendanceId=@AttendanceId and StudentId=@StudentId", con);
                            cmd1.Parameters.AddWithValue("@AttendanceId", adateid);
                            cmd1.Parameters.AddWithValue("@StudentId", student);
                            cmd1.Parameters.AddWithValue("@AttendanceStatus", La_Status);
                            cmd1.ExecuteNonQuery();
                        }
                    }
                }
                catch (Exception exp)
                {
                    MessageBox.Show(exp.Message);
                }
            }
        }

        private void AttendanceView_materialButton8_Click(object sender, EventArgs e)
        {
            var con = Configuration.getInstance().getConnection();
            
            SqlCommand cmd5 = new SqlCommand("Select SA.StudentId, S.FirstName + S.LastName As Name, S.RegistrationNumber,(select Name from LookUp where LookupId=SA.AttendanceStatus) AS Attendance  From Student S Join StudentAttendance SA On S.Id=SA.StudentId JOIN ClassAttendance on SA.AttendanceId=ClassAttendance.Id where ClassAttendance.AttendanceDate=@Date", con);
            DateTime temp = Convert.ToDateTime(Attendance_dateTimePicker2.Text);
            cmd5.Parameters.AddWithValue("@Date", temp);
            SqlDataAdapter da5 = new SqlDataAdapter(cmd5);
            DataTable dt5 = new DataTable();
            da5.Fill(dt5);
            AttendanceMarkStatus_dataGridView7.DataSource = dt5;
        }

        private void MarkEvaluation_materialbutton_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(Evaluation_textBox2.Text) || String.IsNullOrEmpty(evaluationAssessmentComponentID_comboBox2.Text)
                || String.IsNullOrEmpty(evaluationRubricMeasurementLevel_comboBox1.Text))

            {
                // Input textbox is empty
                MessageBox.Show("Please enter all input values", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {

                    var con = Configuration.getInstance().getConnection();
                    SqlCommand cmd1 = new SqlCommand("INSERT into StudentResult values(@StudentId,@AssessmentComponentId,@RubricMeasurementId,@EvaluationDate)", con);
                    cmd1.Parameters.AddWithValue("@StudentId", int.Parse(Evaluation_textBox2.Text));
                    cmd1.Parameters.AddWithValue("@AssessmentComponentId", int.Parse(evaluationAssessmentComponentID_comboBox2.Text));
                    cmd1.Parameters.AddWithValue("@RubricMeasurementId", int.Parse(evaluationRubricMeasurementLevel_comboBox1.Text));
                    DateTime date = DateTime.Now;
                    cmd1.Parameters.AddWithValue("@EvaluationDate", date);
                    cmd1.ExecuteNonQuery();
                    MessageBox.Show("Data has been saved Successfully", "Insertion Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ActiveStudentsView();
                    InActiveStudentViews();
                    CloView();
                    RubricView();
                    RubricLevelView();
                    AssessmentView();
                    AssessmentComponentView();
                    AttendanceView();
                    Evaluation1();
                    Evaluation2();
                    AssessmentReportView();
                    AttendanceGridCheckBoxes();
                    Count();
                    Evaluation_textBox2.Clear();
                    evaluationAssessmentComponentID_comboBox2.Items.Clear();
                    evaluationAssessmentComponentID_comboBox2.ResetText();
                    evaluationRubricMeasurementLevel_comboBox1.Items.Clear();
                    evaluationRubricMeasurementLevel_comboBox1.ResetText();

                }
                catch (Exception exp)
                {
                    MessageBox.Show(exp.Message);
                }
            }
        }

        private void evaluationAssessmentComponentID_comboBox2_Click(object sender, EventArgs e)
        {
            evaluationAssessmentComponentID_comboBox2.Items.Clear();
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd2 = new SqlCommand("Select Id from AssessmentComponent", con);
            DataTable dt3 = new DataTable();
            SqlDataAdapter da3 = new SqlDataAdapter(cmd2);
            da3.Fill(dt3);

            foreach (DataRow dr in dt3.Rows)
            {
                evaluationAssessmentComponentID_comboBox2.Items.Add(dr["Id"].ToString());
            }
        }

        private void evaluationRubricMeasurementLevel_comboBox1_Click(object sender, EventArgs e)
        {
            //evaluationRubricMeasurementLevel_comboBox1.Items.Clear();
            //var con = Configuration.getInstance().getConnection();
            //SqlCommand cmd2 = new SqlCommand("Select Id from RubricLevel", con);
            //DataTable dt3 = new DataTable();
            //SqlDataAdapter da3 = new SqlDataAdapter(cmd2);
            //da3.Fill(dt3);

            //foreach (DataRow dr in dt3.Rows)
            //{
            //    evaluationRubricMeasurementLevel_comboBox1.Items.Add(dr["Id"].ToString());
            //}
        }

        private void materialButton11_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(Evaluation_textBox2.Text) || String.IsNullOrEmpty(evaluationAssessmentComponentID_comboBox2.Text)
            || String.IsNullOrEmpty(evaluationRubricMeasurementLevel_comboBox1.Text))

            {
                // Input textbox is empty
                MessageBox.Show("Please enter all input values", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {

                    var con = Configuration.getInstance().getConnection();
                    SqlCommand cmd1 = new SqlCommand("UPDATE StudentResult SET RubricMeasurementId=@RubricMeasurementId where StudentId=@StudentId and AssessmentComponentId=@AssessmentComponentId", con);
                    cmd1.Parameters.AddWithValue("@StudentId", int.Parse(Evaluation_textBox2.Text));
                    cmd1.Parameters.AddWithValue("@AssessmentComponentId", int.Parse(evaluationAssessmentComponentID_comboBox2.Text));
                    cmd1.Parameters.AddWithValue("@RubricMeasurementId", int.Parse(evaluationRubricMeasurementLevel_comboBox1.Text));
                    DateTime date = DateTime.Now;
                    cmd1.Parameters.AddWithValue("@EvaluationDate", date);
                    cmd1.ExecuteNonQuery();
                    MessageBox.Show("Data has been updated Successfully", "Updation Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Evaluation2();
                    Evaluation_textBox2.Clear();
                    evaluationAssessmentComponentID_comboBox2.Items.Clear();
                    evaluationAssessmentComponentID_comboBox2.ResetText();
                    evaluationRubricMeasurementLevel_comboBox1.Items.Clear();
                    evaluationRubricMeasurementLevel_comboBox1.ResetText();
                }
                catch (Exception exp)
                {
                    MessageBox.Show(exp.Message);
                }
            }
        }

        private void Evaluation_dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex >= 0)
            {
                materialButton11.Enabled = true;
                Evaluation_textBox2.Text = Evaluation_dataGridView3.Rows[e.RowIndex].Cells["Id"].Value.ToString();
                evaluationAssessmentComponentID_comboBox2.Text = Evaluation_dataGridView3.Rows[e.RowIndex].Cells["AssessmentComponentId"].Value.ToString();
            }
        }

        private void AssessmentReport_comboBox1_Click(object sender, EventArgs e)
        {
            AssessmentReport_comboBox1.Items.Clear();
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd2 = new SqlCommand("Select Title from Assessment", con);
            DataTable dt3 = new DataTable();
            SqlDataAdapter da3 = new SqlDataAdapter(cmd2);
            da3.Fill(dt3);
            foreach (DataRow dr in dt3.Rows)
            {
                AssessmentReport_comboBox1.Items.Add(dr["Title"].ToString());
            }


        }

        private void materialButton9_Click(object sender, EventArgs e)
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd19 = new SqlCommand("WITH NewTable AS (SELECT DISTINCT S.RegistrationNumber, S.FirstName+ ''+S.LastName AS Name, A.Title,A.TotalMarks, A.Totalweightage, (CONVERT(FLOAT,RL.MeasurementLevel) / MAX(RL.MeasurementLevel) OVER() * AC.TotalMarks) AS ObtainedMarks, ((CONVERT(FLOAT,RL.MeasurementLevel) / MAX(RL.MeasurementLevel) OVER() * AC.TotalMarks)/A.TotalMarks * A.TotalWeightage) AS ObtainedWeightage FROM  Student S JOIN StudentResult SR ON S.Id=SR.StudentId JOIN RubricLevel RL ON SR.RubricMeasurementId=RL.Id JOIN Rubric R ON RL.RubricId=R.Id JOIN AssessmentComponent AC ON R.Id=AC.RubricId JOIN Assessment A ON AC.AssessmentId=A.Id WHERE SR.StudentId = [StudentId] AND AC.Id = [AssessmentComponentId] AND A.Title ='" + AssessmentReport_comboBox1.Text+ "') SELECT  NewTable.RegistrationNumber, NewTable.Name, NewTable.TotalMarks, SUM(NewTable.ObtainedMarks) AS ObtainedMarks, NewTable.TotalWeightage, SUM(NewTable.ObtainedWeightage) AS ObtainedWeightage FROM  NewTable GROUP BY NewTable.RegistrationNumber, NewTable.Name, NewTable.TotalMarks, NewTable.TotalWeightage", con);
            SqlDataAdapter da19 = new SqlDataAdapter(cmd19);
            DataTable dt19 = new DataTable();
            da19.Fill(dt19);
            AssessmentReportdataGridView5.DataSource = dt19;
            string reportName = AssessmentReport_comboBox1.Text + ' '+ "Result";
            GeneratePdfReportAssessment(dt19,reportName);
            AssessmentReport_comboBox1.Items.Clear();
            AssessmentReport_comboBox1.ResetText();

        }


    



public void GeneratePdfReportAssessment(DataTable dt,string reportName)
    {
        // Create a new PDF document
        PdfDocument document = new PdfDocument();

        // Create a page
        PdfPage page = document.AddPage();

        // Create a graphics object
        XGraphics gfx = XGraphics.FromPdfPage(page);


        // Create a text formatter
        XTextFormatter tf = new XTextFormatter(gfx);

            //// Set the report name
            //string reportName = "Assessment Wise Class Result";

            // Define the font and color for the report name
            XFont font1 = new XFont("Arial", 16, XFontStyle.Bold);
            XFont font2 = new XFont("Arial", 14, XFontStyle.Bold);
            XFont font = new XFont("Arial", 12);
            XBrush brush = XBrushes.Black;

            // Define the rectangle to draw the report name
            XRect reportNameRect = new XRect(0, 20, page.Width, 40);

            // Draw the grey background box for the report name
            XBrush greyBrush = XBrushes.LightGray;
            gfx.DrawRectangle(greyBrush, reportNameRect);

            // Draw the report name centered in the box
            XStringFormat centerFormat = new XStringFormat();
            centerFormat.Alignment = XStringAlignment.Center;
            centerFormat.LineAlignment = XLineAlignment.Center;

            gfx.DrawString(reportName, font1, brush, reportNameRect, centerFormat);

            // Write the table headers
            tf.DrawString("Registration Number", font2, XBrushes.Black, new XRect(20, 70, 150, 20), XStringFormats.TopLeft);
            tf.DrawString("Name", font2, XBrushes.Black, new XRect(170, 70, 150, 60), XStringFormats.TopLeft);
            tf.DrawString("Total Marks", font2, XBrushes.Black, new XRect(320, 70, 150, 20), XStringFormats.TopLeft);
            tf.DrawString("Obtained Marks", font2, XBrushes.Black, new XRect(470, 70, 150, 20), XStringFormats.TopLeft);
            tf.DrawString("Total Weightage", font2, XBrushes.Black, new XRect(620, 70, 150, 20), XStringFormats.TopLeft);
            tf.DrawString("Obtained Weightage", font, XBrushes.Black, new XRect(770, 700, 150, 20), XStringFormats.TopLeft);


        // Write the table data
        int y = 90;
        foreach (DataRow row in dt.Rows)
        {
            tf.DrawString(row["RegistrationNumber"].ToString(), font, XBrushes.Black, new XRect(20, y, 50, 20), XStringFormats.TopLeft);
            tf.DrawString(row["Name"].ToString(), font, XBrushes.Black, new XRect(170, y, 100, 20), XStringFormats.TopLeft);
            tf.DrawString(row["TotalMarks"].ToString(), font, XBrushes.Black, new XRect(320, y, 150, 20), XStringFormats.TopLeft);
            tf.DrawString(row["ObtainedMarks"].ToString(), font, XBrushes.Black, new XRect(470, y, 150, 20), XStringFormats.TopLeft);
            tf.DrawString(row["TotalWeightage"].ToString(), font, XBrushes.Black, new XRect(620, y, 150, 20), XStringFormats.TopLeft);
            tf.DrawString(row["ObtainedWeightage"].ToString(), font, XBrushes.Black, new XRect(770, y, 150, 20), XStringFormats.TopLeft);
            y += 20;
        }

            // Save the document
            string report = $"Assessment Wise Report_{AssessmentReport_comboBox1.Text}.pdf";
            document.Save(report);
            MessageBox.Show("Assessment Wise Report saved successfully", "Insertion Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

public void GeneratePdfReportClo(DataTable dt, string reportName)
    {
            // Create a new PDF document
            PdfDocument document = new PdfDocument();

            // Create a page
            PdfPage page = document.AddPage();

            // Create a graphics object
            XGraphics gfx = XGraphics.FromPdfPage(page);

            // Create a font
            XFont font1 = new XFont("Arial", 16, XFontStyle.Bold);
            XFont font2 = new XFont("Arial", 14, XFontStyle.Bold);
            XFont font = new XFont("Arial", 12);
            // Create a text formatter
            XTextFormatter tf = new XTextFormatter(gfx);
            XBrush brush = XBrushes.Black;

            // Define the rectangle to draw the report name
            XRect reportNameRect = new XRect(0, 20, page.Width, 40);

            // Draw the grey background box for the report name
            XBrush greyBrush = XBrushes.LightGray;
            gfx.DrawRectangle(greyBrush, reportNameRect);

            // Draw the report name centered in the box
            XStringFormat centerFormat = new XStringFormat();
            centerFormat.Alignment = XStringAlignment.Center;
            centerFormat.LineAlignment = XLineAlignment.Center;

            gfx.DrawString(reportName, font1, brush, reportNameRect, centerFormat);

            // Write the table headers
            tf.DrawString("Registration Number", font2, XBrushes.Black, new XRect(20, 70, 150, 20), XStringFormats.TopLeft);
            tf.DrawString("Name", font2, XBrushes.Black, new XRect(170, 70, 150, 60), XStringFormats.TopLeft);
            tf.DrawString("Total Marks", font2, XBrushes.Black, new XRect(320, 70, 150, 20), XStringFormats.TopLeft);
            tf.DrawString("Obtained Marks", font2, XBrushes.Black, new XRect(470, 70, 150, 20), XStringFormats.TopLeft);


            // Write the table data
            int y = 90;
            foreach (DataRow row in dt.Rows)
            {
                tf.DrawString(row["RegistrationNumber"].ToString(), font, XBrushes.Black, new XRect(20, y, 50, 20), XStringFormats.TopLeft);
                tf.DrawString(row["Name"].ToString(), font, XBrushes.Black, new XRect(170, y, 100, 20), XStringFormats.TopLeft);
                tf.DrawString(row["TotalMarks"].ToString(), font, XBrushes.Black, new XRect(320, y, 150, 20), XStringFormats.TopLeft);
                tf.DrawString(row["ObtainedMarks"].ToString(), font, XBrushes.Black, new XRect(470, y, 150, 20), XStringFormats.TopLeft);
                y += 20;
            }
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd21 = new SqlCommand("With cloTable As( Select Clo.Name,Clo.Id From Clo) Select distinct Clo.Name from clo join cloTable on clo.Id='" + CLO_report_comboBox2.Text + "'", con);
            string r = (string)cmd21.ExecuteScalar();
            // Save the document
            string report = $" Clo Wise Report_{r}.pdf";
            document.Save(report);
            MessageBox.Show("Clo Wise Report saved successfully", "Insertion Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }



        private void CLO_report_comboBox2_Click(object sender, EventArgs e)
        {
            CLO_report_comboBox2.Items.Clear();
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd2 = new SqlCommand("Select id from Clo", con);
            DataTable dt3 = new DataTable();
            SqlDataAdapter da3 = new SqlDataAdapter(cmd2);
            da3.Fill(dt3);
            foreach (DataRow dr in dt3.Rows)
            {
                CLO_report_comboBox2.Items.Add(dr["id"].ToString());
            }
        }

        private void materialButton7_Click(object sender, EventArgs e)
        {
            var con = Configuration.getInstance().getConnection();


            SqlCommand cmd20 = new SqlCommand(" With NewTable As( Select S.RegistrationNumber, S.FirstName+ ' '+S.LastName As Name,Clo.Name As [CLO Name],Clo.Id,A.Title,AC.Name As [Assessment Component Name],AC.TotalMarks As TotalMarks , A.Totalweightage, (convert(float,RL.MeasurementLevel) / MAX(RL.MeasurementLevel) OVER() * AC.TotalMarks) as ObtainedMarks, ((convert(float,RL.MeasurementLevel) / MAX(RL.MeasurementLevel) OVER() * AC.TotalMarks)/A.TotalMarks * A.TotalWeightage) as ObtainedWeightage from Student S join StudentResult SR On S.Id=SR.StudentId inner join RubricLevel RL on SR.RubricMeasurementId=RL.Id inner join Rubric R on RL.RubricId=R.Id inner join Clo on R.CloId=Clo.id inner join AssessmentComponent AC on R.Id=AC.RubricId inner join Assessment A ON AC.AssessmentId=A.Id  WHERE SR.StudentId = [StudentId] AND AC.Id = [AssessmentComponentId]) Select   NewTable.RegistrationNumber,NewTable.Name,sum(NewTable.TotalMarks) AS TotalMarks, Sum( NewTable.ObtainedMarks) As ObtainedMarks from NewTable where NewTable.Id='" + CLO_report_comboBox2.Text + "'Group BY NewTable.RegistrationNumber,NewTable.Name, NewTable.Title", con);
            SqlDataAdapter da20 = new SqlDataAdapter(cmd20);
            DataTable dt20 = new DataTable();
            da20.Fill(dt20);
            CLO_Wise_report_dataGridView6.DataSource = dt20;
            SqlCommand cmd21 = new SqlCommand("With cloTable As( Select Clo.Name,Clo.Id From Clo) Select distinct Clo.Name from clo join cloTable on clo.Id='"+ CLO_report_comboBox2.Text+"'", con);
            string reportName = (string)cmd21.ExecuteScalar();
            GeneratePdfReportClo(dt20, reportName);

            evaluationRubricMeasurementLevel_comboBox1.Items.Clear();
            evaluationRubricMeasurementLevel_comboBox1.ResetText();
        }

        private void MakeActiveStudents_materialButton13_Click(object sender, EventArgs e)
        {
            try
            {
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("UPDATE student SET Status=5 WHERE Id=@Id", con);

                SqlCommand cmd2 = new SqlCommand("Select LookupId From Lookup Where Name='Active'", con);
                Student_Status = (Int32)cmd2.ExecuteScalar();
                cmd2.ExecuteNonQuery();
                cmd.Parameters.AddWithValue("@Status", Student_Status);
                cmd.Parameters.AddWithValue("@Id", S_ID);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Student has been activated now.", "Insertion Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                InActiveStudentViews();
                ActiveStudentsView();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void InActive_dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex >= 0)
            {
                var con = Configuration.getInstance().getConnection();
                MakeActiveStudents_materialButton13.Enabled = true;
                string temp = this.InActive_dataGridView1.CurrentRow.Cells[4].Value.ToString();
                SqlCommand cmd = new SqlCommand("select Id from Student where RegistrationNumber=@RegistrationNumber And Status=6", con);
                cmd.Parameters.AddWithValue("RegistrationNumber", temp);
                S_ID = Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        private void materialButton10_Click(object sender, EventArgs e)
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd5 = new SqlCommand("Select SA.StudentId, S.FirstName + ' ' + S.LastName As Name, S.RegistrationNumber,(select Name from LookUp where LookupId=SA.AttendanceStatus) AS Attendance  From Student S Join StudentAttendance SA On S.Id=SA.StudentId JOIN ClassAttendance on SA.AttendanceId=ClassAttendance.Id where ClassAttendance.AttendanceDate=@Date", con);
            DateTime temp = Convert.ToDateTime(AttendanceReport_dateTimePicker2.Text);
            cmd5.Parameters.AddWithValue("@Date", temp);
            SqlDataAdapter da5 = new SqlDataAdapter(cmd5);
            DataTable dt5 = new DataTable();
            da5.Fill(dt5);
            AttendanceReport_dataGridView4.DataSource = dt5;

            string reportName = AttendanceReport_dateTimePicker2.Text + ' ' + "Attendance Report";
            GeneratePdfReportAttendance(dt5, reportName);

        }

        private void monthlyAttendanceReport_materialButton13_Click(object sender, EventArgs e)
        {
            var con = Configuration.getInstance().getConnection();

            SqlCommand cmd5 = new SqlCommand("SELECT " + "S.RegistrationNumber ,"+"S.FirstName + ' '+S.LastName As Name," +   "COUNT(CA.Id) AS TotalClasses, " +  "COUNT(CASE SA.AttendanceStatus WHEN 1 THEN SA.AttendanceId END) AS TotalPresent, " +   "COUNT(CASE SA.AttendanceStatus WHEN 2 THEN SA.AttendanceId END) AS TotalAbsent, " +    "COUNT(CASE SA.AttendanceStatus WHEN 3 THEN SA.AttendanceId END) AS TotalLeave, " +   "COUNT(CASE SA.AttendanceStatus WHEN 4 THEN SA.AttendanceId END) AS TotalLate, " +   "CONVERT(DECIMAL(5,2), (COUNT(CASE SA.AttendanceStatus WHEN 1 THEN SA.AttendanceId END) * 100.0) / COUNT(CA.Id)) AS AveragePresentRate " +   "FROM " +   "Student S " +   "LEFT JOIN ClassAttendance CA ON MONTH(CA.AttendanceDate) = " + MonthlyReport_dateTimePicker1.Value.ToString("MM") + " " +   "LEFT JOIN StudentAttendance SA ON CA.Id = SA.AttendanceId AND S.Id = SA.StudentId " +   "GROUP BY " + "S.RegistrationNumber, " +   "S.FirstName + ' ' +S.LastName", con);
            cmd5.Parameters.AddWithValue("@Date", MonthlyReport_dateTimePicker1.Value.Month);
            SqlDataAdapter da5 = new SqlDataAdapter(cmd5);
            DataTable dt5 = new DataTable();
            da5.Fill(dt5);
            MonthlyAttendanceReport_dataGridView2.DataSource = dt5;
            string reportName = "Overall Attendance Report";
            GeneratePdfReportMonthlyAttendance(dt5, reportName);

        }

        public void GeneratePdfReportMonthlyAttendance(DataTable dt, string reportName)
        {
            // Create a new PDF document
            PdfDocument document = new PdfDocument();

            // Create a page
            PdfPage page = document.AddPage();

            // Create a graphics object
            XGraphics gfx = XGraphics.FromPdfPage(page);

            // Create a font
            XFont font1 = new XFont("Arial", 16, XFontStyle.Bold);
            XFont font2 = new XFont("Arial", 14, XFontStyle.Bold);
            XFont font = new XFont("Arial", 12);

            // Create a text formatter
            XTextFormatter tf = new XTextFormatter(gfx);

            XBrush brush = XBrushes.Black;

            // Define the rectangle to draw the report name
            XRect reportNameRect = new XRect(0, 20, page.Width, 40);

            // Draw the grey background box for the report name
            XBrush greyBrush = XBrushes.LightGray;
            gfx.DrawRectangle(greyBrush, reportNameRect);

            // Draw the report name centered in the box
            XStringFormat centerFormat = new XStringFormat();
            centerFormat.Alignment = XStringAlignment.Center;
            centerFormat.LineAlignment = XLineAlignment.Center;

            gfx.DrawString(reportName, font1, brush, reportNameRect, centerFormat);

            // Write the table headers
            tf.DrawString("Reg No", font2, XBrushes.Black, new XRect(20, 70, 150, 20), XStringFormats.TopLeft);
            tf.DrawString("Name", font2, XBrushes.Black, new XRect(105, 70, 150, 60), XStringFormats.TopLeft);
            tf.DrawString("Classes", font2, XBrushes.Black, new XRect(240, 70, 150, 20), XStringFormats.TopLeft);
            tf.DrawString("Present", font2, XBrushes.Black, new XRect(300, 70, 150, 20), XStringFormats.TopLeft);
            tf.DrawString("Absent", font2, XBrushes.Black, new XRect(360, 70, 150, 20), XStringFormats.TopLeft);
            tf.DrawString("Leave", font2, XBrushes.Black, new XRect(410, 70, 150, 20), XStringFormats.TopLeft);
            tf.DrawString("Late", font2, XBrushes.Black, new XRect(455, 70, 150, 20), XStringFormats.TopLeft);
            tf.DrawString("AveragePresence", font2, XBrushes.Black, new XRect(490, 70, 150, 20), XStringFormats.TopLeft);

            // Write the table data
            int y = 90;
            foreach (DataRow row in dt.Rows)
            {
                tf.DrawString(row["RegistrationNumber"].ToString(), font, XBrushes.Black, new XRect(20, y, 50, 20), XStringFormats.TopLeft);
                tf.DrawString(row["Name"].ToString(), font, XBrushes.Black, new XRect(105, y, 100, 20), XStringFormats.TopLeft);
                tf.DrawString(row["TotalClasses"].ToString(), font, XBrushes.Black, new XRect(240, y, 150, 20), XStringFormats.TopLeft);
                tf.DrawString(row["TotalPresent"].ToString(), font, XBrushes.Black, new XRect(300, y, 150, 20), XStringFormats.TopLeft);
                tf.DrawString(row["TotalAbsent"].ToString(), font, XBrushes.Black, new XRect(360, y, 150, 20), XStringFormats.TopLeft);
                tf.DrawString(row["TotalLeave"].ToString(), font, XBrushes.Black, new XRect(410, y, 150, 20), XStringFormats.TopLeft);
                tf.DrawString(row["TotalLate"].ToString(), font, XBrushes.Black, new XRect(455, y, 150, 20), XStringFormats.TopLeft);
                tf.DrawString(row["AveragePresentRate"].ToString(), font, XBrushes.Black, new XRect(490, y, 150, 20), XStringFormats.TopLeft);

                y += 20;
            }

            // Save the document
            string report = $" Monthly Attendance Report_{MonthlyReport_dateTimePicker1.Value.Month}.pdf";
            document.Save(report);
            MessageBox.Show(" Monthly Attendance Report saved successfully", "Insertion Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void StudentWise_materialButton14_Click(object sender, EventArgs e)
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd5 = new SqlCommand("WITH NewTable AS (SELECT distinct S.RegistrationNumber  ,\r\nA.Title,A.TotalMarks, A.TotalWeightage, \r\n(CONVERT(FLOAT, RL.MeasurementLevel) / MAX(RL.MeasurementLevel) OVER() * AC.TotalMarks)\r\nAS ObtainedMarks,Round(((CONVERT(FLOAT, RL.MeasurementLevel) / MAX(RL.MeasurementLevel)\r\nOVER() * AC.TotalMarks) / A.TotalMarks * A.TotalWeightage),3) AS ObtainedWeightage\r\nFROM Student S JOIN StudentResult SR ON S.Id = SR.StudentId  \r\nJOIN RubricLevel RL ON SR.RubricMeasurementId = RL.Id\r\nJOIN Rubric R ON RL.RubricId = R.Id\r\nJOIN AssessmentComponent AC ON R.Id = AC.RubricId\r\nJOIN Assessment A ON AC.AssessmentId = A.Id\r\nWHERE  S.RegistrationNumber =@Reg)\r\nSELECT NewTable.RegistrationNumber, NewTable.Title, NewTable.TotalMarks, \r\nSUM(NewTable.ObtainedMarks) AS ObtainedMarks, NewTable.TotalWeightage, \r\nSUM(NewTable.ObtainedWeightage) AS ObtainedWeightage \r\nFROM NewTable GROUP BY   NewTable.RegistrationNumber, \r\nNewTable.TotalMarks, NewTable.TotalWeightage,NewTable.Title ", con);
            cmd5.Parameters.AddWithValue("@Reg", StudentWiseReport_comboBox1.Text);
            SqlDataAdapter da5 = new SqlDataAdapter(cmd5);
            DataTable dt5 = new DataTable();
            da5.Fill(dt5);
            StudentWiseReport_dataGridView3.DataSource = dt5;
            string reportName = StudentWiseReport_comboBox1.Text +' '+" Report";
            GeneratePdfStudentWiseReport(dt5, reportName);

        }

        public void GeneratePdfStudentWiseReport(DataTable dt, string reportName)
        {
            // Create a new PDF document
            PdfDocument document = new PdfDocument();

            // Create a page
            PdfPage page = document.AddPage();

            // Create a graphics object
            XGraphics gfx = XGraphics.FromPdfPage(page);

            // Create a font
            XFont font1 = new XFont("Arial", 16, XFontStyle.Bold);
            XFont font2 = new XFont("Arial", 14, XFontStyle.Bold);
            XFont font = new XFont("Arial", 12);
            // Create a text formatter
            XTextFormatter tf = new XTextFormatter(gfx);
            XBrush brush = XBrushes.Black;

            // Define the rectangle to draw the report name
            XRect reportNameRect = new XRect(0, 20, page.Width, 40);

            // Draw the grey background box for the report name
            XBrush greyBrush = XBrushes.LightGray;
            gfx.DrawRectangle(greyBrush, reportNameRect);

            // Draw the report name centered in the box
            XStringFormat centerFormat = new XStringFormat();
            centerFormat.Alignment = XStringAlignment.Center;
            centerFormat.LineAlignment = XLineAlignment.Center;

            gfx.DrawString(reportName, font1, brush, reportNameRect, centerFormat);

            // Write the table headers
            tf.DrawString("RegNo ", font2, XBrushes.Black, new XRect(20, 70, 150, 20), XStringFormats.TopLeft);
            tf.DrawString("A-Title", font2, XBrushes.Black, new XRect(120, 70, 150, 20), XStringFormats.TopLeft);
            tf.DrawString("Marks", font2, XBrushes.Black, new XRect(240, 70, 150, 20), XStringFormats.TopLeft);
            tf.DrawString("weightage", font2, XBrushes.Black, new XRect(340, 70, 150, 20), XStringFormats.TopLeft);
            tf.DrawString("ObtMarks", font2, XBrushes.Black, new XRect(420, 70, 150, 20), XStringFormats.TopLeft);
            tf.DrawString("ObtWeightage", font2, XBrushes.Black, new XRect(500, 70, 150, 20), XStringFormats.TopLeft);


            // Write the table data
            int y = 90;
            foreach (DataRow row in dt.Rows)
            {
                tf.DrawString(row["RegistrationNumber"].ToString(), font, XBrushes.Black, new XRect(20, y, 50, 20), XStringFormats.TopLeft);
                tf.DrawString(row["Title"].ToString(), font, XBrushes.Black, new XRect(120, y, 150, 20), XStringFormats.TopLeft);
                tf.DrawString(row["TotalMarks"].ToString(), font, XBrushes.Black, new XRect(240, y, 150, 20), XStringFormats.TopLeft);
                tf.DrawString(row["TotalWeightage"].ToString(), font, XBrushes.Black, new XRect(340, y, 150, 20), XStringFormats.TopLeft);
                tf.DrawString(row["ObtainedMarks"].ToString(), font, XBrushes.Black, new XRect(420, y, 150, 20), XStringFormats.TopLeft);
                tf.DrawString(row["ObtainedWeightage"].ToString(), font, XBrushes.Black, new XRect(500, y, 150, 20), XStringFormats.TopLeft);
                y += 20;
            }
            // Save the document
            string report = $" StudentWiseReport_{StudentWiseReport_comboBox1.Text}.pdf";
            document.Save(report);
            MessageBox.Show("Overall Student Report saved successfully", "Insertion Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        private void OverallClo_materialButton12_Click(object sender, EventArgs e)
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd5 = new SqlCommand(" SELECT distinct C.Name As [CLO Name],R.Details As Rubric, A.Title AS Assessment,A.TotalMarks As AssessmentMarks, COUNT(DISTINCT SR.StudentId) As [Student Attempted], COUNT(distinct AC.Id) As CountofAssessmentComponent\r\nFROM Assessment A\r\nJOIN AssessmentComponent AC ON A.Id = AC.AssessmentId\r\nJOIN Rubric R ON AC.RubricId = R.Id\r\nJOIN CLO C ON R.CloId = C.Id\r\nLEFT JOIN StudentResult SR ON AC.Id = SR.AssessmentComponentId\r\nGROUP BY A.Id, A.Title, A.DateCreated, A.TotalMarks, R.Details,C.Name;", con);
            SqlDataAdapter da5 = new SqlDataAdapter(cmd5);
            DataTable dt5 = new DataTable();
            da5.Fill(dt5);
            OverallCloReportdataGridView4.DataSource = dt5;
            string reportName = "Overall CLO Report";
            GeneratePdfOverAllReportClo(dt5,reportName);

        }

        public void GeneratePdfOverAllReportClo(DataTable dt, string reportName)
        {
            // Create a new PDF document
            PdfDocument document = new PdfDocument();

            // Create a page
            PdfPage page = document.AddPage();

            // Create a graphics object
            XGraphics gfx = XGraphics.FromPdfPage(page);

            // Create a font
            XFont font1 = new XFont("Arial", 16, XFontStyle.Bold);
            XFont font2 = new XFont("Arial", 14, XFontStyle.Bold);
            XFont font = new XFont("Arial", 12);
            // Create a text formatter
            XTextFormatter tf = new XTextFormatter(gfx);
            XBrush brush = XBrushes.Black;

            // Define the rectangle to draw the report name
            XRect reportNameRect = new XRect(0, 20, page.Width, 40);

            // Draw the grey background box for the report name
            XBrush greyBrush = XBrushes.LightGray;
            gfx.DrawRectangle(greyBrush, reportNameRect);

            // Draw the report name centered in the box
            XStringFormat centerFormat = new XStringFormat();
            centerFormat.Alignment = XStringAlignment.Center;
            centerFormat.LineAlignment = XLineAlignment.Center;

            gfx.DrawString(reportName, font1, brush, reportNameRect, centerFormat);

            // Write the table headers
            tf.DrawString("CLO ", font2, XBrushes.Black, new XRect(20, 70, 150, 20), XStringFormats.TopLeft);
            tf.DrawString("Rubric", font2, XBrushes.Black, new XRect(120, 70, 150, 60), XStringFormats.TopLeft);
            tf.DrawString("Assessment", font2, XBrushes.Black, new XRect(200, 70, 150, 20), XStringFormats.TopLeft);
            tf.DrawString("Marks", font2, XBrushes.Black, new XRect(300, 70, 150, 20), XStringFormats.TopLeft);
            tf.DrawString("AttemptedStudent", font2, XBrushes.Black, new XRect(370, 70, 150, 20), XStringFormats.TopLeft);
            tf.DrawString("AComponents", font2, XBrushes.Black, new XRect(510, 70, 150, 20), XStringFormats.TopLeft);

            // Write the table data
            int y = 90;
            foreach (DataRow row in dt.Rows)
            {
                tf.DrawString(row["CLO Name"].ToString(), font, XBrushes.Black, new XRect(20, y, 50, 20), XStringFormats.TopLeft);
                tf.DrawString(row["Rubric"].ToString(), font, XBrushes.Black, new XRect(120, y, 100, 20), XStringFormats.TopLeft);
                tf.DrawString(row["Assessment"].ToString(), font, XBrushes.Black, new XRect(200, y, 150, 20), XStringFormats.TopLeft);
                tf.DrawString(row["AssessmentMarks"].ToString(), font, XBrushes.Black, new XRect(300, y, 150, 20), XStringFormats.TopLeft);
                tf.DrawString(row["Student Attempted"].ToString(), font, XBrushes.Black, new XRect(370, y, 150, 20), XStringFormats.TopLeft);
                tf.DrawString(row["CountofAssessmentComponent"].ToString(), font, XBrushes.Black, new XRect(510, y, 150, 20), XStringFormats.TopLeft);
                y += 20;
            }
            // Save the document
            string report = $" Overall Clo Report.pdf";
            document.Save(report);
            MessageBox.Show("Overall Clo Report saved successfully", "Insertion Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }



        public void GeneratePdfReportAttendance(DataTable dt, string reportName)
        {
            // Create a new PDF document
            PdfDocument document = new PdfDocument();

            // Create a page
            PdfPage page = document.AddPage();

            // Create a graphics object
            XGraphics gfx = XGraphics.FromPdfPage(page);

            // Create a font
            XFont font1 = new XFont("Arial", 16, XFontStyle.Bold);
            XFont font2 = new XFont("Arial", 14, XFontStyle.Bold);
            XFont font = new XFont("Arial", 12);

            // Create a text formatter
            XTextFormatter tf = new XTextFormatter(gfx);

            XBrush brush = XBrushes.Black;

            // Define the rectangle to draw the report name
            XRect reportNameRect = new XRect(0, 20, page.Width, 40);

            // Draw the grey background box for the report name
            XBrush greyBrush = XBrushes.LightGray;
            gfx.DrawRectangle(greyBrush, reportNameRect);

            // Draw the report name centered in the box
            XStringFormat centerFormat = new XStringFormat();
            centerFormat.Alignment = XStringAlignment.Center;
            centerFormat.LineAlignment = XLineAlignment.Center;

            gfx.DrawString(reportName, font1, brush, reportNameRect, centerFormat);

            // Write the table headers
            tf.DrawString("Student Id", font2, XBrushes.Black, new XRect(20, 70, 150, 20), XStringFormats.TopLeft);
            tf.DrawString("Name", font2, XBrushes.Black, new XRect(100, 70, 150, 60), XStringFormats.TopLeft);
            tf.DrawString("Registration Number", font2, XBrushes.Black, new XRect(320, 70, 150, 20), XStringFormats.TopLeft);
            tf.DrawString("Attendance", font2, XBrushes.Black, new XRect(470, 70, 150, 20), XStringFormats.TopLeft);

            // Write the table data
            int y = 90;
            foreach (DataRow row in dt.Rows)
            {
                tf.DrawString(row["StudentId"].ToString(), font, XBrushes.Black, new XRect(20, y, 50, 20), XStringFormats.TopLeft);
                tf.DrawString(row["Name"].ToString(), font, XBrushes.Black, new XRect(100, y, 100, 20), XStringFormats.TopLeft);
                tf.DrawString(row["RegistrationNumber"].ToString(), font, XBrushes.Black, new XRect(320, y, 150, 20), XStringFormats.TopLeft);
                tf.DrawString(row["Attendance"].ToString(), font, XBrushes.Black, new XRect(470, y, 150, 20), XStringFormats.TopLeft);
                y += 20;
            }

            // Save the document
            string report = $"Attendance Wise Report_{AttendanceReport_dateTimePicker2.Text}.pdf";
            document.Save(report);
            MessageBox.Show("Attendance Wise Report saved successfully", "Insertion Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void AssessmentMarks_textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void AssessmentSearch_materialComboBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void RubricLevel_RubricId_comboBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void ASS_ComponentRubricID_comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void ASS_ComponentAssID_comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void ASS_ComponentRubricID_comboBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void ASS_ComponentAssID_comboBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void evaluationAssessmentComponentID_comboBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void evaluationRubricMeasurementLevel_comboBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void Rubric_CLOID_comboBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void RubricLevel_comboBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void AssessmentReport_comboBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void CLO_report_comboBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void S_Search_materialComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            S_Search_textBox1.Visible = true;
            S_Search_textBox1.Clear();
        }

        private void AssessmentReport_comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            materialButton9.Enabled = true;
        }

        private void CLO_report_comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            materialButton7.Enabled = true;
        }

        private void Clo_Search_materialComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Clo_Search_textBox1.Visible = true;
            Clo_Search_textBox1.Clear();
        }

        private void Rubric_Search_materialComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Rubric_Search_textBox1.Visible= true;
            Rubric_Search_textBox1.Clear();
        }

        private void materialComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox1.Visible = true;
            textBox1.Clear();
        }

        private void ASS_ComponentSearch_materialComboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            ASS_ComponentSearch_textBox7.Visible = true;
            ASS_ComponentSearch_textBox7.Clear();
        }

        private void tableLayoutPanel36_Paint(object sender, PaintEventArgs e)
        {

        }

        private void AttendanceMarkStatus_dataGridView7_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void StudentWiseReport_comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            StudentWise_materialButton14.Enabled = true;

        }
    }
        }

