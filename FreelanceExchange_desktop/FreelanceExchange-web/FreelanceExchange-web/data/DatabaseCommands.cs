﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace FreelanceExchange_desktop.Data
{
    static class DatabaseCommands
    {
        const string connectionString = "Server=31.128.42.195;Database=freelance_platform;Uid=userVanya;Pwd=Vanya20066002;";

        public static ObservableCollection<User> GetUsers()
        {
            var users = new ObservableCollection<User>();
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
                            Roles = new ObservableCollection<string>()
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
        }
        public static ObservableCollection<Task> LoadTasksFromDb()
        {
            ObservableCollection<Task> Tasks = new ObservableCollection<Task>();
            using (var conn = new MySql.Data.MySqlClient.MySqlConnection(connectionString))
            {
                conn.Open();

                string query = @"
    SELECT 
        t.id AS task_id, t.title, t.description, t.creator_id, 
        t.created_at, t.budget, t.status_id, t.pic,
        r.id AS response_id, r.freelancer_id, r.message, 
        r.proposed_price, r.created_at AS response_created_at, r.is_selected
    FROM Tasks t
    LEFT JOIN Responses r ON t.id = r.task_id";

                var cmd = new MySql.Data.MySqlClient.MySqlCommand(query, conn);
                var reader = cmd.ExecuteReader();

                Dictionary<int, Task> taskMap = new Dictionary<int, Task>();

                while (reader.Read())
                {
                    int taskId = reader.GetInt32(reader.GetOrdinal("task_id"));
                    if (!taskMap.TryGetValue(taskId, out Task task))
                    {
                        task = new Task
                        {
                            Id = taskId,
                            Title = reader.IsDBNull(reader.GetOrdinal("title")) ? "" : reader.GetString(reader.GetOrdinal("title")),
                            Description = reader.IsDBNull(reader.GetOrdinal("description")) ? "" : reader.GetString(reader.GetOrdinal("description")),
                            CreatorId = reader.GetInt32(reader.GetOrdinal("creator_id")),
                            CreatedAt = reader.GetDateTime(reader.GetOrdinal("created_at")),
                            Budget = reader.GetDecimal(reader.GetOrdinal("budget")),
                            StatusId = reader.GetInt32(reader.GetOrdinal("status_id")),
                            Image = reader.IsDBNull(reader.GetOrdinal("pic")) ? @"C:\FreelanceExchange\FreelanceExchange_desktop\FreelanceExchange_desktop\Pics\UserPics\ok.png" : @"C:\FreelanceExchange\FreelanceExchange_desktop\FreelanceExchange_desktop\Pics\UserPics\" + reader.GetString(reader.GetOrdinal("pic")),
                            Responses = new List<Response>()
                        };
                        taskMap[taskId] = task;
                        Tasks.Add(task);
                    }

                    if (!reader.IsDBNull(reader.GetOrdinal("response_id")))
                    {
                        Response response = new Response
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("response_id")),
                            TaskId = taskId,
                            FreelancerId = reader.GetInt32(reader.GetOrdinal("freelancer_id")),
                            Message = reader.IsDBNull(reader.GetOrdinal("message")) ? "" : reader.GetString(reader.GetOrdinal("message")),
                            ProposedPrice = reader.GetDecimal(reader.GetOrdinal("proposed_price")),
                            CreatedAt = reader.GetDateTime(reader.GetOrdinal("response_created_at")),
                            IsSelected = reader.GetBoolean(reader.GetOrdinal("is_selected"))
                        };
                        task.Responses.Add(response);
                    }
                }

                reader.Close();
            }

            return Tasks;
        }




        public static void AddTaskToDatabase(Task task)
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string query = @"
        INSERT INTO Tasks (title, description, creator_id, created_at, budget, status_id, pic)
        VALUES (@Title, @Description, @CreatorId, @CreatedAt, @Budget, @StatusId, @Pic)";

                using (var cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@Title", task.Title);
                    cmd.Parameters.AddWithValue("@Description", task.Description);
                    cmd.Parameters.AddWithValue("@CreatorId", task.CreatorId);
                    cmd.Parameters.AddWithValue("@CreatedAt", task.CreatedAt);
                    cmd.Parameters.AddWithValue("@Budget", task.Budget);
                    cmd.Parameters.AddWithValue("@StatusId", task.StatusId);

                    cmd.Parameters.AddWithValue("@Pic", task.Image ?? (object)DBNull.Value);

                    cmd.ExecuteNonQuery();
                }
            }
        }


        public static ObservableCollection<Task> GetTasksByCustomerId(int customerId)
        {
            var tasks = new ObservableCollection<Task>();

            string query = "SELECT id, title, description, creator_id, created_at, budget, status_id FROM Tasks WHERE creator_id = @CustomerId";

            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@CustomerId", customerId);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var task = new Task
                            {
                                Id = reader.GetInt32("id"),
                                Title = reader.GetString("title"),
                                Description = reader.GetString("description"),
                                CreatorId = reader.GetInt32("creator_id"),
                                CreatedAt = reader.GetDateTime("created_at"),
                                Budget = reader.GetDecimal("budget"),
                                StatusId = reader.GetInt32("status_id")
                            };

                            tasks.Add(task);
                        }
                    }
                }
            }

            return tasks;
        }

        public static void DeleteTaskFromDb(Task task)
        {
            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                string query = "DELETE FROM Tasks WHERE id = @TaskId";

                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@TaskId", task.Id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static List<Response> LoadResponsesFromDb()
        {
            List<Response> responses = new List<Response>();

            using (var connection = new MySql.Data.MySqlClient.MySqlConnection(connectionString))
            {
                connection.Open();

                string query = @"
            SELECT 
                Responses.id, 
                Responses.task_id, 
                Responses.freelancer_id, 
                Responses.message, 
                Responses.proposed_price, 
                Responses.created_at, 
                Responses.is_selected,

                Tasks.id AS task_id,
                Tasks.title,
                Tasks.description,
                Tasks.creator_id,
                Tasks.created_at AS task_created_at,
                Tasks.budget,
                Tasks.status_id

            FROM Responses
            INNER JOIN Tasks ON Responses.task_id = Tasks.id";

                using (var command = new MySql.Data.MySqlClient.MySqlCommand(query, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var task = new Task
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("task_id")),
                            Title = reader.IsDBNull(reader.GetOrdinal("title")) ? "" : reader.GetString(reader.GetOrdinal("title")),
                            Description = reader.IsDBNull(reader.GetOrdinal("description")) ? "" : reader.GetString(reader.GetOrdinal("description")),
                            CreatorId = reader.GetInt32(reader.GetOrdinal("creator_id")),
                            CreatedAt = reader.GetDateTime(reader.GetOrdinal("task_created_at")),
                            Budget = reader.GetDecimal(reader.GetOrdinal("budget")),
                            StatusId = reader.GetInt32(reader.GetOrdinal("status_id"))
                        };

                        var response = new Response
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("id")),
                            TaskId = reader.GetInt32(reader.GetOrdinal("task_id")),
                            FreelancerId = reader.GetInt32(reader.GetOrdinal("freelancer_id")),
                            Message = reader.IsDBNull(reader.GetOrdinal("message")) ? "" : reader.GetString(reader.GetOrdinal("message")),
                            ProposedPrice = reader.GetDecimal(reader.GetOrdinal("proposed_price")),
                            CreatedAt = reader.GetDateTime(reader.GetOrdinal("created_at")),
                            IsSelected = reader.GetBoolean(reader.GetOrdinal("is_selected")),
                            Task = task
                        };

                        responses.Add(response);
                    }
                }
            }
            return responses;
        }

        public static void InsertResponse(Response response)
        {
            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                string query = @"
            INSERT INTO Responses 
            (task_id, freelancer_id, message, proposed_price, created_at, is_selected)
            VALUES
            (@TaskId, @FreelancerId, @Message, @ProposedPrice, @CreatedAt, @IsSelected)";

                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@TaskId", response.TaskId);
                    cmd.Parameters.AddWithValue("@FreelancerId", response.FreelancerId);
                    cmd.Parameters.AddWithValue("@Message", response.Message);
                    cmd.Parameters.AddWithValue("@ProposedPrice", response.ProposedPrice);
                    cmd.Parameters.AddWithValue("@CreatedAt", response.CreatedAt);
                    cmd.Parameters.AddWithValue("@IsSelected", response.IsSelected);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void DeleteResponse(Response response)
        {
            using (var conn = new MySql.Data.MySqlClient.MySqlConnection(connectionString))
            {
                conn.Open();

                string query = @"DELETE FROM Responses 
                         WHERE task_id = @TaskId AND freelancer_id = @FreelancerId";

                using (var cmd = new MySql.Data.MySqlClient.MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@TaskId", response.TaskId);
                    cmd.Parameters.AddWithValue("@FreelancerId", response.FreelancerId);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static bool SelectResponseForTask(List<Response> responses, Response selectedResponse, Task task)
        {
            if (responses.Any(r => r.IsSelected))
            {
                //MessageBox.Show("Задание уже назначено", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                using (var transaction = conn.BeginTransaction())
                {
                    try
                    {
                        string updateResponseQuery = "UPDATE Responses SET is_selected = 1 WHERE Id = @ResponseId";
                        using (var cmd = new MySqlCommand(updateResponseQuery, conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@ResponseId", selectedResponse.Id);
                            cmd.ExecuteNonQuery();
                        }

                        string updateTaskQuery = "UPDATE Tasks SET status_id = 2 WHERE Id = @TaskId";
                        using (var cmd = new MySqlCommand(updateTaskQuery, conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@TaskId", task.Id);
                            cmd.ExecuteNonQuery();
                        }

                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        //MessageBox.Show("Ошибка при обновлении: " + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        return false;
                    }
                }

                conn.Close();
            }

            selectedResponse.IsSelected = true;
            return true;
        }


    }
}
