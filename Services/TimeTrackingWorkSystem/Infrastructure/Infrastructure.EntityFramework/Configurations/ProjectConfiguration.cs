using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities;

namespace Infrastructure.EntityFramework.Configurations
{
	public class ProjectConfiguration : IEntityTypeConfiguration<Project>
	{
		public void Configure(EntityTypeBuilder<Project> builder)
		{
			builder.HasKey(x => x.Id);
			builder.Property(x => x.Id).ValueGeneratedOnAdd();
			builder.Property(x => x.Name).IsRequired();
			builder.Property(x => x.Code).IsRequired();
		}
	}
}
