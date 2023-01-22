using CustomerMicroservice.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerMicroservice.Tests
{
    public class TestData
    {
        public static IEnumerable<object[]> GetPostTestData()
        {
            yield return new object[] { new CustomersBaseModel { Id = 1, Nome = "Nome1", Cognome = "Cognome1", Mail = "mail1@mail.com", Telefono = "3429388493" } };
            yield return new object[] { new CustomersBaseModel { Id = 2, Nome = "Nome2", Cognome = "Cognome2", Mail = "mail2@mail.com", Telefono = "3492388493" } };
        }

        public static IEnumerable<object[]> GetPutTestData()
        {
            yield return new object[] { 1, new CustomersBaseModel { Id = 1, Nome = "Nome1", Cognome = "Cognome1", Mail = "mail1@mail.com", Telefono = "3429388493" } };
            yield return new object[] { 2, new CustomersBaseModel { Id = 2, Nome = "Nome2", Cognome = "Cognome2", Mail = "mail2@mail.com", Telefono = "3492388493" } };
        }

    }
}
