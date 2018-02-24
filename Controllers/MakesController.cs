using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CarsCore.Controllers.Resources;
using CarsCore.Models;
using CarsCore.Persistance;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarsCore.Controllers
{
    public class MakesController : Controller
    {
        private readonly IMapper _mapper;
        private readonly CarsDbContext _context;

        public MakesController(CarsDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }


        [HttpGet("/api/makes")]
        public async Task<IEnumerable<MakeResource>> GetMakes()
        {
            var makes = await _context.Makes.Include(m => m.Models).ToListAsync();

            return _mapper.Map<List<Make>,List<MakeResource>>(makes);
        }
    }
}