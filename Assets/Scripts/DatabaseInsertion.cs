using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Mono.Data.Sqlite;
using System.Data;

public class DatabaseInsertion : MonoBehaviour
{
  private static string conString = @"Data Source=" + Global.dbPath + ";Pooling=true;FailIfMissing=false;Version=3";

  public static void InsertTotblManager(string name, string surname, int age)
  {
    using (var conn = new SqliteConnection(conString))
    {
      conn.Open();

      using (var cmd = conn.CreateCommand())
      {
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = "INSERT INTO 'tblManager' ('name', 'surname', 'age') VALUES " +
                                                    "(@param1, @param2, @param3)";
        cmd.CommandType = CommandType.Text;
        cmd.Parameters.Add(new SqliteParameter("@param1", name));
        cmd.Parameters.Add(new SqliteParameter("@param2", surname));
        cmd.Parameters.Add(new SqliteParameter("@param3", age));

        var result = cmd.ExecuteReader();

        Debug.Log("insert schema: " + result);
      }

      conn.Close();
    }
  }

  public static void InsertTotblManagerSkills(int avaliableSkillPoints, int personManagement, int motivation, int adaptation, int decisiveness)
  {
    using (var conn = new SqliteConnection(conString))
    {
      conn.Open();

      using (var cmd = conn.CreateCommand())
      {
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = "INSERT INTO 'tblManagerSkills' (avaliableSkillPoints, personManagement, motivation, adaptation, decisiveness) VALUES " +
                                                          "(@param1, @param2, @param3, @param4, @param5)";

        cmd.CommandType = CommandType.Text;
        cmd.Parameters.Add(new SqliteParameter("@param1", avaliableSkillPoints));
        cmd.Parameters.Add(new SqliteParameter("@param2", personManagement));
        cmd.Parameters.Add(new SqliteParameter("@param3", motivation));
        cmd.Parameters.Add(new SqliteParameter("@param4", adaptation));
        cmd.Parameters.Add(new SqliteParameter("@param5", decisiveness));

        var result = cmd.ExecuteReader();

        Debug.Log("insert schema: " + result);
      }

      conn.Close();
    }
  }

  public static void InsertTotblTeam(string name, string avatar, string logo, int budget)
  {
    using (var conn = new SqliteConnection(conString))
    {
      conn.Open();

      using (var cmd = conn.CreateCommand())
      {
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = "INSERT INTO 'tblTeam' (name, avatar, logo, budget) VALUES " +
                                                          "(@param1, @param2, @param3, @param4)";

        cmd.CommandType = CommandType.Text;
        cmd.Parameters.Add(new SqliteParameter("@param1", name));
        cmd.Parameters.Add(new SqliteParameter("@param2", null));
        cmd.Parameters.Add(new SqliteParameter("@param3", null));
        cmd.Parameters.Add(new SqliteParameter("@param4", budget));

        var result = cmd.ExecuteReader();

        Debug.Log("insert schema: " + result);
      }

      conn.Close();
    }
  }

  public static void InsertTotblGameSettings(string diffuculty)
  {
    using (var conn = new SqliteConnection(conString))
    {
      conn.Open();

      using (var cmd = conn.CreateCommand())
      {
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = "INSERT INTO 'tblGameSettings' (gameDiffuculty) VALUES " +
                                                          "(@param1)";

        cmd.CommandType = CommandType.Text;
        cmd.Parameters.Add(new SqliteParameter("@param1", diffuculty));

        var result = cmd.ExecuteReader();

        Debug.Log("insert schema: " + result);
      }

      conn.Close();
    }
  }

  public static void InsertPilotsTotblPilots(string name, int age, int abilityPoint, int potantial, int salary, string teamID)
  {
    using (var conn = new SqliteConnection(conString))
    {
      conn.Open();

      using (var cmd = conn.CreateCommand())
      {
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = "INSERT INTO 'tblPilot' (name, age, abilityPoint, potantial, salary, teamID) VALUES " +
                                                          "(@param1, @param2, @param3, @param4, @param5, @param6)";

        cmd.CommandType = CommandType.Text;
        cmd.Parameters.Add(new SqliteParameter("@param1", name));
        cmd.Parameters.Add(new SqliteParameter("@param2", age));
        cmd.Parameters.Add(new SqliteParameter("@param3", abilityPoint));
        cmd.Parameters.Add(new SqliteParameter("@param4", potantial));
        cmd.Parameters.Add(new SqliteParameter("@param5", salary));
        cmd.Parameters.Add(new SqliteParameter("@param6", teamID));

        var result = cmd.ExecuteReader();

        Debug.Log("insert schema: " + result);
      }

      conn.Close();
    }
  }

  public static void InsertSpecsTotblPilotSpecs(int extremumSlip, int extremumValue, int asymptoteSlip, int asymptoteValue, int stiffness, int pilotId)
  {
    using (var conn = new SqliteConnection(conString))
    {
      conn.Open();

      using (var cmd = conn.CreateCommand())
      {
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = "INSERT INTO 'tblPilotSpecs' (extremumSlip, extremumValue, asymptoteSlip, asymptoteValue, stiffness, pilotId) VALUES " +
                                                          "(@param1, @param2, @param3, @param4, @param5, @param6)";

        cmd.CommandType = CommandType.Text;
        cmd.Parameters.Add(new SqliteParameter("@param1", extremumSlip));
        cmd.Parameters.Add(new SqliteParameter("@param2", extremumValue));
        cmd.Parameters.Add(new SqliteParameter("@param3", asymptoteSlip));
        cmd.Parameters.Add(new SqliteParameter("@param4", asymptoteValue));
        cmd.Parameters.Add(new SqliteParameter("@param5", stiffness));
        cmd.Parameters.Add(new SqliteParameter("@param6", pilotId));

        var result = cmd.ExecuteReader();

        Debug.Log("insert schema: " + result);
      }

      conn.Close();
    }
  }

