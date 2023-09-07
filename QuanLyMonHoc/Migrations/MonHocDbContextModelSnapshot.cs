﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using QuanLyMonHoc.Data;

#nullable disable

namespace QuanLyMonHoc.Migrations
{
    [DbContext(typeof(MonHocDbContext))]
    partial class MonHocDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.21")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("QuanLyMonHoc.Model.HoiDap", b =>
                {
                    b.Property<string>("MaCauHoi")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("MaHV")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MaMH")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("NoiDung")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<DateTime>("ThoiGian")
                        .HasColumnType("datetime2");

                    b.Property<string>("TieuDe")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("MaCauHoi");

                    b.HasIndex("MaMH");

                    b.ToTable("HoiDap");
                });

            modelBuilder.Entity("QuanLyMonHoc.Model.LopHoc", b =>
                {
                    b.Property<string>("MaLop")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("GhiChu")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("MaMH")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("MaNK")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("SiSo")
                        .HasColumnType("int");

                    b.Property<string>("TenLop")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("MaLop");

                    b.HasIndex("MaMH");

                    b.HasIndex("MaNK");

                    b.ToTable("LopHoc");
                });

            modelBuilder.Entity("QuanLyMonHoc.Model.MonHoc", b =>
                {
                    b.Property<string>("MaMH")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("MaGV")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MoTa")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("NgayGuiPheDuyet")
                        .HasColumnType("datetime2");

                    b.Property<string>("TenMH")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TinhTrang")
                        .HasColumnType("bit");

                    b.HasKey("MaMH");

                    b.ToTable("MonHoc");
                });

            modelBuilder.Entity("QuanLyMonHoc.Model.NienKhoa", b =>
                {
                    b.Property<string>("MaNK")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("TGBatDau")
                        .HasColumnType("int");

                    b.Property<int>("TGKetThuc")
                        .HasColumnType("int");

                    b.HasKey("MaNK");

                    b.ToTable("NienKhoa");
                });

            modelBuilder.Entity("QuanLyMonHoc.Model.TraLoi", b =>
                {
                    b.Property<string>("MaCauTL")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("MaCauHoi")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("MaTacGia")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NoiDung")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<DateTime>("ThoiGian")
                        .HasColumnType("datetime2");

                    b.HasKey("MaCauTL");

                    b.HasIndex("MaCauHoi");

                    b.ToTable("HoiDap_TL");
                });

            modelBuilder.Entity("QuanLyMonHoc.Model.HoiDap", b =>
                {
                    b.HasOne("QuanLyMonHoc.Model.MonHoc", "monHoc")
                        .WithMany("HoiDaps")
                        .HasForeignKey("MaMH")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("monHoc");
                });

            modelBuilder.Entity("QuanLyMonHoc.Model.LopHoc", b =>
                {
                    b.HasOne("QuanLyMonHoc.Model.MonHoc", "monHoc")
                        .WithMany("LopHocs")
                        .HasForeignKey("MaMH")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("QuanLyMonHoc.Model.NienKhoa", "nienKhoa")
                        .WithMany("LopHocs")
                        .HasForeignKey("MaNK")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("monHoc");

                    b.Navigation("nienKhoa");
                });

            modelBuilder.Entity("QuanLyMonHoc.Model.TraLoi", b =>
                {
                    b.HasOne("QuanLyMonHoc.Model.HoiDap", "hoiDap")
                        .WithMany("TraLois")
                        .HasForeignKey("MaCauHoi")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("hoiDap");
                });

            modelBuilder.Entity("QuanLyMonHoc.Model.HoiDap", b =>
                {
                    b.Navigation("TraLois");
                });

            modelBuilder.Entity("QuanLyMonHoc.Model.MonHoc", b =>
                {
                    b.Navigation("HoiDaps");

                    b.Navigation("LopHocs");
                });

            modelBuilder.Entity("QuanLyMonHoc.Model.NienKhoa", b =>
                {
                    b.Navigation("LopHocs");
                });
#pragma warning restore 612, 618
        }
    }
}
