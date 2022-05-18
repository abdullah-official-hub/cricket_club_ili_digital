# cricket_club_ili_digital
Written in c#. net using microservices architecture. Good to explore this architecture for microservices better understanding.

Project is completed using Micro services architecture which contains 4 services and 1 class library.

1)	API GATEWAY
2)	IDENTITY SERVICE
3)	NEWS SERVCIE
4)	PLAYER SERVICE
5)	FRAMEWORK (CLASS LIBRARY)

* API GATEWAY: In 4 services, 1 service that is API GATEWAY is going to be exposed to users, other 3 services are connected to API GATEWAY but hides from users. Ocelot is used as API Gateway.
* FRAMEWORK: Class Library named Framework that contains common functionalities between micro services like basic models, exceptions etc.
* INDIVIDUAL DATABASES: Each service has its own database with specified tables. Each service is independent and responsible for its behavior and data.
* AUDIT & SOFT DELETE: Audit and soft delete is configured in Entity Framework core database context.
* ONION ARCHITECTURE & UNIT OF WORK:  For now, this project each service has basic functionality but I implemented onion architecture and unit of work design pattern for better overview and for easily makes changes in future.
* MIDDLEWARE – EXCEPTION HANDLING: In each service, exception handling middleware is configured. Not Found exception model is also created to throw when a record is not found.
* JWT TOKEN AUTHENTICATION: It is implemented in API Gateway. Each api call requires authentication before proceeding except register and login.
* CONTRACTS & MODELS: Each service contains its own models and contracts (Data Transfer Objects). Models are not going to expose to outer world. Only contracts are visible to outer world to encapsulate the secret and unwanted data.
* CODE FIRST APPROACH: In each service, I used code first approach and each service is configured to update changes in database for development and staging. Just need to add-migration manually.
* POSTGRES: Database which is used for each service is postgres.
* SWAGGER: In development phase, for better understanding of models and API’s, Swagger is configured in each service.
* AUTO MAPPER: To avoid manually mapping, I configured Automapper to make sure mapping between models and contracts.
* HASHED PASSWORD: In Identity Service, hashing of password is done before saving it into the database.

Encryption keys for hashing and connection string for database connections are stored in configuration files.

Abdullah Zafar
