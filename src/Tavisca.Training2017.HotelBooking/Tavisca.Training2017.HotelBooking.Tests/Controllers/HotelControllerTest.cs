using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Moq;
using Tavisca.Training2017.HotelBooking.Controllers;
using Tavisca.Training2017.HotelBooking.Models;
//using Tavisca.Training2017.HotelBooking.Factories;
using Microsoft.AspNetCore.Mvc;

namespace Tavisca.Training2017.HotelBooking.Tests.Controller
{
    public class HotelControllerTest
    {
        [Fact]
        public void Search_Should_Give_Valid_Result_When_SearchRQ_Is_Complete()
        {
            //Arrange
            HotelController hotelController = new HotelController();
            var expectedResponse = new OkResult();
            
            var searchRQ = new SearchRQ()
            {
                SearchText = "Pune",
                CheckIn = System.DateTime.Now,
                CheckOut = System.DateTime.Now
            };

            //Act
            var actualResponse=hotelController.Search(searchRQ);
            

            //Assert
            Assert.Equal(expectedResponse.ToString(),actualResponse.ToString());

        }
    }
}
