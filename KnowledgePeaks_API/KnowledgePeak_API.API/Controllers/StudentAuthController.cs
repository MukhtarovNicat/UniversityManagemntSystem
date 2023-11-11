﻿using KnowledgePeak_API.Business.Dtos.RoleDtos;
using KnowledgePeak_API.Business.Dtos.StudentDtos;
using KnowledgePeak_API.Business.ExternalServices.Interfaces;
using KnowledgePeak_API.Business.Services.Interfaces;
using KnowledgePeak_API.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace KnowledgePeak_API.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StudentAuthController : ControllerBase
{
    readonly IStudentService _service;
    readonly UserManager<Student> _userManager;
    readonly IEmailService _emailService;

    public StudentAuthController(IStudentService service, UserManager<Student> userManager, IEmailService emailService)
    {
        _service = service;
        _userManager = userManager;
        _emailService = emailService;
    }

    [HttpPost("[action]")]
    [Authorize(Roles = "SuperAdmin,Admin")]
    public async Task<IActionResult> Create([FromForm] StudentCreateDto dto)
    {
        await _service.CreateAsync(dto);
        return Ok();
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> Login([FromForm] StudentLoginDto dto)
    {
        return Ok(await _service.LoginAsync(dto));
    }

    [HttpPost("[action]")]
    [Authorize(Roles = "Student")]
    public async Task<IActionResult> LoginWithRefreshToken(string token)
    {
        return Ok(await _service.LoginWithRefreshToken(token));
    }

    [HttpPut("[action]")]
    [Authorize(Roles = "Student")]
    public async Task<IActionResult> UpdateProfile([FromForm] StudentUpdateDto dto)
    {
        await _service.UpdateAsync(dto);
        return Ok();
    }

    [HttpPut("[action]")]
    [Authorize(Roles = "SuperAdmin,Admin")]
    public async Task<IActionResult> UpdateProfileAdmin([FromForm] StudentAdminUpdateDto dto, string userName)
    {
        await _service.UpdatPrfileFromAdmin(userName, dto);
        return Ok();
    }

    [HttpPost("[action]")]
    [Authorize(Roles = "SuperAdmin,Admin")]
    public async Task<IActionResult> AddRole([FromForm] AddRoleDto dto)
    {
        await _service.AddRole(dto);
        return Ok();
    }

    [HttpPost("[action]")]
    [Authorize(Roles = "SuperAdmin,Admin")]
    public async Task<IActionResult> RemoveRole([FromForm] RemoveRoleDto dto)
    {
        await _service.RemoveRole(dto);
        return Ok();
    }

    [HttpGet("[action]")]
    [Authorize(Roles = "SuperAdmin,Admin,Director,Tutor,Teacher")]
    public async Task<IActionResult> Get()
    {
        return Ok(await _service.GetAll(true));
    }

    [HttpGet("[action]/{id}")]
    [Authorize(Roles = "SuperAdmin,Admin,Student,Director,Tutor,Teacher")]
    public async Task<IActionResult> GetById(string id)
    {
        return Ok(await _service.GetByIdAsync(id, true));
    }

    [HttpGet("[action]")]
    [Authorize(Roles = "SuperAdmin,Admin,Student,Director,Tutor,Teacher")]
    public async Task<IActionResult> GetByUserName(string userName)
    {
        return Ok(await _service.GetByUserNameAsync(userName, true));
    }

    [HttpDelete("[action]")]
    [Authorize(Roles = "SuperAdmin,Admin")]
    public async Task<IActionResult> Delete(string userName)
    {
        await _service.DeleteAsync(userName);
        return Ok();
    }

    [HttpPost("[action]")]
    [Authorize(Roles = "Student")]
    public async Task<IActionResult> SignOut()
    {
        await _service.SignOut();
        return Ok();
    }

    [HttpGet("[action]")]
    public async Task<IActionResult> Count()
    {
        return Ok(await _service.StudentCount());
    }
}
