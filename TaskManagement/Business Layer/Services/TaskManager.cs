using Business_Layer.Interfaces;
using Common_Layer.RequestModel;
using Repository_Layer.Entity;
using Repository_Layer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.Services
{
    public class TaskManager : ITaskManager
    {
        private readonly ITaskRepository repository;
        public TaskManager(ITaskRepository repository)
        {
            this.repository = repository;
        }
        public TaskEntity TaskCreation(TaskModel model)
        {
            return repository.TaskCreation(model);
        }
        public TaskEntity GetTaskById(int id)
        {
            return repository.GetTaskById(id);
        }
        public TaskEntity UpdateTask(int id,TaskEntity taskupdate)
        {
            return repository.UpdateTask(id,taskupdate);
        }
        public TaskEntity DeleteTask(int id)
        {
            return repository.DeleteTask(id);
        }
        public List<TaskEntity> GetAllTasks()
        {
            return repository.GetAllTasks();
        }
    }
}
