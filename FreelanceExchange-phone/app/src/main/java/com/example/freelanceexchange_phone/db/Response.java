package com.example.freelanceexchange_phone.db;

import java.io.Serializable;
import java.math.BigDecimal;
import java.time.LocalDateTime;

public class Response implements Serializable {
    public int Id;
    public int TaskId;

    public int getId() {
        return Id;
    }

    public void setId(int id) {
        Id = id;
    }

    public int getTaskId() {
        return TaskId;
    }

    public void setTaskId(int taskId) {
        TaskId = taskId;
    }

    public int getFreelancerId() {
        return FreelancerId;
    }

    public void setFreelancerId(int freelancerId) {
        FreelancerId = freelancerId;
    }

    public String getMessage() {
        return Message;
    }

    public void setMessage(String message) {
        Message = message;
    }

    public BigDecimal getProposedPrice() {
        return ProposedPrice;
    }

    public void setProposedPrice(BigDecimal proposedPrice) {
        ProposedPrice = proposedPrice;
    }

    public LocalDateTime getCreatedAt() {
        return CreatedAt;
    }

    public void setCreatedAt(LocalDateTime createdAt) {
        CreatedAt = createdAt;
    }

    public Boolean isIsSelected() {
        return IsSelected;
    }

    public void setIsSelected(Boolean selected) {
        IsSelected = selected;
    }

    public Task getTask() {
        return Task;
    }

    public void setTask(Task task) {
        Task = task;
    }

    public int FreelancerId;
    public String Message;
    public BigDecimal ProposedPrice;
    public LocalDateTime CreatedAt;
    public Boolean IsSelected;
    public Task Task;
}
