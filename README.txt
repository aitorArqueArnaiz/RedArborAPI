RedArborAPI :

Implementation / Developement Steps : 

Domain-Driven-Developement

1. Think how ti create the data base table/s and how infrastructure module should be
2. Create the API (controllers, startup, error handling, logs , etc...)
3. Create the service : Domin and business logics (depending on the controller)
4. Create Unit Testing for basic flows (add, get, update and delete)


How to test the project : Run it with IIS Express in local machine

Data Base :
	MDF file in solution items folder with already some data created. Attach it to your local machine SQLServer with SQl Management Studio

Add New Employee :
 curl -k -X POST -H "Content-Type: application/json" -d @Example.json https://localhost:44373/Employee -v

Get Employee By Id :
 curl -k -H "Content-Type: application/json" -d @Example.json https://localhost:{PORT}/Employee{ID}/ -v

Get All Employees :
 curl -k -H "Content-Type: application/json" -d @Example.json https://localhost:{PORT}/Employee -v

Update existing employee :
 curl -k -X PATCH -H "Content-Type: application/json" -d @Example.json https://localhost:{PORT}/Employee -v

Delete existing employee :
 curl -k -X DELETE -H "Content-Type: application/json" -d @Example.json https://localhost:{PORT}/Employee/{ID} -v


 Docker :

 There is a docker-compose that starts the service, but in this case it is not usefull because the connection string agaist data base utilizes Integrated Security = true.
 In this dockerfile system this connection to the data base is not posible.