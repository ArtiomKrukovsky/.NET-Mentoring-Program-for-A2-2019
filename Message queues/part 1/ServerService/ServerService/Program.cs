﻿namespace ServerService
{
    using MessageQueues;

    class Program
    {
        static void Main(string[] args)
        {
            //MQListener.ReceiveMessageAndPublish();
            MQListener.ReceiveChunkedMessages();
        }
    }
}
