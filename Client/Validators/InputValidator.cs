// **********************************************************
// Assembly         : JustEat.Client.Shell.Validators
// Author           : Edward Trenchard
// Created          : 17-07-2016
//
// **********************************************************
// <copyright file="InputValidator.cs" company="">
//     Copyright (c) All rights reserved.
// </copyright>
// <summary></summary>

using FluentValidation;
using JustEat.Client.Shell.Models;

namespace JustEat.Client.Shell.Validators
{
    /// <summary>
    /// 
    /// </summary>
    public class InputValidator : AbstractValidator<InputModel>
    {
        /// <summary>
        /// 
        /// </summary>
        public InputValidator()
        {
            RuleFor(input => input.Outcode).NotEmpty();
            // todo more rules, some regex out there for UK Outcode
        }
    }
}
