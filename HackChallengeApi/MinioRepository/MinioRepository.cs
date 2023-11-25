using Minio;
using Minio.DataModel.Args;
using Minio.Exceptions;

namespace HackChallengeApi.MinioRepository;

public class MinioRepository(IMinioClient client)
{
    private readonly MinioClient _client = (MinioClient) client;
    private readonly string _bucketName = "test";
    private readonly string _contentType = "audio/mp3";


    public async Task PutTestObject()
    {
        var objectName = "test";
        var filePath = "test.mp3";

        try
        {
            // Make a bucket on the server, if not already present.
            var beArgs = new BucketExistsArgs()
                .WithBucket(_bucketName);
            bool found = await _client.BucketExistsAsync(beArgs).ConfigureAwait(false);
            if (!found)
            {
                var mbArgs = new MakeBucketArgs()
                    .WithBucket(_bucketName);
                await _client.MakeBucketAsync(mbArgs).ConfigureAwait(false);
            }
            // Upload a file to bucket.
            var putObjectArgs = new PutObjectArgs()
                .WithBucket(_bucketName)
                .WithObject(objectName)
                .WithFileName(filePath)
                .WithContentType(_contentType);
            await _client.PutObjectAsync(putObjectArgs).ConfigureAwait(false);
            Console.WriteLine("Successfully uploaded " + objectName );
        }
        catch (MinioException e)
        {
            Console.WriteLine("File Upload Error: {0}", e.Message);
        }
    }
    
    public async Task<byte[]> GetObject(string fileName = "test")
    {
        try
        {
            using var memStr = new MemoryStream();
            Console.WriteLine("Получение объекта из хранилища" + fileName);
            var args = new GetObjectArgs()
                .WithBucket(_bucketName)
                .WithObject(fileName)
                .WithFile(fileName + ".mp3")
                .WithCallbackStream(stream => { 
                    stream.CopyTo(memStr);
                    stream.Dispose();
                });
            await client.GetObjectAsync(args).ConfigureAwait(false);
            return memStr.ToArray();
        }
        catch (Exception e)
        {
            Console.WriteLine($"[Bucket]  Exception: {e}");
            throw;
        }
    }
}