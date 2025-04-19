using FluentValidation;
using Module34WebAPI.Contracts.Models.Rooms;

namespace Module34WebAPI.Contracts.Validation;

public class EditRoomRequestValidator : AbstractValidator<EditRoomRequest>
{
    public EditRoomRequestValidator()
    {
        RuleFor(x => x.NewName).NotEmpty();
        RuleFor(x => x.NewVoltage).NotEmpty();
        RuleFor(x => x.NewGasConnected).NotEmpty();
    }
}
