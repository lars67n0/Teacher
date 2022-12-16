using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Teacher.Pages.Koordinator
{
    public class CreateCourseModel : PageModel
    {
        public CourseInfo courseInfo = new CourseInfo();
        public String errorMessage = "";
        public String SuccesMessage = "";
        public void OnGet()
        {
        }

        public void OnPost()
        {
            courseInfo.CourseName = Request.Form["CourseName"];
            
            courseInfo.CourseTeacher = Request.Form["CourseTeacher"];

            //if (courseInfo.CourseName.Length == 0 || courseInfo.CourseTeacher.Length == 0)
            //{
            //    errorMessage = "All Fields Are Required";
            //    return;
            //}

            // saving new Teacher

            try
            {
                String connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\rikar\\Documents\\TeacherDB.mdf;Integrated Security=True;Connect Timeout=30";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "INSERT INTO Courses " + "(CourseName, CourseTeacher) VALUES " + "(@CourseName, @CourseTeacher);";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@CourseName", courseInfo.CourseName);
                        command.Parameters.AddWithValue("@CourseTeacher", courseInfo.CourseTeacher);
                       

                        command.ExecuteNonQuery();
                    }

                }


            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }
            courseInfo.CourseName = ""; courseInfo.CourseTeacher = "";
            SuccesMessage = "All New Courses Added Added";

            Response.Redirect("/Koordinator/Koordinator");


        }
    }
}

