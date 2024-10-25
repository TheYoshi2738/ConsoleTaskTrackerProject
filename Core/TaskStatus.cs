using System;

namespace Core
{
    public enum TaskStatus
    {
        Backlog,
        InProgress,
        Done,
        Trashed
    }

    public static class StatusExtension
    {
        public static string? GetStatusNameInRussian(this TaskStatus status)
        {
            switch (status)
            {
                case TaskStatus.Backlog:
                    return "В бэклоге";
                case TaskStatus.InProgress:
                    return "В работе";
                case TaskStatus.Done:
                    return "Завершена";
                case TaskStatus.Trashed:
                    return "Удалена";
                default:
                    return null;
            }
        }

        public static int GetPossibleStatusesCount(this TaskStatus status)
        {
            var statusesCount = 4; 
            return statusesCount;
        }
    }
}
