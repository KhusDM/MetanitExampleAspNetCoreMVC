using MetanitExampleCoreMVC.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetanitExampleCoreMVC.Infrastructure
{
    public class EventModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var datePartValues = bindingContext.ValueProvider.GetValue("Date");
            var timePartValues = bindingContext.ValueProvider.GetValue("Time");
            var namePartValues = bindingContext.ValueProvider.GetValue("Name");
            var idPartValues = bindingContext.ValueProvider.GetValue("Id");

            // получаем значения
            string date = datePartValues.FirstValue;
            string time = timePartValues.FirstValue;
            string name = namePartValues.FirstValue;
            string id = idPartValues.FirstValue;

            // если id не установлен, например, при создании модели, генерируем его
            if (String.IsNullOrEmpty(id)) id = Guid.NewGuid().ToString();

            // если name не установлено
            if (String.IsNullOrEmpty(name)) name = "Неизвестное событие";

            // Парсим дату и время
            DateTime.TryParse(date, out var parsedDateValue);
            DateTime.TryParse(time, out var parsedTimeValue);

            // Объединяем полученные значения в один объект DateTime
            DateTime fullDateTime = new DateTime(parsedDateValue.Year,
                            parsedDateValue.Month,
                            parsedDateValue.Day,
                            parsedTimeValue.Hour,
                            parsedTimeValue.Minute,
                            parsedTimeValue.Second);
            // устанавливаем результат привязки
            bindingContext.Result = ModelBindingResult.Success(new Event { Id = id, EventDate = fullDateTime, Name = name });
            return Task.CompletedTask;
        }
    }
}
