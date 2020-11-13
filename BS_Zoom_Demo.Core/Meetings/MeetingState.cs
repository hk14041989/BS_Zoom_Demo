namespace BS_Zoom_Demo.Meetings
{
    /// <summary>
    /// Possible states of a <see cref="Meeting"/>.
    /// </summary>
    public enum MeetingState : byte
    {
        Waiting = 0,
        /// <summary>
        /// The task is active.
        /// </summary>
        Active = 1,

        /// <summary>
        /// The task is completed.
        /// </summary>
        Completed = 2
    }
}