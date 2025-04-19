using FluentValidation;
using Module34WebAPI.Contracts.Models.Devices;
using System.Collections.Generic;
using System.Linq;

namespace Module34WebAPI.Contracts.Validation;

public class EditDeviceRequestValidator : AbstractValidator<EditDeviceRrequest>
{
    public EditDeviceRequestValidator()
    {
        RuleFor(x => x.NewName).NotEmpty();
        RuleFor(x => x.NewRoom).NotEmpty().Must(BeSupported).WithMessage($"Choose one of the folloving rooms: {string.Join(", ", Values.ValidRooms)}");
    }

    private bool BeSupported(string location)
    {
        return Values.ValidRooms.Any(e => e == location);
    }
}
