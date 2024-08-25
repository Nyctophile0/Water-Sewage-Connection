using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;

namespace WaterSewageConnection.Models
{
	public class BindDropdown : ConClass
	{
		public string id { get; set; }
		public string value { get; set; }
		public string Action { get; set; }
		public string CircleId { get; set; }
		public string ZoneId { get; set; }
		public string DivisionId { get; set; }
		public string RoleTypeId { get; set; }
		public string WardId { get; set; }
		public string MohallaId { get; set; }



		public DataSet getDataSet()
		{
			string connectionString = ConClass.ConnectionString;
			using (SqlConnection con = new SqlConnection(connectionString))
			{
				SqlDataAdapter da = new SqlDataAdapter("SP_DropdownBinder", con);
				da.SelectCommand.CommandType = CommandType.StoredProcedure;


				da.SelectCommand.Parameters.AddWithValue("@Action", this.Action);
				da.SelectCommand.Parameters.AddWithValue("@RoleTypeId", this.RoleTypeId);
				da.SelectCommand.Parameters.AddWithValue("@ZoneId", this.ZoneId);
				da.SelectCommand.Parameters.AddWithValue("@Circleid", this.CircleId);
				da.SelectCommand.Parameters.AddWithValue("@DivisionId", this.DivisionId);
				da.SelectCommand.Parameters.AddWithValue("@WardId", this.WardId);
				da.SelectCommand.Parameters.AddWithValue("@MohallaId", this.MohallaId);



				DataSet ds = new DataSet();
				da.Fill(ds);
				return ds;
			}
		}


		public List<BindDropdown> getDataToBind()
		{
			DataSet ds = getDataSet();
			List<BindDropdown> ls = new List<BindDropdown>();
			if (ds.Tables.Count > 0)
			{
				if (ds.Tables[0].Rows.Count > 0)
				{
					foreach (DataRow dr in ds.Tables[0].Rows)
					{
						BindDropdown obj = new BindDropdown();
						obj.id = dr["id"].ToString();
						obj.value = dr["value"].ToString();
						ls.Add(obj);
					}
				}
			}
			return ls;
		}
	}
}
