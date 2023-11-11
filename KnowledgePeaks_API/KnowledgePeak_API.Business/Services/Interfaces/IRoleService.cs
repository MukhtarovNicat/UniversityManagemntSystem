﻿using KnowledgePeak_API.Business.Dtos.RoleDtos;
using Microsoft.AspNetCore.Identity;

namespace KnowledgePeak_API.Business.Services.Interfaces;

public interface IRoleService
{
    Task<IEnumerable<IdentityRole>> GetAllAsync();
    Task<string> GetByIdAsync(string id);
    Task CreateAsync(string name);
    Task UpdateAsync(string id, string name);
    Task RemoveAsync(string id);

}
