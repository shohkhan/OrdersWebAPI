# Simple Web API application (using the ASP.NET MVC template) # 
 
### Mostly to see how it works ###

### Assumptions: ###
1. This is a book selling site, with a fixed number of books on this site.
2. There is a single customer named John Doe. He is assumed to be logged in and his customer id is to be saved in the web.config file.
3. The customer can - 
	a. Add products to the current order (equivalent to cart), 
	b. Submit the current order and 
	c. View the submitted orders.
4. The current order is not submitted - so can't be seen on the submitted orders.
5. After submitting a order it moves to the "Waiting For Delivery" state.
6. No tax is calculated.

### Can be accomplished with more time: ###
1. Customer login module can be created. Role based access control can be implemented.
2. Customer registration and management modules can be created.
3. Product management, Order notification (i.e. notification after order is submitted) and Order followup modules can be created.
4. The UI can be made more robust by implementing bootstrapping to full extent.

### Database: ###
The database can be restored in two ways:
1. Code first: Please follow the follow steps:
	a. Update the value of "OrdersContext" on the web.config file.
	b. Run the following two commands in the Package Manager Console of the Visual Studio:
		add-migration Initial
		update-database
2. Database first:
	a. Restore the provided .bak file to a SQL server.
	b. Update the value of "OrdersContext" on the web.config file.

Note: After restoring the database, please update the values of "customerId", "orderNotSubmitted" and "orderSubmitted" in the web.config file, if required.

### IDE used: ###
Visual Studio 2017 – Community Edition

### Technologies used: ###
C#, ASP.NET, LINQ, Entity Framework, JavaScript, HTML, CSS, JQuery, Notify, Bootstrap, SQL, ASP.NET MVC, ASP.NET Web API, SQL Server 2016 Developer with Service Pack 1, Power Shell

### Tested on Browser: ###
Google Chrome, Version 56.0.2924.87 (64-bit)
