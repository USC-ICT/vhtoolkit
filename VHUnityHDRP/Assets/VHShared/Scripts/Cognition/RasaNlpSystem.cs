using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Ride.NLP
{
    #region RasaNlpDataStructs

    /// <summary>
    /// Response from the Rasa API
    /// </summary>
    public struct RasaNlpAnswer
    {
        public string recipient_id;
        public string text;
    }

    #endregion

    public class RasaNlpSystem : NlpSystemUnity
    {
        private string m_model = "default-rasa";
        public override void SetSystemPrompt(string prompt)
        {

        }

        /// <summary>
        /// Requests response based on provided user input for Rasa. 
        /// </summary>
        /// <param name="request">User input, string question</param>
        /// <param name="onComplete">Delegate to execute on successful request, typically parses JSON response</param>
        public override async void Request(NlpRequest request, Action<NlpResponse> onComplete)
        {
            m_model = (request.content != string.Empty) ? request.content : m_model;

            string jsonRequestBody = await (new StringContent(
                "{\"message\": \"" + request.content + "\"}", Encoding.UTF8, "application/json"
            )).ReadAsStringAsync();

            // Call rasa endpoint
            DateTime startTime = DateTime.Now;
            string response = await RideIO.Post("http://localhost:8080/webhooks/rest/webhook", jsonRequestBody, "application/json");
            DateTime endTime = DateTime.Now;
            m_responseTime = (endTime - startTime).TotalMilliseconds + " ms";
            
            // Parse the response
            List<RasaNlpAnswer> rasaNlpAnswer = RideIO.JsonDeserialize<List<RasaNlpAnswer>>(response);
            // Concatenating content from multiple json objects (if they exist) in the response into a single response 
            StringBuilder concatenatedResponse = new StringBuilder();
            foreach (var item in rasaNlpAnswer)
            {
                concatenatedResponse.Append(item.text + " ");
            }

            NlpResponse qnaAnswer = new NlpResponse(concatenatedResponse.ToString());

            //Update conversation history
            NlpInteraction interaction = new();
            interaction.input = request.content;
            interaction.response = concatenatedResponse.ToString();
            interaction.inputTimestamp = startTime;
            interaction.responseTimestamp = endTime;
            m_interactionHistory.Add(interaction);

            onComplete?.Invoke(qnaAnswer);
        }
    }
}
