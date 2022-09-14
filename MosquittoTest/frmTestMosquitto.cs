using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using MQTTnet.Client;
using MQTTnet.Extensions.ManagedClient;
using Newtonsoft.Json;
using Serilog;
using MQTTnet;

//using uPLibrary.Networking.M2Mqtt;
//using uPLibrary.Networking.M2Mqtt.Messages;

namespace MosquittoTest
{
    public partial class frmTestMosquitto : Form
    {

        IMqttClient mqttClient;
        IManagedMqttClient mqttSubClient;

        public frmTestMosquitto()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            var msg = new MqttApplicationMessageBuilder()
                    .WithTopic("andi/topic")
                    .WithPayload("Hello World " + new Random().Next().ToString() + " " + txtPayload.Text)
                    //.WithRetainFlag()
                    .Build();

            await mqttClient.PublishAsync(msg);

            Log.Logger.Information("MQTT application message is published.");
        }

        private void frmTestMosquitto_Load(object sender, EventArgs e)
        {
            Log.Logger = new LoggerConfiguration().CreateLogger();

            Log.Logger.ForContext<MqttClient>();
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger();

            Log.Logger.Information("Creating pub");
            MqttClientOptions puboptions = new MqttClientOptionsBuilder()
                .WithClientId("AndiMQTTPub")
                .WithTcpServer("darwinistic.com", 1883)
                .WithTimeout(TimeSpan.FromSeconds(60))
                .WithCleanSession()
                .WithWillQualityOfServiceLevel(MQTTnet.Protocol.MqttQualityOfServiceLevel.ExactlyOnce)
                .Build();

            mqttClient = new MqttFactory().CreateMqttClient();
            mqttClient.ConnectAsync(puboptions).Wait();

            MqttClientOptionsBuilder builder = new MqttClientOptionsBuilder()
                                            .WithClientId("AndiMQTTSub")
                                            .WithTcpServer("darwinistic.com", 1883)
                                            .WithCleanSession(true)
                                            .WithWillQualityOfServiceLevel(MQTTnet.Protocol.MqttQualityOfServiceLevel.ExactlyOnce)
                                            .WithTimeout(TimeSpan.FromSeconds(60));

            ManagedMqttClientOptions options = new ManagedMqttClientOptionsBuilder()
                        .WithAutoReconnectDelay(TimeSpan.FromSeconds(60))
                        .WithClientOptions(builder.Build())
                        .Build();

            mqttSubClient = new MqttFactory().CreateManagedMqttClient();

            mqttSubClient.ConnectedAsync += mqttClient_ConnectedAsync;
            mqttSubClient.DisconnectedAsync += mqttClient_DisconnectedAsync;
            mqttSubClient.ConnectingFailedAsync += mqttClient_ConnectingFailedAsync;
            mqttSubClient.ApplicationMessageReceivedAsync += MqttClient_ApplicationMessageReceivedAsync;

            mqttSubClient.SubscribeAsync("andi/topic");

            mqttSubClient.StartAsync(options).GetAwaiter().GetResult();

        }

        private Task MqttClient_ApplicationMessageReceivedAsync(MqttApplicationMessageReceivedEventArgs arg)
        {
            //Console.WriteLine("Message received: " + arg.ApplicationMessage.ConvertPayloadToString());
            Log.Logger.Information("Message received {0}", arg.ApplicationMessage.ConvertPayloadToString());
            arg.AcknowledgeAsync(CancellationToken.None).Wait();
            arg.IsHandled = true;
            arg.AutoAcknowledge = true;
            return Task.CompletedTask;
        }

        private Task mqttClient_ConnectingFailedAsync(ConnectingFailedEventArgs arg)
        {
            Log.Logger.Information("Connection failed {0}", arg.Exception.Message);
            return Task.CompletedTask;
        }

        private Task mqttClient_DisconnectedAsync(MqttClientDisconnectedEventArgs arg)
        {
            Log.Logger.Information("Disconnected {0}", arg.ReasonString);
            return Task.CompletedTask;
        }

        private Task mqttClient_ConnectedAsync(MqttClientConnectedEventArgs arg)
        {
            Log.Logger.Information("Connection successful {0}", arg.ConnectResult.AssignedClientIdentifier);
            return Task.CompletedTask;
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            await mqttClient.DisconnectAsync();
            await mqttSubClient.StopAsync();

            Application.Exit();
        }
    }
}
