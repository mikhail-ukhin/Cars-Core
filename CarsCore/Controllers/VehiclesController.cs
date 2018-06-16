using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CarsCore.Controllers.Resources;
using CarsCore.Core;
using CarsCore.Models;
using Microsoft.AspNetCore.Mvc;

namespace CarsCore.Controllers
{
    /// <summary>
    /// Контроллер с методами для авто
    /// </summary>
    [Route("/api/[controller]")]
    public class VehiclesController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _uow;

        public VehiclesController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _uow = unitOfWork;
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
            var features = await _uow.Features.GetAllAsync();

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
            var makes = await _uow.Makes.GetAllWithModelsAsync();

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

            var model = await _uow.Models.GetAsync(vehicleResource.ModelId);

            if (model == null)
            {
                return NotFound();
            }

            var vehicle = _mapper.Map<SaveVehicleResource, Vehicle>(vehicleResource);
            vehicle.LastUpdate = DateTime.Now;

            await _uow.Vehicles.AddAsync(vehicle);
            await _uow.CompleteAsync();

            vehicle = await _uow.Vehicles.GetVehicleWithMakesAndFeaturesAsync(vehicle.Id);

            return Ok(_mapper.Map<Vehicle, VehicleResource>(vehicle));
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

            var vehicle = await _uow.Vehicles.GetVehicleWithMakesAndFeaturesAsync(vehicleId);

            if (vehicle == null)
            {
                return NotFound();
            }

            _mapper.Map(vehicleResource, vehicle);
            await _uow.CompleteAsync();

            return Ok(vehicleId);
        }

        /// <summary>
        /// Удалить запись об автомобиле
        /// </summary>
        /// <param name="vehicleId">Id автомобиля</param>
        /// <returns>
        /// <response code="200">Id удаленного автомобиля</response>
        /// <response code="404">Автомобиль не найден</response>
        /// <response code="500">Информации об исключении</response>
        /// </returns>
        [HttpDelete("{vehicleId:int}")]
        public async Task<IActionResult> DeleteVehicle(int vehicleId)
        {
            var vehicle = await _uow.Vehicles.GetAsync(vehicleId);

            if (vehicle == null)
            {
                return NotFound();
            }

            _uow.Vehicles.Remove(vehicle);
            await _uow.CompleteAsync();

            return Ok(vehicleId);
        }

        /// <summary>
        /// Получить информацию об автомобиле по id
        /// </summary>
        /// <param name="vehicleId">Id автомобиля</param>
        /// <returns>
        /// <response code="200">Dto объект с информацией об автомобиле</response>
        /// <response code="404">Автомобиль не найден</response>
        /// <response code="500">Информации об исключении</response>
        /// </returns>
        [HttpGet("{vehicleId:int}")]
        public async Task<IActionResult> GetVehicle(int vehicleId)
        {
            var vehicle = await _uow.Vehicles.GetVehicleWithMakesAndFeaturesAsync(vehicleId);

            if (vehicle == null)
                return NotFound();

            var vehicleResource = _mapper.Map<Vehicle, VehicleResource>(vehicle);

            return Ok(vehicleResource);
        }
    }
}