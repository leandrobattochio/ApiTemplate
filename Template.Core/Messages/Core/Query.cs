using System;
using System.Text.Json.Serialization;
using AutoMapper;
using FluentValidation.Results;
using MediatR;

namespace Template.Core.Messages
{
    /// <summary>
    /// Classe base de uma query do CQRS
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class Query<T> : Message, IRequest<T>
    {
        protected Query()
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.Now;
            Type = MessageType.Query;
        }
    }
}