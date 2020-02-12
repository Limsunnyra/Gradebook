using System;
using Xunit;

namespace GradeBook.Tests
{
    public delegate string WriteLogDelegate(string logMessage);
    public class TypeTest
    {
        int count = 0;
        [Fact]
        public void WriteLogDelegateCanPointToMethod()
        {
            WriteLogDelegate log = ReturnMessage;

            log += ReturnMessage;
            log += IncrementCount;

            var result = log("Hello!");
            Assert.Equal("hello!", result);
            Assert.Equal(3, count);
        }

        string IncrementCount(string message)
        {
            count++;
            return message.ToLower();
        }
        string ReturnMessage(string message)
        {
            count++;
            return message;
        }

        [Fact]
        public void ValueTypesAlsoPassByValue()
        {
            var x = GetInt();
            SetInt(ref x);
            Assert.Equal(42, x);
        }

        private void SetInt(ref int x)
        {
            x = 42;
        }

        private int GetInt()
        {
            return 3;
        }
        [Fact]
        public void StringsBehaveLikeValuetypes()
        {
            string name = "sunny";
            var upper = MakeUpperCase(name);
            Assert.Equal("sunny", name);
            Assert.Equal("SUNNY", upper);
        }

        private string MakeUpperCase(string parameter)
        {
            return parameter.ToUpper();
        }

        [Fact]
        public void CsharpCanPassByref()
        {
            //arrangexc
            var book1 = GetBook("Book1");
            GetBookSetName(ref book1, "New Name");

            Assert.Equal("New Name", book1.Name);
        }

        private void GetBookSetName(ref InMemoryBook book, string name)
        {
            book = new InMemoryBook(name);
            book.Name = name;
        }

        [Fact]
        public void CsharpisPassByValue()
        {
            //arrangexc
            var book1 = GetBook("Book1");
            GetBookSetName(book1, "New Name");

            Assert.Equal("Book1", book1.Name);
        }

        private void GetBookSetName(InMemoryBook book, string name)
        {
            book= new InMemoryBook(name);
            book.Name = name;
        }

        [Fact]
        public void CanSetNameFromReference()
        {
            //arrangexc
            var book1 = GetBook("Book1");
            SetName(book1, "New Name");

            Assert.Equal("New Name", book1.Name);
        }

        private void SetName(InMemoryBook book, string name)
        {
            book.Name = name;
        }

        [Fact]
        public void GetBookReturnsdifferentOjects()
        {
            //arrange
            var book1 = GetBook("Book1");
            var book2 = GetBook("Book2");

            //act
            Assert.Equal("Book1", book1.Name);
            Assert.Equal("Book2", book2.Name);
        }
        [Fact]
        public void TwoVarsCanReferenceSameObject()
        {
            //arrange
            var book1 = GetBook("Book1");
            var book2 = book1;

            //act
            Assert.Equal("Book1", book1.Name);
            Assert.Equal("Book1", book2.Name);

            Assert.Same(book1, book2);
            Assert.True(Object.ReferenceEquals(book1, book2));
        }

        InMemoryBook GetBook(string name)
        {
            return new InMemoryBook(name);
        }

    }
}
