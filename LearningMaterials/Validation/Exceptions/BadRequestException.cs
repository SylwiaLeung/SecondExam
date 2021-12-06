using System;

namespace LearningMaterials.Validation
{
    [Serializable]
    internal class BadRequestException : Exception
    {
        public BadRequestException(string message) : base(message)
        {
        }
    }
}