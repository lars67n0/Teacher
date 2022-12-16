using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Teacher.Pages.Koordinator
{
    public class CreateSemesterModel : PageModel
    {
        public SemesterInfo semesterInfo = new SemesterInfo();
        public String errorMessage = "";
        public String SuccesMessage = "";
        public void OnGet()
        {
        }

        public void OnPost()
        {
            semesterInfo.Name= Request.Form["SemesterName"];

            semesterInfo.SemesterNr = Request.Form["SemesterNr"];

            if (semesterInfo.Name.Length == 0 || semesterInfo.SemesterNr.Length == 0)
            {
                errorMessage = "All Fields Are Required";
                return;
            }

            // saving new Semester

            try
            {
                String connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\rikar\\Documents\\TeacherDB.mdf;Integrated Security=True;Connect Timeout=30";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "INSERT INTO Semester " + "(Name, SemesterNr) VALUES " + "(@SemesterName, @SemesterNr);";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@CourseName", semesterInfo.Name);
                        command.Parameters.AddWithValue("@CourseTeacher", semesterInfo.SemesterNr);


                        command.ExecuteNonQuery();
                    }

                }


            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }
           semesterInfo.Name = ""; semesterInfo.SemesterNr = "";
            SuccesMessage = "All New Semester Added Added";

            Response.Redirect("/Koordinator/Koordinator");


        }

    }
}
