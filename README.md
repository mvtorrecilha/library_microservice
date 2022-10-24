# Library - Microservice (Backend)

A small simple library microservice .net core.

## Technologies:

- ASP.NET Core 6.0
- Entity Framework Core
- Grpc
- CQRS
- Gateway pattern / Gateway Aggregation pattern
- Swagger
- RabbitMQ

## Features available for access:
- Books:
    - Get All Books
        - Url: https://localhost:4001/api/v1/books
        - Url from gateway: https://localhost:5101/book-api/v1/books
    - Borrow a Book
        - Url from Aggregator: https://localhost:1001/api/v1/book/borrow-book
        - Url from gateway: https://localhost:5101/api/v1/borrow-book
    - Return a Book
        - Url: https://localhost:4001/api/v1/book/return-book
        - Url from gateway: https://localhost:5101/book-api/v1/book/return-book

- Courses:
    - Get All Courses
        - Url: https://localhost:2001/api/v1/courses
        - Url from gateway: https://localhost:5101/course-api/v1/courses

- Students:
    - Get All Students
        - Url: https://localhost:3001/api/v1/students
        - Url from gateway: https://localhost:5101/student-api/v1/students

- Borrowing History:
    - Get All Borrowing Histories
        - Url: https://localhost:6001/api/v1/borrowing-history
        - Url from gateway: https://localhost:5101/borrowing-api/v1/borrowing-history

Note: You can download the postman file configuration to import. The file is located in the Postman files folder
