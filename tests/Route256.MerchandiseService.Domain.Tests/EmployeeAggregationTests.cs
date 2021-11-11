using System;
using Route256.MerchandiseService.Domain.AggregationModels.EmployeeAggregationModel;
using Route256.MerchandiseService.Domain.Exceptions;
using Xunit;

namespace Route256.MerchandiseService.Domain.Tests
{
    public class EmployeeAggregationTests
    {
        [Fact]
        public void SetEmployeeName()
        {
            //arrange
            var employee = new Employee(new Name("test"), Position.InternProgrammer,
                new WorkExperience(1), new Email("test@test"));

            var newName = "newTest";
            
            //act
            employee.SetName(new Name(newName));
            
            //arrange
            Assert.Equal(newName, employee.Name.Value);
        }
        
        [Fact]
        public void SetEmployeeEmail()
        {
            //arrange
            var employee = new Employee(new Name("test"), Position.InternProgrammer,
                new WorkExperience(1), new Email("test@test"));

            var newEmail = "newTest@newTest";
            
            //act
            employee.SetEmail(new Email(newEmail));
            
            //arrange
            Assert.Equal(newEmail, employee.Email.Value);
        }
        
        [Fact]
        public void SetEmployeeWorkingExperiencePositiveResult()
        {
            //arrange
            var employee = new Employee(new Name("test"), Position.InternProgrammer,
                new WorkExperience(1), new Email("test@test"));

            var newWorckExperience = 2;
            
            //act
            employee.SetWorkExperience(new WorkExperience(2));
            
            //arrange
            Assert.Equal(2, employee.WorkExperience.Value);
        }
        
        [Fact]
        public void SetEmployeeWorkingExperienceExceptionResult()
        {
            //arrange
            var employee = new Employee(new Name("test"), Position.InternProgrammer,
                new WorkExperience(1), new Email("test@test"));

            var newWorckExperience = -1;
            
            //act

            //arrange
            Assert.Throws<NegativeValueException>(() => employee.SetWorkExperience(new WorkExperience(newWorckExperience)));
        }
    }
}