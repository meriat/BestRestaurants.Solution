using Microsoft.AspNetCore.Mvc;
using BestRestaurants.Models;
using System;
using System.Collections.Generic;

namespace BestRestaurants.Controllers
{
  public class CuisineController : Controller
  {
    [HttpGet("/cuisines")]
    public ActionResult Index()
    {
      List<Cuisine> allCuisines = Cuisine.GetAll();
      return View("Index",allCuisines);
    }
    [HttpPost("/cuisines")]
    public ActionResult Add(string name)
    {
      Cuisine newCuisine = new Cuisine(name);
      newCuisine.Save();
      return RedirectToAction("Index");
    }
    [HttpGet("/cuisines/{id}")]
    public ActionResult Details(int id)
    {
      Cuisine foundCuisine = Cuisine.Find(id);
      return View(foundCuisine);
    }
    [HttpGet("/cuisines/{id}/update")]
    public ActionResult UpdateForm(int id)
    {
      Cuisine existingCuisine = Cuisine.Find(id);
      return View(existingCuisine);
    }
    [HttpPost("/cuisines/{id}/update")]
    public ActionResult Update(string newName, int id)
    {
      Cuisine foundCuisine = Cuisine.Find(id);
      foundCuisine.Edit(newName);
      return RedirectToAction("Index");
    }
    [HttpPost("/cuisines/{id}/delete")]
    public ActionResult Delete(int id)
    {
      Cuisine.DeleteCuisine(id);
      return RedirectToAction("Index");
    }
  }
}
