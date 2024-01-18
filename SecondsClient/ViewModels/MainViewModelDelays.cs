using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondsClient.ViewModels
{
    public readonly record struct MainViewModelDelays
    {
        public required int InstructionVisisbleDurationMs { get; init; }
        public required int GetReadyVisisbleDurationMs { get; init; }
        public required int GoVisisbleDurationMs { get; init; }
        public required int PauseBetweenRoundsDurationMs { get; init; }
    }
}
