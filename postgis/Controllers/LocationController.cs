using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.IO;
using Newtonsoft.Json;
using postgis.Infrastructures.Context;
using postgis.Models;
using postgis.ViewModels;
using System.IO;
using System.Threading.Tasks;
using NetTopologySuite.Geometries;
using System.Collections.Generic;

namespace postgis.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api/controller")]
    public class LocationController : Controller
    {
        private readonly IApplicationDbContext _context;

        public LocationController(IApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("Location/Points")]
        public async Task<IActionResult> GetPoints()
        {
            try
            {
                var result = await _context.LocationPoints.ToListAsync();
                return Ok(result);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("Location/Polygons")]
        public async Task<IActionResult> GetPolygons()
        {
            try
            {
                var result = await _context.LocationPolygons.ToListAsync();
                return Ok(result);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("Location/Polylines")]
        public async Task<IActionResult> GetPolylines()
        {
            try
            {
                var result = await _context.LocationPolyLines.ToListAsync();
                return Ok(result);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        [HttpPost]
        [Route("Location/Create/Point")]
        public async Task<IActionResult> CreateLocationPoint([FromBody] PointModels models)
        {
            try
            {
                GeometryFactory fact = new GeometryFactory();
                var result = new LocationPoint()
                {
                    CityId = models.CityId,
                    Location = fact.CreatePoint(new Coordinate(models.Coordinates[0], models.Coordinates[1]))
                };
                _context.LocationPoints.Add(result);
                await _context.SaveChangeAsync();
                return Ok(result);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("Location/Create/Polygon")]
        public async Task<IActionResult> CreateLocationPolygon([FromBody] PolygonModels models)
        {
            try
            {
                GeometryFactory fact = new GeometryFactory();

                var coordinates = new List<Coordinate>();

                foreach (var items in models.Coordinates)
                {
                    foreach (var data in items)
                    {
                        coordinates.Add(new Coordinate(data[0], data[1]));
                    }
                }

                var result = new LocationPolygon()
                {
                    CityId = models.CityId,
                    Location = fact.CreatePolygon(coordinates.ToArray())
                };
                _context.LocationPolygons.Add(result);
                await _context.SaveChangeAsync();
                return Ok(result);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("Location/Create/PolyLine")]
        public async Task<IActionResult> CreateLocationPolyline([FromBody] PolyLineModels models)
        {
            try
            {
                GeometryFactory fact = new GeometryFactory();

                var coordinates = new List<Coordinate>();

                foreach (var items in models.Coordinates)
                {
                    coordinates.Add(new Coordinate(items[0], items[1]));
                }

                var result = new LocationPolyLine()
                {
                    CityId = models.CityId,
                    LineString = fact.CreateLineString(coordinates.ToArray())
                };
                _context.LocationPolyLines.Add(result);
                await _context.SaveChangeAsync();
                return Ok(result);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
