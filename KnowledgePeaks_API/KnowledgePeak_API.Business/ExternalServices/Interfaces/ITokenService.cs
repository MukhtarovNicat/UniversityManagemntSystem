﻿using KnowledgePeak_API.Business.Dtos.TokenDtos;
using KnowledgePeak_API.Core.Entities;

namespace KnowledgePeak_API.Business.ExternalServices.Interfaces;

public interface ITokenService
{
    TokenResponseDto CreateDirectorToken(Director director, int expires = 60);
    TokenResponseDto CreateTeacherToken(Teacher teacher, int expires = 60);
    TokenResponseDto CreateStudentToken(Student student, int expires = 60);
    TokenResponseDto CreateTutorToken(Tutor tutor, int expires = 60);
    TokenResponseDto CreateAdminToken(Admin admin, int expires = 60);
    string CreateRefreshToken();
}
