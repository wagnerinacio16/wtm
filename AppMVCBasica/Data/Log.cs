using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32.SafeHandles;
using System.Diagnostics;
using System.Net;
using System.Text;

namespace wtm.Data
{
    public class Log
    {
        public string path = String.Concat(Directory.GetCurrentDirectory(), @"\Data\LogOperacoes.txt");
        Stream stream = null;
        WebClient webClient;

        public Log()
        {
            stream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
        }

        public void GerarLogInsert(Object objeto, Stopwatch time, int quantidade)
        {
            Type type = objeto.GetType();
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Insert de {type.Name} ");
            sb.AppendLine($"Quantidade: {quantidade}");
            sb.AppendLine($"Tempo da Operação: {time.Elapsed}");
            sb.AppendLine("|-------------------------------|");

            GravarLog(sb.ToString());
        }

        public void GerarLogUpdate(Object objeto, Stopwatch time, int quantidade)
        {
            Type type = objeto.GetType();
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Update de {type.Name} ");
            sb.AppendLine($"Quantidade: {quantidade}");
            sb.AppendLine($"Tempo da Operação: {time.Elapsed}");
            sb.AppendLine("|-------------------------------|");

            GravarLog(sb.ToString());
        }

        public void GerarLogDelete(Object objeto, Stopwatch time, int quantidade)
        {
            Type type = objeto.GetType();
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Delete de {type.Name} ");
            sb.AppendLine($"Quantidade: {quantidade}");
            sb.AppendLine($"Tempo da Operação: {time.Elapsed}");
            sb.AppendLine("|-------------------------------|");

            GravarLog(sb.ToString());
        }

        public void GravarLog(string log)
        {
            //StreamReader readerContent = new StreamReader(stream);
            //var text = readerContent.ReadLine;

            using (StreamWriter writer = new StreamWriter(stream))
            {
                //writer.WriteLine(text);
                writer.WriteLine(log);
                writer.Close();
                writer.Dispose();
            }
        }

      
            
    }
}
