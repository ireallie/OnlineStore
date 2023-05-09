﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OnlineStore.Data;

#nullable disable

namespace OnlineStore.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class DatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.16")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("OnlineStore.Data.Contracts.Entities.Products.Option", b =>
                {
                    b.Property<int>("OptionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OptionId"), 1L, 1);

                    b.Property<DateTimeOffset>("CreatedDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<DateTimeOffset?>("UpdatedDate")
                        .HasColumnType("datetimeoffset");

                    b.HasKey("OptionId");

                    b.HasIndex("ProductId");

                    b.ToTable("Options");

                    b.HasData(
                        new
                        {
                            OptionId = 1,
                            CreatedDate = new DateTimeOffset(new DateTime(2023, 4, 25, 17, 7, 4, 110, DateTimeKind.Unspecified).AddTicks(3052), new TimeSpan(0, 0, 0, 0, 0)),
                            Name = "Size",
                            ProductId = 1
                        },
                        new
                        {
                            OptionId = 2,
                            CreatedDate = new DateTimeOffset(new DateTime(2023, 4, 25, 17, 7, 4, 110, DateTimeKind.Unspecified).AddTicks(3054), new TimeSpan(0, 0, 0, 0, 0)),
                            Name = "Color",
                            ProductId = 1
                        });
                });

            modelBuilder.Entity("OnlineStore.Data.Contracts.Entities.Products.OptionValue", b =>
                {
                    b.Property<int>("OptionValueId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OptionValueId"), 1L, 1);

                    b.Property<DateTimeOffset>("CreatedDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<int>("OptionId")
                        .HasColumnType("int");

                    b.Property<DateTimeOffset?>("UpdatedDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("OptionValueId");

                    b.HasIndex("OptionId", "Value")
                        .IsUnique();

                    b.ToTable("OptionValues");

                    b.HasData(
                        new
                        {
                            OptionValueId = 1,
                            CreatedDate = new DateTimeOffset(new DateTime(2023, 4, 25, 17, 7, 4, 110, DateTimeKind.Unspecified).AddTicks(3074), new TimeSpan(0, 0, 0, 0, 0)),
                            OptionId = 1,
                            Value = "s-m"
                        },
                        new
                        {
                            OptionValueId = 2,
                            CreatedDate = new DateTimeOffset(new DateTime(2023, 4, 25, 17, 7, 4, 110, DateTimeKind.Unspecified).AddTicks(3076), new TimeSpan(0, 0, 0, 0, 0)),
                            OptionId = 1,
                            Value = "m-l"
                        },
                        new
                        {
                            OptionValueId = 3,
                            CreatedDate = new DateTimeOffset(new DateTime(2023, 4, 25, 17, 7, 4, 110, DateTimeKind.Unspecified).AddTicks(3077), new TimeSpan(0, 0, 0, 0, 0)),
                            OptionId = 1,
                            Value = "xl+"
                        },
                        new
                        {
                            OptionValueId = 4,
                            CreatedDate = new DateTimeOffset(new DateTime(2023, 4, 25, 17, 7, 4, 110, DateTimeKind.Unspecified).AddTicks(3078), new TimeSpan(0, 0, 0, 0, 0)),
                            OptionId = 2,
                            Value = "black"
                        },
                        new
                        {
                            OptionValueId = 5,
                            CreatedDate = new DateTimeOffset(new DateTime(2023, 4, 25, 17, 7, 4, 110, DateTimeKind.Unspecified).AddTicks(3080), new TimeSpan(0, 0, 0, 0, 0)),
                            OptionId = 2,
                            Value = "white"
                        });
                });

            modelBuilder.Entity("OnlineStore.Data.Contracts.Entities.Products.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProductId"), 1L, 1);

                    b.Property<DateTimeOffset>("CreatedDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Description")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<bool>("IsVisible")
                        .HasColumnType("bit");

                    b.Property<string>("Label")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTimeOffset?>("UpdatedDate")
                        .HasColumnType("datetimeoffset");

                    b.HasKey("ProductId");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            ProductId = 1,
                            CreatedDate = new DateTimeOffset(new DateTime(2023, 4, 25, 17, 7, 4, 110, DateTimeKind.Unspecified).AddTicks(2854), new TimeSpan(0, 0, 0, 0, 0)),
                            Description = "T-shirt the hate,100 cotton, machine embroidery, oversized fit.\r\n\r\nso much hate for me everywhere today, can't figure out what i did to cause it... i am trying to ovrercome, to overcome it all.",
                            IsVisible = true,
                            Label = "preorder",
                            Name = "t-shirt the hate"
                        });
                });

            modelBuilder.Entity("OnlineStore.Data.Contracts.Entities.Products.Variant", b =>
                {
                    b.Property<int>("VariantId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("VariantId"), 1L, 1);

                    b.Property<DateTimeOffset>("CreatedDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int?>("Quantity")
                        .HasColumnType("int");

                    b.Property<decimal>("RegularPrice")
                        .HasPrecision(18, 4)
                        .HasColumnType("decimal(18,4)");

                    b.Property<string>("SKU")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<decimal?>("SalePrice")
                        .HasPrecision(18, 4)
                        .HasColumnType("decimal(18,4)");

                    b.Property<DateTimeOffset?>("UpdatedDate")
                        .HasColumnType("datetimeoffset");

                    b.HasKey("VariantId");

                    b.HasIndex("ProductId");

                    b.ToTable("Variants");

                    b.HasData(
                        new
                        {
                            VariantId = 1,
                            CreatedDate = new DateTimeOffset(new DateTime(2023, 4, 25, 17, 7, 4, 110, DateTimeKind.Unspecified).AddTicks(3104), new TimeSpan(0, 0, 0, 0, 0)),
                            ProductId = 1,
                            RegularPrice = 3000m,
                            SKU = "TTHS-MCB"
                        },
                        new
                        {
                            VariantId = 2,
                            CreatedDate = new DateTimeOffset(new DateTime(2023, 4, 25, 17, 7, 4, 110, DateTimeKind.Unspecified).AddTicks(3106), new TimeSpan(0, 0, 0, 0, 0)),
                            ProductId = 1,
                            RegularPrice = 3000m,
                            SKU = "TTHS-MCW"
                        },
                        new
                        {
                            VariantId = 3,
                            CreatedDate = new DateTimeOffset(new DateTime(2023, 4, 25, 17, 7, 4, 110, DateTimeKind.Unspecified).AddTicks(3108), new TimeSpan(0, 0, 0, 0, 0)),
                            ProductId = 1,
                            RegularPrice = 3000m,
                            SKU = "TTHM-LCB"
                        },
                        new
                        {
                            VariantId = 4,
                            CreatedDate = new DateTimeOffset(new DateTime(2023, 4, 25, 17, 7, 4, 110, DateTimeKind.Unspecified).AddTicks(3109), new TimeSpan(0, 0, 0, 0, 0)),
                            ProductId = 1,
                            RegularPrice = 3000m,
                            SKU = "TTHM-LCW"
                        },
                        new
                        {
                            VariantId = 5,
                            CreatedDate = new DateTimeOffset(new DateTime(2023, 4, 25, 17, 7, 4, 110, DateTimeKind.Unspecified).AddTicks(3111), new TimeSpan(0, 0, 0, 0, 0)),
                            ProductId = 1,
                            RegularPrice = 3000m,
                            SKU = "TTHXL+CB"
                        },
                        new
                        {
                            VariantId = 6,
                            CreatedDate = new DateTimeOffset(new DateTime(2023, 4, 25, 17, 7, 4, 110, DateTimeKind.Unspecified).AddTicks(3112), new TimeSpan(0, 0, 0, 0, 0)),
                            ProductId = 1,
                            RegularPrice = 3000m,
                            SKU = "TTHXL+CW"
                        });
                });

            modelBuilder.Entity("OnlineStore.Data.Contracts.Entities.Products.VariantOptionValue", b =>
                {
                    b.Property<int>("VariantId")
                        .HasColumnType("int");

                    b.Property<int>("OptionValueId")
                        .HasColumnType("int");

                    b.HasKey("VariantId", "OptionValueId");

                    b.HasIndex("OptionValueId");

                    b.ToTable("VariantOptionValues");

                    b.HasData(
                        new
                        {
                            VariantId = 1,
                            OptionValueId = 1
                        },
                        new
                        {
                            VariantId = 1,
                            OptionValueId = 4
                        },
                        new
                        {
                            VariantId = 2,
                            OptionValueId = 1
                        },
                        new
                        {
                            VariantId = 2,
                            OptionValueId = 5
                        },
                        new
                        {
                            VariantId = 3,
                            OptionValueId = 2
                        },
                        new
                        {
                            VariantId = 3,
                            OptionValueId = 4
                        },
                        new
                        {
                            VariantId = 4,
                            OptionValueId = 2
                        },
                        new
                        {
                            VariantId = 4,
                            OptionValueId = 5
                        },
                        new
                        {
                            VariantId = 5,
                            OptionValueId = 3
                        },
                        new
                        {
                            VariantId = 5,
                            OptionValueId = 4
                        },
                        new
                        {
                            VariantId = 6,
                            OptionValueId = 3
                        },
                        new
                        {
                            VariantId = 6,
                            OptionValueId = 5
                        });
                });

            modelBuilder.Entity("OnlineStore.Data.Contracts.Entities.Products.Option", b =>
                {
                    b.HasOne("OnlineStore.Data.Contracts.Entities.Products.Product", "Product")
                        .WithMany("Options")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("OnlineStore.Data.Contracts.Entities.Products.OptionValue", b =>
                {
                    b.HasOne("OnlineStore.Data.Contracts.Entities.Products.Option", "Option")
                        .WithMany("OptionValues")
                        .HasForeignKey("OptionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Option");
                });

            modelBuilder.Entity("OnlineStore.Data.Contracts.Entities.Products.Variant", b =>
                {
                    b.HasOne("OnlineStore.Data.Contracts.Entities.Products.Product", "Product")
                        .WithMany("Variants")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("OnlineStore.Data.Contracts.Entities.Products.VariantOptionValue", b =>
                {
                    b.HasOne("OnlineStore.Data.Contracts.Entities.Products.OptionValue", "OptionValue")
                        .WithMany("VariantOptionValues")
                        .HasForeignKey("OptionValueId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OnlineStore.Data.Contracts.Entities.Products.Variant", "Variant")
                        .WithMany("VariantOptionValues")
                        .HasForeignKey("VariantId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("OptionValue");

                    b.Navigation("Variant");
                });

            modelBuilder.Entity("OnlineStore.Data.Contracts.Entities.Products.Option", b =>
                {
                    b.Navigation("OptionValues");
                });

            modelBuilder.Entity("OnlineStore.Data.Contracts.Entities.Products.OptionValue", b =>
                {
                    b.Navigation("VariantOptionValues");
                });

            modelBuilder.Entity("OnlineStore.Data.Contracts.Entities.Products.Product", b =>
                {
                    b.Navigation("Options");

                    b.Navigation("Variants");
                });

            modelBuilder.Entity("OnlineStore.Data.Contracts.Entities.Products.Variant", b =>
                {
                    b.Navigation("VariantOptionValues");
                });
#pragma warning restore 612, 618
        }
    }
}