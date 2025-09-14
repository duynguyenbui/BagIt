using System.ComponentModel.DataAnnotations;

namespace BagIt.Cloud.API.Model;

public class CustomerDatabase
{
    public CustomerDatabase(string connectionString)
    {
        ConnectionString = connectionString;
    }

    public int Id { get; set; }

    [Required]
    public string ConnectionString { get; set; }
}
