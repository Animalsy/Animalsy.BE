﻿namespace Animalsy.BE.Services.ContractorsAPI.Models.Dto
{
    public record ResponseDto
    {
        public object? Result { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
