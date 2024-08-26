using Microsoft.Data.SqlClient;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace WaterSewageConnection.Models
{
    public class Users : ConClass
    {
        public int userId { get; set; }
		[Required(ErrorMessage = "*")]
		public string userName { get; set; }
		[Required(ErrorMessage = "*")]
		public string password { get; set; }
        public string Action { get; set; }
        public string userType { get; set; }
        public int userTypeId { get; set; }
        public string Role { get; set; }

        public async Task<DataSet> getDataSetAsync()
        {
            string connectionString = ConClass.ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlDataAdapter da = new SqlDataAdapter("SP_Users", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;

                da.SelectCommand.Parameters.AddWithValue("@UserId", this.userId);

                da.SelectCommand.Parameters.AddWithValue("@UserName", this.userName);
                da.SelectCommand.Parameters.AddWithValue("@Password", this.password);
                da.SelectCommand.Parameters.AddWithValue("@UserTypeId", this.userTypeId);

                da.SelectCommand.Parameters.AddWithValue("@Action", this.Action);


                DataSet ds = new DataSet();

				// Since SqlDataAdapter.Fill is synchronous, wrapped it in Task.Run to make it asynchronous.
				await Task.Run(() => da.Fill(ds));
                //da.Fill(ds);
                return ds;
            }
        }

        //public void save(out string message)
        //{
        //    string connectionString = conClass.connectString;

        //    using (SqlConnection con = new SqlConnection(connectionString))
        //    {
        //        SqlCommand cmd = new SqlCommand("workMasterSP_crud", con);
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.Parameters.AddWithValue("@zoneId", this.zoneId);
        //        cmd.Parameters.AddWithValue("@projectId", this.projectId);
        //        cmd.Parameters.Add("@msg", System.Data.SqlDbType.NVarChar, 250);
        //        cmd.Parameters["@msg"].Direction = ParameterDirection.Output;

        //        con.Open();
        //        cmd.ExecuteNonQuery();

        //        message = cmd.Parameters["@msg"].Value.ToString();

        //    }
        //}

        public async Task<bool> getObjectAsync()
        {

            DataSet ds = new DataSet();
            ds = await getDataSetAsync();
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataRow dr = ds.Tables[0].Rows[0];
                    //this.userId = Convert.ToInt32(dr["userId"].ToString());
                    this.userName = dr["userName"].ToString();
                    //this.password = dr["password"].ToString();
                    this.userType = dr["userType"].ToString();


                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        
    }
}
