package com.example.freelanceexchange_phone.db;

import android.util.Log;
import java.sql.*;
import java.sql.Date;
import java.time.LocalDateTime;
import java.util.*;
import java.sql.Timestamp;
import java.time.ZoneId;

public class DatabaseCommand {
    private static final String TAG = "DatabaseCommand";
    private static final String CONNECTION_STRING = "jdbc:mysql://31.128.42.195/freelance_platform?user=userVanya&password=Vanya20066002&useSSL=false&allowPublicKeyRetrieval=true";

    public static ArrayList<User> getUsers() {
        ArrayList<User> users = new ArrayList<>();
        Map<Integer, User> userMap = new HashMap<>();

        String query = """
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
            """;

        try {
            // Загружаем драйвер MySQL
            Class.forName("com.mysql.cj.jdbc.Driver");
            Log.d(TAG, "MySQL JDBC Driver загружен успешно");

            try (Connection conn = DriverManager.getConnection(CONNECTION_STRING)) {
                Log.d(TAG, "Подключение к базе данных установлено успешно");
                
                try (PreparedStatement stmt = conn.prepareStatement(query);
                     ResultSet rs = stmt.executeQuery()) {

                    while (rs.next()) {
                        int id = rs.getInt("id");
                        Log.d(TAG, "Обработка пользователя с ID: " + id);

                        User user = userMap.get(id);
                        if (user == null) {
                            user = new User();
                            user.setId(id);
                            user.setEmail(rs.getString("email"));
                            user.setFirstName(rs.getString("first_name"));
                            user.setLastName(rs.getString("last_name"));
                            user.setUsername(rs.getString("username"));
                            user.setPassword(rs.getString("password"));
                            
                            Timestamp ts = rs.getTimestamp("birth_date");
                            if (ts != null) {
                                user.setBirthDate(ts.toInstant()
                                        .atZone(ZoneId.systemDefault())
                                        .toLocalDateTime());
                            }

                            Timestamp regTs = rs.getTimestamp("registration_date");
                            if (regTs != null) {
                                user.setRegistrationDate(
                                        regTs.toInstant()
                                                .atZone(ZoneId.systemDefault())
                                                .toLocalDateTime()
                                );
                            }

                            user.setRoles(new ArrayList<>());
                            userMap.put(id, user);
                            users.add(user);
                            Log.d(TAG, "Пользователь добавлен: " + user.getUsername());
                        }

                        String roleName = rs.getString("role_name");
                        if (roleName != null) {
                            user.getRoles().add(roleName);
                            Log.d(TAG, "Добавлена роль " + roleName + " для пользователя " + user.getUsername());
                        }
                    }
                }
            }
        } catch (ClassNotFoundException e) {
            Log.e(TAG, "MySQL JDBC Driver не найден", e);
            e.printStackTrace();
        } catch (SQLException e) {
            Log.e(TAG, "Ошибка при работе с базой данных", e);
            e.printStackTrace();
        }

        Log.d(TAG, "Всего загружено пользователей: " + users.size());
        return users;
    }

    public static boolean isUserUnique(User newUser) {
        String query = "SELECT COUNT(*) FROM Users WHERE email = ? OR username = ?";
        try (Connection conn = DriverManager.getConnection(CONNECTION_STRING);
             PreparedStatement stmt = conn.prepareStatement(query)) {

            stmt.setString(1, newUser.getEmail());
            stmt.setString(2, newUser.getUsername());

            try (ResultSet rs = stmt.executeQuery()) {
                if (rs.next()) {
                    return rs.getLong(1) == 0;
                }
            }

        } catch (SQLException e) {
            e.printStackTrace();
        }
        return false;
    }

