using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using BestRestaurants;

namespace BestRestaurants.Models
{
  public class Restaurant
  {
    public int Id {get; set;}
    public string Name {get; set;}
    public int Rating {get; set;}
    public int Cuisine_Id {get; set;}

    public Restaurant(string name, int rating, int cuisine_id, int id = 0)
    {
      Name = name;
      Rating = rating;
      Cuisine_Id = cuisine_id;
      Id = id;
    }
    public override bool Equals(System.Object otherRestaurant)
    {
      if (!(otherRestaurant is Restaurant))
      {
        return false;
      }
      else
      {
        Restaurant newRestaurant = (Restaurant) otherRestaurant;
        bool idEquality = (this.Id == newRestaurant.Id);
        bool nameEquality = (this.Name == newRestaurant.Name);
        bool ratingEquality = (this.Rating == newRestaurant.Rating);
        bool cuisine_idEquality = (this.Cuisine_Id == newRestaurant.Cuisine_Id);
        return (nameEquality && idEquality && ratingEquality && cuisine_idEquality);
      }
    }
    public override int GetHashCode()
    {
      return this.Name.GetHashCode();
    }

    public static List<Restaurant> GetAll()
    {
      List<Restaurant> allRestaurants = new List<Restaurant>{};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM restaurants;";
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int id = rdr.GetInt32(0);
        string name = rdr.GetString(1);
        int rating = rdr.GetInt32(2);
        int cuisine_id = rdr.GetInt32(3);
        Restaurant newRestaurant = new Restaurant(name,rating,cuisine_id,id);
        allRestaurants.Add(newRestaurant);
      }
      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
      return allRestaurants;
    }

    public static Restaurant Find(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM restaurants WHERE id=@searchId;";

      MySqlParameter parameterId = new MySqlParameter();
      parameterId.ParameterName = "@searchId";
      parameterId.Value = id;
      cmd.Parameters.Add(parameterId);

      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      Restaurant foundRestaurant = new Restaurant("", 0, 0, 0);
      if (rdr.Read())
      {
        string name = rdr.GetString(1);
        int rating = rdr.GetInt32(2);
        int cuisine_id = rdr.GetInt32(3);

        foundRestaurant = new Restaurant(name, rating, cuisine_id, id);
      }
      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
      return foundRestaurant;
    }

    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO restaurants (name, rating, cuisine_id) VALUES (@name, @rating, @cuisine_id);";

      MySqlParameter name = new MySqlParameter();
      name.ParameterName = "@name";
      name.Value = this.Name;
      cmd.Parameters.Add(name);

      MySqlParameter rating = new MySqlParameter();
      rating.ParameterName = "@rating";
      rating.Value = this.Rating;
      cmd.Parameters.Add(rating);

      MySqlParameter cuisine_id = new MySqlParameter();
      cuisine_id.ParameterName = "@cuisine_id";
      cuisine_id.Value = this.Cuisine_Id;
      cmd.Parameters.Add(cuisine_id);

      cmd.ExecuteNonQuery();
      this.Id = (int) cmd.LastInsertedId;

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public void Edit(string newRestaurant, int newRating)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"UPDATE restaurants SET name = @newRestaurant, rating = @newRating WHERE id = @searchId;";

      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@searchId";
      searchId.Value = this.Id;
      cmd.Parameters.Add(searchId);

      MySqlParameter name = new MySqlParameter();
      name.ParameterName = "@newRestaurant";
      name.Value = newRestaurant;
      cmd.Parameters.Add(name);

      MySqlParameter rating = new MySqlParameter();
      rating.ParameterName = "@newRating";
      rating.Value = newRating;
      cmd.Parameters.Add(rating);

      this.Name = newRestaurant;
      this.Rating = newRating;
      
      cmd.ExecuteNonQuery();

      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
    }

    public static void DeleteRestaurant(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM restaurants WHERE id = @searchId;";

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
      cmd.CommandText = @"DELETE FROM restaurants;";

      cmd.ExecuteNonQuery();

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

  }
}
