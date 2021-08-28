.Net Core 5 API template project

This project is currently in development!

Project is separated into layers:

	* Template.API
		* Holds all the controllers for the API.
	* Template.API.Core
		* Holds common information that may be used for more than one API project.
	* Template.Application
		* Holds all the Commands and Queries used by the API methods.
	* Template.Core
		* Holds common information about Domain abstraction that may be used for more than one Domain project.
	* Template.Infra
		* Holds all information about database engines. Currently only Entity Framework is here, but here is where you
		should add your other database contexts.

Tecnologies:

	- FluentValidation
		- All commands and queries are validated using FluentValidation rules.
	- AutoMapper
		- Auto mapper is configured by default to help you convert your DTOs to query or commands.
	- MediatR
		- All commands, queries and entity notifications are sent using MediatR pattern.
		- All requests are logged using Serilog and updating the completion time.
	- Serilog
		- Serilog for logging.
	- Entity Framework
		- Entity framework is handling the SQL connection. Unit of Work pattern.
	- JWT Authentication
		- At the moment a simple login with no roles is working.
	- Identity Framework
		- Implemented identity framework for user creation and authentication.

Design:

	- DDD
		- Entities can trigger events after changed in database.
	- CQRS
		- Fully implemented. No exclusive database context for reading yet.
			- Implementation
				- All queries have a unique Id (GUID), CreationTime and FinishedExecution time.
				- Todo for commands.
				- Validation is done automagically in MediatR pipelines.
				- Added pipeline to Mediator that will update all commands and queries with the execution status and completion time.

To create your controller and methods you should follow this design:
	- Your API method should receive a DTO that is later automapped to a query or command and then executed by mediator.
