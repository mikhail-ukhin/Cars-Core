using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CarsCore.Controllers;
using CarsCore.Controllers.Resources;
using CarsCore.Core;
using CarsCore.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace CarsCore.Tests
{
    [TestFixture]
    public class VehiclesControllerFeaturesTests
    {
        private Mock<IUnitOfWork> _mockUnitOfWork;
        private Mock<IMapper> _mockMapper;
        private Mock<IFeaturesRepository> _mockFeaturesRepository;

        private readonly List<Feature> _features = new List<Feature>
        {
            new Feature {Id = 1, Name = "A"},
            new Feature {Id = 2, Name = "B"},
            new Feature {Id = 3, Name = "C"}
        };

        private List<KeyValuePairResource> _listKeyValuePairResourses;
        private VehiclesController _vehiclesController;

        [SetUp]
        public void SetUp()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockMapper = new Mock<IMapper>();
            _mockFeaturesRepository = new Mock<IFeaturesRepository>();

            _listKeyValuePairResourses =
                _features.Select(f => new KeyValuePairResource {Id = f.Id, Name = f.Name}).ToList();

            _vehiclesController = new VehiclesController(_mockMapper.Object, _mockUnitOfWork.Object);
            _mockUnitOfWork.Setup(uow => uow.Features).Returns(_mockFeaturesRepository.Object);

            _mockFeaturesRepository.Setup(fr => fr.GetAllAsync())
                .Returns(async () => await Task.Run(() => _features));
            _mockMapper.Setup(m => m.Map<List<Feature>, List<KeyValuePairResource>>(_features))
                .Returns(_listKeyValuePairResourses);
        }

        [Test]
        public async Task GetFeatures_FeaturesIsEmpty_ReturnsNotFound()
        {
            _mockFeaturesRepository.Setup(fr => fr.GetAllAsync())
                .Returns(async () => await Task.Run(() => new List<Feature>()));

            var result = await _vehiclesController.GetFeatures();

            Assert.That(result, Is.InstanceOf(typeof(NotFoundResult)));
        }

        [Test]
        public async Task GetFeatures_3ItemsInFeatures_ReturnsOkObject()
        {
            var result = await _vehiclesController.GetFeatures();

            Assert.That(result, Is.InstanceOf(typeof(OkObjectResult)));
            Assert.That(((OkObjectResult)result).Value, Is.EquivalentTo(_listKeyValuePairResourses));
        }

        [Test]
        public void GetFeatures_WhenCalled_GetAllAsyncIsCalled()
        {
            _mockUnitOfWork.Verify(uow => uow.Features.GetAllAsync());
        }

    }
}
