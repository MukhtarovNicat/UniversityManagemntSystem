﻿using FluentValidation;
using KnowledgePeak_API.Business.Validators;
using KnowledgePeak_API.Core.Enums;
using Microsoft.AspNetCore.Http;
using System.Text.RegularExpressions;

namespace KnowledgePeak_API.Business.Dtos.AdminDtos;

public record AdminCreateDto
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public int Age { get; set; }
    public IFormFile? ImageFile { get; set; }
    public Gender Gender { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}
public class AdminCreateDtoValidator : AbstractValidator<AdminCreateDto>
{
    public AdminCreateDtoValidator()
    {
        RuleFor(t => t.Name)
           .NotNull()
           .WithMessage("Admin Name dont be Null")
           .NotEmpty()
           .WithMessage("Admin Name dont be Empty")
           .MinimumLength(2)
           .WithMessage("Admin Name length must be greather than 2")
           .MaximumLength(25)
           .WithMessage("Admin Name length must be less than 25");
        RuleFor(t => t.Surname)
           .NotNull()
           .WithMessage("Admin Surname dont be Null")
           .NotEmpty()
           .WithMessage("Admin Surname dont be Empty")
           .MinimumLength(2)
           .WithMessage("Admin Surname length must be greather than 2")
           .MaximumLength(30)
           .WithMessage("Admin Surname length must be less than 30");
        RuleFor(t => t.ImageFile)
           .SetValidator(new FileValidator());
        RuleFor(t => t.Age)
            .NotNull()
            .WithMessage("Admin Age dont be Null")
            .NotEmpty()
            .WithMessage("Admin Age dont be Empty")
            .GreaterThan(18)
            .WithMessage("Admin Age must be greather than 18");
        RuleFor(t => t.Gender)
            .Must(ValidateGender)
            .WithMessage("Ivalid gender ");
        RuleFor(t => t.Email)
           .NotNull()
           .WithMessage("Admin Email dont be Null")
           .NotEmpty()
           .WithMessage("Admin Email dont be Empty")
            .Must(t =>
            {
                Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
                var result = regex.Match(t);
                return result.Success;
            })
           .WithMessage("Please enter valid email adress");
        RuleFor(t => t.UserName)
           .NotNull()
           .WithMessage("Admin UserName dont be Null")
           .NotEmpty()
           .WithMessage("Admin UserName dont be Empty")
           .MinimumLength(3)
           .WithMessage("Admin UserName length must be greather than 3")
           .MaximumLength(45)
           .WithMessage("Admin UserName length must be less than 45");
        RuleFor(t => t.Password)
           .NotNull()
           .WithMessage("Admin Password dont be Null")
           .NotEmpty()
           .WithMessage("Admin Password dont be Empty")
           .MinimumLength(6)
           .WithMessage("Admin Password length must be greather than 6");
    }
    private bool ValidateGender(Gender gender)
    {
        return Enum.IsDefined(typeof(Gender), gender);
    }
}
