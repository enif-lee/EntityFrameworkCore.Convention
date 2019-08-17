using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkCore.Convention
{
    public static class ModelBuilderHelper
    {
//        public static ModelBuilder UseSnakeCaseNamingConvention(this ModelBuilder builder)
//        {
//            builder.Model.GetEntityTypes()
//        }

        public static ModelBuilder UseNamingConvention(this ModelBuilder builder, Action<ConventionBuilder> configure)
        {
            var conventionBuilder = new ConventionBuilder();
            configure(conventionBuilder);
            if (!conventionBuilder.Validate(out var message))
                throw new ValidationException($"Failed to validate convention builder(cause : {message})");
            conventionBuilder.Apply(builder);
            return builder;
        }
    }
}