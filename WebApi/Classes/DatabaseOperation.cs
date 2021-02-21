using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;

namespace WebApi.Classes
{
    public class DatabaseOperation
    {
        string strCon = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;

        public List<user> getUserDetails()
        {
            List<user> userList = new List<user>();
            SqlConnection con = new SqlConnection(strCon);
            try
            {
                string query = "Select id,name,convert(varchar, dob, 23) as dob,designation,skill from tbluser";
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                SqlDataReader myReader = cmd.ExecuteReader();
                while (myReader.Read())
                {
                    user u = new user();
                    u.id = Convert.ToInt32(myReader["id"]);
                    u.name = myReader["name"].ToString();
                    u.dob = myReader["dob"].ToString();
                    u.designation =myReader["designation"].ToString();
                    u.skills = Convert.ToInt32(myReader["skill"]);
                    userList.Add(u);
                }

                myReader.Close();
            }
            catch(SqlException ex)
            {
                con.Close();
            }
            finally
            {
                con.Close();
            }
            
            return userList;
        }

        public int saveUser(user u)
        {
            SqlConnection con = new SqlConnection(strCon);
            int result = 0;
            try
            {
                string query = "insert into tbluser (name,dob,designation,skill) values (@name,@dob,@designation,@skill)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("name", u.name);
                cmd.Parameters.AddWithValue("dob", u.dob);
                cmd.Parameters.AddWithValue("designation", u.designation);
                cmd.Parameters.AddWithValue("skill", u.skills);
                con.Open();
                result = cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (SqlException ex)
            {
                con.Close();
            }
            finally
            {
                con.Close();
            }

            return result;
        }


        public int editUser(user u)
        {
            string strcon = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            SqlConnection con = new SqlConnection(strcon);
            int result = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("update tbluser set name=@name,dob=@dob,designation=@designation,skill=@skill where id=@id", con);
                cmd.Parameters.AddWithValue("name", u.name);
                cmd.Parameters.AddWithValue("dob", u.dob);
                cmd.Parameters.AddWithValue("designation", u.designation);
                cmd.Parameters.AddWithValue("skill", u.skills);
                cmd.Parameters.AddWithValue("id", u.id);
                con.Open();
                result = cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (SqlException ex)
            {
                con.Close();
            }
            finally
            {
                con.Close();
            }
            return result;

        }

        public int deleteUser(int id)
        {
            string strcon = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            SqlConnection con = new SqlConnection(strcon);
            int result = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("delete from tbluser where id=@id", con);
                cmd.Parameters.AddWithValue("id", id);
                con.Open();
                result = cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (SqlException ex)
            {
                con.Close();
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                con.Close();
            }
            return result;

        }
    }
}