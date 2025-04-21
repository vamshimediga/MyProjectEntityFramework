using Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.implemation
{
    public class CompanyTypeRepository : ICompanyTypeRepository
    {
        private readonly string _connectionString;

        public CompanyTypeRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<IEnumerable<CompanyTypeDomainModel>> GetAllAsync()
        {
            List<CompanyTypeDomainModel> companyTypes = new List<CompanyTypeDomainModel>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var command = new SqlCommand("GetAllCompanyTypes", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var companyType = new CompanyTypeDomainModel
                            {
                                CompanyTypeID = reader.GetInt32(reader.GetOrdinal("CompanyTypeID")),
                                CompanyTypeName = reader.GetString(reader.GetOrdinal("CompanyTypeName"))
                            };
                            companyTypes.Add(companyType);
                        }
                    }
                }
            }

            return companyTypes;
        }

        public async Task<CompanyTypeDomainModel> GetByIdAsync(int id)
        {
            CompanyTypeDomainModel companyType = null;

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var command = new SqlCommand("GetCompanyTypeById", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@CompanyTypeID", SqlDbType.Int) { Value = id });

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            companyType = new CompanyTypeDomainModel()
                            {
                                CompanyTypeID = reader.GetInt32(reader.GetOrdinal("CompanyTypeID")),
                                CompanyTypeName = reader.GetString(reader.GetOrdinal("CompanyTypeName"))
                            };
                        }
                    }
                }
            }

            return companyType;
        }

        public async Task<int> InsertAsync(CompanyTypeDomainModel companyType)
        {
            int rowsAffected = 0;

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var command = new SqlCommand("InsertCompanyType", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@CompanyTypeName", SqlDbType.NVarChar) { Value = companyType.CompanyTypeName });

                    rowsAffected = await command.ExecuteNonQueryAsync();
                }
            }

            return rowsAffected;
        }

        public async Task<int> UpdateAsync(CompanyTypeDomainModel companyType)
        {
            int rowsAffected = 0;

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var command = new SqlCommand("UpdateCompanyType", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@CompanyTypeID", SqlDbType.Int) { Value = companyType.CompanyTypeID });
                    command.Parameters.Add(new SqlParameter("@CompanyTypeName", SqlDbType.NVarChar) { Value = companyType.CompanyTypeName });

                    rowsAffected = await command.ExecuteNonQueryAsync();
                }
            }

            return rowsAffected;
        }

        public async Task<int> DeleteAsync(int id)
        {
            int rowsAffected = 0;

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var command = new SqlCommand("DeleteCompanyType", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@CompanyTypeID", SqlDbType.Int) { Value = id });

                    rowsAffected = await command.ExecuteNonQueryAsync();
                }
            }

            return rowsAffected;
        }
    }
}
