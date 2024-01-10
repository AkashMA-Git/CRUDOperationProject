using Microsoft.EntityFrameworkCore;

namespace CRUDOperationProject.Entity
{
    public class EntityDbContext :DbContext
    {
        public EntityDbContext(DbContextOptions<EntityDbContext> options) : base(options)

        {
            
        }
    }
}
