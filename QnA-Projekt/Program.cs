using Azure;
using Azure.AI.Language.QuestionAnswering;
using System;


namespace QnA_Projekt
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // This example requires environment variables named "LANGUAGE_KEY" and "LANGUAGE_ENDPOINT"
            Uri endpoint = new Uri("https://ailanguagemodel100.cognitiveservices.azure.com/");
            AzureKeyCredential credential = new AzureKeyCredential("247a74e9b8d84ef59021ae924d0dd6c3");
            string projectName = "LearnFAQ";
            string deploymentName = "production";

            //string question = "how can i learn more about Mirosoft certification";

            QuestionAnsweringClient client = new QuestionAnsweringClient(endpoint, credential);
            QuestionAnsweringProject project = new QuestionAnsweringProject(projectName, deploymentName);

            Console.WriteLine("Ask a question, type quit to quit.");
            while(true)
            {
                Console.WriteLine("Q: ");
                string question = Console.ReadLine();
                if (question.ToLower() == "quit")
                {
                    break;
                }
                try
                {
                    Response<AnswersResult> response = client.GetAnswers(question, project);
                    foreach (KnowledgeBaseAnswer answer in response.Value.Answers)
                    {
                        Console.WriteLine($"Q:{question}");
                        Console.WriteLine($"A:{answer.Answer}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Request error: {ex.Message}");
                }
            }
        }
    }
}
