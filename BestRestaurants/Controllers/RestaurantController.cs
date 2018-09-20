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
  }
}
