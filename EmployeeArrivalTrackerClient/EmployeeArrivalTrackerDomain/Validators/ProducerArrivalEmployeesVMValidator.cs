using Common.Models.Producer;
using FluentValidation;

namespace EmployeeArrivalTrackerDomain.Validators
{
    public class ProducerArrivalEmployeesVMValidator : AbstractValidator<ProducerArrivalEmployeesVM>
    {
        public ProducerArrivalEmployeesVMValidator()
        {
            RuleFor(p => p.EmployeeId).NotEmpty();
            RuleFor(p => p.When).NotEmpty();
        }
    }
}
