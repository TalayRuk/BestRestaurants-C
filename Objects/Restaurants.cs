using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace BestRestaurants
{
  public class Restaurants
  {
    private string _specialty;
    private int _id;

    public Restaurants(string specialty, int id = 0)
    {
      _specialty = specialty;
      _id = id;
    }


    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      string statement = "SELECT * FROM restaurants"
      SqlCommand cmd = new SqlCommand()
    }

  }
}
