using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Mono.Data.Sqlite;
using System.Data;

public class DatabaseSelection : MonoBehaviour
{
  private static string conString = @"Data Source=C:\Users\Faruk\AppData\LocalLow\DefaultCompany\Rally Manager\rallyManagerData.db;Pooling=true;FailIfMissing=false;Version=3";

  public static List<Pilot> SelectFromtblPilot()
  {
    using (var conn = new SqliteConnection(conString))
    {
      conn.Open();

      List<Pilot> pilots = new List<Pilot>();   
      using (var cmd = conn.CreateCommand())
      {
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = "SELECT * FROM 'tblPilot' WHERE teamID IS @param1";
        cmd.CommandType = CommandType.Text;
        cmd.Parameters.Add(new SqliteParameter("@param1", null));

        var result = cmd.ExecuteReader();

        while (result.Read())
        {
          var pilot = new Pilot 
          { 
            id = result.GetValue(0).ToString(),
            name = result.GetValue(1).ToString(),
            age = result.GetValue(2).ToString(),
            abilityPoint = result.GetValue(3).ToString(),
            potantial = result.GetValue(4).ToString(),
            salary = result.GetValue(5).ToString()
          };

          pilots.Add(pilot);
        }
        Debug.Log("select schema: " + result.Read());
      }
      
      conn.Close();
      return pilots;
    }
  }

  public static List<Car> SelectFromtblCar()
  {
    using (var conn = new SqliteConnection(conString))
    {
      conn.Open();

      List<Car> cars = new List<Car>();   
      using (var cmd = conn.CreateCommand())
      {
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = "SELECT * FROM 'tblCar' WHERE teamID IS @param1";
        cmd.CommandType = CommandType.Text;
        cmd.Parameters.Add(new SqliteParameter("@param1", null));

        var result = cmd.ExecuteReader();

        while (result.Read())
        {
          var car = new Car
          { 
            id = result.GetValue(0).ToString(),
            name = result.GetValue(1).ToString(),
            mass = result.GetValue(2).ToString(),
            suspansionDistance = result.GetValue(3).ToString(),
            horsePower = result.GetValue(4).ToString(),
            price = result.GetValue(5).ToString()
          };

          cars.Add(car);
        }
        Debug.Log("select schema: " + result.Read());
      }
      
      conn.Close();
      return cars;
    }
  }

  public static string SelectBudgetFromtblTeam()
  {
    using (var conn = new SqliteConnection(conString))
    {
      conn.Open();

      SqliteDataReader result;
      using (var cmd = conn.CreateCommand())
      {
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = "SELECT budget FROM 'tblTeam' WHERE id = @param1";
        cmd.CommandType = CommandType.Text;
        cmd.Parameters.Add(new SqliteParameter("@param1", 1));

        result = cmd.ExecuteReader();

        Debug.Log("select schema: " + result.Read());
      }
      var stringResult = result.GetValue(0).ToString();

      conn.Close();
      
      return stringResult;
    }
  }
}

public class Pilot 
{
  public string id { get; set; }
  public string name { get; set; }
  public string age { get; set; }
  public string abilityPoint { get; set; }
  public string potantial { get; set; }
  public string salary { get; set; }
}

public class Car 
{
  public string id { get; set; }
  public string name { get; set; }
  public string mass { get; set; }
  public string suspansionDistance { get; set; }
  public string horsePower { get; set; }
  public string price { get; set; }
}