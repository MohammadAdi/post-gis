using NetTopologySuite.Geometries;

namespace postgis.Models
{
    public class LocationPolyLine : BaseGeomEntity
    {
        public LineString LineString { get; set; }
    }
}
