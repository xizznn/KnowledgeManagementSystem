using AutoMapper;
using KnowledgeManagementSystem.Core.DTOs;
using KnowledgeManagementSystem.Core.Entities;

namespace KnowledgeManagementSystem.API.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserEntity, UserDto>()
                .ForMember(dest => dest.RoleTitle, opt => opt.MapFrom(src => src.Role.Title));

            CreateMap<UserEntity, UserProfileDto>()
                .ForMember(dest => dest.RoleTitle, opt => opt.MapFrom(src => src.Role.Title))
                .ForMember(dest => dest.EnrolledCourses,
                    opt => opt.MapFrom(src => src.Courses.Select(uc => uc.Course)));

            CreateMap<RegisterUserDto, UserEntity>()
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
                .ForMember(dest => dest.PasswordSalt, opt => opt.Ignore())
                .ForMember(dest => dest.Role, opt => opt.Ignore())
                .ForMember(dest => dest.Courses, opt => opt.Ignore());

            CreateMap<UpdateUserDto, UserEntity>()
                .ForMember(dest => dest.Email, opt => opt.Condition(src => src.Email != null))
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
                .ForMember(dest => dest.PasswordSalt, opt => opt.Ignore())
                .ForMember(dest => dest.Role, opt => opt.Ignore())
                .ForMember(dest => dest.Courses, opt => opt.Ignore());

            CreateMap<LoginUserDto, UserEntity>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Name, opt => opt.Ignore())
                .ForMember(dest => dest.Surname, opt => opt.Ignore())
                .ForMember(dest => dest.DateOfBirth, opt => opt.Ignore())
                .ForMember(dest => dest.RoleId, opt => opt.Ignore())
                .ForMember(dest => dest.Role, opt => opt.Ignore())
                .ForMember(dest => dest.Courses, opt => opt.Ignore());

            CreateMap<RoleEntity, RoleDto>().ReverseMap();
            CreateMap<CreateRoleDto, RoleEntity>();
            CreateMap<UpdateRoleDto, RoleEntity>();

            CreateMap<CourseEntity, CourseDto>()
                .ReverseMap()
                .ForMember(dest => dest.AddedAt, opt => opt.Ignore())
                .ForMember(dest => dest.EditedAt, opt => opt.Ignore());

            CreateMap<CreateCourseDto, CourseEntity>()
                .ForMember(dest => dest.AddedAt, opt => opt.Ignore())
                .ForMember(dest => dest.EditedAt, opt => opt.Ignore());

            CreateMap<UpdateCourseDto, CourseEntity>()
                .ForMember(dest => dest.AddedAt, opt => opt.Ignore())
                .ForMember(dest => dest.EditedAt, opt => opt.Ignore());
            CreateMap<CourseDto, UpdateCourseDto>();
            CreateMap<TestEntity, TestDto>()
                .ForMember(dest => dest.CourseTitles,
                    opt => opt.MapFrom(src => src.Courses.Select(tc => tc.Course.Title).ToList()))
                .ReverseMap()
                .ForMember(dest => dest.AddedAt, opt => opt.Ignore())
                .ForMember(dest => dest.EditedAt, opt => opt.Ignore());

            CreateMap<CreateTestDto, TestEntity>()
                .ForMember(dest => dest.AddedAt, opt => opt.Ignore())
                .ForMember(dest => dest.EditedAt, opt => opt.Ignore());

            CreateMap<UpdateTestDto, TestEntity>()
                .ForMember(dest => dest.AddedAt, opt => opt.Ignore())
                .ForMember(dest => dest.EditedAt, opt => opt.Ignore());
            CreateMap<TestDto, UpdateTestDto>();

            // Other mappings
            CreateMap<UserOnCourseEntity, UserOnCourseDto>().ReverseMap();
            CreateMap<CreateUserOnCourseDto, UserOnCourseEntity>();

            CreateMap<QuestionEntity, QuestionDto>();
            CreateMap<CreateQuestionDto, QuestionEntity>();
            CreateMap<UpdateQuestionDto, QuestionEntity>();

            CreateMap<QuestionInTestEntity, QuestionInTestDto>().ReverseMap();
            CreateMap<CreateQuestionInTestDto, QuestionInTestEntity>();

            CreateMap<TestInCourseEntity, TestInCourseDto>().ReverseMap();
            CreateMap<CreateTestInCourseDto, TestInCourseEntity>();

            CreateMap<FavoriteCourseEntity, FavoriteCourseDto>();
            CreateMap<CreateFavoriteDto, FavoriteCourseEntity>();

            CreateMap<TestResultEntity, TestResultDto>()
                .ForMember(dest => dest.UserFullName, opt => opt.MapFrom(src => $"{src.User.Name} {src.User.Surname}"))
                .ForMember(dest => dest.TestTitle, opt => opt.MapFrom(src => src.Test.Title));

            CreateMap<CreateTestResultDto, TestResultEntity>();

            CreateMap<FavoriteTestEntity, FavoriteTestDto>();
            CreateMap<CreateFavoriteTestDto, FavoriteTestEntity>();
        }
    }
}