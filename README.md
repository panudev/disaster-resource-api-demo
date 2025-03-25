Disaster Resource Allocation API with Redis Integration (.NET Core C#)
An API for managing disaster response by assigning resource trucks to affected areas based on urgency, time constraints, and available resources. Built with ASP.NET Core and integrated with Redis for caching.

🌐 Live Demo:
https://disaster-resource-api.victoriousglacier-f5d62531.southeastasia.azurecontainerapps.io/swagger/index.html

🔧 Features
Add disaster-affected areas with urgency and resource needs

Register resource trucks and their availability

Auto-assign trucks to areas based on matching logic

Redis integration for caching assignment results

Simple RESTful API with Swagger UI

📦 API Endpoints
➕ Add Affected Areas
POST /api/areas
Add new areas with:

Urgency level

Required resources

Time constraints

🚛 Add Resource Trucks
POST /api/trucks
Add trucks with:

Available resources

Travel times to different areas

⚙️ Process Assignments
POST /api/assignments
Assign trucks to areas based on:

Urgency

Time constraints

Resource availability

📥 Get Latest Assignments
GET /api/assignments
Retrieve the latest processed assignments from Redis cache (if available).

🗑️ Clear Assignments
DELETE /api/assignments
Clear all assignment data from cache.