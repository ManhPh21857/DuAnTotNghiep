using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Project.Core.ApplicationService.Commands;
using Project.Core.Domain;
using Project.Sales.Domain.Payments;
using Project.Sales.Integration.Payments.Command;
using RestSharp;
using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace Project.Sales.ApplicationService.Payments.Command
{
    public class CreateMoMoPaymentCommandHandler
        : CommandHandler<CreateMoMoPaymentCommand, CreateMoMoPaymentCommandResult>
    {
        private readonly IMemoryCache memoryCache;

        private readonly MoMoOption option;

        public CreateMoMoPaymentCommandHandler(IConfiguration configuration, IMemoryCache memoryCache)
        {
            this.memoryCache = memoryCache;
            this.option = new MoMoOption
            {
                MomoApiUrl = configuration["MoMoPayment:MoMoApiUrl"],
                SecretKey = configuration["MoMoPayment:SecretKey"],
                AccessKey = configuration["MoMoPayment:AccessKey"],
                ReturnUrl = configuration["MoMoPayment:ReturnUrl"],
                NotifyUrl = configuration["MoMoPayment:NotifyUrl"],
                PartnerCode = configuration["MoMoPayment:PartnerCode"],
                RequestType = configuration["MoMoPayment:RequestType"]
            };
        }

        public async override Task<CreateMoMoPaymentCommandResult> Handle(
            CreateMoMoPaymentCommand request,
            CancellationToken cancellationToken
        )
        {
            var orderPaymentInfo = new OrderPaymentInfo
            {
                Id = request.OrderId,
                Code = request.OrderCode.ToString(),
                FullName = request.CustomerName,
                Content = "Thanh toán đơn hàng"
            };

            var model = new MoMoRequestInfo
            {
                FullName = request.CustomerName,
                Amount = request.Amount,
                OrderId = DateTime.UtcNow.Ticks.ToString(),
                OrderCode = request.OrderCode.ToString(),
                OrderInfo =
                    $"Mã hóa đơn : {request.OrderCode}. Khách hàng: {request.CustomerName}. Nội dung: Thanh toán đơn hàng.",
                ExtraData = JsonConvert.SerializeObject(orderPaymentInfo)
            };

            var rawData =
                $"partnerCode={this.option.PartnerCode}&accessKey={this.option.AccessKey}&requestId={model.OrderCode}&amount={model.Amount}&orderId={model.OrderId}&orderInfo={model.OrderInfo}&returnUrl={this.option.ReturnUrl}&notifyUrl={this.option.NotifyUrl}{model.OrderId}&extraData={model.ExtraData}";

            var signature = this.ComputeHmacSha256(rawData, this.option.SecretKey);

            var client = new RestClient(this.option.MomoApiUrl);
            var paymentRequest = new RestRequest { Method = Method.Post };
            paymentRequest.AddHeader("Content-Type", "application/json; charset=UTF-8");

            // Create an object representing the request data
            var requestData = new
            {
                accessKey = this.option.AccessKey,
                partnerCode = this.option.PartnerCode,
                requestType = this.option.RequestType,
                notifyUrl = $"{this.option.NotifyUrl}{model.OrderId}",
                returnUrl = this.option.ReturnUrl,
                orderId = model.OrderId,
                amount = model.Amount.ToString(),
                orderInfo = model.OrderInfo,
                requestId = model.OrderCode,
                extraData = model.ExtraData,
                signature = signature
            };

            paymentRequest.AddParameter("application/json",
                JsonConvert.SerializeObject(requestData),
                ParameterType.RequestBody);

            var response = await client.ExecuteAsync(paymentRequest, cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                throw new DomainException(
                    HttpStatusCode.BadRequest.GetHashCode().ToString(),
                    nameof(HttpStatusCode.BadRequest)
                );
            }

            var result = JsonConvert.DeserializeObject<MoMoCreatePaymentResponse>(response.Content);

            if (result is null)
            {
                throw new DomainException(
                    HttpStatusCode.BadRequest.GetHashCode().ToString(),
                    nameof(HttpStatusCode.BadRequest)
                );
            }

            if (result.ErrorCode != 0)
            {
                throw new DomainException(result.ErrorCode.ToString(), result.LocalMessage);
            }

            this.SetMemoryCache(model.OrderCode, model.OrderId);

            return new CreateMoMoPaymentCommandResult(result.PayUrl);
        }

        private void SetMemoryCache(string requestId, string orderId)
        {
            var cacheExpiryOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpiration = DateTime.Now.AddHours(2),
                Priority = CacheItemPriority.High,
                SlidingExpiration = TimeSpan.FromHours(2),
                Size = 1024,
            };

            this.memoryCache.Set("requestId", requestId, cacheExpiryOptions);
            this.memoryCache.Set("orderId", orderId, cacheExpiryOptions);
        }

        private string ComputeHmacSha256(string message, string secretKey)
        {
            var keyBytes = Encoding.UTF8.GetBytes(secretKey);
            var messageBytes = Encoding.UTF8.GetBytes(message);

            byte[] hashBytes;

            using (var hmac = new HMACSHA256(keyBytes))
            {
                hashBytes = hmac.ComputeHash(messageBytes);
            }

            var hashString = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();

            return hashString;
        }
    }
}
