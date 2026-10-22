using System;
using System.Text;

namespace SabreTools.RedumpLib.Web
{
    /// <summary>
    /// String wrapper for event arguments
    /// </summary>
    public class WebClientEventArgs : EventArgs
    {
        /// <summary>
        /// String represented by the event arguments
        /// </summary>
        private readonly string _value;

        /// <summary>
        /// Indicate if this event is a debug statement
        /// </summary>
        private readonly bool _debug;

        /// <summary>
        /// Indicate if this event is an error statement
        /// </summary>
        private readonly bool _error;

        /// <summary>
        /// Constructor for string values
        /// </summary>
        public WebClientEventArgs(string? value, bool debug = false, bool error = false)
        {
            _value = value ?? string.Empty;
            _debug = debug;
            _error = error;
        }

        /// <summary>
        /// Constructor for StringBuilder values
        /// </summary>
        public WebClientEventArgs(StringBuilder? value, bool debug = false, bool error = false)
        {
            _value = value?.ToString() ?? string.Empty;
            _debug = debug;
            _error = error;
        }

        /// <summary>
        /// Event arguments are just the value of the string contained within
        /// </summary>
        public static implicit operator string(WebClientEventArgs args)
        {
            if (args._debug)
                return $"DEBUG: {args._value}";
            else if (args._error)
                return $"ERROR: {args._value}";
            else
                return args._value;
        }

        /// <summary>
        /// Event arguments are just the value of the string contained within
        /// </summary>
        public static implicit operator StringBuilder(WebClientEventArgs args)
        {
            if (args._debug)
                return new($"DEBUG: {args._value}");
            else if (args._error)
                return new($"ERROR: {args._value}");
            else
                return new(args._value);
        }

        /// <summary>
        /// Event arguments are just the value of the string contained within
        /// </summary>
        public static implicit operator WebClientEventArgs(string? str) => new(str);

        /// <summary>
        /// Event arguments are just the value of the string contained within
        /// </summary>
        public static implicit operator WebClientEventArgs(StringBuilder? sb) => new(sb);
    }
}
