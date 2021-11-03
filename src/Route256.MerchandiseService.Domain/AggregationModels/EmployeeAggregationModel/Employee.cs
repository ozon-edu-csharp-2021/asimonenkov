using System;
using Route256.MerchandiseService.Domain.Exceptions;
using Route256.MerchandiseService.Domain.Models;

namespace Route256.MerchandiseService.Domain.AggregationModels.EmployeeAggregationModel
{
    public class Employee : Entity
    {
        public Employee(Name name, Position position, WorkExperience workExperience, Email email)
        {
            SetEmployeeId();
            Name = name;
            WorkExperience = workExperience;
            Email = email;
        }

        /// <summary>
        /// Идентификатор сотрудника
        /// </summary>
        public EmployeeId EmployeeId { get; private set; }
        
        /// <summary>
        /// Имя сотрудника
        /// </summary>
        public Name Name { get; set; }
        
        /// <summary>
        /// Опыт работы сотрудника
        /// </summary>
        public WorkExperience WorkExperience { get; set; }
        
        /// <summary>
        /// Электронная почта сотрудника сотрудника
        /// </summary>
        public Email Email { get; set; }

        public void SetName(Name name)
        {
            Name = name;
        }

        public void SetEmail(Email email)
        {
            Email = email;
        }

        public void SetWorkExperience(WorkExperience workExperience)
        {
            if (workExperience.Value < 0)
            {
                throw new NegativeValueException("Work experience value is negative");
            }
            WorkExperience = workExperience;
        }

        private void SetEmployeeId()
        {
            EmployeeId = new EmployeeId(Guid.NewGuid());
        }
    }
}