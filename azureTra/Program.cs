#region Using
using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;
using Azure.Storage.Blobs;
using NAudio.Wave;
#endregion

class Program
{
    static async Task Main(string[] args)
    {
        var speechConfig = SpeechConfig.FromSubscription("5a8f54eafc074fe9a92944d3734e748f", "eastus");

        speechConfig.SpeechRecognitionLanguage = "es-MX";

        // Nombre del archivo de audio en el blob storage
        string audioBlobName = "baa.wav"; // Cambia esto al nombre de tu archivo de video
        string transcriptionFilePath = @"C:\Users\jona2\Downloads\Prueba.txt"; // Ruta para guardar la transcripción
         

        // Obtener el archivo de audio del blob storage
        string audioFilePath = await DownloadAudioFromBlobStorage(audioBlobName);

        // Convertir el archivo a formato WAV si no lo es
        if (!audioFilePath.ToLower().EndsWith(".wav"))
        {
            string convertedFilePath = ConvertToWav(audioFilePath);
            if (convertedFilePath != null)
            {
                audioFilePath = convertedFilePath;
            }
            else
            {
                Console.WriteLine("No se pudo convertir el archivo a formato WAV.");
                return;
            }
        }

        // Creamos un objeto de AudioConfig desde el archivo de audio
        var audioConfig = AudioConfig.FromWavFileInput(audioFilePath);

        var speechRecognizer = new SpeechRecognizer(speechConfig, audioConfig);
        Console.WriteLine("Procesando audio... esto puede tardar un momento.");

        // Abrimos el archivo de texto para escribir la transcripción
        using (StreamWriter transcriptionWriter = new StreamWriter(transcriptionFilePath))
        {
            // Comenzamos a transcribir el archivo
            speechRecognizer.Recognized += (s, e) =>
            {
                if (e.Result.Reason == ResultReason.RecognizedSpeech)
                {
                    transcriptionWriter.WriteLine(e.Result.Text);
                }
            };
            await speechRecognizer.StartContinuousRecognitionAsync();
            // Deberías manejar el tiempo de duración del audio, como obtenerlo del metadata del blob storage
            await Task.Delay(TimeSpan.FromSeconds(200)); 
            await speechRecognizer.StopContinuousRecognitionAsync();
            Console.WriteLine("Proceso de reconocimiento finalizado.");
        }
        Console.ReadKey();
    }

    static string ConvertToWav(string inputFile)
    {
        try
        {
            string outputFile = Path.ChangeExtension(inputFile, ".wav");

            using (var reader = new MediaFoundationReader(inputFile))
            {
                WaveFileWriter.CreateWaveFile(outputFile, reader);
            }

            return outputFile;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al convertir el archivo a WAV: {ex.Message}");
            return null;
        }
    }

    static async Task<string> DownloadAudioFromBlobStorage(string blobName)
    {
        // Configurar la conexión al almacenamiento de blobs
        string connectionString = "DefaultEndpointsProtocol=https;AccountName=qa32principal;AccountKey=C6FOROukZo4OAO+3nap2ViX+TR/7e+tsll6hNHp34xmwacshyk+e8UDi56wT4NlLEBYLe2VaT2mM+ASt88Ybtw==;EndpointSuffix=core.windows.net"; // Cambia esto por tu cadena de conexión a Azure Blob Storage
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
