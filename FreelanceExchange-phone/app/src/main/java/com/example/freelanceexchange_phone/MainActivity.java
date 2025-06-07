package com.example.freelanceexchange_phone;

import android.os.Bundle;
import android.view.View;
import android.widget.EditText;
import android.widget.Toast;

import androidx.activity.EdgeToEdge;
import androidx.appcompat.app.AppCompatActivity;
import androidx.core.graphics.Insets;
import androidx.core.view.ViewCompat;
import androidx.core.view.WindowInsetsCompat;

import com.example.freelanceexchange_phone.db.User;
import java.util.ArrayList;

public class MainActivity extends AppCompatActivity {

    private EditText editTextLogin, editTextPassword;
    private ArrayList<User> Users;
    private User CurrentUser;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        EdgeToEdge.enable(this);
        setContentView(R.layout.activity_main);
        ViewCompat.setOnApplyWindowInsetsListener(findViewById(R.id.main), (v, insets) -> {
            Insets systemBars = insets.getInsets(WindowInsetsCompat.Type.systemBars());
            v.setPadding(systemBars.left, systemBars.top, systemBars.right, systemBars.bottom);
            return insets;
        });

        Users = (ArrayList<User>) getIntent().getSerializableExtra("users");
        CurrentUser = (User) getIntent().getSerializableExtra("currentUser");

        editTextLogin = findViewById(R.id.editTextLogin);
        editTextPassword = findViewById(R.id.editTextPassword);
    }

    public void onLoginClick(View view) {
        String login = editTextLogin.getText().toString();
        String password = editTextPassword.getText().toString();

        boolean found = false;
        for (User user : Users) {
            if (user.getUsername().equals(login) && user.getPassword().equals(password)) {
                found = true;

                break;
            }

        }

        if (found) {
            Toast.makeText(this, "Авторизация успешна!", Toast.LENGTH_SHORT).show();
            finish();
        } else {
            Toast.makeText(this, "Авторизация успешна!", Toast.LENGTH_SHORT).show();
        }
    }

    public void onNoAccountClick(View view) {

    }
}