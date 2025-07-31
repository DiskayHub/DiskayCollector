using System.Globalization;
using DiskayCollector.Application.Contracts;
using DiskayCollector.Application.Contracts.Other;
using DiskayCollector.Core.Models;

namespace DiskayCollector.Application.Data;

public class ScheduleFormatter {
    public static DayScheduleEntity? FormatScheduleDay(List<ApiItem> data, DateOnly dayFilter, string group) {
        var items = new List<ItemEntity>();

        foreach (ApiItem item in data) {
            var subGroupItems = new List<SubGroupItemEntity>();
            if (item.SubGroup != null) {
                foreach (ApiSubGroup subItem in item.SubGroup){
                    var subGroupItem = new SubGroupItemEntity (
                        name: subItem.STitle,
                        description: subItem.STopic,
                        roomName: subItem.SGCaID,
                        subGroup: new GroupEntity(subItem.SGrID)
                    );
                    subGroupItems.Add(subGroupItem);
                }
            }

            var time_start = DateTime.ParseExact(item.start, "yyyy-MM-dd HH:mm", 
                CultureInfo.InvariantCulture);
            var time_end = DateTime.ParseExact(item.end, "yyyy-MM-dd HH:mm", 
                CultureInfo.InvariantCulture);
            
            if (DateOnly.FromDateTime(time_end) == dayFilter) {
                var itemEntity = new ItemEntity (
                    name: item.title,
                    description: item.topic,
                    roomName: item.room,
                    subGroupsItems: subGroupItems.Count != 0 ? subGroupItems : null,
                    startTime: TimeOnly.FromDateTime(time_start),
                    endTime: TimeOnly.FromDateTime(time_end)
                );
                    
                items.Add(itemEntity);
            }
        }
        if (items.Count != 0){
            var dayEntity = new DayScheduleEntity(
                day: dayFilter,
                items: items,
                mainGroup: new GroupEntity(group)
            );
            return dayEntity;
        }
        return null;
    }

    public static List<DayScheduleEntity?> FormatSchedulePeriod(List<ApiItem> data, string group) {
        var days = new Dictionary<DateOnly, DayScheduleEntity>();

        foreach (ApiItem item in data) {
            var subGroupItems = new List<SubGroupItemEntity>();
            if (item.SubGroup != null) {
                foreach (ApiSubGroup subItem in item.SubGroup){
                    var subGroupItem = new SubGroupItemEntity (
                        name: subItem.STitle,
                        description: subItem.STopic,
                        roomName: subItem.SGCaID,
                        subGroup: new GroupEntity(subItem.SGrID)
                    );
                    subGroupItems.Add(subGroupItem);
                }
            }

            var time_start = DateTime.ParseExact(item.start, "yyyy-MM-dd HH:mm", 
                CultureInfo.InvariantCulture);
            var time_end = DateTime.ParseExact(item.end, "yyyy-MM-dd HH:mm", 
                CultureInfo.InvariantCulture);


            var day_only = new DateOnly(time_end.Year, time_end.Month, time_end.Day);

            if (!days.ContainsKey(day_only)) {
                days[day_only] = new DayScheduleEntity(day_only, 
                    new GroupEntity(group), new List<ItemEntity>());
            }
                
            var itemEntity = new ItemEntity (
                name: item.title,
                description: item.topic,
                roomName: item.room,
                subGroupsItems: subGroupItems.Count != 0 ? subGroupItems : null,
                startTime: TimeOnly.FromDateTime(time_start),
                endTime: TimeOnly.FromDateTime(time_end)
            );
                
            days[day_only].Items.Add(itemEntity);
        }
        
        if (days.Count != 0){
            var daysEntities = new List<DayScheduleEntity>();
            foreach (var pair in days){
                daysEntities.Add(pair.Value);
            }
            return daysEntities;
        }
        return null;
    }
}