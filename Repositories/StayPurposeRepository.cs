using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectMedicalExam.Models;

namespace ProjectMedicalExam.Repositories
{
    class StayPurposeRepository
    {
        private readonly List<StayPurpose> _purposes;
        public StayPurposeRepository()
        {
            _purposes = new List<StayPurpose>
            {
                new StayPurpose { Name = "Трудовая деятельность" },
                new StayPurpose { Name = "Иная деятельность" }
            };
        }

        public List<StayPurpose> GetListPurposes() => _purposes;

        public List<string> GetAllNames()
        {
            return _purposes.Select(p => p.Name).ToList();
        }

        public StayPurpose GetByName(string stayPurposeName)
        {
            return _purposes.FirstOrDefault(p => p.Name.Equals(stayPurposeName, StringComparison.OrdinalIgnoreCase));
        }
    }
}
