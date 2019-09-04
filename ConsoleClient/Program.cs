using DataObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using KellermanSoftware.CompareNetObjects;

namespace ConsoleClient
{
    class Program
    {
        static void CompareRows(DataTable table1, DataTable table2)
        {
            CompareLogic compareLogic = new CompareLogic();
            ComparisonResult comparisonResult = compareLogic.Compare(table1, table2);
            if (comparisonResult.Differences.Count > 0)
            {
                Console.WriteLine("Unmatched Rows - {0} exists in {1} ", comparisonResult.Differences.Count, table1.TableName);
                foreach(Difference difference in comparisonResult.Differences)
                {
                    Console.WriteLine(difference.Object1Value + " || " + difference.Object2Value);
                }
            }
        }
        static void Main(string[] args)
        {
            DataSet ds_1 = new DataSet();

            Dictionary<int, Object> AllTestCases = new Dictionary<int, object>();

            Dictionary<string, string> TestCase1 = new Dictionary<string, string>();
            TestCase1.Add("ID", "123");

            Dictionary<string, string> TestCase2 = new Dictionary<string, string>();
            TestCase2.Add("ID", "123");

            AllTestCases.Add(1, TestCase1);
            AllTestCases.Add(2, TestCase2);

            for (int item = 1;item <= AllTestCases.Count;item++)
            {
                Console.WriteLine("\n");
                Console.WriteLine("Testcase number " + (item));
                Console.WriteLine("\n");

                ds_1 = DbContextExtensions.ExecuteStoredProcedure("GetSampleData_1", (Dictionary<string,string>)AllTestCases[item]);

                DataSet ds_2 = new DataSet();

                ds_2 = DbContextExtensions.ExecuteStoredProcedure("GetSampleData_2", (Dictionary<string,string>)AllTestCases[item]);

                #region Check if any dataset null
                Console.WriteLine("*******Check if any dataset null*******");
                if (ds_1 == null || ds_2 == null)
                {
                    Console.WriteLine("One of the returned dataset is null");
                    return;
                }

                if (ds_1.Tables.Count == 0 || ds_2.Tables.Count == 0)
                {
                    Console.WriteLine("One of the returned dataset is null");
                    return;
                }

                #endregion

                #region Compare Tables Count between Datasets
                Console.WriteLine("*******Compare Tables Count between Datasets*******");
                if (ds_1.Tables.Count != ds_2.Tables.Count)
                {
                    Console.WriteLine("There is a mismatch in tables recieved between datasets , 1st DS Count = {0} : 2nd DS Count = {1} " + ds_1.Tables.Count, ds_2.Tables.Count);
                    return;
                }

                #endregion

                #region Compare Table Names between Datasets
                Console.WriteLine("*******Compare Table Names between Datasets*******");
                List<string> ds_1_TableNames = new List<string>();
                List<string> ds_2_TableNames = new List<string>();

                //Load list with ds_1 tables
                foreach (DataTable table in ds_1.Tables)
                {
                    ds_1_TableNames.Add(table.TableName);
                }

                //Load list with ds_2 tables
                foreach (DataTable table in ds_2.Tables)
                {
                    ds_2_TableNames.Add(table.TableName);
                }

                for (int i = 0; i < ds_1_TableNames.Count; i++)
                {
                    if (ds_1_TableNames[i] != ds_2_TableNames[i])
                    {
                        Console.WriteLine("There is a mismatch in table names b/w tables , 1st Table = {0} : 2nd Table = {1} " + ds_1_TableNames[i], ds_2_TableNames[i]);
                        return;
                    }
                }

                #endregion

                #region Compare each table Columns
                Console.WriteLine("*******Compare each table Columns*******");
                DataTable dt_1, dt_2;

                for (int i = 0; i < ds_1_TableNames.Count; i++)
                {
                    dt_1 = ds_1.Tables[i];
                    dt_2 = ds_2.Tables[i];
                    CompareRows(dt_1, dt_2);
                }

                #endregion

            }
            Console.ReadLine();

        }
    }
}
