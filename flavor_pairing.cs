using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace AzureFunction_Docker
{
    public static class flavor_pairing
    {
        [FunctionName("flavor_pairing")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string flavor = req.Query["flavor"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            flavor = flavor ?? data?.flavor;
            switch (flavor)
            {
                case "Strawberry":
                    return (ActionResult)new OkObjectResult("The light-fruity strawberry needs some chocolate!");
                    break;
                case "Chocolate":
                    return (ActionResult)new OkObjectResult("This will pair up nicely with some Strawberry! \n Fruitiness and deep-chocolatey flavor, mmmmm");
                    break;
                case "Lemon":
                    return (ActionResult)new OkObjectResult("Any flavor will be good with Lemon! \n Petty neutral-tartness provider");
                    break;
                case "Pistacchio":
                    return (ActionResult)new OkObjectResult("Just. pistacchio. - Amazing. There's no flavor that can make it better.");
                    break;
                case "Vanilla":
                    return (ActionResult)new OkObjectResult("Vanilla is kinda plain, try pairing it with the tartness of the lemon, or go for a more deep route with some chocolate");
                    break;
                default:
                    return (ActionResult)new OkObjectResult("Hi! This is the -Azure Functions Ice Cream local- ! \n Our flavors are 'Strawberry' // 'Chocolate' // 'Vanilla' // 'Lemon' // 'Pistacchio' \n Choose one and we will recommend you the best option to compliment it!");
                    break;
            }
             }
    }
}