using BorrowingMicroservice.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BorrowingMicroservice.Test
{
    class TestData
    {
        public static IEnumerable<object[]> GetPostTestData()
        {
            yield return new object[] { new BorrowingsBaseModel { Id = 1, CustomerId=1,BookId=1,Date= new DateTime(2022,10,1) } };
            yield return new object[] { new BorrowingsBaseModel { Id = 2, CustomerId=2,BookId=2,Date= new DateTime(2022,12,10) } };
        }
        // id customerid bookid datetime
        public static IEnumerable<object[]> GetPutTestData()
        {
            yield return new object[] { 1, new BorrowingsBaseModel { Id = 1, CustomerId = 1, BookId = 1, Date = new DateTime(2022, 10, 1) } };
            yield return new object[] { 2, new BorrowingsBaseModel { Id = 2, CustomerId = 2, BookId = 2, Date = new DateTime(2022, 12, 10) } };
        }

    }
}