    public static boolean addUser(User user, String roleName) {
        String insertUserQuery = """
            INSERT INTO Users (email, first_name, last_name, username, password, birth_date, registration_date)
            VALUES (?, ?, ?, ?, ?, ?, ?)
            """;

        String getRoleIdQuery = "SELECT id FROM Roles WHERE name = ?";
        String insertUserRoleQuery = "INSERT INTO UserRoles (user_id, role_id) VALUES (?, ?)";

        try (Connection conn = DriverManager.getConnection(CONNECTION_STRING)) {
            conn.setAutoCommit(false);

            int userId;
            try (PreparedStatement insertUserStmt = conn.prepareStatement(insertUserQuery, Statement.RETURN_GENERATED_KEYS)) {
                insertUserStmt.setString(1, user.getEmail());
                insertUserStmt.setString(2, user.getFirstName());
                insertUserStmt.setString(3, user.getLastName());
                insertUserStmt.setString(4, user.getUsername());
                insertUserStmt.setString(5, user.getPassword());
                insertUserStmt.setDate(6, Date.valueOf(user.getBirthDate()));
                insertUserStmt.setTimestamp(7, Timestamp.valueOf(String.valueOf(LocalDateTime.now())));

                int affectedRows = insertUserStmt.executeUpdate();
                if (affectedRows == 0) throw new SQLException("Creating user failed, no rows affected.");

                try (ResultSet generatedKeys = insertUserStmt.getGeneratedKeys()) {
                    if (generatedKeys.next()) {
                        userId = generatedKeys.getInt(1);
                    } else {
                        throw new SQLException("Creating user failed, no ID obtained.");
                    }
                }
            }

            int roleId;
            try (PreparedStatement getRoleIdStmt = conn.prepareStatement(getRoleIdQuery)) {
                getRoleIdStmt.setString(1, roleName);
                try (ResultSet rs = getRoleIdStmt.executeQuery()) {
                    if (!rs.next()) {
                        throw new SQLException("Role '" + roleName + "' not found.");
                    }
                    roleId = rs.getInt("id");
                }
            }

            try (PreparedStatement insertUserRoleStmt = conn.prepareStatement(insertUserRoleQuery)) {
                insertUserRoleStmt.setInt(1, userId);
                insertUserRoleStmt.setInt(2, roleId);
                insertUserRoleStmt.executeUpdate();
            }

            conn.commit();
            return true;

        } catch (SQLException e) {
            e.printStackTrace();
            // If connection was set to manual commit, rollback
            // Cannot rollback here without connection reference,
            // so better to manage connection outside or use a transaction manager
            return false;
        }
    }

    // ==================== Tasks ====================

    public static ArrayList<Task> loadTasksFromDb() {
        ArrayList<Task> tasks = new ArrayList<Task>();
        String query = """
            SELECT
                t.id AS task_id, t.title, t.description, t.creator_id,
                t.created_at, t.budget, t.status_id, t.pic,
                r.id AS response_id, r.freelancer_id, r.message,
                r.proposed_price, r.created_at AS response_created_at, r.is_selected
            FROM Tasks t
            LEFT JOIN Responses r ON t.id = r.task_id
            """;

        Map<Integer, Task> taskMap = new HashMap<>();

        try (Connection conn = DriverManager.getConnection(CONNECTION_STRING);
             PreparedStatement stmt = conn.prepareStatement(query);
             ResultSet rs = stmt.executeQuery()) {

            while (rs.next()) {
                int taskId = rs.getInt("task_id");
                Task task = taskMap.get(taskId);

                if (task == null) {
                    task = new Task();
                    task.setId(taskId);
                    task.setTitle(rs.getString("title") != null ? rs.getString("title") : "");
                    task.setDescription(rs.getString("description") != null ? rs.getString("description") : "");
                    task.setCreatorId(rs.getInt("creator_id"));
                    Timestamp createdAtTs = rs.getTimestamp("created_at");
                    if (createdAtTs != null) {
                        task.setCreatedAt(
                                createdAtTs.toInstant()
                                        .atZone(ZoneId.systemDefault())
                                        .toLocalDateTime()
                        );
                    }

                    task.setBudget(rs.getBigDecimal("budget"));
                    task.setStatusId(rs.getInt("status_id"));
                    String pic = rs.getString("pic");
                    task.setImage(pic == null ? "C:\\FreelanceExchange\\FreelanceExchange_desktop\\FreelanceExchange_desktop\\Pics\\UserPics\\ok.png" :
                            "C:\\FreelanceExchange\\FreelanceExchange_desktop\\FreelanceExchange_desktop\\Pics\\UserPics\\" + pic);
                    task.setResponses(new ArrayList<>());
                    taskMap.put(taskId, task);
                    tasks.add(task);
                }

                Object obj = rs.getObject("response_id");
                Integer responseId = null;
                if (obj != null) {
                    responseId = ((Number) obj).intValue();
                }


                if (responseId != null) {
                    Response response = new Response();
                    response.setId(responseId);
                    response.setTaskId(taskId);
                    response.setFreelancerId(rs.getInt("freelancer_id"));
                    response.setMessage(rs.getString("message") != null ? rs.getString("message") : "");
                    response.setProposedPrice(rs.getBigDecimal("proposed_price"));
                    Timestamp ts = rs.getTimestamp("response_created_at");
                    if (ts != null) {
                        response.setCreatedAt(
                                ts.toInstant()
                                        .atZone(ZoneId.systemDefault())
                                        .toLocalDateTime()
                        );
                    }

                    response.setIsSelected(rs.getBoolean("is_selected"));

                    task.getResponses().add(response);
                }
            }

        } catch (SQLException e) {
            e.printStackTrace();
        }

        return tasks;
    }

