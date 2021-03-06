﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using Model;

namespace CSVNameProcessor.ReadWriteHelpers
{
    public interface IReadExcel
    {
        string HeaderNames { get; set; }
        string FileName { get; set; }
        string GetFirstSheetName { get; set; }
        List<AddressBook> GetDataRows();
    }

    public class ReadExcel : IReadExcel
    {
        public string FileName { get; set; }
        public string GetFirstSheetName { get; set; }
        public string HeaderNames { get; set; }
        private string _connectionString;
        private DataTable _dataTable;
        private string _fileExt;

        public List<AddressBook> GetDataRows()
        {
            CreateConnection();
            PopulateSheetName();
            return ExtractRows();
        }
        private void PopulateSheetName()
        {
            using (OleDbConnection conn = new OleDbConnection(_connectionString))
            {

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                _dataTable = conn.GetSchema(OdbcMetaDataCollectionNames.Tables, null);
                if (_dataTable != null && _dataTable.Rows.Count > 0)
                {
                    GetFirstSheetName = _dataTable.Rows[0]["TABLE_NAME"].ToString().Replace("'", "");
                    if (String.Compare(_fileExt, "csv", StringComparison.OrdinalIgnoreCase) == 0)
                    {
                        GetFirstSheetName = "Address-Book.csv";
                    }
                    PopulateHeadersColumnNames();
                }
                conn.Close();
            }


        }
        private List<AddressBook> ExtractRows()
        {
            using (OleDbConnection conn = new OleDbConnection(_connectionString))
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                string selectCmd = string.Format("SELECT * From [" + GetFirstSheetName + "]");
                var cmd = new OleDbCommand(selectCmd, conn);
                var da = new OleDbDataAdapter(cmd) {SelectCommand = cmd};
                DataTable dt = new DataTable {TableName = GetFirstSheetName};
                da.Fill(dt);

                string[] headerNamesArray = HeaderNames.Split(',');
                return (from DataRow row in dt.Rows
                        select
                            new AddressBook
                            {
                                FullName = row[headerNamesArray[0]].ToString(),
                                Address = row[headerNamesArray[1]].ToString()
                            }).ToList();
            }
        }
        private void PopulateHeadersColumnNames()
        {
            string headNames = string.Empty;
            using (OleDbConnection conn = new OleDbConnection(_connectionString))
            {

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                string selectCmd = string.Format("select * from [{0}]", GetFirstSheetName + "A" + "1" + ":Z" + "1");
                if (String.Compare(_fileExt, "csv", StringComparison.OrdinalIgnoreCase) == 0)
                {
                    selectCmd = string.Format("select * from [{0}]", GetFirstSheetName);
                }

                var cmd = new OleDbCommand(selectCmd, conn);
                var da = new OleDbDataAdapter(cmd);

                var ds = new DataSet();
                da.Fill(ds);

                foreach (DataColumn column in ds.Tables[0].Columns)
                {
                    headNames += column.ColumnName + ",";
                }
                HeaderNames = headNames.Trim(',');

            }
        }
        private void CreateConnection()
        {
            _fileExt = GetFileExtension();
            if (String.Compare(_fileExt, "xls", StringComparison.OrdinalIgnoreCase) == 0)
            {
                _connectionString = string.Concat("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=", FileName, ";" + "Extended Properties='Excel 8.0;HDR=YES;'");
            }
            else if (String.Compare(_fileExt, "xlsx", StringComparison.OrdinalIgnoreCase) == 0)
            {
                _connectionString = string.Concat("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=", FileName, ";" + "Extended Properties='Excel 12.0 Xml;HDR=YES;'");
            }
            else if (String.Compare(_fileExt, "csv", StringComparison.OrdinalIgnoreCase) == 0)
            {
                string path = Path.GetDirectoryName(FileName);
                _connectionString = string.Concat("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=", path, ";Extended Properties=\"Text;Excel 12.0;HDR=No;IMEX=1;FMT=Delimited\"");
            }

        }
        private string GetFileExtension()
        {
            if (string.IsNullOrEmpty(FileName)) return string.Empty;
            string extension = string.Empty;
            char[] arr = FileName.ToCharArray();
            int index = 0;
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] == '.')
                {
                    index = i;
                }
            }
            for (int x = index + 1; x < arr.Length; x++)
            {
                extension = extension + arr[x];
            }
            return extension;
        }
    }
}
