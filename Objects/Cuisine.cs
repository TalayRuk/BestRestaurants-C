using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace BestRestaurants.Objects
{
  public class Cuisine
    {
      private string _name;
      private int _id;
    }
    public Cuisine(string name, int id = 0 )
    {
      _name = name;
      _id = id;
    }
    public string GetName()
    {
      return _name;
    }
    public int GetId()
    {
      return _id;
    }
    public void SetName(string name)
    {
      _name = name;
    }
    public void SetId(int id)
    {
      _id = id;
    }
    
}
