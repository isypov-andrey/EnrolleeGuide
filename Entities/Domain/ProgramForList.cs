using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Domain
{
    public class ProgramForList
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IEnumerable<string> Subjects { get; set; }
        public IEnumerable<TrainingFormType> TrainingForms { get; set; }
        public int? BudgetExamPoints { get; set; }
        public int? BudgetPlacesCount { get; set; }
        public decimal? Price { get; set; }
        public int DurationInYears { get; set; }

        public string SubjectsString
        {
            get
            {
                return String.Join(", ", Subjects);
            }
        }

        public string TrainingFormsString
        {
            get
            {
                if (TrainingForms == null)
                {
                    return "";
                }
                return string.Join("/", TrainingForms.Distinct().Select((type) =>
                {
                    switch (type)
                    {
                        case TrainingFormType.FullTime:
                            return "Очно";
                        case TrainingFormType.Extramural:
                            return "Заочно";
                        case TrainingFormType.Distance:
                            return "Дистанционно";
                        case TrainingFormType.PartTime:
                            return "Очно-заочно";
                        default:
                            return "";
                    }
                }));
            }
        }
    }
}
