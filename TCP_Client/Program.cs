using System.Net;
using System.Net.Sockets;
using System.Text;

Console.WriteLine("***** TCP Client *****");

string message = String.Empty;
int port = 8005;
string address = "127.0.0.1";

try
{
    IPEndPoint localEP = new IPEndPoint(IPAddress.Parse(address), port);
    Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
    socket.Connect(localEP);

    Console.WriteLine("Введите сообщение для сервера!");

    do
    {
        message = Console.ReadLine();
    } while (message == null);
    
    
    byte[] buffer = Encoding.UTF8.GetBytes(message);
    socket.Send(buffer);

    buffer = new byte[256];
    StringBuilder builder = new StringBuilder();
    int bytes = 0;

    do
    {
        bytes = socket.Receive(buffer, buffer.Length, 0);
        builder.Append(Encoding.UTF8.GetString(buffer, 0, bytes));
    } while (socket.Available > 0);

    Console.WriteLine($"Ответ сервера: {builder}");

    socket.Shutdown(SocketShutdown.Both);
    socket.Close();
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}