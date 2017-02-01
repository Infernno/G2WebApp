using LinqToDB;
using LinqToDB.DataProvider.SQLite;

namespace G2WebApp.Core.Util
{
    public static class Library
    {
        private const string connectionString = "Data Source=database.db";
        private static readonly SQLiteDataProvider provider = new SQLiteDataProvider();
        private static bool isResolved;

        public static DataContext GetContext()
        {
            
            if (!isResolved)
            {
                try
                {
                    // Call static constructor
                    SQLiteTools.GetDataProvider();

                    // If Mono's sqlite library cannot be resolved, switch to official SQLite wrapper instead
                    SQLiteTools.AssemblyName = "System.Data.SQLite";
                    SQLiteTools.ConnectionName = "SQLiteConnection";
                    SQLiteTools.DataReaderName = "SQLiteDataReader";
                }
                 catch
                {
                    // ignored
                }

                isResolved = true;
            }
           

            return new DataContext(provider, connectionString);
             
            //return new DataContext();
        }
    }
}
