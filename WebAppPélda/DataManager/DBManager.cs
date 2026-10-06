using MySql.Data.MySqlClient;                                                                   // A MySql.Data.MySqlClient névtér importálása, amely lehetővé teszi a MySQL adatbázis kezelését C# nyelven.
namespace WebAppPelda.DataManager                                                               // A WebAppPelda.DataManager névtér definiálása, amely tartalmazza a DBManager osztályt.
{
    public class DBManager                                                                      // A DBManager osztály definiálása, amely az adatbázis kapcsolat kezeléséért felelős.
    {
        string connectionString =                                                               // A connectionString változó definiálása, amely tartalmazza az adatbázis kapcsolat paramétereit.
            "Server=localhost;" +                                                               // A connectionString változó definiálása, amely tartalmazza az adatbázis kapcsolat paramétereit.
            "Database=WebbAppPeldaDB;" +                                                        // A connectionString változó definiálása, amely tartalmazza az adatbázis kapcsolat paramétereit.
            "User = root;" +                                                                    // A connectionString változó definiálása, amely tartalmazza az adatbázis kapcsolat paramétereit.
            "Password = ;";                                                                     // A connectionString változó definiálása, amely tartalmazza az adatbázis kapcsolat paramétereit.

        public MySqlConnection CreateConnection()                                               // A CreateConnection metódus definiálása, amely létrehozza az adatbázis kapcsolatot.
        {
            MySqlConnection connection = new MySqlConnection(connectionString);                 // A MySqlConnection objektum létrehozása a connectionString paraméterrel, amely lehetővé teszi az adatbázis kapcsolat létrehozását.
            return connection;                                                                  // A MySqlConnection objektum visszaadása a metódus végén.
        }
    }
}
