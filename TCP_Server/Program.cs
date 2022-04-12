using System.Net;
using System.Net.Sockets;
using System.Text;

Console.WriteLine("***** TCP Server *****");

string message = "Ваше сообщение доставлено";
int port = 8005;
IPEndPoint localEP = new IPEndPoint(IPAddress.Loopback, port);
Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

try
{
    socket.Bind(localEP);
    socket.Listen(10);

    Console.WriteLine("Сервер запущен!");

    while (true)
    {
        Socket handler = socket.Accept();
        StringBuilder builder = new StringBuilder();
        int bytes = 0;
        byte[] buffer = new byte[256];

        do
        {
            bytes = handler.Receive(buffer);
            builder.Append(Encoding.UTF8.GetString(buffer, 0, bytes));
        } while (handler.Available > 0);

        Console.WriteLine(DateTime.Now.ToShortTimeString() + $": {builder}");

        buffer = Encoding.UTF8.GetBytes(message);
        handler.Send(buffer);
        handler.Shutdown(SocketShutdown.Both);
        handler.Close();
    }
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}