using Microsoft.EntityFrameworkCore;

namespace Data.Context;

class DataContext(DbContextOptions options) : DbContext(options)
{

}
