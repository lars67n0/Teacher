using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Teacher.Pages.Koordinator
{
    public class CreateModel : PageModel
    {
        public TeacherInfo teacherInfo = new TeacherInfo();
        public String errorMessage = "";
        public String SuccesMessage = "";
        public void OnGet()
        {
        }

        public void OnPost()
        {
            teacherInfo.FirstName = Request.Form["First Name"];
            teacherInfo.LastName = Request.Form["Last Name"];
            teacherInfo.Initialer = Request.Form["Initialer"];

            if (teacherInfo.FirstName.Length == 0 || teacherInfo.LastName.Length == 0 || teacherInfo.Initialer.Length == 0)
            {
                errorMessage = "All Fields Are Required";
                return;
            }

            // saving new Teacher

            try
            {
                String connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\rikar\\Documents\\TeacherDB.mdf;Integrated Security=True;Connect Timeout=30";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "INSERT INTO fag " + "(FirstName, LastName, Initialer) VALUES " + "(@FirstName, @LastName, @Initialer);";
              
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@FirstName", teacherInfo.FirstName);
                        command.Parameters.AddWithValue("@LastName", teacherInfo.LastName);
                        command.Parameters.AddWithValue("@Initialer", teacherInfo.Initialer);

                        command.ExecuteNonQuery();
                    }
                
                }

            
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }
            teacherInfo.FirstName = ""; teacherInfo.LastName = ""; teacherInfo.Initialer = "";
            SuccesMessage = "All New Teachers Added";

            Response.Redirect("/Koordinator/Koordinator");


        }
    }
}
