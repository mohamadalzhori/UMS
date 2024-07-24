using System.Globalization;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace UMS.API.Controllers.v1;

[ApiController]
[Route("Test")]
[ApiVersion(1)]
public class TestController(IStringLocalizer<TestController> _localizer) : ControllerBase
{

   [HttpGet("Test")]
   public ActionResult<string> Test([FromQuery] string culture = "en-US")
   {
      // Validate and set the culture
      if (string.IsNullOrWhiteSpace(culture) || !CultureInfo.GetCultures(CultureTypes.SpecificCultures).Any(c => c.Name == culture))
      {
         culture = "en-US"; // Default to "en-US" if invalid culture is provided
      }

      var cultureInfo = new CultureInfo(culture);
      CultureInfo.CurrentCulture = cultureInfo;
      CultureInfo.CurrentUICulture = cultureInfo;

      // Get the localized message
      var localizedMessage = _localizer["Hello"];

      return Ok(localizedMessage);
   }
   
   
}