using Common_Layer.RequestModel;
using Repository_Layer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.Interfaces
{
    public interface ITaskManager
    {
        public TaskEntity TaskCreation(TaskModel model);
        public TaskEntity GetTaskById(int id);
        public TaskEntity UpdateTask(int id,TaskEntity taskUpdate);
        public TaskEntity DeleteTask(int id);
        public List<TaskEntity> GetAllTasks();
    }
}
