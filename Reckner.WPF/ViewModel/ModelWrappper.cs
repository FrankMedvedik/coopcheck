using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;


namespace Reckner.WPF.ViewModel
{
    public class ModelWrappper<T> : NotifyDataErrorInfoBase
    {
        public ModelWrappper(T model) 
        {
            if (model == null) throw new ArgumentNullException(nameof(model));
            Model = model;
            Validate();
        }

        public T Model { get; private set; }

        //protected void SetValue<TValue>(TValue newValue, [CallerMemberName] string propertyName = null)
        //{
        //    var propertyInfo = Model.GetType().GetProperty(propertyName);
        //    var currentValue = propertyInfo.GetValue(Model);
        //    if (!Equals(currentValue, newValue))
        //    {
        //        propertyInfo.SetValue(Model, newValue);
        //        Validate();
        //        NotifyPropertyChanged(propertyName);
        //    }
        //}

        protected void Validate()
        {
            ClearErrors();
            var results = new List<ValidationResult>();
            var context = new ValidationContext(this);
            Validator.TryValidateObject(this, context, results,true);
            if (results.Any())
            {
                var propertyNames = results.SelectMany(r => r.MemberNames).Distinct().ToList();
                foreach (var propertyName in propertyNames)
                {
                    Errors[propertyName] = results
                        .Where(r => r.MemberNames.Contains(propertyName))
                        .Select(r => r.ErrorMessage)
                        .Distinct()
                        .ToList();
                    OnErrorsChanged(propertyName);
                }
            }
            OnErrorsChanged(nameof(IsValid));
        }


        public bool IsValid
        {
            get { return !HasErrors; }
        }
        
        //protected TValue GetValue<TValue>([CallerMemberName] string propertyName = null) 
        //{
        //    var propertyInfo = Model.GetType().GetProperty(propertyName);
        //        return (TValue) propertyInfo.GetValue(Model);
            
        //}
        
    }
}