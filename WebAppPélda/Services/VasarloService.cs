using MySql.Data.MySqlClient;                                                                                                                                   // Beimportálom a MySQL adatbázis kezeléséhez szükséges osztályokat                                                          
using WebAppPelda.Models;                                                                                                                                       // Beimportálom a Vasarlo modellt, amely a vásárló adatait reprezentálja
using WebAppPelda.DataManager;                                                                                                                                  // Beimportálom a DBManager osztályt, amely az adatbázis kapcsolatot kezeli
using System.Security.Cryptography.Xml;                                                                    

namespace WebAppPelda.Service                                                                                                                                   // Namespace deklarációja, amely a VasarloService osztályt tartalmazza
{
    public class VasarloService                                                                                                                                 // VasarloService osztály deklarációja, amely a vásárlókkal kapcsolatos adatbázis műveleteket kezeli
    {
        public List<Vasarlo> GetAllVasarlo()                                                                                                                    // GetAllVasarlo metódus deklarációja, amely visszaadja az összes vásárlót az adatbázisból
        {
            using (MySqlConnection conn = new DBManager().CreateConnection())                                                                                   // Létrehozok egy új MySQL kapcsolatot a DBManager osztály segítségével
            {
                try                                                                                                                                             // Próbálkozás az adatbázis műveletek végrehajtására
                {
                    conn.Open();                                                                                                                                // Megnyitom az adatbázis kapcsolatot
                    List<Vasarlo> result = new List<Vasarlo>();                                                                                                 // Létrehozok egy új listát, amely a vásárlókat fogja tartalmazni
                    string sql = "SELECT * FROM vasarlo;";                                                                                                      // SQL lekérdezés, amely az összes vásárlót lekéri az adatbázisból
                    MySqlCommand comm = new MySqlCommand(sql, conn);                                                                                            // Létrehozok egy új MySQL parancsot a lekérdezés és a kapcsolat megadásával
                    MySqlDataReader reader = comm.ExecuteReader();                                                                                              // Végrehajtom a lekérdezést és létrehozok egy MySQL adatolvasót az eredmények olvasásához
                    while (reader.Read())                                                                                                                       // Amíg van olvasható sor az eredmények között, addig végrehajtom a következő kódot
                    {                                                                                                                                           // Létrehozok egy új Vasarlo objektumot és feltöltöm az adatbázisból olvasott adatokkal
                        Vasarlo ujVasarlo = new Vasarlo();                                                                                                      // Létrehozok egy új Vasarlo objektumot
                        ujVasarlo.Id = reader.GetInt32("id");                                                                                                   // Beállítom az Id tulajdonságot az adatbázisból olvasott értékre
                        ujVasarlo.Nev = reader.GetString("Nev");                                                                                                // Beállítom a Nev tulajdonságot az adatbázisból olvasott értékre
                        ujVasarlo.Cim = reader.GetString("Cim");                                                                                                // Beállítom a Cim tulajdonságot az adatbázisból olvasott értékre
                        ujVasarlo.Email = reader.GetString("Email");                                                                                            // Beállítom az Email tulajdonságot az adatbázisból olvasott értékre
                        ujVasarlo.Telefon = reader.GetString("Telefon");                                                                                        // Beállítom a Telefon tulajdonságot az adatbázisból olvasott értékre
                        ujVasarlo.Pontszam = reader.GetInt32("Pontszam");                                                                                       // Beállítom a Pontszam tulajdonságot az adatbázisból olvasott értékre
                        result.Add(ujVasarlo);                                                                                                                  // Hozzáadom az új Vasarlo objektumot a result listához
                    }                                                                                                                                           // Amikor már nincs több olvasható sor, kilépek a while ciklusból
                    conn.Close();                                                                                                                               // Bezárom az adatbázis kapcsolatot
                    return result;                                                                                                                              // Visszaadom az összes vásárlót tartalmazó listát
                }                                                                                                                                               // Ha hiba történik az adatbázis műveletek során, elkapom a kivételt
                catch (Exception ex)                                                                                                                            // Elkapom az esetleges kivételeket, és kiírom a hibaüzenetet a konzolra
                {                                                                                                                                               // Kiírom a hibaüzenetet a konzolra
                    Console.WriteLine(ex.Message);                                                                                                              // Visszaadok egy listát, amely tartalmazza a hibaüzenetet egy új Vasarlo objektumban
                    return new List<Vasarlo>()                                                                                                                  // Visszaadok egy listát, amely tartalmazza a hibaüzenetet egy új Vasarlo objektumban
                    { 
                        new Vasarlo() { Nev = ex.Message }                                                                                                      // Létrehozok egy új Vasarlo objektumot, amelynek a Nev tulajdonságát a hibaüzenetre állítom
                    };                                                                                                                                          // Visszaadok egy listát, amely tartalmazza a hibaüzenetet egy új Vasarlo objektumban
                }                                                                                                                                               // Visszaadok egy listát, amely tartalmazza a hibaüzenetet egy új Vasarlo objektumban
            }                                                                                                                                                   // Visszaadok egy listát, amely tartalmazza a hibaüzenetet egy új Vasarlo objektumban
        }                                                                                                                                                       // Visszaadok egy listát, amely tartalmazza a hibaüzenetet egy új Vasarlo objektumban

