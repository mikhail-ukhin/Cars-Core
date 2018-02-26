using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CarsCore.Controllers.Resources;
using CarsCore.Models;
using CarsCore.Persistance;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarsCore.Controllers
{
    public class VehicleController : Controller
    {
        private readonly CarsDbContext _context;
        private readonly IMapper _mapper;

        public VehicleController(CarsDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost("/api/vehicle")]
        public IActionResult CreateVehicle([FromBody]VehicleResource vehicleResource) {
            return Ok(_mapper.Map<VehicleResource,Vehicle>(vehicleResource));
        }


        [HttpGet("/api/vehicle/features")]
        public async Task<List<FeatureResource>> GetFeatures()
        {
            var features = await _context.Features.ToListAsync();
            return _mapper.Map<List<Feature>, List<FeatureResource>>(features);
        }

        [HttpGet("/api/vehicle/makes")]
        public async Task<IEnumerable<MakeResource>> GetMakes()
        {
            var makes = await _context.Makes.Include(m => m.Models).ToListAsync();

            return _mapper.Map<List<Make>, List<MakeResource>>(makes);
        }
    }
}