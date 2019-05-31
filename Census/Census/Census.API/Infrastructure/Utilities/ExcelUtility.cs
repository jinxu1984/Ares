using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.IO;
using OfficeOpenXml;
using System.Text;
using EPPlus.Core.Extensions;

namespace Census.API.Infrastructure.Utilities
{
    public class ExcelUtility
    {
        public static List<TEntity> ReadFromWorksheet<TEntity>(string filePath, string workSheetName) 
            where TEntity : class, new()
        {
            var file = new FileInfo(filePath);
            using (var package = new ExcelPackage(file))
            {
                var results = package.GetWorksheet(workSheetName).ToList<TEntity>();
                return results;
            }
        }
    }
}
