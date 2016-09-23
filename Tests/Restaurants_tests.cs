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
    public void Test2_CheckEqualIsOverride()
    {
      //Arrange
      Restaurant restaurantOne = new Restaurant("thai");
      Restaurant restaurantTwo = new Restaurant("thai");

      //Assert
      Assert.Equal(restaurantOne, restaurantTwo);
    }
    //test Save
    [Fact]
    public void Test3_SaveWork()
    {
      //Arrange
      Restaurant testRestaurant = new Restaurant("thai");
      testRestaurant.Save();

      //Act
      List<Restaurant> result = Restaurant.GetAll();
      List<Restaurant> testList = new List<Restaurant>{testRestaurant};

      //Assert
      Assert.Equal(testList, result);
    }
    //test save id
    [Fact]
    public void Test4_SaveIdToDb()
    {
      //Arrange
      Restaurant testRestaurant = new Restaurant("thai");
      testRestaurant.Save();

      //Act
      Restaurant savedRestaurant = Restaurant.GetAll()[0];

      int result = savedRestaurant.GetId();
      int testRestaurantId = testRestaurant.GetId();

      //Assert
      Assert.Equal(testRestaurantId, result);
    }
    //test Find
    [Fact]
    public void Test5_FindId()
    {
      //Arrange
    Restaurant testRestaurant = new Restaurant("thai");
    testRestaurant.Save();

    //Act
    Restaurant foundRestaurant = Restaurant.Find(testRestaurant.GetId());

    //Assert
    Assert.Equal(testRestaurant, foundRestaurant);
    }

    //clear database
    public void Dispose()
    {
      Cuisine.DeleteAll();
      Restaurant.DeleteAll();
    }
  }
}
