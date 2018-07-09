using System.Data.Entity;
using ApiContent.Models;

namespace ApiContent.DataAccess
{
    public class DataContext : DbContext
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //    modelBuilder.Entity<ContentAreaType>()
            //        .HasMany(c => c.Parents)
            //        .WithMany(c => c.Children)
            //        .Map(m =>
            //        {
            //            m.MapLeftKey("ChildId");
            //            m.MapRightKey("ParentId");
            //            m.ToTable("ContentAreaTypeLinks");
            //        });

                modelBuilder.Entity<Content>()
                    .HasRequired(c => c.Template)
                    .WithMany()
                    .WillCascadeOnDelete(false);

                base.OnModelCreating(modelBuilder);
            }
        public DbSet<User> Users { get; set; }
        public DbSet<Text> Texts { get; set; }

        public DbSet<TextLang> TextLangs { get; set; }

        public DbSet<Content> Contents { get; set; }

        public DbSet<ContentLibraryItem> ContentLibraryItems { get; set; }

        public DbSet<ContentText> ContentTexts { get; set; }

        public DbSet<Page> Pages { get; set; }

        public DbSet <PageMenuItem> PageMenuItems { get; set; }

        public DbSet<PageContent> PageContents { get; set; }

        public DbSet<ContentTemplate> ContentTemplates { get; set; }
    }
}