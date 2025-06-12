using Xunit;
using Moq;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HouseBroker.Api.Controllers;
using HouseBroker.Infra.Services;
using HouseBroker.Infra.Dtos;
using HouseBroker.Infra.Dtos.FilterDtos;
using System.Collections.Generic;
using FluentAssertions;
using HouseBroker.Domain.Models;

namespace HouseBroker.Tests.Controllers
{
    public class PropertyListingControllerTests
    {
        private readonly Mock<IPropertyListingService> _mockService;
        private readonly PropertyListingController _controller;

        public PropertyListingControllerTests()
        {
            _mockService = new Mock<IPropertyListingService>();
            _controller = new PropertyListingController(_mockService.Object);
        }

        [Fact]
        public async Task GetAll_ShouldReturnOk_WithListings()
        {
            var filter = new PropertyListingFilter();
            var mockData = new PagedObject<PropertyListing>(
                         new List<PropertyListing> { new PropertyListing { Id = Guid.NewGuid() } },
                         totalPages: 1,
                            currentPage: 1,
                                totalRecords: 1
                                                 );
            _mockService.Setup(s => s.GetAllListingsAsync(filter)).ReturnsAsync(mockData);
            var result = await _controller.GetAll(filter);
            var okResult = result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult.Value.Should().BeEquivalentTo(mockData);
        }

        [Fact]
        public async Task GetById_ShouldReturnOk_IfFound()
        {
            var id = Guid.NewGuid();
            var listing = new PropertyListingDtos { Id = id };
            _mockService.Setup(s => s.GetListingByIdAsync(id)).ReturnsAsync(listing);
            var result = await _controller.GetById(id);
            var okResult = result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult.Value.Should().BeEquivalentTo(listing);
        }

        [Fact]
        public async Task GetById_ShouldReturnNotFound_IfNull()
        {
            var id = Guid.NewGuid();
            _mockService.Setup(s => s.GetListingByIdAsync(id)).ReturnsAsync((PropertyListingDtos)null);
            var result = await _controller.GetById(id);
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task Create_ShouldReturnCreatedAtAction()
        {
            var dto = new PropertyListingDtos { Id = Guid.NewGuid() };
            _mockService.Setup(s => s.AddListingAsync(dto)).ReturnsAsync(dto);
            var result = await _controller.Create(dto);
            var createdAtActionResult = result as CreatedAtActionResult;
            createdAtActionResult.Should().NotBeNull();
            createdAtActionResult.Value.Should().BeEquivalentTo(dto);
        }

        [Fact]
        public async Task Create_ShouldReturn500_IfCreationFails()
        {
            var dto = new PropertyListingDtos();
            _mockService.Setup(s => s.AddListingAsync(dto)).ReturnsAsync((PropertyListingDtos)null);
            var result = await _controller.Create(dto);
            var statusResult = result as ObjectResult;
            statusResult.StatusCode.Should().Be(500);
        }

        [Fact]
        public async Task Update_ShouldReturnOk_WithUpdatedData()
        {
            var dto = new PropertyListingDtos { Id = Guid.NewGuid() };
            _mockService.Setup(s => s.UpdateListingAsync(dto)).ReturnsAsync(dto);
            var result = await _controller.Update(dto);
            var okResult = result as OkObjectResult;
            okResult.Value.Should().BeEquivalentTo(dto);
        }

        [Fact]
        public async Task Delete_ShouldReturnNoContent_IfSuccess()
        {
            var id = Guid.NewGuid();
            _mockService.Setup(s => s.DeleteListingAsync(id)).ReturnsAsync(true);
            var result = await _controller.Delete(id);
            result.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public async Task Delete_ShouldReturnNotFound_IfFailed()
        {
            var id = Guid.NewGuid();
            _mockService.Setup(s => s.DeleteListingAsync(id)).ReturnsAsync(false);
            var result = await _controller.Delete(id);
            result.Should().BeOfType<NotFoundResult>();
        }
    }
}
