using System;
using System.Data.OleDb;
using System.Windows.Forms;
using varManager.Properties;

namespace varManager
{
    static class Program
    {
        static void ensureOleDb()
        {
            try
            {
                var connectionString = Settings.Default.varManagerConnectionString;
                
                using (var connection = new OleDbConnection(connectionString))
                {
                    connection.Open(); // Check if connection can be opened here

                    // using (var command = new OleDbCommand(query, connection))
                    // {
                    //     var reader = command.ExecuteReader();
                    //     while (reader.Read())
                    //     {
                    //         // Process each row
                    //     }
                    //
                    //     reader.Close();
                    // }
                }
            }
            catch (OleDbException ex)
            {
                // Handle specific OleDb exceptions
                Console.WriteLine($"Database error: {ex.Message}");
            }
            catch (Exception ex)
            {
                // Handle general exceptions
                Console.WriteLine($"General error: {ex.Message}");
            }
        }

        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                if (!OperatingSystem.IsOSPlatformVersionAtLeast("Windows", 6, 1))
                {
                    Console.WriteLine("Only Windows Vista or newer is supported at this time");
                    Environment.Exit(1);
                }
                else
                {
                    // hack to make sure DB connection is properly initialized before use
                    // was causing intermittent crashing on startup
                    // var varManagerDataSet = new varManagerDataSet();
                    ensureOleDb();

                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(new Form1());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}