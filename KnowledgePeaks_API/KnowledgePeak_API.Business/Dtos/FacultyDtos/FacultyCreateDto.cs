﻿using FluentValidation;

namespace KnowledgePeak_API.Business.Dtos.FacultyDtos;

public record FacultyCreateDto
{
    public string Name { get; set; }
    public string ShortName { get; set; }
}
public class FacultyCreateDtoValidator : AbstractValidator<FacultyCreateDto>
{
    public FacultyCreateDtoValidator()
    {
        RuleFor(f => f.Name)
            .NotNull()
            .WithMessage("Faculty name not be null")
            .NotEmpty()
            .WithMessage("Faculty name not be empty")
            .MinimumLength(2)
            .WithMessage("Faculty name length greather than 2");
        RuleFor(f => f.ShortName)
           .NotNull()
           .WithMessage("Faculty ShortName not be null")
           .NotEmpty()
           .WithMessage("Faculty ShortName not be empty")
           .MinimumLength(2)
           .WithMessage("Faculty ShortName length greather than 2");
    }
}
