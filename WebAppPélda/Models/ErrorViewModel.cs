namespace WebAppPélda.Models
{
    public class ErrorViewModel
    {
        public string? RequestId { get; set; }                                          // A RequestId tulajdonság a hibakérés azonosítóját tárolja, amely lehetővé teszi a hiba nyomon követését és azonosítását.

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);                  // A ShowRequestId tulajdonság egy logikai értéket ad vissza, amely jelzi, hogy a RequestId tulajdonság nem üres vagy null értékű-e. Ha a RequestId értéke nem üres, akkor a ShowRequestId true értéket ad vissza, különben false értéket.
    }
}
