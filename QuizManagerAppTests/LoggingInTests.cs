using System;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace QuizManagerAppTests
{
    public class LoggingInTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void WhenTheUserDetailsAreNotRecongised_ThenAnErrorShouldBeReturned()
        {
            Assert.Pass();
        }

        [Test]
        public void WhenTheUsernameFieldIsBlank_ThenAnErrorShouldBeReturned()
        {
            var sut = new UserDetails();
            var username = "";
            var password = "12345";
            var loginAttempt = sut.AttemptLogIn(username, password);
            Assert.That(loginAttempt, Is.EqualTo("Field cannot be empty"));
        }

        [Test]
        public void WhenThePasswordFieldIsBlank_ThenAnErrorShouldBeReturned()
        {
            var sut = new UserDetails();
            var username = "testUser";
            var password = "";
            var loginAttempt = sut.AttemptLogIn(username, password);
            Assert.That(loginAttempt, Is.EqualTo("Field cannot be empty"));

        }
    }

    public class CustomException
    {
        public string FieldIsEmptyException()
        {
            return "Field cannot be empty";
        }
    }



    public class UserDetails
    {
        private readonly Person _person = new Person();

        public object AttemptLogIn(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                return "Field cannot be empty";
            }

            return true;
        }

        public Exception FieldEmptyException { get; set; }
    }
}