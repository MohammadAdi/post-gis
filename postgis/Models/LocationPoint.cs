
using NetTopologySuite.Geometries;

namespace postgis.Models
{
    public class LocationPoint : BaseGeomEntity
    {
        public Point Location { get; set; }
    }
}
