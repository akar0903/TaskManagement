Task Management Application

This repository contains a full-stack Task Management application, featuring an ASP.NET Core Web API backend and a responsive HTML/CSS/JavaScript front-end. The application allows users to create, read, update, delete, and search tasks, with a modern UI and robust backend data management.

Backend: ASP.NET Core 8.0 Web API with Entity Framework Core and SQL Server. Front-end: HTML, CSS (Bootstrap 5.3), and JavaScript, with a dynamic task management interface.

Features 1.Create tasks with title, description, due date, and completion status. 2.View all tasks in a list with search-by-ID functionality. 3.Edit or delete existing tasks. 4.Responsive UI with animated task cards and gradient styling. 5.RESTful API with CRUD operations and error handling.

Prerequisites To build and run the application, ensure you have the following installed: .NET 8.0 SDK: For the backend API. SQL Server: For the database. Download SQL Server Express or use an existing instance. Node.js (optional): For local front-end development with live reloading (e.g., using a tool like live-server). Postman (optional): For testing API endpoints. Browser: Modern browser (Chrome, Firefox, Edge) for the front-end UI. Git: To clone the repository.

Assumptions 1.The backend uses SQL Server as the database. SQLite or other databases can be configured by modifying the connection string and EF Core provider. 2.The front-end assumes the backend API is running at https://localhost:7248. Update script.js if the API runs on a different port. 3.CORS is enabled for http://localhost to allow front-end requests. Adjust CORS settings in Program.cs for production.

Shortcuts and Trade-offs 1.No Authentication: The API is unsecured for simplicity. In production, JWT or OAuth would be added. 2.Basic Validation: The backend validates only required fields (e.g., Title). Additional validation (e.g., date formats) could be enhanced. 3.Local Database: SQL Server is assumed to be local. For cloud deployment, a managed database like Azure SQL would be better. 4.Front-end Hosting: The front-end is served statically. A production setup would use a web server like Nginx or host on a CDN.

Troubleshooting 1.API Errors: Ensure the backend is running and the connection string is correct. Use Postman to test endpoints. 2.CORS Issues: Verify CORS is configured in Program.cs to allow front-end requests. 3.Database Issues: Run dotnet ef database update to apply migrations. Check SQL Server is accessible. 4.Front-end Errors: Check the browser console for API call errors and ensure the API URL in script.js matches the backend.

Future Improvements 1.Add JWT authentication to secure API endpoints. 2.Support task categories and sorting by due date. 3.Implement pagination for large task lists. 4.Add full-text search for tasks by title or description. 5.Deploy the backend to a cloud platform like Azure and the front-end to a CDN. 6.Search Limitation: The search feature only supports ID-based queries. Full-text search could be added with additional backend logic.
