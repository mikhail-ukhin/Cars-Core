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
    public class FeatureController : Controller
    {
        private readonly CarsDbContext _context;
        private readonly IMapper _mapper;

        public FeatureController(CarsDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet("/api/features")]
        public async Task<List<FeatureResource>> GetFeatures() {
            var features = await _context.Features.ToListAsync();
            return _mapper.Map<List<Feature>, List<FeatureResource>>(features);
        }
    }
}