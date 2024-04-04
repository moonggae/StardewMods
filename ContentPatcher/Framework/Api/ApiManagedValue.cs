using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using StardewModdingAPI.Utilities;

namespace ContentPatcher.Framework.Api
{
    /// <summary>A parsed string linked to the Content Patcher context for an API consumer. This implementation is <strong>per-screen</strong>, so the result depends on the screen that's active when calling the members.</summary>
    internal class ApiManagedValue : IManagedValue
    {
        /*********
        ** Fields
        *********/
        /// <summary>The underlying conditions.</summary>
        private readonly PerScreen<IManagedValue> ManagedValues;


        /*********
        ** Accessors
        *********/
        /// <inheritdoc />
        [MemberNotNullWhen(false, nameof(ApiManagedConditions.ValidationError))]
        public bool IsValid => this.ManagedValues.Value.IsValid;

        /// <inheritdoc />
        public string? ValidationError => this.ManagedValues.Value.ValidationError;

        /// <inheritdoc />
        public bool IsReady => this.ManagedValues.Value.IsReady;

        /// <inheritdoc />
        public string? Value => this.ManagedValues.Value.Value;


        /*********
        ** Public methods
        *********/
        /// <summary>Construct an instance.</summary>
        /// <param name="parse">Get parsed conditions for the currently active screen.</param>
        public ApiManagedValue(Func<IManagedValue> parse)
        {
            this.ManagedValues = new PerScreen<IManagedValue>(parse);
        }

        /// <inheritdoc />
        public IEnumerable<int> UpdateContext()
        {
            return new HashSet<int>(
                this.ManagedValues
                    .GetActiveValues()
                    .Select(p => p.Value)
                    .SelectMany(p => p.UpdateContext())
            );
        }
    }
}
