using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Infrastructure;

namespace EntityFrameworkCore.Convention.Test.Infrastructure
{
	public class UncachedModelSource : IModelSource
	{
	    /// <summary>
	    ///     Creates a new <see cref="T:Microsoft.EntityFrameworkCore.Infrastructure.ModelSource" /> instance.
	    /// </summary>
	    /// <param name="dependencies"> The dependencies to use. </param>
	    public UncachedModelSource([NotNull] ModelSourceDependencies dependencies)
	    {
	      Dependencies = dependencies;
	    }

	    /// <summary>
	    ///     Dependencies used to create a <see cref="T:Microsoft.EntityFrameworkCore.Infrastructure.ModelSource" />
	    /// </summary>
	    protected virtual ModelSourceDependencies Dependencies { get; }

	    /// <summary>
	    ///     Returns the model from the cache, or creates a model if it is not present in the cache.
	    /// </summary>
	    /// <param name="context"> The context the model is being produced for. </param>
	    /// <param name="conventionSetBuilder"> The convention set to use when creating the model. </param>
	    /// <returns> The model to be used. </returns>
	    public virtual IModel GetModel(
	      DbContext context,
	      IConventionSetBuilder conventionSetBuilder)
	    {
		    var modelBuilder = new ModelBuilder(conventionSetBuilder.CreateConventionSet());
		    Dependencies.ModelCustomizer.Customize(modelBuilder, context);
		    return modelBuilder.FinalizeModel();
	    }
	}
}