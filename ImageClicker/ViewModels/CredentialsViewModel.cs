using FluentValidation.Attributes;
using ImageClicker.ViewModels.Validators;

namespace ImageClicker.ViewModels

{
    [Validator(typeof(CredentialsViewModelValidator))]
    public class CredentialsViewModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
