using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using BestRestaurants.Models;

namespace BestRestaurants.TestTools
{
  [TestClass]
  public class CuisineTests : IDisposable
  {
    public void Dispose()
    {
      Cuisine.DeleteAll();
    }
    public CuisineTests()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=best_restaurants_test;";
    }

    [TestMethod]
    public void GetAll_DBStartsEmpty_0()
    {
      //Arrange
      //Act
      int result = Cuisine.GetAll().Count;

      //Assert
      Assert.AreEqual(0,result);
    }
    [TestMethod]
    public void GetAll_DBCompareObjects_Equal()
    {
      //Arrange
      List <Cuisine> cuisines = new List<Cuisine>{};
      Cuisine newCuisine = new Cuisine("French");
      newCuisine.Save();

      //Act
      cuisines = Cuisine.GetAll();

      //Assert
      Assert.AreEqual(newCuisine, cuisines[0]);
    }
    [TestMethod]
    public void Find_FindCuisineInDatabase_Cuisine()
    {
      //Arrange
      Cuisine newCuisine = new Cuisine("French");
      newCuisine.Save();

      //Act
      Cuisine foundCuisine = Cuisine.Find(newCuisine.Id);

      //Assert
      Assert.AreEqual(newCuisine, foundCuisine);
    }

    [TestMethod]
    public void Edit_UpdatesItemInDatabase_String()
    {
      //Arrange
      Cuisine firstCuisine = new Cuisine("French");
      firstCuisine.Save();
      string testCuisine = "Italian";

      //Act
      firstCuisine.Edit(testCuisine);
      string result = Cuisine.Find(firstCuisine.Id).Name;

      //Assert
      Assert.AreEqual(testCuisine,result);

    }
    [TestMethod]
    public void DeleteCuisine_DeleteCuisineInDB()
    {
      //Arrange
      Cuisine firstCuisine = new Cuisine("French");
      firstCuisine.Save();

      //Act
      Cuisine.DeleteCuisine(firstCuisine.Id);
      int count = Cuisine.GetAll().Count;

      //Assert
      Assert.AreEqual(0,count);
    }
  }
}
