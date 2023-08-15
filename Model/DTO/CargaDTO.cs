using toner_store.Models;

namespace toner_store.Model.DTO
{
    public class CargaDTO 
    {
        public int Id { get; set; }
        public int? IdUser { get; set; }
        public int? IdToner { get; set; }
        public int? IdService { get; set; }
        public int? Cant { get; set; }
        public DateTime? CargaAt { get; set; }
    }
}
