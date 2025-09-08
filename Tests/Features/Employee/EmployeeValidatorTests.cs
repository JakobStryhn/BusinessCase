using Core.Features.Employee.DataTransferObjects;
using Core.Features.Employee.DataTransferObjects.Validator;
using FluentValidation.TestHelper;
using Xunit;

namespace Tests.Features.Employee;

public class CreateEmployeeRequestValidatorTests
{
    private readonly CreateEmployeeRequestValidator _validator;

    public CreateEmployeeRequestValidatorTests()
    {
        _validator = new CreateEmployeeRequestValidator();
    }

    public CreateEmployeeRequest testCase = new()
    {
        OfficeId = Guid.NewGuid(),
        FirstName = "Anders",
        LastName = "And",
        Birthdate = new DateOnly(2000, 1, 12)
    };

    [Fact]
    public void ShouldHaveError_WhenFirstNameIsEmpty()
    {
        // Arrange
        testCase.FirstName = string.Empty;

        // Act
        var result = _validator.TestValidate(testCase);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.FirstName);
    }

    [Fact]
    public void ShouldHaveError_WhenFirstNameIsTooLong()
    {
        // Arrange
        testCase.FirstName = new string('a', 101);

        // Act
        var result = _validator.TestValidate(testCase);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.FirstName);
    }

    [Fact]
    public void ShouldHaveError_WhenLastNameIsEmpty()
    {
        // Arrange
        testCase.LastName = string.Empty;

        // Act
        var result = _validator.TestValidate(testCase);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.LastName);
    }

    [Fact]
    public void ShouldHaveError_WhenLastNameIsTooLong()
    {
        // Arrange
        testCase.LastName = new string('a', 101);

        // Act
        var result = _validator.TestValidate(testCase);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.LastName);
    }

    [Fact]
    public void ShouldHaveError_WhenLastNameHasWhiteSpace()
    {
        // Arrange
        testCase.LastName = "Last Name";

        // Act
        var result = _validator.TestValidate(testCase);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.LastName);
    }

    [Fact]
    public void ShouldHaveError_WhenBirthdateIsDefault()
    {
        // Arrange
        testCase.Birthdate = new DateOnly();

        // Act
        var result = _validator.TestValidate(testCase);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Birthdate);
    }

    [Fact]
    public void ShouldHaveError_WhenEmployeeIsTooYoung()
    {
        // Arrange
        var tooYoungBirthdate = DateOnly.FromDateTime(DateTime.Today.AddYears(-15));
        testCase.Birthdate = tooYoungBirthdate;

        // Act
        var result = _validator.TestValidate(testCase);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Birthdate);
    }

    [Fact]
    public void ShouldHaveError_WhenEmployeeIsTooOld()
    {
        // Arrange
        var tooOldBirthdate = DateOnly.FromDateTime(DateTime.Today.AddYears(-121));
        testCase.Birthdate = tooOldBirthdate;

        // Act
        var result = _validator.TestValidate(testCase);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Birthdate);
    }

    [Fact]
    public void ShouldNotHaveError_WhenRequestIsValid()
    {
        // Arrange


        // Act
        var result = _validator.TestValidate(testCase);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }
}