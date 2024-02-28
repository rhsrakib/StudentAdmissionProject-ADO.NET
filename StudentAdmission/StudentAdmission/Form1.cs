using StudentAdmission.DAL;
using StudentAdmission.Report;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StudentAdmission
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            FillSubjects();
            ResetStudentsMaster();
            ResetAdmission();
            SetGridStudentsMaster();
        }
        private void SetGridStudentsMaster()
        {
            dgvDetails.DataSource = null;
            dgvDetails.DataSource = GetStudentsMaster();
        }

        public List<StudentsMasterVM> GetStudentsMaster()
        {
            var listStudentsMaster = new List<StudentsMasterVM>();
            using (SqlConnection con = new SqlConnection(ConncetionHelper.Student))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand(@"SELECT StudentID,StudentName,Phone,Address,DOB,Gender FROM StudentsMaster ORDER BY StudentID", con);
                    cmd.CommandType = CommandType.Text;
                    con.Open();
                    SqlDataReader dataReader = cmd.ExecuteReader();
                    while (dataReader.Read())
                    {
                        var oStudentsMaster = new StudentsMasterVM();
                        oStudentsMaster.StudentID = dataReader.GetInt32(0);
                        oStudentsMaster.StudentName = dataReader.GetString(1);
                        oStudentsMaster.Phone = dataReader.GetString(2);
                        oStudentsMaster.Address = dataReader.GetString(3);
                        oStudentsMaster.DOB = dataReader.GetDateTime(4);
                        oStudentsMaster.Gender = dataReader.GetString(5);
                        listStudentsMaster.Add(oStudentsMaster);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    con.Close();
                }
                return listStudentsMaster;
            }
        }

        public StudentsMasterVM GetStudentsMaster(int studentId)
        {
            var oStudentsMaster = new StudentsMasterVM();
            using (SqlConnection con = new SqlConnection(ConncetionHelper.Student))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand(@"SELECT StudentID,StudentName,Phone,Address,DOB,Gender FROM StudentsMaster WHERE StudentID=" + studentId, con);
                    cmd.CommandType = CommandType.Text;
                    con.Open();
                    SqlDataReader dataReader = cmd.ExecuteReader();
                    while (dataReader.Read())
                    {
                        oStudentsMaster.StudentID = dataReader.GetInt32(0);
                        oStudentsMaster.StudentName = dataReader.GetString(1);
                        oStudentsMaster.Phone = dataReader.GetString(2);
                        oStudentsMaster.Address = dataReader.GetString(3);
                        oStudentsMaster.DOB = dataReader.GetDateTime(4);
                        oStudentsMaster.Gender = dataReader.GetString(5);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    con.Close();
                }
                return oStudentsMaster;
            }   
        }

        public List<AdmissionVM> GetAdmission(int studentMasterID)
        {
            var listAdmissionItem = new List<AdmissionVM>();
            using (SqlConnection con = new SqlConnection(ConncetionHelper.Student))
            {
                try
                {
                  
                    SqlCommand cmd = new SqlCommand(@"SELECT ad.[AdmissionID]
                                                    ,ad.[StudentMasterID]
                                                    ,ad.[SubjectID]
                                                    ,ad.[Credits]
                                                    ,ad.[Semester]
                                                    ,ad.[TotalCredits]
                                                    ,ad.[TotalFee]
                                                    ,s.SubjectName
                                                    FROM [Admission] ad
                                                    LEFT JOIN [Subjects] s ON s.SubjectID=ad.SubjectID
                                                    WHERE ad.[StudentMasterID]=" + studentMasterID, con);
                    cmd.CommandType = CommandType.Text;
                    con.Open();
                    SqlDataReader dataReader = cmd.ExecuteReader();
                    while (dataReader.Read())
                    {
                        var oAdmissionItem = new AdmissionVM();
                        oAdmissionItem.AdmissionID = dataReader.GetInt32(0);
                        oAdmissionItem.StudentMasterID = dataReader.GetInt32(1);
                        oAdmissionItem.SubjectID = dataReader.GetInt32(2);
                        oAdmissionItem.Credits = dataReader.GetDecimal(3);
                        oAdmissionItem.Semester = dataReader.GetDecimal(4);
                        oAdmissionItem.TotalCredits = dataReader.GetDecimal(5);
                        oAdmissionItem.TotalFee = dataReader.GetDecimal(6);
                        oAdmissionItem.SubjectName = dataReader.GetString(7);
                        listAdmissionItem.Add(oAdmissionItem);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    con.Close();
                }
                return listAdmissionItem;
            }
        }

        public List<SubjectsVM> GetItem()
        {
            var listSubject = new List<SubjectsVM>();
            using (SqlConnection con = new SqlConnection(ConncetionHelper.Student))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SELECT SubjectID, SubjectName FROM Subjects ORDER BY SubjectName", con);
                    cmd.CommandType = CommandType.Text;
                    con.Open();
                    SqlDataReader dataReader = cmd.ExecuteReader();

                    while (dataReader.Read())
                    {
                        SubjectsVM oSubject = new SubjectsVM();
                        oSubject.SubjectID = dataReader.GetInt32(0);
                        oSubject.SubjectName = dataReader.GetString(1);
                        listSubject.Add(oSubject);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    con.Close();
                }
                return listSubject;
            }              
        }
        public void FillSubjects()
        {
            try
            {
                cbSubjects.Items.Clear();
                cbSubjects.DataSource = GetItem();
                cbSubjects.ValueMember = "SubjectID";
                cbSubjects.DisplayMember = "SubjectName";
                cbSubjects.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
            }
        }
        public SubjectsVM GetSubjectsById(int Id)
        {
            var oSubjects = new SubjectsVM();
            using (SqlConnection con = new SqlConnection(ConncetionHelper.Student))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SELECT SubjectID, SubjectName, Credits, TotalCredits FROM Subjects WHERE SubjectID = " + Id, con);
                    cmd.CommandType = CommandType.Text;
                    con.Open();
                    SqlDataReader dataReader = cmd.ExecuteReader();
                    while (dataReader.Read())
                    {
                        oSubjects.SubjectID = dataReader.GetInt32(0);
                        oSubjects.SubjectName = dataReader.GetString(1);
                        oSubjects.Credits = dataReader.GetDecimal(2);
                        oSubjects.TotalCredits = dataReader.GetDecimal(3);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    con.Close();
                }
                return oSubjects;
            }  
        }
        private void cbSubjects_SelectedIndexChanged(object sender, EventArgs e)
        {
            var subjects = (SubjectsVM)cbSubjects.SelectedItem;
            var oSubjects = GetSubjectsById(subjects.SubjectID);
            if (oSubjects != null)
            {
                txtCredits.Text = oSubjects.Credits.ToString();
                txtTotalCredit.Text = oSubjects.TotalCredits.ToString();
                txtTotalFee.Text = CalculateTotalFee(Convert.ToDecimal(oSubjects.Credits), Convert.ToDecimal(txtSemester.Text.Trim()), Convert.ToDecimal(oSubjects.TotalCredits)).ToString();
            }
        }
        private static decimal CalculateTotalFee(decimal credits, decimal semester, decimal totalCredit)
        {
            decimal netCredits = credits * semester;
            return netCredits * 5600;
        }

        private void txtSemester_TextChanged(object sender, EventArgs e)
        {
            txtTotalFee.Text = CalculateTotalFee(Convert.ToDecimal(txtCredits.Text.Trim()), Convert.ToDecimal(txtSemester.Text.Trim()), Convert.ToDecimal(txtTotalCredit.Text.Trim())).ToString();
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            AdmissionVM oAddVM = new AdmissionVM();
            oAddVM.TotalFee = Convert.ToDecimal(txtTotalFee.Text.Trim());
            oAddVM.Credits = Convert.ToDecimal(txtCredits.Text.Trim());
            oAddVM.Semester = Convert.ToDecimal(txtSemester.Text.Trim());
            oAddVM.TotalCredits = Convert.ToDecimal(txtTotalCredit.Text.Trim());
            if (oAddVM.Credits > 0)
            {
                var oItem = (SubjectsVM)cbSubjects.SelectedItem;
                oAddVM.SubjectID = oItem.SubjectID;
                oAddVM.AdmissionID = (int)DateTime.UtcNow.Subtract(new DateTime(2020, 1, 1)).TotalSeconds;
                oAddVM.SubjectName = oItem.SubjectName.Trim();
                var listSI = dgvSubject.DataSource == null ? new List<AdmissionVM>() : (List<AdmissionVM>)dgvSubject.DataSource;
                listSI.Add(oAddVM);
                dgvSubject.DataSource = null;
                dgvSubject.DataSource = listSI;
                ResetAdmission();
            }
            else
            {
                MessageBox.Show("Choose a subject.");
            }
        }
        public StudentsMasterVM AddStudentsMaster(StudentsMasterVM model)
        {
            SqlCommand cmd = null;
            using (SqlConnection con = new SqlConnection(ConncetionHelper.Student))
            {
                try
                {
                    #region ADD

                    cmd = new SqlCommand("INSERT INTO StudentsMaster (StudentName,Phone,Address,DOB,Gender) VALUES('" + model.StudentName + "','" + model.Phone + "','" + model.Address + "','" + model.DOB + "','" + model.Gender + "')", con);
                    cmd.CommandType = CommandType.Text;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    #endregion
                    #region Get Students-Master-ID
                    using (SqlConnection con1 = new SqlConnection(ConncetionHelper.Student))
                    {
                        cmd = new SqlCommand("SELECT MAX(StudentID) FROM StudentsMaster", con1);
                        cmd.CommandType = CommandType.Text;
                        con1.Open();
                        model.StudentID = Convert.ToInt32(cmd.ExecuteScalar().ToString());
                        con1.Close();
                    }
                    #endregion
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    con.Close();
                }
                return model;
            } 
        }
        public StudentsMasterVM UpdateStudentsMaster(StudentsMasterVM model)
        {
            SqlCommand cmd = null;
            using (SqlConnection con = new SqlConnection(ConncetionHelper.Student))
            {
                try
                {
                    #region ADD

                    cmd = new SqlCommand("UPDATE StudentsMaster SET StudentName='" + model.StudentName + "',Phone='" + model.Phone + "',Address='" + model.Address + "',DOB='" + model.DOB + "',Gender='" + model.Gender + "' WHERE StudentID=" + model.StudentID, con);
                    cmd.CommandType = CommandType.Text;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    #endregion
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    con.Close();
                }
                return model;
            }  
        }
        public void RemoveAdmission(int studentID)
        {
            SqlCommand cmd = null;
            using (SqlConnection con = new SqlConnection(ConncetionHelper.Student))
            {
                try
                {
                    #region ADD
                    
                    cmd = new SqlCommand("DELETE Admission WHERE [StudentMasterID]=" + studentID, con);
                    cmd.CommandType = CommandType.Text;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    #endregion
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    con.Close();
                }
            }  
        }
        public void RemoveStudentsMaster(int studentID)
        {
            SqlCommand cmd = null;
            using (SqlConnection con = new SqlConnection(ConncetionHelper.Student)) {
                try
                {
                    #region ADD
                  
                    cmd = new SqlCommand("DELETE StudentsMaster WHERE StudentID=" + studentID, con);
                    cmd.CommandType = CommandType.Text;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    #endregion
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    con.Close();
                }
            }    
        }
        public void AddAdmission(List<AdmissionVM> admissions, int studentMasterID)
        {
            foreach (AdmissionVM subject in admissions)
            {
                subject.StudentMasterID = studentMasterID;
               
                SqlCommand cmd = null;
                using (SqlConnection con = new SqlConnection(ConncetionHelper.Student))
                {
                    try
                    {
                        #region ADD
                       
                        cmd = new SqlCommand(@"INSERT INTO [dbo].[Admission]
                                               ([StudentMasterID]
                                               ,[SubjectID]
                                               ,[Credits]
                                               ,[Semester]
                                               ,[TotalCredits]
                                               ,[TotalFee])
                                            VALUES
                                               ('" + subject.StudentMasterID + "'" +
                                                   ",'" + subject.SubjectID + "'" +
                                                   ",'" + subject.Credits + "'" +
                                                   ",'" + subject.Semester + "'" +
                                                   ",'" + subject.TotalCredits + "'" +
                                                   ",'" + subject.TotalFee + "')", con);
                        cmd.CommandType = CommandType.Text;
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                        #endregion
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    finally
                    {
                        con.Close();
                    }
                }  
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            #region Students Master
            var StudentID = Convert.ToInt32(lblStudentID.Text.Trim());
            #region Set Students-Master Data
            var oStudentsMaster = new StudentsMasterVM();
            oStudentsMaster.StudentID = StudentID;
            oStudentsMaster.StudentName = txtStudent.Text.Trim();
            oStudentsMaster.Phone = txtPhone.Text.Trim();
            oStudentsMaster.Address = txtAddress.Text.Trim();
            oStudentsMaster.DOB = txtDOB.Value;
            oStudentsMaster.Gender = radioMale.Checked == true ? "Male" : "Female";
            #endregion
            if (StudentID > 0)
            {
                #region UPDATE
                oStudentsMaster = UpdateStudentsMaster(oStudentsMaster);
                #endregion
            }
            else
            {
                #region ADD
                oStudentsMaster = AddStudentsMaster(oStudentsMaster);
                #endregion
            }
            #endregion
            #region Admission
            RemoveAdmission(oStudentsMaster.StudentID);
            var listSI = dgvSubject.DataSource == null ? new List<AdmissionVM>() : (List<AdmissionVM>)dgvSubject.DataSource;
            AddAdmission(listSI, oStudentsMaster.StudentID);
            #endregion
            ResetAdmission();
            dgvSubject.DataSource = null;
            ResetStudentsMaster();
            SetGridStudentsMaster();
            MessageBox.Show("Saved successfully.", "Message");
        }
        private void dgvSubject_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvSubject.Columns[e.ColumnIndex].Name == "btnRemove")
            {
                var AdmissionID = Convert.ToInt32(dgvSubject.Rows[e.RowIndex].Cells["AdmissionID"].Value);
                var listAdmission = dgvSubject.DataSource == null ? new List<AdmissionVM>() : (List<AdmissionVM>)dgvSubject.DataSource;
                var oAdmission = listAdmission.Where(x => x.AdmissionID == AdmissionID).FirstOrDefault();
                if (oAdmission != null)
                {
                    listAdmission.Remove(oAdmission);
                    dgvSubject.DataSource = null;
                    dgvSubject.DataSource = listAdmission;
                }
            }
        }
        private void ResetAdmission()
        {
            txtTotalFee.Text = "0";
            txtSemester.Text = "0";
            txtCredits.Text = "0";
            txtTotalCredit.Text = "0";
        }

        private void ResetStudentsMaster()
        {
            lblStudentID.Text = "0";
            txtAddress.Text = "";
            txtStudent.Text = "";
            txtPhone.Text = "";
            txtDOB.Text = "";
            radioMale.Checked = true;
            radioFemale.Checked = false;
            btnSubmit.Text = "ADD";
        }
        private void dgvDetails_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvDetails.Columns[e.ColumnIndex].Name == "btnReports")
            {
                Details cdr = new Details();
                cdr.Show();
            }
            else if (dgvDetails.Columns[e.ColumnIndex].Name == "btnDelete")
            {
                DialogResult dialogResult = MessageBox.Show("Are you sure to delete?", "Confirm!", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    var StudentID = Convert.ToInt32(dgvDetails.Rows[e.RowIndex].Cells["StudentID"].Value);
                    #region Sale Item
                    RemoveAdmission(StudentID);
                    #endregion
                    #region Students Master
                    RemoveStudentsMaster(StudentID);
                    #endregion
                    ResetStudentsMaster();
                    dgvSubject.DataSource = null;
                    ResetAdmission();
                    GetStudentsMaster();
                    MessageBox.Show("Deleted successfully.", "Message");
                }
            }
            else if (dgvDetails.Columns[e.ColumnIndex].Name == "btnEdit")
            {
                var StudentID = Convert.ToInt32(dgvDetails.Rows[e.RowIndex].Cells["StudentID"].Value);
                lblStudentID.Text = StudentID.ToString();
                #region Students Master
                var oSM = GetStudentsMaster(StudentID);
                if (oSM != null)
                {
                    txtAddress.Text = oSM.Address;
                    txtStudent.Text = oSM.StudentName;
                    txtPhone.Text = oSM.Phone;
                    txtDOB.Text = oSM.DOB.ToShortDateString();
                    radioMale.Text = oSM.Gender;
                    radioFemale.Text = oSM.Gender;
                }
                #endregion
                #region Student
                var listSI = GetAdmission(StudentID);
                dgvSubject.DataSource = null;
                dgvSubject.DataSource = listSI;
                #endregion
                btnSubmit.Text = "UPDATE";
            }
        }
        private void btnReset_Click(object sender, EventArgs e)
        {
            ResetStudentsMaster();
            dgvSubject.DataSource = null;
            ResetAdmission();
            GetStudentsMaster();
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(ConncetionHelper.Student))
            {
                con.Open();
                string q = "select * from StudentsMaster;";
                SqlCommand cmd = new SqlCommand(q, con);
                SqlDataAdapter adap = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adap.Fill(ds, "StudentsMaster");
                MasterReport cr1 = new MasterReport();
                cr1.SetDataSource(ds);
                crystalReportViewer1.ReportSource = cr1;
                con.Close();
                crystalReportViewer1.Refresh();
                con.Close();
            }
        }

        private void crystalReportViewer2_Load(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(ConncetionHelper.Student))
            {
                con.Open();
                string q = "select * from Admission;";
                SqlCommand cmd = new SqlCommand(q, con);
                SqlDataAdapter adap = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adap.Fill(ds, "Admission");
                DetailsReport cr2 = new DetailsReport();
                cr2.SetDataSource(ds);
                crystalReportViewer2.ReportSource = cr2;
                con.Close();
                crystalReportViewer1.Refresh();
                con.Close();
            }
        }
    }
}
