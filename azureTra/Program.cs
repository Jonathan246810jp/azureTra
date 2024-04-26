//#region Using
//using System;
//using System.IO;
//using System.Threading.Tasks;
//using Microsoft.CognitiveServices.Speech;
//using Microsoft.CognitiveServices.Speech.Audio;
//using NAudio.Wave;
//#endregion
//class Program
//{
//    static async Task Main(string[] args)
//    {
//        var speechConfig = SpeechConfig.FromSubscription("5a8f54eafc074fe9a92944d3734e748f", "eastus");
//        speechConfig.SpeechRecognitionLanguage = "es-MX";
//        string audioFilePath = @"C:\Users\jona2\Downloads\vi.mp4";
//        string transcriptionFilePath = @"C:\Users\jona2\Downloads\Prueba.txt";
//        // Convertimos archivo de audio a WAV
//        string wavFilePath = ConvertToWav(audioFilePath);
//        // Creamos objeto de AudioConfig desde el archivo WAV
//        var audioConfig = AudioConfig.FromWavFileInput(wavFilePath);
//        var speechRecognizer = new SpeechRecognizer(speechConfig, audioConfig);
//        Console.WriteLine("Procesando audio esto tardara un rato :) ...");
//        // Abrir el archivo de texto para escribir la transcripción
//        using (StreamWriter transcriptionWriter = new StreamWriter(transcriptionFilePath))
//        {
//            //esto lo comente ya que muestra en consola la transcripcion con saltos de parrafos
//            // Iniciar reconocimiento continuo
//            //speechRecognizer.Recognizing += (s, e) =>
//            //{
//            //    Console.WriteLine($"Texto reconocido: {e.Result.Text}");
//            //};

//            //transcribimos el archivo
//            speechRecognizer.Recognized += (s, e) =>
//            {
//                if (e.Result.Reason == ResultReason.RecognizedSpeech)
//                {
//                    transcriptionWriter.WriteLine(e.Result.Text);
//                }
//            };
//            await speechRecognizer.StartContinuousRecognitionAsync();
//            // Cambiar a la duración deseada
//        await Task.Delay(TimeSpan.FromSeconds(300));
//            // Detener reconocimiento continuo
//          await speechRecognizer.StopContinuousRecognitionAsync();
//            Console.WriteLine("Proceso de reconocimiento finalizado.");
//        }
//        Console.ReadKey();
//    }


//    /// <summary>
//    /// Metodo  que ocupamos para quitar el formato que tenga el archivo y cambiarlo por wav 
//    /// </summary>
//    /// <param name="audioFilePath">trae el tipo de archivo con su terminacion</param>
//    /// <returns> el archivp con su nuevo formato</returns>
//    /// <exception cref="NotSupportedException"></exception>
//    static string ConvertToWav(string audioFilePath)
//    {
//        string wavFilePath = Path.ChangeExtension(audioFilePath, ".wav");

//        if (Path.GetExtension(audioFilePath).Equals(".mp3", StringComparison.OrdinalIgnoreCase))
//        {
//            using (var reader = new Mp3FileReader(audioFilePath))
//            {
//                WaveFileWriter.CreateWaveFile(wavFilePath, reader);
//            }
//        }
//        else if (Path.GetExtension(audioFilePath).Equals(".mp4", StringComparison.OrdinalIgnoreCase))
//        {
//            using (var reader = new MediaFoundationReader(audioFilePath))
//            {
//                WaveFileWriter.CreateWaveFile(wavFilePath, reader);
//            }
//        }
//        else
//        {
//            throw new NotSupportedException("Formato de archivo no compatible. Solo se admiten archivos MP3 o MP4.");
//        }

//        return wavFilePath;
//    }
//}


