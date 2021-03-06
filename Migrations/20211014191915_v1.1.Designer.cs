// <auto-generated />
using AuthorsBooksDB;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AuthorsBooksDB.Migrations
{
    [DbContext(typeof(AuthorsBooksDBAppDbContext))]
    [Migration("20211014191915_v1.1")]
    partial class v11
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.11")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("AuthorsBooksDB.Author", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Surname")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Authors");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Jonas",
                            Surname = "Biliūnas"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Balys",
                            Surname = "Sruoga"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Žemaitė"
                        });
                });

            modelBuilder.Entity("AuthorsBooksDB.AuthorBook", b =>
                {
                    b.Property<int>("AuthorId")
                        .HasColumnType("int");

                    b.Property<int>("BookId")
                        .HasColumnType("int");

                    b.HasKey("AuthorId", "BookId");

                    b.HasIndex("BookId");

                    b.ToTable("AuthorBook");

                    b.HasData(
                        new
                        {
                            AuthorId = 1,
                            BookId = 3
                        },
                        new
                        {
                            AuthorId = 1,
                            BookId = 4
                        },
                        new
                        {
                            AuthorId = 2,
                            BookId = 2
                        },
                        new
                        {
                            AuthorId = 3,
                            BookId = 1
                        });
                });

            modelBuilder.Entity("AuthorsBooksDB.Book", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Books");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Title = "Marti"
                        },
                        new
                        {
                            Id = 2,
                            Title = "Dievų miškas"
                        },
                        new
                        {
                            Id = 3,
                            Title = "Kliudžiau"
                        },
                        new
                        {
                            Id = 4,
                            Title = "Liūdna pasaka"
                        });
                });

            modelBuilder.Entity("AuthorsBooksDB.AuthorBook", b =>
                {
                    b.HasOne("AuthorsBooksDB.Author", null)
                        .WithMany()
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AuthorsBooksDB.Book", null)
                        .WithMany()
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
