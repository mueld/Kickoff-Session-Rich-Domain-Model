using System;
using System.Collections.Generic;
using System.Text;

namespace Noser_Fitness_Application.Abstractions;

internal interface IEmailService
{
    Task SendEmail(string email, string Text);
}
