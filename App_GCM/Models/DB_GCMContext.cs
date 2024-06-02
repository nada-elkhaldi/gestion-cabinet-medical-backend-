using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace App_GCM.Models
{
    public partial class DB_GCMContext : DbContext
    {
        public DB_GCMContext()
        {
        }

        public DB_GCMContext(DbContextOptions<DB_GCMContext> options)
            : base(options)
        {
        }

        public virtual DbSet<DossiersMedicaux> DossiersMedicauxes { get; set; }  
        public virtual DbSet<Infermier> Infermiers { get; set; }  
        public virtual DbSet<Medecin> Medecins { get; set; }  
        public virtual DbSet<Medicament> Medicaments { get; set; }  
        public virtual DbSet<Patient> Patients { get; set; }  
        public virtual DbSet<Personnel> Personnels { get; set; } 
        public virtual DbSet<RendezVou> RendezVous { get; set; }  
        public virtual DbSet<Specialite> Specialites { get; set; }  

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DESKTOP-RQMN5DU\\SQLEXPRESS;Database=DB_GCM;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DossiersMedicaux>(entity =>
            {
                entity.ToTable("dossiers_medicaux");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AntecedentsMed)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("antecedentsMed");

                entity.Property(e => e.IdMedecin).HasColumnName("id_medecin");

                entity.Property(e => e.IdPatient).HasColumnName("id_patient");

                entity.Property(e => e.TypeTraitement)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("typeTraitement");

                entity.HasOne(d => d.IdPatientNavigation)
                    .WithMany(p => p.DossiersMedicauxes)
                    .HasForeignKey(d => d.IdPatient)
                    .HasConstraintName("FK__dossiers___id_pa__534D60F1");

                entity.HasOne(d => d.IdMedecinNavigation)
                   .WithMany(p => p.DossiersMedicauxes)
                   .HasForeignKey(d => d.IdMedecin)
                   .HasConstraintName("FK_DossiersMedicaux_Medecin");
            });

            modelBuilder.Entity<Infermier>(entity =>
            {
                entity.ToTable("infermiers");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.IdMedecin).HasColumnName("id_medecin");

                entity.Property(e => e.NomInf)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("nomInf");

                entity.Property(e => e.Nomtele)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("nomtele");

                entity.Property(e => e.PrenomInf)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("prenomInf");

                entity.HasOne(d => d.IdMedecinNavigation)
                    .WithMany(p => p.Infermiers)
                    .HasForeignKey(d => d.IdMedecin)
                    .HasConstraintName("FK__infermier__id_me__4E88ABD4");
            });

            modelBuilder.Entity<Medecin>(entity =>
            {
                entity.ToTable("medecins");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.IdSpecialite).HasColumnName("id_specialite");

                entity.Property(e => e.NomM)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("nomM");

                entity.Property(e => e.Numtel)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("numtel");

                entity.Property(e => e.Occupe).HasColumnName("occupe");

                entity.Property(e => e.PrenomM)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("prenomM");

                entity.HasOne(d => d.IdSpecialiteNavigation)
                    .WithMany(p => p.Medecins)
                    .HasForeignKey(d => d.IdSpecialite)
                    .HasConstraintName("FK__medecins__id_spe__4BAC3F29");

               
            });

            modelBuilder.Entity<Medicament>(entity =>
            {
                entity.ToTable("medicaments");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Libelle)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("libelle");
            });

            modelBuilder.Entity<Patient>(entity =>
            {
                entity.ToTable("patients");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Cin)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("cin");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.NomP)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("nomP");

                entity.Property(e => e.Numtel)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("numtel");

                entity.Property(e => e.PrenomP)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("prenomP");
            });

            modelBuilder.Entity<Personnel>(entity =>
            {
                entity.ToTable("personnels");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Nom)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("nom");

                entity.Property(e => e.Prenom)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("prenom");

                entity.Property(e => e.Role)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("role");

                entity.Property(e => e.Tele)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("tele");
                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.Password)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("password");
            });

            modelBuilder.Entity<RendezVou>(entity =>
            {
                entity.ToTable("rendez_vous");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DateR)
                    .HasColumnType("date")
                    .HasColumnName("dateR");

                entity.Property(e => e.HeureR).HasColumnName("heureR");

                entity.Property(e => e.IdMedecin).HasColumnName("id_medecin");

                entity.Property(e => e.IdPatient).HasColumnName("id_patient");

                entity.Property(e => e.Status)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("status");
                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.HasOne(d => d.IdMedecinNavigation)
                    .WithMany(p => p.RendezVous)
                    .HasForeignKey(d => d.IdMedecin)
                    .HasConstraintName("FK__rendez_vo__id_me__59063A47");

                entity.HasOne(d => d.IdPatientNavigation)
                    .WithMany(p => p.RendezVous)
                    .HasForeignKey(d => d.IdPatient)
                    .HasConstraintName("FK__rendez_vo__id_pa__5812160E");
            });

            modelBuilder.Entity<Specialite>(entity =>
            {
                entity.ToTable("specialite");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Specialite1)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("specialite");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
