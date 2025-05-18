using Business_Layer.Interfaces;
using Common_Layer.RequestModel;
using Common_Layer.ResponseModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository_Layer.Context;
using Repository_Layer.Entity;

namespace Task_Management.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private ITaskManager manager;
        private readonly TaskContext context;
        public TaskController(ITaskManager manager, TaskContext context)
        {
            this.manager = manager;
            this.context = context;
        }
        [HttpPost]
        [Route("TaskCreation")]
        public ActionResult BookCreation(TaskModel model)
        {
            //int id = Convert.ToInt32(User.FindFirst("Id").Value);
            var response = manager.TaskCreation(model);
            if (response != null)
            {
                return Ok(new ResModel<TaskEntity> { Success = true, Message = "Task Created successfully", Data = response });
            }
            else
            {
                return BadRequest(new ResModel<TaskEntity> { Success = false, Message = "Task Creation Failed", Data = response });
            }
        }
        [HttpGet]
        [Route("GetTaskById")]
        public ActionResult GetTaskById(int id)
        {
            try
            {
                var response = manager.GetTaskById(id);
                if (response != null)
                {
                    return Ok(new ResModel<TaskEntity> { Success = true, Message = "Task Fetched Successful", Data = response });
                }
                return BadRequest(new ResModel<TaskEntity> { Success = false, Message = "Fetching Fail", Data = response });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResModel<TaskEntity> { Success = false, Message = "Task not found", Data = null });
            }
        }
        [HttpPut]
        [Route("UpdateTask")]
        public ActionResult UpdateTask(TaskEntity taskUpdate)
        {
            try
            {
                var response = manager.UpdateTask(taskUpdate.Id, taskUpdate);
                if (response != null)
                {
                    return Ok(new ResModel<TaskEntity> { Success = true, Message = "Task is updated", Data = response });
                }
                else
                {
                    return BadRequest(new ResModel<TaskEntity> { Success = false, Message = "Task is not found", Data = response });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new ResModel<TaskEntity> { Success = false, Message = ex.Message, Data = null });
            }
        }
        [HttpDelete]
        [Route("DeleteTask")]
        public ActionResult DeleteTask(int id)
        {
            try
            {
                var response = manager.DeleteTask(id);
                if (response != null)
                {
                    return Ok(new ResModel<TaskEntity> { Success = true, Message = "Task Deleted", Data = response });
                }
                else
                {
                    return BadRequest(new ResModel<TaskEntity> { Success = false, Message = "Task is not deleted", Data = response });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new ResModel<TaskEntity> { Success = false, Message = ex.Message, Data = null });
            }
        }
        [HttpGet]
        [Route("GetAllTasks")]
        public ActionResult GetAllTasks()
        {
            List<TaskEntity> response = manager.GetAllTasks();
            if (response != null)
            {
                return Ok(new ResModel<List<TaskEntity>> { Success = true, Message = "Fetched Successfully", Data = response });
            }
            else
            {
                return BadRequest(new ResModel<List<TaskEntity>> { Success = true, Message = "Creation Failed", Data = response });
            }
        }
    }
}