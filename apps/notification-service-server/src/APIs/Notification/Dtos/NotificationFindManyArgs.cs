using Microsoft.AspNetCore.Mvc;
using NotificationService.APIs.Common;
using NotificationService.Infrastructure.Models;

namespace NotificationService.APIs.Dtos;

[BindProperties(SupportsGet = true)]
public class NotificationFindManyArgs : FindManyInput<Notification, NotificationWhereInput> { }
