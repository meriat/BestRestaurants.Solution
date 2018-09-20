using Microsoft.AspNetCore.Mvc;
using BestRestaurants.Models;
using System;
using System.Collections.Generic;

namespace BestRestaurants.Controllers
{
  public class RestaurantController : Controller
  {
    [HttpPost("/cuisines/{cuisine_id}/restaurants")]
    public ActionResult CreateRestaurant(int cuisine_id, string restaurant_name, int restaurant_rating)
    {
      Restaurant newRestaurant = new Restaurant(restaurant_name,restaurant_rating,cuisine_id);
      newRestaurant.Save();
      return RedirectToAction("Details", "Cuisine", new {id=cuisine_id});
    }
    [HttpGet("/cuisines/{cuisine_id}/restaurants/{restaurant_id}/update")]
    public ActionResult UpdateForm(int cuisine_id, int restaurant_id)
    {
      Restaurant newRestaurant = Restaurant.Find(restaurant_id);
      return View(newRestaurant);
    }
    [HttpPost("/cuisines/{cuisine_id}/restaurants/{restaurant_id}")]
    public ActionResult Update(int cuisine_id, int restaurant_id, string newName, int newRating)
    {
      Restaurant foundRestaurant = Restaurant.Find(restaurant_id);
      foundRestaurant.Edit(newName, newRating);
      return RedirectToAction("Details", "Cuisine", new {id=cuisine_id});
    }
    [HttpPost("/cuisines/{cuisine_id}/restaurants/{restaurant_id}/delete")]
    public ActionResult Delete(int cuisine_id, int restaurant_id)
    {
      Restaurant.DeleteRestaurant(restaurant_id);
      return RedirectToAction("Details", "Cuisine", new {id=cuisine_id});
    }
  }
}
