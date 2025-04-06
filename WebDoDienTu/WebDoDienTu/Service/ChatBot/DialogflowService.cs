using Google.Cloud.Dialogflow.V2;

namespace WebDoDienTu.Service.ChatBot
{
    public class DialogflowService
    {
        private readonly SessionsClient _sessionsClient;
        private readonly string _projectId;

        public DialogflowService(string projectId, string jsonCredentialsPath)
        {
            // Tải thông tin xác thực từ tệp JSON
            System.Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", jsonCredentialsPath);

            // Khởi tạo SessionsClient
            _sessionsClient = SessionsClient.Create();
            _projectId = projectId;
        }

        public async Task<string> DetectIntentAsync(string sessionId, string text)
        {
            var sessionName = new SessionName(_projectId, sessionId);
            var queryInput = new QueryInput
            {
                Text = new TextInput
                {
                    Text = text,
                    LanguageCode = "vi" 
                }
            };

            // Gọi Dialogflow
            var response = await _sessionsClient.DetectIntentAsync(sessionName, queryInput);

            Console.WriteLine($"Phản hồi từ Dialogflow: {response.QueryResult.FulfillmentText}");

            return response.QueryResult.FulfillmentText;
        }

    }
}
