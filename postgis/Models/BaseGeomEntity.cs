using System.ComponentModel.DataAnnotations;

namespace postgis.Models
{
    public class BaseGeomEntity
    {
        [Key]
        public long ID { get; set; }
        [Required]
        public long CityId { get; set; }

        public City City { get; set; }

    }
}
