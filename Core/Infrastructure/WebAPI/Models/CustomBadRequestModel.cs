using Microsoft.AspNetCore.Mvc;

namespace Project.Core.Infrastructure.WebAPI.Models;

public class CustomBadRequestModel
{
    /// <summary>
    /// Can use error code to display at message title dialog
    /// </summary>
    public string ErrorCode { get; set; }

    public string ErrorMessage { get; set; }

    /// <summary>
    /// Use this key to get mapped language in multi language env
    /// </summary>
    public string ErrorMessageKey { get; set; }

    /// <summary>
    /// Binding this params to value place holder in the message if exists
    /// </summary>
    public IDictionary<string, string> ErrorMessageParam { get; set; }

    /// <summary>
    /// ctr
    /// </summary>
    /// <param name="context"></param>
    public CustomBadRequestModel(ActionContext context)
    {
        ConstructErrorMessages(context);
    }

    private void ConstructErrorMessages(ActionContext context)
    {
        ErrorCode = "400";
        var lastError = context.ModelState.LastOrDefault();
        var lastErrorDetail = lastError.Value?.Errors.LastOrDefault();
        if (lastErrorDetail != null)
        {
            var getError = GetBadRequestType(lastError.Key, lastErrorDetail.ErrorMessage);
            if (getError.type != null)
            {
                // check if error has a common, return a common message.
                var keyFormatted = lastError.Key.Replace("$.", "");
                ErrorMessage = GetErrorMessageInENByType(getError.type.Value, keyFormatted, getError.message);
                ErrorMessageKey = GetErrorMessageInENByType(getError.type.Value, keyFormatted, getError.message);
                return;
            }
        }

        // If error is not handler by common and dev, default error will show in message params
        ErrorMessageParam = new Dictionary<string, string>();
        foreach (var keyModelStatePair in context.ModelState)
        {
            var key = keyModelStatePair.Key;
            var errors = keyModelStatePair.Value.Errors;
            if (errors != null && errors.Count > 0)
            {
                var errorMessages = new string[errors.Count];
                for (var i = 0; i < errors.Count; i++)
                {
                    if (!ErrorMessageParam.ContainsKey(key))
                    {
                        ErrorMessageParam.Add(key, errors[i].ErrorMessage);
                    }
                }
            }
        }

        ErrorMessage = "Message detail in params";
        ErrorMessageKey = "Multiple error messages";
    }

    private (BadRequestType? type, string? message) GetBadRequestType(string key, string message)
    {
        if (string.IsNullOrEmpty(key)) return (BadRequestType.BodyEmpty, null);
        if (message.EndsWith("field is required.") || message.EndsWith("field is required."))
        {
            return (BadRequestType.FieldRequired, null);
        }
        else if (message.StartsWith("The JSON value could not be converted to"))
        {
            var value = message.Split("The JSON value could not be converted to")[1].Split(" ")[1].Trim();
            return (BadRequestType.TypeMismatch, value.Substring(0, value.Length - 1));
        }
        else if (message.Contains("The value") && message.Contains("is not valid for"))
        {
            return (BadRequestType.ParamNotValid, message);
        }

        return (null, null);
    }

    private string GetErrorMessageInENByType(BadRequestType badRequestType, string key, string value)
    {
        switch (badRequestType)
        {
            case BadRequestType.BodyEmpty: return "object is required."; // object is required.
            case BadRequestType.FieldRequired: return $"{key} is required."; // data field is required.
            case BadRequestType.ParamNotValid: return value;
            case BadRequestType.TypeMismatch:
                return $"The value of field {key} is not valid for {value}."; // data type mismatch.
        }

        return string.Empty;
    }
}