//#region Using
//using System;
//using System.IO;
//using System.Threading.Tasks;
//using Microsoft.CognitiveServices.Speech;
//using Microsoft.CognitiveServices.Speech.Audio;
//using NAudio.Wave;
//#endregion
//class Program
//{
//    static async Task Main(string[] args)
//    {
//        var speechConfig = SpeechConfig.FromSubscription("5a8f54eafc074fe9a92944d3734e748f", "eastus");
//        speechConfig.SpeechRecognitionLanguage = "es-MX";
//        string audioFilePath = @"C:\Users\jona2\Downloads\vi.mp4";
//        string transcriptionFilePath = @"C:\Users\jona2\Downloads\Prueba.txt";
//        // Convertimos archivo de audio a WAV
//        string wavFilePath = ConvertToWav(audioFilePath);
//        // Creamos un objeto de AudioConfig desde el archivo WAV
//        var audioConfig = AudioConfig.FromWavFileInput(wavFilePath);
//        // aqui obtenemos la duración del archivo
//        var audioDuration = GetAudioDuration(audioFilePath);
//        var speechRecognizer = new SpeechRecognizer(speechConfig, audioConfig);
//        Console.WriteLine("Procesando audio esto tardará un rato :) ...");
//        // aqui abrimos el archivo de texto para escribir la transcripción
//        using (StreamWriter transcriptionWriter = new StreamWriter(transcriptionFilePath))
//        {
//            // comienza a transcribir el archivo
//            speechRecognizer.Recognized += (s, e) =>
//            {
//                if (e.Result.Reason == ResultReason.RecognizedSpeech)
//                {
//                    transcriptionWriter.WriteLine(e.Result.Text);
//                }
//            };
//            await speechRecognizer.StartContinuousRecognitionAsync();
//            //aqui le pasamos el tiempo que dura el video para que lo transcriba
//            await Task.Delay(audioDuration);
//            // detenemos la transcripcion
//            await speechRecognizer.StopContinuousRecognitionAsync();
//            Console.WriteLine("Proceso de reconocimiento finalizado.");
//        }
//        Console.ReadKey();
//    }
//    static TimeSpan GetAudioDuration(string audioFilePath)
//    {
//        using (var audioFile = TagLib.File.Create(audioFilePath))
//        {
//            return audioFile.Properties.Duration;
//        }
//    }
//    /// <summary>
//    /// Metodo  que ocupamos para quitar el formato que tenga el archivo y cambiarlo por wav 
//    /// </summary>
//    /// <param name="audioFilePath">trae el tipo de archivo con su terminacion</param>
//    /// <returns> el archivp con su nuevo formato</returns>
//    /// <exception cref="NotSupportedException"></exception>
//    static string ConvertToWav(string audioFilePath)
//    {
//        string wavFilePath = Path.ChangeExtension(audioFilePath, ".wav");

//        if (Path.GetExtension(audioFilePath).Equals(".mp3", StringComparison.OrdinalIgnoreCase))
//        {
//            using (var reader = new Mp3FileReader(audioFilePath))
//            {
//                WaveFileWriter.CreateWaveFile(wavFilePath, reader);
//            }
//        }
//        else if (Path.GetExtension(audioFilePath).Equals(".mp4", StringComparison.OrdinalIgnoreCase))
//        {
//            using (var reader = new MediaFoundationReader(audioFilePath))
//            {
//                WaveFileWriter.CreateWaveFile(wavFilePath, reader);
//            }
//        }
//        else
//        {
//            throw new NotSupportedException("Formato de archivo no compatible. Solo se admiten archivos MP3 o MP4.");
//        }

//        return wavFilePath;
//    }
//}


#region Using
using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;
using Azure.Storage.Blobs;
using NAudio.Wave;
using static System.Net.WebRequestMethods;
using System.Diagnostics;
#endregion

