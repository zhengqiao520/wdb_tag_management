using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aliyun.OSS;
using System.IO;
using Aliyun.OSS.Common;

namespace Infrastructure
{
    public class OSSUtils
    {
        public const string AccessKeyId = "LTAI3FIgF26XglMg";
        public const string AccessKeySecret = "blEXM575lLjKYHpbclm0t4fmdCxz8l";
        private static OssClient ossClient;
        private static object obj = new object();
        private  OSSUtils(){ }
        public static OssClient OssClient
        {
            get
            {
                lock (obj)
                {
                    if (null == ossClient)
                    {
                        ossClient = new OssClient("oss-cn-hangzhou.aliyuncs.com/", AccessKeyId, AccessKeySecret);
                    }
                    return ossClient;
                }
            }
        }
        public static PutObjectResult PutObjectProgress(string filePath, string keyName,string bucketName = "wdb-test")
        {
            try 
            {
                using (var fs = File.Open(filePath, FileMode.Open,FileAccess.Read,FileShare.Read))
                {
                    var putObjectRequest = new PutObjectRequest(bucketName, $"{keyName}", fs);
                    putObjectRequest.StreamTransferProgress += streamProgressCallback;
                    return  OssClient.PutObject(putObjectRequest);
                }
            }
            catch (OssException ex)
            {
                Console.WriteLine("Failed with error code: {0}; Error info: {1}. \nRequestID:{2}\tHostID:{3}",
                    ex.ErrorCode, ex.Message, ex.RequestId, ex.HostId);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed with error info: {0}", ex.Message);
            }
            return null;
        }
        private static void streamProgressCallback(object sender, StreamTransferProgressArgs args)
        {
            System.Console.WriteLine("ProgressCallback - TotalBytes:{0}, TransferredBytes:{1}",
                args.TotalBytes, args.TransferredBytes);
        }
    }
}
