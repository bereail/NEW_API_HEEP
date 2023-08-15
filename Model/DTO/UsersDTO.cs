using toner_store.Models;

namespace toner_store.Model.DTO
{
    public class UsersDTO : Users
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Pass { get; set; }
        public int IdRol { get; set; }
    }
}
