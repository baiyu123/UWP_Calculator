using System;
using System.Diagnostics;
using System.Data;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Text;

namespace CalculatorUWP
{
    internal class Connector
    {
        MySqlConnection conn;
        public Connector()
        {
            string connStr = "server=localhost;user=root;database=calculatordb;port=3306;password=baiyusql;SslMode=None;";
            conn = new MySqlConnection(connStr);
        }

        public string LoginUser(string username, string password) {
            string hashPass = GetPassword(username);
            if (hashPass == "") return "User does not exist.";
            if (hashPass != Hash(password)) return "Invalid password.";
            return "Success";
        }

        private string GetPassword(string username) {
            string result ="";
            try
            {
                Debug.WriteLine("Connecting to MySQL...");
                conn.Open();
                string sql = "SELECT Password FROM users WHERE UserName = '" + username + "'";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();
                
                while (rdr.Read())
                {
                    result = rdr.GetString(0);
                }
                rdr.Close();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                Debug.WriteLine("we have exception");
            }
            conn.Close();
            Debug.WriteLine("Done.");
            return result;
        }

        private bool checkUserExist(string username) {
            if (GetPassword(username) != "") {
                return true;
            }
            return false;
        }

        public string RegisterNewUser(string username, string password) {
            if (checkUserExist(username)) {
                return "Username already Registered.";
            }
            string hashPassword = Hash(password);
            try
            {
                conn.Open();
                string sql = "INSERT INTO Users VALUES ('" + username + "','" + hashPassword + "')";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex) {
                Debug.WriteLine(ex.ToString());
                Debug.WriteLine("Adding new user fail");
            }
            conn.Close();
            return "Successfully registered.";
        }

        public void InsertAnOperation(string username, string operation) {
             try
            {
                conn.Open();
                string sql = "INSERT INTO History VALUES ('" + username + "','" + operation + "')";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                Debug.WriteLine("Adding new operation failed.");
            }
            conn.Close();
        }

        public string Hash(string password)
        {
            var bytes = new UTF8Encoding().GetBytes(password);
            var hashBytes = System.Security.Cryptography.MD5.Create().ComputeHash(bytes);
            return Convert.ToBase64String(hashBytes);
        }
    }
}