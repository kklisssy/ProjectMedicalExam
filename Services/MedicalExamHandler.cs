using ProjectMedicalExam.Models;
using ProjectMedicalExam.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMedicalExam.Services
{
    class MedicalExamHandler
    {
        private readonly EnterRepository _enterRepository;
        private readonly RuleRepository _ruleRepository;

        public MedicalExamHandler(EnterRepository enterRepository, RuleRepository ruleRepository)
        {
            _enterRepository = enterRepository;
            _ruleRepository = ruleRepository;
        }

        public int EnterDate(DateTime dateOfEntry)
        {
            var enter = _enterRepository.CreateEnter(dateOfEntry);
            return enter.EnterId;
        }

        public string GetCitizenshipAndPurposeNames()
        {
            return _ruleRepository.GetCitizenshipAndPurposeNames();
        }

        //public List<string> GetCitizenshipsNames()
        //{
        //    return _ruleRepository.GetCitizenshipsNames();
        //}

        //public List<string> GetPurposesNames()
        //{
        //    return _ruleRepository.GetPurposesNames();
        //}

        public string GetMessage(string stayPurposeName, string citizenshipName, int enterId)
        {
            return _ruleRepository.GetMessage(stayPurposeName, citizenshipName, enterId);
        }
    }
}
