using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using VEMS_RDLC_API.Models;

namespace VEMS_RDLC_API.Repositories
{
    public class ChallanRepository : IChallanRepository
    {
        private readonly string _connectionString;

        public ChallanRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Connection string 'DefaultConnection' is not configured.");
        }

        public async Task<ChallanData?> GetChallanReportDataAsync(string challanNo)
        {
            if (string.IsNullOrWhiteSpace(challanNo))
                return null;

            await using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            await using var command = new SqlCommand("GetChallanData", connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            command.Parameters.Add(new SqlParameter("@ChallanNo", SqlDbType.NVarChar, 30) { Value = challanNo });

            await using var reader = await command.ExecuteReaderAsync(CommandBehavior.SingleRow);
            return reader.Read() ? MapChallanData(reader) : null;
        }

        private static ChallanData MapChallanData(SqlDataReader reader)
        {
            var data = new ChallanData();
            var properties = typeof(ChallanData)
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => p.CanWrite && !p.GetCustomAttributes<NotMappedAttribute>().Any());

            for (var i = 0; i < reader.FieldCount; i++)
            {
                var columnName = reader.GetName(i);
                var property = properties.FirstOrDefault(p =>
                    string.Equals(p.Name, columnName, StringComparison.OrdinalIgnoreCase));

                if (property == null || reader.IsDBNull(i))
                    continue;

                property.SetValue(data, ConvertValue(reader.GetValue(i), property.PropertyType));
            }

            return data;
        }

        private static object? ConvertValue(object value, Type propertyType)
        {
            var targetType = Nullable.GetUnderlyingType(propertyType) ?? propertyType;

            if (targetType == typeof(short))
                return Convert.ToInt16(value);

            if (targetType == typeof(int))
                return Convert.ToInt32(value);

            if (targetType == typeof(decimal))
                return Convert.ToDecimal(value);

            if (targetType == typeof(bool))
                return Convert.ToBoolean(value);

            if (targetType == typeof(DateTime))
                return Convert.ToDateTime(value);

            if (targetType == typeof(string))
                return Convert.ToString(value);

            return Convert.ChangeType(value, targetType);
        }
    }
}
