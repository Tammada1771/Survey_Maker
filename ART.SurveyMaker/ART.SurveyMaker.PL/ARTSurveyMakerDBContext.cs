using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace ART.SurveyMaker.PL
{
    public partial class SurveyMakerEntities : DbContext
    {
        public SurveyMakerEntities()
        {
        }

        public SurveyMakerEntities(DbContextOptions<SurveyMakerEntities> options)
            : base(options)
        {
        }

        public virtual DbSet<tblAnswer> tblAnswers { get; set; }
        public virtual DbSet<tblQuestion> tblQuestions { get; set; }
        public virtual DbSet<tblQuestionAnswer> tblQuestionAnswers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=(localdb)\\ProjectsV13;Database=ART.SurveyMaker.DB;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<tblAnswer>(entity =>
            {
                entity.ToTable("tblAnswer");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Answer)
                    .IsRequired()
                    .IsUnicode(false);
            });

            modelBuilder.Entity<tblQuestion>(entity =>
            {
                entity.ToTable("tblQuestion");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Question)
                    .IsRequired()
                    .IsUnicode(false);
            });

            modelBuilder.Entity<tblQuestionAnswer>(entity =>
            {
                entity.ToTable("tblQuestionAnswer");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne(d => d.Answer)
                    .WithMany(p => p.tblQuestionAnswers)
                    .HasForeignKey(d => d.AnswerId)
                    .HasConstraintName("tblQuestionAnswer_AnswerId");

                entity.HasOne(d => d.Question)
                    .WithMany(p => p.tblQuestionAnswers)
                    .HasForeignKey(d => d.QuestionId)
                    .HasConstraintName("tblQuestionAnswer_QuestionId");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
