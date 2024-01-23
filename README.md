# Asset_Loaning_Application
Overview
The school nearby has a policy to issue Chromebooks to its students for a school term and collect them back at the end of the term. As of now, it is a manual process, and the college is looking to automate the process using an Asset Loaning Application. You are required to work on this application. 
Details
This Asset Loaning Application will be working on the following data:
-	Users (Students and Supervisors) – Data will be provided.
-	Assets (Mix of Chromebooks and other Assets) – Datra will be provided.
-	Transactions (Loaned / Returned) – This application will create this data.
The Asset Loaning Application has the following capabilities:
-	View the list of all transactions (Loaned / Received) based on the user.
o	A student type of user will just see transactions for the assets loaned to him/her.
o	A supervisor will see all transactions regardless of the students.
o	This list can be filtered based on Assets, Date and User (Student / Supervisor)
-	Create a loan transaction for a student.
o	When creating a transaction, one will need the following:
	Student Name
	Supervisor Name
	Asset Number
	Loan Date
o	Only a supervisor can create a loan transaction.
-	Create a receive transaction for a student.
o	When creating a receive transaction, one will need the following:
	Student Name (Pre-filled)
	Supervisor Name (Pre-filled)
	Asset (Pre-filled)
	Loan Date (Pre-filled)
	Return Date
	Receiving Supervisor
o	Only a supervisor can create a receive transaction.
For this project, you do not need to add any authentication. As an alternative to be able to switch between two personas (Student/Supervisor), we will add a UI dropdown on the screen that can be used to switch the user using the application.
Developer Tasks
Backend Developer
-	Create a Database Schema for the application based on the given data and the expected data that will be created by this application.
-	Add an API to create a Loan Transaction.
o	Accessible to Supervisors 
-	Add an API to update the Loan Transaction
o	Accessible to Supervisors
-	Add an API to get a loan transaction from the Transaction ID.
o	Accessible to Students and Supervisors
-	Add an API to create a Return Transaction.
o	Accessible to Supervisors 
-	Add an API to update the Return Transaction
o	Accessible to Supervisors
-	Add an API to get a Return transaction from the Transaction ID.
o	Accessible to Students and Supervisors
-	Add an API to get the list of Loan/Return Transactions based on filter criteria.
o	Filters possible
	Student (User ID)
	Supervisor (User ID)
	Asset (Asset ID)
	Date (Transaction Date)
o	Accessible to Supervisors and Students
