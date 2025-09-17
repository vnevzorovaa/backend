namespace JobsService.DTOs;

public record CreateJobDto(Guid EmployerId, string Title, string Description, string Requirements, int SalaryMin, int SalaryMax, int ExperienceRequired, string Industry);