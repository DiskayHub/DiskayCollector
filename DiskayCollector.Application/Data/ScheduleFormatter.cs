using System.Globalization;
using DiskayCollector.Application.Contracts;
using DiskayCollector.Core.Models;

namespace DiskayCollector.Application.Data;

public class ScheduleFormatter {
    public static List<ItemEntity>? FormatScheduleItems(List<ApiItem> data, DateOnly? dayFilter = null) {
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

            if (dayFilter != null){
                if (DateOnly.FromDateTime(time_end) != dayFilter) {
                    continue;
                }
            }
            
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
        if (items.Count != 0) {
            return items;
        }
        return null;
    }
}