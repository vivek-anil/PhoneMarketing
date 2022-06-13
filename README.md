# Quality guidelines

1.	The application takes input (Phone numbers) from a csv file. It can be extended/modified to get data/input from a json or any other file format or keyboard. Below guideline should be followed for this:
a.	Add another class (ex: JsonFileService) and add dependency.
2.	Currently the output is sent to the Console. It can be extended/modified to send the output to a file by creating an interface IOutputService and implementing it.
3.	Centralized exception handling can be done in middleware.
4.	The application is following SOLID principles.
5.	The files are opened and read in using block to make sure the objects are disposed properly.
6.	Exceptions are logged with class and method name.
7.	Asynchronous programming is used to improve the performance 
8.	All unused usings are removed.
9.	Input validation is done.
10.	New language features like Null-conditional operators (?) and null forgiving operator (!) are used  to avoid the Null Reference Exception at runtime.
11.	Methods are short having less number of codes.
12.	Used lambda for shorter and cleaner code for better readability.
13.	Unit test project should be added with unit test cases covering all methods.





