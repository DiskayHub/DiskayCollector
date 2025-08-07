using System.Globalization;
using DiskayCollector.CollegeAPI.Contracts;
using DiskayCollector.CollegeAPI.Contracts.Other;
using DiskayCollector.Core.Models;

namespace DiskayCollector.CollegeAPI.Data;

public class ScheduleFormatter {
    public static DayScheduleEntity? FormatScheduleDay(List<ApiItem> data, DateOnly dayFilter, string group) {
        var dayScheduleItems = DayScheduleEntity.Create(dayFilter, group, new List<ItemEntity>());
        var items = new List<ItemEntity>();

        foreach (ApiItem item in data) {
            var time_start = DateTime.ParseExact(item.start, "yyyy-MM-dd HH:mm", 
                CultureInfo.InvariantCulture);
            var time_end = DateTime.ParseExact(item.end, "yyyy-MM-dd HH:mm", 
                CultureInfo.InvariantCulture);
            
            if (DateOnly.FromDateTime(time_end) == dayFilter) {
                var itemEntity = ItemEntity.Create (
                    name: item.title,
                    description: item.topic,
                    roomName: item.room,
                    subGroupsItems: null,
                    startTime: TimeOnly.FromDateTime(time_start),
                    endTime: TimeOnly.FromDateTime(time_end),
                    dayScheduleId: dayScheduleItems.Id
                );
                
                var subGroupItems = new List<SubGroupItemEntity>();
                if (item.SubGroup != null) {
                    itemEntity.SubGroupsItems = new List<SubGroupItemEntity>();
                    foreach (ApiSubGroup subItem in item.SubGroup){
                        var subGroupItem = SubGroupItemEntity.Create (
                            name: subItem.STitle,
                            description: subItem.STopic,
                            roomName: subItem.SGCaID,
                            subGroup: subItem.SGrID,
                            itemId: itemEntity.Id
                        );
                        subGroupItems.Add(subGroupItem);
                        itemEntity.SubGroupsItems.Add(subGroupItem);
                    }
                }
                items.Add(itemEntity);
            }
        }
        if (items.Count != 0){
            foreach (var item in items){
                dayScheduleItems.Items.Add(item);
            }
            return dayScheduleItems;
        }
        return null;
    }

    public static List<DayScheduleEntity?> FormatSchedulePeriod(List<ApiItem> data, string group) {
        var days = new Dictionary<DateOnly, DayScheduleEntity>();
    
        foreach (ApiItem item in data) {
            var subGroupItems = new List<SubGroupItemEntity>();
            
            var time_start = DateTime.ParseExact(item.start, "yyyy-MM-dd HH:mm", 
                CultureInfo.InvariantCulture);
            var time_end = DateTime.ParseExact(item.end, "yyyy-MM-dd HH:mm", 
                CultureInfo.InvariantCulture);
            
            var day_only = new DateOnly(time_end.Year, time_end.Month, time_end.Day);
            
            if (!days.ContainsKey(day_only)) {
                days[day_only] = DayScheduleEntity.Create(day_only, 
                    group, new List<ItemEntity>());
            }
            
            var itemEntity = ItemEntity.Create(
                name: item.title,
                description: item.topic,
                roomName: item.room,
                subGroupsItems: null,
                startTime: TimeOnly.FromDateTime(time_start),
                endTime: TimeOnly.FromDateTime(time_end),
                dayScheduleId: days[day_only].Id
            );
            
            if (item.SubGroup != null) {
                foreach (ApiSubGroup subItem in item.SubGroup){
                    var subGroupItem = SubGroupItemEntity.Create (
                        name: subItem.STitle,
                        description: subItem.STopic,
                        roomName: subItem.SGCaID,
                        subGroup: subItem.SGrID,
                        itemId: itemEntity.Id
                    );
                    subGroupItems.Add(subGroupItem);
                }
                itemEntity.SubGroupsItems = subGroupItems;
            }
                
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