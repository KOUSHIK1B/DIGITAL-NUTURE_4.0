//using Confluent.Kafka;
//using System;
//using System.Threading;
//using System.Windows.Forms;

//namespace KafkaWinFormsChat
//{
//    public partial class Form1 : Form
//    {
//        private const string topic = "chat-topic";
//        private const string bootstrapServers = "localhost:9092";
//        private CancellationTokenSource cancelToken;

//        public Form1()
//        {
//            InitializeComponent();
//            StartConsumer();
//        }

//        private async void btnSend_Click(object sender, EventArgs e)
//        {
//            var config = new ProducerConfig { BootstrapServers = bootstrapServers };

//            using (var producer = new ProducerBuilder<Confluent.Kafka.Null, string>(config).Build())
//            {
//                try
//                {
//                    await producer.ProduceAsync(topic, new Message<Confluent.Kafka.Null, string> { Value = txtMessage.Text });
//                    lstMessages.Items.Add("You: " + txtMessage.Text);
//                    txtMessage.Clear();
//                }
//                catch (Exception ex)
//                {
//                    MessageBox.Show("Error producing message: " + ex.Message);
//                }
//            }
//        }

//        private void StartConsumer()
//        {
//            cancelToken = new CancellationTokenSource();
//            var config = new ConsumerConfig
//            {
//                BootstrapServers = bootstrapServers,
//                GroupId = Guid.NewGuid().ToString(),
//                AutoOffsetReset = AutoOffsetReset.Earliest
//            };

//            Thread thread = new Thread(() =>
//            {
//                using (var consumer = new ConsumerBuilder<Confluent.Kafka.Ignore, string>(config).Build())
//                {
//                    consumer.Subscribe(topic);

//                    try
//                    {
//                        while (!cancelToken.Token.IsCancellationRequested)
//                        {
//                            var cr = consumer.Consume(cancelToken.Token);
//                            Invoke(new Action(() =>
//                            {
//                                lstMessages.Items.Add("Friend: " + cr.Message.Value);
//                            }));
//                        }
//                    }
//                    catch (OperationCanceledException) { }
//                    finally
//                    {
//                        consumer.Close();
//                    }
//                }
//            });

//            thread.IsBackground = true;
//            thread.Start();
//        }

//        protected override void OnFormClosing(FormClosingEventArgs e)
//        {
//            if (cancelToken != null)
//            {
//                cancelToken.Cancel();
//            }
//            base.OnFormClosing(e);
//        }

//        private void button1_Click(object sender, EventArgs e)
//        {

//        }
//    }
//}

using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Confluent.Kafka;

namespace KafkaChatApp
{
    public partial class Form1 : Form
    {
        private const string bootstrapServers = "localhost:9092";
        private const string topicName = "chat-topic";
        private IProducer<Null, string> producer;
        private CancellationTokenSource cts = new CancellationTokenSource();

        public Form1()
        {
            InitializeComponent();
            InitializeKafkaProducer();
            StartKafkaConsumer();
        }

        // Initialize Kafka Producer
        private void InitializeKafkaProducer()
        {
            var config = new ProducerConfig
            {
                BootstrapServers = bootstrapServers
            };
            producer = new ProducerBuilder<Null, string>(config).Build();
        }

        // Kafka Consumer in Background
        private void StartKafkaConsumer()
        {
            Task.Run(() =>
            {
                var config = new ConsumerConfig
                {
                    BootstrapServers = bootstrapServers,
                    GroupId = $"chat-consumer-{Guid.NewGuid()}",
                    AutoOffsetReset = AutoOffsetReset.Earliest
                };

                 var consumer = new ConsumerBuilder<Ignore, string>(config).Build();
                consumer.Subscribe(topicName);

                try
                {
                    while (!cts.Token.IsCancellationRequested)
                    {
                        var cr = consumer.Consume(cts.Token);
                        Invoke(new Action(() =>
                        {
                            lstMessages.Items.Add(cr.Message.Value);
                        }));
                    }
                }
                catch (OperationCanceledException)
                {
                    consumer.Close();
                }
            });
        }

        // Send button click
        private async void button1_Click(object sender, EventArgs e)
        {
            var message = txtMessage.Text.Trim();
            if (!string.IsNullOrEmpty(message))
            {
                await producer.ProduceAsync(topicName, new Message<Null, string> { Value = message });
                txtMessage.Clear();
            }
        }

        // Clean up
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            cts.Cancel();
            base.OnFormClosing(e);
        }
    }
}
