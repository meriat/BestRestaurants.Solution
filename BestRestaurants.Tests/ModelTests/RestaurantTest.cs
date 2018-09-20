using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using BestRestaurants.Models;

namespace BestRestaurants.Tests
{
  [TestClass]
  public class RestaurantTests : IDisposable
  {
    public RestaurantTests()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=best_restaurants_test;";
    }

    public void Dispose()
    {
      Restaurant.DeleteAll();
      Cuisine.DeleteAll();

      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"ALTER TABLE restaurants AUTO_INCREMENT = 1;";
      cmd.ExecuteNonQuery();
      cmd.CommandText = @"ALTER TABLE cuisines AUTO_INCREMENT = 1;";
      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }


    [TestMethod]
    public void Save_SaveRestaurant()
    {
      //Arrange
      Cuisine cuisine = new Cuisine("French");
      cuisine.Save();
      Restaurant restaurant = new Restaurant("Laduree", 5, cuisine.Id);

      //Act
      restaurant.Save();

      //Assert
      Assert.AreEqual(restaurant, Restaurant.GetAll()[0]);
    }

    [TestMethod]
    public void GetAll_DBStartsEmpty_0()
    {
      // Restaurant newRestaurant = new Restaurant(
      //Arrange
      List <Restaurant> allRestaurants = Restaurant.GetAll();

      //Act
      int actualCount = allRestaurants.Count;

      //Assert
      Assert.AreEqual(0, actualCount);
    }

    [TestMethod]
    public void GetAll_ReturnCorrectList_List()
    {
      //Arrange
      Cuisine cuisine = new Cuisine("French");
      cuisine.Save();
      Restaurant restaurant1 = new Restaurant("Laduree", 5, cuisine.Id);
      restaurant1.Save();
      Restaurant restaurant2 = new Restaurant("Pierre Herme", 5, cuisine.Id);
      restaurant2.Save();
      List <Restaurant> restaurants = new List<Restaurant>{restaurant1, restaurant2};

      //Act
      List <Restaurant> returnedList = Restaurant.GetAll();

      //Assert
      CollectionAssert.AreEqual(restaurants, returnedList);
    }

    [TestMethod]
    public void Find_FindRestaurant_Restaurant()
    {
      //Arrange
      Cuisine cuisine = new Cuisine("French");
      cuisine.Save();
      Restaurant restaurant = new Restaurant("Laduree", 5, cuisine.Id);
      restaurant.Save();

      //Act
      Restaurant foundRestaurant = Restaurant.Find(restaurant.Id);

      //Assert
      Assert.AreEqual(restaurant, foundRestaurant);
    }
    [TestMethod]
    public void Edit_EditRestaurantName()
    {
      //Arrange
      Cuisine cuisine = new Cuisine("French");
      cuisine.Save();
      Restaurant restaurant = new Restaurant("Laduree", 5, cuisine.Id);
      restaurant.Save();

      //Act
      restaurant.Edit("Pierre Herme");
      Restaurant expectedRestaurant = new Restaurant("Pierre Herme", 5, cuisine.Id, restaurant.Id);

      //Assert
      Assert.AreEqual(expectedRestaurant, restaurant);
    }
    [TestMethod]
    public void DeleteRestaurant_DeleteARestaurant()
    {
      //Arrange
      Cuisine cuisine = new Cuisine("French");
      cuisine.Save();
      Restaurant restaurant = new Restaurant("Laduree", 5, cuisine.Id);
      restaurant.Save();

      //Act
      Restaurant.DeleteRestaurant(restaurant.Id);
      int actualCount = Restaurant.GetAll().Count;

      //Assert
      Assert.AreEqual(0, actualCount);
    }
  }
}
