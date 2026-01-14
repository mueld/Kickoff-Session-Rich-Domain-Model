using System;
using System.Collections.Generic;
using System.Text;
using Mediator;
using Microsoft.EntityFrameworkCore.Storage;
using Noser_Fitness.Domain.Abstractions;

namespace Noser_Fitness_Application.Abstractions.Behaviors;

public sealed class TransactionBehavior<TCommand, TResponse>(INoserFitnessDbContext dbContext)
    : IPipelineBehavior<TCommand, TResponse>
    where TCommand : ICommand
{
    private readonly INoserFitnessDbContext _dbContext = dbContext;

    public async ValueTask<TResponse> Handle(
        TCommand command,
        MessageHandlerDelegate<TCommand, TResponse> next,
        CancellationToken cancellationToken
    )
    {
        if (_dbContext.Database.CurrentTransaction is not null)
        {
            var responseNested = await next(command, cancellationToken);

            await _dbContext.SaveChangesAsync(cancellationToken);
            return responseNested;
        }

        await using IDbContextTransaction tx = await _dbContext.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            var response = await next(command, cancellationToken);

            await _dbContext.SaveChangesAsync(cancellationToken);
            await tx.CommitAsync(cancellationToken);

            return response;
        }
        catch
        {
            await tx.RollbackAsync(cancellationToken);
            throw;
        }
    }
}
