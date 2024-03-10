using Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.EntityConfigurations
{
    internal class MilitaryDataConfiguration : IEntityTypeConfiguration<MilitaryData>
    {
        public void Configure(EntityTypeBuilder<MilitaryData> builder)
        {
            builder.ToTable("military_data");

            builder.Property(m => m.PatientDataId).HasColumnName("patient_data_id");
            builder.Property(m => m.IsVeteran).HasColumnName("is_veteran");
            builder.Property(m => m.Specialty).HasColumnName("specialty");
            builder.Property(m => m.ServicePlace).HasColumnName("service_place");
            builder.Property(m => m.IsOnVacation).HasColumnName("is_on_vacation");
            builder.Property(m => m.HasDisability).HasColumnName("has_disability");
            builder.Property(m => m.DisabilityCategory).HasColumnName("disability_category");
            builder.Property(m => m.HealthProblems).HasColumnName("health_problems");
            builder.Property(m => m.NeedMedicalOrPsychoCare).HasColumnName("need_medical_or_psycho_care");
            builder.Property(m => m.HasDocuments).HasColumnName("has_documents");
            builder.Property(m => m.DocumentNumber).HasColumnName("document_number");
            builder.Property(m => m.RehabilitationAndSupportNeeds).HasColumnName("rehabilitation_and_support_needs");
            builder.Property(m => m.HasFamilyInNeed).HasColumnName("has_family_in_need");
            builder.Property(m => m.HowLearnedAboutRehabCenter).HasColumnName("how_learned_about_rehab_center");
            builder.Property(m => m.WasRehabilitated).HasColumnName("was_rehabilitated");
            builder.Property(m => m.PlaceOfRehabilitation).HasColumnName("place_of_rehabilitation");
            builder.Property(m => m.ResultOfRehabilitation).HasColumnName("result_of_rehabilitation");
        }
    }
}
