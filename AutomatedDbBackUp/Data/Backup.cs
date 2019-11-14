using AutomatedDbBackUp.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace AutomatedDbBackUp.Data
{
    public class Backup
    {
        private Settings _settings;

        public Backup(Settings settings)
        {
            _settings = settings;
        }
        public void CreateBackUp()
        {
            using (SqlConnection connection = new SqlConnection(_settings.MasterConnString))
            {
                using (SqlCommand cmd = new SqlCommand("[dbo].[BackUpAppDb]", connection))
                {
                    connection.Open();

                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
