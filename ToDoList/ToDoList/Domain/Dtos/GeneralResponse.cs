using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;

namespace ToDoList.Domain.Dtos;

public class GeneralResponse
{
    public string Message { get; set; }
    public object Data { get; set; }

    public GeneralResponse()
    {
        
    }
    
    public GeneralResponse(string message, object data)
    {
        Message = message;
        Data = data;
    }


    public static IActionResult GetResponse(int statusCode, string message, object data)
    {
        var response = new GeneralResponse(message, data);
        return new ObjectResult(response)
        {
            StatusCode = statusCode
        };
    }
    
    public static IActionResult GetResponse(int statusCode, string message)
    {
        return GetResponse(statusCode, message, null);
    }
    
    public static IActionResult GetResponse(int statusCodes, object data)
    {
        var reasonPhrase = ReasonPhrases.GetReasonPhrase(statusCodes);
        return GetResponse(statusCodes, reasonPhrase, data);
    }
}