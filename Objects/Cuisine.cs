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
        string CuisineName = rdr.GetString(0);
        int CuisineId = rdr.GetInt32(1);
        Cuisine newCuisine = new Cuisine(CuisineName, CuisineId);
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

      string statement = "INSERT INTO cuisine (name) OUTPUT INSERTED.id VALUES (@CuisineName);";
      SqlCommand cmd = new SqlCommand (statement, conn);

      SqlParameter nameParameter = new SqlParameter();
      nameParameter.ParameterName = "@CuisineName";
      nameParameter.Value = this.GetName();
      cmd.Parameters.Add(nameParameter);
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
      SqlParameter CuisineIdParameter = new SqlParameter();
      CuisineIdParameter.ParameterName = "@CuisineId";
      CuisineIdParameter.Value = id.ToString();
      cmd.Parameters.Add(CuisineIdParameter);
      SqlDataReader rdr = cmd.ExecuteReader();

      int foundCuisineId = 0;
      string foundCuisineName = null;

      while(rdr.Read() )
      {
        foundCuisineId = rdr.GetInt32(1);
        foundCuisineName = rdr.GetString(0);
      }
      Cuisine foundCuisine = new Cuisine(foundCuisineName, foundCuisineId);

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
        return (idEquality && nameEquality);
      }
    }
    public override int GetHashCode()
    {
      return this.GetName().GetHashCode();
    }
  }
}