  public static void InsertSpecsTotblPilotSpecs(int mass, int suspansionDistance, int horsePower, int carId)
  {
    using (var conn = new SqliteConnection(conString))
    {
      conn.Open();

      using (var cmd = conn.CreateCommand())
      {
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = "INSERT INTO 'tblPilotSpecs' (mass, suspansionDistance, horsePower, carId) VALUES " +
                                                          "(@param1, @param2, @param3, @param4)";

        cmd.CommandType = CommandType.Text;
        cmd.Parameters.Add(new SqliteParameter("@param1", mass));
        cmd.Parameters.Add(new SqliteParameter("@param2", suspansionDistance));
        cmd.Parameters.Add(new SqliteParameter("@param3", horsePower));
        cmd.Parameters.Add(new SqliteParameter("@param4", carId));

        var result = cmd.ExecuteReader();

        Debug.Log("insert schema: " + result);
      }

      conn.Close();
    }
  }

  public static void InsertCarsTotblCar(string name, int mass, int suspansionDistance, int horsePower, int price, string teamID)
  {
    using (var conn = new SqliteConnection(conString))
    {
      conn.Open();

      using (var cmd = conn.CreateCommand())
      {
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = "INSERT INTO 'tblCar' (name, mass, suspansionDistance, horsePower, price, teamID) VALUES " +
                                                          "(@param1, @param2, @param3, @param4, @param5, @param6)";

        cmd.CommandType = CommandType.Text;
        cmd.Parameters.Add(new SqliteParameter("@param1", name));
        cmd.Parameters.Add(new SqliteParameter("@param2", mass));
        cmd.Parameters.Add(new SqliteParameter("@param3", suspansionDistance));
        cmd.Parameters.Add(new SqliteParameter("@param4", horsePower));
        cmd.Parameters.Add(new SqliteParameter("@param5", price));
        cmd.Parameters.Add(new SqliteParameter("@param6", teamID));

        var result = cmd.ExecuteReader();

        Debug.Log("insert schema: " + result);
      }

      conn.Close();
    }
  }

  public static void InsertTotblFixture(string name, string date, bool status)
  {
    using (var conn = new SqliteConnection(conString))
    {
      conn.Open();

      using (var cmd = conn.CreateCommand())
      {
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = "INSERT INTO 'tblFixture' (name, date, status) VALUES " +
                                                          "(@param1, @param2, @param3)";

        cmd.CommandType = CommandType.Text;
        cmd.Parameters.Add(new SqliteParameter("@param1", name));
        cmd.Parameters.Add(new SqliteParameter("@param2", date));
        cmd.Parameters.Add(new SqliteParameter("@param3", status));

        var result = cmd.ExecuteReader();

        Debug.Log("insert schema: " + result);
      }

      conn.Close();
    }
  }

  public static void InsertTotblScoreBoard(int score, string time, int pilotId, int teamId, int fixtureId, int leagueId, int seasonId)
  {
    using (var conn = new SqliteConnection(conString))
    {
      conn.Open();

      using (var cmd = conn.CreateCommand())
      {
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = "INSERT INTO 'tblScoreBoard' (score, time, pilotId, teamId, fixtureId, leagueId, seasonId) VALUES " +
                                                          "(@param1, @param2, @param3, @param4, @param5, @param6, @param7)";

        cmd.CommandType = CommandType.Text;
        cmd.Parameters.Add(new SqliteParameter("@param1", score));
        cmd.Parameters.Add(new SqliteParameter("@param2", time));
        cmd.Parameters.Add(new SqliteParameter("@param3", pilotId));
        cmd.Parameters.Add(new SqliteParameter("@param4", teamId));
        cmd.Parameters.Add(new SqliteParameter("@param5", fixtureId));
        cmd.Parameters.Add(new SqliteParameter("@param6", leagueId));
        cmd.Parameters.Add(new SqliteParameter("@param7", seasonId));


        var result = cmd.ExecuteReader();

        Debug.Log("insert schema: " + result);
      }

      conn.Close();
    }
  }

  public static void InsertTotblLeagues(string name, bool status)
  {
    using (var conn = new SqliteConnection(conString))
    {
      conn.Open();

      using (var cmd = conn.CreateCommand())
      {
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = "INSERT INTO 'tblLeagues' (name, status) VALUES " +
                                                    "(@param1, @param2)";

        cmd.CommandType = CommandType.Text;
        cmd.Parameters.Add(new SqliteParameter("@param1", name));
        cmd.Parameters.Add(new SqliteParameter("@param2", status));

        var result = cmd.ExecuteReader();

        Debug.Log("insert schema: " + result);
      }

      conn.Close();
    }
  }

  public static void InsertTotblSeason(string name)
  {
    using (var conn = new SqliteConnection(conString))
    {
      conn.Open();

      using (var cmd = conn.CreateCommand())
      {
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = "INSERT INTO 'tblSeason' (name) VALUES (@param1)";

        cmd.CommandType = CommandType.Text;
        cmd.Parameters.Add(new SqliteParameter("@param1", name));

        var result = cmd.ExecuteReader();

        Debug.Log("insert schema: " + result);
      }

      conn.Close();
    }
  }
}
