using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace BestRestaurants
{
  public class Cuisine
  {
    private string _name;
    private int _id;
    private int _restaurantId;
    //add restaurantId
    public Cuisine(string name, int RestaurantId = 0, int id = 0 )
    {
      _name = name;
      _id = id;
      _restaurantId = RestaurantId;

    }
    public string GetName()
    {
      return _name;
    }
    public int GetId()
    {
      return _id;
    }
    //add restaurantId
    public int GetRestaurantId()
    {
      return _restaurantId;
    }
    public void SetName(string name)
    {
      _name = name;
    }
    public void SetId(int id)
    {
      _id = id;
    }
    //add restaurantId
    public void SetRestaurantId(int newRestaurantId)
    {
      _restaurantId = newRestaurantId;
    }

    //GetAll
    public static List<Cuisine> GetAll()
    {
      List<Cuisine> allCuisine = new List<Cuisine> {};

      SqlConnection conn = DB.Connection();
      conn.Open();

      string statement = "SELECT * FROM cuisine;";
      SqlCommand cmd = new SqlCommand(statement, conn);
      SqlDataReader rdr = cmd.ExecuteReader();

      while (rdr.Read())
      {
        int CuisineId = rdr.GetInt32(0);
        string CuisineName = rdr.GetString(1);
        //add restaurantId
        int CuisineRestaurantId = rdr.GetInt32(2);
        //add restaurantId
        Cuisine newCuisine = new Cuisine(CuisineName, CuisineRestaurantId, CuisineId);
        allCuisine.Add(newCuisine);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }

      return allCuisine;
    }

    public void Save()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      //add restaurantId
      string statement = "INSERT INTO cuisine (name, restaurants_id) OUTPUT INSERTED.id VALUES (@CuisineName, @CuisineRestaurantId);";
      SqlCommand cmd = new SqlCommand (statement, conn);

      SqlParameter nameParameter = new SqlParameter();
      nameParameter.ParameterName = "@CuisineName";
      nameParameter.Value = this.GetName();
      //add restaurantId
      SqlParameter restaurantIdParameter = new SqlParameter();
      restaurantIdParameter.ParameterName = "@CuisineRestaurantId";
      restaurantIdParameter.Value = this.GetRestaurantId();

      cmd.Parameters.Add(nameParameter);
      //add restaurantId
      cmd.Parameters.Add(restaurantIdParameter);

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

    public static Cuisine Find(int id)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      string statement = "SELECT * FROM cuisine WHERE id = @CuisineId;";
      SqlCommand cmd = new SqlCommand(statement, conn);
      SqlParameter cuisineIdParameter = new SqlParameter();
      cuisineIdParameter.ParameterName = "@CuisineId";
      cuisineIdParameter.Value = id.ToString();
      cmd.Parameters.Add(cuisineIdParameter);
      SqlDataReader rdr = cmd.ExecuteReader();

      int foundCuisineId = 0;
      string foundCuisineName = null;
      //add restaurantId
      int foundCuisineRestaurantId = 0;

      while(rdr.Read() )
      {
        foundCuisineId = rdr.GetInt32(1);
        foundCuisineName = rdr.GetString(0);
      }
      //add restaurantId
      Cuisine foundCuisine = new Cuisine(foundCuisineName, foundCuisineRestaurantId, foundCuisineId);

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return foundCuisine;
    }

    public void DeleteOne()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("DELETE FROM cuisine WHERE id = @CuisineId;", conn);
      SqlParameter CuisineIdParameter = new SqlParameter();
      CuisineIdParameter.ParameterName = "@CuisineId";
      CuisineIdParameter.Value = this.GetId();
      cmd.Parameters.Add(CuisineIdParameter);
      cmd.ExecuteNonQuery();

      if (conn != null)
      {
        conn.Close();
      }
    }

    public static void DeleteAll()
    {
    SqlConnection conn = DB.Connection();
    conn.Open();
    SqlCommand cmd = new SqlCommand("DELETE FROM cuisine;", conn);
    cmd.ExecuteNonQuery();
    conn.Close();
    }

    public override bool Equals(System.Object otherCuisine)
    {
      if (!(otherCuisine is Cuisine))
      {
        return false;
      }
      else
      {
        Cuisine newCuisine = (Cuisine) otherCuisine;
        bool idEquality = (this.GetId() == newCuisine.GetId());
        bool nameEquality = (this.GetName() == newCuisine.GetName());
        bool restaurantEquality = this.GetRestaurantId() == newCuisine.GetRestaurantId();
        return (idEquality && nameEquality && restaurantEquality);
      }
    }
    public override int GetHashCode()
    {
      return this.GetName().GetHashCode();
    }
  }
}
