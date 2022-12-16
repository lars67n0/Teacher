using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Teacher.Pages.Koordinator
{
    public class UnderviserModel : PageModel
    {
        public List<TeacherInfo> listTeachers = new List<TeacherInfo>();

        public List<CourseInfo> listCourses = new List<CourseInfo>();

        public List<SemesterInfo> listsemesters = new List<SemesterInfo>();

        public void OnGet()
        {

            // List Teachers
            try
            {
                string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\rikar\\Documents\\TeacherDB.mdf;Integrated Security=True;Connect Timeout=30";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT * FROM fag";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read()) 
                            {
                                TeacherInfo teacher = new TeacherInfo();
                                teacher.id = reader.GetInt32(0);
                                teacher.FirstName = reader.GetString(1);
                                teacher.LastName = reader.GetString(2);
                                teacher.Initialer= reader.GetString(3);

                                listTeachers.Add(teacher);
                            }
                        }
                    }
                }
            }
            catch (Exception ex) { }

            // List Courses
            try
            {
                string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\rikar\\Documents\\TeacherDB.mdf;Integrated Security=True;Connect Timeout=30";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT * FROM Courses";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                CourseInfo courses = new CourseInfo();
                                courses.id = reader.GetInt32(0);
                                courses.CourseName = reader.GetString(1);
                             
                                courses.CourseTeacher = reader.GetString(2);

                                listCourses.Add(courses);
                             

                                
                            }
                        }
                    }
                }
            }
            catch (Exception ex) { }

            //List OF Semester
            try
            {
                string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\rikar\\Documents\\TeacherDB.mdf;Integrated Security=True;Connect Timeout=30";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT * FROM Semester";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                SemesterInfo semesters = new SemesterInfo();
                                semesters.id= reader.GetInt32(0);
                                semesters.Name= reader.GetString(1);
                                semesters.SemesterNr = reader.GetString(2);

                                listsemesters.Add(semesters);



                            }
                        }
                    }
                }
            }
            catch (Exception ex) { }

        }



    }


    //Classes For DB
    public class SemesterInfo
    {
        public int id;
        public string Name;
        public string SemesterNr;
    }

    public class CourseInfo
    {
        public int id;
        public string CourseName;
        
        public string CourseTeacher;
    }
    
    public class TeacherInfo
    {
        public int id;
        public string FirstName;
        public string LastName;
        public string Initialer;
    }
}
