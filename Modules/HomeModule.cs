using System.Collections.Generic;
using Nancy;
using Nancy.ViewEngines.Razor;
using BestRestaurants.Objects;



namespace BestRestaurants
{
  public class HomeModule : NancyModule
  {
    public HomeModule()
    {
      Get["/"] = _ => {
        return View["index.cshtml"];
      };

    }
  }
}
