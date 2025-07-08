using RakbnyMa_aak.Models;
using RakbnyMa_aak.Services.Interfaces;
using System;
using System.Threading.Tasks;
using static RakbnyMa_aak.Utilities.Enums;

namespace RakbnyMa_aak.Services.Implementations
{
    public class MockPaymentService : IPaymentService
    {
        private const decimal MAX_CARD_AMOUNT = 10000m;
        private const decimal MAX_VODAFONE_AMOUNT = 5000m;

        public async Task<PaymentResult> ProcessCardPayment(decimal amount, string cardToken)
        {
            // Simulate API call delay
            await Task.Delay(500);

            if (string.IsNullOrWhiteSpace(cardToken))
                return new PaymentResult(
                    PaymentStatus.Failed,
                    FailureReason: "Invalid card token",
                    Message: "Card token is required");

            if (amount <= 0)
                return new PaymentResult(
                    PaymentStatus.Failed,
                    FailureReason: "Invalid amount",
                    Message: "Amount must be positive");

            if (amount > MAX_CARD_AMOUNT)
                return new PaymentResult(
                    PaymentStatus.Failed,
                    FailureReason: "Amount limit exceeded",
                    Message: $"Maximum card payment is {MAX_CARD_AMOUNT} EGP");

            // Simulate random failures (10% chance)
            if (new Random().Next(0, 10) == 0)
                return new PaymentResult(
                    PaymentStatus.Failed,
                    FailureReason: "Payment gateway timeout",
                    Message: "Payment processing timed out");

            return new PaymentResult(
                PaymentStatus.Completed,
                TransactionId: $"CARD_{DateTime.UtcNow.Ticks}_{Guid.NewGuid().ToString().Substring(0, 8)}",
                Message: "Payment processed successfully");
        }

        public async Task<PaymentResult> ProcessVodafoneCashPayment(decimal amount, string phoneNumber)
        {
            // Simulate API call delay
            await Task.Delay(500);

            if (string.IsNullOrWhiteSpace(phoneNumber) || !phoneNumber.StartsWith("01"))
                return new PaymentResult(
                    PaymentStatus.Failed,
                    FailureReason: "Invalid phone number",
                    Message: "Valid Egyptian phone number required");

            if (amount <= 0)
                return new PaymentResult(
                    PaymentStatus.Failed,
                    FailureReason: "Invalid amount",
                    Message: "Amount must be positive");

            if (amount > MAX_VODAFONE_AMOUNT)
                return new PaymentResult(
                    PaymentStatus.Failed,
                    FailureReason: "Amount limit exceeded",
                    Message: $"Maximum Vodafone Cash payment is {MAX_VODAFONE_AMOUNT} EGP");

            // Simulate random failures (15% chance)
            if (new Random().Next(0, 7) == 0)
                return new PaymentResult(
                    PaymentStatus.Failed,
                    FailureReason: "Insufficient wallet balance",
                    Message: "User doesn't have enough balance in Vodafone Cash");

            return new PaymentResult(
                PaymentStatus.Completed,
                TransactionId: $"VODA_{DateTime.UtcNow.Ticks}",
                Message: "Vodafone Cash payment successful");
        }
    }
}