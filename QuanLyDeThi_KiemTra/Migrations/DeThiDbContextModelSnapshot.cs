﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using QuanLyDeThi_KiemTra.Data;

#nullable disable

namespace QuanLyDeThi_KiemTra.Migrations
{
    [DbContext(typeof(DeThiDbContext))]
    partial class DeThiDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.21")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("QuanLyDeThi_KiemTra.Model.CauHoiTL", b =>
                {
                    b.Property<string>("MaCH")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("GioiHanSoTu")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("HinhThucTL")
                        .HasColumnType("bit");

                    b.Property<string>("MaDeThi")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("NoiDung")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MaCH");

                    b.HasIndex("MaDeThi");

                    b.ToTable("CauHoiTL");
                });

            modelBuilder.Entity("QuanLyDeThi_KiemTra.Model.CHTracNghiem", b =>
                {
                    b.Property<string>("MaCHTrN")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("DoKho")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<bool>("HinhThucTL")
                        .HasMaxLength(25)
                        .HasColumnType("bit");

                    b.Property<string>("MaDeThi")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("NoiDung")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MaCHTrN");

                    b.HasIndex("MaDeThi");

                    b.ToTable("CauHoiTN");
                });

            modelBuilder.Entity("QuanLyDeThi_KiemTra.Model.DeThi", b =>
                {
                    b.Property<string>("MaDT")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("FileDeThi")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("HinhThuc")
                        .HasColumnType("bit");

                    b.Property<string>("MaBM")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MaMH")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("NgayPD")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("NgayTao")
                        .HasColumnType("datetime2");

                    b.Property<string>("NguoiTao")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("SLCauHoi")
                        .HasColumnType("int");

                    b.Property<string>("TenDeThi")
                        .IsRequired()
                        .HasMaxLength(225)
                        .HasColumnType("nvarchar(225)");

                    b.Property<int>("ThangDiem")
                        .HasColumnType("int");

                    b.Property<int>("ThoiLuong")
                        .HasColumnType("int");

                    b.Property<bool>("TinhTrangPD")
                        .HasColumnType("bit");

                    b.HasKey("MaDT");

                    b.ToTable("DeThi");
                });

            modelBuilder.Entity("QuanLyDeThi_KiemTra.Model.TepCauhoi", b =>
                {
                    b.Property<int>("STT")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("STT"), 1L, 1);

                    b.Property<string>("TenTep")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("STT");

                    b.ToTable("TepCauHoi");
                });

            modelBuilder.Entity("QuanLyDeThi_KiemTra.Model.TLTracNghiem", b =>
                {
                    b.Property<string>("MaCH")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("LaDapAnA")
                        .HasColumnType("bit");

                    b.Property<bool>("LaDapAnB")
                        .HasColumnType("bit");

                    b.Property<bool>("LaDapAnC")
                        .HasColumnType("bit");

                    b.Property<bool>("LaDapAnD")
                        .HasColumnType("bit");

                    b.Property<string>("NoiDungA")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NoiDungB")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NoiDungC")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NoiDungD")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MaCH");

                    b.ToTable("TraLoiTN");
                });

            modelBuilder.Entity("QuanLyDeThi_KiemTra.Model.TraLoiTL", b =>
                {
                    b.Property<string>("MaCH")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CauTL")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FileCauTL")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MaCH");

                    b.ToTable("CauTraLoiTL");
                });

            modelBuilder.Entity("QuanLyDeThi_KiemTra.Model.CauHoiTL", b =>
                {
                    b.HasOne("QuanLyDeThi_KiemTra.Model.DeThi", "DeThi")
                        .WithMany()
                        .HasForeignKey("MaDeThi")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DeThi");
                });

            modelBuilder.Entity("QuanLyDeThi_KiemTra.Model.CHTracNghiem", b =>
                {
                    b.HasOne("QuanLyDeThi_KiemTra.Model.DeThi", "DeThi")
                        .WithMany()
                        .HasForeignKey("MaDeThi");

                    b.Navigation("DeThi");
                });

            modelBuilder.Entity("QuanLyDeThi_KiemTra.Model.TLTracNghiem", b =>
                {
                    b.HasOne("QuanLyDeThi_KiemTra.Model.CHTracNghiem", "CHTracNghiem")
                        .WithMany()
                        .HasForeignKey("MaCH")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CHTracNghiem");
                });

            modelBuilder.Entity("QuanLyDeThi_KiemTra.Model.TraLoiTL", b =>
                {
                    b.HasOne("QuanLyDeThi_KiemTra.Model.CauHoiTL", "CauHoiTL")
                        .WithMany()
                        .HasForeignKey("MaCH")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CauHoiTL");
                });
#pragma warning restore 612, 618
        }
    }
}
