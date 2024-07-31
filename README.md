# UMS

## Domain Model

- Course
  - Create
  - SetEnrollmentPeriod
    
- Class: Instance of a course that is assigned to a teacher.
  - Register
  - AddSession
  - Enroll
    
- Session: A time slot related to a class.
  - Create
  - SetTime

- User
  - Teachers
  - Students
 
## Features

- [x] Global Exception Handling with Custom Exceptions
- [x] API Versioning
- [x] Structured Logging with Serilog to Seq container, + Correlation ID Logging
- [x] Caching to Redis container
- [x] Health checks for postgres db, redis and seq, /heathz
- [x] Background Jobs with Hangfire, /Jobs

- [x] Authentication & Authorization using Keycloak
- [x] Utilizing Azure Blob Storage to store Profile Pictures, using Azurite container 