        public Vasarlo GetById(int id)                                                                                                                          // GetById metódus deklarációja, amely visszaadja a vásárlót az adatbázisból az adott id alapján
        {
            try                                                                                                                                                 // Próbálkozás az adatbázis műveletek végrehajtására
            {
                Vasarlo result = new Vasarlo();                                                                                                                 // Létrehozok egy új Vasarlo objektumot, amelybe az adatbázisból olvasott adatokat fogom menteni
                using (MySqlConnection conn = new DBManager().CreateConnection())                                                                               // Létrehozok egy új MySQL kapcsolatot a DBManager osztály segítségével
                {
                    conn.Open();                                                                                                                                // Megnyitom az adatbázis kapcsolatot
                    string sql = "SELECT * FROM vasarlo WHERE id=@id;";                                                                                         // SQL lekérdezés, amely az adott id alapján lekéri a vásárlót az adatbázisból
                    MySqlCommand cmd = new MySqlCommand(sql, conn);                                                                                             // Létrehozok egy új MySQL parancsot a lekérdezés és a kapcsolat megadásával
                    cmd.Parameters.AddWithValue("@id", id);                                                                                                     // Hozzáadom az id paramétert a lekérdezéshez
                    MySqlDataReader reader = cmd.ExecuteReader();                                                                                               // Végrehajtom a lekérdezést és létrehozok egy MySQL adatolvasót az eredmények olvasásához
                    reader.Read();                                                                                                                              // Amíg van olvasható sor az eredmények között, addig végrehajtom a következő kódot
                    result.Id = reader.GetInt32("id");                                                                                                          // Beállítom az Id tulajdonságot az adatbázisból olvasott értékre
                    result.Nev = reader.GetString("Nev");                                                                                                       // Beállítom a Nev tulajdonságot az adatbázisból olvasott értékre
                    result.Cim = reader.GetString("Cim");                                                                                                       // Beállítom a Cim tulajdonságot az adatbázisból olvasott értékre
                    result.Email = reader.GetString("Email");                                                                                                   // Beállítom az Email tulajdonságot az adatbázisból olvasott értékre
                    result.Telefon = reader.GetString("Telefon");                                                                                               // Beállítom a Telefon tulajdonságot az adatbázisból olvasott értékre
                    result.Pontszam = reader.GetInt32("Pontszam");                                                                                              // Beállítom a Pontszam tulajdonságot az adatbázisból olvasott értékre
                    conn.Close();                                                                                                                               // Bezárom az adatbázis kapcsolatot
                }                                                                                                                                               // Visszaadom az adatbázisból olvasott vásárlót tartalmazó Vasarlo objektumot
                return result;                                                                                                                                  // Visszaadom az adatbázisból olvasott vásárlót tartalmazó Vasarlo objektumot
            }                                                                                                                                                   // Ha hiba történik az adatbázis műveletek során, elkapom a kivételt
            catch (Exception ex)                                                                                                                                // Elkapom az esetleges kivételeket, és kiírom a hibaüzenetet a konzolra
            {                                                                                                                                                   // Kiírom a hibaüzenetet a konzolra
                Vasarlo hiba = new Vasarlo();                                                                                                                   // Létrehozok egy új Vasarlo objektumot, amelybe a hibaüzenetet fogom menteni
                hiba.Nev = ex.Message;                                                                                                                          // Beállítom a Nev tulajdonságot a hibaüzenetre
                return hiba;                                                                                                                                    // Visszaadok egy Vasarlo objektumot, amelynek a Nev tulajdonsága a hibaüzenet
            }                                                                                                                                                   // Visszaadok egy Vasarlo objektumot, amelynek a Nev tulajdonsága a hibaüzenet
        }                                                                                                                                                       // Visszaadok egy Vasarlo objektumot, amelynek a Nev tulajdonsága a hibaüzenet

