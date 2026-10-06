namespace WebAppPelda.Models                                // Logikai csoportosítás, ez a mappa (névtér) tartalmazza a modelleket
{
    public class Vasarlo                                    // Létrehoz egy 'Vasarlo' nevű osztályt, amely egy vásárló adatait reprezentálja                        
    {
        public int Id { get; set; }                         // A vásárló egyedi számszerű azonosítója (elsődleges kulcs)              
        public string Nev { get; set; }                     // A vásárló teljes neve (szöveges formátumban)          
        public string Cim { get; set; }                     // A vásárló lakcíme vagy szállítási címe           
        public string Email { get; set; }                   // A vásárló elektronikus levelezési címe       
        public string Telefon { get; set; }                 // A vásárló telefonszáma (szövegként tárolva a formázások miatt)       
        public int Pontszam { get; set; }                   // A vásárló hűségpontjai vagy pontszáma (egész számként)      
    }
}
