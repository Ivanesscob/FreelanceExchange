package com.example.freelanceexchange_phone;

import android.content.Intent;
import android.os.Bundle;
import android.widget.Toast;

import androidx.activity.EdgeToEdge;
import androidx.appcompat.app.AppCompatActivity;
import androidx.core.graphics.Insets;
import androidx.core.view.ViewCompat;
import androidx.core.view.WindowInsetsCompat;

import com.example.freelanceexchange_phone.db.*;

import java.io.Serializable;
import java.util.ArrayList;

public class MainWindow extends AppCompatActivity implements Serializable {

    public ArrayList<User> Users;
    public User CurrentUser;
    public ArrayList<Task> Tasks;
    public ArrayList<Response> Responses;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        EdgeToEdge.enable(this);
        setContentView(R.layout.activity_main_window);
        ViewCompat.setOnApplyWindowInsetsListener(findViewById(R.id.main), (v, insets) -> {
            Insets systemBars = insets.getInsets(WindowInsetsCompat.Type.systemBars());
            v.setPadding(systemBars.left, systemBars.top, systemBars.right, systemBars.bottom);
            return insets;
        });

        LoadData();

        Intent intent = new Intent(this, MainActivity.class);
        intent.putExtra("users", Users);
        intent.putExtra("currentUser", CurrentUser);
        startActivity(intent);
    }

    private void LoadData() {
        Tasks = DatabaseCommand.loadTasksFromDb();
        Responses = DatabaseCommand.loadResponsesFromDb();
        Users = DatabaseCommand.getUsers();
        
        if (Users == null) {
            Toast.makeText(this, "Ошибка загрузки пользователей", Toast.LENGTH_SHORT).show();
        } else {
            Toast.makeText(this, "Загружено пользователей: " + Users.size(), Toast.LENGTH_SHORT).show();
        }
    }
}