namespace ImageRecognizer.Web.Models
{
    public enum RecognitionResultEnum
    {
        /// <summary>
        /// The text recognition process has not started.
        /// </summary>
        NotStarted

        ,

        /// <summary>
        /// The text recognition is being processed.
        /// </summary>
        Running

        ,

        /// <summary>
        /// The text recognition process failed.
        /// </summary>
        Failed

        ,

        /// <summary>
        /// The text recognition process succeeded.
        /// </summary>
        Succeeded
    }
}