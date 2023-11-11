﻿using KnowledgePeak_API.Business.Dtos.GroupDtos;
using KnowledgePeak_API.Business.Dtos.LessonDtos;
using KnowledgePeak_API.Business.Dtos.TeacherDtos;
using KnowledgePeak_API.Business.Dtos.TutorDtos;

namespace KnowledgePeak_API.Business.Dtos.SpecialityDtos;

public record SpecialityDetailDto
{
    public int Id { get; set; }
    public bool IsDeleted { get; set; }
    public string Name { get; set; }
    public string ShortName { get; set; }
    public int? FacultyId { get; set; }
    public DateTime CreateTime { get; set; }
    public IEnumerable<SpecialityLessonDto> LessonSpecialities { get; set; }
    public IEnumerable<GroupListItemDto> Groups { get; set; }
    public List<TeacherFacultyDto> TeacherSpecialities { get; set; }
    public List<TutorInfoDto> Tutors { get; set; }
}
