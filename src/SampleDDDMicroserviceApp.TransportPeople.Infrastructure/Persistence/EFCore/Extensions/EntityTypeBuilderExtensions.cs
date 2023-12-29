using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SampleDDDMicroserviceApp.TransportPeople.Domain.SharedKernel.Entity;

namespace SampleDDDMicroserviceApp.TransportPeople.Infrastructure.Persistence.EFCore.Extensions;

public static class EntityTypeBuilderExtensions
{
    public static IndexBuilder HasUniqueIndexArchivable<TEntity>(
        this EntityTypeBuilder<TEntity> builder,
        Expression<Func<TEntity, object?>> indexExpression
    )
        where TEntity : class, IArchivableEntity
    {
        return builder
            .HasIndex(indexExpression)
            .HasFilter($"{nameof(IArchivableEntity.IsArchived)} = 0")
            .IsUnique();
    }
}