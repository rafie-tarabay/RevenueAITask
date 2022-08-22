using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Threading.Tasks;

public static class GeneralDBFunctions
{

    public static async Task<DataTable>  SQL2DataTable(this DbContext context,
           string sqlQuery, params DbParameter[] parameters)
    {
        DbConnection conn = context.Database.GetDbConnection();

        using (var cmd = conn.CreateCommand())
        {
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = sqlQuery;
            if (parameters != null)
            {
                foreach (var item in parameters)
                {
                    cmd.Parameters.Add(item);
                }
            }

            var table = new DataTable();
            if (conn.State.Equals(ConnectionState.Closed)) { conn.Open(); }
            using (var reader = await cmd.ExecuteReaderAsync())
            {
                table.Load(reader);  
            }
            if (conn.State.Equals(ConnectionState.Open)) { conn.Close(); }
            return table;
        }

    }



    //List<Student> studentDetails = new List<Student>();
    //studentDetails = ConvertDataTable<Student>(dt);
    public static List<T> DataTable2DBSet<T>(DataTable dt)
    {
        List<T> data = new List<T>();
        foreach (DataRow row in dt.Rows)
        {
            T item = GetItem<T>(row);
            data.Add(item);
        }
        return data;
    }
    private static T GetItem<T>(DataRow dr)
    {
        Type temp = typeof(T);
        T obj = Activator.CreateInstance<T>();

        foreach (DataColumn column in dr.Table.Columns)
        {
            foreach (PropertyInfo pro in temp.GetProperties())
            {
                if (pro.Name == column.ColumnName)
                    pro.SetValue(obj, dr[column.ColumnName], null);
                else
                    continue;
            }
        }
        return obj;
    }

    //List<UserAttendace> mylist = _context.SQL2DBSet<UserAttendace>("sql", null);
    public static async Task<List<T>> SQL2DBSet<T>(this DbContext context,
                                       string sqlQuery, params DbParameter[] parameters)
    {
        return DataTable2DBSet<T>(await SQL2DataTable(context, sqlQuery, parameters));
    }


}