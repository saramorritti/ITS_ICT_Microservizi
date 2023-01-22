using BookMicroservice.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace BookMicroservice.Tests
{
    public class TestData
    {
        public static IEnumerable<object[]> GetPostTestData()
        {
            yield return new object[] { new BooksBaseModel { Id = 1, Titolo = "Titolo1", Autore = "Autore1", Anno = 2000, Disponibile = true } };
            yield return new object[] { new BooksBaseModel { Id = 2, Titolo = "Titolo2", Autore = "Autore2", Anno = 1999, Disponibile = true } };
        }

        public static IEnumerable<object[]> GetPutTestData()
        {
            yield return new object[] { 1, new BooksBaseModel { Id = 1, Titolo = "Titolo1", Autore = "Autore1", Anno = 2000, Disponibile = true } };
            yield return new object[] { 2, new BooksBaseModel { Id = 2, Titolo = "Titolo2", Autore = "Autore2", Anno = 1999, Disponibile = true } };
        }


    }
}

