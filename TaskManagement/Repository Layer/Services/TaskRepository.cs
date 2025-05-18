using Common_Layer.RequestModel;
using Repository_Layer.Context;
using Repository_Layer.Entity;
using Repository_Layer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository_Layer.Services
{
    public class TaskRepository : ITaskRepository
    {
        private readonly TaskContext context;
        public TaskRepository(TaskContext context)
        {
            this.context = context;
        }
        public TaskEntity TaskCreation(TaskModel model)
        {
            TaskEntity entity = new TaskEntity();
            entity.Title = model.Title;
            entity.Description = model.Description;
            entity.DueDate = model.DueDate;
            entity.IsComplete = model.IsComplete;
            context.Tasks.Add(entity);
            context.SaveChanges();
            return entity;
        }
        public TaskEntity GetTaskById(int id)
        {
            TaskEntity Tasks = context.Tasks.FirstOrDefault(x => x.Id == id);
            if (Tasks != null)
            {
                return Tasks;
            }
            else
            {
                throw new Exception("No Task Found");
            }
        }
        public TaskEntity UpdateTask(int id, TaskEntity updatedTask)
        {
            var task = context.Tasks.FirstOrDefault(x => x.Id == updatedTask.Id);

            if (task == null)
                throw new Exception("Task not found");
            if(task.Id != updatedTask.Id) {
                throw new Exception("ID of the task cant be updated");
            }
            if (string.IsNullOrWhiteSpace(updatedTask.Title) && string.IsNullOrWhiteSpace(updatedTask.Description))
                throw new Exception("Title and description both cannot be empty");
            if (!string.IsNullOrWhiteSpace(updatedTask.Title))
                task.Title = updatedTask.Title;
            if (!string.IsNullOrWhiteSpace(updatedTask.Description))
                task.Description = updatedTask.Description;
            if (string.IsNullOrWhiteSpace(updatedTask.Title))
                throw new Exception("Title cannot be empty");
            if (string.IsNullOrWhiteSpace(updatedTask.Description))
                throw new Exception("Description cannot be empty");

            task.DueDate = updatedTask.DueDate;
            task.IsComplete = updatedTask.IsComplete;

            context.SaveChanges();
            return task;
        }
        public TaskEntity DeleteTask(int id)
        {
            var Task = context.Tasks.FirstOrDefault(x => x.Id == id);
            if (Task != null)
            {
                context.Tasks.Remove(Task);
                context.SaveChanges();
                return Task;
            }
            else
            {
                throw new Exception("Task is not found");
            }
        }
        public List<TaskEntity> GetAllTasks()
        {
            return context.Tasks.ToList();
        }
    }
}

