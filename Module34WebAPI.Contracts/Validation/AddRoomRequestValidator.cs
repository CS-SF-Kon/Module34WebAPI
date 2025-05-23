﻿using FluentValidation;
using Module34WebAPI.Contracts.Models.Rooms;

namespace Module34WebAPI.Contracts.Validation;

public class AddRoomRequestValidator : AbstractValidator<AddRoomRequest>
{
    public AddRoomRequestValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Area).NotEmpty();
        RuleFor(x => x.Voltage).NotEmpty();
        RuleFor(x => x.GasConnected).NotEmpty();
    }
}
