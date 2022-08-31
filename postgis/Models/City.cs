using System.ComponentModel.DataAnnotations;

namespace postgis.Models
{
    public class City
    {
        [Key]
        public long ID { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
