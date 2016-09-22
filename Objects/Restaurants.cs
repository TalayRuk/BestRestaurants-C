using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace BestRestaurants
{
  public class Restaurant
  {
    private string _specialty;
    private int _id;

    public Restaurant(string specialty, int id = 0)
    {
      _specialty = specialty;
      _id = id;
    }
    //Getter & setter
    public string GetSpecialty()
    {
      return _specialty;
    }
    public int GetId()
    {
      return _id;
    }
    public void SetSpecialty(string newSpecialty)
    {
      _specialty = newSpecialty;
    }
    public void SetId(int id)
    {
      _id = id;
    }

    //override
    public override bool Equals(System.Object otherRestaurant)
    {
      if(!(otherRestaurant is Restaurant))
      {
        return false;
      }
      else
      {
        Restaurant newRestaurant = (Restaurant) otherRestaurant;
        bool idE
      }
    }

    //GetAll
    public static List<Restaurant> GetAll()
    {
      List<Restaurant> allRestaurants = new List<Restaurant> {};

      SqlConnection conn = DB.Connection();
      conn.Open();

      string statement = "SELECT * FROM restaurants;";
      SqlCommand cmd = new SqlCommand(statement, conn);
      SqlDataReader rdr = cmd.ExecuteReader();
      //read
      while (rdr.Read())
      {
        int restaurantId = rdr.GetInt32(0);
        string restaurantSpecialty = rdr.GetString(1);
        Restaurant newRestaurant = new Restaurant(restaurantSpecialty, restaurantId);
        allRestaurants.Add(newRestaurant);
      }
      //close
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return allRestaurants;
    }

    //DeleteAll
    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      string statement = "DELETE FROM restaurants;";
      SqlCommand cmd = new SqlCommand(statement, conn);
      cmd.ExecuteNonQuery();
      conn.Close();
    }

  }
}
