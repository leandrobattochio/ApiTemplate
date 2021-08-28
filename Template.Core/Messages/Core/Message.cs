using System;
using AutoMapper;
using FluentValidation.Results;

namespace Template.Core.Messages
{
    public abstract class Message
    {
        [IgnoreMap]
        public Guid Id { get; protected set; }

        [IgnoreMap]
        public DateTime CreatedAt { get; protected set; }

        [IgnoreMap]
        public DateTime? FinalizedAt { get; protected set; }

        [IgnoreMap]
        public MessageType Type { get; protected set; }

        [IgnoreMap]
        public MessageStatus Status { get; protected set; }

        protected ValidationResult ValidationResult { get; set; }

        public Message()
        {
            ValidationResult = new ValidationResult();
        }

        public void UpdateCompletionTime(DateTime value)
        {
            FinalizedAt = value;
        }

        public ValidationResult GetValidationResult()
        {
            return ValidationResult;
        }

        public void SetStatus(MessageStatus status)
        {
            Status = status;
        }
    }

    public enum MessageType
    {
        Command = 1,
        Query
    }

    public enum MessageStatus
    {
        Nao_Executado = 0,
        Executado = 1,
        Executado_Com_Erros = 2
    }
}