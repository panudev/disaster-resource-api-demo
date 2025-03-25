# Disaster Resource Allocation API with Redis Integration (.NET Core C#)

An API for managing disaster response by assigning resource trucks to affected areas based on urgency, time constraints, and available resources. Built with **ASP.NET Core** and integrated with **Redis** for caching.

🌐 **Live Demo:**  
[https://disaster-resource-api.victoriousglacier-f5d62531.southeastasia.azurecontainerapps.io/swagger/index.html](https://disaster-resource-api.victoriousglacier-f5d62531.southeastasia.azurecontainerapps.io/swagger/index.html)

---

## 🔧 Features

- Add disaster-affected areas with urgency levels and resource requirements  
- Register available resource trucks and their travel times  
- Automatically assign trucks to areas based on defined criteria  
- Use Redis to cache the latest assignment data  
- Simple RESTful API with Swagger UI for testing  

---

## 📦 API Endpoints

### ➕ Add Affected Areas  
`POST /api/areas`  
Add a new area with:  
- Urgency level  
- Required resources  
- Time constraints  

### 🚛 Add Resource Trucks  
`POST /api/trucks`  
Register a truck with:  
- Available resources  
- Travel times to specific areas  

### ⚙️ Process Assignments  
`POST /api/assignments`  
Automatically assign trucks to areas based on:  
- Urgency  
- Time constraints  
- Available resources  

### 📥 Get Latest Assignments  
`GET /api/assignments`  
Retrieve the most recent assignment results (from Redis cache if available).  

### 🗑️ Clear Assignments  
`DELETE /api/assignments`  
Clear all current assignment data from the cache.  

---
