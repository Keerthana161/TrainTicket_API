using Assignment.Model;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Assignment.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class RegisterTicket : Controller
    {
        [HttpPost("create")]
        public IActionResult CreateTicket([FromBody] TrainCreate input)
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), @"UserData");
            Directory.CreateDirectory(filePath);
            string fullPath = Path.Combine(filePath, "user.json");
            if (input.sectionIn.Equals(section.sectionA.ToString()) || input.sectionIn.Equals(section.sectionB.ToString()))
            {
                try
                {
                    var personDetails = new
                    {
                       From = input.from,
                       To = input.to,
                        FirstName = input.firstName,
                        LastName = input.lastName,
                        Email = input.email,
                        Section = input.sectionIn,
                        PricePaid = input.price
                    };
                    // Serialize input to JSON
                    string json = JsonSerializer.Serialize(input);

                    // Write JSON data to a file
                    System.IO.File.WriteAllText(fullPath, json);

                    return Ok(new { data = personDetails, message = $"Train booked for {input.firstName} {input.lastName}." });
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Failed to save data: {ex.Message}");
                }
            }
            return BadRequest(new { message = "Select proper section." });
        }


        [HttpPost("modify")]
        public IActionResult ModifyTicket([FromBody] TrainModify input)
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), @"UserData");
            Directory.CreateDirectory(filePath);
            string fullPath = Path.Combine(filePath, "User.json");
            if (input.sectionIn.Equals(section.sectionA.ToString()) || input.sectionIn.Equals(section.sectionB.ToString()))
            {
                try
                {

                    string existingData = System.IO.File.ReadAllText(fullPath);

                    // Deserialize JSON data to a dictionary
                    var existingTicket = JsonSerializer.Deserialize<Dictionary<string, object>>(existingData);

                    // Update specific fields based on the input
                    if (!string.IsNullOrEmpty(input.firstName))
                    {
                        existingTicket["firstName"] = input.firstName;
                    }

                    if (!string.IsNullOrEmpty(input.lastName))
                    {
                        existingTicket["lastName"] = input.lastName;
                    }

                    if (!string.IsNullOrEmpty(input.email))
                    {
                        existingTicket["email"] = input.email;
                    }

                    if (!string.IsNullOrEmpty(input.sectionIn))
                    {
                        existingTicket["sectionIn"] = input.sectionIn;
                    }

                    // Serialize modified object back to JSON
                    string modifiedJson = JsonSerializer.Serialize(existingTicket);

                    // Write modified JSON data to the file
                    System.IO.File.WriteAllText(fullPath, modifiedJson);

                    return Ok(new { message = $"Train details updated for {input.firstName} {input.lastName}." });
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Failed to save data: {ex.Message}");
                }
            }
            return BadRequest(new { message = "Select proper section." });
        }


        [HttpPost("remove")]
        public IActionResult RemoveTicket([FromBody] TrainRemove input)
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), @"UserData");
            string fullPath = Path.Combine(filePath, "User.json");

            if (input.sectionIn.Equals(section.sectionA.ToString()) || input.sectionIn.Equals(section.sectionB.ToString()))
            {
                try
                {
                    string jsonContent = System.IO.File.ReadAllText(fullPath);

                    // Parse the JSON content
                    JObject json = JObject.Parse(jsonContent);

                    // Check if all specified fields are present
                    bool containsAllFields =
                        json["firstName"] != null &&
                        json["lastName"] != null &&
                        json["sectionIn"] != null;

                    // If all fields are present, remove all data from the JSON
                    if (containsAllFields)
                    {
                        json.RemoveAll();
                    }
                    else
                    {
                        Console.WriteLine("Not all specified fields are present in the JSON.");
                    }

                    // Write the updated JSON back to the file
                    System.IO.File.WriteAllText(fullPath, json.ToString());
                    return Ok(new { message = "Removed user." });
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Failed to remove data: {ex.Message}");
                }
            }
            return BadRequest(new { message = "Select proper section." });
        }


        [HttpPost("show")]
        public IActionResult ShowTicket([FromBody] TrainShow input)
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), @"UserData");
            string fullPath = Path.Combine(filePath, "User.json");

            try
            {
                string jsonContent = System.IO.File.ReadAllText(fullPath);

                // Deserialize JSON content into UserData object
                TrainCreate userData = JsonConvert.DeserializeObject<TrainCreate>(jsonContent);

                // Check if the specified user matches the data in the deserialized object
                if (userData.firstName == input.firstName && userData.lastName == input.lastName)
                {
                    return Ok(userData); // Return the matching user data
                }
                else
                {
                    return NotFound(new { message = "User not found." });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Failed to retrieve data: {ex.Message}");
            }
        }










    }



}