class Program
{
    static async Task Main(string[] args)
    {
        string ffmpegPath = @"C:\Users\jona2\source\repos\azureTra\azureTra\tp\FFmpeg\bin\ffmpeg.exe";
        var speechConfig = SpeechConfig.FromSubscription("5a8f54eafc074fe9a92944d3734e748f", "eastus");

        speechConfig.SpeechRecognitionLanguage = "es-MX";
        // Nombre del archivo en el blogstorage
        string audioBlobName = "0024cabf-5038-463f-bd2c-347d198a4329.avi";
        string audioSalida = "supervision/0024cabf-5038-463f-bd2c-347d198a4329.mp4";
        string transcriptionFilePath = @"C:\Users\jona2\Downloads\Prueba.txt";

        string arguments = $"-i \"{audioBlobName}\" -c:v libx264 -c:a aac -strict experimental \"{audioSalida}\"";
        ProcessStartInfo processStartInfo = new ProcessStartInfo
        {
            FileName = ffmpegPath,
            Arguments = arguments,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        Process process = new Process();
        process.StartInfo = processStartInfo;
        process.Start();

        // Obtener el archivo de audio del blogstorage
        string audioFilePath = await DownloadAudioFromBlobStorage(audioSalida);
        
        // Convertimos archivo de audio a WAV si es necesario
        string wavFilePath = ConvertToWav(audioFilePath);

        // Creamos un objeto de AudioConfig desde el archivo WAV
        var audioConfig = AudioConfig.FromWavFileInput(wavFilePath);

        var speechRecognizer = new SpeechRecognizer(speechConfig, audioConfig);
        Console.WriteLine("Procesando audio esto tardará un rato :) ...");

        // aqui abrimos el archivo de texto para escribir la transcripción
        using (StreamWriter transcriptionWriter = new StreamWriter(transcriptionFilePath))
        {
            // comienza a transcribir el archivo
            speechRecognizer.Recognized += (s, e) =>
            {
                if (e.Result.Reason == ResultReason.RecognizedSpeech)
                {
                    transcriptionWriter.WriteLine(e.Result.Text);
                }
            };

            await speechRecognizer.StartContinuousRecognitionAsync();

            // Aquí deberías manejar el tiempo de duración del audio, como obtenerlo del metadata del blobstorage

            await Task.Delay(TimeSpan.FromSeconds(60)); // Ejemplo: transcribir durante 60 segundos

            // detenemos la transcripcion
            await speechRecognizer.StopContinuousRecognitionAsync();
            Console.WriteLine("Proceso de reconocimiento finalizado.");
        }
        Console.ReadKey();
    }
    //<summary>
        /// Metodo  que ocupamos para quitar el formato que tenga el archivo y cambiarlo por wav 
        /// </summary>
        /// <param name="audioFilePath">trae el tipo de archivo con su terminacion</param>
        /// <returns> el archivp con su nuevo formato</returns>
        /// <exception cref="NotSupportedException"></exception>
        static string ConvertToWav(string audioFilePath)
    {
        string wavFilePath = Path.ChangeExtension(audioFilePath, ".wav");

        if (Path.GetExtension(audioFilePath).Equals(".mp3", StringComparison.OrdinalIgnoreCase))
        {
            using (var reader = new Mp3FileReader(audioFilePath))
            {
                WaveFileWriter.CreateWaveFile(wavFilePath, reader);
            }
        }
        else if (Path.GetExtension(audioFilePath).Equals(".mp4", StringComparison.OrdinalIgnoreCase))
        {
            using (var reader = new MediaFoundationReader(audioFilePath))
            {
                WaveFileWriter.CreateWaveFile(wavFilePath, reader);
            }
        }
        else
        {
            throw new NotSupportedException("Formato de archivo no compatible. Solo se admiten archivos MP3 o MP4.");
        }

        return wavFilePath;
    }

    static async Task<string> DownloadAudioFromBlobStorage(string blobName)
    {
        // Configurar la conexión al almacenamiento de blobs
        string connectionString = "DefaultEndpointsProtocol=https;AccountName=qa32principal;AccountKey=C6FOROukZo4OAO+3nap2ViX+TR/7e+tsll6hNHp34xmwacshyk+e8UDi56wT4NlLEBYLe2VaT2mM+ASt88Ybtw==;EndpointSuffix=core.windows.net"; // Cambia esto por tu conexión a Azure Blob Storage
        string containerName = "supervision"; // Cambia esto por el nombre de tu contenedor

      
        BlobServiceClient blobServiceClient = new BlobServiceClient(connectionString);

        // Obtener una referencia al contenedor
        BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(containerName);

        // Descargar el blob a un archivo temporal
        string localFilePath = Path.GetTempFileName();
        BlobClient blobClient = containerClient.GetBlobClient(blobName);
        await blobClient.DownloadToAsync(localFilePath);

        return localFilePath;
    }

}