    public static void addTaskToDatabase(Task task) {
        String query = """
            INSERT INTO Tasks (title, description, creator_id, created_at, budget, status_id, pic)
            VALUES (?, ?, ?, ?, ?, ?, ?)
            """;

        try (Connection conn = DriverManager.getConnection(CONNECTION_STRING);
             PreparedStatement stmt = conn.prepareStatement(query)) {

            stmt.setString(1, task.getTitle());
            stmt.setString(2, task.getDescription());
            stmt.setInt(3, task.getCreatorId());
            stmt.setTimestamp(4, Timestamp.valueOf(task.getCreatedAt().toString()));
            stmt.setBigDecimal(5, task.getBudget());
            stmt.setInt(6, task.getStatusId());

            if (task.getImage() == null) {
                stmt.setNull(7, Types.VARCHAR);
            } else {
                stmt.setString(7, task.getImage());
            }

            stmt.executeUpdate();

        } catch (SQLException e) {
            e.printStackTrace();
        }
    }

    public static ArrayList<Task> getTasksByCustomerId(int customerId) {
        ArrayList<Task> tasks = new ArrayList<>();
        String query = "SELECT id, title, description, creator_id, created_at, budget, status_id FROM Tasks WHERE creator_id = ?";

        try (Connection conn = DriverManager.getConnection(CONNECTION_STRING);
             PreparedStatement stmt = conn.prepareStatement(query)) {

            stmt.setInt(1, customerId);
            try (ResultSet rs = stmt.executeQuery()) {
                while (rs.next()) {
                    Task task = new Task();
                    task.setId(rs.getInt("id"));
                    task.setTitle(rs.getString("title"));
                    task.setDescription(rs.getString("description"));
                    task.setCreatorId(rs.getInt("creator_id"));
                    Timestamp ts = rs.getTimestamp("created_at");
                    if (ts != null) {
                        task.setCreatedAt(
                                ts.toInstant()
                                        .atZone(ZoneId.systemDefault())
                                        .toLocalDateTime()
                        );
                    }

                    task.setBudget(rs.getBigDecimal("budget"));
                    task.setStatusId(rs.getInt("status_id"));

                    tasks.add(task);
                }
            }

        } catch (SQLException e) {
            e.printStackTrace();
        }

        return tasks;
    }

    public static void deleteTaskFromDb(Task task) {
        String query = "DELETE FROM Tasks WHERE id = ?";

        try (Connection conn = DriverManager.getConnection(CONNECTION_STRING);
             PreparedStatement stmt = conn.prepareStatement(query)) {

            stmt.setInt(1, task.getId());
            stmt.executeUpdate();

        } catch (SQLException e) {
            e.printStackTrace();
        }
    }

