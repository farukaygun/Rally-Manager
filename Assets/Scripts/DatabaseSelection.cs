using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Mono.Data.Sqlite;
using System.Data;

public class DatabaseSelection : MonoBehaviour
{
  private static string conString = @"Data Source=" + Global.dbPath + ";Pooling=true;FailIfMissing=false;Version=3";

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
        cmd.Parameters.Add(new SqliteParameter("@param1", "3"));

        result = cmd.ExecuteReader();

        Debug.Log("select schema: " + result.Read());
      }
      var stringResult = result.GetValue(0).ToString();

      conn.Close();

      return stringResult;
    }
  }

  public static List<Pilot> SelectFromtblTeamPilots()
  {
    using (var conn = new SqliteConnection(conString))
    {
      conn.Open();

      List<Pilot> pilots = new List<Pilot>();
      using (var cmd = conn.CreateCommand())
      {
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = "SELECT * FROM 'tblPilot' WHERE teamID = @param1";
        cmd.CommandType = CommandType.Text;
        cmd.Parameters.Add(new SqliteParameter("@param1", "3"));

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
            salary = result.GetValue(5).ToString(),
            teamID = result.GetValue(6).ToString()
          };

          pilots.Add(pilot);
        }
        Debug.Log("select schema: " + result.Read());
      }

      conn.Close();
      return pilots;
    }
  }

  public static PilotSpecs SelectSpecsFromtblTeamPilotSpecs(int id)
  {
    using (var conn = new SqliteConnection(conString))
    {
      conn.Open();

      PilotSpecs pilot;
      using (var cmd = conn.CreateCommand())
      {
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = "SELECT * FROM 'tblPilotSpecs' WHERE id = @param1";
        cmd.CommandType = CommandType.Text;
        cmd.Parameters.Add(new SqliteParameter("@param1", id));

        var result = cmd.ExecuteReader();

        pilot = new PilotSpecs
        {
          extremumSlip = int.Parse(result.GetValue(1).ToString()),
          extremumValue = int.Parse(result.GetValue(2).ToString()),
          asymptoteSlip = int.Parse(result.GetValue(3).ToString()),
          asymptoteValue = int.Parse(result.GetValue(4).ToString()),
          stiffness = int.Parse(result.GetValue(5).ToString())
        };
        Debug.Log("select schema: " + result.Read());
      }

      conn.Close();
      return pilot;
    }
  }

  public static CarSpecs SelectSpecsFromtblTeamCar(int id)
  {
    using (var conn = new SqliteConnection(conString))
    {
      conn.Open();

      CarSpecs car;
      using (var cmd = conn.CreateCommand())
      {
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = "SELECT id, name, mass, suspansionDistance, horsePower FROM 'tblCar' WHERE id = @param1";
        cmd.CommandType = CommandType.Text;
        cmd.Parameters.Add(new SqliteParameter("@param1", id));

        var result = cmd.ExecuteReader();

        car = new CarSpecs
        {
          name = result.GetValue(1).ToString(),
          mass = int.Parse(result.GetValue(2).ToString()),
          suspansionDistance = int.Parse(result.GetValue(3).ToString()),
          horsePower = int.Parse(result.GetValue(4).ToString())
        };
        Debug.Log("select schema: " + result.Read());
      }

      conn.Close();
      return car;
    }
  }

  public static List<Car> SelectFromtblTeamCars()
  {
    using (var conn = new SqliteConnection(conString))
    {
      conn.Open();

      List<Car> cars = new List<Car>();
      using (var cmd = conn.CreateCommand())
      {
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = "SELECT * FROM 'tblCar' WHERE teamID = @param1";
        cmd.CommandType = CommandType.Text;
        cmd.Parameters.Add(new SqliteParameter("@param1", "3"));

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

  // public static string SelectFixtureFromGameSettings()
  // {
  //   using (var conn = new SqliteConnection(conString))
  //   {
  //     conn.Open();

  //     SqliteDataReader result;
  //     using (var cmd = conn.CreateCommand())
  //     {
  //       cmd.CommandType = CommandType.Text;
  //       cmd.CommandText = "SELECT fixtureId FROM 'tblGameSettings'";
  //       cmd.CommandType = CommandType.Text;
  //       cmd.Parameters.Add(new SqliteParameter("@param1", "1"));

  //       result = cmd.ExecuteReader();

  //       Debug.Log("select schema: " + result.Read());
  //     }

  //     conn.Close();
  //     Debug.Log(result.GetValue(0).ToString());
  //     return result.GetValue(0).ToString();
  //   }
  // }

  public static Fixture SelectFromFixture(bool isDone)
  {
    using (var conn = new SqliteConnection(conString))
    {
      conn.Open();

      Fixture fixture;
      using (var cmd = conn.CreateCommand())
      {
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = "SELECT * FROM 'tblFixture' WHERE status = @param1 ORDER BY id asc LIMIT 1";
        cmd.CommandType = CommandType.Text;
        cmd.Parameters.Add(new SqliteParameter("@param1", isDone));

        var result = cmd.ExecuteReader();

        fixture = new Fixture
        {
          id = result.GetValue(0).ToString(),
          name = result.GetValue(1).ToString(),
          date = result.GetValue(2).ToString(),
          status = bool.Parse(result.GetValue(3).ToString())
        };
        Debug.Log("fixture: " + fixture.id + " " + fixture.name + " " + fixture.date);
        Debug.Log("select schema: " + result.Read());
      }
      conn.Close();
      
      return fixture;
    }
  }

  public static List<ScoreBoard> SelectCurrentPilotScoreBoardFromtblScoreBoard(int currentFixtureId)
  {
    using (var conn = new SqliteConnection(conString))
    {
      conn.Open();

      List<ScoreBoard> scoreBoard = new List<ScoreBoard>();
      using (var cmd = conn.CreateCommand())
      {
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = "SELECT t.name as TakýmAdý,p.name as PilotAdý,score,time FROM " + 
                          "tblScoreBoard sb inner join tblPilot p ON sb.pilotId=p.id " + 
                          "INNER JOIN tblTeam t ON sb.teamId=t.id " +
                          "WHERE fixtureId = @param1";
        cmd.CommandType = CommandType.Text;
        cmd.Parameters.Add(new SqliteParameter("param1", currentFixtureId));

        var result = cmd.ExecuteReader();

        while (result.Read())
        {
          var item = new ScoreBoard
          {
            teamName = result.GetValue(0).ToString(),
            pilotName = result.GetValue(1).ToString(),
            point = result.GetValue(2).ToString(),
            time = result.GetValue(3).ToString()
          };

          scoreBoard.Add(item);
        }

        Debug.Log("select schema: " + result.Read());
      }
      conn.Close();

      return scoreBoard;
    }
  }

  public static List<ScoreBoard> SelectCurrentTeamScoreBoardFromtblScoreBoard(int currentFixtureId)
  {
    using (var conn = new SqliteConnection(conString))
    {
      conn.Open();

      List<ScoreBoard> scoreBoard = new List<ScoreBoard>();
      using (var cmd = conn.CreateCommand())
      {
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = "SELECT t.name AS TakýmAdý, sum(score) AS ToplamPuan " +
                          "FROM tblScoreBoard sb " +
                          "INNER JOIN tblTeam t ON sb.teamId = t.id " +
                          "WHERE fixtureId = @param1  " +
                          "GROUP BY teamId " +
                          "ORDER BY ToplamPuan DESC";
        cmd.CommandType = CommandType.Text;
        cmd.Parameters.Add(new SqliteParameter("param1", currentFixtureId));

        var result = cmd.ExecuteReader();

        while (result.Read())
        {
          var item = new ScoreBoard
          {
            teamName = result.GetValue(0).ToString(),
            point = result.GetValue(1).ToString(),
          };

          scoreBoard.Add(item);
        }

        Debug.Log("select schema: " + result.Read());
      }
      conn.Close();

      return scoreBoard;
    }
  }

  public static List<ScoreBoard> SelectTeamScoreBoardFromtblScoreBoard(int id)
  {
    using (var conn = new SqliteConnection(conString))
    {
      conn.Open();

      List<ScoreBoard> scoreBoard = new List<ScoreBoard>();
      using (var cmd = conn.CreateCommand())
      {
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = "SELECT  t.name as TakýmAdý, sum(score) as ToplamPuan " +
                          "FROM tblScoreBoard sb " +
                          "INNER JOIN tblTeam t on sb.teamId=t.id " +
                          "INNER JOIN tblLeagues l ON sb.leagueId = l.id " +
                          "INNER JOIN tblSeason s ON sb.seasonId = s.id " +
                          "WHERE s.id = @param1 " +
                          "GROUP BY teamId " +
                          "ORDER BY (sum(score)) DESC";
        cmd.CommandType = CommandType.Text;
        cmd.Parameters.Add(new SqliteParameter("param1", id));

        var result = cmd.ExecuteReader();

        while (result.Read())
        {
          var item = new ScoreBoard
          {
            teamName = result.GetValue(0).ToString(),
            point = result.GetValue(1).ToString(),
          };

          scoreBoard.Add(item);
        }

        Debug.Log("select schema: " + result.Read());
      }
      conn.Close();

      return scoreBoard;
    }
  }

  public static List<ScoreBoard> SelectPilotScoreBoardFromtblScoreBoard(int id)
  {
    using (var conn = new SqliteConnection(conString))
    {
      conn.Open();

      List<ScoreBoard> scoreBoard = new List<ScoreBoard>();
      using (var cmd = conn.CreateCommand())
      {
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = "SELECT t.name, p.name as PilotAdý, sum(score) " +
                          "FROM tblScoreBoard sb " +
                          "INNER JOIN tblPilot p ON sb.pilotId=p.id " +
                          "INNER JOIN tblTeam t ON sb.teamId=t.id " +
                          "INNER JOIN tblLeagues l ON sb.leagueId = l.id " +
                          "INNER JOIN tblSeason s ON sb.seasonId = s.id " +
                          "WHERE s.id = @param1 " +
                          "GROUP BY sb.pilotId " +
                          "ORDER BY sum(score) DESC";
        cmd.CommandType = CommandType.Text;
        cmd.Parameters.Add(new SqliteParameter("param1", id));

        var result = cmd.ExecuteReader();

        while (result.Read())
        {
          var item = new ScoreBoard
          {
            teamName = result.GetValue(0).ToString(),
            pilotName = result.GetValue(1).ToString(),
            point = result.GetValue(2).ToString() 
          };

          scoreBoard.Add(item);
        }

        Debug.Log("select schema: " + result.Read());
      }
      conn.Close();

      return scoreBoard;
    }
  }


  public static League SelectFromTblLeagues()
  {
    using (var conn = new SqliteConnection(conString))
    {
      conn.Open();

      League league;
      using (var cmd = conn.CreateCommand())
      {
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = "SELECT * FROM 'tblLeagues' WHERE status = true";
        cmd.CommandType = CommandType.Text;

        var result = cmd.ExecuteReader();

        league = new League
        {
          id = result.GetValue(0).ToString(),
          name = result.GetValue(1).ToString(),
          status = result.GetValue(2).ToString()
        };

        Debug.Log("select schema: " + result.Read());
      }
      conn.Close();

      return league;
    }
  }

  public static string SelectFromTblSeason()
  {
    using (var conn = new SqliteConnection(conString))
    {
      conn.Open();

      string season;
      using (var cmd = conn.CreateCommand())
      {
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = "SELECT * FROM 'tblSeason' ORDER BY id DESC LIMIT 1";
        cmd.CommandType = CommandType.Text;

        var result = cmd.ExecuteReader();
        season = result.GetValue(0).ToString();


        Debug.Log("select schema: " + result.Read());
      }
      conn.Close();

      return season;
    }
  }
}

public struct Pilot
{
  public string id { get; set; }
  public string name { get; set; }
  public string age { get; set; }
  public string abilityPoint { get; set; }
  public string potantial { get; set; }
  public string salary { get; set; }
  public string teamID { get; set; }
}

public struct Car
{
  public string id { get; set; }
  public string name { get; set; }
  public string mass { get; set; }
  public string suspansionDistance { get; set; }
  public string horsePower { get; set; }
  public string price { get; set; }
}

public struct Fixture
{
  public string id { get; set; }
  public string name { get; set; }
  public string date { get; set; }
  public bool status { get; set; }
}

public struct PilotSpecs
{
  public float extremumSlip { get; set; }
  public float extremumValue { get; set; }
  public float asymptoteSlip { get; set; }
  public float asymptoteValue { get; set; }
  public float stiffness { get; set; }
}

public struct CarSpecs
{
  public string name { get; set; }
  public int mass { get; set; }
  public int suspansionDistance { get; set; }
  public int horsePower { get; set; }
}

public struct ScoreBoard
{
  public string teamName { get; set; }
  public string pilotName { get; set; }
  public string time { get; set; }
  public string point { get; set; }
}

public struct League
{
  public string id { get; set; }
  public string name { get; set; }
  public string status { get; set; }
}