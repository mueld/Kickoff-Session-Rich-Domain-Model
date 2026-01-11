using Noser_Fitness.Domain;

namespace Noser_Fitness_Application.Abstractions;

internal interface IEmailService
{
    Task SendEmail(Member member, Course course);
}
