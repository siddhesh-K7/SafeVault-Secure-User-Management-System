using Xunit;
using SafeVault.DTOs;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace SafeVault.Tests;

public class SqlInjectionTests
{
    [Fact]
    public void Register_SqlInjectionPayload_FailsValidation()
    {
        var request = new RegisterRequest { Username = "admin'--", Email = "test@test.com", Password = "Valid1!@", DisplayName = "Test" };
        var context = new ValidationContext(request);
        var results = new List<ValidationResult>();

        var isValid = Validator.TryValidateObject(request, context, results, true);

        // Should fail due to alphanumeric regex on Username
        Assert.False(isValid);
    }
}