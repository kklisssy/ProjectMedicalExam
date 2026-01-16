using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMedicalExam.Models
{
    class MedicalDocument
    {
        public string Name { get; set; }
        public List<MedicalOrganization> Organizations { get; set; }

        public MedicalDocument()
        {
            Organizations = new List<MedicalOrganization>();
        }

        public string GetOrganizationsAddresses()
        {
            if (Organizations == null || Organizations.Count == 0)
                return "Медицинские организации не указаны";

            var sb = new StringBuilder();

            for (int i = 0; i < Organizations.Count; i++)
            {
                sb.AppendLine($"{i + 1}. {Organizations[i].Name}");
                sb.AppendLine($"Адрес: {Organizations[i].Address}");
            }

            return sb.ToString();
        }
    }
}
