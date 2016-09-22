using System;
using Xunit;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using BestRestaurants;
using BestRestaurants.Objects;

namespace BestRestaurants
{
  public class Cuisine_Tests : IDisposable
   {
     public Cuisine_Tests()
      {
        DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=bestRestaurants_tests;Integrated Security=SSPI;";
      }
      [Fact]
      public void Test1_IsDbEmpty_true()
      {
        //Act
        int AnyRows = Cuisine.GetAll().Count;
        //Assert
        Assert.Equal(0, AnyRows);
      }
      [Fact]
      public void test2_CheckEqualsOverride()
      {
        Cuisine firstCuisine = new Cuisine ("tacos");
        Cuisine secondCuisine = new Cuisine ("tacos");
        Assert.Equal(firstCuisine, secondCuisine);
      }

      [Fact]
      public void Test3_canWeSave_true()
      {
        //Arrange
        Cuisine newCuisine = new Cuisine ("noodles");
        //Act
        newCuisine.Save();
        Cuisine result = Cuisine.GetAll()[0];
        //Assert
        Assert.Equal(newCuisine, result);
      }

      [Fact]
      public void Test4_DeleteAllDoesDelete_true()
      {
        //Arrange
        Cuisine newCuisineOne = new Cuisine ("noodles");
        Cuisine newCuisineTwo = new Cuisine ("pizza");
        newCuisineOne.Save();
        newCuisineTwo.Save();
        //Act
        Cuisine.DeleteAll();
        List<Cuisine> result = Cuisine.GetAll();
        //Assert
        Assert.Equal(0 , result.Count);
      }

      [Fact]
      public void Test5_Find_DoesFind()
      {
        // Arrange
        Cuisine foundCuisine = new Cuisine("noodles");
        foundCuisine.Save();
        // Act
        Cuisine resultCuisine = Cuisine.Find(foundCuisine.GetId());
        // Assert
        Assert.Equal(foundCuisine, resultCuisine);
      }

      [Fact]
      public void TEst6_DeleteOne()
      {
        //Assert
        Cuisine oneCuisine = new Cuisine("noodles");
        Cuisine twoCuisine = new Cuisine("pizza");
        oneCuisine.Save();
        twoCuisine.Save();
        // Act
        oneCuisine.DeleteOne();
        List<Cuisine> result = Cuisine.GetAll();
        //Assert
        Assert.Equal(1, result.Count);
      }

      public void Dispose()
      {
        Cuisine.DeleteAll();
      }
    }
  }




  //spec
  // |GetAll() returns all rows present in db|INPUT: 2 Car instances| OUTPUT: List containing both cars
  //
  // [Fact]
  // public void TestGetAll_true()
  // {
  //   //Arrange
  //   Car carOne = new Car("Ford", 1993);
  //   Car carTwo = new Car("Chevy", 2001);
  //   List<Car> expectedList = new List<Car>() {carOne, carTwo};
  //   //Act
  //   carOne.Save();
  //   carTwo.Save();
  //   //Assert
  //   List<Car> result = Car.GetAll();
  //   Assert.Equal(expectedList, result);
  // }
