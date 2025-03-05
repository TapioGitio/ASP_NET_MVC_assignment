using Microsoft.EntityFrameworkCore;

namespace Data.Context;

public class DataContext(DbContextOptions options) : DbContext(options)
{

}
