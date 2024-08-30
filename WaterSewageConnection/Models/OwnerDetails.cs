using Microsoft.Data.SqlClient;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Net;
using System.Numerics;

namespace WaterSewageConnection.Models
{
	public class OwnerDetails : ConClass
	{
		public string OwnerUser_Number { get; set; }
		[Required(ErrorMessage ="*")]
		public string OwnerName { get; set; }
		[Required(ErrorMessage = "*")]
		public string OwnerFatherHusbandName { get; set; }
		public string Email { get; set; }
		[Required(ErrorMessage = "*")]
		public string MobileNo { get; set; }
		[Required(ErrorMessage = "*")]
		public string Address { get; set; }
		[Required(ErrorMessage = "*")]
		public int PinCode { get; set; }
		public string IP_Address { get; set; }
		public string Action { get; set; }
		[Required(ErrorMessage = "*")]
		public string WardId { get; set; }
		public string WardName { get; set; }
		[Required(ErrorMessage = "*")]
		public string MohallaId { get; set; }
		public string MohallaName { get; set; }
		[Required(ErrorMessage ="*")]
		public string ZoneId { get; set; }
		public string ZoneName { get; set; }
		[Required(ErrorMessage = "*")]
		public string Password { get; set; }
		[Required(ErrorMessage = "*")]
		public string ConfirmPassword { get; set; }

		public DataSet getDataSet()
		{
			string connectionString = ConClass.ConnectionString;
			using (SqlConnection con = new SqlConnection(connectionString))
			{
				SqlDataAdapter da = new SqlDataAdapter("SP_OwnerDetails", con);
				da.SelectCommand.CommandType = CommandType.StoredProcedure;

				da.SelectCommand.Parameters.AddWithValue("@OwnerUser_Number", this.OwnerUser_Number);
				da.SelectCommand.Parameters.AddWithValue("@OwnerName", this.OwnerName);
				da.SelectCommand.Parameters.AddWithValue("@Action", this.Action);
				da.SelectCommand.Parameters.AddWithValue("@Address", this.Address);
				da.SelectCommand.Parameters.AddWithValue("@WardId", this.WardId);
				da.SelectCommand.Parameters.AddWithValue("@MohallaId", this.MohallaId);


				DataSet ds = new DataSet();
				da.Fill(ds);
				return ds;
			}
		}

		public void save(out string message)
		{
			string connectionString = ConClass.ConnectionString;
			using (SqlConnection con = new SqlConnection(connectionString))
			{
				SqlCommand cmd = new SqlCommand("SP_OwnerDetails", con);
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@OwnerName", this.OwnerName);
				cmd.Parameters.AddWithValue("@OwnerFatherHusbandName", this.OwnerFatherHusbandName);
				cmd.Parameters.AddWithValue("@MobileNo", this.MobileNo);
				cmd.Parameters.AddWithValue("@Address", this.Address);
				cmd.Parameters.AddWithValue("@ZoneId", this.ZoneId);
				cmd.Parameters.AddWithValue("@WardId", this.WardId);
				cmd.Parameters.AddWithValue("@MohallaId", this.MohallaId);
				cmd.Parameters.AddWithValue("@Email", this.Email);
				cmd.Parameters.AddWithValue("@Password", this.Password);
				cmd.Parameters.AddWithValue("@ConfirmPassword", this.ConfirmPassword);

				cmd.Parameters.AddWithValue("@Action", this.Action);
				cmd.Parameters.Add("@msg", System.Data.SqlDbType.NVarChar, 250);
				cmd.Parameters["@msg"].Direction = ParameterDirection.Output;

				con.Open();
				cmd.ExecuteNonQuery();

				message = cmd.Parameters["@msg"].Value.ToString();
			}
		}


		public bool getObject()
		{

			DataSet ds = new DataSet();
			ds = getDataSet();
			if (ds.Tables.Count > 0)
			{
				if (ds.Tables[0].Rows.Count > 0)
				{
					DataRow dr = ds.Tables[0].Rows[0];
					this.OwnerUser_Number = dr["OwnerUser_Number"].ToString();
					this.OwnerName = dr["OwnerName"].ToString();
					this.OwnerFatherHusbandName = dr["OwnerFatherHusbandName"].ToString();
					this.MobileNo = dr["MobileNo"].ToString();
					this.Email = dr["Email"].ToString();
					this.Address = dr["Address"].ToString();
					this.ZoneName = dr["ZoneName"].ToString();
					this.WardName = dr["WardName"].ToString();
					this.MohallaName = dr["MohallaName"].ToString();


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
