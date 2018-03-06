using System;
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
    /// <summary>
    /// Контроллер с методами для авто
    /// </summary>
    [Route("/api/[controller]")]
    public class VehiclesController : Controller
    {
        private readonly CarsDbContext _context;
        private readonly IMapper _mapper;

        public VehiclesController(CarsDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Получить список функций для авто
        /// </summary>
        /// <returns>
        /// <response code="200">Список функций для авто</response>
        /// <response code="404">Список функций пуст</response>
        /// <response code="500">Информации об исключении</response>
        /// </returns>
        [HttpGet("features")]
        public async Task<IActionResult> GetFeatures()
        {
            var features = await _context.Features.ToListAsync();

            if (!features.Any())
                return NotFound();

            return Ok(_mapper.Map<List<Feature>, List<KeyValuePairResource>>(features));
        }

        /// <summary>
        /// Получить список производителей авто
        /// </summary>
        /// <returns>
        /// <response code="200">Список производителей авто</response>
        /// <response code="404">Список производителей пуст</response>
        /// <response code="500">Информации об исключении</response>
        /// </returns>
        [HttpGet("makes")]
        public async Task<IActionResult> GetMakes()
        {
            var makes = await _context.Makes.Include(m => m.Models).ToListAsync();

            if (!makes.Any())
                return NotFound();

            return Ok(_mapper.Map<List<Make>, List<MakeResource>>(makes));
        }

        /// <summary>
        /// Добавить новую модель авто
        /// </summary>
        /// <param name="vehicleResource">Dto объект, содержащий информацию об авто</param>
        /// <returns>
        /// <response code="200">Информация о добавленном авто</response>
        /// <response code="400">Информации о состоянии модели валидации</response>
        /// <response code="500">Информации об исключении</response>
        /// </returns>
        [HttpPost]
        public async Task<IActionResult> CreateVehicle([FromBody]SaveVehicleResource vehicleResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var model = await _context.Models.FindAsync(vehicleResource.ModelId);

            if (model == null)
            {
                return NotFound();
            }

            var vehicle = _mapper.Map<SaveVehicleResource, Vehicle>(vehicleResource);
            vehicle.LastUpdate = DateTime.Now;

            await _context.Vehicles.AddAsync(vehicle);
            await _context.SaveChangesAsync();

            return Ok(_mapper.Map<Vehicle, SaveVehicleResource>(vehicle));
        }

        /// <summary>
        /// Обновить запись о существующем авто
        /// </summary>
        /// <param name="vehicleId">Id авто</param>
        /// <param name="vehicleResource">Dto объект, содержащий информацию об авто</param>
        /// <returns>
        /// <response code="200">Id обновленного авто</response>
        /// <response code="400">Информации о состоянии модели валидации</response>
        /// <response code="500">Информации об исключении</response>
        /// </returns>
        [HttpPut("{vehicleId:int}")]
        public async Task<IActionResult> UpdateVehicle(int vehicleId, [FromBody]SaveVehicleResource vehicleResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var vehicle = await _context.Vehicles
            .Include(f => f.Features)
            .SingleOrDefaultAsync(v => v.Id == vehicleId);

            if (vehicle == null)
            {
                return NotFound();
            }

            _mapper.Map<SaveVehicleResource, Vehicle>(vehicleResource, vehicle);
            await _context.SaveChangesAsync();

            return Ok(vehicleId);
        }

        /// <summary>
        /// Удалить запись об автомобиле
        /// </summary>
        /// <param name="vehicleId">Id аавтомобиля</param>
        /// <returns>Id автомобиля</returns>
        [HttpDelete("{vehicleId:int}")]
        public async Task<IActionResult> DeleteVehicle(int vehicleId)
        {
            var vehicle = await _context.Vehicles.FindAsync(vehicleId);

            if (vehicle == null)
            {
                return NotFound();
            }

            _context.Remove(vehicle);
            await _context.SaveChangesAsync();

            return Ok(vehicleId);
        }

        /// <summary>
        /// Получить информацию об автомобиле по id
        /// </summary>
        /// <param name="vehicleId">Id автомобиля</param>
        /// <returns>Dto объект, содержащий информацию об автомобиле</returns>
        [HttpGet("{vehicleId:int}")]
        public async Task<IActionResult> GetVehicle(int vehicleId)
        {
            var vehicle = await _context.Vehicles
                .Include(v => v.Features)
                .ThenInclude(vf => vf.Feature)
                .Include(v => v.Model)
                .ThenInclude(m => m.Make)
                .SingleOrDefaultAsync(v => v.Id == vehicleId);

            if (vehicle == null)
                return NotFound();

            var vehicleResource = _mapper.Map<Vehicle, VehicleResource>(vehicle);

            return Ok(vehicleResource);
        }
    }
}