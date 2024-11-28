using OpenAiBlazorApp.Infrastructure.Security;
using Xunit;

namespace OpenAiBlazorApp.Tests
{
    public class PasswordHasherTests
    {
        private readonly PasswordHasher _passwordHasher;

        public PasswordHasherTests()
        {
            _passwordHasher = new PasswordHasher();
        }

        [Fact]
        public void HashPassword_ShouldReturnHashedPassword()
        {
            // Arrange
            string password = "TestPassword123";

            // Act
            string hashedPassword = _passwordHasher.HashPassword(password);

            // Assert
            Assert.False(string.IsNullOrEmpty(hashedPassword));
            Assert.NotEqual(password, hashedPassword);
        }

        [Fact]
        public void VerifyPassword_ShouldReturnTrueForValidPassword()
        {
            // Arrange
            string password = "TestPassword123";
            string hashedPassword = _passwordHasher.HashPassword(password);

            // Act
            bool isValid = _passwordHasher.VerifyPassword(password, hashedPassword);

            // Assert
            Assert.True(isValid);
        }

        [Fact]
        public void VerifyPassword_ShouldReturnFalseForInvalidPassword()
        {
            // Arrange
            string password = "TestPassword123";
            string hashedPassword = _passwordHasher.HashPassword(password);
            string wrongPassword = "WrongPassword123";

            // Act
            bool isValid = _passwordHasher.VerifyPassword(wrongPassword, hashedPassword);

            // Assert
            Assert.False(isValid);
        }
    }
}
