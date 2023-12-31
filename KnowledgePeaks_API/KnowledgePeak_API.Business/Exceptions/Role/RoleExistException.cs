﻿using KnowledgePeak_API.Business.Exceptions.Commons;
using Microsoft.AspNetCore.Http;

namespace KnowledgePeak_API.Business.Exceptions.Role;

public class RoleExistException : Exception, IBaseException
{
    public int StatusCode => StatusCodes.Status400BadRequest;
    public string ErrorMessage { get; }
    public RoleExistException()
    {
        ErrorMessage = "Role is Already Exist";
    }
    public RoleExistException(string? message) : base(message)
    {
        ErrorMessage = message;
    }
}
