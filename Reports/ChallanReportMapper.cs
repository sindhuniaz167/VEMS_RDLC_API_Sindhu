using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Reflection;
using VEMS_RDLC_API.Models;

namespace VEMS_RDLC_API.Reports
{
    public static class ChallanReportMapper
    {
        public static DataTable ToDataTable(ChallanData data)
        {
            var table = new DataTable("GetChallanData");
            var properties = typeof(ChallanData)
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => p.CanRead && !p.GetCustomAttributes<NotMappedAttribute>().Any())
                .ToArray();

            foreach (var property in properties)
            {
                var columnType = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;
                table.Columns.Add(property.Name, columnType);
            }

            var row = table.NewRow();
            foreach (var property in properties)
            {
                var value = property.GetValue(data);
                row[property.Name] = value ?? DBNull.Value;
            }

            table.Rows.Add(row);
            return table;
        }
    }
}
