﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using MigrationTool;
using System;

namespace MigrationTool.Migrations
{
    [DbContext(typeof(MigrationContext))]
    [Migration("20181025165653_nlog")]
    partial class Nlog
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Enrollment.Data.Automatic.VariableMetaData", b =>
                {
                    b.Property<int>("VariableMetaDataId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Data");

                    b.Property<DateTime>("LastUpdated");

                    b.HasKey("VariableMetaDataId");

                    b.ToTable("VariableMetaData","Automatic");
                });

            modelBuilder.Entity("Enrollment.Data.Entities.Academic", b =>
                {
                    b.Property<int>("UserId");

                    b.Property<bool>("AttendedPriorColleges");

                    b.Property<bool>("EarnedCreditAtCmc");

                    b.Property<DateTime>("FromDate");

                    b.Property<DateTime?>("GedDate");

                    b.Property<string>("GedLocation");

                    b.Property<string>("GraduationStatus")
                        .IsRequired()
                        .HasMaxLength(256);

                    b.Property<string>("HomeSchoolAssociation")
                        .HasMaxLength(256);

                    b.Property<string>("HomeSchoolType")
                        .HasMaxLength(256);

                    b.Property<string>("LastHighSchoolLocation")
                        .IsRequired()
                        .HasMaxLength(256);

                    b.Property<string>("NcHighSchoolName")
                        .HasMaxLength(256);

                    b.Property<bool?>("ReceivedGed");

                    b.Property<DateTime>("ToDate");

                    b.HasKey("UserId");

                    b.ToTable("Academic");
                });

            modelBuilder.Entity("Enrollment.Data.Entities.Admissions", b =>
                {
                    b.Property<int>("UserId");

                    b.Property<string>("EnrollmentTerm")
                        .IsRequired()
                        .HasMaxLength(6);

                    b.Property<string>("EnrollmentYear")
                        .IsRequired()
                        .HasMaxLength(4);

                    b.Property<string>("EnteringStatus")
                        .IsRequired()
                        .HasMaxLength(1);

                    b.Property<string>("Program")
                        .IsRequired()
                        .HasMaxLength(55);

                    b.Property<string>("ProgramType")
                        .IsRequired()
                        .HasMaxLength(55);

                    b.HasKey("UserId");

                    b.ToTable("Admissions");
                });

            modelBuilder.Entity("Enrollment.Data.Entities.Certification", b =>
                {
                    b.Property<int>("UserId");

                    b.Property<bool>("CertificateStatementChecked");

                    b.Property<bool>("DeclarationStatementChecked");

                    b.Property<bool>("PolicyStatementsChecked");

                    b.HasKey("UserId");

                    b.ToTable("Certification");
                });

            modelBuilder.Entity("Enrollment.Data.Entities.ContactInfo", b =>
                {
                    b.Property<int>("UserId");

                    b.Property<DateTime>("DateOfBirth");

                    b.Property<string>("EnergencyContactFirstName")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.Property<string>("EnergencyContactLastName")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.Property<string>("EnergencyContactPhoneNumber")
                        .IsRequired()
                        .HasMaxLength(12);

                    b.Property<string>("EnergencyContactRelationship")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.Property<string>("Ethnicity")
                        .IsRequired()
                        .HasMaxLength(3);

                    b.Property<string>("FormerFirstName")
                        .HasMaxLength(30);

                    b.Property<string>("FormerLastName")
                        .HasMaxLength(30);

                    b.Property<string>("FormerMiddleName")
                        .HasMaxLength(30);

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasMaxLength(1);

                    b.Property<bool>("HasFormerName");

                    b.Property<string>("Race")
                        .IsRequired()
                        .HasMaxLength(3);

                    b.Property<string>("SocialSecurityNumber")
                        .IsRequired()
                        .HasMaxLength(11);

                    b.HasKey("UserId");

                    b.ToTable("ContactInfo");
                });

            modelBuilder.Entity("Enrollment.Data.Entities.Institution", b =>
                {
                    b.Property<int>("InstitutionId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("EndYear")
                        .IsRequired()
                        .HasMaxLength(4);

                    b.Property<string>("HighestDegreeEarned")
                        .IsRequired()
                        .HasMaxLength(4);

                    b.Property<string>("InstitutionName")
                        .IsRequired();

                    b.Property<string>("InstitutionState")
                        .IsRequired()
                        .HasMaxLength(256);

                    b.Property<DateTime?>("MonthYearGraduated");

                    b.Property<string>("StartYear")
                        .IsRequired()
                        .HasMaxLength(4);

                    b.Property<int>("UserId");

                    b.HasKey("InstitutionId");

                    b.HasIndex("UserId");

                    b.ToTable("Institutions");
                });

            modelBuilder.Entity("Enrollment.Data.Entities.LookUps", b =>
                {
                    b.Property<int>("LookUpsID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ListName")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<int>("Order");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasMaxLength(256);

                    b.HasKey("LookUpsID");

                    b.ToTable("LookUps");
                });

            modelBuilder.Entity("Enrollment.Data.Entities.MoreInfo", b =>
                {
                    b.Property<int>("UserId");

                    b.Property<string>("GovernmentBenefits")
                        .HasMaxLength(10);

                    b.Property<bool>("IsVeteran");

                    b.Property<string>("MilitaryBranch")
                        .HasMaxLength(4);

                    b.Property<string>("MilitaryStatus")
                        .HasMaxLength(4);

                    b.Property<string>("OverallEducationalGoal")
                        .IsRequired()
                        .HasMaxLength(4);

                    b.Property<string>("ReasonForAttending")
                        .IsRequired()
                        .HasMaxLength(4);

                    b.Property<string>("VeteranType")
                        .HasMaxLength(4);

                    b.HasKey("UserId");

                    b.ToTable("MoreInfo");
                });

            modelBuilder.Entity("Enrollment.Data.Entities.Personal", b =>
                {
                    b.Property<int>("UserId");

                    b.Property<string>("Address1")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.Property<string>("Address2")
                        .HasMaxLength(30);

                    b.Property<string>("CellPhone")
                        .HasMaxLength(15);

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.Property<string>("County")
                        .HasMaxLength(25);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.Property<string>("MiddleName")
                        .HasMaxLength(20);

                    b.Property<string>("OtherPhone")
                        .HasMaxLength(15);

                    b.Property<string>("PrimaryEmail")
                        .HasMaxLength(40);

                    b.Property<string>("State")
                        .IsRequired()
                        .HasMaxLength(25);

                    b.Property<string>("Suffix")
                        .HasMaxLength(6);

                    b.Property<string>("ZipCode")
                        .IsRequired()
                        .HasMaxLength(5);

                    b.HasKey("UserId");

                    b.ToTable("Personal");
                });

            modelBuilder.Entity("Enrollment.Data.Entities.Residency", b =>
                {
                    b.Property<int>("UserId");

                    b.Property<string>("CitizenshipStatus")
                        .IsRequired()
                        .HasMaxLength(6);

                    b.Property<string>("CountryOfCitizenship")
                        .HasMaxLength(55);

                    b.Property<string>("DriversLicenseNumber")
                        .HasMaxLength(25);

                    b.Property<string>("DriversLicenseState")
                        .HasMaxLength(10);

                    b.Property<bool>("HasValidDriversLicense");

                    b.Property<string>("ImmigrationStatus")
                        .HasMaxLength(6);

                    b.Property<string>("ResidentState")
                        .IsRequired()
                        .HasMaxLength(55);

                    b.HasKey("UserId");

                    b.ToTable("Residency");
                });

            modelBuilder.Entity("Enrollment.Data.Entities.StateLivedIn", b =>
                {
                    b.Property<int>("StateLivedInId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("State")
                        .IsRequired()
                        .HasMaxLength(25);

                    b.Property<int>("UserId");

                    b.HasKey("StateLivedInId");

                    b.HasIndex("UserId");

                    b.ToTable("StatesLivedIn");
                });

            modelBuilder.Entity("Enrollment.Data.Entities.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("UserName");

                    b.HasKey("UserId");

                    b.ToTable("User");
                });

            modelBuilder.Entity("Enrollment.Data.Rules.RulesModule", b =>
                {
                    b.Property<int>("RulesModuleId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Application")
                        .IsRequired();

                    b.Property<DateTime>("LastUpdated");

                    b.Property<string>("LoggedInUserId");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<byte[]>("ResourceSetFile")
                        .IsRequired();

                    b.Property<byte[]>("RuleSetFile")
                        .IsRequired();

                    b.HasKey("RulesModuleId");

                    b.HasIndex("Name", "Application")
                        .IsUnique()
                        .HasName("uc_RulesModule");

                    b.ToTable("RulesModule","Rules");
                });

            modelBuilder.Entity("Enrollment.Data.Entities.Academic", b =>
                {
                    b.HasOne("Enrollment.Data.Entities.User", "User")
                        .WithOne("Academic")
                        .HasForeignKey("Enrollment.Data.Entities.Academic", "UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Enrollment.Data.Entities.Admissions", b =>
                {
                    b.HasOne("Enrollment.Data.Entities.User", "User")
                        .WithOne("Admissions")
                        .HasForeignKey("Enrollment.Data.Entities.Admissions", "UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Enrollment.Data.Entities.Certification", b =>
                {
                    b.HasOne("Enrollment.Data.Entities.User", "User")
                        .WithOne("Certification")
                        .HasForeignKey("Enrollment.Data.Entities.Certification", "UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Enrollment.Data.Entities.ContactInfo", b =>
                {
                    b.HasOne("Enrollment.Data.Entities.User", "User")
                        .WithOne("ContactInfo")
                        .HasForeignKey("Enrollment.Data.Entities.ContactInfo", "UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Enrollment.Data.Entities.Institution", b =>
                {
                    b.HasOne("Enrollment.Data.Entities.Academic", "Academic")
                        .WithMany("Institutions")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Enrollment.Data.Entities.MoreInfo", b =>
                {
                    b.HasOne("Enrollment.Data.Entities.User", "User")
                        .WithOne("MoreInfo")
                        .HasForeignKey("Enrollment.Data.Entities.MoreInfo", "UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Enrollment.Data.Entities.Personal", b =>
                {
                    b.HasOne("Enrollment.Data.Entities.User", "User")
                        .WithOne("Personal")
                        .HasForeignKey("Enrollment.Data.Entities.Personal", "UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Enrollment.Data.Entities.Residency", b =>
                {
                    b.HasOne("Enrollment.Data.Entities.User", "User")
                        .WithOne("Residency")
                        .HasForeignKey("Enrollment.Data.Entities.Residency", "UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Enrollment.Data.Entities.StateLivedIn", b =>
                {
                    b.HasOne("Enrollment.Data.Entities.Residency", "Residency")
                        .WithMany("StatesLivedIn")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
