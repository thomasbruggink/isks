using System;
using System.Linq;
using System.Security.Cryptography;
using Business.Connections;
using Business.Models;
using Cassandra;

namespace Business.Repositories
{
    public class UserRepository
    {
        private readonly CassandraConnection _cassandraConnection;
        private readonly PreparedStatement _createUser;
        private readonly PreparedStatement _getUserByName;

        public UserRepository()
        {
            const string getUserByName = "SELECT * FROM users WHERE username = :username";
            const string createUser = "INSERT INTO users (username, password, salt) VALUES (:username, :password, :salt)";

            _cassandraConnection = CassandraConnection.GetInstance();
            _getUserByName = _cassandraConnection.PreparedStatement(getUserByName);
            _createUser = _cassandraConnection.PreparedStatement(createUser);
        }

        public User GetUserByName(string username)
        {
            var result = _cassandraConnection.ExecuteReader(_getUserByName.Bind(new
            {
                username = username
            }));

            var userRow = result.GetRows().FirstOrDefault();
            if (userRow == null)
                return null;

            return new User
            {
                UserName = userRow.GetValue<string>("username")
            };
        }

        public User Login(string username, string password)
        {
            var result = _cassandraConnection.ExecuteReader(_getUserByName.Bind(new
            {
                username = username
            }));

            var userRow = result.GetRows().FirstOrDefault();
            if(userRow == null)
                return null;

            var rowPassword = Utils.FromHexString(userRow.GetValue<string>("password"));
            var rowSalt = Utils.FromHexString(userRow.GetValue<string>("salt"));

            using (var deriveBytes = new Rfc2898DeriveBytes(password, rowSalt))
            {
                var newKey = deriveBytes.GetBytes(30);
                if (!SlowEquals(newKey, rowPassword))
                    return null;
            }

            return new User
            {
                UserName = userRow.GetValue<string>("username")
            };
        }

        public User Register(string username, string password)
        {
            using (var deriveBytes = new Rfc2898DeriveBytes(password, 30))
            {
                var salt = deriveBytes.Salt;
                var key = deriveBytes.GetBytes(30);

                _cassandraConnection.ExecuteNonReader(_createUser.Bind(new
                {
                    username = username,
                    password = Utils.ToHexString(key),
                    salt = Utils.ToHexString(salt)
                }));
            }
            return new User
            {
                UserName = username
            };
        }

        private bool SlowEquals(byte[] a, byte[] b)
        {
            var diff = a.Length ^ b.Length;
            for (var i = 0; i < a.Length && i < b.Length; i++)
                diff |= a[i] ^ b[i];
            return diff == 0;
        }
    }
}
