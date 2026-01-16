using ProjectMedicalExam.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMedicalExam.Repositories
{
    class RuleRepository
    {
        private readonly List<Rule> _rules;
        private readonly EnterRepository _enterRepository;
        private readonly StayPurposeRepository _stayPurposeRepository;
        private readonly CitizenshipRepository _citizenshipRepository;

        public RuleRepository(StayPurposeRepository stayPurposeRepository, CitizenshipRepository citizenshipRepository, EnterRepository enterRepository, MedicalDocument document)
        {
            _stayPurposeRepository = stayPurposeRepository;
            _citizenshipRepository = citizenshipRepository;
            _enterRepository = enterRepository;
            _rules = new List<Rule>();

            foreach (var citizenship in _citizenshipRepository.GetListCitizenships())
            {
                //для трудовой деятельности - 30 дней
                _rules.Add(new Rule
                {
                    StayPurpose = _stayPurposeRepository.GetByName("Трудовая деятельность"),
                    Citizenship = citizenship,
                    Days = 30,
                    MedicalDocument = document
                });

                //для иной деятельности - 90 дней
                _rules.Add(new Rule
                {
                    StayPurpose = _stayPurposeRepository.GetByName("Иная деятельность"),
                    Citizenship = citizenship,
                    Days = 90,
                    MedicalDocument = document
                });
            }

        }

        public string GetCitizenshipAndPurposeNames()
        {
            var sb = new StringBuilder();

            sb.AppendLine("\nДоступные цели пребывания:");
            var purposes = _stayPurposeRepository.GetAllNames();
            for (int i = 0; i < purposes.Count; i++)
                sb.AppendLine($"{i + 1}. {purposes[i]}");

            sb.AppendLine("\nДоступные гражданства:");
            var citizenships = _citizenshipRepository.GetAllNames();
            for (int i = 0; i < citizenships.Count; i++)
                sb.AppendLine($"{i + 1}. {citizenships[i]}");

            return sb.ToString();
        }

        public Rule GetRule(StayPurpose stayPurpose, Citizenship citizenship)
        {
            return _rules.FirstOrDefault(r =>
            string.Equals(r.StayPurpose?.Name, stayPurpose?.Name, StringComparison.OrdinalIgnoreCase) &&
            string.Equals(r.Citizenship?.Name, citizenship?.Name, StringComparison.OrdinalIgnoreCase));
        }

        public string GetMessage(string stayPurposeName, string citizenshipName, int enterId)
        {
            var purpose = _stayPurposeRepository.GetByName(stayPurposeName);
            if (purpose == null)
            {
                return $"Цель пребывания '{stayPurposeName}' не найдена.";
            }

            var citizenship = _citizenshipRepository.GetByName(citizenshipName);
            if (citizenship == null)
            {
                return $"Гражданство '{citizenshipName}' не найдено.";
            }

            var rule = GetRule(purpose, citizenship);
            var enter = _enterRepository.GetEnterById(enterId);

            return rule.BuildMessage(enter);
        }
    }
}
