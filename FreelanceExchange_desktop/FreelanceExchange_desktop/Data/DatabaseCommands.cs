using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;

namespace FreelanceExchange_desktop.Data
{
    static class DatabaseCommands
    {
        const string connectionString = "Server=31.128.42.195;Database=freelance_platform;Uid=userVanya;Pwd=Vanya20066002;";

        public static List<User> GetUsers()
        {
            var users = new List<User>();
            var userMap = new Dictionary<int, User>();

            MySqlConnection connection = null;
            MySqlCommand command = null;
            MySqlDataReader reader = null;

            try
            {
                connection = new MySqlConnection(connectionString);
                connection.Open();

                string query = @"
            SELECT 
                Users.id,
                Users.email,
                Users.first_name,
                Users.last_name,
                Users.username,
                Users.password,
                Users.birth_date,
                Users.registration_date,
                Roles.name AS role_name
            FROM Users
            LEFT JOIN UserRoles ON Users.id = UserRoles.user_id
            LEFT JOIN Roles ON UserRoles.role_id = Roles.id
            ORDER BY Users.id;
        ";

                command = new MySqlCommand(query, connection);
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    int id = Convert.ToInt32(reader["id"]);

                    if (!userMap.ContainsKey(id))
                    {
                        var user = new User
                        {
                            Id = id,
                            Email = reader["email"].ToString(),
                            FirstName = reader["first_name"].ToString(),
                            LastName = reader["last_name"].ToString(),
                            Username = reader["username"].ToString(),
                            Password = reader["password"].ToString(),
                            BirthDate = Convert.ToDateTime(reader["birth_date"]),
                            RegistrationDate = Convert.ToDateTime(reader["registration_date"]),
                            Roles = new List<string>()
                        };

                        userMap[id] = user;
                        users.Add(user);
                    }

                    if (reader["role_name"] != DBNull.Value)
                    {
                        userMap[id].Roles.Add(reader["role_name"].ToString());
                    }
                }
            }
            finally
            {
                if (reader != null) reader.Close();
                if (command != null) command.Dispose();
                if (connection != null) connection.Close();
            }

            return users;
        }

        public static bool IsUserUnique(User newUser)
        {
            bool isUnique = true;

            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string query = @"
            SELECT COUNT(*) 
            FROM Users 
            WHERE email = @Email OR username = @Username;
        ";

                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Email", newUser.Email);
                    command.Parameters.AddWithValue("@Username", newUser.Username);

                    long count = (long)command.ExecuteScalar();
                    isUnique = count == 0;
                }
            }

            return isUnique;
        }

        public static bool AddUser(User user, string roleName)
        {

            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                MySqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    // 1. Добавляем пользователя
                    string insertUserQuery = @"
                INSERT INTO Users 
                (email, first_name, last_name, username, password, birth_date, registration_date)
                VALUES 
                (@Email, @FirstName, @LastName, @Username, @Password, @BirthDate, @RegistrationDate);
                SELECT LAST_INSERT_ID();
            ";

                    int userId;
                    using (var command = new MySqlCommand(insertUserQuery, connection, transaction))
                    {
                        command.Parameters.AddWithValue("@Email", user.Email);
                        command.Parameters.AddWithValue("@FirstName", user.FirstName);
                        command.Parameters.AddWithValue("@LastName", user.LastName);
                        command.Parameters.AddWithValue("@Username", user.Username);
                        command.Parameters.AddWithValue("@Password", user.Password);
                        command.Parameters.AddWithValue("@BirthDate", user.BirthDate);
                        command.Parameters.AddWithValue("@RegistrationDate", DateTime.Now);

                        userId = Convert.ToInt32(command.ExecuteScalar());
                    }

                    string getRoleIdQuery = "SELECT id FROM Roles WHERE name = @RoleName";
                    int roleId;

                    using (var command = new MySqlCommand(getRoleIdQuery, connection, transaction))
                    {
                        command.Parameters.AddWithValue("@RoleName", roleName);
                        object result = command.ExecuteScalar();

                        if (result == null)
                            throw new Exception($"Роль '{roleName}' не найдена в таблице Roles.");

                        roleId = Convert.ToInt32(result);
                    }

                    string insertUserRoleQuery = "INSERT INTO UserRoles (user_id, role_id) VALUES (@UserId, @RoleId)";
                    using (var command = new MySqlCommand(insertUserRoleQuery, connection, transaction))
                    {
                        command.Parameters.AddWithValue("@UserId", userId);
                        command.Parameters.AddWithValue("@RoleId", roleId);
                        command.ExecuteNonQuery();
                    }

                    transaction.Commit();
                    return true;
                }
                catch
                {
                    transaction.Rollback();
                    return false;
                }
            }
        }
        public static List<Task> LoadTasksFromDb()
        {
            List<Task> Tasks = new List<Task>();
            MySql.Data.MySqlClient.MySqlConnection conn = new MySql.Data.MySqlClient.MySqlConnection(connectionString);
            conn.Open();

            string query = @"
            SELECT id, title, description, creator_id, created_at, budget, status_id, image_data
            FROM Tasks";

            // Создаём команду
            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(query, conn);
            MySql.Data.MySqlClient.MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Task task = new Task
                {
                    Id = reader.GetInt32(reader.GetOrdinal("id")),
                    Title = reader.IsDBNull(reader.GetOrdinal("title")) ? "" : reader.GetString(reader.GetOrdinal("title")),
                    Description = reader.IsDBNull(reader.GetOrdinal("description")) ? "" : reader.GetString(reader.GetOrdinal("description")),
                    CreatorId = reader.GetInt32(reader.GetOrdinal("creator_id")),
                    CreatedAt = reader.GetDateTime(reader.GetOrdinal("created_at")),
                    Budget = reader.GetDecimal(reader.GetOrdinal("budget")),
                    StatusId = reader.GetInt32(reader.GetOrdinal("status_id"))
                    //ImageData = reader.IsDBNull(reader.GetOrdinal("image_data")) ? null : (byte[])reader["image_data"]
                };

                Tasks.Add(task);
            }

            reader.Close();
            cmd.Dispose();
            conn.Close();
            conn.Dispose();

            return Tasks;
        }

        public static void AddTaskToDatabase(Task task)
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string query = @"
            INSERT INTO Tasks (title, description, creator_id, created_at, budget, status_id)
            VALUES (@Title, @Description, @CreatorId, @CreatedAt, @Budget, @StatusId)";

                using (var cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@Title", task.Title);
                    cmd.Parameters.AddWithValue("@Description", task.Description);
                    cmd.Parameters.AddWithValue("@CreatorId", task.CreatorId);
                    cmd.Parameters.AddWithValue("@CreatedAt", task.CreatedAt);
                    cmd.Parameters.AddWithValue("@Budget", task.Budget);
                    cmd.Parameters.AddWithValue("@StatusId", task.StatusId);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
