﻿using AutoMapper;
using KnowledgePeak_API.Business.Dtos.FacultyDtos;
using KnowledgePeak_API.Business.Dtos.TeacherDtos;
using KnowledgePeak_API.Business.Exceptions.Commons;
using KnowledgePeak_API.Business.Exceptions.Faculty;
using KnowledgePeak_API.Business.Services.Interfaces;
using KnowledgePeak_API.Core.Entities;
using KnowledgePeak_API.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace KnowledgePeak_API.Business.Services.Implements;

public class FacultyService : IFacultyService
{
    readonly IFacultyRepository _repo;
    readonly IMapper _mapper;

    public FacultyService(IFacultyRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    public async Task CreateAsync(FacultyCreateDto dto)
    {
        var nameExist = await _repo.IsExistAsync(f => f.Name == dto.Name);
        if (nameExist) throw new FacultyNameIsExistException();

        var map = _mapper.Map<Faculty>(dto);
        await _repo.CreateAsync(map);
        await _repo.SaveAsync();
    }

    public async Task DeleteAsync(int id)
    {
        if (id <= 0) throw new IdIsNegativeException<Faculty>();
        var entity = await _repo.FIndByIdAsync(id, "Specialities", "TeacherFaculties", "TeacherFaculties.Teacher", "Rooms");
        if (entity == null) throw new NotFoundException<Faculty>();

        if (entity.Specialities != null)
        {
            if (entity.Specialities.Count() > 0) throw new FacultySpecialitiesNotEmptyException();
        }
        if (entity.TeacherFaculties != null)
        {
            if (entity.TeacherFaculties.Count() > 0) throw new FacultyTeachersNotEmptyException();
        }
        if (entity.Rooms != null)
        {
            if (entity.Rooms.Count() > 0) throw new FacultyTeachersNotEmptyException("Faculty Room Not Empty");
        }
        await _repo.DeleteAsync(id);
        await _repo.SaveAsync();
    }

    public async Task<int> FacultyCount()
    {
        var data = await _repo.GetAll().ToListAsync();
        return data.Count();
    }

    public async Task<IEnumerable<FacultyListItemDto>> GetAllAsync(bool takeAll)
    {
        List<Teacher> teacher = new();
        var dto = new List<FacultyListItemDto>();
        var data = _repo.GetAll("Specialities", "TeacherFaculties", "TeacherFaculties.Teacher","Rooms");
        if (takeAll)
        {
            foreach (var item in data)
            {
                teacher.Clear();
                foreach (var items in item.TeacherFaculties)
                {
                    teacher.Add(items.Teacher);
                }
                var dtoItem = _mapper.Map<FacultyListItemDto>(item);
                dtoItem.Teacher = _mapper.Map<List<TeacherDetailDto>>(teacher);
                dto.Add(dtoItem);
            }
        }
        else
        {
            var additionalEntities = await data.Where(b => b.IsDeleted).ToListAsync();
            foreach (var item in additionalEntities)
            {
                teacher.Clear();
                foreach (var items in item.TeacherFaculties)
                {
                    teacher.Add(items.Teacher);
                }
                var dtoItem = _mapper.Map<FacultyListItemDto>(item);
                dtoItem.Teacher = _mapper.Map<List<TeacherDetailDto>>(teacher);
                dto.Add(dtoItem);
            }
        }
        return dto;
    }

    public async Task<FacultyDetailDto> GetByIdAsync(int id, bool takeAll)
    {
        Faculty? entity;
        if (id <= 0) throw new IdIsNegativeException<Faculty>();
        if (!takeAll)
        {
            entity = await _repo.GetSingleAsync(f => f.Id == id && f.IsDeleted == false,
                "Specialities", "TeacherFaculties", "TeacherFaculties.Teacher","Rooms");
            if (entity == null) throw new NotFoundException<Faculty>();
        }
        else
        {
            entity = await _repo.GetSingleAsync(f => f.Id == id,
                "Specialities", "TeacherFaculties", "TeacherFaculties.Teacher", "Rooms");
            if (entity == null) throw new NotFoundException<Faculty>();
        }
        return _mapper.Map<FacultyDetailDto>(entity);
    }

    public async Task RevertSoftDeleteAsync(int id)
    {
        if (id <= 0) throw new IdIsNegativeException<Faculty>();
        var entity = await _repo.FIndByIdAsync(id);
        if (entity == null) throw new NotFoundException<Faculty>();

        _repo.RevertSoftDelete(entity);
        await _repo.SaveAsync();
    }

    public async Task SoftDeleteAsync(int id)
    {
        if (id <= 0) throw new IdIsNegativeException<Faculty>();
        var entity = await _repo.FIndByIdAsync(id, "Specialities", "TeacherFaculties", "TeacherFaculties.Teacher");
        if (entity == null) throw new NotFoundException<Faculty>();

        if (entity.Specialities != null)
        {
            if (entity.Specialities.Count() > 0) throw new FacultySpecialitiesNotEmptyException();
        }
        if (entity.TeacherFaculties != null)
        {
            if (entity.TeacherFaculties.Count() > 0) throw new FacultyTeachersNotEmptyException();
        }

        _repo.SoftDelete(entity);
        await _repo.SaveAsync();
    }

    public async Task UpdateAsync(int id, FacultyUpdateDto dto)
    {
        if (id <= 0) throw new IdIsNegativeException<Faculty>();
        var entity = await _repo.FIndByIdAsync(id);
        if (entity == null) throw new NotFoundException<Faculty>();

        var exist = await _repo.IsExistAsync(f => f.Name == dto.Name && f.Id != id);
        if (exist) throw new FacultyNameIsExistException();

        _mapper.Map(dto, entity);
        await _repo.SaveAsync();
    }
}
