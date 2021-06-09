// <copyright file="LoaderBackgroundStatus.cs" company="None">
// Free and open source code.
// </copyright>
namespace Hilres.Stock.Updater
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;

    /// <summary>
    /// Stock database loader background status class.
    /// Show the status of what the background worker is doing.
    /// </summary>
    public class LoaderBackgroundStatus
    {
        private int count = 0;
        private LoaderRunMode mode = LoaderRunMode.Stop;
        private LoaderRunState state = LoaderRunState.Stopped;

        /// <summary>
        /// Property changed event.
        /// </summary>
        public event Func<string, object, Task> OnPropertyChangedAsync;

        /// <summary>
        /// Gets the iteration count.  (Example only).
        /// </summary>
        public int Count
        {
            get => this.count;
            internal set => this.SetPropertyField(ref this.count, value, nameof(this.Count));
        }

        /// <summary>
        /// Gets a value indicating whether the loader is running.
        /// </summary>
        public bool IsRunning => this.state == LoaderRunState.Running || this.state == LoaderRunState.Stopping;

        /// <summary>
        /// Gets or sets the mode of the loader background state.
        /// Change this to set the background service into a new state.
        /// </summary>
        public LoaderRunMode Mode
        {
            get => this.mode;
            set => this.SetPropertyField(ref this.mode, value, nameof(this.Mode));
        }

        /// <summary>
        /// Gets the state of the stock database loader.
        /// </summary>
        public LoaderRunState State
        {
            get => this.state;
            internal set => this.SetPropertyField(ref this.state, value, nameof(this.State));
        }

        /// <summary>
        /// Set property field.
        /// </summary>
        /// <typeparam name="T">type.</typeparam>
        /// <param name="field">Private field to update.</param>
        /// <param name="newValue">New value.</param>
        /// <param name="propertyName">Name of property.</param>
        protected void SetPropertyField<T>(ref T field, T newValue, [CallerMemberName] string propertyName = null)
        {
            if (!EqualityComparer<T>.Default.Equals(field, newValue))
            {
                T oldValue = field;
                field = newValue;
                this.OnPropertyChangedAsync?.Invoke(propertyName, (object)oldValue);
            }
        }
    }
}