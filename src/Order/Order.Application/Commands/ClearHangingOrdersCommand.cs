using System.ComponentModel.DataAnnotations;
using MediatR;

namespace Order.Application.Commands
{
    public class ClearHangingOrdersCommand: IRequest
    {
        [Required]
        public TimeSpan MaxOrderAge { get; init; }
    }
}

