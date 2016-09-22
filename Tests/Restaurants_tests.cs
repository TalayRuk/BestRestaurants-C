using Xunit;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using BestRestaurants;

namespace BestRestaurants
{
  public class Restaurant_tests : IDisposable
  {
    public Restaurant_Tests()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=best_restaurants_tests;Integrated Security=SSPI;";
    }
    //when first created Restaurant_Tests :error cs1520 doesn't have return type!!

    //test GetAll
    [Fact]
    public void Test1_IsRestaurantDbEmpty()
    {
      //Act
      int result = Restaurant.GetAll().Count;
      //Assert
      Assert.Equal(0, result);
    }
    //test override
    [Fact]
    public void Test1_CheckEqualIsOverride()
    {
      //Arrange
      Restaurant restaurantOne = new Restaurant("thai");
      Restaurant restaurantTwo = new Restaurant("thai");

      //Assert
      Assert.Equal(restaurantOne, restaurantTwo);
    }

    public void Dispose()
    {
      Restaurant.DeleteAll();
    }
  }
}
