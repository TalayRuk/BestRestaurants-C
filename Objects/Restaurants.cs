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
        bool idEquality = (this).GetId() == newRestaurant.GetId();
        bool specialtyEquality = (this).GetSpecialty() == newRestaurant.GetSpecialty();
        return (idEquality && specialtyEquality);
      }
    }
    public override int GetHashCode()
    {
      return this.GetSpecialty().GetHashCode();
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
    //save
    public void Save()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      string statement = "INSERT INTO restaurants (specialty) OUTPUT INSERTED.id VALUES (@RestaurantSpecial);";
      SqlCommand cmd = new SqlCommand (statement, conn);

      SqlParameter specialtyParameter = new SqlParameter();
      specialtyParameter.ParameterName = "@RestaurantSpecial";
      specialtyParameter.Value = this.GetSpecialty();
      cmd.Parameters.Add(specialtyParameter);
      SqlDataReader rdr = cmd.ExecuteReader();

      while (rdr.Read() )
      {
        this._id = rdr.GetInt32(0);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
    }
    //Find
    public static Restaurant Find(int id)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      string statement = "SELECT * FROM restaurants WHERE id =@restaurantId;";
      SqlCommand cmd = new SqlCommand(statement, conn);
      SqlParameter restaurantIdParameter = new SqlParameter();
      restaurantIdParameter.ParameterName = "restaurantId";
      restaurantIdParameter.Value = id.ToString();
      cmd.Parameters.Add(restaurantIdParameter);
      SqlDataReader rdr = cmd.ExecuteReader();

      int foundRestaurantId = 0;
      string foundRestaurantSpecial = null;

      while (rdr.Read())
      {
        foundRestaurantId = rdr.GetInt32(0);
        foundRestaurantSpecial = rdr.GetString(1);
      }
      Restaurant foundRestaurant = new Restaurant(foundRestaurantSpecial, foundRestaurantId);

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return foundRestaurant;
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
