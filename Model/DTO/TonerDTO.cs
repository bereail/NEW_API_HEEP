using toner_store.Models;

namespace toner_store.Model.DTO
{
    public class TonerDTO : Toner
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public int Cant { get; set; }
    }
}