    // ==================== Responses ====================
    public static ArrayList<Response> loadResponsesFromDb() {
        ArrayList<Response> responses = new ArrayList<>();

        String query = """
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
            INNER JOIN Tasks ON Responses.task_id = Tasks.id
            """;

        try (Connection conn = DriverManager.getConnection(CONNECTION_STRING);
             PreparedStatement stmt = conn.prepareStatement(query);
             ResultSet rs = stmt.executeQuery()) {

            while (rs.next()) {
                Task task = new Task();
                task.setId(rs.getInt("task_id"));
                task.setTitle(rs.getString("title") != null ? rs.getString("title") : "");
                task.setDescription(rs.getString("description") != null ? rs.getString("description") : "");
                task.setCreatorId(rs.getInt("creator_id"));
                Timestamp ts = rs.getTimestamp("task_created_at");
                if (ts != null) {
                    task.setCreatedAt(
                            ts.toInstant()
                                    .atZone(ZoneId.systemDefault())
                                    .toLocalDateTime()
                    );
                }

                task.setBudget(rs.getBigDecimal("budget"));
                task.setStatusId(rs.getInt("status_id"));

                Response response = new Response();
                response.setId(rs.getInt("id"));
                response.setTaskId(rs.getInt("task_id"));
                response.setFreelancerId(rs.getInt("freelancer_id"));
                response.setMessage(rs.getString("message") != null ? rs.getString("message") : "");
                response.setProposedPrice(rs.getBigDecimal("proposed_price"));
                Timestamp ts1 = rs.getTimestamp("created_at");
                if (ts1 != null) {
                    response.setCreatedAt(
                            ts1.toInstant()
                                    .atZone(ZoneId.systemDefault())
                                    .toLocalDateTime()
                    );
                }

                response.setIsSelected(rs.getBoolean("is_selected"));
                response.setTask(task);

                responses.add(response);
            }

        } catch (SQLException e) {
            e.printStackTrace();
        }

        return responses;
    }

    public static void insertResponse(Response response) {
        String query = """
            INSERT INTO Responses (task_id, freelancer_id, message, proposed_price, created_at, is_selected)
            VALUES (?, ?, ?, ?, ?, ?)
            """;

        try (Connection conn = DriverManager.getConnection(CONNECTION_STRING);
             PreparedStatement stmt = conn.prepareStatement(query)) {

            stmt.setInt(1, response.getTaskId());
            stmt.setInt(2, response.getFreelancerId());
            stmt.setString(3, response.getMessage());
            stmt.setBigDecimal(4, response.getProposedPrice());
            stmt.setTimestamp(5, Timestamp.valueOf(response.getCreatedAt().toString()));
            stmt.setBoolean(6, response.isIsSelected());

            stmt.executeUpdate();

        } catch (SQLException e) {
            e.printStackTrace();
        }
    }

    public static void deleteResponse(Response response) {
        String query = "DELETE FROM Responses WHERE task_id = ? AND freelancer_id = ?";

        try (Connection conn = DriverManager.getConnection(CONNECTION_STRING);
             PreparedStatement stmt = conn.prepareStatement(query)) {

            stmt.setInt(1, response.getTaskId());
            stmt.setInt(2, response.getFreelancerId());

            stmt.executeUpdate();

        } catch (SQLException e) {
            e.printStackTrace();
        }
    }

    public static boolean selectResponseForTask(List<Response> responses, Response selectedResponse, Task task) {
        if (responses.stream().anyMatch(Response::isIsSelected)) {
            return false;
        }

        String updateResponseQuery = "UPDATE Responses SET is_selected = 1 WHERE id = ?";
        String updateTaskQuery = "UPDATE Tasks SET status_id = 2 WHERE id = ?";

        try (Connection conn = DriverManager.getConnection(CONNECTION_STRING)) {
            conn.setAutoCommit(false);

            try (PreparedStatement updateResponseStmt = conn.prepareStatement(updateResponseQuery)) {
                updateResponseStmt.setInt(1, selectedResponse.getId());
                updateResponseStmt.executeUpdate();
            }

            try (PreparedStatement updateTaskStmt = conn.prepareStatement(updateTaskQuery)) {
                updateTaskStmt.setInt(1, task.getId());
                updateTaskStmt.executeUpdate();
            }

            conn.commit();

        } catch (SQLException e) {
            e.printStackTrace();
            return false;
        }

        selectedResponse.setIsSelected(true);
        return true;
    }
}
