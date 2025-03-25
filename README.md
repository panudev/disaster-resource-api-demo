# disaster-resource-api-demo
Disaster Resource Allocation API with Redis Integration (.Net Core C#)

Live Demo: https://disaster-resource-api.victoriousglacier-f5d62531.southeastasia.azurecontainerapps.io/swagger/index.html

API Endpoints:
o POST /api/areas: Allows adding affected areas, with details such as urgency level, resources
needed, and time constraints.
o POST /api/trucks: Allows adding resource trucks, with details about available resources and
travel times to areas.
o POST /api/assignments: Processes and returns truck assignments for each area based on
urgency, time constraints, and available resources.
o GET /api/assignments: Returns the last processed assignments, retrieving them from a Redis
cache if available.
o DELETE /api/assignments: Clears the current assignment data from the cache.