using RabbitMQ.Client;
using System;
using System.Globalization;
using System.Text;
using System.Windows.Forms;

namespace IM
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "hello",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                string message = DateTime.Now.ToString(CultureInfo.InvariantCulture);
                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "",
                                     routingKey: "hello",
                                     basicProperties: null,
                                     body: body);
                string text = $" [x] Sent {message}";
                richTextBox1.Text += text + "\r";
            }
        }
    }
}
