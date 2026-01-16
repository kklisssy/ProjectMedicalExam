using ProjectMedicalExam.Models;
using ProjectMedicalExam.Repositories;
using ProjectMedicalExam.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMedicalExam
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;

            var enterRepository = new EnterRepository();
            var stayPurposeRepository = new StayPurposeRepository();
            var citizenshipRepository = new CitizenshipRepository();

            var medicalDocument = new MedicalDocument
            {
                Name = "Медицинское освидетельствование",
                Organizations = new List<MedicalOrganization>
                {
                    new MedicalOrganization
                    {
                        Name = "ОКБ №2",
                        Address = "г. Тюмень, ул. Мельникайте, д. 75к3"
                    },
                    new MedicalOrganization
                    {
                        Name = "Городская поликлиника №3",
                        Address = "г. Тюмень, ул. Ленина, д. 23С1"
                    },
                    new MedicalOrganization
                    {
                        Name = "Городская поликлиника №5",
                        Address = "г. Тюмень, ул. Червишевский Тракт, д. 68А/1"
                    },
                    new MedicalOrganization
                    {
                        Name = "Городская поликлиника №6",
                        Address = "г. Тюмень, ул. 50 лет ВЛКСМ, д. 97"
                    },
                    new MedicalOrganization
                    {
                        Name = "Городская поликлиника №12",
                        Address = "г. Тюмень, ул. Пермякова, д. 76к1"
                    }
                }
            };

            var ruleRepository = new RuleRepository(stayPurposeRepository, citizenshipRepository, enterRepository, medicalDocument);

            var medicalExamHandler = new MedicalExamHandler(enterRepository, ruleRepository);

            GetRoadmap(medicalExamHandler);
        }

        static void GetRoadmap(MedicalExamHandler medicalExamHandler)
        {
            Console.Write("Ввведите дату въезда (дд.мм.гггг): ");
            if (!DateTime.TryParse(Console.ReadLine(), out DateTime entryDate))
            {
                Console.WriteLine("Введена некорректная дата.");
                return;
            }

            if (entryDate > DateTime.Now.Date)
            {
                Console.WriteLine("Дата въезда не может быть в будущем.");
                return;
            }

            int enter = medicalExamHandler.EnterDate(entryDate);
            var text = medicalExamHandler.GetCitizenshipAndPurposeNames();
            Console.WriteLine(text);

            var lines = text.Split(
                new[] { Environment.NewLine },
                StringSplitOptions.None);

            var purposes = new List<string>();
            var citizenships = new List<string>();

            bool inPurposes = false;
            bool inCitizenships = false;

            foreach (var raw in lines)
            {
                var line = raw.Trim();
                if (line.Length == 0) continue;

                if (line == "Доступные цели пребывания:")
                {
                    inPurposes = true;
                    inCitizenships = false;
                    continue;
                }

                if (line == "Доступные гражданства:")
                {
                    inPurposes = false;
                    inCitizenships = true;
                    continue;
                }

                if (!inPurposes && !inCitizenships) continue;

                int dotIndex = line.IndexOf('.');
                if (dotIndex <= 0) continue;

                string name = line.Substring(dotIndex + 1).Trim();

                if (inPurposes) 
                    purposes.Add(name);
                else 
                    citizenships.Add(name);
            }

            Console.Write("Выберите номер цели пребывания: ");
            if (!int.TryParse(Console.ReadLine(), out int purposeInd))
            {
                Console.WriteLine("Введите число.");
                return;
            }

            if (purposeInd < 1 || purposeInd > purposes.Count)
            {
                Console.WriteLine("Введен некорректный номер цели.");
                return;
            }


            Console.Write("Выберите номер гражданства: ");
            if (!int.TryParse(Console.ReadLine(), out int citizenshipInd))
            {
                Console.WriteLine("Введите число.");
                return;
            }

            if (citizenshipInd < 1 || citizenshipInd > citizenships.Count)
            {
                Console.WriteLine("Введен некорректный номер гражданства.");
                return;
            }

            var selectedPurpose = purposes[purposeInd - 1];
            var selectedCitizenship = citizenships[citizenshipInd - 1];

            string message = medicalExamHandler.GetMessage(selectedPurpose, selectedCitizenship, enter);
            Console.WriteLine("\nНаправление: ");
            Console.WriteLine(message);
        }
    }
}
