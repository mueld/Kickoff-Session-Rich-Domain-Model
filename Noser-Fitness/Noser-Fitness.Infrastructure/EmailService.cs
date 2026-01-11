using Noser_Fitness_Application.Abstractions;
using Noser_Fitness.Domain;

namespace Noser_Fitness.Infrastructure;

internal class EmailService : IEmailService
{
    public Task SendEmail(Member member, Course course)
    {
        return Task.CompletedTask;
    }
}
