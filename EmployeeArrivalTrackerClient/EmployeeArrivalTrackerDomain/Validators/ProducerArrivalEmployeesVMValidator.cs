using Common.Models.Producer;
using FluentValidation;
using System;

namespace EmployeeArrivalTrackerDomain.Validators
{
    public class ProducerArrivalEmployeesVMValidator : AbstractValidator<ProducerArrivalEmployeesVM>
    {
        public ProducerArrivalEmployeesVMValidator()
        {
            RuleFor(p => p.EmployeeId).NotEmpty();

            RuleFor(p => p.When).NotEmpty();

            RuleFor(u => u.When)
                    .Cascade(CascadeMode.Continue)
                    .Custom(ValidateDate);
        }

        private void ValidateDate<TObject>(DateTime date, ValidationContext<TObject> context)
        {
            try
            {
                DateTime.SpecifyKind(date, DateTimeKind.Utc);
            }
            catch (Exception ex)
            {
                context.AddFailure(ex.Message);
            }
        }
    }
}
