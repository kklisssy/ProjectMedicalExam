using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMedicalExam.Models
{
    class Rule
    {
        public StayPurpose StayPurpose { get; set; }
        public Citizenship Citizenship { get; set; }
        public int Days { get; set; }
        public MedicalDocument MedicalDocument { get; set; }

        public string BuildMessage(EnterOfForeignCitizen enter)
        {
            DateTime deadline = enter.DateOfEntry.AddDays(Days);
            int daysPassed = (DateTime.Now - enter.DateOfEntry).Days;
            int daysRemaining = Days - daysPassed;

            var result = new StringBuilder();
            result.AppendLine($"Дата въезда: {enter.DateOfEntry:dd.MM.yyyy}");
            result.AppendLine($"Срок: {Days} дней");
            result.AppendLine($"Крайний срок: {deadline:dd.MM.yyyy}");

            if (daysRemaining > 0)
            {
                result.AppendLine($"На прохождение медицинского освидетельствования осталось дней: {daysRemaining}.");
                result.AppendLine("Медицинские организации в которые можно обратиться:");
                result.AppendLine(MedicalDocument.GetOrganizationsAddresses());

            }
            else
                result.AppendLine($"Срок истек! Просрочено на {-daysRemaining} дней.");

            return result.ToString();
        }
    }
}
