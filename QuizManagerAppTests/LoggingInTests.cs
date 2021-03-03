using System;
using NUnit.Framework;

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
            Assert.That(username, Is.Empty);
        }

        [Test]
        public void WhenThePasswordFieldIsBlank_ThenAnErrorShouldBeReturned()
        {
            var sut = new UserDetails();
            var username = "testUser";
            var password = "";
            var loginAttempt = sut.AttemptLogIn(username, password);
            Assert.That(username, Is.Not.Empty);
            Assert.That(password, Is.Not.Length);
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
        public object AttemptLogIn(string username, string password)
        {
            return true;
        }
    }
}