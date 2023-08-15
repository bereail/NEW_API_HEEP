using Microsoft.AspNetCore.Mvc;
using toner_store.Models;

namespace toner_store.Controllers
{
    public class BaseController : ControllerBase
    {
        protected readonly tonerStoreContext _dbContext;

        public BaseController(tonerStoreContext dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
