using System.ComponentModel.DataAnnotations;

namespace BagIt.Cloud.API.Model;

public class CustomerDatabase
{
    public CustomerDatabase(string connection) => Connection = connection;

    public int Id { get; set; }

    [Required]
    public string Connection { get; set; }
}
