using ProjectMedicalExam.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMedicalExam.Repositories
{
    class EnterRepository
    {
        private readonly List<EnterOfForeignCitizen> _enters;

        public EnterRepository()
        {
            _enters = new List<EnterOfForeignCitizen>();
        }

        public EnterOfForeignCitizen CreateEnter(DateTime dateOfEntry)
        {
            var enter = new EnterOfForeignCitizen
            {
                EnterId = _enters.Count + 1,
                DateOfEntry = dateOfEntry
            };

            _enters.Add(enter);

            return enter;
        }

        public EnterOfForeignCitizen GetEnterById(int enterId)
        {
            return _enters.FirstOrDefault(e => e.EnterId == enterId);
        }
    }
}
