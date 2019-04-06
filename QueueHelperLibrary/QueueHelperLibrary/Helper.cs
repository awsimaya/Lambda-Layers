using Amazon.SQS;
using Amazon.SQS.Model;
using System;
using System.Threading.Tasks;
using Amazon.SimpleSystemsManagement;
using Amazon.SimpleSystemsManagement.Model;

namespace QueueHelperLibrary
{
    public class Helper
    {
        private AmazonSQSClient SqsClient { get; set; }
        private AmazonSimpleSystemsManagementClient SystemsManagementClient { get; set; }
        public Helper(Amazon.RegionEndpoint region)
        {
            SqsClient = new AmazonSQSClient(region);
            SystemsManagementClient = new AmazonSimpleSystemsManagementClient(region);
        }

        public async Task<SendMessageResponse> SendMessage(string message)
        {
            var response = await SystemsManagementClient.GetParameterAsync(new GetParameterRequest()
            {
                Name = "OrdersQueueName"
            });
            return await SqsClient.SendMessageAsync(new SendMessageRequest()
            {
                MessageBody = message,
                QueueUrl = response.Parameter.Value
            });
        }
    }
}