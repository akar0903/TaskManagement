let editTaskId = 0;
let tasks;
let lastAddedTaskId = null; // Track the ID of the last added task
const taskList = document.getElementById('taskList');
const formTitle = document.getElementById('formTitle');
const submitBtn = document.getElementById('submitBtn');

document.addEventListener('DOMContentLoaded', function() {
    const form = document.getElementById('taskForm');

    // Set default radio button to "False" on page load
    document.getElementById('isCompleteFalse').checked = true;

    displayTasks();

    form.addEventListener('submit', async function(e) {
        e.preventDefault();
        const title = document.getElementById('title').value.trim();
        const description = document.getElementById('description').value.trim();
        const dueDate = document.getElementById('dueDate').value;
        const isComplete = document.querySelector('input[name="isComplete"]:checked').value === 'true';

        let obj = {
            id: editTaskId,
            title: title,
            description: description,
            dueDate: dueDate,
            isComplete: isComplete
        };

        console.log('Task object being sent to API:', obj); // Debug the object being sent

        if (editTaskId == 0) {
            try {
                const response = await fetch('https://localhost:7248/api/Task/TaskCreation', {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json'
                    },
                    body: JSON.stringify(obj)
                });
                if (!response.ok) {
                    throw new Error(`HTTP error! status: ${response.status}`);
                }
                const createdTask = await response.json();
                lastAddedTaskId = createdTask.data.id; // Store the ID of the newly added task
                console.log(`New task added with ID: ${lastAddedTaskId}, isComplete: ${obj.isComplete}`);
            } catch (err) {
                console.error('Error creating task:', err);
            }
            form.reset();
            document.getElementById('isCompleteFalse').checked = true; // Ensure False is selected
            displayTasks();
        } else {
            try {
                const response = await fetch('https://localhost:7248/api/Task/UpdateTask', {
                    method: 'PUT',
                    headers: {
                        'Content-Type': 'application/json'
                    },
                    body: JSON.stringify(obj)
                });
                if (!response.ok) {
                    throw new Error(`HTTP error! status: ${response.status}`);
                }
                const updatedTask = await response.json();
                console.log(`Task updated with ID: ${obj.id}, new isComplete: ${obj.isComplete}`, updatedTask);
            } catch (err) {
                console.error('Error updating task:', err);
            }
            form.reset();
            document.getElementById('isCompleteFalse').checked = true; // Ensure False is selected

            // Reset form to add mode
            formTitle.textContent = 'Add New Task';
            submitBtn.textContent = 'Add Task';
            editTaskId = 0;
            displayTasks();
        }
    });
});

async function displayTasks() {
    try {
        const response = await fetch('https://localhost:7248/api/Task/GetAllTasks', {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json'
            }
        });
        if (!response.ok) {
            throw new Error(`HTTP error! status: ${response.status}`);
        }
        tasks = await response.json();
        tasks = tasks.data;
        console.log('Tasks from API:', tasks); // Debug API response
        taskList.innerHTML = '';

        if (tasks && tasks.length > 0) {
            tasks.forEach((task, i) => {
                const taskDiv = document.createElement('div');
                taskDiv.className = 'task-item';

                // Don't apply 'completed' class to the last added task, even if isComplete is true
                const shouldApplyCompletedClass = task.isComplete && task.id !== lastAddedTaskId;

                taskDiv.innerHTML = `
                            <p style="color: #f6e05e;"><strong>ID:</strong> ${task.id}</p>
                            <h5 class="${shouldApplyCompletedClass ? 'completed' : ''}" style="color: #63b3ed;">
                                <strong>Title:</strong> ${task.title}
                            </h5>
                            <p><strong>Description:</strong> ${task.description}</p>
                            <p><strong>Due:</strong> ${task.dueDate ? task.dueDate.split('T')[0] : 'No due date'}</p>
                            <p><strong>Completed:</strong> ${task.isComplete ? 'Yes' : 'No'}</p>
                            <button class="btn btn-sm btn-danger me-2" onclick="deleteTask(${task.id})">Delete</button>
                            <button class="btn btn-sm btn-success" onclick="editTask(${task.id})">✏️ Edit</button>
                        `;

                taskList.appendChild(taskDiv);
            });
        }
        // Reset lastAddedTaskId after rendering to ensure subsequent renders apply the completed class
        lastAddedTaskId = null;
    } catch (err) {
        console.error('Error fetching tasks:', err);
    }
}

async function deleteTask(index) {
    try {
        const response = await fetch(`https://localhost:7248/api/Task/DeleteTask?id=${index}`, {
            method: 'DELETE'
        });
        if (!response.ok) {
            throw new Error(`HTTP error! status: ${response.status}`);
        }
    } catch (err) {
        console.error('Error deleting task:', err);
    }
    displayTasks();
}

function editTask(index) {
    editTaskId = index;
    let curTask;
    for (let task of tasks) {
        if (task.id == index) {
            curTask = task;
        }
    }

    document.getElementById('title').value = curTask.title;
    document.getElementById('description').value = curTask.description;
    document.getElementById('dueDate').value = curTask.dueDate ? curTask.dueDate.split('T')[0] : '';
    document.getElementById('isCompleteTrue').checked = curTask.isComplete;
    document.getElementById('isCompleteFalse').checked = !curTask.isComplete;

    // Update form title and button text for edit mode
    formTitle.textContent = 'Update Task';
    submitBtn.textContent = 'Update Task';

    displayTasks();
}

async function filterTasks() {
    const query = document.getElementById('searchInput').value.trim();

    if (query === '') {
        displayTasks();
        return;
    }

    try {
        const response = await fetch(`https://localhost:7248/api/Task/GetTaskById?id=${query}`, {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json'
            }
        });
        if (!response.ok) {
            throw new Error(`HTTP error! status: ${response.status}`);
        }

        let result = await response.json();
        let filteredTasks = result.data;

        const taskList = document.getElementById('taskList');
        taskList.innerHTML = ''; // Clear previous results

        if (!filteredTasks || filteredTasks.length === 0) {
            taskList.innerHTML = `<p>No tasks found with ID ${query}</p>`;
            return;
        }

        const tasksToRender = Array.isArray(filteredTasks) ? filteredTasks : [filteredTasks];

        tasksToRender.forEach(task => {
            const taskDiv = document.createElement('div');
            taskDiv.className = 'task-item';

            // Don't apply 'completed' class to the last added task, even if isComplete is true
            const shouldApplyCompletedClass = task.isComplete && task.id !== lastAddedTaskId;

            taskDiv.innerHTML = `
                        <p style="color: #f6e05e;"><strong>ID:</strong> ${task.id}</p>
                        <h5 class="${shouldApplyCompletedClass ? 'completed' : ''}" style="color: #63b3ed;">
                            <strong>Title:</strong> ${task.title}
                        </h5>
                        <p><strong>Description:</strong> ${task.description}</p>
                        <p><strong>Due:</strong> ${task.dueDate ? task.dueDate : 'No due date'}</p>
                        <p><strong>Completed:</strong> ${task.isComplete ? 'Yes' : 'No'}</p>
                        <button class="btn btn-sm btn-danger me-2" onclick="deleteTask(${task.id})">Delete</button>
                        <button class="btn btn-sm btn-success" onclick="editTask(${task.id})">✏️ Edit</button>
                    `;

            taskList.appendChild(taskDiv);
        });

        // Reset lastAddedTaskId after rendering
        lastAddedTaskId = null;
    } catch (err) {
        console.error('Error fetching task by ID:', err);
    }
}