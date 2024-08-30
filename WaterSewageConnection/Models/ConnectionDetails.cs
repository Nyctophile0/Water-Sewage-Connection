﻿using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace WaterSewageConnection.Models
{
	public class ConnectionDetails : ConClass
	{
		public string ConnectionId { get; set; }
		[Required(ErrorMessage = "*")]
		public string ConnectionName { get; set; }
		[Required(ErrorMessage = "*")]
		public string ConnectionType { get; set; }
		[Required(ErrorMessage = "*")]
		public string ApplicantName { get; set; }
		[Required(ErrorMessage = "*")]
		public string ApplicantAddress { get; set; }
		[Required(ErrorMessage = "*")]
		public string FatherSpouseName { get; set; }
		[Required(ErrorMessage = "*")]
		public string ZoneId { get; set; }
		[Required(ErrorMessage = "*")]
		public string ZoneName { get; set; }
		[Required(ErrorMessage = "*")]
		public string WardId { get; set; }
		public string WardName { get; set; }
		[Required(ErrorMessage = "*")]
		public string MohallaId { get; set; }
		public string MohallaName { get; set; }
		[Required(ErrorMessage = "*")]
		public string LandLord { get; set; }
		[Required(ErrorMessage = "*")]
		public string BuildingOwnerName { get; set; }
		[Required(ErrorMessage = "*")]
		public string PlumberName { get; set; }
		[Required(ErrorMessage = "*")]
		public string PlumberMobile { get; set; }
		[Required(ErrorMessage = "*")]
		public IFormFile HouseMap { get; set; }
		public string HouseMapPath { get; set; }
		[Required(ErrorMessage = "*")]
		public IFormFile ElectricityBill { get; set; }
		public string ElectricityBillPath { get; set; }
		[Required(ErrorMessage = "*")]
		public IFormFile HouseTax { get; set; }
		public string HouseTaxPath { get; set; }
		[Required(ErrorMessage = "*")]
		public IFormFile Registry { get; set; }
		public string RegistryPath { get; set; }
		public string filepath { get; set; }
		public string filetype { get; set; }
		public string guid { get; set; }
		[Required(ErrorMessage = "*")]
		public string PreviousConnection1 { get; set; }
		[Required(ErrorMessage = "*")]
		public string PreviousConnection2 { get; set; }
		[Required(ErrorMessage = "*")]
		public int NoofConnections { get; set; }
		public string Action { get; set; }




		public async Task<DataSet> getDataSetAsync()
		{
			string connectionString = ConClass.ConnectionString;
			using (SqlConnection con = new SqlConnection(connectionString))
			{
				SqlDataAdapter da = new SqlDataAdapter("SP_Connections", con);
				da.SelectCommand.CommandType = CommandType.StoredProcedure;

				da.SelectCommand.Parameters.AddWithValue("@ConnectionId", this.ConnectionId);
				da.SelectCommand.Parameters.AddWithValue("@ConnectionName", this.ConnectionName);
				da.SelectCommand.Parameters.AddWithValue("@ConnectionType", this.ConnectionType);
				da.SelectCommand.Parameters.AddWithValue("@ApplicantName", this.ApplicantName);
				da.SelectCommand.Parameters.AddWithValue("@ZoneId", this.ZoneId);
				da.SelectCommand.Parameters.AddWithValue("@WardId", this.WardId);
				da.SelectCommand.Parameters.AddWithValue("@MohallaId", this.MohallaId);
				da.SelectCommand.Parameters.AddWithValue("@Action", this.Action);
				da.SelectCommand.Parameters.AddWithValue("@guid", this.guid);

				DataSet ds = new DataSet();
				await Task.Run(() => da.Fill(ds));
				return ds;
			}
		}

		//public void save(out string message)
		//{
		//	string connectionString = ConClass.ConnectionString;
		//	using (SqlConnection con = new SqlConnection(connectionString))
		//	{
		//		SqlCommand cmd = new SqlCommand("SP_Connections", con);
		//		cmd.CommandType = CommandType.StoredProcedure;
		//		cmd.Parameters.AddWithValue("@ConnectionName", this.ConnectionName);
		//		cmd.Parameters.AddWithValue("@ConnectionType", this.ConnectionType);
		//		cmd.Parameters.AddWithValue("@FatherSpouseName", this.FatherSpouseName);
		//		cmd.Parameters.AddWithValue("@ApplicantName", this.ApplicantName);
		//		cmd.Parameters.AddWithValue("@ApplicantAddress", this.ApplicantAddress);
		//		cmd.Parameters.AddWithValue("@ZoneId", this.ZoneId);
		//		cmd.Parameters.AddWithValue("@WardId", this.WardId);
		//		cmd.Parameters.AddWithValue("@MohallaId", this.MohallaId);
		//		cmd.Parameters.AddWithValue("@LandLord", this.LandLord);
		//		cmd.Parameters.AddWithValue("@BuildingOwnerName", this.BuildingOwnerName);
		//		cmd.Parameters.AddWithValue("@PlumberName", this.PlumberName);
		//		cmd.Parameters.AddWithValue("@PlumberMobile", this.PlumberMobile);
		//		cmd.Parameters.AddWithValue("@HouseMap", this.HouseMapPath);
		//		cmd.Parameters.AddWithValue("@ElectricityBill", this.ElectricityBillPath);
		//		cmd.Parameters.AddWithValue("@HouseTax", this.HouseTaxPath);
		//		cmd.Parameters.AddWithValue("@Registry", this.RegistryPath);
		//		cmd.Parameters.AddWithValue("@guid", this.guid);

		//		cmd.Parameters.AddWithValue("@Action", this.Action);
		//		cmd.Parameters.Add("@msg", System.Data.SqlDbType.NVarChar, 250);
		//		cmd.Parameters["@msg"].Direction = ParameterDirection.Output;

		//		con.Open();
		//		cmd.ExecuteNonQuery();

		//		message = cmd.Parameters["@msg"].Value.ToString();
		//	}
		//}

		public async Task<string> saveAsync()
		{
			string message = "";
			string connectionString = ConClass.ConnectionString;
			using (SqlConnection con = new SqlConnection(connectionString))
			{
				SqlCommand cmd = new SqlCommand("SP_Connections", con);
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@ConnectionName", this.ConnectionName);
				cmd.Parameters.AddWithValue("@ConnectionType", this.ConnectionType);
				cmd.Parameters.AddWithValue("@FatherSpouseName", this.FatherSpouseName);
				cmd.Parameters.AddWithValue("@ApplicantName", this.ApplicantName);
				cmd.Parameters.AddWithValue("@ApplicantAddress", this.ApplicantAddress);
				cmd.Parameters.AddWithValue("@ZoneId", this.ZoneId);
				cmd.Parameters.AddWithValue("@WardId", this.WardId);
				cmd.Parameters.AddWithValue("@MohallaId", this.MohallaId);
				cmd.Parameters.AddWithValue("@LandLord", this.LandLord);
				cmd.Parameters.AddWithValue("@BuildingOwnerName", this.BuildingOwnerName);
				cmd.Parameters.AddWithValue("@PlumberName", this.PlumberName);
				cmd.Parameters.AddWithValue("@PlumberMobile", this.PlumberMobile);
				cmd.Parameters.AddWithValue("@HouseMap", this.HouseMapPath);
				cmd.Parameters.AddWithValue("@ElectricityBill", this.ElectricityBillPath);
				cmd.Parameters.AddWithValue("@HouseTax", this.HouseTaxPath);
				cmd.Parameters.AddWithValue("@Registry", this.RegistryPath);
				cmd.Parameters.AddWithValue("@guid", this.guid);

				cmd.Parameters.AddWithValue("@Action", this.Action);
				cmd.Parameters.Add("@msg", System.Data.SqlDbType.NVarChar, 250);
				cmd.Parameters["@msg"].Direction = ParameterDirection.Output;

				con.Open();
				await Task.Run(()=>cmd.ExecuteNonQuery());

				message = cmd.Parameters["@msg"].Value.ToString();

				return message;
			}
		}

		public async Task<bool> getObjectAsync()
		{

			DataSet ds = new DataSet();
			ds = await getDataSetAsync();
			if (ds.Tables.Count > 0)
			{
				if (ds.Tables[0].Rows.Count > 0)
				{
					DataRow dr = ds.Tables[0].Rows[0];
					this.ConnectionName = dr["ConnectionName"].ToString();
					this.ConnectionType = dr["ConnectionType"].ToString();
					this.ApplicantName = dr["ApplicantName"].ToString();
					this.ApplicantAddress = dr["ApplicantAddress"].ToString();
					this.FatherSpouseName = dr["FatherSpouseName"].ToString();
					this.LandLord = dr["LandLord"].ToString();
					this.BuildingOwnerName = dr["BuildingOwnerName"].ToString();
					this.ZoneName = dr["ZoneName"].ToString();
					this.WardName = dr["WardName"].ToString();
					this.MohallaName = dr["MohallaName"].ToString();
					this.PlumberName = dr["PlumberName"].ToString();
					this.PlumberMobile = dr["PlumberMobile"].ToString();
					this.HouseMapPath = dr["HouseMap"].ToString();
					this.ElectricityBillPath = dr["ElectricityBill"].ToString();
					this.HouseTaxPath = dr["HouseTax"].ToString();
					this.RegistryPath = dr["Registry"].ToString();


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
