using Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace BusinessLayer.implemation
{
    public class OpptypeRepository : IOpptypeRepository
    {

        private readonly string _connectionString;

        public OpptypeRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<List<OpptypeDomainModel>> Get()
        {
            var opptypes = new List<OpptypeDomainModel>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("GetOpptype", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                await conn.OpenAsync();

                using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        opptypes.Add(new OpptypeDomainModel
                        {
                            OpptypeID = Convert.ToInt32(reader["OpptypeID"]),
                            OpptypeName = reader["OpptypeName"].ToString(),
                            CompanyTypeID = Convert.ToInt32(reader["CompanyTypeID"])
                        });
                    }
                }
            }

            return opptypes;
        }

        public async Task<OpptypeDomainModel> GetOpptypeByIdAsync(int opptypeId)
        {
            OpptypeDomainModel opptype = null;

            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("GetOptypeById", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@OpptypeID", opptypeId);

                await conn.OpenAsync();

                using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        opptype = new OpptypeDomainModel
                        {
                            OpptypeID = Convert.ToInt32(reader["OpptypeID"]),
                            OpptypeName = reader["OpptypeName"].ToString(),
                            CompanyTypeID = Convert.ToInt32(reader["CompanyTypeID"])
                        };
                    }
                }
            }

            return opptype;
        }

        public async Task<int> InsertOpptypeAsync(OpptypeDomainModel opptypeDomainModel)
        {
            int result = 0;

            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("InsertOptype", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@OpptypeName", opptypeDomainModel.OpptypeName);
                cmd.Parameters.AddWithValue("@CompanyTypeID", opptypeDomainModel.CompanyTypeID);

                await conn.OpenAsync();
                result = await cmd.ExecuteNonQueryAsync();
            }

            return result;
        }


        public async Task<bool> Update(OpptypeDomainModel opptypeDomainModel)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("UpdateOptype", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                // Add parameters
                cmd.Parameters.AddWithValue("@OpptypeID", opptypeDomainModel.OpptypeID);
                cmd.Parameters.AddWithValue("@OpptypeName", opptypeDomainModel.OpptypeName ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@CompanyTypeID", opptypeDomainModel.CompanyTypeID);

                // Open connection and execute
                await conn.OpenAsync();
                await cmd.ExecuteNonQueryAsync();

                return true;
            }
        }

    }
}
