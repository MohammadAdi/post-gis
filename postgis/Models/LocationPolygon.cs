using NetTopologySuite.Geometries;

namespace postgis.Models
{
    public class LocationPolygon : BaseGeomEntity
    {
        public Polygon Location { get; set; }
    }
}
