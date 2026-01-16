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
            _purposes = new List<StayPurpose>();
            string[] names = { "Трудовая деятельность", "Иная деятельность" };

            foreach (var name in names)
            {
                _purposes.Add(new StayPurpose { Name = name });
            }
        }

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
