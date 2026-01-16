using ProjectMedicalExam.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
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
            const string idFile = "enter_id.txt";

            if (!File.Exists(idFile))
                File.WriteAllText(idFile, "0");

            var enterId = int.Parse(File.ReadAllText(idFile));
            var nextId = enterId + 1;

            File.WriteAllText(idFile, nextId.ToString());

            var enter = new EnterOfForeignCitizen
            {
                EnterId = nextId,
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
