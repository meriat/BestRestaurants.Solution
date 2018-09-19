using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using BestRestaurants;

namespace BestRestaurants.Models
{
  public class Cuisine
  {
    public int Id {get; set;}
    public string Name {get; set;}

    public Cuisine(string name, int id = 0)
    {
      Name = name;
      Id = id;
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
        bool idEquality = (this.Id == newCuisine.Id);
        bool nameEquality = (this.Name == newCuisine.Name);
        return (nameEquality && idEquality);
      }
    }

    public override int GetHashCode()
    {
      return this.Name.GetHashCode();
    }

    public static List<Cuisine> GetAll()
    {
      List<Cuisine> allCuisines = new List<Cuisine>{};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM cuisines;";
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int id = rdr.GetInt32(0);
        string name = rdr.GetString(1);
        Cuisine newCuisine = new Cuisine(name,id);
        allCuisines.Add(newCuisine);
      }
      conn.Close();
      if (conn !=null)
      {
        conn.Dispose();
      }
      return allCuisines;
    }

    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO cuisines (name) VALUES (@name);";

      MySqlParameter name = new MySqlParameter();
      name.ParameterName = "@name";
      name.Value = this.Name;
      cmd.Parameters.Add(name);

      cmd.ExecuteNonQuery();
      Id = (int) cmd.LastInsertedId;

      conn.Close();
      if (conn != null)
      {
          conn.Dispose();
      }
    }

    public static Cuisine Find(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM cuisines WHERE id=@thisId;";

      MySqlParameter thisId = new MySqlParameter();
      thisId.ParameterName = "@thisId";
      thisId.Value = id;
      cmd.Parameters.Add(thisId);

      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;

      int cuisineId = 0;
      string cuisineName = "";

      while (rdr.Read())
      {
        cuisineId = rdr.GetInt32(0);
        cuisineName = rdr.GetString(1);
      }

      Cuisine foundCuisine = new Cuisine(cuisineName, cuisineId);

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return foundCuisine;
    }

    public void Edit(string newCuisine)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"UPDATE cuisines SET name = @newCuisine WHERE id = @searchId;";

      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@searchId";
      searchId.Value = this.Id;
      cmd.Parameters.Add(searchId);

      MySqlParameter name = new MySqlParameter();
      name.ParameterName = "@newCuisine";
      name.Value = newCuisine;
      cmd.Parameters.Add(name);

      this.Name = newCuisine;
      cmd.ExecuteNonQuery();

      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
    }

    public static void DeleteCuisine(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM cuisines WHERE id = @searchId;";

      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@searchId";
      searchId.Value = id;
      cmd.Parameters.Add(searchId);

      cmd.ExecuteNonQuery();

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public static void DeleteAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM cuisines;";

      cmd.ExecuteNonQuery();

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

  }
}