        public string PostVasarlo(Vasarlo uj)                                                                                                                   // PostVasarlo metódus deklarációja, amely beszúr egy új vásárlót az adatbázisba
        {
            try                                                                                                                                                 // Próbálkozás az adatbázis műveletek végrehajtására
            {                                                                                                                                                   // Létrehozok egy új MySQL kapcsolatot a DBManager osztály segítségével
                string result = "";                                                                                                                             // Létrehozok egy üres string változót, amelybe a beszúrás eredményét fogom menteni
                using (MySqlConnection conn = new DBManager().CreateConnection())                                                                               // Létrehozok egy új MySQL kapcsolatot a DBManager osztály segítségével
                {                                                                                                                                            
                    conn.Open();                                                                                                                                // Megnyitom az adatbázis kapcsolatot
                    string sql = "INSERT INTO vasarlo (Nev, Cim, Email, Telefon, Pontszam) VALUES (@nev, @cim, @email, @telefon, @pontszam);";                  // SQL lekérdezés, amely beszúr egy új vásárlót az adatbázisba
                    MySqlCommand cmd = new MySqlCommand(sql, conn);                                                                                             // Létrehozok egy új MySQL parancsot a lekérdezés és a kapcsolat megadásával
                    cmd.Parameters.AddWithValue("@nev", uj.Nev);                                                                                                // Hozzáadom a nev paramétert a lekérdezéshez
                    cmd.Parameters.AddWithValue("@cim", uj.Cim);                                                                                                // Hozzáadom a cim paramétert a lekérdezéshez
                    cmd.Parameters.AddWithValue("@email", uj.Email);                                                                                            // Hozzáadom az email paramétert a lekérdezéshez
                    cmd.Parameters.AddWithValue("@telefon", uj.Telefon);                                                                                        // Hozzáadom a telefon paramétert a lekérdezéshez
                    cmd.Parameters.AddWithValue("@pontszam", uj.Pontszam);                                                                                      // Hozzáadom a pontszam paramétert a lekérdezéshez
                    int sorokszama = cmd.ExecuteNonQuery();                                                                                                     // Végrehajtom a lekérdezést és visszaadom a beszúrt sorok számát
                    if (sorokszama > 0)                                                                                                                         // Ha a beszúrt sorok száma nagyobb, mint 0, akkor sikeres a beszúrás
                    { 
                        result = "Sikeres beszúrás!";                                                                                                           // Ha a beszúrt sorok száma nagyobb, mint 0, akkor sikeres a beszúrás
                    }
                    else
                    {
                        result = "Sikertelen beszúrás!";                                                                                                        // Ha a beszúrt sorok száma 0, akkor sikertelen a beszúrás
                    }
                    conn.Close();                                                                                                                               // Bezárom az adatbázis kapcsolatot
                }
                return result;                                                                                                                                  // Visszaadom a beszúrás eredményét
            }
            catch (Exception ex)                                                                                                                                // Ha hiba történik az adatbázis műveletek során, elkapom a kivételt
            {
                return ex.Message;                                                                                                                              // Visszaadom a hibaüzenetet
            }
        }

        public string PutVasarlo(Vasarlo uj)
        {
            try
            {
                string result = "";
                using (MySqlConnection conn = new DBManager().CreateConnection())
                {
                    conn.Open();
                    string sql = "UPDATE vasarlo SET nev=@nev, cim=@cim, email=@email, telefon=@telefon, pontszam=@pontszam WHERE id=@id;";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@id", uj.Id);
                    cmd.Parameters.AddWithValue("@nev", uj.Nev);
                    cmd.Parameters.AddWithValue("@cim", uj.Cim);
                    cmd.Parameters.AddWithValue("@email", uj.Email);
                    cmd.Parameters.AddWithValue("@telefon", uj.Telefon);
                    cmd.Parameters.AddWithValue("@pontszam", uj.Pontszam);
                    int sorokszama = cmd.ExecuteNonQuery();
                    if (sorokszama > 0)
                    {
                        result = "Sikeres módosítás!";
                    }
                    else
                    {
                        result = "Sikertelen módosítás!";
                    }
                    conn.Close();
                }
                return result;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string DeleteVasarlo(Vasarlo uj)
        {
            try
            {
                string result = "";
                using (MySqlConnection conn = new DBManager().CreateConnection())
                {
                    conn.Open();
                    string sql = "DELETE FROM `vasarlo` WHERE id=@id";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@id", uj.Id);
                    int sorokszama = cmd.ExecuteNonQuery();
                    if (sorokszama > 0)
                    {
                        result = "Sikeres törlés!";
                    }
                    else
                    {
                        result = "Sikertelen törlés!";
                    }
                    conn.Close();
                }
                return result;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
