using Grpc.Core;
using Library.Web.Aggregator.Models.Borrowing;
using Library.Web.Aggregator.Services.Interfaces;
namespace Library.Web.Aggregator.Services;

public class BorrowingService : IBorrowingService
{
    public BorrowingService()
    {

    }

    public async Task BorrowBookAsync(BorrowingBookRequest borrowingBookRequest)
    {
        if (borrowingBookRequest is null)
        {
            return;
        }

        try
        {
            //var student = await _studentService.GetStudentWithCourseByIdAsync(borrowingBookRequest.StudentId);

            //if (student is null)
            //{
            //    return;
            //}

            //var book = await _bookService.IsValidBookAsync(borrowingBookRequest.BookId, student.CourseId);

            //if (book is null)
            //{
            //    return;
            //}


        }
        catch (RpcException e)
        {
            throw e;
        }
    }
}
