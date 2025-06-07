package com.example.freelanceexchange_phone.db;

import java.io.Serializable;
import java.math.BigDecimal;
import java.time.LocalDateTime;
import java.util.ArrayList;
import java.util.List;

public class Task implements Serializable {
    public String getImage() {
        return Image;
    }

    public void setImage(String image) {
        Image = image;
    }

    public List<Response> getResponses() {
        return Responses;
    }

    public void setResponses(List<Response> responses) {
        Responses = responses;
    }

    public int getStatusId() {
        return StatusId;
    }

    public void setStatusId(int statusId) {
        StatusId = statusId;
    }

    public BigDecimal getBudget() {
        return Budget;
    }

    public void setBudget(BigDecimal budget) {
        Budget = budget;
    }

    public LocalDateTime getCreatedAt() {
        return CreatedAt;
    }

    public void setCreatedAt(LocalDateTime createdAt) {
        CreatedAt = createdAt;
    }

    public int getCreatorId() {
        return CreatorId;
    }

    public void setCreatorId(int creatorId) {
        CreatorId = creatorId;
    }

    public String getDescription() {
        return Description;
    }

    public void setDescription(String description) {
        Description = description;
    }

    public String getTitle() {
        return Title;
    }

    public void setTitle(String title) {
        Title = title;
    }

    public int getId() {
        return Id;
    }

    public void setId(int id) {
        Id = id;
    }

    public int Id;
    public String Title;
    public String Description;
    public int CreatorId;
    public LocalDateTime CreatedAt;
    public BigDecimal Budget;
    public int StatusId;
    public List<Response> Responses  = new ArrayList<Response>();
    public String Image;


}
