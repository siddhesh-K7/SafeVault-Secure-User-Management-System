using Xunit;
using SafeVault.DTOs;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace SafeVault.Tests;

public class InputValidationTests
{
    [Fact]
    public void Register_WeakPassword_FailsValidation()
    {
        var request = new RegisterRequest { Username = "admin", Email = "test@test.com", Password = "weak", DisplayName = "Test" };
        var context = new ValidationContext(request);
        var results = new List<ValidationResult>();

        var isValid = Validator.TryValidateObject(request, context, results, true);

        Assert.False(isValid);
    }
}