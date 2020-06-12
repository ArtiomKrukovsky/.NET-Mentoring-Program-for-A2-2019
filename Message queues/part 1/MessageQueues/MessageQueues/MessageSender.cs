﻿namespace MessageQueues
{
    using System.Collections.Generic;
    using System.Text;
    using RabbitMQ.Client;
    using Newtonsoft.Json;
    using System.Linq;

    public static class MessageSender
    {
        public static void SendMessage(List<FileViewModel> files)
        {
            var filesJson = ConvertFilesToJson(files);
            using (var model = MQConnection.GetRabbitChannel(Constants.QueryName))
            {
                PublishData(model, filesJson, Constants.QueryName);
            }
        }

        private static void PublishData(IModel model, IEnumerable<string> filesJson, string routingKey)
        {
            foreach (var fileJson in filesJson)
            {
                var body = ConvertJsonToBytes(fileJson);
                model.BasicPublish(string.Empty, routingKey, body: body);
            }   
        }

        private static IEnumerable<string> ConvertFilesToJson(List<FileViewModel> files)
        {
            var filesJson = files.Select(x => JsonConvert.SerializeObject(x));
            return filesJson ?? new List<string>();
        }

        private static byte[] ConvertJsonToBytes(string fileJson)
        {
            var bytes = Encoding.UTF8.GetBytes(fileJson);
            return bytes;
        }
    }
}