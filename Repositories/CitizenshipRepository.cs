using ProjectMedicalExam.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMedicalExam.Repositories
{
    class CitizenshipRepository
    {
        private readonly List<Citizenship> _citizenships;
        public CitizenshipRepository()
        {
            _citizenships = new List<Citizenship>
            {
                new Citizenship { Name = "Азербайджан" },
                new Citizenship { Name = "Армения" },
                new Citizenship { Name = "Туркменистан" },
                new Citizenship { Name = "Китай" },
                new Citizenship { Name = "Казахстан" },
                new Citizenship { Name = "Белорусь" }
            };
        }

        public List<Citizenship> GetListCitizenships() => _citizenships;

        public List<string> GetAllNames()
        {
            return _citizenships.Select(p => p.Name).ToList();
        }

        public Citizenship GetByName(string citizenshipName)
        {
            return _citizenships.FirstOrDefault(c => c.Name.Equals(citizenshipName, StringComparison.OrdinalIgnoreCase));
        }
    }
}
